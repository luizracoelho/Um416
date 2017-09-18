(function () {
    'use strict';

    angular
        .module('ngApp')
        .controller('titulosController', titulosController);

    function titulosController($rootScope, $scope, $stateParams, loginService, titulosService, notificacoesService, chamadosService) {
        var vm = this;

        vm.areSubmitting = false;

        vm.showGrid = function () {
            return vm.titulos != null && vm.titulos.length != 0;
        }

        vm.list = function () {
            console.log($stateParams.vendaId);
            titulosService
                .list($stateParams.vendaId)
                .then(function (titulos) {
                    vm.titulos = titulos;
                }, function (error) {
                    vm.error = error;
                });
        };

        vm.find = function (id) {
            if (id != null)
                titulosService
                    .find(id)
                    .then(function (titulo) {
                        $scope.tituloForm.$setPristine();

                        vm.titulo = titulo;

                        vm.errorDetail = null;

                        $('#modalDetails').modal('show');
                    }, function (error) {
                        vm.error = error;
                    });
        };
    }
})();