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
                loteamento.Url = $"{parametro?.UrlVenda ?? ""}#!/loteamentos/{loteamento.Id}";
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
            try
            {
                Validar(entity);

                if (entity.Mapa?.Source != null)
                {
                    var imagemBo = new ImagemLogic();
                    imagemBo.Save(entity.Mapa);
                    entity.MapaId = entity.Mapa.Id;
                }

                base.Save(entity);
            }
            catch (Exception)
            {

                throw;
            }
        }

        private static void Validar(Loteamento entity)
        {
            if (string.IsNullOrEmpty(entity.Nome))
                throw new Exception("O campo Nome deve ser preenchido corretamente.");

            if (string.IsNullOrEmpty(entity.Descricao))
                throw new Exception("O campo Descrição deve ser preenchido corretamente.");

            if (entity.IndicadorMultinivel < 0)
                throw new Exception("O campo Indicador MMN deve ser maior que 0 (zero).");
        }
    }
}
