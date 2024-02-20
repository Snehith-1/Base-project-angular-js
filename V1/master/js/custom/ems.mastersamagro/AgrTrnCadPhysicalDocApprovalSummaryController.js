﻿(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AgrTrnCadPhysicalDocApprovalSummaryController', AgrTrnCadPhysicalDocApprovalSummaryController);

        AgrTrnCadPhysicalDocApprovalSummaryController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams'];

    function AgrTrnCadPhysicalDocApprovalSummaryController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AgrTrnCadPhysicalDocApprovalSummaryController';

        activate();

        function activate() {
            lockUI();
            var url = 'api/AgrTrnPhysicalDocument/GetCADPhysicalDocApproverSummary';
            SocketService.get(url).then(function (resp) {
                $scope.scannedapproverpendinglist = resp.data.physicalmakerapplication;
            });

            var url = 'api/AgrTrnPhysicalDocument/CADAppPhysicalDocCount';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.overallCountinfo = resp.data;
            });
        }

        $scope.approvalPendingSummary = function () {
            lockUI();
            var url = 'api/AgrTrnPhysicalDocument/GetCADPhysicalDocApproverSummary';
            SocketService.get(url).then(function (resp) {
                $scope.scannedapproverpendinglist = resp.data.physicalmakerapplication;
            });

            var url = 'api/AgrTrnPhysicalDocument/CADAppPhysicalDocCount';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.overallCountinfo = resp.data;
            });
        }

        $scope.approvalFollowupSummary = function () {
            lockUI();
            var url = 'api/AgrTrnPhysicalDocument/GetCADPhysicalDocFollowupApproverSummary';
            SocketService.get(url).then(function (resp) {
                $scope.scannedapproverFollowuplist = resp.data.physicalmakerapplication;
            });
            var url = 'api/AgrTrnPhysicalDocument/CADAppPhysicalDocCount';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.overallCountinfo = resp.data;
            });
        }

        $scope.maker = function () {
            $location.url('app/AgrTrnCadPhysicalDocSummary');
        }

        $scope.checker = function () {
            $location.url('app/AgrTrnCadPhysicalDocCheckerSummary');
        }

        $scope.approval = function () {
            $location.url('app/AgrTrnCadPhysicalDocApprovalSummary');
        }

        $scope.view = function (val) {
            $location.url('app/AgrMstCadApplicationView?application_gid=' + val + '&lspage=CadPhysicalDocApproval');
        }

        $scope.Completed = function () {
            $location.url('app/AgrTrnCadPhysicalDocCompletedSummary');
        }

        $scope.approver_process = function (val, val1, processtypeassign_gid) {
            if (processtypeassign_gid != undefined)
                $location.url('app/AgrTrnCadPhysicalDocGuarantorDtls?application_gid=' + val + '&lsemployee_gid=' + val1 + '&lsprocesstypeassign_gid=' + processtypeassign_gid + '&lspage=CadPhysicalDocApproval&lspath=Approver');
            else
                $location.url('app/AgrTrnCadPhysicalDocGuarantorDtls?application_gid=' + val + '&lsemployee_gid=' + val1 + '&lspage=CadPhysicalDocApproval');
        }
    }
})();