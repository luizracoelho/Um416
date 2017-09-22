(function () {
    'use strict';

    angular
        .module('ngApp')
        .controller('finalizarController', finalizarController);

    function finalizarController($scope, $state, $stateParams, lotesService, vendasService) {
        var vm = this;

        vm.areSubmitting = false;

        vm.init = function () {
            vm.clienteLogado = JSON.parse(sessionStorage.getItem('login'));
            vm.loteSelecionado = JSON.parse(sessionStorage.getItem('lote'));

            //Verificar se trocou de loteamento
            if (!vm.clienteLogado || !vm.loteSelecionado || $stateParams.id != vm.loteSelecionado.loteamentoId) {
                sessionStorage.removeItem('lote');
                sessionStorage.removeItem('login');

                $state.go('mapa', { id: $stateParams.id });
            }

            vm.limparCampos(vm.clienteLogado.id, vm.loteSelecionado.id)
        };

        vm.limparCampos = function (clienteId, loteId) {
            vm.errorDetail = null;

            lotesService
                .find(loteId)
                .then(function (lote) {
                    vm.lote = lote;

                    var valorParcela = vm.lote.valor / vm.lote.loteamento.quantParcelas;

                    vm.venda = {
                        id: 0,
                        numero: null,
                        valor: vm.lote.valor,
                        dataHora: moment().format('DD/MM/YYYY'),
                        _dataHora: moment().format('DD/MM/YYYY'),
                        quantParcelas: vm.lote.loteamento.quantParcelas,
                        diaVencimento: null,
                        clienteId: clienteId,
                        loteId: loteId,
                        valorParcela: valorParcela
                    };

                    $scope.vendaForm.$setPristine();
                }, function (error) {
                    vm.error = error;
                });
        }

        vm.save = function () {
            vm.areSubmitting = true;
            vendasService
                .save(vm.venda)
                .then(function () {
                    window.alert("Salvou!")
                    vm.areSubmitting = false;
                }, function (error) {
                    vm.areSubmitting = false;
                    vm.errorDetail = error;
                });
        };

        vm.calculaParcela = function () {
            vm.venda.valorParcela = vm.venda.valor / vm.venda.quantParcelas;
        }
    }
})();