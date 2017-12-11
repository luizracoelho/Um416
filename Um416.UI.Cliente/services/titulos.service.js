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
            this.filtrar = filtrar;
            this.gerarBoleto = gerarBoleto;
            this.consultarBoleto = consultarBoleto;
        };

        return new Service();

        // Implementação
        function list(vendaId) {
            var login = loginService.getLogin();

            if (login != null) {
                var def = $q.defer();
                $http({
                    method: "get",
                    url: baseUrl + `clientes/${login.id}/venda/${vendaId}/titulos`,
                    headers: { 'Authorization': loginService.getToken() }
                })
                    .then(function success(resp) {
                        def.resolve(resp.data);
                    }, function error(err) {
                        def.reject("Não foi possível obter os títulos.");

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

        function filtrar(filtro) {
            var def = $q.defer();

            $http({
                method: "post",
                url: baseUrl + "titulos/filtrar",
                headers: { 'Authorization': loginService.getToken() },
                data: filtro
            })
                .then(function success(resp) {
                    def.resolve(resp.data);
                }, function error(err) {
                    def.reject(err.data.exceptionMessage);

                    if (err.status == 401)
                        loginService.logout();
                });

            return def.promise;
        }

        function gerarBoleto(id) {
            var def = $q.defer();

            var login = loginService.getLogin();

            $http({
                method: "get",
                url: baseUrl + `titulos/${id}/cliente/${login.id}/gerarboleto`,
                headers: { 'Authorization': loginService.getToken() },
            })
                .then(function success(resp) {
                    def.resolve(resp.data);

                }, function error(err) {
                    def.reject(err.data.exceptionMessage);

                    if (err.status == 401)
                        loginService.logout();
                });

            return def.promise;
        };

        function consultarBoleto(id) {
            var def = $q.defer();

            var login = loginService.getLogin();

            $http({
                method: "get",
                url: baseUrl + `titulos/${id}/cliente/${login.id}/consultarboleto`,
                headers: { 'Authorization': loginService.getToken() },
            }).then(function success(resp) {
                def.resolve(resp.data);
            }, function error(err) {
                def.reject(err.data.exceptionMessage);

                if (err.status == 401)
                    loginService.logout();
            });

            return def.promise;
        };
    };
})();