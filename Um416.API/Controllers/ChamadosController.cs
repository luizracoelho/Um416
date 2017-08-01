using System.Collections.Generic;
using System.Web.Http;
using Um416.API.Controllers.Base;
using Um416.BLL;
using Um416.TransferModels;

namespace Um416.API.Controllers
{
    [Authorize]
    public class ChamadosController : BaseController<Chamado, ChamadoLogic>
    {
        [HttpGet, Route("api/chamados/{id}/list")]
        public IEnumerable<Chamado> List(long id)
        {
            return _bo.List(id);
        }

        [Route("api/chamados/clientes")]
        public void PostComIteracao(ChamadoIteracao entity)
        {
            _bo.Save(entity.Chamado, entity.Iteracao);
        }
    }
}
