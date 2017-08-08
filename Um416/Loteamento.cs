using System;
using Um416.Base;

namespace Um416
{
    public class Loteamento : BaseClass
    {
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public DateTime DataCadastro { get; set; }
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Uf { get; set; }
        public string Cep { get; set; }
        public virtual Imagem Mapa { get; set; }
        public long? MapaId { get; set; }
    }
}
