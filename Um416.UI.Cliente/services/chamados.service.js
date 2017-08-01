(function () {
    'use strict';

    angular
        .module('ngApp')
        .service('chamadosService', chamadosService);

    function chamadosService($http, $q, baseUrl, loginService) {
        // Retorno
        var Service = function () {
            this.list = list;
            this.find = find;
            this.save = save;
            this.close = close;
        };

        return new Service();

        // Implementação
        function list() {
            var def = $q.defer();

            var login = loginService.getLogin();

            if (login != null) {
                $http({
                    method: "get",
                    url: baseUrl + `chamados/${login.Id}/list`,
                    headers: { 'Authorization': loginService.getToken() }
                })
                    .then(function success(resp) {
                        def.resolve(resp.data);
                    }, function error(err) {
                        def.reject("Não foi possível obter as notificações.");

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
                url: baseUrl + "chamados/" + id,
                headers: { 'Authorization': loginService.getToken() }
            })
                .then(function success(resp) {
                    def.resolve(resp.data);
                }, function error(err) {
                    def.reject("Não foi possível obter o chamado.");

                    if (err.status == 401)
                        loginService.logout();
                });

            return def.promise;
        };

        function save(chamado, iteracao) {
            var def = $q.defer();

            chamado.ClienteId = loginService.getLogin().Id;

            $http({
                method: "post",
                url: baseUrl + "chamados/clientes",
                headers: { 'Authorization': loginService.getToken() },
                data: {
                    'Chamado': chamado,
                    'Iteracao': iteracao
                }
            })
                .then(function success(resp) {
                    def.resolve("Chamado salvo com sucesso.");
                }, function error(err) {
                    def.reject(err.data.ExceptionMessage);

                    if (err.status == 401)
                        loginService.logout();
                });

            return def.promise;
        };

        function close(chamado) {
            var def = $q.defer();

            $http({
                method: "post",
                url: baseUrl + "chamados",
                headers: { 'Authorization': loginService.getToken() },
                data: chamado
            })
                .then(function success(resp) {
                    def.resolve("Chamado encerrado com sucesso.");
                }, function error(err) {
                    def.reject(err.data.ExceptionMessage);

                    if (err.status == 401)
                        loginService.logout();
                });

            return def.promise;
        };
    };
})();