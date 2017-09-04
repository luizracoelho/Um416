using System;
using System.Collections.Generic;
using Um416.BLL.Base;
using Um416.DAL;

namespace Um416.BLL
{
    public class LoteamentoLogic : BaseLogic<Loteamento, LoteamentoRepository>
    {
        public IEnumerable<Loteamento> List(long empresaId)
        {
            return _dao.List(empresaId);
        }

        protected override void Insert(Loteamento entity)
        {
            entity.DataCadastro = DateTime.Today;
            base.Insert(entity);
        }

        public override void Save(Loteamento entity)
        {
            if (entity.Mapa != null)
            {
                var imagemBo = new ImagemLogic();
                imagemBo.Save(entity.Mapa);
                entity.MapaId = entity.Mapa.Id;
            }

            base.Save(entity);
        }
    }
}
