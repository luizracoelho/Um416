using System;
using Um416.Base;

namespace Um416
{
    public class IteracaoChamado : BaseClass
    {
        public string Conteudo { get; set; }
        public DateTime DataHora { get; set; }
        public virtual UsuarioAdmin Admin { get; set; }
        public long? AdminId { get; set; }
        public virtual Chamado Chamado { get; set; }
        public long ChamadoId { get; set; }
    }
}