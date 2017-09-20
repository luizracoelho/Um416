using System.Collections.Generic;
using System.Web.Http;
using Um416.API.Controllers.Base;
using Um416.BLL;

namespace Um416.API.Controllers
{
    public class TitulosController : BaseController<Titulo, TituloLogic>
    {
        [HttpGet, Route("api/clientes/venda/{vendaId}/titulos")]
        public IEnumerable<Titulo> List(long vendaId)
        {
            return _bo.List(vendaId);
        }

        [HttpGet, Route("api/titulos/baixar/{tituloId}")]
        public void BaixarTitulo(int tituloId)
        {
            _bo.BaixarTitulo(tituloId);
        }

        [HttpGet, Route("api/titulos/estornar/{tituloId}")]
        public void EstornarTitulo(int tituloId)
        {
            _bo.EstornarTitulo(tituloId);
        }
    }
}