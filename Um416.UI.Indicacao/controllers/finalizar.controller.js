(function () {
    'use strict';

    angular
        .module('ngApp')
        .controller('finalizarController', finalizarController);

    function finalizarController($scope, $state, $stateParams) {
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
        };
    }
})();