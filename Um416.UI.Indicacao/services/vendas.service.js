(function () {
    'use strict';

    angular
        .module('ngApp')
        .service('vendasService', vendasService);

    function vendasService($http, $q, baseUrl) {
        // Retorno
        var Service = function () {
            this.save = save;
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
    };
})();