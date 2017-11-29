(function () {
    'use strict';

    angular
        .module('ngApp')
        .config(registerRoutes)
        .config(['$provide', configureTemplateFactory]);

    function registerRoutes($locationProvider, $stateProvider, $urlRouterProvider) {

        $stateProvider.state('mapa', {
            abstract: true,
        });

        $stateProvider.state('cadastro', {
            abstract: true,
        });

        $stateProvider.state('finalizar', {
            abstract: true,
        });

        //Mapa
        $stateProvider.state("mapa.default", {
            url: '/loteamentos/:id',
            templateUrl: 'views/mapa.view.html',
            controller: 'mapaController',
            controllerAs: 'vm'
        });

        $stateProvider.state("mapa.indicador", {
            url: '/loteamentos/:id/indicador/:indicador',
            templateUrl: 'views/mapa.view.html',
            controller: 'mapaController',
            controllerAs: 'vm'
        });

        //Cadastro
        $stateProvider.state("cadastro.default", {
            url: '/loteamentos/:id/cadastro',
            templateUrl: 'views/cadastro.view.html',
            controller: 'cadastroController',
            controllerAs: 'vm'
        });

        $stateProvider.state("cadastro.indicador", {
            url: '/loteamentos/:id/indicador/:indicador/cadastro',
            templateUrl: 'views/cadastro.view.html',
            controller: 'cadastroController',
            controllerAs: 'vm'
        });

        //Finalizar
        $stateProvider.state("finalizar.default", {
            url: '/loteamentos/:id/finalizar',
            templateUrl: 'views/finalizar.view.html',
            controller: 'finalizarController',
            controllerAs: 'vm'
        });

        $stateProvider.state("finalizar.indicador", {
            url: '/loteamentos/:id/indicador/:indicador/finalizar',
            templateUrl: 'views/finalizar.view.html',
            controller: 'finalizarController',
            controllerAs: 'vm'
        });

        //Concluído
        $stateProvider.state("concluido", {
            url: '/concluido',
            templateUrl: 'views/concluido.view.html',
            controller: 'concluidoController',
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