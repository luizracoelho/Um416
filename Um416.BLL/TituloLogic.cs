using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using Um416.BLL.Base;
using Um416.DAL;
using Um416.TransferModels;

namespace Um416.BLL
{
    public class TituloLogic : BaseLogic<Titulo, TituloRepository>
    {
        public IEnumerable<Titulo> List(long id) => _dao.List(id);

        public IEnumerable<Titulo> ListPorCliente(long clienteId, long? vendaId = null)
        {
            var titulos = _dao.ListPorCliente(clienteId, vendaId);

            if (titulos.Count() == 0)
                throw new Exception("Não foi possível encontrar parcelas solicitadas.");

            return titulos;
        }

        public void GerarTitulos(long vendaId)
        {
            var vendaBo = new VendaLogic();
            var venda = vendaBo.Get(vendaId);
            var titulos = new List<Titulo>();
            var dataVcto = DateTime.Today;

            if (dataVcto.Day > venda.DiaVencimento)
                dataVcto = dataVcto.AddMonths(1);

            dataVcto = Convert.ToDateTime($"{dataVcto.Year}-{dataVcto.Month}-{venda.DiaVencimento}");

            for (int i = 0; i < venda.QuantParcelas; i++)
            {
                var titulo = new Titulo
                {
                    Numero = i + 1,
                    DataVencimento = dataVcto.AddMonths(i),
                    Valor = venda.ValorParcela,
                    VendaId = vendaId
                };

                titulos.Add(titulo);
            }

            var diferencaValor = Math.Round(venda.Valor - titulos.Sum(x => x.Valor), 2);
            var ultimoTitulo = titulos.First();
            ultimoTitulo.Valor += diferencaValor;

            foreach (var titulo in titulos)
            {
                Save(titulo);
            }
        }

        public void BaixarTitulo(long tituloId, long empresaId)
        {
            // Obter o título
            var titulo = Get(tituloId, empresaId);

            // Se o boleto não tiver sido gerado, efetua o cálculo do valor líquido
            if (!titulo.BoletoGerado)
                titulo.ValorLiquido = CalcularValorLiquido(titulo);

            // Preencher os dados da baixa
            titulo.DataPgto = DateTime.Today;
            titulo.Pago = true;
            titulo.ValorPgto = titulo.ValorLiquido;

            // Baixar o título
            _dao.BaixarEstornar(titulo);
        }

        public void EstornarTitulo(long tituloId, long empresaId)
        {
            var titulo = Get(tituloId, empresaId);

            titulo.DataPgto = null;
            titulo.ValorPgto = null;
            titulo.Pago = false;

            _dao.BaixarEstornar(titulo);
        }

        private decimal CalcularPercentualDesconto(long vendaId)
        {
            var desconto = 0M;
            var diferenca = 0M;

            var vendaBo = new VendaLogic();
            var venda = vendaBo.Get(vendaId);
            var indicadorMMN = venda.Lote.Loteamento.IndicadorMultinivel;

            var nivelUm = vendaBo.ListPorIndicador(vendaId);

            foreach (var vendaUm in nivelUm)
            {
                if (vendaUm.Pagas > 0 && vendaUm.Vencidas == 0)
                {
                    var descUm = (0.5M / indicadorMMN);
                    desconto += descUm;

                    if (venda.Valor > vendaUm.Valor)
                        diferenca += descUm * ((100 - (vendaUm.Valor * 100 / venda.Valor)) / 100);
                }

                var nivelDois = vendaBo.ListPorIndicador(vendaUm.Id);

                foreach (var vendaDois in nivelDois)
                {
                    if (vendaDois.Pagas > 0 && vendaDois.Vencidas == 0)
                    {
                        var descDois = (0.5M / (indicadorMMN * indicadorMMN));
                        desconto += descDois;

                        if (vendaUm.Valor > vendaDois.Valor)
                            diferenca += descDois * ((100 - (vendaDois.Valor * 100 / vendaUm.Valor)) / 100);
                    }
                }
            }

            desconto -= diferenca;

            return desconto;
        }

        public decimal CalcularValorLiquido(Titulo titulo) =>
            titulo.Valor - (titulo.Valor * CalcularPercentualDesconto(titulo.VendaId));

        public Titulo Get(long tituloId, long empresaId) =>
            _dao.Get(tituloId, empresaId);

        public Titulo GetByCliente(long tituloId, long clienteId) =>
            _dao.GetByCliente(tituloId, clienteId);

        public IEnumerable<Titulo> Filtrar(TituloFiltroDTO filtro)
        {
            return _dao.Filtrar(filtro);
        }

        public string GerarBoleto(long tituloId, long clienteId)
        {
            // Obter o título
            var titulo = GetByCliente(tituloId, clienteId);

            // Calcular o valor líquido do título e atualizar no banco
            titulo.ValorLiquido = CalcularValorLiquido(titulo);
            Save(titulo);

            // Obter as configurações o boleto
            var logic = new ConfiguracaoBoletoLogic();
            var config = logic.Get();

            // Instanciar a classe do Itaú e retornar os dados criptografados
            var _itau = new Itaucripto.cripto();

            return _itau.geraDados(config.CodigoEmpresa, titulo.Id.ToString(), titulo.ValorLiquido.ToString("N2"), "3", config.Chave, titulo.Venda.Cliente.Nome, "01", titulo.Venda.Cliente.Cpf, $"{titulo.Venda.Cliente.Logradouro}, {titulo.Venda.Cliente.Numero}", titulo.Venda.Cliente.Bairro, titulo.Venda.Cliente.Cep, titulo.Venda.Cliente.Cidade, titulo.Venda.Cliente.Uf, titulo.DataVencimento.ToString("ddMMyyyy"), config.UrlRetorna, config.Observacao1, config.Observacao2, config.Observacao3);
        }

        public void ConsultarBoleto(long tituloId, long clienteId)
        {
            // Obter o título
            var titulo = GetByCliente(tituloId, clienteId);

            // Obter as configurações o boleto
            var logic = new ConfiguracaoBoletoLogic();
            var config = logic.Get();

            // Instanciar a classe do Itaú e retornar os dados criptografados
            var _itau = new Itaucripto.cripto();

            // Obter os dados da consulta criptografados
            var dc = _itau.geraConsulta(config.CodigoEmpresa.ToUpper(), titulo.Id.ToString(), "1", config.Chave.ToUpper());

            try
            {
                Stream requestStream = null;
                WebResponse response = null;
                StreamReader reader = null;

                var request = WebRequest.Create("https://shopline.itau.com.br/shopline/consulta.aspx");
                request.Method = WebRequestMethods.Http.Post;
                request.ContentType = "application/x-www-form-urlencoded";
                byte[] byteBuffer = null;

                var postData = String.Format("DC={0}", dc);

                byteBuffer = Encoding.UTF8.GetBytes(postData);
                //timeout recomendado para efetuar requisicao
                request.Timeout = 60000;
                request.ContentLength = byteBuffer.Length;
                requestStream = request.GetRequestStream();
                requestStream.Write(byteBuffer, 0, byteBuffer.Length);
                requestStream.Close();

                response = request.GetResponse();
                var responseStream = response.GetResponseStream();
                var encoding = Encoding.Default;
                reader = new StreamReader(responseStream, encoding);

                var xmlDocument = new XmlDocument();
                xmlDocument.Load(reader);

                string tipPag = string.Empty; // 00 - Não escolhido / 01 - Pagamento à vista / 02 - boleto / 03 - cartão de crédito
                string sitPag = string.Empty; // 00 - pagamento efetuado / 01 - não finalizada (tente novamente) / 02 - erro no processamento da consulta (tente novamente) / 03 - pagamento não localizado / 04 - boleto emitido com sucesso / 05 - pagamento efetuado, aguardando compensação / 06 - pagamento não compensado
                string dtPag = string.Empty; // ddmmaaaa
                string valorPago = string.Empty;

                // Preencher as variáveis
                var nosDaTransacao = xmlDocument.GetElementsByTagName("PARAM");
                foreach (XmlNode xnTransacao in nosDaTransacao)
                {
                    var id = xnTransacao.Attributes["ID"];
                    var value = xnTransacao.Attributes["VALUE"];

                    switch (id.InnerText)
                    {
                        case "tipPag":
                            tipPag = value.InnerText;
                            break;

                        case "sitPag":
                            sitPag = value.InnerText;
                            break;

                        case "dtPag":
                            dtPag = Regex.Replace(value.InnerText, @"(\d{2})(\d{2})(\d{2})", "$3-$2-$1"); // Definir como aaaa-mm-dd
                            break;

                        case "Valor":
                            valorPago = value.InnerText;
                            break;
                    }
                }

                // Pagamento a vista ou  cartão de credito
                if (tipPag == "01" || tipPag == "03")
                {
                    // Atualizar o título
                    titulo.BoletoGerado = true;
                    Save(titulo);

                    // Baixar o título
                    titulo.Pago = true;
                    titulo.DataPgto = Convert.ToDateTime(dtPag);
                    BaixarTitulo(titulo.Id, titulo.Venda.Lote.Loteamento.EmpresaId);
                }

                // Pagamento com boleto bancario
                if (tipPag == "02")
                {
                    if (sitPag == "00") // 00 - Pagamento efetuado
                    {
                        // Atualizar o título
                        titulo.BoletoGerado = true;
                        Save(titulo);

                        // Baixar o título
                        titulo.Pago = true;
                        titulo.DataPgto = Convert.ToDateTime(dtPag);
                        BaixarTitulo(titulo.Id, titulo.Venda.Lote.Loteamento.EmpresaId);
                    }

                    if (sitPag == "04") // 04 - Boleto emitido com sucesso
                    {
                        // Atualizar o título
                        titulo.BoletoGerado = true;
                        Save(titulo);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void ConsultarBoletos()
        {
            var titulos = _dao.GetTitulosEmAbertoBoletosGerados();
            foreach (var titulo in titulos)
                ConsultarBoleto(titulo.Id, titulo.Venda.ClienteId);
        }
    }
}
