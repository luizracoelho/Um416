using Um416.BLL.Base;
using Um416.DAL;

namespace Um416.BLL
{
    public class LoteamentoLogic : BaseLogic<Loteamento, LoteamentoRepository>
    {
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
