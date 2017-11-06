using System;

namespace Um416.TransferModels
{
    public class TituloFiltroDTO
    {
        public long ClienteId { get; set; }
        public DateTime? DataEmissaoDe { get; set; }
        public DateTime? DataEmissaoAte { get; set; }
        public DateTime? DataVencimentoDe { get; set; }
        public DateTime? DatavencimentoAte { get; set; }
        public DateTime? DataPgtoDe { get; set; }
        public DateTime? DataPgtoAte { get; set; }
        public bool? Status { get; set; }
    }
}
