(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AgrTrnCadDocChecklistApprovalSummaryController', AgrTrnCadDocChecklistApprovalSummaryController);

    AgrTrnCadDocChecklistApprovalSummaryController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', '$sce'];

    function AgrTrnCadDocChecklistApprovalSummaryController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, $sce) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AgrTrnCadDocChecklistApprovalSummaryController';

        activate();
        lockUI();
        function activate() {
            var url = 'api/AgrTrnCAD/GetCADDocChecklistApprovalSummary';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.sanctionmaker_list = resp.data.cadapplicationlist;
            });

            var url = 'api/AgrTrnCAD/CADDocChecklistSummaryCount';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.cadmaker_count = resp.data.cadmaker_count;
                $scope.cadchecker_count = resp.data.cadchecker_count;
                $scope.cadcheckerapproval_count = resp.data.cadcheckerapproval_count;
                $scope.makerfollowup_count = resp.data.makerfollowup_count;
                $scope.checkerfollowup_count = resp.data.checkerfollowup_count;
                $scope.approvalcompleted_count = resp.data.approvalcompleted_count;
            });

        }
        $scope.maker = function () {
            $location.url('app/AgrTrnCadDocChecklistSummary');
        }

        $scope.checker = function () {
            $location.url('app/AgrTrnCadDocChecklistCheckerSummary');
        }

        $scope.approval = function () {
            $location.url('app/AgrTrnCadDocChecklistApprovalSummary');
        }
        $scope.view = function (val) {
            $location.url('app/AgrMstCadApplicationView?application_gid=' + val + '&lspage=CadDocumentApprovalChecklist');
        }
        $scope.approvalcompleted = function () {
            $location.url('app/AgrTrnDocChecklistApprovalCompleted');
        }
        $scope.process = function (val, val1, val2) { 
            $location.url('app/AgrTrnCadGuarantorApproval?application_gid=' + val + '&lsemployee_gid=' + val1 + '&approve_flag=' + val2 + '&lspage=CadDocumentChecklist');
        }

        $scope.processApprove = function (val, val1) {
            localStorage.setItem('TaggedBy', 'Approver');
            $location.url('app/AgrTrnCadGuarantorDetails?application_gid=' + val + '&lsemployee_gid=' + val1 + '&lspage=CadDocumentChecklist&lspath=maker');
        }
    }
})();
