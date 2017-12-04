(function () {
    'use strict';

    angular
        .module('ngApp')
        .controller('homeController', homeController);

    function homeController($rootScope, loginService, dashboardService) {
        var vm = this;

        vm.dashboard = null;
        vm.error = null;

        vm.init = function () {
            $rootScope.login = loginService.getLogin();

            vm.comprasCollapse = true;
            vm.arvoresCollapse = true;
            vm.debitosCollapse = true;

            vm.getDashboard();

            //notificacoesService
            //    .list()
            //    .then(function (notificacoes) {
            //        $rootScope.notificacoes = notificacoes.filter(x => !x.lida).length;
            //    }, function (error) {
            //        $rootScope.notificacoes = 0;
            //    });

            //chamadosService
            //    .list()
            //    .then(function (chamados) {
            //        $rootScope.chamados = chamados.filter(x => !x.encerrado).length;
            //    }, function (error) {
            //        $rootScope.chamados = 0;
            //    });
        };

        vm.getDashboard = function () {
            dashboardService
                .getDashboard()
                .then(function (dashboard) {
                    vm.dashboard = dashboard;
                    if (dashboard.indicacoes) {
                        vm.drawChart(dashboard.indicacoes);
                    }
                }, function (error) {
                    vm.error = error;
                })
        };

        vm.drawChart = function (indicacoes) {
            var array = [['Mês/Ano', 'Indicações']];

            if (Array.isArray(indicacoes)) {
                indicacoes.forEach(function (indicacao) {
                    array.push([Object.keys(indicacao)[0], Object.values(indicacao)[0]]);
                });
            }
            else {
                if (!angular.equals({}, indicacoes)) {
                    array.push([Object.keys(indicacoes)[0], Object.values(indicacoes)[0]]);
                }
                else {
                    array.push(["", 0]);
                }
            }

            var data = google.visualization.arrayToDataTable(array);

            var chart = new google.visualization.AreaChart(document.getElementById('chart_div'));
            chart.draw(data, {
                legend: 'bottom',
                chartArea: { left: '5%', top: '5%', width: "90%", height: "80%" }
            });
        };

        vm.copy = function (url) {
            var temp = $('<input>');
            $('body').append(temp);
            temp.val(url).select();
            document.execCommand('copy');
            temp.remove();
        };

        vm.logout = function () {
            loginService.logout();
        };
    }
})();