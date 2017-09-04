﻿using System.Web.Http;
using Um416.API.Controllers.Base;
using Um416.BLL;
using Um416.TransferModels;

namespace Um416.API.Controllers
{
    [Authorize]
    public class UsuariosEmpresasController : BaseController<UsuarioEmpresa, UsuarioEmpresaLogic>
    {
        [HttpPost, Route("api/usuariosEmpresas/password"), Authorize]
        public void ChangePassword(PasswordProfile profile)
        {
            _bo.ChangePassword(profile);
        }
    }
}
