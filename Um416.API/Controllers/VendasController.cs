using System.Collections.Generic;
using System.Web.Http;
using Um416.API.Controllers.Base;
using Um416.BLL;

namespace Um416.API.Controllers
{
    public class VendasController : BaseController<Venda, VendaLogic>
    {
        [HttpGet, Route("api/clientes/{clienteId}/vendas")]
        public IEnumerable<Venda> ListPorCliente(long clienteId) => _bo.ListPorCliente(clienteId);

        [HttpGet, Route("api/empresas/{empresaId}/vendas")]
        public IEnumerable<Venda> ListPorEmpresa(long empresaId) => _bo.ListPorEmpresa(empresaId);

        [HttpGet, Route("api/vendas/lotes/{loteId}")]
        public Venda GetPorLote(long loteId) => _bo.GetPorLote(loteId);

        [HttpGet, Route("api/clientes/{clienteId}/arvores/{vendaId}")]
        public IEnumerable<Venda> GetArvores(long clienteId, long? vendaId) => _bo.GetArvores(clienteId, vendaId);
    }
}