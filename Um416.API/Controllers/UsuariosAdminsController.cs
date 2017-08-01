using System.Web.Http;
using Um416.API.Controllers.Base;
using Um416.BLL;
using Um416.TransferModels;

namespace Um416.API.Controllers
{
    [Authorize(Roles = "admin")]
    public class UsuariosAdminsController : BaseController<UsuarioAdmin, UsuarioAdminLogic>
    {
        [HttpPost, Route("api/usuariosAdmins/password"), Authorize]
        public void ChangePassword(PasswordProfile profile)
        {
            _bo.ChangePassword(profile);
        }
    }
}
