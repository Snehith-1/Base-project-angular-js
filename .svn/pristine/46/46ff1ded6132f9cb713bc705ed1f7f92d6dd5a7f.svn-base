(function () {
    'use strict';

    angular
        .module('angle')
        .controller('RptEmployeeLoanReportController', RptEmployeeLoanReportController);

    RptEmployeeLoanReportController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams', 'DownloaddocumentService'];

    function RptEmployeeLoanReportController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams, DownloaddocumentService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'RptEmployeeLoanReportController';
        activate();
        function activate() {
            var url = 'api/RptEmployeeLoanReport/GetEmployeeLoanReportSummary';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.Report_list = resp.data.SummaryReport_list;
                unlockUI();
            });
        }

        var url = 'api/RptEmployeeLoanReport/GetReportCounts';
        SocketService.get(url).then(function (resp) {
            $scope.totalcount = resp.data.total_count;
            $scope.pendingcount = resp.data.pending_count;
            $scope.rejectedcount = resp.data.rejected_count;
            $scope.completedcount = resp.data.completed_count; 
            $scope.WithdrawnCount = resp.data.WithdrawnCount;            
        });       
        $scope.Loanreport = function () {
            lockUI();
            var url = 'api/RptEmployeeLoanReport/ExportLoanReport';
            SocketService.get(url).then(function (resp) {
                if (resp.data.status == true) {
                    unlockUI();
                    DownloaddocumentService.Downloaddocument(resp.data.lscloudpath, resp.data.lsname);
                 
                }
                else {
                    unlockUI();
                    Notify.alert('Error Occurred While Export !', 'success')

                }

            });
        }
    }
})();