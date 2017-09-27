using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using Um416.BLL.Base;
using Um416.BLL.Tools;
using Um416.DAL;

namespace Um416.BLL
{
    public class VendaLogic : BaseLogic<Venda, VendaRepository>
    {
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

            var tituloBo = new TituloLogic();


            foreach (var venda in vendas)
            {
                var titulos = new List<Titulo>();

                venda.Lote.Loteamento.Url = $"{parametro?.UrlVenda ?? ""}#!/loteamentos/{venda.Lote.LoteamentoId}/indicador/{venda.Cliente.Login}";
                venda.Lote.Loteamento.NomeHashtag = venda.Lote.Loteamento.Nome.ToHashtag();

                titulos = tituloBo.List(venda.Id).ToList();

                venda.Pagas = titulos.Where(x => x.Pago == true).Count();
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
    }
}
