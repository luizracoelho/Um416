
(function () {
    'use strict';

    angular
        .module('ngApp')
        .service('lotesService', lotesService);

    function lotesService($http, $q, baseUrl) {
        // Retorno
        var Service = function () {
            this.list = list;
            this.find = find;
        };

        return new Service();

        // Implementação
        function list(id) {
            var def = $q.defer();
            $http({
                method: "get",
                url: baseUrl + `loteamentos/${id}/lotes`,
            }).then(function success(resp) {
                def.resolve(resp.data);
            }, function error(err) {
                def.reject("Não foi possível obter os lotes.");
            });

            return def.promise;
        };

        function find(id) {
            var def = $q.defer();

            $http({
                method: "get",
                url: baseUrl + "lotes/" + id,
            }).then(function success(resp) {
                def.resolve(resp.data);
            }, function error(err) {
                def.reject("Não foi possível obter o registro.");
            });

            return def.promise;
        };
    };
})();