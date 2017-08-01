using System.Collections.Generic;
using System.Web.Http;
using Um416.BLL;

namespace Um416.API.Controllers
{
    [Authorize]
    public class UfsController : ApiController
    {
        UfLogic _logic;

        public UfsController()
        {
            _logic = new UfLogic();
        }

        public IList<Uf> Get()
        {
            return _logic.Listar();
        }
    }
}
