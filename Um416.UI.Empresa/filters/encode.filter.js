(function () {
    'use strict';

    angular
        .module('ngApp')
        .filter('encode', function () {
            return window.encodeURIComponent;
        });
})();