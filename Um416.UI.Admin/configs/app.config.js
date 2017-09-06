(function () {
    'use strict';

    let dev = 'http://localhost/Um416/api/';
    let dist = 'http://um416.onsoft.ddns.net/api/';

    let baseUrl = dev;

    angular
        .module('ngApp', ['ui.router', 'angularMoment', 'ui.bootstrap', 'ui.utils.masks', 'idf.br-filters'])
        .constant('baseUrl', baseUrl)
        .constant('tokenUrl', baseUrl + 'token')
        .run(function ($rootScope, loginService) {
            $rootScope.$on('$stateChangeStart', function (event, next) {
                loginService.authorize();
            });
        })
})();