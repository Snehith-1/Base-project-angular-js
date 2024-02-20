(function () {
    'use strict';

    angular
        .module('angle')
        .controller('idasTrnCourierReportController', idasTrnCourierReportController);

    idasTrnCourierReportController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams', 'DownloaddocumentService'];

    function idasTrnCourierReportController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams, DownloaddocumentService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'idasTrnCourierReportController';
        activate();
        function activate() {
 
        var url = 'api/CourierReport/CourierReportSummary';
        lockUI();
        SocketService.get(url).then(function (resp) {
            $scope.idasTrnCourierReport = resp.data.CourierReportSummaryDtls;
        unlockUI();
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
            $scope.cbocustomer_name = customer_name;
            $scope.customer_gid = customer_gid;
            $scope.customer_list = null;
        }
        $scope.all = function () {
            $scope.cbocustomer_name = "";
            $scope.cbocourier_type= "";
            activate();
        }
        $scope.search = function () {
           var params={
            courier_type : $scope.cbocourier_type,
            customer_name:$scope.cbocustomer_name
           }
        var url = 'api/CourierReport/ReportSearch';
        lockUI();
        SocketService.getparams(url,params).then(function (resp) {
            $scope.idasTrnCourierReport = resp.data.CourierReportSummaryDtls;
        unlockUI();
            });
        
        }

        $scope.exportcourierreport = function () {
            var params={
                courier_type : $scope.cbocourier_type,
                customer_name:$scope.cbocustomer_name
               }
            lockUI();
            var url = 'api/CourierReport/ExportReport';
            SocketService.getparams(url,params).then(function (resp) {
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
                    Notify.alert('Error Occurred While Export !', 'success')
                    
                }

            });
        }
    }
})();