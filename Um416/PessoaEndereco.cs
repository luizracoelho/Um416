using System;

namespace Um416
{
    public class PessoaEndereco : Pessoa
    {
        public DateTime DataCadastro { get; set; }
        public string Rg { get; set; }
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Uf { get; set; }
        public string Cep { get; set; }
    }
}
