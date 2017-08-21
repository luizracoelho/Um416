(function () {
    'use strict';

    angular
        .module('ngApp')
        .controller('homeController', homeController);

    function homeController($rootScope, loginService) {
        var vm = this;

        vm.init = function () {
            $rootScope.login = loginService.getLogin();
        };

        vm.logout = function () {
            loginService.logout();
        };
    }
})();