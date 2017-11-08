using System.Web.Http;
using Um416.BLL;
using Um416.TransferModels;

namespace Um416.API.Controllers
{
    [Authorize(Roles = Autorizacao.Cliente)]
    public class DashboardClienteController : ApiController
    {
        [HttpGet, Route("api/clientes/{clienteId}/dashboard")]
        public DashboardClienteDTO GetDashboard(long clienteId)
        {
            var dashboardBo = new DashboardClienteLogic();

            return dashboardBo.GetDashboard(clienteId);
        }
    }
}
