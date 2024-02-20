(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AgrTrnCadDocChecklistSummaryController', AgrTrnCadDocChecklistSummaryController);

    AgrTrnCadDocChecklistSummaryController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', '$sce'];

    function AgrTrnCadDocChecklistSummaryController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, $sce) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AgrTrnCadDocChecklistSummaryController';

        activate();
        lockUI();
        function activate() {
            lockUI();
            var url = 'api/AgrTrnCAD/GetCADDocChecklistMakerSummary';
            SocketService.get(url).then(function (resp) {
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

        $scope.ChecklistPendingSummary = function () {
            lockUI();
            var url = 'api/AgrTrnCAD/GetCADDocChecklistMakerSummary';
            SocketService.get(url).then(function (resp) {
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

        $scope.ChecklistFollowupSummary = function () {
            lockUI();
            var url = 'api/AgrTrnCAD/GetCADDocChecklistFollowupMakerSummary';
            SocketService.get(url).then(function (resp) {
                $scope.checklistfollowupmaker_list = resp.data.cadapplicationlist;
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

        $scope.approvalcompleted = function () {
            $location.url('app/AgrTrnDocChecklistApprovalCompleted');
        }

        $scope.view = function (val) {
            $location.url('app/AgrMstCadApplicationView?application_gid=' + val + '&lspage=CadDocumentChecklist');
        }

        $scope.process = function (val, val1, followup) {
            localStorage.setItem('TaggedBy', 'Maker');
            $location.url('app/AgrTrnCadGuarantorDetails?application_gid=' + val + '&lsemployee_gid=' + val1 + '&lspage=CadDocumentChecklist&lspath=maker&lsfollowup=' + followup);
        }
    }
})();
