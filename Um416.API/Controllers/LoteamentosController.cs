using System.Web.Http;
using Um416.API.Controllers.Base;
using Um416.API.Interfaces;
using Um416.BLL;

namespace Um416.API.Controllers
{
    [Authorize(Roles = "admin")]
    public class LoteamentosController : BaseController<Loteamento, LoteamentoLogic>
    {
    }
}
