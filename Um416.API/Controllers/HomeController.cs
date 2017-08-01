using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Web.Http;

namespace Um416.API.Controllers
{
    public class HomeController : ApiController
    {
        public string Get()
        {
            return $"Api works on ip {GetIPAddress()}!";
        }

        protected string GetIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            return host.AddressList.FirstOrDefault(ip => ip.AddressFamily == AddressFamily.InterNetwork).ToString();
        }
    }
}
