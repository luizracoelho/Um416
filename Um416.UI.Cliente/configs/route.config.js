(function () {
    'use strict';

    angular
        .module('ngApp')
        .config(function ($locationProvider, $stateProvider, $urlRouterProvider) {

            var loginState = {
                abstract: true,
                views: {
                    'login': {
                        templateUrl: 'views/login.layout.html'
                    }
                }
            };

            var painelState = {
                abstract: true,
                views: {
                    'painel': {
                        templateUrl: 'views/painel.layout.html'
                    }
                }
            };

            var entrarState = {
                url: '/login',
                templateUrl: 'views/login.view.html',
                controller: 'loginController',
                controllerAs: 'vm',
                resolve: {
                    auth: function (loginService) {
                        if (loginService.getLogin() != null)
                            window.location = "/";
                    }
                }
            };

            var homeState = {
                url: '/',
                templateUrl: 'views/vendas.view.html',
                controller: 'vendasController',
                controllerAs: 'vm',
                resolve: {
                    auth: function (loginService) {
                        loginService.authorize();
                    }
                }
            };

            var perfilState = {
                url: '/perfil',
                templateUrl: 'views/perfil.view.html',
                controller: 'usuariosController',
                controllerAs: 'vm',
                resolve: {
                    auth: function (loginService) {
                        loginService.authorize();
                    }
                }
            };

            var alterarSenhaState = {
                url: '/alterar-senha',
                templateUrl: 'views/alterar-senha.view.html',
                controller: 'usuariosController',
                controllerAs: 'vm',
                resolve: {
                    auth: function (loginService) {
                        loginService.authorize();
                    }
                }
            };

            var notificacoesState = {
                url: '/notificacoes',
                templateUrl: 'views/notificacoes.view.html',
                controller: 'notificacoesController',
                controllerAs: 'vm',
                resolve: {
                    auth: function (loginService) {
                        loginService.authorize();
                    }
                }
            };

            var chamadosState = {
                url: '/chamados',
                templateUrl: 'views/chamados.view.html',
                controller: 'chamadosController',
                controllerAs: 'vm',
                resolve: {
                    auth: function (loginService) {
                        loginService.authorize();
                    }
                }
            };

            var titulosState = {
                url: '/titulos/compra/:vendaId',
                templateUrl: 'views/titulos.view.html',
                controller: 'titulosController',
                controllerAs: 'vm',
                resolve: {
                    auth: function (loginService) {
                        loginService.authorize();
                    }
                }
            };

            $stateProvider.state("login", loginState);
            $stateProvider.state("login.entrar", entrarState);
            $stateProvider.state("painel", painelState);
            $stateProvider.state("painel.home", homeState);
            $stateProvider.state("painel.perfil", perfilState);
            $stateProvider.state("painel.alterar-senha", alterarSenhaState);
            $stateProvider.state("painel.notificacoes", notificacoesState);
            $stateProvider.state("painel.chamados", chamadosState);
            $stateProvider.state("painel.titulos", titulosState);

            //Outras rotas
            $urlRouterProvider.otherwise('/');
        });
})();