(function () {
    'use strict';

    angular
        .module('ngApp')
        .service('usuariosService', usuariosService);

    function usuariosService($http, $q, baseUrl, loginService) {
        // Retorno
        var Service = function () {
            this.find = find;
            this.saveProfile = saveProfile;
            this.changePassword = changePassword;
        };

        return new Service();

        // Implementação
        function find(id) {
            var def = $q.defer();

            if (id == null)
                id = loginService.getLogin().id;

            $http({
                method: "get",
                url: baseUrl + "usuariosClientes/" + id,
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

        function saveProfile(usuario) {
            var def = $q.defer();

            $http({
                method: "post",
                url: baseUrl + "usuariosClientes/name",
                headers: { 'Authorization': loginService.getToken() },
                data: usuario
            })
            .then(function success(resp) {
                def.resolve("Usuário salvo com sucesso.");
            }, function error(err) {
                def.reject(err.data.exceptionMessage);

                if (err.status == 401)
                    loginService.logout();
            });

            return def.promise;
        };

        function changePassword(perfilSenha) {
            var def = $q.defer();

            $http({
                method: "post",
                url: baseUrl + "usuariosClientes/password",
                headers: { 'Authorization': loginService.getToken() },
                data: perfilSenha
            })
            .then(function success(resp) {
                def.resolve("Senha alterada com sucesso.");
            }, function error(err) {
                def.reject(err.data.exceptionMessage);

                if (err.status == 401)
                    loginService.logout();
            });

            return def.promise;
        };
    };
})();