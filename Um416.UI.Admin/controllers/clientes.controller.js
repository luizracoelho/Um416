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
            { Id: 0, Nome: 'Masculino' },
            { Id: 1, Nome: 'Feminino' }
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
                Id: 0,
                Nome: null,
                Cpf: null,
                Rg: null,
                Sexo: null,
                DataNascimento: null,
                _DataNascimento: null,
                DataCadastro: moment().format('DD/MM/YYYY'),
                _DataCadastro: moment().format('DD/MM/YYYY'),
                Email: null,
                TelFixo: null,
                TelMovel: null,
                Logradouro: null,
                Numero: null,
                Complemento: null,
                Bairro: null,
                Cidade: null,
                Uf: null,
                Cep: null,
                Login: null,
                Senha: null,
                ConfirmarSenha: null
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

                        vm.cliente._DataNascimento = vm.cliente.DataNascimento.toJsDate();
                        vm.cliente._DataCadastro = vm.cliente.DataCadastro.toJsDate();

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

            vm.cliente.DataNascimento = vm.cliente._DataNascimento.toCsDate();

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
                .remove(vm.cliente.Id)
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