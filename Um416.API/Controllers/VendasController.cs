using System.Collections.Generic;
using System.Web.Http;
using Um416.API.Controllers.Base;
using Um416.BLL;

namespace Um416.API.Controllers
{
    public class VendasController : BaseController<Venda, VendaLogic>
    {
        [HttpGet, Route("api/clientes/{clienteId}/vendas")]
        public IEnumerable<Venda> GetPorCliente(long clienteId)
        {
            return _bo.List(clienteId);
        }
    }
}