using System.Collections.Generic;
using System.Web.Http;
using Um416.API.Controllers.Base;
using Um416.BLL;
using Um416.TransferModels;

namespace Um416.API.Controllers
{
    [Authorize]
    public class TitulosController : BaseController<Titulo, TituloLogic>
    {
        [HttpGet, Route("api/clientes/{clienteId}/venda/{vendaId}/titulos")]
        public IEnumerable<Titulo> List(long clienteId, long? vendaId)
        {
            return _bo.ListPorCliente(clienteId, vendaId);
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

        [HttpGet, Route("api/titulos/{tituloId}/cliente/{clienteId}/gerarboleto")]
        public string GerarBoleto(long tituloId, long clienteId)
        {
           return _bo.GerarBoleto(tituloId, clienteId);
        }

        [HttpGet, Route("api/titulos/{tituloId}/empresa/{empresaId}")]
        public Titulo Get(long tituloId, long empresaId)
        {
            return _bo.Get(tituloId, empresaId);
        }

        [HttpPost, Route("api/titulos/filtrar")]
        public IEnumerable<Titulo> Filtrar(TituloFiltroDTO filtro)
        {
            return _bo.Filtrar(filtro);
        }
    }
}