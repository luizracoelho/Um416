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
                var sql = "Select * From Loteamentos Left Join Imagens on Imagens.Id = Loteamentos.MapaId Where Loteamentos.Id = @Id";
                return db.Query<Loteamento, Imagem, Loteamento>(sql, (lot, img) =>
                {
                    lot.Mapa = img;
                    return lot;
                }, new { Id = id }).FirstOrDefault();
            }
        }
    }
}
