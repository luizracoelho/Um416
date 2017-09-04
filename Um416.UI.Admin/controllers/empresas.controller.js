(function () {
    'use strict';

    angular
        .module('ngApp')
        .controller('empresasController', empresasController);

    function empresasController($scope, empresasService, ufsService) {
        var vm = this;

        vm.areSubmitting = false;

        vm.showGrid = function () {
            return vm.empresas != null && vm.empresas.length != 0;
        }

        vm.tipoPessoa = {
            fisica: 0,
            juridica: 1
        };

        vm.list = function () {
            empresasService
                .list()
                .then(function (empresas) {
                    vm.empresas = empresas;
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

        vm.add = function () {
            vm.modalTitle = 'Adicionar';
            vm.errorDetail = null;
            vm.empresa = {
                id: 0,
                tipoPessoa: vm.tipoPessoa.fisica,
                nome: null,
                cpf: null,
                rg: null,
                dataCadastro: moment().format('DD/MM/YYYY'),
                _dataCadastro: moment().format('DD/MM/YYYY'),
                email: null,
                telFixo: null,
                telMovel: null,
                ativa: true,
                logradouro: null,
                numero: null,
                complemento: null,
                bairro: null,
                cidade: null,
                uf: null,
                cep: null,
                login: null,
                senha: null,
                confirmarSenha: null
            };
            $scope.empresaForm.$setPristine();
            $('#modalDetails').modal('show');
        }

        vm.find = function (id, isRemoving = false) {
            if (id != null)
                empresasService
                    .find(id)
                    .then(function (empresa) {
                        vm.empresa = empresa;

                        vm.empresa._dataCadastro = vm.empresa.dataCadastro.toJsDate();

                        $scope.empresaForm.$setPristine();

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

            empresasService
                .save(vm.empresa)
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

        vm.remove = function () {
            vm.areSubmitting = true;
            empresasService
                .remove(vm.empresa.id)
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