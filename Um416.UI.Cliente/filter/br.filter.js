(function () {
    'use strict';

    angular
        .module('ngApp')
        .filter('br', function ($sce) {
            return function (text) {
                if (text)
                    text = text.replace(/\n/g, "<br />");

                return $sce.trustAsHtml(text);
            };
        });
})();