namespace Um416.TransferModels
{
    public class DashboardClienteDTO
    {
        //Compras
        public int QuantCompras { get; set; }
        public int QuantMMNAtivo { get; set; }
        public decimal ValorTotalCompras { get; set; }
        public decimal ValorDescontoCompras { get; set; }

        //Árvores
        public int QuantIndicacoesDiretas { get; set; }
        public int QuantIndicacoesDiretasAtencao { get; set; }
        public int QuantIndicacoesIndiretas { get; set; }
        public int QuantIndicacoesIndiretasAtencao { get; set; }

        //Débitos
        //--Neste mês
        public int QuantParcelasVencendo { get; set; }
        public decimal ValorParcelasVencendo { get; set; }
        public int DiaVctoProximo { get; set; }
        public decimal ValorVencendoDiaProximo { get; set; }

        //--Geral
        public int QuantAbertas { get; set; }
        public decimal ValorAbertas { get; set; }
        public int QuantPagas { get; set; }
        public decimal ValorPagas { get; set; }
    }
}
