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

        [HttpPost, Route("api/titulos/baixar/{tituloId}/empresa/{empresaId}")]
        public void BaixarTitulo(long tituloId, long empresaId)
        {
            _bo.BaixarTitulo(tituloId, empresaId);
        }

        [HttpPost, Route("api/titulos/estornar/{tituloId}/empresa/{empresaId}")]
        public void EstornarTitulo(long tituloId, long empresaId)
        {
            _bo.EstornarTitulo(tituloId, empresaId);
        }

        [HttpGet, Route("api/titulos/{tituloId}/empresa/{empresaId}")]
        public Titulo Get(long tituloId, long empresaId)
        {
            return _bo.Get(tituloId, empresaId);
        }
    }
}