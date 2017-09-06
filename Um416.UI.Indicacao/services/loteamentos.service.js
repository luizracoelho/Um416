(function () {
    'use strict';

    angular
        .module('ngApp')
        .service('loteamentosService', loteamentosService);

    function loteamentosService($http, $q, baseUrl) {
        // Retorno
        var Service = function () {
            this.find = find;
        };

        return new Service();

        // Implementação
        function find(id) {
            var def = $q.defer();

            $http({
                method: "get",
                url: baseUrl + `loteamentos/${id}/indicacao`,
            })
            .then(function success(resp) {
                def.resolve(resp.data);
            }, function error(err) {
                def.reject("Não foi possível obter o loteamento.");
            });

            return def.promise;
        };
    };
})();