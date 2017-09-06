(function () {
    'use strict';

    angular
        .module('ngApp')
        .controller('mapaController', mapaController);

    function mapaController($state, $stateParams, loteamentosService) {
        var vm = this;

        vm.init = function () {
            loteamentosService
                .find($stateParams.id)
                .then(function (loteamento) {
                    if (loteamento == null)
                        $state.go('erro');

                    vm.loteamento = loteamento;
                }, function (error) {
                    $state.go('erro');
                });
        }
    }
})();