using System.Collections.Generic;
using System.Web.Http;
using Um416.API.Controllers.Base;
using Um416.BLL;
using Um416.TransferModels;

namespace Um416.API.Controllers
{
    [Authorize]
    public class LotesController : BaseController<Lote, LoteLogic>
    {
        [HttpGet, AllowAnonymous, Route("api/loteamentos/{id}/lotes")]
        public IEnumerable<Lote> GetByLoteamento(long? id)
        {
            return _bo.List(id);
        }

        [AllowAnonymous]
        public override Lote Get(long id)
        {
            return base.Get(id);
        }

        [HttpGet, Authorize(Roles = Autorizacao.Empresa), Route("api/loteamentos/{id}/generate")]
        public IEnumerable<Lote> GenerateLotes(long? id)
        {
            if (id == null)
                return null;

            return _bo.Generate((long)id);
        }

        [HttpPost, Authorize(Roles = Autorizacao.Empresa), Route("api/lotes/multiple")]
        public void PostMultiple(IList<Lote> lotes)
        {
            _bo.Save(lotes);
        }

        [HttpDelete, Authorize(Roles = Autorizacao.Empresa), Route("api/lotes/multiple")]
        public void DeleteMultiple(long[] ids)
        {
            _bo.Delete(ids);
        }
    }
}