using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
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

        [HttpGet, Route("api/loteamentos/{id}/generate")]
        public IEnumerable<Lote> GenerateLotes(long? id)
        {
            if (id == null)
                return null;

            return _bo.Generate((long)id);
        }

        [HttpPost, Route("api/lotes/multiple")]
        public void PostMultiple(IList<Lote> lotes)
        {
            _bo.Save(lotes);
        }

        [HttpDelete, Route("api/lotes/multiple")]
        public void DeleteMultiple(long[] ids)
        {
            _bo.Delete(ids);
        }
    }
}