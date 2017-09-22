using Dapper;
using System.Collections.Generic;
using System.Data.SqlClient;
using Um416.DAL.Base;
using System;
using System.Linq;

namespace Um416.DAL
{
    public class LoteRepository : BaseRepository<Lote>
    {
        public IEnumerable<Lote> List(long? id)
        {
            using (var db = new SqlConnection(ConnectionString))
            {
                var filter = id == null ? " IS NULL" : " = @LoteamentoId";
                var query = $"Select * From Lotes Left Join Loteamentos On Loteamentos.Id = Lotes.LoteamentoId Where Lotes.LoteamentoId { filter } Order By Lotes.Codigo";

                return db.Query<Lote, Loteamento, Lote>(query, (lote, loteamento) =>
                {
                    lote.Loteamento = loteamento;
                    return lote;
                }, new { LoteamentoId = id });
            }
        }

        public override Lote Get(long id)
        {
            using (var db = new SqlConnection(ConnectionString))
            {
                var query = "SELECT l.*, lo.QuantParcelas FROM Lotes l LEFT JOIN Loteamentos lo ON l.LoteamentoId = lo.Id WHERE l.Id = @loteId; ";
                return db.Query<Lote, Loteamento, Lote>(query, (lote, loteamento) =>
                {
                    lote.Loteamento = loteamento;
                    return lote;
                }, new { loteId = id }, splitOn: "QuantParcelas").FirstOrDefault();
            }
        }
    }
}
