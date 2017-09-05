(function () {
    'use strict';

    angular
        .module('ngApp')
        .controller('parametrosController', parametrosController);

    function parametrosController($scope, $state, parametrosService, ufsService) {
        var vm = this;

        vm.areSubmitting = false;

        vm.find = function () {
                parametrosService
                    .find()
                    .then(function (parametro) {
                        vm.parametro = parametro;
                    }, function (error) {
                        vm.error = error;
                    });
        };

        vm.save = function () {
            vm.areSubmitting = true;

            parametrosService
                .save(vm.parametro)
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