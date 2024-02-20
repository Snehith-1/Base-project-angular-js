(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AgrTrnSuprCadPhysicalDocApprovalSummaryController', AgrTrnSuprCadPhysicalDocApprovalSummaryController);

        AgrTrnSuprCadPhysicalDocApprovalSummaryController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams'];

    function AgrTrnSuprCadPhysicalDocApprovalSummaryController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AgrTrnSuprCadPhysicalDocApprovalSummaryController';

        activate();

        function activate() {
            lockUI();
            var url = 'api/AgrTrnSuprPhysicalDocument/GetCADPhysicalDocApproverSummary';
            SocketService.get(url).then(function (resp) {
                $scope.scannedapproverpendinglist = resp.data.physicalmakerapplication;
            });

            var url = 'api/AgrTrnSuprPhysicalDocument/CADAppPhysicalDocCount';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.overallCountinfo = resp.data;
            });
        }

        $scope.approvalPendingSummary = function () {
            lockUI();
            var url = 'api/AgrTrnSuprPhysicalDocument/GetCADPhysicalDocApproverSummary';
            SocketService.get(url).then(function (resp) {
                $scope.scannedapproverpendinglist = resp.data.physicalmakerapplication;
            });

            var url = 'api/AgrTrnSuprPhysicalDocument/CADAppPhysicalDocCount';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.overallCountinfo = resp.data;
            });
        }

        $scope.approvalFollowupSummary = function () {
            lockUI();
            var url = 'api/AgrTrnSuprPhysicalDocument/GetCADPhysicalDocFollowupApproverSummary';
            SocketService.get(url).then(function (resp) {
                $scope.scannedapproverFollowuplist = resp.data.physicalmakerapplication;
            });
            var url = 'api/AgrTrnSuprPhysicalDocument/CADAppPhysicalDocCount';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.overallCountinfo = resp.data;
            });
        }

        $scope.maker = function () {
            $location.url('app/AgrTrnSuprCadPhysicalDocSummary');
        }

        $scope.checker = function () {
            $location.url('app/AgrTrnSuprCadPhysicalDocCheckerSummary');
        }

        $scope.approval = function () {
            $location.url('app/AgrTrnSuprCadPhysicalDocApprovalSummary');
        }

        $scope.view = function (val) {
            $location.url('app/AgrMstSuprCadApplicationView?application_gid=' + val + '&lspage=CadPhysicalDocApproval');
        }

        $scope.Completed = function () {
            $location.url('app/AgrTrnSuprCadPhysicalDocCompletedSummary');
        }

        $scope.approver_process = function (val, val1, processtypeassign_gid) {
            if (processtypeassign_gid != undefined)
                $location.url('app/AgrTrnSuprCadPhysicalDocGuarantorDtls?application_gid=' + val + '&lsemployee_gid=' + val1 + '&lsprocesstypeassign_gid=' + processtypeassign_gid + '&lspage=CadPhysicalDocApproval&lspath=Approver');
            else
                $location.url('app/AgrTrnSuprCadPhysicalDocGuarantorDtls?application_gid=' + val + '&lsemployee_gid=' + val1 + '&lspage=CadPhysicalDocApproval');
        }
    }
})();
