﻿(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AgrMstCadChequeMgmtApprovalSummary', AgrMstCadChequeMgmtApprovalSummary);

    AgrMstCadChequeMgmtApprovalSummary.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams'];

    function AgrMstCadChequeMgmtApprovalSummary($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AgrMstCadChequeMgmtApprovalSummary';
        $scope.lspage = $location.search().lspage;
        var lspage = $scope.lspage;
        activate();

        function activate() {
            var url = 'api/AgrUdcManagement/GetChequeApproverSummary';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.approver_list = resp.data.cadapplicationlist;
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

        $scope.approvalcompleted = function () {
            $location.url('app/AgrMstChequeApprovalCompleted');
        }

        $scope.approval_process = function (application_gid, created_by, lsmakersummary_flag) {
            $location.url('app/AgrMstChequeApprovalDtls?application_gid=' + application_gid + '&lsmakersummary_flag=N');
        }

        $scope.view = function (val) {
            $location.url('app/AgrMstCadApplicationView?application_gid=' + val + '&lspage=CadchequeManagementApprover');
        }
    }
})();