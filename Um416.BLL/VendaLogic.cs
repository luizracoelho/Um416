using System;
using Um416.BLL.Base;
using Um416.DAL;

namespace Um416.BLL
{
    public class VendaLogic : BaseLogic<Venda, VendaRepository>
    {
        public override void Save(Venda entity)
        {
            try
            {
                if (entity.QuantParcelas <= 0)
                    throw new Exception("A quantidade de parcelas deve ser maior que 0 (zero).");

                if (entity.DiaVencimento == null)
                    throw new Exception("A data de vencimento deve ser informada corretamente.");

                base.Save(entity);
            }
            catch (Exception)
            {

                throw;
            }
        }

        protected override void Insert(Venda entity)
        {
            var loteBo = new LoteLogic();
            var lote = loteBo.Get(entity.LoteId);

            entity.Valor = lote.Valor;
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
    }
}
