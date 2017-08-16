(function () {
    'use strict';

    angular
        .module('ngApp')
        .controller('usuariosController', usuariosController);

    function usuariosController($scope, $location, usuariosService, loginService) {
        var vm = this;

        vm.areSubmitting = false;

        vm.showGrid = function () {
            return vm.usuarios != null && vm.usuarios.length != 0;
        }

        vm.list = function () {
            usuariosService
                .list()
                .then(function (usuarios) {
                    vm.usuarios = usuarios;
                }, function (error) {
                    vm.error = error;
                });
        };

        vm.add = function () {
            vm.modalTitle = 'Adicionar';
            vm.errorDetail = null;
            vm.usuario = {
                id: 0,
                nome: null,
                cpf: null,
                email: null,
                telFixo: null,
                telMovel: null,
                login: null,
                senha: null,
                confirmarSenha: null
            };
            $scope.usuarioForm.$setPristine();
            $('#modalDetails').modal('show');
        }

        vm.find = function (id, isRemoving = false) {
            usuariosService
                .find(id)
                .then(function (usuario) {

                    usuario.senha = null;
                    vm.usuario = usuario;

                    $scope.usuarioForm.$setPristine();

                    if (!isRemoving) {
                        vm.modalTitle = 'Atualizar';
                        vm.errorDetail = null;

                        $('#modalDetails').modal('show');
                    }
                    else
                        $('#modalRemove').modal('show');
                }, function (error) {
                    vm.error = error;
                });
        };

        vm.save = function () {
            vm.areSubmitting = true;
            usuariosService
                .save(vm.usuario)
                .then(function () {
                    vm.areSubmitting = false;
                    vm.list();
                    $('#modalDetails').modal('hide');
                }, function (error) {
                    vm.areSubmitting = false;
                    vm.errorDetail = error;
                    $('#modalDetails').animate({ scrollTop: '0px' }, 300);
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

        vm.remove = function () {
            vm.areSubmitting = true;
            usuariosService
                .remove(vm.usuario.id)
                .then(function () {
                    vm.areSubmitting = false;
                    vm.list();
                    $('#modalRemove').modal('hide');
                }, function (error) {
                    vm.areSubmitting = false;
                    vm.errorDetail = error;
                    $('#modalRemove').animate({ scrollTop: '0px' }, 300);
                });
        };
    }
})();