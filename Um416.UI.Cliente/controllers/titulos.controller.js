(function () {
    'use strict';

    angular
        .module('ngApp')
        .controller('titulosController', titulosController);

    function titulosController($rootScope, $scope, $stateParams, loginService, titulosService) {
        var vm = this;

        vm.areSubmitting = false;

        vm.isFirst = true;

        vm.vendaId = $stateParams.vendaId;

        vm.showGrid = function () {
            return vm.titulos != null && vm.titulos.length != 0;
        }

        vm.list = function () {
            if (vm.isFirst == true)
                vm.createFiltro();

            if (vm.vendaId == null)
                vm.filtrar();
            else
                titulosService
                    .list(vm.vendaId)
                    .then(function (titulos) {
                        vm.titulos = titulos;

                        vm.titulos.forEach(function (titulo) {
                            titulo.valorDesconto = titulo.valor - titulo.valorPgto;

                            if (titulo.pago == true) {
                                titulo.status = "Paga";
                                titulo.icone = "fa-check";
                                titulo.corStatus = 'success';
                            }
                            else {
                                if (new Date(titulo.dataVencimento) < new Date()) {
                                    titulo.status = "Vencida";
                                    titulo.icone = "fa-ban";
                                    titulo.corStatus = 'danger';
                                }
                                else {
                                    titulo.status = "Em Aberto";
                                    titulo.icone = "fa-exclamation-triangle";
                                    titulo.corStatus = 'warning';
                                }
                            }
                        });
                    }, function (error) {
                        vm.error = error;
                    })

            vm.filtroCollapse = true;
        };

        vm.find = function (id) {
            if (id != null)
                titulosService
                    .find(id)
                    .then(function (titulo) {
                        $scope.tituloForm.$setPristine();

                        vm.titulo = titulo;

                        vm.errorDetail = null;

                        $('#modalDetails').modal('show');
                    }, function (error) {
                        vm.error = error;
                    });
        };

        vm.createFiltro = function () {
            var login = loginService.getLogin();

            vm.filtro = {
                clienteId: login.id,
                _dataEmissaoDe: null,
                dataEmissaoDe: null,
                _dataEmissaoAte: null,
                dataEmissaoAte: null,
                _dataVencimentoDe: moment().add(-1, 'months').format('DD/MM/YYYY'),
                dataVencimentoDe: moment().add(-1, 'months').format('DD/MM/YYYY'),
                _dataVencimentoAte: moment().add(1, 'months').format('DD/MM/YYYY'),
                dataVencimentoAte: moment().add(1, 'months').format('DD/MM/YYYY'),
                _dataPgtoDe: null,
                dataPgtoDe: null,
                _dataPgtoAte: null,
                dataPgtoAte: null,
                status: false
            };

            vm.isFirst = false;
        }

        vm.filtrar = function () {
            if (vm.filtro._dataVencimentoDe != null && vm.filtro._dataVencimentoAte != null) {
                vm.filtro.dataVencimentoDe = vm.filtro._dataVencimentoDe.toString().toCsDate();
                vm.filtro.dataVencimentoAte = vm.filtro._dataVencimentoAte.toString().toCsDate();
            }

            if (vm.filtro._dataEmissaoDe != null && vm.filtro._dataEmissaoAte != null) {
                vm.filtro.dataEmissaoDe = vm.filtro._dataEmissaoDe.toString().toCsDate();
                vm.filtro.dataEmissaoAte = vm.filtro._dataEmissaoAte.toString().toCsDate();
            }

            if (vm.filtro._dataPgtoDe != null && vm.filtro._dataPgtoAte != null) {
                vm.filtro.dataPgtoDe = vm.filtro._dataPgtoDe.toString().toCsDate();
                vm.filtro.dataPgtoAte = vm.filtro._dataPgtoAte.toString().toCsDate();
            }

            titulosService
                .filtrar(vm.filtro)
                .then(function (titulos) {
                    vm.titulos = titulos;

                    vm.titulos.forEach(function (titulo) {
                        titulo.valorDesconto = titulo.valor - titulo.valorPgto;

                        if (titulo.pago == true) {
                            titulo.status = "Paga";
                            titulo.icone = "fa-check";
                            titulo.corStatus = 'success';
                        }
                        else {
                            if (new Date(titulo.dataVencimento) < new Date()) {
                                titulo.status = "Vencida";
                                titulo.icone = "fa-ban";
                                titulo.corStatus = 'danger';
                            }
                            else {
                                titulo.status = "Em Aberto";
                                titulo.icone = "fa-exclamation-triangle";
                                titulo.corStatus = 'warning';
                            }
                        }
                    });
                }, function (error) {
                    vm.error = error;
                })
        };

        vm.gerarBoleto = function (id) {
            titulosService
                .gerarBoleto(id)
                .then(function (dados) {
                    vm.dc = dados;
                    if (vm.dc != null) {

                        // criando um form para enviar as informacoes via post
                        var form = document.createElement("form");
                        form.setAttribute("method", "post");
                        form.setAttribute("action", "https://shopline.itau.com.br/shopline/shopline.aspx");
                        form.setAttribute("target", "SHOPLINE");

                        var hiddenField = document.createElement("input");
                        hiddenField.setAttribute("type", "hidden");
                        hiddenField.setAttribute("name", "DC");
                        hiddenField.setAttribute("value", vm.dc);
                        form.appendChild(hiddenField);
                        document.body.appendChild(form);

                        var popup = window.open('', 'SHOPLINE', 'toolbar=yes,menubar=yes,resizable=yes,status=no,scrollbars=yes,width=675,height=485');

                        form.submit();

                        var intervalo = setInterval(function () {
                            if (popup == null || popup.closed) {
                                window.clearInterval(intervalo);
                                vm.consultarBoleto(id);
                            }
                        }, 1000);
                    }
                }, function (error) {
                    vm.error = error;
                });
        };

        vm.consultarBoleto = function (id) {
            titulosService
                .consultarBoleto(id)
                .then(function (dados) {
                    vm.filtrar();
                }, function (error) {
                    vm.error = error;
                });
        };
    }
})();