using System.Collections.Generic;
using System.Web.Http;
using Um416.API.Controllers.Base;
using Um416.BLL;

namespace Um416.API.Controllers
{
    [Authorize]
    public class NotificacoesController : BaseController<Notificacao, NotificacaoLogic>
    {
        [HttpGet, Route("api/notificacoes/{id}/clientes")]
        public IEnumerable<NotificacaoLeitura> List(long id)
        {
            return _bo.List(id);
        }

        [HttpGet, Route("api/notificacoes/{id}/clientes/{clienteId}")]
        public void Read(long id, long clienteId)
        {
            _bo.Read(id, clienteId);
        }
    }
}
