using System;
using System.Collections.Generic;
using Um416.Base;
using Um416.Enumerators;

namespace Um416
{
    public class Chamado : BaseClass
    {
        public string Titulo { get; set; }
        public DateTime DataHoraCriacao { get; set; }
        public DateTime? DataHoraEncerramento { get; set; }
        public TipoEncerramento? TipoEncerramento { get; set; }
        public virtual UsuarioAdmin AdminEncerramento { get; set; }
        public long? AdminEncerramentoId { get; set; }
        public bool Encerrado { get; set; }
        public virtual UsuarioCliente Cliente { get; set; }
        public long ClienteId { get; set; }
        public virtual IList<IteracaoChamado> IteracoesChamados { get; set; }
    }
}
