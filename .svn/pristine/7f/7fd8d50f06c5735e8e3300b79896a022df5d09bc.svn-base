(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AgrMstSuprCadDeferralApprovalSummary', AgrMstSuprCadDeferralApprovalSummary);

        AgrMstSuprCadDeferralApprovalSummary.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams'];

    function AgrMstSuprCadDeferralApprovalSummary($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AgrMstSuprCadDeferralApprovalSummary';

        activate();

        function activate() {
            lockUI();
            var url = 'api/AgrMstSuprScannedDocument/GetCADScannedDocApproverSummary';
            SocketService.get(url).then(function (resp) {
                $scope.scannedapproverpendinglist = resp.data.scannedmakerapplication;
            });
             
            var url = 'api/AgrMstSuprScannedDocument/CADAppScannedDocCount';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.overallCountinfo = resp.data;
            });
        }

        $scope.approvalPendingSummary = function () {
            lockUI();
            var url = 'api/AgrMstSuprScannedDocument/GetCADScannedDocApproverSummary';
            SocketService.get(url).then(function (resp) {
                $scope.scannedapproverpendinglist = resp.data.scannedmakerapplication;
            });

            var url = 'api/AgrMstSuprScannedDocument/CADAppScannedDocCount';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.overallCountinfo = resp.data;
            });
        }

        $scope.approvalFollowupSummary = function () {
            lockUI();
            var url = 'api/AgrMstSuprScannedDocument/GetCADScannedDocFollowupApproverSummary';
            SocketService.get(url).then(function (resp) {
                $scope.scannedapproverFollowuplist = resp.data.scannedmakerapplication;
            });
            var url = 'api/AgrMstSuprScannedDocument/CADAppScannedDocCount';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.overallCountinfo = resp.data;
            });
        }

        $scope.maker = function () {
            $location.url('app/AgrMstSuprCadDeferralSummary');
        }

        $scope.checker = function () {
            $location.url('app/AgrMstSuprCadDeferralCheckerSummary');
        }

        $scope.approval = function () {
            $location.url('app/AgrMstSuprCadDeferralApprovalSummary');
        }

        $scope.view = function (val) {
            $location.url('app/AgrMstSuprCadApplicationView?application_gid=' + val + '&lspage=CadScannedDocApproval');
        }

        $scope.Completed = function () {
            $location.url('app/AgrMstSuprScannedCompletedSummary');
        }

        $scope.approver_process = function (val, val1, processtypeassign_gid) {
            if (processtypeassign_gid != undefined)
                $location.url('app/AgrTrnSuprCadDeferralGuarantorDtls?application_gid=' + val + '&lsemployee_gid=' + val1 + '&lsprocesstypeassign_gid=' + processtypeassign_gid + '&lspage=CadDeferralApproval&lspath=Approver');
            else
                $location.url('app/AgrTrnSuprCadDeferralGuarantorDtls?application_gid=' + val + '&lsemployee_gid=' + val1 + '&lspage=CadDeferralApproval');
        }
    }
})();