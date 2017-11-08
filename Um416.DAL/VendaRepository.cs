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
                var query = @"SELECT v.*, (SELECT COUNT(*) FROM Titulos WHERE Pago = 1 AND VendaId = v.Id) Pagas, (SELECT COUNT(*) FROM Titulos WHERE Pago = 0 AND DataVencimento < GETDATE() AND VendaId = v.Id) Vencidas, c.Nome, c.Email, c.TelFixo, c.TelMovel, l.Codigo, l.LoteamentoId, l.Descricao, l.Area, lo.Nome, lo.IndicadorMultinivel 
                                FROM Vendas v 
                                LEFT JOIN UsuariosClientes c ON v.ClienteId = c.Id 
                                LEFT JOIN Lotes l ON v.LoteId = l.Id 
                                LEFT JOIN Loteamentos lo ON l.LoteamentoId = lo.Id
                                WHERE v.Id = @id;";
                return db.Query<Venda, UsuarioCliente, Lote, Loteamento, Venda>(query, (venda, cli, lote, loteamento) =>
                {
                    venda.Cliente = cli;
                    lote.Loteamento = loteamento;
                    venda.Lote = lote;
                    return venda;
                }, new { id = id }, splitOn: "Nome, Codigo, Nome").FirstOrDefault();
            }
        }

        public IEnumerable<Venda> ListPorCliente(long clienteId)
        {
            using (var db = new SqlConnection(ConnectionString))
            {
                var query = "SELECT v.*, (SELECT COUNT(*) FROM Titulos WHERE Pago = 1 AND VendaId = v.Id) Pagas, c.Nome, c.Login, l.Codigo, l.LoteamentoId, lo.Nome, lo.IndicadorMultinivel, lo.EmpresaId FROM Vendas v LEFT JOIN UsuariosClientes c ON v.ClienteId = c.Id LEFT JOIN Lotes l ON v.LoteId = l.Id LEFT JOIN Loteamentos lo ON l.LoteamentoId = lo.Id WHERE c.Id = @id;";
                return db.Query<Venda, UsuarioCliente, Lote, Loteamento, Venda>(query, (venda, cli, lote, loteamento) =>
                {
                    venda.Cliente = cli;
                    lote.Loteamento = loteamento;
                    venda.Lote = lote;
                    return venda;
                }, new { id = clienteId }, splitOn: "Nome, Codigo, Nome");
            }
        }

        public IEnumerable<Venda> ListPorEmpresa(long empresaId)
        {
            using (var db = new SqlConnection(ConnectionString))
            {
                var query = "SELECT v.*, c.Nome, l.Codigo, l.LoteamentoId, lo.Nome FROM Vendas v LEFT JOIN UsuariosClientes c ON v.ClienteId = c.Id LEFT JOIN Lotes l ON v.LoteId = l.Id LEFT JOIN Loteamentos lo ON l.LoteamentoId = lo.Id WHERE lo.EmpresaId = @id;";
                return db.Query<Venda, UsuarioCliente, Lote, Loteamento, Venda>(query, (venda, cli, lote, loteamento) =>
                {
                    venda.Cliente = cli;
                    lote.Loteamento = loteamento;
                    venda.Lote = lote;
                    return venda;
                }, new { id = empresaId }, splitOn: "Nome, Codigo, Nome");
            }
        }

        public IEnumerable<Venda> ListPorIndicador(long vendaId)
        {
            using (var db = new SqlConnection(ConnectionString))
            {
                var query = "SELECT v.*, (SELECT COUNT(*) FROM Titulos WHERE Pago = 1 AND VendaId = v.Id) Pagas, (SELECT COUNT(*) FROM Titulos WHERE Pago = 0 AND DataVencimento < GETDATE() AND VendaId = v.Id) Vencidas, c.Nome, c.Cpf, c.TelFixo, c.TelMovel, c.Email, l.Codigo, l.LoteamentoId, lo.Nome, lo.IndicadorMultinivel FROM Vendas v LEFT JOIN UsuariosClientes c ON v.ClienteId = c.Id LEFT JOIN Lotes l ON v.LoteId = l.Id LEFT JOIN Loteamentos lo ON l.LoteamentoId = lo.Id WHERE v.IndicadorId = @id;";
                return db.Query<Venda, UsuarioCliente, Lote, Loteamento, Venda>(query, (venda, cli, lote, loteamento) =>
                {
                    venda.Cliente = cli;
                    lote.Loteamento = loteamento;
                    venda.Lote = lote;
                    return venda;
                }, new { id = vendaId }, splitOn: "Nome, Codigo, Nome");
            }
        }

        public override long Insert(Venda entity)
        {
            using (var db = new SqlConnection(ConnectionString))
            {
                var query = @"SELECT MAX(v.Numero) FROM Vendas v
                                LEFT JOIN Lotes l ON v.LoteId = l.Id
                                LEFT JOIN Loteamentos lo ON l.LoteamentoId = lo.Id
                                LEFT JOIN Empresas e ON lo.EmpresaId = e.Id
                                WHERE e.Id = 1;";
                var ultimoNumero = db.Query<long?>(query, new { id = entity.LoteId }).FirstOrDefault();

                entity.Numero = ultimoNumero != null ? (long)ultimoNumero + 1 : 1;

                return base.Insert(entity);
            }
        }

        public Venda GetPorLote(long loteId)
        {
            using (var db = new SqlConnection(ConnectionString))
            {
                var query = "SELECT v.*, c.Nome, c.Cpf, c.TelFixo, c.TelMovel, c.Email, l.Codigo, l.LoteamentoId, lo.Nome, lo.IndicadorMultinivel, lo.EmpresaId FROM Vendas v LEFT JOIN UsuariosClientes c ON v.ClienteId = c.Id LEFT JOIN Lotes l ON v.LoteId = l.Id LEFT JOIN Loteamentos lo ON l.LoteamentoId = lo.Id WHERE v.LoteId = @id;";
                return db.Query<Venda, UsuarioCliente, Lote, Loteamento, Venda>(query, (venda, cli, lote, loteamento) =>
                {
                    venda.Cliente = cli;
                    lote.Loteamento = loteamento;
                    venda.Lote = lote;
                    return venda;
                }, new { id = loteId }, splitOn: "Nome, Codigo, Nome").FirstOrDefault();
            }
        }

        public Venda GetPorCliente(long clienteId, long vendaId)
        {
            using (var db = new SqlConnection(ConnectionString))
            {
                var query = @"SELECT v.*, (SELECT COUNT(*) FROM Titulos WHERE Pago = 1 AND VendaId = v.Id) Pagas, (SELECT COUNT(*) FROM Titulos WHERE Pago = 0 AND DataVencimento < GETDATE() AND VendaId = v.Id) Vencidas, c.Nome, c.Email, c.TelFixo, c.TelMovel, l.Codigo, l.LoteamentoId, l.Descricao, l.Area, lo.Nome, lo.IndicadorMultinivel 
                                FROM Vendas v 
                                LEFT JOIN UsuariosClientes c ON v.ClienteId = c.Id 
                                LEFT JOIN Lotes l ON v.LoteId = l.Id 
                                LEFT JOIN Loteamentos lo ON l.LoteamentoId = lo.Id
                                WHERE v.Id = @vendaId AND c.Id = @clienteId;";
                return db.Query<Venda, UsuarioCliente, Lote, Loteamento, Venda>(query, (venda, cli, lote, loteamento) =>
                {
                    venda.Cliente = cli;
                    lote.Loteamento = loteamento;
                    venda.Lote = lote;
                    return venda;
                }, new { vendaId = vendaId, clienteId = clienteId }, splitOn: "Nome, Codigo, Nome").FirstOrDefault();
            }
        }
    }
}
