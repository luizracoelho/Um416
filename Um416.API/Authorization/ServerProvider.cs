using Microsoft.Owin.Security.OAuth;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Um416.BLL;

namespace Um416.API.Authorization
{
    public class ServerProvider : OAuthAuthorizationServerProvider
    {
        string regra;

        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            await Task.Run(() =>
            {
                context.Validated();

                regra = context.Parameters.FirstOrDefault(x => x.Key == "regra").Value.FirstOrDefault();
            });
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            await Task.Run(() =>
            {
                context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });

                var bo = new UsuarioLogic();
                var usuario = bo.GetAuthenticatedUser(context.UserName, context.Password, regra);

                if (usuario == null)
                    context.Rejected();
                else
                {
                    var identity = new ClaimsIdentity(context.Options.AuthenticationType);
                    identity.AddClaim(new Claim(ClaimTypes.Name, usuario.Login));
                    identity.AddClaim(new Claim(ClaimTypes.Role, regra));
                    context.Validated(identity);
                }
            });
        }
    }
}