using Dapper;
using System.Collections.Generic;
using System.Data.SqlClient;
using Um416.DAL.Base;

namespace Um416.DAL
{
    public class IteracaoChamadoRepository : BaseRepository<IteracaoChamado>
    {
        public IEnumerable<IteracaoChamado> List(long id)
        {
            using (var db = new SqlConnection(ConnectionString))
            {
                var sql = "Select * From IteracoesChamados Left Join UsuariosAdmins On UsuariosAdmins.Id = IteracoesChamados.AdminId Where IteracoesChamados.ChamadoId = @id;";

                return db.Query<IteracaoChamado, UsuarioAdmin, IteracaoChamado>(sql, (chamado, admin) =>
                {
                    chamado.Admin = admin;
                    return chamado;
                }, new { id });
            }
        }
    }
}
