(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AgrMstSuprChequeApprovalCompletedController', AgrMstSuprChequeApprovalCompletedController);

    AgrMstSuprChequeApprovalCompletedController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams'];

    function AgrMstSuprChequeApprovalCompletedController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AgrMstSuprChequeApprovalCompletedController';
        $scope.lspage = $location.search().lspage;
        var lspage = $scope.lspage;

        activate();

        function activate() {
            var url = 'api/AgrSuprUdcManagement/GetChequeCompletedApprover';
            lockUI();
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.approvercompleted_list = resp.data.cadapplicationlist;
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

        $scope.approved_process = function (application_gid, created_by) {
            $location.url('app/AgrMstSuprChequeMakerFollowDtls?application_gid=' + application_gid + '&lspage=CompletedApprocal');
        }

        $scope.view = function (val) {
            $location.url('app/AgrMstSuprCadApplicationView?application_gid=' + val + '&lspage=CadchequeManagementApprovalCompleted');
        }
    }
})();
