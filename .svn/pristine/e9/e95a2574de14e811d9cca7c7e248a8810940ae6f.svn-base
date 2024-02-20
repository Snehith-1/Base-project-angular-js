(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstCadLSASummaryController', MstCadLSASummaryController);

    MstCadLSASummaryController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams'];

    function MstCadLSASummaryController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstCadLSASummaryController';

        activate();

        function activate() {
            lockUI();
            var url = 'api/MstLSA/GetLSAMakerSummary';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.lsamaker_list = resp.data.MdlLSAMakerSummary;
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

        $scope.makerPendingSUmmary = function () {
            lockUI();
            var url = 'api/MstLSA/GetLSAMakerSummary';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.lsamaker_list = resp.data.MdlLSAMakerSummary;
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

        $scope.makerfollowupSUmmary = function () {
            lockUI();
            var url = 'api/MstLSA/GetLSAFollowupMakerSummary';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.lsamaker_list = resp.data.MdlLSAMakerSummary;
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
            $location.url('app/MstCadApplicationView?application_gid=' + val + '&lspage=CadLsaMaker');
        }
        $scope.maker_process = function (val, val1, followup) {
                $location.url('app/MstCadLSADtlSummary?application_gid=' + val + '&application2sanction_gid=' + val1 + '&lspage=CadLsaMaker&lsfollowup=' + followup);
        }
    }
})();