(function () {
    'use strict';

    angular
        .module('ngApp')
        .controller('homeController', homeController);

    function homeController($rootScope, loginService, dashboardService) {
        var vm = this;

        vm.dashboard = null;
        vm.error = null;

        vm.init = function () {
            $rootScope.login = loginService.getLogin();

            vm.comprasCollapse = true;
            vm.arvoresCollapse = true;
            vm.debitosCollapse = true;

            vm.getDashboard();

            //notificacoesService
            //    .list()
            //    .then(function (notificacoes) {
            //        $rootScope.notificacoes = notificacoes.filter(x => !x.lida).length;
            //    }, function (error) {
            //        $rootScope.notificacoes = 0;
            //    });

            //chamadosService
            //    .list()
            //    .then(function (chamados) {
            //        $rootScope.chamados = chamados.filter(x => !x.encerrado).length;
            //    }, function (error) {
            //        $rootScope.chamados = 0;
            //    });
        };

        vm.getDashboard = function () {
            dashboardService
                .getDashboard()
                .then(function (dashboard) {
                    vm.dashboard = dashboard;
                }, function (error) {
                    vm.error = error;
                })
        };


        vm.logout = function () {
            loginService.logout();
        };
    }
})();