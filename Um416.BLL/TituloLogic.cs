using System;
using System.Collections.Generic;
using System.Linq;
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
            var titulo = Get(tituloId, empresaId);

            titulo.DataPgto = DateTime.Today;
            titulo.Pago = true;
            titulo.ValorLiquido = CalcularValorLiquido(titulo);
            titulo.ValorPgto = CalcularValorLiquido(titulo);

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

        public decimal CalcularPercentualDesconto(long vendaId)
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

        public IEnumerable<Titulo> Filtrar(TituloFiltroDTO filtro)
        {
            return _dao.Filtrar(filtro);
        }

        public string GerarBoleto(Titulo titulo)
        {
            var codEmpresa = "J0196936980001040000027166";
            var chave = "O1n3S5e5r9vGs109";
            var urlRetorna = "";
            var obsAdicional1 = "Nao receber após vencimento";
            var obsAdicional2 = "Nao receber após vencimento";
            var obsAdicional3 = "Nao receber após vencimento";

            var _itau = new Itaucripto.cripto();

            return _itau.geraDados(codEmpresa, titulo.Numero.ToString(), titulo.ValorLiquido.ToString("N2"), "3", chave, titulo.Venda.Cliente.Nome, "01", titulo.Venda.Cliente.Cpf, $"{titulo.Venda.Cliente.Logradouro}, {titulo.Venda.Cliente.Numero}", titulo.Venda.Cliente.Bairro, titulo.Venda.Cliente.Cep, titulo.Venda.Cliente.Cidade, titulo.Venda.Cliente.Uf, titulo.DataVencimento.ToString("ddMMyyyy"), urlRetorna, obsAdicional1, obsAdicional2, obsAdicional3);
        }
    }
}
