using System;
using System.Collections.Generic;
using Um416.Enumerators;

namespace Um416
{
    public class UsuarioCliente : PessoaEndereco
    {
        public DateTime DataNascimento { get; set; }
        public Sexo Sexo { get; set; }
        public virtual IList<Chamado> Chamados { get; set; }
    }
}
