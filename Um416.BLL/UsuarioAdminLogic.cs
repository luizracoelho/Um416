using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Um416.BLL.Base;
using Um416.BLL.Interfaces;
using Um416.BLL.Tools;
using Um416.DAL;
using Um416.TransferModels;

namespace Um416.BLL
{
    public class UsuarioAdminLogic : BaseLogic<UsuarioAdmin, UsuarioAdminRepository>, IUsuarioLogic
    {
        Crypt _crypt;

        public UsuarioAdminLogic()
        {
            _crypt = new Crypt();
        }
        public override IEnumerable<UsuarioAdmin> List()
        {
            return base.List().OrderBy(x => x.Nome);
        }

        public override IEnumerable<UsuarioAdmin> List(Expression<Func<UsuarioAdmin, bool>> predicate)
        {
            return base.List(predicate).OrderBy(x=>x.Nome);
        }

        protected override void Insert(UsuarioAdmin entity)
        {
            entity.Senha = _crypt.Encrypt(entity.Senha);

            base.Insert(entity);
        }

        protected override void Update(UsuarioAdmin entity)
        {
            entity.Senha = Get(entity.Id).Senha;

            base.Update(entity);
        }

        public override void Save(UsuarioAdmin entity)
        {
            var usuario = Get(x => x.Login == entity.Login);

            if (usuario != null && usuario.Id != entity.Id)
                throw new Exception("Já existe um usuário cadastrado com este login.");

            base.Save(entity);
        }

        public void ChangePassword(PasswordProfile profile)
        {
            var usuarioDB = Get(profile.Id);

            var senhaAtual = _crypt.Encrypt(profile.SenhaAtual);

            if (senhaAtual != usuarioDB.Senha)
                throw new InvalidOperationException("A senha atual está incorreta.");

            usuarioDB.Senha = _crypt.Encrypt(profile.SenhaNova);

            _dao.Update(usuarioDB);
        }

        public Pessoa Get(string login)
        {
            return Get(x => x.Login ==login);
        }
    }
}
