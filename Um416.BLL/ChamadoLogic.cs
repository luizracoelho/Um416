using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Um416.BLL.Base;
using Um416.DAL;
using Um416.Enumerators;

namespace Um416.BLL
{
    public class ChamadoLogic : BaseLogic<Chamado, ChamadoRepository>
    {
        public override Chamado Get(long id)
        {
            var chamado = base.Get(id);

            if (chamado != null)
            {
                var iteracaoBo = new IteracaoChamadoLogic();
                chamado.IteracoesChamados = iteracaoBo.List(chamado.Id)?.ToList();
            }

            return chamado;
        }

        public IEnumerable<Chamado> List(long id)
        {
            return _dao.List(id);
        }

        public override void Save(Chamado entity)
        {
            //Encerramento do Chamado
            entity.DataHoraEncerramento = DateTime.Now;
            entity.Encerrado = true;
            entity.TipoEncerramento = entity.AdminEncerramentoId == null ? TipoEncerramento.Admin : TipoEncerramento.Cliente;

            base.Save(entity);
        }

        public void Save(Chamado entity, IteracaoChamado iteracao)
        {
            if (entity.Titulo == null)
                throw new ArgumentNullException("Titulo", "Campo obrigatório.");

            if (entity.Id == 0)
            {
                //Preencher a data/hora atual
                entity.DataHoraCriacao = DateTime.Now;

                base.Insert(entity);
            }

            //Adicionar a iteração inicial do chamado
            iteracao.ChamadoId = entity.Id;
            iteracao.DataHora = DateTime.Now;

            var iteracaoBo = new IteracaoChamadoLogic();
            iteracaoBo.Save(iteracao);
        }
    }
}
