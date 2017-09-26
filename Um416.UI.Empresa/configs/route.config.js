(function () {
    'use strict';

    angular
        .module('ngApp')
        .config(registerRoutes)
        .config(configureTemplateFactory);

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

        var lotesState = {
            url: '/lotes',
            templateUrl: 'views/lotes.view.html',
            controller: 'lotesController',
            controllerAs: 'vm',
            resolve: {
                auth: function (loginService) {
                    loginService.authorize();
                }
            }
        };

        $stateProvider.state("login", loginState);
        $stateProvider.state("painel", painelState);
        $stateProvider.state("login.entrar", entrarState);
        $stateProvider.state("painel.home", homeState);
        $stateProvider.state("painel.perfil", perfilState);
        $stateProvider.state("painel.alterar-senha", alterarSenhaState);
        $stateProvider.state("painel.notificacoes", notificacoesState);
        $stateProvider.state("painel.chamados", chamadosState);
        $stateProvider.state("painel.loteamentos", loteamentosState);
        $stateProvider.state("painel.lotes", lotesState);

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