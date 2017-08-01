using System;

namespace Um416
{
    public class NotificacaoLeitura : Notificacao
    {
        public bool Lida { get; set; }
        public DateTime? DataHoraLeitura { get; set; }
    }
}
