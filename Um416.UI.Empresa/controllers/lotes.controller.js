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

        //Definir os status de admissão
        vm.STATUSADICAO_ADICAO = 0;
        vm.STATUSADICAO_ALTERACAO = 1;
        vm.STATUSADICAO_REMOCAO = 2;

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
                .list(vm.loteamentoSelected.id)
                .then(function (lotes) {
                    vm.lotes = lotes;
                }, function (error) {
                    vm.error = error;
                });
        };

        vm.select = function () {
            vm.error = null;
            vm.loteamentoSelected = vm.loteamentos.filter(x => x.id == vm.loteamentoId)[0];

            if (vm.loteamentoSelected == null) {
                vm.loteamentoSelected = {
                    id: null,
                    nome: 'Lotes Avulsos'
                };
                vm.tipoLote = vm.TIPOLOTE_AVULSO;
            }
            else {
                loteamentosService
                    .find(vm.loteamentoSelected.id)
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
                id: 0,
                nome: null,
                codigo: null,
                area: null,
                valor: null,
                tipoLote: null,
                descricao: null,
                logradouro: null,
                numero: null,
                complemento: null,
                bairro: null,
                cidade: null,
                uf: null,
                cep: null,
                loteamentoId: null
            };
            $scope.loteForm.$setPristine();
        }

        vm.add = function () {
            vm.modalTitle = 'Adicionar';
            vm.limparCampos();
            $('#modalDetails').modal('show');
        }

        vm.import = function () {
            vm.areSubmitting = true;

            lotesService
                .listAdd(vm.loteamentoSelected.id)
                .then(function (lotes) {
                    vm.lotesDefault = [];
                    for (var lote in lotes) {
                        var existingLote = vm.lotesDefault.filter(x => x.cor == lotes[lote].cor)[0];
                        if (existingLote == null)
                            vm.lotesDefault.push({
                                cor: lotes[lote].cor,
                                valor: 0,
                                descricao: null
                            });
                    }

                    vm.clearDefault();

                    vm.areSubmitting = false;
                    vm.lotesAdd = lotes;

                    $('#valoresPadrao').collapse('hide');
                    $('#lotesGerados').collapse('show');

                    $('#modalImport').modal('show');
                }, function (error) {
                    vm.areSubmitting = false;
                    vm.error = error;
                });
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
            else {
                vm.lote = null;
                $('#modalRemove').modal('show');
            }
        };

        vm.save = function () {
            vm.areSubmitting = true;

            //Informar o tipo do lote
            vm.lote.tipoLote = vm.tipoLote;

            //Informar a qual loteamento o lote pertence
            if (vm.tipoLote == vm.TIPOLOTE_LOTEAMENTO)
                vm.lote.loteamentoId = vm.loteamentoSelected.id;

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

        vm.saveMultiple = function () {
            vm.areSubmitting = true;

            vm.lotesAdd.forEach(function (lote) {
                //preencher o id lotamento
                lote.loteamentoId = vm.loteamentoSelected.id;
            })

            //Salvar
            lotesService
                .saveMultiple(vm.lotesAdd)
                .then(function () {
                    vm.areSubmitting = false;
                    vm.list();
                    $('#modalImport').modal('hide');
                }, function (error) {
                    vm.areSubmitting = false;
                    vm.errorImport = error;
                    $('#modalImport').animate({ scrollTop: '0px' }, 300);
                });
        };

        vm.remove = function () {
            vm.areSubmitting = true;

            if (vm.lote != null)
                lotesService
                    .remove(vm.lote.id)
                    .then(function () {
                        vm.areSubmitting = false;
                        vm.list();
                        $('#modalRemove').modal('hide');
                    }, function (error) {
                        vm.areSubmitting = false;
                        vm.errorDetail = error;
                        $('#modalRemove').animate({ scrollTop: '0px' }, 300);
                    });
            else {
                var ids = [];
                vm.lotes.forEach(function (lote) {
                    if (lote.selected)
                        ids.push(lote.id);
                });

                lotesService
                    .removeMultiple(ids)
                    .then(function () {
                        vm.areSubmitting = false;
                        vm.list();
                        $('#modalRemove').modal('hide');
                    }, function (error) {
                        vm.areSubmitting = false;
                        vm.errorDetail = error;
                        $('#modalRemove').animate({ scrollTop: '0px' }, 300);
                    });
            }
        };

        vm.setDefault = function () {
            vm.areSubmitting = true;

            for (var lote in vm.lotesDefault) {
                var lotesAdd = vm.lotesAdd.filter(x => x.cor == vm.lotesDefault[lote].cor);

                for (var loteAdd in lotesAdd) {
                    lotesAdd[loteAdd].valor = vm.lotesDefault[lote].valor;
                    lotesAdd[loteAdd].descricao = vm.lotesDefault[lote].descricao;
                }
            }

            $('#valoresPadrao').collapse('hide');

            vm.areSubmitting = false;
        }

        vm.clearDefault = function () {
            vm.areSubmitting = true;

            for (var lote in vm.lotesDefault) {
                vm.lotesDefault[lote].valor = 0;
                vm.lotesDefault[lote].descricao = null;
            }

            for (var lote in vm.lotesAdd) {
                vm.lotesAdd[lote].valor = 0;
                vm.lotesAdd[lote].descricao = null;
            }

            $('#valoresPadrao').collapse('hide');

            vm.areSubmitting = false;
        }

        vm.selectAllToRemove = function () {
            vm.lotes.forEach(function (lote) {
                lote.selected = vm.todosSelected;
            });

            vm.lotesToRemove = vm.lotes;
        }

        vm.selectToRemove = function () {
            vm.lotesToRemove = vm.lotes.filter(x => x.selected);

            if (vm.lotesToRemove.length == 0)
                vm.todosSelected = false;
            else
                vm.todosSelected = true;
        }
    }
})();