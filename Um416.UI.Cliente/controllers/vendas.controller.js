(function () {
    'use strict';

    angular
        .module('ngApp')
        .controller('vendasController', vendasController);

    function vendasController($rootScope, $scope, loginService, vendasService, titulosService) {
        var vm = this;

        vm.areSubmitting = false;

        vm.showGrid = function () {
            return vm.vendas != null && vm.vendas.length != 0;
        }

        vm.list = function () {
            var login = loginService.getLogin();

            vendasService
                .list(login.id)
                .then(function (vendas) {
                    vm.vendas = vendas;

                    vm.vendas.forEach(function (venda) {
                        venda.totalDesconto = 0;

                        titulosService
                            .list(venda.id)
                            .then(function (titulos) {
                                vm.titulos = titulos;

                                vm.titulos.forEach(function (titulo) {
                                    if (titulo.pago == true)
                                        venda.totalDesconto += titulo.valor - titulo.valorPgto;
                                });
                            }, function (error) {
                                vm.error = error;
                            });
                    });
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

        vm.copy = function (url) {
            var temp = $('<input>');
            $('body').append(temp);
            temp.val(url).select();
            document.execCommand('copy');
            temp.remove();
        }
    }
})();