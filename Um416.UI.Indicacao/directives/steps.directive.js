(function () {
    'use strict';

    angular
        .module('ngApp')
        .directive('steps', steps);

    function steps() {
        return {
            restrict: 'E',
            scope: {
                step: '=?step',
                indicador: '=?indicador',
            },
            templateUrl: 'views/steps.component.html',
            link: function (scope, element, attrs) {
                scope.$watch('step', function (value) {
                    if (isNaN(value) || value <= 0)
                        scope.step = 1;
                    else if (value > 3)
                        scope.step = 3;
                });

                scope.lote = JSON.parse(sessionStorage.getItem('lote'));
                scope.login = JSON.parse(sessionStorage.getItem('login'));
            }
        };
    };

})();