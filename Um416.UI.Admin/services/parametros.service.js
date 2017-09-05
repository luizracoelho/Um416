(function () {
    'use strict';

    angular
        .module('ngApp')
        .service('parametrosService', parametrosService);

    function parametrosService($http, $q, baseUrl, loginService) {
        // Retorno
        var Service = function () {
            this.find = find;
            this.save = save;
        };

        return new Service();

        // Implementação
        function find() {
            var def = $q.defer();
            $http({
                method: "get",
                url: baseUrl + "parametros/1",
                headers: { 'Authorization': loginService.getToken() }
            })
            .then(function success(resp) {
                def.resolve(resp.data);
            }, function error(err) {
                def.reject("Não foi possível obter os parâmetros.");

                if (err.status == 401)
                    loginService.logout();
            });

            return def.promise;
        };

        function save(usuario) {
            var def = $q.defer();

            $http({
                method: "post",
                url: baseUrl + "parametros",
                headers: { 'Authorization': loginService.getToken() },
                data: usuario
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
    };
})();