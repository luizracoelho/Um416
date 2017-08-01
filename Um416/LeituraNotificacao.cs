using System;
using Um416.Base;

namespace Um416
{
    public class LeituraNotificacao : BaseClass
    {
        public virtual Notificacao Notificacao { get; set; }
        public long NotificacaoId { get; set; }
        public virtual UsuarioCliente Cliente { get; set; }
        public long ClienteId { get; set; }
        public DateTime? DataHoraLeitura { get; set; }
        public bool Lida { get; set; }
    }
}