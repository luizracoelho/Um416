(function () {
    'use strict';

    angular
        .module('ngApp')
        .service('vendasService', vendasService);

    function vendasService($http, $q, baseUrl) {
        // Retorno
        var Service = function () {
            this.save = save;
            this.find = find;
        };

        return new Service();

        // Implementação
        function save(venda) {
            var def = $q.defer();

            $http({
                method: "post",
                url: baseUrl + "vendas",
                data: venda
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

        function find(id) {
            var def = $q.defer();
            $http({
                method: "get",
                url: baseUrl + "vendas/" + id,
            })
                .then(function success(resp) {
                    def.resolve(resp.data);
                }, function error(err) {
                    def.reject("Não foi possível obter o indicador.");
                });

            return def.promise;
        };
    };
})();