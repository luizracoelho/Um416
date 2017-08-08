(function () {
    'use strict';

    angular
        .module('ngApp')
        .controller('loteamentosController', loteamentosController);

    function loteamentosController($scope   , loteamentosService, ufsService) {
        var vm = this;

        vm.areSubmitting = false;

        vm.showGrid = function () {
            return vm.loteamentos != null && vm.loteamentos.length != 0;
        }

        vm.list = function () {
            loteamentosService
                .list()
                .then(function (loteamentos) {
                    vm.loteamentos = loteamentos;
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

        vm.limparCampos = function () {
            vm.errorDetail = null;
            vm.loteamento = {
                Id: 0,
                Nome: null,
                Descricao: null,
                Mapa: null,
                DataCadastro: moment().format('DD/MM/YYYY'),
                _DataCadastro: moment().format('DD/MM/YYYY'),
                Logradouro: null,
                Numero: null,
                Bairro: null,
                Cidade: null,
                Uf: null,
                Cep: null
            };
            $scope.loteamentoForm.$setPristine();
            $('input[type=file]').val(null);
            vm.mapa = null;
        }

        vm.add = function () {
            vm.modalTitle = 'Adicionar';

            vm.limparCampos();

            $('#modalDetails').modal('show');
        }

        vm.find = function (id, isRemoving = false) {
            if (id != null)
                loteamentosService
                    .find(id)
                    .then(function (loteamento) {
                        vm.limparCampos();

                        vm.loteamento = loteamento;

                        vm.loteamento._DataCadastro = vm.loteamento.DataCadastro.toJsDate();
                        vm.mapa = vm.loteamento.Mapa != null ? vm.loteamento.Mapa.Source : null;

                        $scope.loteamentoForm.$setPristine();

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

            vm.loteamento.Mapa = {
                Source : vm.mapa
            };

            loteamentosService
                .save(vm.loteamento)
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
            loteamentosService
                .remove(vm.loteamento.Id)
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