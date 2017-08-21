(function () {
    'use strict';

    angular
        .module('ngApp')
        .controller('vendedoresController', vendedoresController);

    function vendedoresController($scope, vendedoresService, ufsService) {
        var vm = this;

        vm.areSubmitting = false;

        vm.showGrid = function () {
            return vm.vendedores != null && vm.vendedores.length != 0;
        }

        vm.sexos = [
            { id: 0, nome: 'Masculino' },
            { id: 1, nome: 'Feminino' }
        ];

        vm.list = function () {
            vendedoresService
                .list()
                .then(function (vendedores) {
                    vm.vendedores = vendedores;
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
            vm.vendedor = {
                id: 0,
                nome: null,
                cpf: null,
                rg: null,
                sexo: null,
                dataNascimento: null,
                _dataNascimento: null,
                dataCadastro: moment().format('DD/MM/YYYY'),
                _dataCadastro: moment().format('DD/MM/YYYY'),
                email: null,
                telFixo: null,
                telMovel: null,
                logradouro: null,
                numero: null,
                complemento: null,
                bairro: null,
                cidade: null,
                uf: null,
                cep: null,
                login: null,
                senha: null,
                confirmarSenha: null,
                percentualComissao: 0
            };
            $scope.vendedorForm.$setPristine();
            $('#modalDetails').modal('show');
        }

        vm.find = function (id, isRemoving = false) {
            if (id != null)
                vendedoresService
                    .find(id)
                    .then(function (vendedor) {
                        vm.vendedor = vendedor;

                        vm.vendedor._dataNascimento = vm.vendedor.dataNascimento.toJsDate();
                        vm.vendedor._dataCadastro = vm.vendedor.dataCadastro.toJsDate();

                        $scope.vendedorForm.$setPristine();

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

            vm.vendedor.dataNascimento = vm.vendedor._dataNascimento.toCsDate();

            vendedoresService
                .save(vm.vendedor)
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
            vendedoresService
                .remove(vm.vendedor.id)
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