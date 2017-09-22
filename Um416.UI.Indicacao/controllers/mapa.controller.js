(function () {
    'use strict';

    angular
        .module('ngApp')
        .controller('mapaController', mapaController);

    function mapaController($compile, $scope, $state, $stateParams, loteamentosService, lotesService) {
        var vm = this;

        vm.areSubmitting = false;

        vm.init = function () {
            vm.loteSelecionado = JSON.parse(sessionStorage.getItem('lote'));

            //Verificar se trocou de loteamento
            if (vm.loteSelecionado && $stateParams.id != vm.loteSelecionado.loteamentoId) {
                sessionStorage.removeItem('lote');
                sessionStorage.removeItem('login');

                vm.loteSelecionado = null;
            }

            loteamentosService
                .find($stateParams.id)
                .then(function (loteamento) {
                    if (loteamento == null)
                        $state.go('erro');

                    vm.loteamento = loteamento;

                    lotesService
                        .list($stateParams.id)
                        .then(function (lotes) {
                            vm.lotes = lotes;
                        }, function (error) {
                            $state.go('erro');
                        });
                }, function (error) {
                    $state.go('erro');
                });
        }

        vm.avancar = function () {
            $state.go('cadastro', { id: $stateParams.id });
        }

        vm.corIndisponivel = '#aaa';

        vm.carregarLotes = function (elements, lotes) {
            [].forEach.call(elements, function (element) {
                if (vm.lotes != null) {
                    var lote = vm.lotes.find(x => x.codigo == $(element).attr('id'));
                    if (lote != null) {
                        if (!lote.comprado) {
                            $(element).attr('ng-click', `vm.selecionarLote(${lote.id});`);
                            $compile($(element))($scope);

                            $(element).css('cursor', 'pointer');
                        }
                        else {
                            $(element).css({ fill: vm.corIndisponivel });
                            $(element).css('cursor', 'not-allowed');
                        }
                    }
                    else {
                        $(element).css({ fill: vm.corIndisponivel });
                        $(element).css('cursor', 'not-allowed');
                    }
                }
                else {
                    $(element).css({ fill: vm.corIndisponivel });
                    $(element).css('cursor', 'not-allowed');
                }

            });

            $('#mapa').css('display', 'block');
        }

        vm.selecionarLote = function (id) {
            vm.lote = vm.lotes.find(x => x.id == id);
            $('#modalDetails').modal('show');
        }

        vm.loteComprado = false;

        vm.save = function () {
            vm.loteComprado = true;
            vm.areSubmitting = true;
            sessionStorage.setItem('lote', JSON.stringify(vm.lote));
            $('#modalDetails').modal('hide');
            vm.areSubmitting = false;
        }

        $('#modalDetails').on('hidden.bs.modal', function () {
            if (vm.loteComprado)
                vm.avancar();
        })
    }
})();