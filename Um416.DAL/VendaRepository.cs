using Dapper;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Um416.DAL.Base;

namespace Um416.DAL
{
    public class VendaRepository : BaseRepository<Venda>
    {
        public override IEnumerable<Venda> List()
        {
            using (var db = new SqlConnection())
            {
                var query = "SELECT v.*, c.Nome FROM Vendas v LEFT JOIN UsuariosClientes c ON v.ClienteId = c.Id;";
                return db.Query<Venda, UsuarioCliente, Venda>(query, (venda, cli) => {
                    venda.Cliente = cli;
                    return venda;
                }, splitOn: "Nome");
            }
        }

        public override Venda Get(long id)
        {
            using (var db = new SqlConnection())
            {
                var query = "SELECT v.*, c.Nome FROM Vendas v LEFT JOIN UsuariosClientes c ON v.ClienteId = c.Id;";
                return db.Query<Venda, UsuarioCliente, Venda>(query, (venda, cli) => {
                    venda.Cliente = cli;
                    return venda;
                }, splitOn: "Nome").FirstOrDefault();
            }
        }
    }
}
