using Um416.BLL.Interfaces;
using Um416.BLL.Tools;
using Um416.TransferModels;

namespace Um416.BLL
{
    public class UsuarioLogic
    {
                Crypt _crypt;

        public UsuarioLogic()
        {
            _crypt = new Crypt();
        }

        public Usuario GetUser(string login, string regraAcesso)
        {
            if (login == null)
                return null;

            IUsuarioLogic logic;

            if (regraAcesso == RegraAcesso.Admin)
                logic = new UsuarioAdminLogic();
            else if (regraAcesso == RegraAcesso.Cliente)
                logic = new UsuarioClienteLogic();
            else if (regraAcesso == RegraAcesso.Vendedor)
                logic = new UsuarioVendedorLogic();
            else
                return null;

            return logic.Get(login);
        }

        public Usuario GetAuthenticatedUser(string login, string senha, string regraAcesso)
        {
            if (login == null || senha == null)
                return null;

            var usuario = GetUser(login, regraAcesso);

            if (usuario == null)
                return null;

            var senhaCrypt = _crypt.Encrypt(senha);

            if (usuario.Senha != senhaCrypt)
                return null;

            return usuario;
        }
    }
}
