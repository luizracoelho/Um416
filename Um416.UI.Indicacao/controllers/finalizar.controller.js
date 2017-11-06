(function () {
    'use strict';

    angular
        .module('ngApp')
        .controller('finalizarController', finalizarController);

    function finalizarController($scope, $state, $stateParams, lotesService, vendasService) {
        var vm = this;

        vm.areSubmitting = false;

        vm.init = function () {
            vm.indicador = $stateParams.indicador;

            vm.clienteLogado = JSON.parse(sessionStorage.getItem('login'));
            vm.loteSelecionado = JSON.parse(sessionStorage.getItem('lote'));

            //Verificar se trocou de loteamento
            if (!vm.clienteLogado || !vm.loteSelecionado || $stateParams.id != vm.loteSelecionado.loteamentoId) {
                sessionStorage.removeItem('lote');
                sessionStorage.removeItem('login');

                if (vm.indicador == null)
                    $state.go('mapa.default', { id: $stateParams.id });
                else
                    $state.go('mapa.indicador', { id: $stateParams.id, login: vm.indicador });
            }

            if (vm.indicador != null)
                vendasService
                    .find(vm.indicador)
                    .then(function (venda) {
                        vm.indicadorSelecionado = venda.cliente;
                    }, function (error) {
                        vm.error = error;
                    });

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
                        valorParcela: valorParcela,
                        indicadorId: null
                    };

                    $scope.vendaForm.$setPristine();
                }, function (error) {
                    vm.error = error;
                });
        }

        vm.save = function () {
            vm.areSubmitting = true;

            if (vm.indicador != null)
                vendasService
                    .find(vm.indicador)
                    .then(function (venda) {
                        vm.venda.indicadorId = venda.id;
                        vm.salvarVenda(vm.venda);
                    }, function (error) {
                        vm.errorDetail = error;
                    });
            else
                vm.salvarVenda(vm.venda);
        };

        vm.calculaParcela = function () {
            vm.venda.valorParcela = vm.venda.valor / vm.venda.quantParcelas;
        }

        vm.salvarVenda = function (venda) {
            vendasService
                .save(venda)
                .then(function () {
                    $state.go("concluido");
                    vm.areSubmitting = false;
                }, function (error) {
                    vm.areSubmitting = false;
                    vm.errorDetail = error;
                });
        }
    }
})();