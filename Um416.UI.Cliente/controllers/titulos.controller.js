﻿(function () {
    'use strict';

    angular
        .module('ngApp')
        .controller('titulosController', titulosController);

    function titulosController($rootScope, $scope, $stateParams, loginService, titulosService) {
        var vm = this;

        vm.areSubmitting = false;

        vm.showGrid = function () {
            return vm.titulos != null && vm.titulos.length != 0;
        }

        vm.list = function () {
            titulosService
                .list($stateParams.vendaId)
                .then(function (titulos) {
                    vm.titulos = titulos;

                    vm.titulos.forEach(function (titulo) {
                        if (titulo.pago == true) {
                            titulo.status = "Paga";
                            titulo.icone = "fa-check";
                            titulo.corStatus = 'success';
                        }
                        else {
                            if (new Date(titulo.dataVencimento) < new Date()) {
                                titulo.status = "Vencida";
                                titulo.icone = "fa-ban";
                                titulo.corStatus = 'danger';
                            }
                            else {
                                titulo.status = "Em Aberto";
                                titulo.icone = "fa-exclamation-triangle";
                                titulo.corStatus = 'warning';
                            }
                        }
                    });
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