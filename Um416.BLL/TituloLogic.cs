using System;
using System.Collections.Generic;
using System.Linq;
using Um416.BLL.Base;
using Um416.DAL;

namespace Um416.BLL
{
    public class TituloLogic : BaseLogic<Titulo, TituloRepository>
    {
        public IEnumerable<Titulo> List(long id)
        {
            return _dao.List(id);
        }

        public void GerarTitulos(long vendaId)
        {
            var vendaBo = new VendaLogic();
            var venda = vendaBo.Get(vendaId);
            var titulos = new List<Titulo>();
            var dataVcto = DateTime.Today;

            if (dataVcto.Day > venda.DiaVencimento)
               dataVcto = dataVcto.AddMonths(1);

            dataVcto = Convert.ToDateTime($"{dataVcto.Year}-{dataVcto.Month}-{venda.DiaVencimento}");

            for (int i = 0; i < venda.QuantParcelas; i++)
            {
                var titulo = new Titulo
                {
                    Numero = i + 1,
                    DataVencimento = dataVcto.AddMonths(i),
                    Valor = venda.ValorParcela,
                    VendaId = vendaId
                };

                titulos.Add(titulo);
            }

            var diferencaValor = Math.Round(venda.Valor - titulos.Sum(x => x.Valor), 2);
            var ultimoTitulo = titulos.First();
            ultimoTitulo.Valor += diferencaValor;

            foreach (var titulo in titulos)
            {
                Save(titulo);
            }
        }

        public void BaixarTitulo(long tituloId)
        {
            var titulo = Get(tituloId);

            titulo.DataPgto = DateTime.Today;
            titulo.ValorPgto = titulo.Valor;
            titulo.Pago = true;

            Save(titulo);
        }

        public void EstornarTitulo(long tituloId)
        {
            var titulo = Get(tituloId);

            titulo.DataPgto = null;
            titulo.ValorPgto = null;
            titulo.Pago = false;

            Save(titulo);
        }
    }
}
