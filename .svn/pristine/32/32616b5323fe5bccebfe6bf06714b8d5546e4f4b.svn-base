(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstCadChequeManagementSummary', MstCadChequeManagementSummary);

    MstCadChequeManagementSummary.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams'];

    function MstCadChequeManagementSummary($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstCadChequeManagementSummary';
        $scope.lspage = $location.search().lspage;
        var lspage = $scope.lspage;
        activate();

        function activate() {
            lockUI();
            var url = 'api/UdcManagement/GetChequeMakerSummary';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.chequemaker_list = resp.data.cadapplicationlist;
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

        $scope.makerPendingSUmmary = function () {
            var url = 'api/UdcManagement/GetChequeMakerSummary';
            lockUI();
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.chequemaker_list = resp.data.cadapplicationlist;
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

        $scope.makerfollowupSUmmary = function () {
            var url = 'api/UdcManagement/GetChequeFollowupMakerSummary';
            lockUI();
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.chequefollowup_list = resp.data.cadapplicationlist;
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

        $scope.maker_process = function (application_gid, created_by) {
            $location.url('app/MstUDCMakerSummary?application_gid=' + application_gid + '&lsmakersummary_flag=N');            
        }

        $scope.maker_followupprocess = function (application_gid, created_by) {
            $location.url('app/MstChequeMakerFollowDtls?application_gid=' + application_gid + '&lspage=makerfollowup');
        }

        $scope.approvalcompleted = function () {
            $location.url('app/MstChequeApprovalCompleted');
        }

        $scope.view = function (val) {
            $location.url('app/MstCadApplicationView?application_gid=' + val + '&lspage=CadchequeManagementMaker');
        }
    }
})();