using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using Um416.BLL.Base;
using Um416.BLL.Tools;
using Um416.DAL;

namespace Um416.BLL
{
    public class VendaLogic : BaseLogic<Venda, VendaRepository>
    {
        TituloLogic _tituloBo;

        public VendaLogic()
        {
            _tituloBo = new TituloLogic();
        }

        protected override void Insert(Venda entity)
        {
            if (entity.DiaVencimento == 0)
                throw new Exception("O dia de vencimento deve ser informada corretamente.");

            using (var scope = new TransactionScope())
            {
                var loteBo = new LoteLogic();
                var loteamentoBo = new LoteamentoLogic();
                var lote = loteBo.Get(entity.LoteId);

                if (lote.Comprado)
                    throw new Exception("O lote selecionado foi comprado por outra pessoa enquanto você concluía sua aquisição.\nPor favor, selecione outro lote.");

                lote.Comprado = true;
                loteBo.Save(lote);

                if (lote.LoteamentoId != null)
                    lote.Loteamento = loteamentoBo.Get((long)lote.LoteamentoId);

                if (entity.QuantParcelas < 1 || entity.QuantParcelas > lote.Loteamento.QuantParcelas)
                    throw new Exception($"A quantidade de parcelas deve estar entre 1 e {lote.Loteamento.QuantParcelas}.");

                entity.Valor = lote.Valor;
                entity.ValorParcela = entity.Valor / entity.QuantParcelas;
                entity.DataHora = DateTime.Now;

                base.Insert(entity);

                var tituloBo = new TituloLogic();
                tituloBo.GerarTitulos(entity.Id);

                scope.Complete();
            }
        }

        public IEnumerable<Venda> ListPorCliente(long clienteId)
        {
            var vendas = _dao.ListPorCliente(clienteId);

            var parametroBo = new ParametroLogic();
            var parametro = parametroBo.Get(1);

            foreach (var venda in vendas)
            {
                venda.Lote.Loteamento.Url = $"{parametro?.UrlVenda ?? ""}#!/loteamentos/{venda.Lote.LoteamentoId}/indicador/{venda.Id}";
                venda.Lote.Loteamento.NomeHashtag = venda.Lote.Loteamento.Nome.ToHashtag();

                //Validar a Venda
                Validar(venda);
            }

            return vendas;
        }

        public IEnumerable<Venda> ListPorEmpresa(long empresaId)
        {
            var vendas = _dao.ListPorCliente(empresaId);

            var parametroBo = new ParametroLogic();
            var parametro = parametroBo.Get(1);

            var tituloBo = new TituloLogic();


            foreach (var venda in vendas)
            {
                var titulos = new List<Titulo>();
                titulos = tituloBo.List(venda.Id).ToList();
                venda.Pagas = titulos.Where(x => x.Pago == true).Count();
            }

            return vendas;
        }

        public override bool Delete(long id)
        {
            var venda = Get(id);
            var loteBo = new LoteLogic();
            var lote = loteBo.Get(venda.LoteId);

            lote.Comprado = false;

            loteBo.Save(lote);

            return base.Delete(id);
        }

        public Venda GetPorLote(long loteId)
        {
            return _dao.GetPorLote(loteId);
        }

        public IEnumerable<Venda> GetArvores(long clienteId)
        {
            var vendas = ListPorCliente(clienteId);
            var arvores = new List<Venda>();

            foreach (var venda in vendas)
            {
                var indicadorMultinivel = venda.Lote.Loteamento.IndicadorMultinivel;

                venda.VendasIndicadas = ListPorIndicador(venda.Id);
                if (venda.VendasIndicadas.Count < indicadorMultinivel)
                {
                    var diferenca = indicadorMultinivel - venda.VendasIndicadas.Count;
                    for (int i = 0; i < diferenca; i++)
                        venda.VendasIndicadas.Add(new Venda());
                }

                foreach (var vendaIndicada in venda.VendasIndicadas)
                {
                    vendaIndicada.VendasIndicadas = ListPorIndicador(vendaIndicada.Id);
                    if (vendaIndicada.VendasIndicadas.Count < indicadorMultinivel)
                    {
                        var diferenca = indicadorMultinivel - vendaIndicada.VendasIndicadas.Count;
                        for (int i = 0; i < diferenca; i++)
                            vendaIndicada.VendasIndicadas.Add(new Venda());
                    }
                }

                arvores.Add(venda);
            }

            return arvores;
        }

        public List<Venda> ListPorIndicador(long vendaId)
        {
            var vendas = _dao.ListPorIndicador(vendaId).ToList();

            foreach (var venda in vendas)
            {
                //Validar a Venda
                Validar(venda);
            }

            return vendas;
        }

        public override Venda Get(long id)
        {
            var venda = base.Get(id);

            //Validar a venda 
            Validar(venda);

            return venda;
        }

        private void Validar(Venda venda)
        {
            venda.Valida = true;
            var sb = new StringBuilder();

            if (venda.Pagas == 0)
            {
                venda.Valida = false;
                sb.Append("- Pague a primeira parcela de seu lote para habilitar a indicação e conseguir descontos.\n");
            }

            if (venda.Vencidas > 0)
            {
                venda.Valida = false;
                sb.Append("- Existe(m) parcela(s) vencida(s). Os descontos referentes à essa indicação estão temporariamente indisponíveis.\n");
            }

            venda.Mensagem = sb.ToString();
        }
    }
}
