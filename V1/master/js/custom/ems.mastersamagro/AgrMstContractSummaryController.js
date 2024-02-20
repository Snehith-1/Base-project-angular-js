(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AgrMstContractSummaryController', AgrMstContractSummaryController);

    AgrMstContractSummaryController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams'];

    function AgrMstContractSummaryController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AgrMstContractSummaryController';

        activate();
        
        function activate() {
            lockUI();
            var url = 'api/AgrTrnContract/GetContractMakerSummary';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.sanctionmaker_list = resp.data.cadapplicationlist;
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

        $scope.makerPendingSUmmary = function () {
            lockUI();
            var url = 'api/AgrTrnContract/GetContractMakerSummary';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.sanctionmaker_list = resp.data.cadapplicationlist;
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
                $scope.RejectedCount =  resp.data.RejectedCount;
            });
        }

        $scope.makerfollowupSUmmary = function () {
            lockUI();
            var url = 'api/AgrTrnContract/GetContractFollowupMakerSummary';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.sanctionmaker_list = resp.data.cadapplicationlist;
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

        //$scope.view = function (val) {
        //    $location.url('app/AgrMstCadApplicationView');
        //}

        $scope.view = function (val) {
            $location.url('app/AgrMstCadApplicationView?application_gid=' + val + '&lspage=ContractMakerSummaryView');
        }

        
        $scope.maker_process = function (val, val1) {
            // $location.url('app/AgrMstContractDtlSummary');
            $location.url('app/AgrMstContractDtlSummary?application_gid=' + val + '&lsemployee_gid=' + val1 + '&lspage=ContractMaker');
        }
        $scope.maker_processfollowup = function (val, val1) {
            // $location.url('app/AgrMstContractDtlSummary');
            $location.url('app/AgrMstContractDtlViewSummary?application_gid=' + val + '&lsemployee_gid=' + val1 + '&lspage=ContractMakerFollowup');
        }
    }
})();
