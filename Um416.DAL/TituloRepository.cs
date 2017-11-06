using Dapper;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Um416.DAL.Base;
using Um416.TransferModels;

namespace Um416.DAL
{
    public class TituloRepository : BaseRepository<Titulo>
    {
        public IEnumerable<Titulo> List(long vendaId)
        {
            using (var db = new SqlConnection(ConnectionString))
            {
                var query = "SELECT t.*, v.QuantParcelas, v.DiaVencimento, v.ClienteId, v.LoteId, v.ValorParcela, c.Nome, l.Codigo, lo.Nome, lo.EmpresaId FROM Titulos t LEFT JOIN Vendas v ON t.VendaId = v.Id LEFT JOIN UsuariosClientes c ON v.ClienteId = c.Id LEFT JOIN Lotes l ON v.LoteId = l.Id LEFT JOIN Loteamentos lo ON l.LoteamentoId = lo.Id WHERE t.VendaId = @vendaId;";
                return db.Query<Titulo, Venda, UsuarioCliente, Lote, Loteamento, Titulo>(query, (titulo, venda, cli, lote, loteamento) =>
                {
                    venda.Cliente = cli;
                    lote.Loteamento = loteamento;
                    venda.Lote = lote;
                    titulo.Venda = venda;
                    return titulo;
                }, new { vendaId = vendaId }, splitOn: "QuantParcelas, Nome, Codigo, Nome");
            }
        }

        public IEnumerable<Titulo> ListPorCliente(long clienteId, long? vendaId)
        {
            using (var db = new SqlConnection(ConnectionString))
            {
                if (vendaId != null)
                {
                    var query = "SELECT t.*, v.QuantParcelas, v.DiaVencimento, v.ClienteId, v.LoteId, v.ValorParcela, c.Nome, l.Codigo, lo.Nome, lo.EmpresaId FROM Titulos t LEFT JOIN Vendas v ON t.VendaId = v.Id LEFT JOIN UsuariosClientes c ON v.ClienteId = c.Id LEFT JOIN Lotes l ON v.LoteId = l.Id LEFT JOIN Loteamentos lo ON l.LoteamentoId = lo.Id WHERE t.VendaId = @vendaId AND v.ClienteId = @clienteId;";
                    return db.Query<Titulo, Venda, UsuarioCliente, Lote, Loteamento, Titulo>(query, (titulo, venda, cli, lote, loteamento) =>
                    {
                        venda.Cliente = cli;
                        lote.Loteamento = loteamento;
                        venda.Lote = lote;
                        titulo.Venda = venda;
                        return titulo;
                    }, new { clienteId = clienteId, vendaId = vendaId }, splitOn: "QuantParcelas, Nome, Codigo, Nome");
                }
                else
                {
                    var query = @"SELECT t.*, v.QuantParcelas, v.DiaVencimento, v.ClienteId, v.LoteId, v.ValorParcela, c.Nome, l.Codigo, lo.Nome FROM Titulos t 
                                    LEFT JOIN Vendas v ON t.VendaId = v.Id 
                                    LEFT JOIN UsuariosClientes c ON v.ClienteId = c.Id 
                                    LEFT JOIN Lotes l ON v.LoteId = l.Id 
                                    LEFT JOIN Loteamentos lo ON l.LoteamentoId = lo.Id 
                                    WHERE v.ClienteId = @clienteId
                                    ORDER BY t.Pago, t.DataVencimento;";
                    return db.Query<Titulo, Venda, UsuarioCliente, Lote, Loteamento, Titulo>(query, (titulo, venda, cli, lote, loteamento) =>
                    {
                        venda.Cliente = cli;
                        lote.Loteamento = loteamento;
                        venda.Lote = lote;
                        titulo.Venda = venda;
                        return titulo;
                    }, new { clienteId = clienteId }, splitOn: "QuantParcelas, Nome, Codigo, Nome");
                }
            }
        }

        public override Titulo Get(long id)
        {
            using (var db = new SqlConnection(ConnectionString))
            {
                var query = "SELECT t.*, v.QuantParcelas, v.Numero, v.DiaVencimento, v.ClienteId, v.LoteId, v.ValorParcela FROM Titulos t LEFT JOIN Vendas v ON t.VendaId = v.Id WHERE t.Id = @id;";
                return db.Query<Titulo, Venda, Titulo>(query, (titulo, venda) =>
                {
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
                }, new { tituloId = tituloId, empresaId = empresaId }, splitOn: "Id, Id, IndicadorMultinivel").FirstOrDefault();
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

        public IEnumerable<Titulo> Filtrar(TituloFiltroDTO filtro)
        {
            using (var db = new SqlConnection(ConnectionString))
            {
                var query = new StringBuilder(@"SELECT t.*, v.Id, v.ClienteId, v.QuantParcelas, v.DiaVencimento, v.LoteId, v.ValorParcela, c.Nome, l.Codigo, lo.Nome, lo.EmpresaId FROM Titulos t LEFT JOIN Vendas v ON t.VendaId = v.Id LEFT JOIN UsuariosClientes c ON v.ClienteId = c.Id LEFT JOIN Lotes l ON v.LoteId = l.Id LEFT JOIN Loteamentos lo ON l.LoteamentoId = lo.Id WHERE v.ClienteId = @ClienteId");

                if (filtro.DataEmissaoDe != null && filtro.DataEmissaoAte != null)
                    query.Append(" AND v.DataHora >= @DataEmissaoDe AND v.DataHora <= @DataEmissaoAte");

                if (filtro.DataVencimentoDe != null && filtro.DatavencimentoAte != null)
                    query.Append(" AND t.DataVencimento >= @DataVencimentoDe AND t.DataVencimento <= @DataVencimentoAte");

                if (filtro.DataPgtoDe != null && filtro.DataPgtoAte != null)
                    query.Append(" AND t.DataPgto >= @DataPgtoDe AND t.DataPgto <= @DataPgtoAte");

                if (filtro.Status != null)
                    query.Append(" AND t.Pago = @Status");

                query.Append(" ORDER BY t.Pago, t.DataVencimento;");

                return db.Query<Titulo, Venda, UsuarioCliente, Lote, Loteamento, Titulo>(query.ToString(), (tit, venda, cliente,  lote, loteamento) =>
                {
                    lote.Loteamento = loteamento;
                    venda.Lote = lote;
                    venda.Cliente = cliente;
                    tit.Venda = venda;
                    return tit;
                }, filtro, splitOn: "Id, Nome, Codigo, Nome");
            }
        }
    }
}
