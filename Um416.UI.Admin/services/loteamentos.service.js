(function () {
    'use strict';

    angular
        .module('ngApp')
        .service('loteamentosService', loteamentosService);

    function loteamentosService($http, $q, baseUrl, loginService) {
        // Retorno
        var Service = function () {
            this.list = list;
            this.find = find;
            this.save = save;
            this.remove = remove;
        };

        return new Service();

        // Implementação
        function list() {
            var def = $q.defer();
            $http({
                method: "get",
                url: baseUrl + "loteamentos",
                headers: { 'Authorization': loginService.getToken() }
            })
            .then(function success(resp) {
                def.resolve(resp.data);
            }, function error(err) {
                def.reject("Não foi possível obter os loteamentos.");

                if (err.status == 401)
                    loginService.logout();
            });

            return def.promise;
        };

        function find(id) {
            var def = $q.defer();

            $http({
                method: "get",
                url: baseUrl + "loteamentos/" + id,
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

        function save(loteamento) {
            var def = $q.defer();

            $http({
                method: "post",
                url: baseUrl + "loteamentos",
                headers: { 'Authorization': loginService.getToken() },
                data: loteamento
            })
            .then(function success(resp) {
                def.resolve("Salvo com sucesso.");
            }, function error(err) {
                def.reject(err.data.ExceptionMessage);

                if (err.status == 401)
                    loginService.logout();
            });

            return def.promise;
        };

        function remove(id) {
            var def = $q.defer();

            $http({
                method: "delete",
                url: baseUrl + "loteamentos/" + id,
                headers: { 'Authorization': loginService.getToken() },
            })
            .then(function success(resp) {
                def.resolve("Removido com sucesso.");
            }, function error(err) {
                def.reject(err.data.ExceptionMessage);

                if (err.status == 401)
                    loginService.logout();
            });

            return def.promise;
        };
    };
})();