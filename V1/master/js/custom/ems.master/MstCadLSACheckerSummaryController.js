(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstCadLSACheckerSummaryController', MstCadLSACheckerSummaryController);

    MstCadLSACheckerSummaryController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function MstCadLSACheckerSummaryController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstCadLSACheckerSummaryController';

        activate();
        
        function activate() {
            lockUI();
            var url = 'api/MstLSA/GetLSAPendingCheckerSummary';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.LSACheckerSummary = resp.data.MdlLSACheckerSummary;
            });
            var url = 'api/MstLSA/CADLSASummaryCount';
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

        $scope.checkerPendingSummary = function () {
            lockUI();
            var url = 'api/MstLSA/GetLSAPendingCheckerSummary';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.LSACheckerSummary = resp.data.MdlLSACheckerSummary;
            });
            var url = 'api/MstLSA/CADLSASummaryCount';
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

        $scope.checkerfollowupSummary = function () {
            lockUI();
            var url = 'api/MstLSA/GetLSAFollowupCheckerSummary';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.LSACheckerSummary = resp.data.MdlLSACheckerSummary;
            });
            var url = 'api/MstLSA/CADLSASummaryCount';
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
            $location.url('app/MstCadLSASummary');
        }

        $scope.checker = function () {
            $location.url('app/MstCadLSACheckerSummary');
        }

        $scope.approval = function () {
            $location.url('app/MstCadLSAApprovalSummary');
        }


        $scope.approvalcompleted = function () {
            $location.url('app/MstLSAApprovalCompleted');
        }

        $scope.view = function (val) {
            $location.url('app/MstCadApplicationView?application_gid=' + val + '&lspage=CadLsaChecker');
        }

         $scope.maker_process = function (val, val1, followup) {
                $location.url('app/MstCadLSADtlSummary?application_gid=' + val + '&application2sanction_gid=' + val1 + '&lspage=CadLsaChecker&lsfollowup=' + followup);
        }
    }
})();