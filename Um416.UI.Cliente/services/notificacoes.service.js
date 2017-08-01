(function () {
    'use strict';

    angular
        .module('ngApp')
        .service('notificacoesService', notificacoesService);

    function notificacoesService($http, $q, baseUrl, loginService) {
        // Retorno
        var Service = function () {
            this.list = list;
            this.read = read;
        };

        return new Service();

        // Implementação
        function list() {
            var def = $q.defer();

            var login = loginService.getLogin();

            if (login != null) {
                $http({
                    method: "get",
                    url: baseUrl + `notificacoes/${login.Id}/clientes`,
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

        function read(id) {
            var def = $q.defer();

            var clienteId = loginService.getLogin().Id;

            $http({
                method: "get",
                url: baseUrl + `notificacoes/${id}/clientes/${clienteId}`,
                headers: { 'Authorization': loginService.getToken() }
            })
            .then(function success(resp) {
                def.resolve("Notificação salva com sucesso.");
            }, function error(err) {
                def.reject(err.data.ExceptionMessage);

                if (err.status == 401)
                    loginService.logout();
            });

            return def.promise;
        };
    };
})();