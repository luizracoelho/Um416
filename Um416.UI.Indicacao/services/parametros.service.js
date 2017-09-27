(function () {
    'use strict';

    angular
        .module('ngApp')
        .service('parametrosService', parametrosService);

    function parametrosService($http, $q, baseUrl) {
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
                url: baseUrl + "parametros/" + id,
            })
                .then(function success(resp) {
                    def.resolve(resp.data);
                }, function error(err) {
                    def.reject("Não foi possível obter os parâmetros.");
                });

            return def.promise;
        };
    };
})();