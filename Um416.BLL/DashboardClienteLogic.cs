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
            var vendas = _vendaBo.ListPorCliente(clienteId);
            var titulos = _tituloBo.ListPorCliente(clienteId);
            var diretas = GetIndicacoesDiretas(vendas);

            var dashboard = new DashboardClienteDTO
            {
                //Compras
                QuantCompras = vendas.Count(),
                QuantMMNAtivo = GetQuantMMNAtivo(vendas),
                ValorTotalCompras = GetValorTotalCompras(vendas),
                ValorDescontoCompras = GetValorDescontoCompras(titulos),

                //Árvores
                QuantIndicacoesDiretas = diretas.Count(),
                QuantIndicacoesIndiretas = GetIndicacoesIndiretas(diretas).Count(),
                QuantIndicacoesDiretasAtencao = GetQuantIndicacoesDiretasAtencao(diretas),
                QuantIndicacoesIndiretasAtencao = GetQuantIndicacoesIndiretasAtencao(diretas),

                //Débitos
                //--Neste mês
                QuantParcelasVencendo = GetQuantParcelasVencendo(titulos),
                ValorParcelasVencendo = GetValorParcelasVencendo(titulos),
                DiaVctoProximo = GetDiaVctoProximo(titulos),
                ValorVencendoDiaProximo = GetValorVencendoDiaProximo(titulos),
                //--Geral
                QuantAbertas = GetQuantAbertas(titulos),
                ValorAbertas = GetValorAbertas(titulos),
                QuantPagas = GetQuantPagas(titulos),
                ValorPagas = GetValorPagas(titulos)
            };

            return dashboard;
        }

        //Compras
        private decimal GetValorTotalCompras(IEnumerable<Venda> vendas)
        {
            return vendas.Select(x => x.Valor).Sum();
        }

        private decimal GetValorDescontoCompras(IEnumerable<Titulo> titulos)
        {
            var titulosPagos = titulos.Where(x => x.Pago);

            return (titulosPagos.Select(x => x.Valor).Sum() - titulosPagos.Select(x => (decimal)x.ValorPgto).Sum());
        }

        private int GetQuantMMNAtivo(IEnumerable<Venda> vendas)
        {
            return vendas.Where(x => x.Pagas > 0 && x.Vencidas == 0).Count();
        }

        //Árvores
        private IEnumerable<Venda> GetIndicacoesDiretas(IEnumerable<Venda> vendas)
        {
            var diretas = new List<Venda>();

            foreach (var venda in vendas)
            {
                var indicadas = _vendaBo.ListPorIndicador(venda.Id);
                diretas.AddRange(indicadas);
            }

            return diretas;
        }

        private IEnumerable<Venda> GetIndicacoesIndiretas(IEnumerable<Venda> diretas)
        {
            var indiretas = new List<Venda>();

            foreach (var direta in diretas)
            {
                indiretas.AddRange(_vendaBo.ListPorIndicador(direta.Id));
            }

            return indiretas;
        }

        private int GetQuantIndicacoesDiretasAtencao(IEnumerable<Venda> diretas)
        {
            return diretas.Where(x => x.Pagas == 0 || x.Vencidas != 0).Count();
        }

        private int GetQuantIndicacoesIndiretasAtencao(IEnumerable<Venda> diretas)
        {
            var indiretas = GetIndicacoesIndiretas(diretas);

            return indiretas.Where(x => x.Pagas == 0 || x.Vencidas != 0).Count();
        }

        //Débitos
        //--Neste mês
        private int GetQuantParcelasVencendo(IEnumerable<Titulo> titulos)
        {
            var data = DateTime.Today;
            var dataInicial = new DateTime(data.Year, data.Month, 1);
            var dataFinal = new DateTime(data.Year, data.Month, dataInicial.AddMonths(1).AddDays(-1).Day);

            return titulos.Where(x => x.DataVencimento >= dataInicial && x.DataVencimento <= dataFinal).Count();
        }

        private decimal GetValorParcelasVencendo(IEnumerable<Titulo> titulos)
        {
            var data = DateTime.Today;
            var dataInicial = new DateTime(data.Year, data.Month, 1);
            var dataFinal = new DateTime(data.Year, data.Month, DateTime.DaysInMonth(data.Year, data.Month));

            return titulos.Where(x => x.DataVencimento >= dataInicial && x.DataVencimento <= dataFinal).Select(x => x.Valor).Sum();
        }

        private int GetDiaVctoProximo(IEnumerable<Titulo> titulos)
        {
            return titulos.FirstOrDefault(x => x.DataVencimento >= DateTime.Today).DataVencimento.Day;
        }

        private decimal GetValorVencendoDiaProximo(IEnumerable<Titulo> titulos)
        {
            var data = new DateTime(DateTime.Today.Year, DateTime.Today.Month, GetDiaVctoProximo(titulos));

            return titulos.Where(x => x.DataVencimento == data).Select(x => x.Valor).Sum();
        }

        //--Geral
        private int GetQuantAbertas(IEnumerable<Titulo> titulos)
        {
            return titulos.Where(x => !x.Pago).Count();
        }

        private decimal GetValorAbertas(IEnumerable<Titulo> titulos)
        {
            return titulos.Where(x => !x.Pago).Select(x => x.Valor).Sum();
        }

        private int GetQuantPagas(IEnumerable<Titulo> titulos)
        {
            return titulos.Where(x => x.Pago).Count();
        }

        private decimal GetValorPagas(IEnumerable<Titulo> titulos)
        {
            return titulos.Where(x => x.Pago).Select(x => (decimal)x.ValorPgto).Sum();
        }
    }
}
