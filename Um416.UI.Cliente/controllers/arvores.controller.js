(function () {
    'use strict';

    angular
        .module('ngApp')
        .controller('arvoresController', arvoresController);

    function arvoresController($rootScope, $scope, loginService, vendasService) {
        var vm = this;

        vm.areSubmitting = false;

        vm.showGrid = function () {
            return vm.arvores != null && vm.arvores.length != 0;
        }

        vm.list = function () {
            vendasService
                .listArvores()
                .then(function (arvores) {
                    vm.arvores = arvores;
                }, function (error) {
                    vm.error = error;
                });
        };

        vm.find = function (id) {
            if (id != null)
                vendasService
                    .find(id)
                    .then(function (arvore) {
                        $scope.arvoreForm.$setPristine();

                        vm.arvore = arvore;

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

        vm.carouselNext = function () {
            $('#carousel').carousel('next');
        }
        vm.carouselPrev = function () {
            $('#carousel').carousel('prev');
        }
    }
})();