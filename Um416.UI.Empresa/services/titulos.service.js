(function () {
    'use strict';

    angular
        .module('ngApp')
        .service('titulosService', titulosService);

    function titulosService($http, $q, baseUrl, loginService) {
        // Retorno
        var Service = function () {
            this.list = list;
            this.find = find;
            this.baixar = baixar;
            this.estornar = estornar;
        };

        return new Service();

        // Implementação
        function list(clienteId, vendaId) {
                var def = $q.defer();
                $http({
                    method: "get",
                    url: baseUrl + `clientes/${clienteId}/venda/${vendaId}/titulos`,
                    headers: { 'Authorization': loginService.getToken() }
                })
                    .then(function success(resp) {
                        def.resolve(resp.data);
                    }, function error(err) {
                        def.reject("Não foi possível obter os títulos.");

                        if (err.status == 401)
                            loginService.logout();
                    });

            return def.promise;
        };

        function find(id) {
            var def = $q.defer();
            $http({
                method: "get",
                url: baseUrl + "titulos/" + id,
                headers: { 'Authorization': loginService.getToken() }
            })
                .then(function success(resp) {
                    def.resolve(resp.data);
                }, function error(err) {
                    def.reject("Não foi possível obter o usuário.");

                    if (err.status == 401)
                        loginService.logout();
                });

            return def.promise;
        };

        function baixar(tituloId) {
            var def = $q.defer();
            $http({
                method: "get",
                url: baseUrl + `titulos/baixar/${tituloId}`,
                headers: { 'Authorization': loginService.getToken() }
            })
                .then(function success(resp) {
                    def.resolve(resp.data);
                }, function error(err) {
                    def.reject("Não foi possível baixar o título.");

                    if (err.status == 401)
                        loginService.logout();
                });

            return def.promise;
        };

        function estornar(tituloId) {
            var def = $q.defer();
            $http({
                method: "get",
                url: baseUrl + `titulos/estornar/${tituloId}`,
                headers: { 'Authorization': loginService.getToken() }
            })
                .then(function success(resp) {
                    def.resolve(resp.data);
                }, function error(err) {
                    def.reject("Não foi possível estornar o título.");

                    if (err.status == 401)
                        loginService.logout();
                });

            return def.promise;
        };
    };
})();