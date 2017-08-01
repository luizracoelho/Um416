using System;
using System.Collections.Generic;
using Um416.BLL.Base;
using Um416.DAL;

namespace Um416.BLL
{
    public class IteracaoChamadoLogic : BaseLogic<IteracaoChamado, IteracaoChamadoRepository>
    {
        public IEnumerable<IteracaoChamado> List(long id)
        {
            return _dao.List(id);
        }

        public override void Save(IteracaoChamado entity)
        {
            if (entity.Conteudo == null)
                throw new ArgumentNullException("Conteudo", "Campo obrigatório.");

            base.Save(entity);
        }

        protected override void Insert(IteracaoChamado entity)
        {
            //Preencher a data/hora atual
            entity.DataHora = DateTime.Now;

            base.Insert(entity);
        }

        protected override void Update(IteracaoChamado entity)
        {
            //Manter a data/hora original
            var iteracao = Get(entity.Id);
            entity.DataHora = iteracao.DataHora;

            base.Update(entity);
        }
    }
}