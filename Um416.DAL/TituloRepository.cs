using Dapper;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Um416.DAL.Base;

namespace Um416.DAL
{
    public class TituloRepository : BaseRepository<Titulo>
    {
        public IEnumerable<Titulo> List(long id)
        {
            using (var db = new SqlConnection(ConnectionString))
            {
                var query = "SELECT t.*, v.QuantParcelas, v.DiaVencimento, v.ClienteId, v.LoteId, v.ValorParcela, c.Nome, l.Codigo, lo.Nome FROM Titulos t LEFT JOIN Vendas v ON t.VendaId = v.Id LEFT JOIN UsuariosClientes c ON v.ClienteId = c.Id LEFT JOIN Lotes l ON v.LoteId = l.Id LEFT JOIN Loteamentos lo ON l.LoteamentoId = lo.Id WHERE t.VendaId = @id;";
                return db.Query<Titulo, Venda, UsuarioCliente, Lote, Loteamento, Titulo>(query, (titulo, venda, cli, lote, loteamento) => {
                    venda.Cliente = cli;
                    lote.Loteamento = loteamento;
                    venda.Lote = lote;
                    titulo.Venda = venda;
                    return titulo;
                }, new { id = id }, splitOn: "QuantParcelas, Nome, Codigo, Nome");
            }
        }

        public IEnumerable<Titulo> ListPorCliente(long clienteId, long id)
        {
            using (var db = new SqlConnection(ConnectionString))
            {
                var query = "SELECT t.*, v.QuantParcelas, v.DiaVencimento, v.ClienteId, v.LoteId, v.ValorParcela, c.Nome, l.Codigo, lo.Nome FROM Titulos t LEFT JOIN Vendas v ON t.VendaId = v.Id LEFT JOIN UsuariosClientes c ON v.ClienteId = c.Id LEFT JOIN Lotes l ON v.LoteId = l.Id LEFT JOIN Loteamentos lo ON l.LoteamentoId = lo.Id WHERE t.VendaId = @id AND v.ClienteId = @clienteId;";
                return db.Query<Titulo, Venda, UsuarioCliente, Lote, Loteamento, Titulo>(query, (titulo, venda, cli, lote, loteamento) => {
                    venda.Cliente = cli;
                    lote.Loteamento = loteamento;
                    venda.Lote = lote;
                    titulo.Venda = venda;
                    return titulo;
                }, new { clienteId = clienteId, id = id }, splitOn: "QuantParcelas, Nome, Codigo, Nome");
            }
        }

        public override Titulo Get(long id)
        {
            using (var db = new SqlConnection(ConnectionString))
            {
                var query = "SELECT t.*, v.QuantParcelas, v.Numero, v.DiaVencimento, v.ClienteId, v.LoteId, v.ValorParcela FROM Titulos t LEFT JOIN Vendas v ON t.VendaId = v.Id WHERE t.Id = @id;";
                return db.Query<Titulo, Venda, Titulo>(query, (titulo, venda) => {
                    titulo.Venda = venda;
                    return titulo;
                }, new { id = id }, splitOn: "QuantParcelas").FirstOrDefault();
            }
        }

        public Titulo Get(long tituloId, long empresaId)
        {
            using (var db = new SqlConnection(ConnectionString))
            {
                var query = @"SELECT t.*, v.Id, v.QuantParcelas, v.Numero, v.DiaVencimento, v.ClienteId, v.LoteId, v.ValorParcela, l.Id, lo.IndicadorMultinivel FROM Titulos t
                                LEFT JOIN Vendas v ON t.VendaId = v.Id
                                LEFT JOIN Lotes l ON v.LoteId = l.Id
                                LEFT JOIN Loteamentos lo on l.LoteamentoId = lo.Id
                                WHERE t.Id = @tituloId AND lo.EmpresaId = @empresaId;";
                return db.Query<Titulo, Venda, Lote, Loteamento, Titulo>(query, (tit, venda, lote, loteam) =>
                {
                    lote.Loteamento = loteam;
                    venda.Lote = lote;
                    tit.Venda = venda;
                    return tit;
                }, new { tituloId = tituloId, empresaId = empresaId }, splitOn:"Id, Id, IndicadorMultinivel" ).FirstOrDefault();
            }
        }

        public void BaixarEstornar(Titulo titulo)
        {
            using (var db = new SqlConnection(ConnectionString))
            {
                var query = "UPDATE Titulos SET DataPgto = @DataPgto, ValorPgto = @ValorPgto, Pago = @Pago WHERE VendaId = @VendaId AND Id = @Id";
                db.Query(query, titulo);
            }
        }
    }
}
