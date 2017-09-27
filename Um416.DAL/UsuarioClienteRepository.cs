using Dapper;
using System.Data.SqlClient;
using System.Linq;
using Um416.DAL.Base;

namespace Um416.DAL
{
    public class UsuarioClienteRepository : BaseRepository<UsuarioCliente>
    {
        public UsuarioCliente GetByLogin(string login)
        {
            using (var db = new SqlConnection(ConnectionString))
            {
                var query = "select * from UsuariosClientes where Login = @login;";
                return db.Query<UsuarioCliente>(query, new { login = login }).FirstOrDefault();
            }
        }
    }
}
