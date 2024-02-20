(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AgrMstCadDeferralApprovalSummary', AgrMstCadDeferralApprovalSummary);

        AgrMstCadDeferralApprovalSummary.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams'];

    function AgrMstCadDeferralApprovalSummary($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AgrMstCadDeferralApprovalSummary';

        activate();

        function activate() {
            lockUI();
            var url = 'api/AgrMstScannedDocument/GetCADScannedDocApproverSummary';
            SocketService.get(url).then(function (resp) {
                $scope.scannedapproverpendinglist = resp.data.scannedmakerapplication;
            });
             
            var url = 'api/AgrMstScannedDocument/CADAppScannedDocCount';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.overallCountinfo = resp.data;
            });
        }

        $scope.approvalPendingSummary = function () {
            lockUI();
            var url = 'api/AgrMstScannedDocument/GetCADScannedDocApproverSummary';
            SocketService.get(url).then(function (resp) {
                $scope.scannedapproverpendinglist = resp.data.scannedmakerapplication;
            });

            var url = 'api/AgrMstScannedDocument/CADAppScannedDocCount';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.overallCountinfo = resp.data;
            });
        }

        $scope.approvalFollowupSummary = function () {
            lockUI();
            var url = 'api/AgrMstScannedDocument/GetCADScannedDocFollowupApproverSummary';
            SocketService.get(url).then(function (resp) {
                $scope.scannedapproverFollowuplist = resp.data.scannedmakerapplication;
            });
            var url = 'api/AgrMstScannedDocument/CADAppScannedDocCount';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.overallCountinfo = resp.data;
            });
        }

        $scope.maker = function () {
            $location.url('app/AgrMstCadDeferralSummary');
        }

        $scope.checker = function () {
            $location.url('app/AgrMstCadDeferralCheckerSummary');
        }

        $scope.approval = function () {
            $location.url('app/AgrMstCadDeferralApprovalSummary');
        }

        $scope.view = function (val) {
            $location.url('app/AgrMstCadApplicationView?application_gid=' + val + '&lspage=CadScannedDocApproval');
        }

        $scope.Completed = function () {
            $location.url('app/AgrMstScannedCompletedSummary');
        }

        $scope.approver_process = function (val, val1, processtypeassign_gid) {
            if (processtypeassign_gid != undefined)
                $location.url('app/AgrTrnCadDeferralGuarantorDtls?application_gid=' + val + '&lsemployee_gid=' + val1 + '&lsprocesstypeassign_gid=' + processtypeassign_gid + '&lspage=CadDeferralApproval&lspath=Approver');
            else
                $location.url('app/AgrTrnCadDeferralGuarantorDtls?application_gid=' + val + '&lsemployee_gid=' + val1 + '&lspage=CadDeferralApproval');
        }
    }
})();