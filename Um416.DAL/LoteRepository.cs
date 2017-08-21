using Dapper;
using System.Collections.Generic;
using System.Data.SqlClient;
using Um416.DAL.Base;
using System;

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

                return db.Query<Lote, Loteamento, Lote>(query, (lote, loteamento) => {
                    lote.Loteamento = loteamento;
                    return lote;
                }, new { LoteamentoId = id });
            }
        }
    }
}
