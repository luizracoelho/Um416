using Dapper;
using System.Data.SqlClient;
using System.Linq;
using Um416.DAL.Base;
using System.Collections.Generic;

namespace Um416.DAL
{
    public class LoteamentoRepository : BaseRepository<Loteamento>
    {
        public IEnumerable<Loteamento> List(long empresaId)
        {
            using (var db = new SqlConnection(ConnectionString))
            {
                var sql = "Select * From Loteamentos Where Loteamentos.EmpresaId = @EmpresaId";
                return db.Query<Loteamento>(sql, new { EmpresaId = empresaId });
            }
        }

        public override Loteamento Get(long id)
        {
            using (var db = new SqlConnection(ConnectionString))
            {
                var query = "Select * From Loteamentos Left Join Imagens on Imagens.Id = Loteamentos.MapaId Where Loteamentos.Id = @LoteamentoId";
                var loteamento = db.Query<Loteamento, Imagem, Loteamento>(query, (loteam, img) =>
                {
                    loteam.Mapa = img;
                    return loteam;
                }, new { LoteamentoId = id }).FirstOrDefault();

                var queryLotes = "Select * From Lotes Where Lotes.LoteamentoId = @LoteamentoId;";
                loteamento.Lotes = db.Query<Lote>(queryLotes, new { LoteamentoId = id }).ToList();

                return loteamento;
            }
        }
    }
}
