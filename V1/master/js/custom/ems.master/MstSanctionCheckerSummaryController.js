(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstSanctionCheckerSummaryController', MstSanctionCheckerSummaryController);

    MstSanctionCheckerSummaryController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function MstSanctionCheckerSummaryController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstSanctionCheckerSummaryController';

        activate();
        
        function activate() {
            lockUI();
            var url = 'api/MstCAD/SanctionToCheckerSummary';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.sanctiontocheckerlist = resp.data.sanctiondetails;
            });
            var url = 'api/MstCAD/CADSanctionSummaryCount';
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

        $scope.checkerPendingSummary = function () {
            lockUI();
            var url = 'api/MstCAD/SanctionToCheckerSummary';
            SocketService.get(url).then(function (resp) {
                $scope.sanctiontocheckerlist = resp.data.sanctiondetails;
            });
            var url = 'api/MstCAD/CADSanctionSummaryCount';
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

        $scope.checkerfollowupSummary = function () {
            lockUI();
            var url = 'api/MstCAD/SanctionToCheckerFollowupSummary';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.sanctiontocheckerlist = resp.data.sanctiondetails;
            });
            var url = 'api/MstCAD/CADSanctionSummaryCount';
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
            $location.url('app/MstSanctionSummary');
        }

        $scope.checker = function () {
            $location.url('app/MstSanctionCheckerSummary');
        }

        $scope.approval = function () {
            $location.url('app/MstSanctionApprovalSummary');
        }

        $scope.approvalcompleted = function () {
            $location.url('app/MstSanctionApprovalCompleted');
        }

        $scope.accepted = function () {
            $location.url('app/MstSanctionAccepted');
        }

        $scope.view = function (val) {
            $location.url('app/MstCadApplicationView?application_gid=' + val +  '&lspage=SanctionChecker');
        }

        $scope.checker_process = function (val) {
            $location.url('app/MstSanctionDtlSummary?application_gid=' + val + '&lspage=SanctionChecker');
        }
        $scope.sanctiontocheckerview = function (application2sanction_gid,application_gid, followuppage) {
            var page = 'checkersummary';
            if (followuppage == 'Y')
                page = 'checkerfollowupsummary';
            $location.url('app/MstSanctionDtlViewSummary?sanction_gid=' + application2sanction_gid + '&application_gid=' + application_gid + '&lspage=' + page);
        }
    }
})();
