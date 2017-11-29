using System;
using Um416.Base;

namespace Um416
{
    public class Titulo : BaseClass
    {
        public int Numero { get; set; }
        public decimal Valor { get; set; }
        public decimal ValorLiquido { get; set; }
        public DateTime DataVencimento { get; set; }
        public bool Pago { get; set; }
        public DateTime? DataPgto { get; set; }
        public decimal? ValorPgto { get; set; }
        public long VendaId { get; set; }
        public Venda Venda { get; set; }
    }
}
