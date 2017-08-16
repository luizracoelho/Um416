(function () {
    'use strict';

    angular
        .module('ngApp')
        .controller('usuariosController', usuariosController);

    function usuariosController($scope, $location, usuariosService, loginService, ufsService) {
        var vm = this;

        vm.sexos = [
            { id: 0, nome: 'Masculino' },
            { id: 1, nome: 'Feminino' }
        ];

        vm.find = function (id) {
            usuariosService
                .find(id)
                .then(function (usuario) {
                    vm.usuario = usuario;

                    vm.usuario._dataNascimento = vm.usuario.dataNascimento.toJsDate();
                    vm.usuario._dataCadastro = vm.usuario.dataCadastro.toJsDate();

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
                    login.nome = vm.usuario.nome;
                    loginService.setLogin(login);
                    $location.url('/', true);
                }, function (error) {
                    vm.areSubmitting = false;
                    vm.error = error;
                });
        };

        vm.changePassword = function () {
            vm.areSubmitting = true;

            vm.profile.id = vm.usuario.id;

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