(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AtmRptAuditVisitReportApprovalSummaryController', AtmRptAuditVisitReportApprovalSummaryController);

    AtmRptAuditVisitReportApprovalSummaryController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', '$modal', 'cmnfunctionService'];

    function AtmRptAuditVisitReportApprovalSummaryController($rootScope, $scope, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, $modal, cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AtmRptAuditVisitReportApprovalSummaryController';

        activate();

        function activate() {


            var url = 'api/AtmRptAuditReports/GetAuditVisitReportApprovalPendingSummary';
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

            });
        }

        $scope.approvalpending = function () {
            $state.go('app.AtmRptAuditVisitReportApprovalSummary');
        }

        $scope.approvedapproved = function () {
            $state.go('app.AtmRptAuditVisitReportApprovalApprovedSummary');
        }

        $scope.editauditvisitreport = function (val) {

            $location.url('app/AtmRptAuditVisitReportApproval?hash=' + cmnfunctionService.encryptURL('auditvisit_gid=' + val));

        }

        $scope.viewauditvisitreport = function (val) {
            $location.url('app/AtmRptAuditVisitReportApprovalPendingView?hash=' + cmnfunctionService.encryptURL('auditvisit_gid=' + val));


        }



    }
})();