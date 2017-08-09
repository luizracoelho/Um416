using System.Collections.Generic;
using System.Web.Http;
using Um416.API.Controllers.Base;
using Um416.BLL;

namespace Um416.API.Controllers
{
    [Authorize]
    public class LotesController : BaseController<Lote, LoteLogic>
    {
        [HttpGet, Route("api/loteamentos/{id}/lotes")]
        public IEnumerable<Lote> GetByLoteamento(long? id)
        {
            return _bo.List(id);
        }
    }
}