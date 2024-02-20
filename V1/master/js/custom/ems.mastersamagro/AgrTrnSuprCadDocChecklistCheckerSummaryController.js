
(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AgrTrnSuprCadDocChecklistCheckerSummaryController', AgrTrnSuprCadDocChecklistCheckerSummaryController);

    AgrTrnSuprCadDocChecklistCheckerSummaryController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', '$sce'];

    function AgrTrnSuprCadDocChecklistCheckerSummaryController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, $sce) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AgrTrnSuprCadDocChecklistCheckerSummaryController';

        activate();
        lockUI();
        function activate() {
            lockUI();
            var url = 'api/AgrTrnSuprCAD/GetCADDocChecklistCheckerSummary';
            SocketService.get(url).then(function (resp) { 
                $scope.sanctionmaker_list = resp.data.cadapplicationlist;
            });

            var url = 'api/AgrTrnSuprCAD/CADDocChecklistSummaryCount';
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

        $scope.checklistCheckerPendingSummary = function () {
            lockUI();
             var url = 'api/AgrTrnSuprCAD/GetCADDocChecklistCheckerSummary';
            SocketService.get(url).then(function (resp) { 
                $scope.sanctionmaker_list = resp.data.cadapplicationlist;
            });

            var url = 'api/AgrTrnSuprCAD/CADDocChecklistSummaryCount';
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

        $scope.checklistCheckerFollowupSummary = function () {
            lockUI();
            var url = 'api/AgrTrnSuprCAD/GetCADDocChecklistCheckerFollowupSummary';
            SocketService.get(url).then(function (resp) { 
                $scope.checklistfollowup_list = resp.data.cadapplicationlist;
            });

            var url = 'api/AgrTrnSuprCAD/CADDocChecklistSummaryCount';
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
            $location.url('app/AgrTrnSuprCadDocChecklistSummary');
        }

        $scope.checker = function () {
            $location.url('app/AgrTrnSuprCadDocChecklistCheckerSummary');
        }

        $scope.approval = function () {
            $location.url('app/AgrTrnSuprCadDocChecklistApprovalSummary');
        }

        $scope.approvalcompleted = function () {
            $location.url('app/AgrTrnSuprDocChecklistApprovalCompleted');
        }

        $scope.view = function (val) {
            $location.url('app/AgrMstSuprCadApplicationView?application_gid=' + val + '&lspage=CadDocumentCheckerChecklist');
        }

        $scope.process = function (val, val1, lsfollowup) {
            
            localStorage.setItem('TaggedBy', 'Checker');
            $location.url('app/AgrTrnSuprCadGuarantorDetails?application_gid=' + val + '&lsemployee_gid=' + val1 + '&lspage=CadDocumentChecklist&lspath=checker&lsfollowup=' + lsfollowup);
        }
    }
})();
