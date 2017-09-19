(function () {
    'use strict';

    angular
        .module('ngApp')
        .controller('vendasController', vendasController);

    function vendasController($rootScope, $scope, loginService, vendasService, notificacoesService, chamadosService, titulosService) {
        var vm = this;

        vm.areSubmitting = false;

        vm.showGrid = function () {
            return vm.vendas != null && vm.vendas.length != 0;
        }

        vm.init = function () {
            $rootScope.login = loginService.getLogin();

            notificacoesService
                .list()
                .then(function (notificacoes) {
                    $rootScope.notificacoes = notificacoes.filter(x => !x.lida).length;
                }, function (error) {
                    $rootScope.notificacoes = 0;
                });

            chamadosService
                .list()
                .then(function (chamados) {
                    $rootScope.chamados = chamados.filter(x => !x.encerrado).length;
                }, function (error) {
                    $rootScope.chamados = 0;
                });
        };

        vm.list = function () {
            var login = loginService.getLogin();

            vendasService
                .list(login.id)
                .then(function (vendas) {
                    vm.vendas = vendas;
                }, function (error) {
                    vm.error = error;
                });
        };

        vm.find = function (id) {
            if (id != null)
                vendasService
                    .find(id)
                    .then(function (venda) {
                        $scope.vendaForm.$setPristine();

                        vm.venda = venda;

                        vm.errorDetail = null;

                        $('#modalDetails').modal('show');
                    }, function (error) {
                        vm.error = error;
                    });
        };

        vm.logout = function () {
            loginService.logout();
        };

        vm.copy = function (url) {
            var temp = $('<input>');
            $('body').append(temp);
            temp.val(url).select();
            document.execCommand('copy');
            temp.remove();
        }
    }
})();