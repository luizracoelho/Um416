(function () {
    'use strict';

    angular
        .module('ngApp')
        .controller('chamadosController', chamadosController);

    function chamadosController($rootScope, $scope, chamadosService) {
        var vm = this;

        vm.areSubmitting = false;

        vm.showGrid = function () {
            return vm.chamados != null && vm.chamados.length != 0;
        }

        vm.list = function () {
            chamadosService
                .list()
                .then(function (chamados) {
                    vm.chamados = chamados;
                    $rootScope.chamados = chamados.filter(x => !x.Encerrado).length;
                }, function (error) {
                    vm.error = error;
                    $rootScope.chamados = 0;
                });
        };

        vm.add = function () {
            vm.modalTitle = 'Adicionar';
            vm.errorDetail = null;
            vm.chamado = {
                Id: 0,
                Titulo: null,
                DataHoraCriacao: moment().format('DD/MM/YYYY HH:mm'),
                _DataHoraCriacao: moment().format('DD/MM/YYYY HH:mm')
            };
            vm.iteracao = {
                Id: 0,
                Conteudo: null
            }
            $scope.chamadoForm.$setPristine();
            $('#modalDetails').modal('show');
        }

        vm.find = function (id, action = null) {
            if (id != null)
                chamadosService
                    .find(id)
                    .then(function (chamado) {
                        $scope.chamadoForm.$setPristine();

                        vm.chamado = chamado;

                        vm.chamado._DataHoraCriacao = vm.chamado.DataHoraCriacao.toJsDateTime();

                        $scope.chamadoForm.$setPristine();

                        if (action == null) {
                            vm.modalTitle = 'Atualizar';
                            vm.errorDetail = null;

                            $('#modalDetails').modal('show');

                            $('#iteracoes').css('max-height', ($(document).height() - 400) + 'px');
                            $('#iteracoes').animate({ scrollTop: $(document).height() + 'px' }, 300);
                        }
                        else if (action == 'visualizar') {
                            vm.modalTitle = 'Visualizar';
                            vm.errorDetail = null;

                            $('#modalDetails').modal('show');

                            $('#iteracoes').css('max-height', ($(document).height() - 310) + 'px');
                            $('#iteracoes').animate({ scrollTop: $(document).height() + 'px' }, 300);
                        }
                        else if (action == 'encerrar')
                            $('#modalClose').modal('show');

                    }, function (error) {
                        vm.error = error;
                    });
        };

        vm.save = function () {
            vm.areSubmitting = true;

            vm.chamado.DataHoraCriacao = vm.chamado._DataHoraCriacao.toCsDateTime();

            chamadosService
                .save(vm.chamado, vm.iteracao)
                .then(function () {
                    vm.areSubmitting = false;
                    vm.list();
                    $('#modalDetails').modal('hide');
                }, function (error) {
                    vm.areSubmitting = false;
                    vm.errorDetail = error;
                    $('#modalDetails').animate({ scrollTop: '0px' }, 300);
                });
        };

        vm.close = function () {
            vm.areSubmitting = true;
            chamadosService
                .close(vm.chamado)
                .then(function () {
                    vm.areSubmitting = false;
                    vm.list();
                    $('#modalClose').modal('hide');
                }, function (error) {
                    vm.areSubmitting = false;
                    vm.errorDetail = error;
                    $('#modalClose').animate({ scrollTop: '0px' }, 300);
                });
        };
    }
})();