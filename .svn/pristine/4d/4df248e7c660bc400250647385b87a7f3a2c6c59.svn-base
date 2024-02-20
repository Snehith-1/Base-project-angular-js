(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AtmRptAuditVisitReportManagementApprovedSummaryController', AtmRptAuditVisitReportManagementApprovedSummaryController);

    AtmRptAuditVisitReportManagementApprovedSummaryController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', '$modal', 'DownloaddocumentService', 'cmnfunctionService'];

    function AtmRptAuditVisitReportManagementApprovedSummaryController($rootScope, $scope, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, $modal, DownloaddocumentService, cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AtmRptAuditVisitReportManagementApprovedSummaryController';

        activate();

        function activate() {


            var url = 'api/AtmRptAuditReports/GetAuditVisitReportManagementApprovedSummary';
            SocketService.get(url).then(function (resp) {
                $scope.VisitReportList = resp.data.VisitReportList;

            });

            var url = 'api/AtmRptAuditReports/GetAuditVisitReportCounts';
            SocketService.get(url).then(function (resp) {
                unlockUI()
                $scope.pending = resp.data.pending;
                $scope.approved = resp.data.approved;
                $scope.approval_pending = resp.data.approval_pending;
                $scope.approval_approved = resp.data.approval_approved;
                $scope.management_pending = resp.data.management_pending;
                $scope.management_approved = resp.data.management_approved;

            });

        }
        $scope.AuditVisitApprovedReport = function () {
            lockUI();
            var url = 'api/AtmRptAuditReports/ExportAuditVisitApprovedReport';
            SocketService.get(url).then(function (resp) {
                if (resp.data.status == true) {
                    unlockUI();
                    DownloaddocumentService.Downloaddocument(resp.data.lscloudpath, resp.data.lsname);

                    //var phyPath = resp.data.lspath;
                    //var relPath = phyPath.split("EMS");
                    //var relpath1 = relPath[1].replace("\\", "/");
                    //var hosts = window.location.host;
                    //var prefix = location.protocol + "//";
                    //var str = prefix.concat(hosts, relpath1);
                    //var link = document.createElement("a");
                    //var name = resp.data.lsname.split('.');
                    //link.download = name[0];
                    //var uri = str;
                    //link.href = uri;
                    //link.click();
                }
                else {
                    unlockUI();
                    Notify.alert('Error Occurred While Exporting !', 'warning')

                }

            });
        }
        $scope.approvalpending = function () {
            $state.go('app.AtmRptAuditVisitReportManagementPendingSummary');
        }

        $scope.approvedapproved = function () {
            $state.go('app.AtmRptAuditVisitReportManagementApprovedSummary');
        }

        //$scope.editauditvisitreport = function (val) {

        //    $location.url('app/AtmRptAuditVisitReportEdit?auditvisit_gid=' + val);

        //}

        $scope.viewauditvisitreport = function (val) {
            $location.url('app/AtmRptAuditVisitReportManagementApprovedView?hash=' + cmnfunctionService.encryptURL('auditvisit_gid=' + val));


        }



    }
})();
