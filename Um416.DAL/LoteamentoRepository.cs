using Dapper;
using System.Data.SqlClient;
using System.Linq;
using Um416.DAL.Base;

namespace Um416.DAL
{
    public class LoteamentoRepository : BaseRepository<Loteamento>
    {
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
