﻿using System;
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
    public class UsuarioVendedorLogic : BaseLogic<UsuarioVendedor, UsuarioVendedorRepository>, IUsuarioLogic
    {
        Crypt _crypt;

        public UsuarioVendedorLogic()
        {
            _crypt = new Crypt();
        }

        public override IEnumerable<UsuarioVendedor> List()
        {
            return base.List().OrderBy(x => x.Nome);
        }

        public override IEnumerable<UsuarioVendedor> List(Expression<Func<UsuarioVendedor, bool>> predicate)
        {
            return base.List(predicate).OrderBy(x => x.Nome);
        }

        protected override void Insert(UsuarioVendedor entity)
        {
            entity.DataCadastro = DateTime.Today;
            entity.Senha = _crypt.Encrypt(entity.Senha);

            base.Insert(entity);
        }

        protected override void Update(UsuarioVendedor entity)
        {
            var usuario = Get(entity.Id);

            entity.DataCadastro = usuario.DataCadastro;
            entity.Senha = usuario.Senha;

            base.Update(entity);
        }

        public override void Save(UsuarioVendedor entity)
        {
            var usuario = Get(x => x.Login == entity.Login);

            if (usuario != null && usuario.Id != entity.Id)
                throw new Exception("Já existe um usuário cadastrado com este login.");

            base.Save(entity);
        }

        public UsuarioVendedor GetAuthenticatedUser(string login, string senha)
        {
            if (login == null || senha == null)
                return null;

            var usuario = Get(x => x.Login == login);

            if (usuario == null)
                return null;

            var senhaCrypt = _crypt.Encrypt(senha);

            if (usuario.Senha != senhaCrypt)
                return null;

            return usuario;
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

        public Usuario Get(string login)
        {
            return Get(x => x.Login.Equals(login));
        }
    }
}
