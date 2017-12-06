(function () {
    'use strict';

    angular
        .module('ngApp')
        .controller('configuracoesBoletosController', configuracoesBoletosController);

    function configuracoesBoletosController($scope, $state, configuracoesBoletosService, ufsService) {
        var vm = this;

        vm.areSubmitting = false;

        vm.find = function () {
                configuracoesBoletosService
                    .find()
                    .then(function (configuracaoBoleto) {
                        vm.configuracaoBoleto = configuracaoBoleto;
                    }, function (error) {
                        vm.error = error;
                    });
        };

        vm.save = function () {
            vm.areSubmitting = true;

            configuracoesBoletosService
                .save(vm.configuracaoBoleto)
                .then(function () {
                    vm.areSubmitting = false;
                    $state.go('painel.home');
                }, function (error) {
                    vm.areSubmitting = false;
                    vm.error = error;
                });
        };
    }
})();