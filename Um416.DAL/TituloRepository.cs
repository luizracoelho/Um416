using Dapper;
using System.Collections.Generic;
using System.Data.SqlClient;
using Um416.DAL.Base;

namespace Um416.DAL
{
    public class TituloRepository : BaseRepository<Titulo>
    {
        public IEnumerable<Titulo> List(long id)
        {
            using (var db = new SqlConnection(ConnectionString))
            {
                var query = "SELECT t.*, v.QuantParcelas, v.DiaVencimento, v.ClienteId, v.LoteId, v.ValorParcela, c.Nome, l.Codigo FROM Titulos t LEFT JOIN Vendas v ON t.VendaId = v.Id LEFT JOIN UsuariosClientes c ON v.ClienteId = c.Id LEFT JOIN Lotes l ON v.LoteId = l.Id WHERE t.VendaId = @id;";
                return db.Query<Titulo, Venda, UsuarioCliente, Lote, Titulo>(query, (titulo, venda, cli, lote) => {
                    venda.Cliente = cli;
                    venda.Lote = lote;
                    titulo.Venda = venda;
                    return titulo;
                }, new { id = id }, splitOn: "QuantParcelas, Nome, Codigo");
            }
        }
    }
}
