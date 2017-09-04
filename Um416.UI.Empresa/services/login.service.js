(function () {
    'use strict';

    angular
        .module('ngApp')
        .service('loginService', loginService);

    function loginService($rootScope, $http, $q, $state, baseUrl, tokenUrl) {
        // Retorno
        var Service = function () {
            this.login = login;
            this.logout = logout;
            this.getToken = getToken;
            this.getLogin = getLogin;
            this.setLogin = setLogin;
            this.authorize = authorize;
        };

        return new Service();

        // Implementação
        function login(username, password) {
            var def = $q.defer();

            var login = {
                'grant_type': 'password',
                'username': username,
                'password': password,
                'regra': 'empresa'
            };

            var config = {
                headers: {
                    "Content-Type": "application/x-www-form-urlencoded;charset=utf-8"
                },
                transformRequest: function (obj) {
                    var str = [];
                    for (var p in obj)
                        str.push(encodeURIComponent(p) + "=" + encodeURIComponent(obj[p]));
                    return str.join("&");
                },
            };

            $http.post(tokenUrl, login, config)
                .then(function success(resp) {
                    var token = 'Bearer ' + resp.data.access_token;
                    localStorage.setItem('token', token);
                    def.resolve('Login bem sucedido.');

                    getLoginInfo(token)
                        .then(function (login) {
                            setLogin(login);
                            $state.go('painel.home');
                        }, function (error) {
                            def.reject('Não foi possível obter o usuário.');
                        });
                }, function error() {
                    def.reject('Usuário ou senha incorretos.');
                });

            return def.promise;
        };

        function getLoginInfo(token) {
            var def = $q.defer();
            $http({
                method: 'get',
                url: baseUrl + 'login/',
                headers: {
                    'Authorization': token
                }
            })
                .then(function success(resp) {
                    def.resolve(resp.data);
                }, function error() {
                    def.reject('Não foi possível recuperar o usuário.');
                });

            return def.promise;
        };

        function getToken() {
            return localStorage.getItem('token');
        };

        function getLogin() {
            var login = localStorage.getItem('login');
            return JSON.parse(login);
        };

        function setLogin(login) {
            localStorage.setItem('login', JSON.stringify(login));
        }

        function logout() {
            $state.go('login.entrar');
            localStorage.removeItem('token');
            localStorage.removeItem('login');
        }

        function authorize() {
            var login = getLogin();

            if (login == null)
                logout();

            return;
        };
    };
})();