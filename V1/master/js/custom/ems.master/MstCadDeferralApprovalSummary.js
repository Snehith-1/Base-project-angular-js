(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstCadDeferralApprovalSummary', MstCadDeferralApprovalSummary);

    MstCadDeferralApprovalSummary.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams'];

    function MstCadDeferralApprovalSummary($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstCadDeferralApprovalSummary';

        activate();

        function activate() {
            lockUI();
            var url = 'api/MstScannedDocument/GetCADScannedDocApproverSummary';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.scannedapproverpendinglist = resp.data.scannedmakerapplication;
            });
             
            var url = 'api/MstScannedDocument/CADAppScannedDocCount';
            lockUI();
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.overallCountinfo = resp.data;
            });
        }

        $scope.approvalPendingSummary = function () {
            lockUI();
            var url = 'api/MstScannedDocument/GetCADScannedDocApproverSummary';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.scannedapproverpendinglist = resp.data.scannedmakerapplication;
            });

            var url = 'api/MstScannedDocument/CADAppScannedDocCount';
            lockUI();
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.overallCountinfo = resp.data;
            });
        }

        $scope.approvalFollowupSummary = function () {
            lockUI();
            var url = 'api/MstScannedDocument/GetCADScannedDocFollowupApproverSummary';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.scannedapproverFollowuplist = resp.data.scannedmakerapplication;
            });
            var url = 'api/MstScannedDocument/CADAppScannedDocCount';
            lockUI();
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.overallCountinfo = resp.data;
            });
        }

        $scope.maker = function () {
            $location.url('app/MstCadDeferralSummary');
        }

        $scope.checker = function () {
            $location.url('app/MstCadDeferralCheckerSummary');
        }

        $scope.approval = function () {
            $location.url('app/MstCadDeferralApprovalSummary');
        }

        $scope.view = function (val) {
            $location.url('app/MstCadApplicationView?application_gid=' + val + '&lspage=CadScannedDocApproval');
        }

        $scope.Completed = function () {
            $location.url('app/MstScannedCompletedSummary');
        }

        $scope.approver_process = function (val, val1, processtypeassign_gid) {
            if (processtypeassign_gid != undefined)
                $location.url('app/MstCadDeferralGuarantorDtls?application_gid=' + val + '&lsemployee_gid=' + val1 + '&lsprocesstypeassign_gid=' + processtypeassign_gid + '&lspage=CadDeferralApproval&lspath=Approver');
            else
                $location.url('app/MstCadDeferralGuarantorDtls?application_gid=' + val + '&lsemployee_gid=' + val1 + '&lspage=CadDeferralApproval');
        }
    }
})();