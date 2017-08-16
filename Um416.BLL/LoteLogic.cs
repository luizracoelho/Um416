using Svg;
using Svg.Pathing;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Transactions;
using System.Xml.Linq;
using Um416.BLL.Base;
using Um416.BLL.Tools;
using Um416.DAL;
using Um416.Enumerators;

namespace Um416.BLL
{
    public class LoteLogic : BaseLogic<Lote, LoteRepository>
    {
        public virtual IEnumerable<Lote> List(long? id)
        {
            return _dao.List(id);
        }

        public IEnumerable<Lote> Generate(long id)
        {
            //Criar a lista de lotes
            var lotes = new List<Lote>();

            //Instanciar o bo do loteamneto e recuperar o loteamento selecionado
            var loteamentoBo = new LoteamentoLogic();
            var loteamento = loteamentoBo.Get(id);

            //Caso o loteamento não possua mapa retorna nulo
            if (loteamento.MapaId == null)
                return null;

            //Formata o source do mapa removendo os carateres adicionados de base64
            var rgx = new Regex(@"data:image/svg\+xml;base64,?");
            var source = rgx.Replace(loteamento.Mapa.Source, "");

            //Converte o base64 em um array de bytes
            var buffer = Convert.FromBase64String(source);
            using (var ms = new MemoryStream(buffer))
            {
                //Carrega o arquivo SVG (que na verdade é um XML)
                var doc = XDocument.Load(ms);

                //Selecione os retangulos (rects) do svg, calcula a area e adiciona o lote
                var rects = doc.Descendants().Where(x => x.Name.LocalName == "rect");
                foreach (var rect in rects)
                {
                    var width = Convert.ToDecimal(rect.Attribute("width").Value);
                    var height = Convert.ToDecimal(rect.Attribute("height").Value);

                    lotes.Add(new Lote
                    {
                        Codigo = rect.Attribute("id").Value,
                        Area = Area.Retangulo(width, height),
                        StatusAdicao = StatusAdicao.Adicao
                    });
                }

                //Selecione os caminhos (paths) do svg, calcula a area e adiciona o lote
                var paths = doc.Descendants().Where(x => x.Name.LocalName == "path");
                foreach (var path in paths)
                {
                    var d = SvgPathBuilder.Parse(path.Attribute("d").Value);

                    var points = new List<PointF>();
                    foreach (var seg in d)
                    {
                        if (seg != d.Last)
                        {
                            points.Add(new PointF(seg.Start.X, seg.Start.Y));
                            points.Add(new PointF(seg.End.X, seg.End.Y));
                        }

                        if (seg.GetType() == typeof(SvgCubicCurveSegment))
                        {
                            var cubic = (SvgCubicCurveSegment)seg;

                            points.Add(new PointF(cubic.FirstControlPoint.X, cubic.FirstControlPoint.Y));
                            points.Add(new PointF(cubic.SecondControlPoint.X, cubic.SecondControlPoint.Y));
                        }
                    }

                    points.Add(points[0]);
                    var area = (int)Math.Abs(points.Take(points.Count - 1)
                       .Select((p, i) => (points[i + 1].X - p.X) * (points[i + 1].Y + p.Y))
                       .Sum() / 2);

                    lotes.Add(new Lote
                    {
                        Codigo = path.Attribute("id").Value,
                        Area = area,
                        StatusAdicao = StatusAdicao.Adicao
                    });
                }
            }

            //Recuperar os lotes do loteamento
            var lotesDb = List(id).ToList();

            //Marcar os lotes como alteração
            foreach (var loteDb in lotesDb)
            {
                var lote = lotes.FirstOrDefault(x => x.Codigo == loteDb.Codigo);

                if (lote != null)
                {
                    loteDb.StatusAdicao = StatusAdicao.Alteracao;
                    lotes.Insert(lotes.IndexOf(lote), loteDb);
                    lotes.Remove(lote);
                }

            }

            //Marcar os lotes como remoção
            var lotesRemocao = lotesDb.Where(x => !lotes.Select(l => l.Codigo).Contains(x.Codigo));
            foreach (var lote in lotesRemocao)
                lote.StatusAdicao = StatusAdicao.Remocao;

            //Retornar a lista de lotes 
            return lotes.OrderBy(x => x.Codigo);
        }

        public void Save(IList<Lote> lotes)
        {
            using (var scope = new TransactionScope())
            {
                //Salvar os lotes marcadors como adição ou alteração
                foreach (var lote in lotes)
                    Save(lote);

                //Remover os lotes marcados para remoção
                var lotesRemoverIds = lotes.Where(x => x.StatusAdicao == StatusAdicao.Remocao).Select(x => x.Id);
                foreach (var id in lotesRemoverIds)
                    Delete(id);

                //Definir a transação como completa
                scope.Complete();
            }
        }
    }
}
