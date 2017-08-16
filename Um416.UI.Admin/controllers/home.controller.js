(function () {
    'use strict';

    angular
        .module('ngApp')
        .controller('homeController', homeController);

    function homeController($rootScope, loginService, chamadosService) {
        var vm = this;

        vm.init = function () {
            $rootScope.login = loginService.getLogin();

            chamadosService
                .list()
                .then(function (chamados) {
                    $rootScope.chamados = chamados.filter(x => !x.encerrado).length;
                }, function (error) {
                    $rootScope.chamados = 0;
                });
        };

        vm.logout = function () {
            loginService.logout();
        };
    }
})();