(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AgrMstSupplierCCReportController', AgrMstSupplierCCReportController);

        AgrMstSupplierCCReportController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams','DownloaddocumentService'];

    function AgrMstSupplierCCReportController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams,DownloaddocumentService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AgrMstSupplierCCReportController';
        activate();
        function activate() {
            var url = 'api/AgrMstApplicationReport/MstSupplierCCSummary';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.MstCCReport_list = resp.data.MstCCSummaryList;
                unlockUI();
            });
        }

        //var url = 'api/MstApplicationReport/ApplicationCounts';
        //SocketService.get(url).then(function (resp) {
        //    $scope.count_Report = resp.data.count_Report;
        //    $scope.count_submit = resp.data.count_submit;
        //    $scope.count_incomplete = resp.data.count_incomplete;

        //});

        $scope.ccreport = function () {
            lockUI();
            var url = 'api/AgrMstApplicationReport/ExportMstSupplierCCReport';
            SocketService.get(url).then(function (resp) {
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