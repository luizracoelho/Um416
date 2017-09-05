﻿(function () {
    'use strict';

    let dev = 'http://192.168.15.14/Um416/api/';
    let dist = 'http://um416.onsoft.ddns.net:8081/api/';

    let baseUrl = dev;

    angular
        .module('ngApp', ['ui.router', 'angularMoment', 'ui.bootstrap', 'ui.utils.masks', 'idf.br-filters'])
        .constant('baseUrl', baseUrl)
        .constant('tokenUrl', baseUrl + 'token');
})();