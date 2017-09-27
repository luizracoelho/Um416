(function () {
    'use strict';

    angular
        .module('ngApp')
        .controller('concluidoController', concluidoController);

    function concluidoController($scope, $state, $stateParams, parametrosService) {
        var vm = this;

        vm.areSubmitting = false;

        vm.init = function () {
            //Limpar sessionStorage
            sessionStorage.removeItem('lote');
            sessionStorage.removeItem('login');

            parametrosService
                .find(1)
                .then(function (parametro) {
                    vm.parametro = parametro;
                }, function (error) {
                    vm.error = error;
                });
        };
    }
})();