using System;
using System.Web.Http;
using Um416.API.Controllers.Base;
using Um416.BLL;
using Um416.TransferModels;

namespace Um416.API.Controllers
{
    [Authorize]
    public class UsuariosClientesController : BaseController<UsuarioCliente, UsuarioClienteLogic>
    {
        [HttpPost, Route("api/usuariosClientes/password")]
        public void ChangePassword(PasswordProfile profile)
        {
            _bo.ChangePassword(profile);
        }

        [AllowAnonymous]
        public override UsuarioCliente Get(long id)
        {
            return base.Get(id);
        }

        [AllowAnonymous, HttpPost, Route("api/usuariosClientes/id")]
        public long PostId(UsuarioCliente entity)
        {
            base.Post(entity);
            return entity.Id;
        }

        [AllowAnonymous, HttpPost, Route("api/usuariosClientes/login")]
        public UsuarioCliente PostLogin(LoginInfo entity)
        {
            var cliente = _bo.GetAuthenticatedUser(entity.Login, entity.Senha);

            if (cliente == null)
                throw new Exception("Usuário ou senha inválidos.");

            return cliente;
        }

        [AllowAnonymous, Route("api/usuariosClientes/login/{login}")]
        public UsuarioCliente GetByLogin(string login)
        {
            return _bo.GetByLogin(login);
        }
    }
}
