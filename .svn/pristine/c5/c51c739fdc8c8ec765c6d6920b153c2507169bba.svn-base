(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AgrMstCadChequeManagementSummary', AgrMstCadChequeManagementSummary);

    AgrMstCadChequeManagementSummary.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams'];

    function AgrMstCadChequeManagementSummary($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AgrMstCadChequeManagementSummary';
        $scope.lspage = $location.search().lspage;
        var lspage = $scope.lspage;
        activate();

        function activate() {
            lockUI();
            var url = 'api/AgrUdcManagement/GetChequeMakerSummary';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.chequemaker_list = resp.data.cadapplicationlist;
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

        $scope.makerPendingSUmmary = function () {
            var url = 'api/AgrUdcManagement/GetChequeMakerSummary';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.chequemaker_list = resp.data.cadapplicationlist;
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

        $scope.makerfollowupSUmmary = function () {
            var url = 'api/AgrUdcManagement/GetChequeFollowupMakerSummary';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.chequefollowup_list = resp.data.cadapplicationlist;
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

        $scope.maker_process = function (application_gid, created_by) {
            $location.url('app/AgrMstUDCMakerSummary?application_gid=' + application_gid + '&lsmakersummary_flag=N');
        }

        $scope.maker_followupprocess = function (application_gid, created_by) {
            $location.url('app/AgrMstChequeMakerFollowDtls?application_gid=' + application_gid + '&lspage=makerfollowup');
        }

        $scope.approvalcompleted = function () {
            $location.url('app/AgrMstChequeApprovalCompleted');
        }

        $scope.view = function (val) {
            $location.url('app/AgrMstCadApplicationView?application_gid=' + val + '&lspage=CadchequeManagementMaker');
        }
    }
})();