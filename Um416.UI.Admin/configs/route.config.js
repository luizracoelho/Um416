(function () {
    'use strict';

    angular
        .module('ngApp')
        .config(registerRoutes)
        .config(['$provide', configureTemplateFactory]);

    function registerRoutes($locationProvider, $stateProvider, $urlRouterProvider) {

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

        var vendedoresState = {
            url: '/vendedores',
            templateUrl: 'views/vendedores.view.html',
            controller: 'vendedoresController',
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

        var empresasState = {
            url: '/empresas',
            templateUrl: 'views/empresas.view.html',
            controller: 'empresasController',
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

        var parametrosState = {
            url: '/parametros',
            templateUrl: 'views/parametros.view.html',
            controller: 'parametrosController',
            controllerAs: 'vm',
            resolve: {
                auth: function (loginService) {
                    loginService.authorize();
                }
            }
        };

        var configuracoesBoletosState = {
            url: '/configuracoesBoletos',
            templateUrl: 'views/configuracoesBoletos.view.html',
            controller: 'configuracoesBoletosController',
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
        $stateProvider.state("painel.usuarios", usuariosState);
        $stateProvider.state("painel.perfil", perfilState);
        $stateProvider.state("painel.alterar-senha", alterarSenhaState);
        $stateProvider.state("painel.vendedores", vendedoresState);
        $stateProvider.state("painel.clientes", clientesState);
        $stateProvider.state("painel.empresas", empresasState);
        $stateProvider.state("painel.notificacoes", notificacoesState);
        $stateProvider.state("painel.chamados", chamadosState);
        $stateProvider.state("painel.parametros", parametrosState);
        $stateProvider.state("painel.configuracoesBoletos", configuracoesBoletosState);

        //Outras rotas
        $urlRouterProvider.otherwise('/');
    }

    //Remover o cache dos templates
    function configureTemplateFactory($provide) {
        // Set a suffix outside the decorator function 
        var cacheBuster = Date.now().toString();

        function templateFactoryDecorator($delegate) {
            var fromUrl = angular.bind($delegate, $delegate.fromUrl);
            $delegate.fromUrl = function (url, params) {
                if (url !== null && angular.isDefined(url) && angular.isString(url)) {
                    url += (url.indexOf("?") === -1 ? "?" : "&");
                    url += "v=" + cacheBuster;
                }

                return fromUrl(url, params);
            };

            return $delegate;
        }

        $provide.decorator('$templateFactory', ['$delegate', templateFactoryDecorator]);
    }
})();