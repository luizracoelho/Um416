(function () {
    'use strict';

    angular
        .module('ngApp')
        .controller('notificacoesController', notificacoesController);

    function notificacoesController($rootScope, $scope, notificacoesService, ufsService) {
        var vm = this;

        vm.areSubmitting = false;

        vm.showGrid = function () {
            return vm.notificacoes != null && vm.notificacoes.length != 0;
        }

        vm.list = function () {
            notificacoesService
                .list()
                .then(function (notificacoes) {
                    vm.notificacoes = notificacoes;
                    $rootScope.notificacoes = notificacoes.filter(x => !x.lida).length;
                }, function (error) {
                    vm.error = error;
                    $rootScope.notificacoes = 0;
                });
        };

        vm.read = function (id) {
            vm.areSubmitting = true;

            notificacoesService
                .read(id)
                .then(function () {
                    vm.areSubmitting = false;
                    vm.list();
                }, function (error) {
                    vm.areSubmitting = false;
                    vm.error = error;
                });
        };
    }
})();