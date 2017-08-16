
(function () {
    'use strict';

    angular
        .module('ngApp')
        .service('lotesService', lotesService);

    function lotesService($http, $q, baseUrl, loginService) {
        // Retorno
        var Service = function () {
            this.listAdd = listAdd;
            this.list = list;
            this.find = find;
            this.save = save;
            this.saveMultiple = saveMultiple;
            this.remove = remove;
        };

        return new Service();

        // Implementação
        function list(id) {
            var def = $q.defer();
            $http({
                method: "get",
                url: baseUrl + `loteamentos/${id}/lotes`,
                headers: { 'Authorization': loginService.getToken() }
            })
                .then(function success(resp) {
                    def.resolve(resp.data);
                }, function error(err) {
                    def.reject("Não foi possível obter os lotes.");

                    if (err.status == 401)
                        loginService.logout();
                });

            return def.promise;
        };

        function listAdd(id) {
            var def = $q.defer();
            $http({
                method: "get",
                url: baseUrl + `loteamentos/${id}/generate`,
                headers: { 'Authorization': loginService.getToken() }
            })
                .then(function success(resp) {
                    def.resolve(resp.data);
                }, function error(err) {
                    def.reject("Não foi possível obter os lotes.");

                    if (err.status == 401)
                        loginService.logout();
                });

            return def.promise;
        };

        function find(id) {
            var def = $q.defer();

            $http({
                method: "get",
                url: baseUrl + "lotes/" + id,
                headers: { 'Authorization': loginService.getToken() }
            })
                .then(function success(resp) {
                    def.resolve(resp.data);
                }, function error(err) {
                    def.reject("Não foi possível obter o registro.");

                    if (err.status == 401)
                        loginService.logout();
                });

            return def.promise;
        };

        function save(lote) {
            var def = $q.defer();

            $http({
                method: "post",
                url: baseUrl + "lotes",
                headers: { 'Authorization': loginService.getToken() },
                data: lote
            })
                .then(function success(resp) {
                    def.resolve("Salvo com sucesso.");
                }, function error(err) {
                    def.reject(err.data.exceptionMessage);

                    if (err.status == 401)
                        loginService.logout();
                });

            return def.promise;
        };

        function saveMultiple(lotes) {
            var def = $q.defer();

            $http({
                method: "post",
                url: baseUrl + 'lotes/multiple',
                headers: { 'Authorization': loginService.getToken() },
                data: lotes
            })
                .then(function success(resp) {
                    def.resolve("Salvo com sucesso.");
                }, function error(err) {
                    def.reject(err.data.exceptionMessage);

                    if (err.status == 401)
                        loginService.logout();
                });

            return def.promise;
        };

        function remove(id) {
            var def = $q.defer();

            $http({
                method: "delete",
                url: baseUrl + "lotes/" + id,
                headers: { 'Authorization': loginService.getToken() },
            })
                .then(function success(resp) {
                    def.resolve("Removido com sucesso.");
                }, function error(err) {
                    def.reject(err.data.exceptionMessage);

                    if (err.status == 401)
                        loginService.logout();
                });

            return def.promise;
        };
    };
})();