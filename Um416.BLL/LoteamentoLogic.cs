using System;
using System.Collections.Generic;
using Um416.BLL.Base;
using Um416.BLL.Tools;
using Um416.DAL;

namespace Um416.BLL
{
    public class LoteamentoLogic : BaseLogic<Loteamento, LoteamentoRepository>
    {
        public IEnumerable<Loteamento> List(long empresaId)
        {
            var loteamentos = _dao.List(empresaId);

            var parametroBo = new ParametroLogic();
            var parametro = parametroBo.Get(1);

            foreach (var loteamento in loteamentos)
            {
                loteamento.Url = $"{parametro.UrlVenda}loteamentos/{loteamento.Id}/adquira";
                loteamento.NomeHashtag = loteamento.Nome.ToHashtag();
            }

            return loteamentos;
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
