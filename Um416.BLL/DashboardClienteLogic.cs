using System;
using System.Collections.Generic;
using System.Linq;
using Um416.TransferModels;

namespace Um416.BLL
{
    public class DashboardClienteLogic
    {
        private VendaLogic _vendaBo;
        private TituloLogic _tituloBo;

        public DashboardClienteLogic()
        {
            _vendaBo = new VendaLogic();
            _tituloBo = new TituloLogic();
        }

        public DashboardClienteDTO GetDashboard(long clienteId)
        {
            var vendasMMNAtivo = _vendaBo.ListMMNAtivoPorCliente(clienteId).ToList();
            var titulos = _tituloBo.ListPorCliente(clienteId);

            var dashboard = new DashboardClienteDTO
            {
                SituacaoCadastral = vendasMMNAtivo != null,
                VendasMMNAtivo = vendasMMNAtivo,
                GanhoDoDia = GetGanhosDoDia(titulos),
                GanhoDoMes = GetGanhosDoMes(titulos),
                GanhoTotal = GetGanhosTotal(titulos),
                Indicacoes = GetIndicacoes(vendasMMNAtivo)
            };

            return dashboard;
        }

        private decimal GetGanhosTotal(IEnumerable<Titulo> titulos)
        {
            var titulosPagos = titulos.Where(x => x.Pago);

            return (titulosPagos.Select(x => x.Valor).Sum() - titulosPagos.Select(x => (decimal)x.ValorPgto).Sum());
        }

        private decimal GetGanhosDoMes(IEnumerable<Titulo> titulos)
        {
            var hoje = DateTime.Today;
            var primeiroDiaDoMes = new DateTime(hoje.Year, hoje.Month, 1);
            var ultimoDiaDoMes = new DateTime(hoje.Year, hoje.Month, DateTime.DaysInMonth(hoje.Year, hoje.Month));

            var titulosPagos = titulos.Where(x => x.Pago && x.DataPgto >= primeiroDiaDoMes && x.DataPgto <= ultimoDiaDoMes);

            return (titulosPagos.Select(x => x.Valor).Sum() - titulosPagos.Select(x => (decimal)x.ValorPgto).Sum());
        }

        private decimal GetGanhosDoDia(IEnumerable<Titulo> titulos)
        {
            var titulosPagos = titulos.Where(x => x.Pago && x.DataPgto == DateTime.Today);

            return (titulosPagos.Select(x => x.Valor).Sum() - titulosPagos.Select(x => (decimal)x.ValorPgto).Sum());
        }

        private Dictionary<string, int> GetIndicacoes(IEnumerable<Venda> vendas)
        {
            var seisMesesAtras = DateTime.Today.AddMonths(-6);
            var indicacoes = new List<Venda>();

            foreach (var venda in vendas)
            {
                var indicacoesDiretas = _vendaBo.ListPorIndicador(venda.Id);

                indicacoes.AddRange(indicacoesDiretas);

                foreach (var indicacaoDireta in indicacoesDiretas)
                {
                    if (indicacaoDireta.DataHora >= seisMesesAtras)
                    {
                        indicacoes.Add(indicacaoDireta);

                        var indicacoesIndiretas = _vendaBo.ListPorIndicador(indicacaoDireta.Id);

                        foreach (var indicacaoIndireta in indicacoesIndiretas)
                        {
                            if (indicacaoIndireta.DataHora >= seisMesesAtras)
                                indicacoes.Add(indicacaoIndireta);
                        }
                    }
                }
            }

            return indicacoes.GroupBy(x => x.DataHora.ToString("MM/yyyy")).ToDictionary(g => g.Key, g => g.Count());
        }
    }
}
