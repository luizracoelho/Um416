using System;
using System.Collections.Generic;

namespace Um416.TransferModels
{
    public class DashboardClienteDTO
    {
        public bool SituacaoCadastral { get; set; }
        public IList<Venda> VendasMMNAtivo { get; set; }
        public decimal GanhoDoDia { get; set; }
        public decimal GanhoDoMes { get; set; }
        public decimal GanhoTotal { get; set; }
        public Dictionary<string, int> Indicacoes { get; set; }
    }
}
