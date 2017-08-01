(function () {
    'use strict';

    angular
        .module('ngApp')
        .directive('onLoading', onLoading);

    onLoading.$injector = ['$http'];

    function onLoading($http) {
        return {
            restrict: 'E',
            link: function (scope, elem) {
                scope.isLoading = isLoading;

                scope.$watch(scope.isLoading, toggleElement);

                function toggleElement(loading) {

                    if (loading) {
                        elem[0].style.display = "block";
                    } else {
                        elem[0].style.display = "none";
                    }
                }

                function isLoading() {
                    return $http.pendingRequests.length > 0;
                }
            }
        }
    };

})();