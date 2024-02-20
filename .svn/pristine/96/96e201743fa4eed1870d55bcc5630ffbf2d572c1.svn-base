(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstChequeApprovalCompletedController', MstChequeApprovalCompletedController);

    MstChequeApprovalCompletedController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams'];

    function MstChequeApprovalCompletedController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstChequeApprovalCompletedController';
        $scope.lspage = $location.search().lspage;
        var lspage = $scope.lspage;

        activate();

        function activate() {
            var url = 'api/UdcManagement/GetChequeCompletedApprover';
            lockUI();
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.approvercompleted_list = resp.data.cadapplicationlist;
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

        $scope.approvalcompleted = function () {
            $location.url('app/MstChequeApprovalCompleted');
        }

        $scope.approved_process = function (application_gid, created_by) {
            $location.url('app/MstChequeMakerFollowDtls?application_gid=' + application_gid + '&lspage=CompletedApprocal');
        }

        $scope.view = function (val) {
            $location.url('app/MstCadApplicationView?application_gid=' + val + '&lspage=CadchequeManagementApprovalCompleted');
        }
    }
})();
