using System.Collections.Generic;
using System.Web.Http;
using Um416.API.Controllers.Base;
using Um416.BLL;

namespace Um416.API.Controllers
{
    public class VendasController : BaseController<Venda, VendaLogic>
    {
        [HttpGet, Route("api/clientes/{clienteId}/vendas")]
        public IEnumerable<Venda> ListPorCliente(long clienteId)
        {
            return _bo.ListPorCliente(clienteId);
        }

        [HttpGet, Route("api/empresas/{empresaId}/vendas")]
        public IEnumerable<Venda> ListPorEmpresa(long empresaId)
        {
            return _bo.ListPorEmpresa(empresaId);
        }

        [HttpGet, Route("api/vendas/lotes/{loteId}")]
        public Venda GetPorLote(long loteId)
        {
            return _bo.GetPorLote(loteId);
        }
    }
}