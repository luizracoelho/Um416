using System.Collections.Generic;
using System.Web.Http;
using Um416.API.Controllers.Base;
using Um416.BLL;
using Um416.TransferModels;

namespace Um416.API.Controllers
{
    [Authorize(Roles = RegraAcesso.Empresa)]
    public class LoteamentosController : BaseController<Loteamento, LoteamentoLogic>
    {
        [Route("api/empresas/{empresaId}/loteamentos")]
        public IEnumerable<Loteamento> Get(long empresaId)
        {
            return _bo.List(empresaId);
        }
    }
}
