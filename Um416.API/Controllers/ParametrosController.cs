using System.Web.Http;
using Um416.API.Controllers.Base;
using Um416.BLL;
using Um416.TransferModels;

namespace Um416.API.Controllers
{
    [Authorize(Roles = Autorizacao.AdminEmpresa)]
    public class ParametrosController : BaseController<Parametro, ParametroLogic>
    {
        [AllowAnonymous]
        public override Parametro Get(long id)
        {
            return base.Get(id);
        }
    }
}