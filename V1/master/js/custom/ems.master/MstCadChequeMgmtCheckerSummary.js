﻿(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstCadChequeMgmtCheckerSummary', MstCadChequeMgmtCheckerSummary);

    MstCadChequeMgmtCheckerSummary.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams'];

    function MstCadChequeMgmtCheckerSummary($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstCadChequeMgmtCheckerSummary';
        $scope.lspage = $location.search().lspage;
        var lspage = $scope.lspage;
        activate();

        function activate() {
            var url = 'api/UdcManagement/GetChequeCheckerSummary';
            lockUI();
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.chequecheckerpending_list = resp.data.cadapplicationlist;
            });
            var url = 'api/UdcManagement/CADChequeSummaryCount';
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
            $location.url('app/MstCadChequeManagementSummary');
        }

        $scope.checker = function () {
            $location.url('app/MstCadChequeMgmtCheckerSummary');
        }

        $scope.approval = function () {
            $location.url('app/MstCadChequeMgmtApprovalSummary');
        }

        $scope.CheckerPendingSummary = function () {
            var url = 'api/UdcManagement/GetChequeCheckerSummary';
            lockUI();
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.chequecheckerpending_list = resp.data.cadapplicationlist;
            });
            var url = 'api/UdcManagement/CADChequeSummaryCount';
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

        $scope.CheckerfollowupSUmmary = function () {
            var url = 'api/UdcManagement/GetChequeFollowupCheckerSummary';
            lockUI();
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.chequecheckerfollowup_list = resp.data.cadapplicationlist;
            });
            var url = 'api/UdcManagement/CADChequeSummaryCount';
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

        $scope.approvalcompleted = function () {
            $location.url('app/MstChequeApprovalCompleted');
        }

        $scope.checker_pendingprocess = function (application_gid, created_by) {
            $location.url('app/MstChequeCheckerDtls?application_gid=' + application_gid);
        }

        $scope.checker_followupprocess = function (application_gid, created_by) {
            $location.url('app/MstChequeMakerFollowDtls?application_gid=' + application_gid + '&lspage=checkerfollowup');
        }

        $scope.view = function (val) {
            $location.url('app/MstCadApplicationView?application_gid=' + val + '&lspage=CadchequeManagementChecker');
        }

    }
})();