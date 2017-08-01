(function () {
    'use strict';

    angular
        .module('ngApp')
        .service('ufsService', ufsService);

    function ufsService($http, $q, baseUrl, loginService) {
        // Retorno
        var Service = function () {
            this.list = list;
        };

        return new Service();

        // Implementação
        function list() {
            var def = $q.defer();
            $http({
                method: "get",
                url: baseUrl + "ufs",
                headers: { 'Authorization': loginService.getToken() }
            })
            .then(function success(resp) {
                def.resolve(resp.data);
            }, function error(err) {
                def.reject("Não foi possível obter as unidades federativas.");

                if (err.status == 401)
                    loginService.logout();
            });

            return def.promise;
        };
    };
})();