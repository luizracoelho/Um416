using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Net.Http.Formatting;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Um416.API
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            // Habilitar o CORS
            var cors = new EnableCorsAttribute("*", "*", "*");
            config.EnableCors(cors);

            // Remover json xml
            config.Formatters.Remove(config.Formatters.XmlFormatter);
            config.Formatters.Remove(config.Formatters.JsonFormatter);

            // Configurar json como cammel case
            var serializer = new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() };
            var formatter = new JsonMediaTypeFormatter { SerializerSettings = serializer };
            config.Formatters.Add(formatter);

            // Web API routes
            config.MapHttpAttributeRoutes();

            // Allow show inner exception errors
            config.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Always;

            // Default Route
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { controller = "Home", id = RouteParameter.Optional }
            );
        }
    }
}
