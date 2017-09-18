using System;
using System.Collections.Generic;
using System.Transactions;
using Um416.BLL.Base;
using Um416.BLL.Tools;
using Um416.DAL;

namespace Um416.BLL
{
    public class VendaLogic : BaseLogic<Venda, VendaRepository>
    {
        public override void Save(Venda entity)
        {
            try
            {
                if (entity.DiaVencimento == 0)
                    throw new Exception("A data de vencimento deve ser informada corretamente.");

                if (entity.QuantParcelas < 1)
                    throw new Exception("A quantidade de parcelas deve ser maior que 0 (zero).");

                Insert(entity);
            }
            catch (Exception)
            {

                throw;
            }
        }

        protected override void Insert(Venda entity)
        {
            using (var scope = new TransactionScope())
            {
                var loteBo = new LoteLogic();
                var lote = loteBo.Get(entity.LoteId);
                lote.Comprado = true;
                loteBo.Save(lote);

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

            foreach (var venda in vendas)
            {
                venda.Lote.Loteamento.Url = $"{parametro?.UrlVenda ?? ""}#!/loteamentos/{venda.Lote.LoteamentoId}/indicador/{venda.ClienteId}";
                venda.Lote.Loteamento.NomeHashtag = venda.Lote.Loteamento.Nome.ToHashtag();
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
