(function () {
    'use strict';

    angular
        .module('ngApp')
        .config(function ($locationProvider, $stateProvider, $urlRouterProvider) {

            //Mapa
            $stateProvider.state("mapa", {
                url: '/loteamentos/:id',
                templateUrl: 'views/mapa.view.html',
                controller: 'mapaController',
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
        });
})();