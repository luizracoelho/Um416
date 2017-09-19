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
            using (var db = new SqlConnection(ConnectionString))
            {
                var query = "SELECT v.*, c.Nome FROM Vendas v LEFT JOIN UsuariosClientes c ON v.ClienteId = c.Id;";
                return db.Query<Venda, UsuarioCliente, Venda>(query, (venda, cli) =>
                {
                    venda.Cliente = cli;
                    return venda;
                }, splitOn: "nome");
            }
        }

        public override Venda Get(long id)
        {
            using (var db = new SqlConnection(ConnectionString))
            {
                var query = "SELECT v.*, c.Nome, l.Codigo, l.LoteamentoId, lo.Nome FROM Vendas v LEFT JOIN UsuariosClientes c ON v.ClienteId = c.Id LEFT JOIN Lotes l ON v.LoteId = l.Id LEFT JOIN Loteamentos lo ON l.LoteamentoId = lo.Id WHERE v.Id = @id;";
                return db.Query<Venda, UsuarioCliente, Lote, Loteamento, Venda>(query, (venda, cli, lote, loteamento) =>
                {
                    venda.Cliente = cli;
                    lote.Loteamento = loteamento;
                    venda.Lote = lote;
                    return venda;
                }, new { id = id }, splitOn: "Nome, Codigo, Nome").FirstOrDefault();
            }
        }

        public IEnumerable<Venda> List(long id)
        {
            using (var db = new SqlConnection(ConnectionString))
            {
                var query = "SELECT v.*, c.Nome, l.Codigo, l.LoteamentoId, lo.Nome FROM Vendas v LEFT JOIN UsuariosClientes c ON v.ClienteId = c.Id LEFT JOIN Lotes l ON v.LoteId = l.Id LEFT JOIN Loteamentos lo ON l.LoteamentoId = lo.Id;";
                return db.Query<Venda, UsuarioCliente, Lote, Loteamento, Venda>(query, (venda, cli, lote, loteamento) =>
                {
                    venda.Cliente = cli;
                    lote.Loteamento = loteamento;
                    venda.Lote = lote;
                    return venda;
                }, new { id = id }, splitOn: "Nome, Codigo, Nome");
            }
        }

        public override long Insert(Venda entity)
        {
            using (var db = new SqlConnection(ConnectionString))
            {
                var query = "select max(Numero) from Vendas where Vendas.LoteId = @id;";
                var ultimoNumero = db.Query<long?>(query, new { id = entity.LoteId }).FirstOrDefault();

                entity.Numero = ultimoNumero != null ? (long)ultimoNumero + 1 : 1;

                return base.Insert(entity);
            }
        }
    }
}
