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

                lote.Comprado = true;
                loteBo.Save(lote);

                if (lote.LoteamentoId != null)
                    lote.Loteamento = loteamentoBo.Get((long)lote.LoteamentoId);

                entity.QuantParcelas = lote.Loteamento.QuantParcelas;

                if (entity.QuantParcelas < 1)
                    throw new Exception("A quantidade de parcelas deve ser maior que 0 (zero).");

                entity.Valor = lote.Valor;
                entity.ValorParcela = entity.Valor / entity.QuantParcelas;
                entity.DataHora = DateTime.Now;

                base.Insert(entity);

                var tituloBo = new TituloLogic();
                tituloBo.GerarTitulos(entity.Id);

                scope.Complete();
            }
        }

        public IEnumerable<Venda> List(long id)
        {
            var vendas = _dao.List(id);

            var parametroBo = new ParametroLogic();
            var parametro = parametroBo.Get(1);

            var tituloBo = new TituloLogic();
            

            foreach (var venda in vendas)
            {
                var titulos = new List<Titulo>();

                venda.Lote.Loteamento.Url = $"{parametro?.UrlVenda ?? ""}#!/loteamentos/{venda.Lote.LoteamentoId}/indicador/{venda.ClienteId}";
                venda.Lote.Loteamento.NomeHashtag = venda.Lote.Loteamento.Nome.ToHashtag();

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
    }
}
