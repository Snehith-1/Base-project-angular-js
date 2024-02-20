(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstCadPhysicalDocApprovalSummaryController', MstCadPhysicalDocApprovalSummaryController);

    MstCadPhysicalDocApprovalSummaryController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams'];

    function MstCadPhysicalDocApprovalSummaryController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstCadPhysicalDocApprovalSummaryController';

        activate();

        function activate() {
            lockUI();
            var url = 'api/MstPhysicalDocument/GetCADPhysicalDocApproverSummary';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.scannedapproverpendinglist = resp.data.physicalmakerapplication;
            });

            var url = 'api/MstPhysicalDocument/CADAppPhysicalDocCount';
            lockUI();
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.overallCountinfo = resp.data;
            });
        }

        $scope.approvalPendingSummary = function () {
            lockUI();
            var url = 'api/MstPhysicalDocument/GetCADPhysicalDocApproverSummary';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.scannedapproverpendinglist = resp.data.physicalmakerapplication;
            });

            var url = 'api/MstPhysicalDocument/CADAppPhysicalDocCount';
            lockUI();
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.overallCountinfo = resp.data;
            });
        }

        $scope.approvalFollowupSummary = function () {
            lockUI();
            var url = 'api/MstPhysicalDocument/GetCADPhysicalDocFollowupApproverSummary';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.scannedapproverFollowuplist = resp.data.physicalmakerapplication;
            });
            var url = 'api/MstPhysicalDocument/CADAppPhysicalDocCount';
            lockUI();
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.overallCountinfo = resp.data;
            });
        }

        $scope.maker = function () {
            $location.url('app/MstCadPhysicalDocSummary');
        }

        $scope.checker = function () {
            $location.url('app/MstCadPhysicalDocCheckerSummary');
        }

        $scope.approval = function () {
            $location.url('app/MstCadPhysicalDocApprovalSummary');
        }

        $scope.view = function (val) {
            $location.url('app/MstCadApplicationView?application_gid=' + val + '&lspage=CadPhysicalDocApproval');
        }

        $scope.Completed = function () {
            $location.url('app/MstCadPhysicalDocCompletedSummary');
        }

        $scope.approver_process = function (val, val1, processtypeassign_gid) {
            if (processtypeassign_gid != undefined)
                $location.url('app/MstCadPhysicalDocGuarantorDtls?application_gid=' + val + '&lsemployee_gid=' + val1 + '&lsprocesstypeassign_gid=' + processtypeassign_gid + '&lspage=CadPhysicalDocApproval&lspath=Approver');
            else
                $location.url('app/MstCadPhysicalDocGuarantorDtls?application_gid=' + val + '&lsemployee_gid=' + val1 + '&lspage=CadPhysicalDocApproval');
        }
    }
})();
