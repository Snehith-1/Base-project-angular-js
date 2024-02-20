(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AgrMstContractCheckerSummaryController', AgrMstContractCheckerSummaryController);

    AgrMstContractCheckerSummaryController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function AgrMstContractCheckerSummaryController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AgrMstContractCheckerSummaryController';

        activate();
        
        function activate() {
            lockUI();
            var url = 'api/AgrTrnContract/ContractToCheckerSummary';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.sanctiontocheckerlist = resp.data.sanctiondetails;
            });
            var url = 'api/AgrTrnContract/CADContractSummaryCount';
            lockUI();
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.MakerPendingCount = resp.data.MakerPendingCount;
                $scope.MakerFollowUpCount = resp.data.MakerFollowUpCount;
                $scope.CheckerPendingCount = resp.data.CheckerPendingCount;
                $scope.CheckerFollowUpCount = resp.data.CheckerFollowUpCount;
                $scope.ApproverPendingCount = resp.data.ApproverPendingCount;
                $scope.CompletedCount = resp.data.CompletedCount;
                $scope.AcceptedCount = resp.data.AcceptedCount;
                $scope.RejectedCount =  resp.data.RejectedCount;

            });
        }

        $scope.checkerPendingSummary = function () {
            lockUI();
            var url = 'api/AgrTrnContract/ContractToCheckerSummary';
            SocketService.get(url).then(function (resp) {
                $scope.sanctiontocheckerlist = resp.data.sanctiondetails;
            });
            var url = 'api/AgrTrnContract/CADContractSummaryCount';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.MakerPendingCount = resp.data.MakerPendingCount;
                $scope.MakerFollowUpCount = resp.data.MakerFollowUpCount;
                $scope.CheckerPendingCount = resp.data.CheckerPendingCount;
                $scope.CheckerFollowUpCount = resp.data.CheckerFollowUpCount;
                $scope.ApproverPendingCount = resp.data.ApproverPendingCount;
                $scope.CompletedCount = resp.data.CompletedCount;
                $scope.RejectedCount =  resp.data.RejectedCount;
            });
        }

        $scope.checkerfollowupSummary = function () {
            lockUI();
            var url = 'api/AgrTrnContract/ContractToCheckerFollowupSummary';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.sanctiontocheckerlist = resp.data.sanctiondetails;
            });
            var url = 'api/AgrTrnContract/CADContractSummaryCount';
            lockUI();
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.MakerPendingCount = resp.data.MakerPendingCount;
                $scope.MakerFollowUpCount = resp.data.MakerFollowUpCount;
                $scope.CheckerPendingCount = resp.data.CheckerPendingCount;
                $scope.CheckerFollowUpCount = resp.data.CheckerFollowUpCount;
                $scope.ApproverPendingCount = resp.data.ApproverPendingCount;
                $scope.CompletedCount = resp.data.CompletedCount;
                $scope.AcceptedCount = resp.data.AcceptedCount;
            });
        }

         $scope.maker = function () {
            $location.url('app/AgrMstContractSummary');
        }

        $scope.checker = function () {
            $location.url('app/AgrMstContractCheckerSummary');
        }

        $scope.approval = function () {
            $location.url('app/AgrMstContractApprovalSummary');
        }

        $scope.approvalcompleted = function () {
            $location.url('app/AgrMstContractApprovalCompleted');
        }
        $scope.accepted = function () {
            $location.url('app/AgrMstContractAccepted');
        }
        $scope.Rejected = function () {
            $location.url('app/AgrMstContractRejectedSummary');
        }


        $scope.view = function (val) {
            $location.url('app/AgrMstCadApplicationView?application_gid=' + val +  '&lspage=ContractCheckerView');
        }

        //$scope.checker_process = function (val) {
        //    $location.url('app/MstSanctionDtlSummary?application_gid=' + val + '&lspage=SanctionChecker');
        //}
        $scope.sanctiontocheckerview = function (application2sanction_gid,application_gid, followuppage) {
            var page = 'checkersummary';
            if (followuppage == 'Y')
                page = 'checkerfollowupsummary';
            $location.url('app/AgrMstContractDtlViewSummary?sanction_gid=' + application2sanction_gid + '&application_gid=' + application_gid + '&lspage=' + page);
        }

        //Checker Process
        $scope.checker_process = function (val, val1) {
            // $location.url('app/AgrMstContractDtlSummary');
            $location.url('app/AgrMstContractDtlSummary?application_gid=' + val + '&lsemployee_gid=' + val1 + '&lspage=ContractChecker');
        }
    }
})();
