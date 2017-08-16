(function () {
    'use strict';

    angular
        .module('ngApp')
        .controller('clientesController', clientesController);

    function clientesController($scope, clientesService, ufsService) {
        var vm = this;

        vm.areSubmitting = false;

        vm.showGrid = function () {
            return vm.clientes != null && vm.clientes.length != 0;
        }

        vm.sexos = [
            { id: 0, nome: 'Masculino' },
            { id: 1, nome: 'Feminino' }
        ];

        vm.list = function () {
            clientesService
                .list()
                .then(function (clientes) {
                    vm.clientes = clientes;
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
            vm.cliente = {
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
                confirmarSenha: null
            };
            $scope.clienteForm.$setPristine();
            $('#modalDetails').modal('show');
        }

        vm.find = function (id, isRemoving = false) {
            if (id != null)
                clientesService
                    .find(id)
                    .then(function (cliente) {
                        vm.cliente = cliente;

                        vm.cliente._dataNascimento = vm.cliente.dataNascimento.toJsDate();
                        vm.cliente._dataCadastro = vm.cliente.dataCadastro.toJsDate();

                        $scope.clienteForm.$setPristine();

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

            vm.cliente.dataNascimento = vm.cliente._dataNascimento.toCsDate();

            clientesService
                .save(vm.cliente)
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
            clientesService
                .remove(vm.cliente.id)
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