(function () {
    'use strict';

    angular
        .module('ngApp')
        .controller('arvoresController', arvoresController);

    function arvoresController($rootScope, $scope, $stateParams, loginService, vendasService) {
        var vm = this;

        vm.areSubmitting = false;

        vm.showGrid = function () {
            return vm.arvores != null && vm.arvores.length != 0;
        }

        vm.list = function () {
            vendasService
                .listArvores($stateParams.vendaId)
                .then(function (arvores) {
                    vm.arvores = arvores;
                }, function (error) {
                    vm.error = error;
                });
        };

        vm.find = function (id, indicador=false) {
            if (id != null)
                vendasService
                    .find(id)
                    .then(function (venda) {
                        vm.venda = venda;
                        //Informar se a venda que está sendo exibida é do indicador
                        vm.venda.indicador = indicador;
                        $('#modalDetails').animate({ scrollTop: 0 }, 300).modal('show');
                    }, function (error) {
                        vm.error = error;
                        $('html').animate({ scrollTop: 0 }, 300);
                    });
        };

        vm.copy = function (url) {
            var temp = $('<input>');
            $('body').append(temp);
            temp.val(url).select();
            document.execCommand('copy');
            temp.remove();
        }

        vm.carouselNext = function () {
            $('#carousel').carousel('next');
        }
        vm.carouselPrev = function () {
            $('#carousel').carousel('prev');
        }
    }
})();