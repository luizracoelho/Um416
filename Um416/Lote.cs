using Um416.Base;
using Um416.Enumerators;

namespace Um416
{
    public class Lote : BaseClass
    {
        public string Nome { get; set; }
        public string Codigo { get; set; }
        public string Descricao { get; set; }
        public decimal Area { get; set; }
        public decimal Valor { get; set; }
        public TipoLote TipoLote { get; set; }
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Uf { get; set; }
        public string Cep { get; set; }
        public virtual Loteamento Loteamento { get; set; }
        public long? LoteamentoId { get; set; }
        public string Cor { get; set; }
        public bool Comprado { get; set; }
        //Propriedade Visual
        public StatusAdicao StatusAdicao { get; set; }
    }
}
