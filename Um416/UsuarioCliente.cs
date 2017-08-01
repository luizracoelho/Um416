using System;
using System.Collections.Generic;
using Um416.Enumerators;

namespace Um416
{
    public class UsuarioCliente : Usuario
    {
        public DateTime DataCadastro { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Rg { get; set; }
        public Sexo Sexo { get; set; }
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Uf { get; set; }
        public string Cep { get; set; }
        public virtual IList<Chamado> Chamados { get; set; }
    }
}
