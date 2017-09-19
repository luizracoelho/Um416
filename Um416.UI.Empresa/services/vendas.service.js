(function () {
    'use strict';

    angular
        .module('ngApp')
        .service('vendasService', vendasService);

    function vendasService($http, $q, baseUrl, loginService) {
        // Retorno
        var Service = function () {
            this.list = list;
            this.find = find;
        };

        return new Service();

        // Implementação
        function list() {
            var def = $q.defer();

            var login = loginService.getLogin();

            if (login != null) {
                $http({
                    method: "get",
                    url: baseUrl + `clientes/${login.id}/vendas`,
                    headers: { 'Authorization': loginService.getToken() }
                })
                    .then(function success(resp) {
                        def.resolve(resp.data);
                    }, function error(err) {
                        def.reject(err);
                        //def.reject("Não foi possível obter as vendas.");

                        if (err.status == 401)
                            loginService.logout();
                    });
            }

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