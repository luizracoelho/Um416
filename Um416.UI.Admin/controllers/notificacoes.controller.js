(function () {
    'use strict';

    angular
        .module('ngApp')
        .controller('notificacoesController', notificacoesController);

    function notificacoesController($scope, notificacoesService, ufsService) {
        var vm = this;

        vm.areSubmitting = false;

        vm.showGrid = function () {
            return vm.notificacoes != null && vm.notificacoes.length != 0;
        }

        vm.list = function () {
            notificacoesService
                .list()
                .then(function (notificacoes) {
                    vm.notificacoes = notificacoes;
                }, function (error) {
                    vm.error = error;
                });
        };

        vm.add = function () {
            vm.modalTitle = 'Adicionar';
            vm.errorDetail = null;
            vm.notificacao = {
                Id: 0,
                Titulo: null,
                Mensagem: null,
                DataHora: moment().format('DD/MM/YYYY HH:mm'),
                _DataHora: moment().format('DD/MM/YYYY HH:mm')
            };
            $scope.notificacaoForm.$setPristine();
            $('#modalDetails').modal('show');
        }

        vm.find = function (id, action = null) {
            if (id != null)
                notificacoesService
                    .find(id)
                    .then(function (notificacao) {
                        vm.notificacao = notificacao;

                        vm.notificacao._DataHora = vm.notificacao.DataHora.toJsDateTime();

                        $scope.notificacaoForm.$setPristine();

                        if (action == null) {
                            vm.modalTitle = 'Atualizar';
                            vm.errorDetail = null;

                            $('#modalDetails').modal('show');
                        }
                        if (action == 'duplicar') {
                            vm.modalTitle = 'Duplicar';
                            vm.errorDetail = null;

                            //Resetar os valores
                            vm.notificacao.Id = 0;
                            vm.notificacao.DataHora = moment().format('DD/MM/YYYY HH:mm');
                            vm.notificacao._DataHora = moment().format('DD/MM/YYYY HH:mm');

                            $('#modalDetails').modal('show');
                        }
                        else if (action == 'remover')
                            $('#modalRemove').modal('show');

                    }, function (error) {
                        vm.error = error;
                    });
        };

        vm.save = function () {
            vm.areSubmitting = true;

            vm.notificacao.DataHora = vm.notificacao._DataHora.toCsDateTime();

            notificacoesService
                .save(vm.notificacao)
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

        vm.remove = function () {
            vm.areSubmitting = true;
            notificacoesService
                .remove(vm.notificacao.Id)
                .then(function () {
                    vm.areSubmitting = false;
                    vm.list();
                    $('#modalRemove').modal('hide');
                }, function (error) {
                    vm.areSubmitting = false;
                    vm.errorDetail = error;
                    $('#modalRemove').animate({ scrollTop: '0px' }, 300);
                });
        };
    }
})();