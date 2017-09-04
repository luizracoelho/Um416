(function () {
    'use strict';

    angular
        .module('ngApp')
        .service('empresasService', empresasService);

    function empresasService($http, $q, baseUrl, loginService) {
        // Retorno
        var Service = function () {
            this.list = list;
            this.find = find;
            this.save = save;
            this.remove = remove;
            this.saveProfile = saveProfile;
            this.changePassword = changePassword;
        };

        return new Service();

        // Implementação
        function list() {
            var def = $q.defer();
            $http({
                method: "get",
                url: baseUrl + "usuariosEmpresas",
                headers: { 'Authorization': loginService.getToken() }
            })
            .then(function success(resp) {
                def.resolve(resp.data);
            }, function error(err) {
                def.reject("Não foi possível obter os registros.");

                if (err.status == 401)
                    loginService.logout();
            });

            return def.promise;
        };

        function find(id) {
            var def = $q.defer();
            $http({
                method: "get",
                url: baseUrl + "usuariosEmpresas/" + id,
                headers: { 'Authorization': loginService.getToken() }
            })
            .then(function success(resp) {
                def.resolve(resp.data);
            }, function error(err) {
                def.reject("Não foi possível obter o registro.");

                if (err.status == 401)
                    loginService.logout();
            });

            return def.promise;
        };

        function save(usuario) {
            var def = $q.defer();

            $http({
                method: "post",
                url: baseUrl + "usuariosEmpresas",
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

        function remove(usuarioId) {
            var def = $q.defer();

            $http({
                method: "delete",
                url: baseUrl + "usuariosEmpresas/" + usuarioId,
                headers: { 'Authorization': loginService.getToken() },
            })
            .then(function success(resp) {
                def.resolve("Removido com sucesso.");
            }, function error(err) {
                def.reject(err.data.exceptionMessage);

                if (err.status == 401)
                    loginService.logout();
            });

            return def.promise;
        };

        function saveProfile(usuario) {
            var def = $q.defer();

            $http({
                method: "post",
                url: baseUrl + "usuariosEmpresas/name",
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

        function changePassword(perfilSenha) {
            var def = $q.defer();

            $http({
                method: "post",
                url: baseUrl + "usuariosEmpresas/password",
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