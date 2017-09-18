(function () {
    'use strict';

    angular
        .module('ngApp')
        .directive('input', input);

    function input($timeout) {
        return {
            restrict: 'E',
            link: function ($scope, $element, $attrs) {
                $element.bind('keydown', function ($event) {
                    $timeout(function () {
                        $event.target.selectionStart = $event.target.value.length;
                        $event.target.selectionEnd = $event.target.value.length;
                    });
                });
            }
        };
    }
})();