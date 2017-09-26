(function () {
    'use strict';

    angular
        .module('ngApp')
        .config(registerRoutes)
        .config(configureTemplateFactory);

    function registerRoutes($locationProvider, $stateProvider, $urlRouterProvider) {

        //Mapa
        $stateProvider.state("mapa", {
            url: '/loteamentos/:id',
            templateUrl: 'views/mapa.view.html',
            controller: 'mapaController',
            controllerAs: 'vm'
        });

        //Cadastro
        $stateProvider.state("cadastro", {
            url: '/loteamentos/:id/cadastro',
            templateUrl: 'views/cadastro.view.html',
            controller: 'cadastroController',
            controllerAs: 'vm'
        });

        //Finalizar
        $stateProvider.state("finalizar", {
            url: '/loteamentos/:id/finalizar',
            templateUrl: 'views/finalizar.view.html',
            controller: 'finalizarController',
            controllerAs: 'vm'
        });

        //Error
        $stateProvider.state("erro", {
            url: '/erro',
            templateUrl: 'views/erro.view.html',
            controller: 'erroController',
            controllerAs: 'vm'
        });

        //Outras rotas
        $urlRouterProvider.otherwise('/erro');
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