(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AgrMstSuprCadChequeMgmtApprovalSummaryController', AgrMstSuprCadChequeMgmtApprovalSummaryController);

    AgrMstSuprCadChequeMgmtApprovalSummaryController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams'];

    function AgrMstSuprCadChequeMgmtApprovalSummaryController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AgrMstSuprCadChequeMgmtApprovalSummaryController';
        $scope.lspage = $location.search().lspage;
        var lspage = $scope.lspage;
        activate();

        function activate() {
            var url = 'api/AgrSuprUdcManagement/GetChequeApproverSummary';
            lockUI();
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.approver_list = resp.data.cadapplicationlist;
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

        $scope.approvalcompleted = function () {
            $location.url('app/AgrMstSuprChequeApprovalCompleted');
        }

        $scope.approval_process = function (application_gid, created_by, lsmakersummary_flag) {
            $location.url('app/AgrMstSuprChequeApprovalDtls?application_gid=' + application_gid + '&lsmakersummary_flag=N');
        }

        $scope.view = function (val) {
            $location.url('app/AgrMstSuprCadApplicationView?application_gid=' + val + '&lspage=CadchequeManagementApprover');
        }
    }
})();