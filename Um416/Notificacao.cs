using System;
using System.Collections.Generic;
using Um416.Base;

namespace Um416
{
    public class Notificacao : BaseClass
    {
        public string Titulo { get; set; }
        public string Mensagem { get; set; }
        public DateTime DataHora { get; set; }
        public virtual IList<LeituraNotificacao> LeiturasNotificacoes { get; set; }
    }
}
