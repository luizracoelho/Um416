using System.Web.Http;
using Um416.API.Controllers.Base;
using Um416.BLL;
using Um416.TransferModels;

namespace Um416.API.Controllers
{
    [Authorize(Roles = RegraAcesso.Admin)]
    public class LoteamentosController : BaseController<Loteamento, LoteamentoLogic>
    {
    }
}
