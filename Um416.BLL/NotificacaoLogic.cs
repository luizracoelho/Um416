using System;
using System.Collections.Generic;
using Um416.BLL.Base;
using Um416.DAL;

namespace Um416.BLL
{
    public class NotificacaoLogic : BaseLogic<Notificacao, NotificacaoRepository>
    {
        public override void Save(Notificacao entity)
        {
            if (entity.Titulo == null)
                throw new ArgumentNullException("Titulo", "Campo obrigatório.");

            if (entity.Mensagem == null)
                throw new ArgumentNullException("Mensagem", "Campo obrigatório.");

            base.Save(entity);
        }

        protected override void Insert(Notificacao entity)
        {
            entity.DataHora = DateTime.Now;

            base.Insert(entity);

            //Adicionar as leituras da notificação
            var leituraBo = new LeituraNotificacaoLogic();
            var clienteBo = new UsuarioClienteLogic();

            var clientes = clienteBo.List();

            foreach (var cliente in clientes)
            {
                leituraBo.Save(new LeituraNotificacao
                {
                    ClienteId = cliente.Id,
                    NotificacaoId = entity.Id,
                    Lida = false
                });
            }
        }

        protected override void Update(Notificacao entity)
        {
            //Mantera a data/hora original
            var notificacao = Get(entity.Id);
            entity.DataHora = notificacao.DataHora;

            base.Update(entity);
        }

        public IEnumerable<NotificacaoLeitura> List(long id)
        {
            return _dao.List(id);
        }

        public void Read(long id, long clienteId)
        {
            var leituraBo = new LeituraNotificacaoLogic();

            var leitura = leituraBo.Get(x => x.NotificacaoId == id && x.ClienteId == clienteId);

            if (leitura.Lida)
            {
                leitura.Lida = false;
                leitura.DataHoraLeitura = null;
            }
            else
            {
                leitura.Lida = true;
                leitura.DataHoraLeitura = DateTime.Now;
            }

            leituraBo.Save(leitura);
        }
    }
}
