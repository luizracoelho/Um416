(function () {
    'use strict';

    angular
        .module('ngApp')
        .service('notificacoesService', notificacoesService);

    function notificacoesService($http, $q, baseUrl, loginService) {
        // Retorno
        var Service = function () {
            this.list = list;
            this.find = find;
            this.save = save;
            this.remove = remove;
        };

        return new Service();

        // Implementação
        function list() {
            var def = $q.defer();
            $http({
                method: "get",
                url: baseUrl + "notificacoes",
                headers: { 'Authorization': loginService.getToken() }
            })
            .then(function success(resp) {
                def.resolve(resp.data);
            }, function error(err) {
                def.reject("Não foi possível obter as notificações.");

                if (err.status == 401)
                    loginService.logout();
            });

            return def.promise;
        };

        function find(id) {
            var def = $q.defer();
            $http({
                method: "get",
                url: baseUrl + "notificacoes/" + id,
                headers: { 'Authorization': loginService.getToken() }
            })
            .then(function success(resp) {
                def.resolve(resp.data);
            }, function error(err) {
                def.reject("Não foi possível obter a notificação.");

                if (err.status == 401)
                    loginService.logout();
            });

            return def.promise;
        };

        function save(notificacao) {
            var def = $q.defer();

            $http({
                method: "post",
                url: baseUrl + "notificacoes",
                headers: { 'Authorization': loginService.getToken() },
                data: notificacao
            })
            .then(function success(resp) {
                def.resolve("Notificação salva com sucesso.");
            }, function error(err) {
                def.reject(err.data.exceptionMessage);

                if (err.status == 401)
                    loginService.logout();
            });

            return def.promise;
        };

        function remove(notificacaoId) {
            var def = $q.defer();

            $http({
                method: "delete",
                url: baseUrl + "notificacoes/" + notificacaoId,
                headers: { 'Authorization': loginService.getToken() },
            })
            .then(function success(resp) {
                def.resolve("Notificação removida com sucesso.");
            }, function error(err) {
                def.reject(err.data.exceptionMessage);

                if (err.status == 401)
                    loginService.logout();
            });

            return def.promise;
        };
    };
})();