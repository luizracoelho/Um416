(function () {
    'use strict';

    angular
        .module('ngApp')
        .service('vendasService', vendasService);

    function vendasService($http, $q, baseUrl, loginService) {
        // Retorno
        var Service = function () {
            this.find = find;
            this.findByLote = findByLote;
        };

        return new Service();

        // Implementação
        function findByLote(loteId) {
            var def = $q.defer();
            $http({
                method: "get",
                url: baseUrl +  `vendas/lotes/${loteId}`,
                headers: { 'Authorization': loginService.getToken() }
            })
                .then(function success(resp) {
                    def.resolve(resp.data);
                }, function error(err) {
                    def.reject("Não foi possível obter a venda.");

                    if (err.status == 401)
                        loginService.logout();
                });

            return def.promise;
        };

        function find(id) {
            var def = $q.defer();
            $http({
                method: "get",
                url: baseUrl + "vendas/" + id,
                headers: { 'Authorization': loginService.getToken() }
            })
                .then(function success(resp) {
                    def.resolve(resp.data);
                }, function error(err) {
                    def.reject("Não foi possível obter a venda.");

                    if (err.status == 401)
                        loginService.logout();
                });

            return def.promise;
        };
    };
})();