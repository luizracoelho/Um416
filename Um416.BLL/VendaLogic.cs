using System;
using System.Collections.Generic;
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
                if (entity.Id == 0)
                {
                    var loteBo = new LoteLogic();
                    var lote = loteBo.Get(entity.LoteId);

                    entity.Valor = lote.Valor;
                }

                if (entity.DiaVencimento == 0)
                    throw new Exception("A data de vencimento deve ser informada corretamente.");

                if (entity.QuantParcelas > 0)
                    entity.ValorParcela = entity.Valor / entity.QuantParcelas;
                else
                    throw new Exception("A quantidade de parcelas deve ser maior que 0 (zero).");

                base.Save(entity);
            }
            catch (Exception)
            {

                throw;
            }
        }

        protected override void Insert(Venda entity)
        {
            entity.DataHora = DateTime.Now;

            base.Insert(entity);
        }

        protected override void Update(Venda entity)
        {
            var vendaBd = Get(entity.Id);

            entity.Valor = vendaBd.Valor;
            entity.DataHora = vendaBd.DataHora;

            base.Update(entity);
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
    }
}
