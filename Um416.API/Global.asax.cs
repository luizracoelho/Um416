using System.Web.Http;
using Um416.DAL.Mappings.Register;

namespace Um416.API
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RegisterMappings.Register();
        }
    }
}
