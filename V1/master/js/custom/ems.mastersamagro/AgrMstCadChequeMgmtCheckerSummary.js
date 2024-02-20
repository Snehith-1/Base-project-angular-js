(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AgrMstCadChequeMgmtCheckerSummary', AgrMstCadChequeMgmtCheckerSummary);

    AgrMstCadChequeMgmtCheckerSummary.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams'];

    function AgrMstCadChequeMgmtCheckerSummary($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AgrMstCadChequeMgmtCheckerSummary';
        $scope.lspage = $location.search().lspage;
        var lspage = $scope.lspage;
        activate();

        function activate() {
            var url = 'api/AgrUdcManagement/GetChequeCheckerSummary';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.chequecheckerpending_list = resp.data.cadapplicationlist;
            });
            var url = 'api/AgrUdcManagement/CADChequeSummaryCount';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.MakerPendingCount = resp.data.MakerPendingCount;
                $scope.MakerFollowUpCount = resp.data.MakerFollowUpCount;
                $scope.CheckerPendingCount = resp.data.CheckerPendingCount;
                $scope.CheckerFollowUpCount = resp.data.CheckerFollowUpCount;
                $scope.ApproverPendingCount = resp.data.ApproverPendingCount;
                $scope.CompletedCount = resp.data.CompletedCount;
            });
        }

        $scope.maker = function () {
            $location.url('app/AgrMstCadChequeManagementSummary');
        }

        $scope.checker = function () {
            $location.url('app/AgrMstCadChequeMgmtCheckerSummary');
        }

        $scope.approval = function () {
            $location.url('app/AgrMstCadChequeMgmtApprovalSummary');
        }

        $scope.CheckerPendingSummary = function () {
            var url = 'api/AgrUdcManagement/GetChequeCheckerSummary';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.chequecheckerpending_list = resp.data.cadapplicationlist;
            });
            var url = 'api/AgrUdcManagement/CADChequeSummaryCount';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.MakerPendingCount = resp.data.MakerPendingCount;
                $scope.MakerFollowUpCount = resp.data.MakerFollowUpCount;
                $scope.CheckerPendingCount = resp.data.CheckerPendingCount;
                $scope.CheckerFollowUpCount = resp.data.CheckerFollowUpCount;
                $scope.ApproverPendingCount = resp.data.ApproverPendingCount;
                $scope.CompletedCount = resp.data.CompletedCount;
            });
        }

        $scope.CheckerfollowupSUmmary = function () {
            var url = 'api/AgrUdcManagement/GetChequeFollowupCheckerSummary';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.chequecheckerfollowup_list = resp.data.cadapplicationlist;
            });
            var url = 'api/AgrUdcManagement/CADChequeSummaryCount';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.MakerPendingCount = resp.data.MakerPendingCount;
                $scope.MakerFollowUpCount = resp.data.MakerFollowUpCount;
                $scope.CheckerPendingCount = resp.data.CheckerPendingCount;
                $scope.CheckerFollowUpCount = resp.data.CheckerFollowUpCount;
                $scope.ApproverPendingCount = resp.data.ApproverPendingCount;
                $scope.CompletedCount = resp.data.CompletedCount;
            });
        }

        $scope.approvalcompleted = function () {
            $location.url('app/AgrMstChequeApprovalCompleted');
        }

        $scope.checker_pendingprocess = function (application_gid, created_by) {
            $location.url('app/AgrMstChequeCheckerDtls?application_gid=' + application_gid);
        }

        $scope.checker_followupprocess = function (application_gid, created_by) {
            $location.url('app/AgrMstChequeMakerFollowDtls?application_gid=' + application_gid + '&lspage=checkerfollowup');
        }

        $scope.view = function (val) {
            $location.url('app/AgrMstCadApplicationView?application_gid=' + val + '&lspage=CadchequeManagementChecker');
        }

    }
})();