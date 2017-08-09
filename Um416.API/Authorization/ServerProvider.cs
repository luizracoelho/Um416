using Microsoft.Owin.Security.OAuth;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Um416.BLL;

namespace Um416.API.Authorization
{
    public class ServerProvider : OAuthAuthorizationServerProvider
    {
        string tipo;

        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            await Task.Run(() =>
            {
                context.Validated();

                tipo = context.Parameters.FirstOrDefault(x => x.Key == "tipo").Value.FirstOrDefault();
            });
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            await Task.Run(() =>
            {
                context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });

                Usuario usuario;

                if (tipo == "admin")
                {
                    var bo = new UsuarioAdminLogic();
                    usuario = bo.GetAuthenticatedUser(context.UserName, context.Password);
                }
                else if (tipo == "cliente")
                {
                    var bo = new UsuarioClienteLogic();
                    usuario = bo.GetAuthenticatedUser(context.UserName, context.Password);
                }
                else
                {
                    usuario = null;
                }

                if (usuario == null)
                    context.Rejected();
                else
                {
                    var identity = new ClaimsIdentity(context.Options.AuthenticationType);
                    identity.AddClaim(new Claim(ClaimTypes.Name, usuario.Login));
                    identity.AddClaim(new Claim(ClaimTypes.Role, tipo));
                    context.Validated(identity);
                }
            });
        }
    }
}