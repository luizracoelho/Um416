using Dapper;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Um416.DAL.Base;

namespace Um416.DAL
{
    public class ChamadoRepository : BaseRepository<Chamado>
    {
        public override IEnumerable<Chamado> List()
        {
            using (var db = new SqlConnection(ConnectionString))
            {
                var sql = "Select * From Chamados Left Join UsuariosClientes On UsuariosClientes.Id = Chamados.ClienteId Left Join UsuariosAdmins On UsuariosAdmins.Id = Chamados.AdminEncerramentoId Order By Encerrada, DataHoraCriacao Desc, DataHoraEncerramento;";

                return db.Query<Chamado, UsuarioCliente, UsuarioAdmin, Chamado>(sql, (chamado, cliente, admin) =>
                {
                    chamado.Cliente = cliente;
                    chamado.AdminEncerramento = admin;
                    return chamado;
                });
            }
        }

        public IEnumerable<Chamado> List(long id)
        {
            using (var db = new SqlConnection(ConnectionString))
            {
                var sql = "Select * From Chamados Left Join UsuariosClientes On UsuariosClientes.Id = Chamados.ClienteId Left Join UsuariosAdmins On UsuariosAdmins.Id = Chamados.AdminEncerramentoId Where Chamados.ClienteId = @ClienteId Order By Encerrada, DataHoraCriacao Desc, DataHoraEncerramento;";

                return db.Query<Chamado, UsuarioCliente, UsuarioAdmin, Chamado>(sql, (chamado, cliente, admin) =>
                {
                    chamado.Cliente = cliente;
                    chamado.AdminEncerramento = admin;
                    return chamado;
                }, new { ClienteId = id });
            }
        }

        public override Chamado Get(long id)
        {
            using (var db = new SqlConnection(ConnectionString))
            {
                var sql = "Select * From Chamados Left Join UsuariosClientes On UsuariosClientes.Id = Chamados.ClienteId Where Chamados.Id = @id;";

                return db.Query<Chamado, UsuarioCliente, Chamado>(sql, (chamado, cliente) =>
                {
                    chamado.Cliente = cliente;
                    return chamado;
                }, new { id }).FirstOrDefault();
            }
        }
    }
}
