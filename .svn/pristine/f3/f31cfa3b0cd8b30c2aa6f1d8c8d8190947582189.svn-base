(function () {
    'use strict';

    angular
        .module('angle')
        .controller('idasTrnLsaReportController', idasTrnLsaReportController);

    idasTrnLsaReportController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams','DownloaddocumentService'];

    function idasTrnLsaReportController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams,DownloaddocumentService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'idasTrnLsaReportController';
        activate();
        function activate() {
            var url = 'api/idasTrnLsaReport/GetidasLsaSummary';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.idasTrnLsaReport = resp.data.idasTrnLsaReportSummaryList;
                unlockUI();
            });
        }
        //Export Excel
        $scope.exportlsareport = function () {
            var customer2sanction_gid;
            if ($scope.cbocustomer2sanction_gid == null || $scope.cbocustomer2sanction_gid == "" || $scope.cbocustomer2sanction_gid == undefined) {
                customer2sanction_gid = "";
            }
            else {
                customer2sanction_gid = $scope.cbocustomer2sanction_gid.customer2sanction_gid;
            }
            var params = {
                customer_gid: $scope.customer_gid,
                customer2sanction_gid: customer2sanction_gid,
            }

            lockUI();
            var url = 'api/idasTrnLsaReport/IdasExportExcel';
            SocketService.post(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    unlockUI();
                    DownloaddocumentService.Downloaddocument(resp.data.lscloudpath, resp.data.lsname);
                    // var phyPath = resp.data.lspath;
                    // var relPath = phyPath.split("EMS");
                    // var relpath1 = relPath[1].replace("\\", "/");
                    // var hosts = window.location.host;
                    // var prefix = location.protocol + "//";
                    // var str = prefix.concat(hosts, relpath1);
                    // var link = document.createElement("a");
                    // var name = resp.data.lsname.split('.');
                    // link.download = name[0];
                    // var uri = str;
                    // link.href = uri;
                    // link.click();
                }
                else {
                    unlockUI();
                    Notify.alert('Error Occurred While Export !')

                }

            });
        }

        $scope.complete = function (string) {
            if (string.length >= 3) {
                $scope.message = "";
                var url = 'api/customer/ExploreCustomer';
                var params = {
                    customername: string
                }
                SocketService.getparams(url, params).then(function (resp) {
                    if (resp.data.status == true) {
                        $scope.message = "";
                        $scope.customer_list = resp.data.Customers;
                    }
                    else {
                        $scope.message = "No Records";
                    }
                    
                });
            }
            else {
                $scope.customer_list = null;
                $scope.message = "Type atleast three character";
            }
        }

        $scope.fillTextbox = function (customer_gid, customer_name) {
            $scope.customer = customer_name;
            $scope.customer_gid = customer_gid;
            $scope.customer_list = null;
            var params = {
                customer_gid: customer_gid
            }
            var url = 'api/idasTrnLsaReport/Getcustomer2sanction';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.customer2sanction_list = resp.data.customersanction_list;

            });
        }

        $scope.all = function () {
            $scope.cbocustomer2sanction_gid.customer2sanction_gid = "";
            $scope.customer_gid = "";
            $scope.cbocustomer2sanction_gid = "";
            $scope.customer = "";
            $scope.customer2sanction_gid = "";
            activate();
        }
        //search for customer
        $scope.search = function () {
            var customer2sanction_gid;
            var params = {
                customer_gid: $scope.customer_gid,
                customer2sanction_gid: $scope.cbocustomer2sanction_gid.customer2sanction_gid
                 }

            var url = 'api/idasTrnLsaReport/lsafilter';
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                $scope.idasTrnLsaReport = resp.data.idasTrnLsaReportSummaryList;
                unlockUI();
            });
        }
    }
})();