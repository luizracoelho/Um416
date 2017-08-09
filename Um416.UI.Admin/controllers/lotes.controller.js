(function () {
    'use strict';

    angular
        .module('ngApp')
        .controller('lotesController', lotesController)

    function lotesController($scope, lotesService, loteamentosService, ufsService) {
        var vm = this;

        //Definir tipos de loteamento
        vm.TIPOLOTE_LOTEAMENTO = 0;
        vm.TIPOLOTE_AVULSO = 1;

        vm.areSubmitting = false;

        vm.showGrid = function () {
            return vm.lotes != null && vm.lotes.length != 0;
        }

        vm.init = function () {
            vm.loteamentoId = null;

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
        }

        vm.list = function () {
            lotesService
                .list(vm.loteamentoSelected.Id)
                .then(function (lotes) {
                    vm.lotes = lotes;
                }, function (error) {
                    vm.error = error;
                });
        };

        vm.select = function () {
            vm.error = null;
            vm.loteamentoSelected = vm.loteamentos.filter(x => x.Id == vm.loteamentoId)[0];

            if (vm.loteamentoSelected == null) {
                vm.loteamentoSelected = {
                    Id: null,
                    Nome: 'Lotes Avulsos'
                };
                vm.tipoLote = vm.TIPOLOTE_AVULSO;
            }
            else {
                loteamentosService
                    .find(vm.loteamentoSelected.Id)
                    .then(function (loteamento) {
                        vm.loteamentoSelected = loteamento;
                    }, function (error) {
                        vm.error = error;
                    });
                vm.tipoLote = vm.TIPOLOTE_LOTEAMENTO;
            }

            vm.list();
        };

        vm.deselect = function () {
            vm.error = null;
            vm.loteamentoSelected = null;
        };

        vm.limparCampos = function () {
            vm.errorDetail = null;
            vm.lote = {
                Id: 0,
                Nome: null,
                Codigo: null,
                Area: null,
                Valor: null,
                TipoLote: null,
                Descricao: null,
                Logradouro: null,
                Numero: null,
                Complemento: null,
                Bairro: null,
                Cidade: null,
                Uf: null,
                Cep: null,
                LoteamentoId: null
            };
            $scope.loteForm.$setPristine();
        }

        vm.add = function () {
            vm.modalTitle = 'Adicionar';
            vm.limparCampos();
            $('#modalDetails').modal('show');
        }

        vm.find = function (id, isRemoving = false) {
            if (id != null)
                lotesService
                    .find(id)
                    .then(function (lote) {
                        vm.limparCampos();

                        vm.lote = lote;

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

            //Informar o tipo do lote
            vm.lote.TipoLote = vm.tipoLote;

            //Informar a qual loteamento o lote pertence
            if (vm.tipoLote == vm.TIPOLOTE_LOTEAMENTO)
                vm.lote.LoteamentoId = vm.loteamentoSelected.Id;

            lotesService
                .save(vm.lote)
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
            lotesService
                .remove(vm.lote.Id)
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