(function () {
    'use strict';

    angular
        .module('ngApp')
        .controller('cadastroController', cadastroController);

    function cadastroController($scope, $state, $stateParams, clientesService) {
        var vm = this;

        vm.areSubmitting = false;

        vm.sexos = [
            { id: 0, nome: 'Masculino' },
            { id: 1, nome: 'Feminino' }
        ];

        vm.init = function () {
            vm.clienteLogado = JSON.parse(sessionStorage.getItem('login'));
            vm.loteSelecionado = JSON.parse(sessionStorage.getItem('lote'));

            //Verificar se trocou de loteamento
            if (!vm.loteSelecionado ||$stateParams.id != vm.loteSelecionado.loteamentoId) {
                sessionStorage.removeItem('lote');
                sessionStorage.removeItem('login');

                $state.go('mapa', { id: $stateParams.id });
            }
        }

        vm.avancar = function () {
            $state.go('finalizar', { id: $stateParams.id });
        }

        vm.showCadastro = function () {
            vm.errorCadastro = null;
            vm.cliente = {
                id: 0,
                nome: null,
                cpf: null,
                rg: null,
                sexo: null,
                dataNascimento: null,
                _dataNascimento: null,
                dataCadastro: moment().format('DD/MM/YYYY'),
                _dataCadastro: moment().format('DD/MM/YYYY'),
                email: null,
                telFixo: null,
                telMovel: null,
                logradouro: null,
                numero: null,
                complemento: null,
                bairro: null,
                cidade: null,
                uf: null,
                cep: null,
                login: null,
                senha: null,
                confirmarSenha: null
            };
            $scope.clienteForm.$setPristine();
            $('#modalCadastro').modal('show');
        }

        vm.usuarioLogado = false;

        vm.save = function () {
            vm.areSubmitting = true;

            vm.cliente.dataNascimento = vm.cliente._dataNascimento.toCsDate();

            clientesService
                .save(vm.cliente)
                .then(function (cliente) {
                    vm.areSubmitting = false;
                    sessionStorage.setItem('login', JSON.stringify(cliente));
                    vm.usuarioLogado = true;
                    $('#modalCadastro').modal('hide');
                }, function (error) {
                    vm.areSubmitting = false;
                    vm.errorCadastro = error;
                    $('#modalCadastro').animate({ scrollTop: '0px' }, 300);
                });
        };

        vm.showLogin = function () {
            vm.errorLogin = null;
            vm.login = {
                login: null,
                senha: null
            };
            $scope.loginForm.$setPristine();
            $('#modalLogin').modal('show');
        }

        vm.logar = function () {
            vm.areSubmitting = true;

            clientesService
                .getLogin(vm.login)
                .then(function (cliente) {
                    console.log(cliente);
                    vm.areSubmitting = false;
                    sessionStorage.setItem('login', JSON.stringify(cliente));
                    vm.usuarioLogado = true;
                    $('#modalLogin').modal('hide');
                }, function (error) {
                    vm.areSubmitting = false;
                    vm.errorLogin = error;
                    $('#modalLogin').animate({ scrollTop: '0px' }, 300);
                });
        }

        $('#modalCadastro, #modalLogin').on('hidden.bs.modal', function () {
            if (vm.usuarioLogado)
                vm.avancar();
        })
    }
})();