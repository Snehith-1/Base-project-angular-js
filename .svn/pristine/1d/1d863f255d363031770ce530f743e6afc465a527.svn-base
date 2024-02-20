(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstCadChequeMgmtApprovalSummary', MstCadChequeMgmtApprovalSummary);

    MstCadChequeMgmtApprovalSummary.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams'];

    function MstCadChequeMgmtApprovalSummary($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstCadChequeMgmtApprovalSummary';
        $scope.lspage = $location.search().lspage;
        var lspage = $scope.lspage;
        activate();

        function activate() {
            var url = 'api/UdcManagement/GetChequeApproverSummary';
            lockUI();
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.approver_list = resp.data.cadapplicationlist;
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

        $scope.approval_process = function (application_gid, created_by, lsmakersummary_flag) {
            $location.url('app/MstChequeApprovalDtls?application_gid=' + application_gid + '&lsmakersummary_flag=N');
        }

        $scope.view = function (val) {
            $location.url('app/MstCadApplicationView?application_gid=' + val + '&lspage=CadchequeManagementApprover');
        }
    }
})();