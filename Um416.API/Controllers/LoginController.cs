using System.Linq;
using System.Security.Claims;
using System.Web.Http;
using Um416.BLL;
using Um416.TransferModels;

namespace Um416.API.Controllers
{
    [Authorize]
    public class LoginController : ApiController
    {
        public UserInfo Get()
        {
            var identity = (ClaimsIdentity)User.Identity;
            var role = identity.Claims.Where(x => x.Type == ClaimTypes.Role).Select(x => x.Value).FirstOrDefault();

            Usuario usuario;

            if (role == "admin")
            {
                var bo = new UsuarioAdminLogic();
                usuario = bo.Get(x => x.Login == identity.Name);
            }
            else
            {
                var bo = new UsuarioClienteLogic();
                usuario = bo.Get(x => x.Login == identity.Name);
            }

            return new UserInfo
            {
                Id = usuario.Id,
                Nome = usuario.Nome,
                Login = usuario.Login,
            };
        }
    }
}
