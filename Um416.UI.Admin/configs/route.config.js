(function () {
    'use strict';

    angular
        .module('ngApp')
        .config(function ($locationProvider, $stateProvider, $urlRouterProvider) {

            var loginState = {
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

            var logoutState = {
                url: '/logout',
                templateUrl: 'views/login.view.html',
                controller: 'loginController',
                controllerAs: 'vm',
                resolve: {
                    auth: function (loginService) {
                        loginService.authorize();
                    }
                }
            };

            var homeState = {
                url: '/',
                templateUrl: 'views/home.view.html',
                controller: 'homeController',
                controllerAs: 'vm',
                resolve: {
                    auth: function (loginService) {
                        loginService.authorize();
                    }
                }
            };

            var usuariosState = {
                url: '/usuarios',
                templateUrl: 'views/usuarios.view.html',
                controller: 'usuariosController',
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

            var clientesState = {
                url: '/clientes',
                templateUrl: 'views/clientes.view.html',
                controller: 'clientesController',
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

            var loteamentosState = {
                url: '/loteamentos',
                templateUrl: 'views/loteamentos.view.html',
                controller: 'loteamentosController',
                controllerAs: 'vm',
                resolve: {
                    auth: function (loginService) {
                        loginService.authorize();
                    }
                }
            };

            $stateProvider.state("login", loginState);
            $stateProvider.state("logout", logoutState);
            $stateProvider.state("home", homeState);
            $stateProvider.state("usuarios", usuariosState);
            $stateProvider.state("perfil", perfilState);
            $stateProvider.state("alterar-senha", alterarSenhaState);
            $stateProvider.state("clientes", clientesState);
            $stateProvider.state("notificacoes", notificacoesState);
            $stateProvider.state("chamados", chamadosState);
            $stateProvider.state("loteamentos", loteamentosState);

            //Outras rotas
            $urlRouterProvider.otherwise('/');
        });
})();