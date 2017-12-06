using System.Web.Http;
using Um416.API.Controllers.Base;
using Um416.BLL;

namespace Um416.API.Controllers
{
    [Authorize]
    public class ConfiguracoesBoletosController : BaseController<ConfiguracaoBoleto, ConfiguracaoBoletoLogic>
    {
    }
}
