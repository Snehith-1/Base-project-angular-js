(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AtmRptAuditVisitReportApprovedSummaryController', AtmRptAuditVisitReportApprovedSummaryController);

    AtmRptAuditVisitReportApprovedSummaryController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', '$modal', 'cmnfunctionService'];

    function AtmRptAuditVisitReportApprovedSummaryController($rootScope, $scope, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, $modal, cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AtmRptAuditVisitReportApprovedSummaryController';

        activate();

        function activate() {


            var url = 'api/AtmRptAuditReports/GetAuditVisitReportApprovedSummary';
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

        $scope.reportpending = function () {
            $state.go('app.AtmRptAuditVisitReportSummary');
        }

        $scope.reportapproved = function () {
            $state.go('app.AtmRptAuditVisitReportApprovedSummary');
        }

        $scope.editauditvisitreport = function (val) {

            $location.url('app/AtmRptAuditVisitReportEdit?hash=' + cmnfunctionService.encryptURL('auditvisit_gid=' + val));

        }

        $scope.viewauditvisitreport = function (val) {
            $location.url('app/AtmRptAuditVisitReportApprovedView?hash=' + cmnfunctionService.encryptURL('auditvisit_gid=' + val));


        }
        $scope.createauditvisit = function () {
            $state.go('app.AtmRptAuditVisitReport');
        }


    }
})();
