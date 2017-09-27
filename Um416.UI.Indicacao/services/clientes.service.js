(function () {
    'use strict';

    angular
        .module('ngApp')
        .service('clientesService', clientesService);

    function clientesService($http, $q, baseUrl) {
        // Retorno
        var Service = function () {
            this.find = find;
            this.save = save;
            this.getLogin = getLogin;
            this.findByLogin = findByLogin;
        };

        return new Service();

        // Implementação
        function find(id) {
            var def = $q.defer();
            $http({
                method: "get",
                url: baseUrl + "usuariosClientes/" + id,
            })
                .then(function success(resp) {
                    def.resolve(resp.data);
                }, function error(err) {
                    def.reject("Não foi possível obter o usuário.");
                });

            return def.promise;
        };

        function save(cliente) {
            var def = $q.defer();

            $http({
                method: "post",
                url: baseUrl + "usuariosClientes/id",
                data: cliente
            })
                .then(function success(resp) {
                    cliente.id = resp.data;
                    def.resolve(cliente);
                }, function error(err) {
                    def.reject(err.data.exceptionMessage);
                });

            return def.promise;
        };

        function getLogin(login) {
            var def = $q.defer();

            $http({
                method: "post",
                url: baseUrl + "usuariosClientes/login",
                data: login
            })
                .then(function success(resp) {
                    def.resolve(resp.data);
                }, function error(err) {
                    def.reject(err.data.exceptionMessage);
                });

            return def.promise;
        };

        function findByLogin(login) {
            var def = $q.defer();
            $http({
                method: "get",
                url: baseUrl + `usuariosClientes/login/${login}`,
            })
                .then(function success(resp) {
                    def.resolve(resp.data);
                }, function error(err) {
                    def.reject("Não foi possível obter o usuário.");
                });

            return def.promise;
        };
    };
})();