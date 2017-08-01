(function () {
    'use strict';

    angular
        .module('ngApp')
        .controller('usuariosController', usuariosController);

    function usuariosController($scope, $location, usuariosService, loginService, ufsService) {
        var vm = this;

        vm.sexos = [
            { Id: 0, Nome: 'Masculino' },
            { Id: 1, Nome: 'Feminino' }
        ];

        vm.find = function (id) {
            usuariosService
                .find(id)
                .then(function (usuario) {
                    vm.usuario = usuario;

                    vm.usuario._DataNascimento = vm.usuario.DataNascimento.toJsDate();
                    vm.usuario._DataCadastro = vm.usuario.DataCadastro.toJsDate();

                    $scope.usuarioForm.$setPristine();
                }, function (error) {
                    vm.error = error;
                });

            ufsService
                .list()
                .then(function (ufs) {
                    vm.ufs = ufs;
                }, function (error) {
                    vm.error = error;
                });
        };

        vm.saveProfile = function () {
            vm.areSubmitting = true;
            usuariosService
                .saveProfile(vm.usuario)
                .then(function () {
                    vm.areSubmitting = false;
                    var login = loginService.getLogin();
                    login.Nome = vm.usuario.Nome;
                    loginService.setLogin(login);
                    $location.url('/', true);
                }, function (error) {
                    vm.areSubmitting = false;
                    vm.error = error;
                });
        };

        vm.changePassword = function () {
            vm.areSubmitting = true;

            vm.profile.Id = vm.usuario.Id;

            usuariosService
                .changePassword(vm.profile)
                .then(function () {
                    vm.areSubmitting = false;
                    loginService.logout();
                }, function (error) {
                    vm.areSubmitting = false;
                    vm.error = error;
                });
        };
    }
})();