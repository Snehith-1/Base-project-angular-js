(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AgrMstSuprCadChequeManagementSummaryController', AgrMstSuprCadChequeManagementSummaryController);

    AgrMstSuprCadChequeManagementSummaryController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams'];

    function AgrMstSuprCadChequeManagementSummaryController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AgrMstSuprCadChequeManagementSummaryController';
        $scope.lspage = $location.search().lspage;
        var lspage = $scope.lspage;
        activate();

        function activate() {
            lockUI();
            var url = 'api/AgrSuprUdcManagement/GetChequeMakerSummary';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.chequemaker_list = resp.data.cadapplicationlist;
            });
            var url = 'api/AgrSuprUdcManagement/CADChequeSummaryCount';
            lockUI();
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
            var url = 'api/AgrSuprUdcManagement/GetChequeMakerSummary';
            lockUI();
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.chequemaker_list = resp.data.cadapplicationlist;
            });
            var url = 'api/AgrSuprUdcManagement/CADChequeSummaryCount';
            lockUI();
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
            var url = 'api/AgrSuprUdcManagement/GetChequeFollowupMakerSummary';
            lockUI();
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.chequefollowup_list = resp.data.cadapplicationlist;
            });
            var url = 'api/AgrSuprUdcManagement/CADChequeSummaryCount';
            lockUI();
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
            $location.url('app/AgrMstSuprCadChequeManagementSummary');
        }

        $scope.checker = function () {
            $location.url('app/AgrMstSuprCadChequeMgmtCheckerSummary');
        }

        $scope.approval = function () {
            $location.url('app/AgrMstSuprCadChequeMgmtApprovalSummary');
        }

        $scope.maker_process = function (application_gid, created_by) {
            $location.url('app/AgrMstSuprUDCMakerSummary?application_gid=' + application_gid + '&lsmakersummary_flag=N');
        }

        $scope.maker_followupprocess = function (application_gid, created_by) {
            $location.url('app/AgrMstSuprChequeMakerFollowDtls?application_gid=' + application_gid + '&lspage=makerfollowup');
        }

        $scope.approvalcompleted = function () {
            $location.url('app/AgrMstSuprChequeApprovalCompleted');
        }

        $scope.view = function (val) {
            $location.url('app/AgrMstSuprCadApplicationView?application_gid=' + val + '&lspage=CadchequeManagementMaker');
        }
    }
})();