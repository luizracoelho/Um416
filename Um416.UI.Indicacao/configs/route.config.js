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
        });
})();