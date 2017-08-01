(function () {
    'use strict';

    angular
        .module('ngApp')
        .controller('loginController', loginController);

    function loginController($rootScope, loginService) {
        var vm = this;

        vm.areSubmitting = false;

        vm.init = function () {
            $rootScope.token = null;
            $rootScope.login = null;
        }

        vm.autenticar = function () {
            vm.areSubmitting = true;

            loginService
                .login(vm.login, vm.senha)
                .then(function () {
                    vm.areSubmitting = false;
                }, function (error) {
                    vm.areSubmitting = false;
                    vm.error = error;
                });
        }
    }
})();