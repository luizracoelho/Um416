(function () {
    'use strict';

    angular
        .module('ngApp')
        .service('dashboardService', dashboardService);

    function dashboardService($http, $q, baseUrl, loginService) {
        // Retorno
        var Service = function () {
            this.getDashboard = getDashboard;
        };

        return new Service();

        // Implementação
        function getDashboard() {
            var def = $q.defer();

            var login = loginService.getLogin();

            if (login != null) {
                $http({
                    method: "get",
                    url: baseUrl + `clientes/${login.id}/dashboard`,
                    headers: { 'Authorization': loginService.getToken() }
                })
                    .then(function success(resp) {
                        def.resolve(resp.data);
                    }, function error(err) {
                        def.reject("Não foi possível obter o Dashboard.");

                        if (err.status == 401)
                            loginService.logout();
                    });
            }

            return def.promise;
        };
    };
})();