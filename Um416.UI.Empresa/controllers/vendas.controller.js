(function () {
    'use strict';

    angular
        .module('ngApp')
        .controller('vendasController', vendasController);

    function vendasController($scope, vendasService) {
        var vm = this;

        vm.areSubmitting = false;

        vm.showGrid = function () {
            return vm.vendas != null && vm.vendas.length != 0;
        }

        vm.list = function () {
            vendasService
                .list()
                .then(function (vendas) {
                    vm.vendas = vendas;
                }, function (error) {
                    vm.error = error;
                });
        };

        vm.find = function (id, isRemoving = false) {
            if (id != null)
                vendasService
                    .find(id)
                    .then(function (venda) {
                        vm.limparCampos();

                        vm.venda = venda;

                        vm.venda._dataCadastro = vm.venda.dataCadastro.toJsDate();

                        $scope.vendaForm.$setPristine();

                        if (!isRemoving) {
                            vm.modalTitle = 'Atualizar';
                            vm.errorDetail = null;

                            $('#modalDetails').modal('show');
                        }
                        else
                            $('#modalRemove').modal('show');
                    }, function (error) {
                        vm.error = error;
                    });
        };
    }
})();