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
            var regra = identity.Claims.Where(x => x.Type == ClaimTypes.Role).Select(x => x.Value).FirstOrDefault();

            var bo = new UsuarioLogic();
            var usuario = bo.GetUser(identity.Name, regra);

            return new UserInfo
            {
                Id = usuario.Id,
                Nome = usuario.Nome,
                Login = usuario.Login,
            };
        }
    }
}
