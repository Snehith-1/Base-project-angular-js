(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstCadDocChecklistCheckerSummaryController', MstCadDocChecklistCheckerSummaryController);

    MstCadDocChecklistCheckerSummaryController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', '$sce'];

    function MstCadDocChecklistCheckerSummaryController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, $sce) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstCadDocChecklistCheckerSummaryController';

        activate();
        
        function activate() {
            lockUI();
            var url = 'api/MstCAD/GetCADDocChecklistCheckerSummary';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.sanctionmaker_list = resp.data.cadapplicationlist;
            });

            var url = 'api/MstCAD/CADDocChecklistSummaryCount';
            lockUI();
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
             var url = 'api/MstCAD/GetCADDocChecklistCheckerSummary';
             SocketService.get(url).then(function (resp) {
                 unlockUI();
                $scope.sanctionmaker_list = resp.data.cadapplicationlist;
            });

             var url = 'api/MstCAD/CADDocChecklistSummaryCount';
             lockUI();
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
            var url = 'api/MstCAD/GetCADDocChecklistCheckerFollowupSummary';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.checklistfollowup_list = resp.data.cadapplicationlist;
            });

            var url = 'api/MstCAD/CADDocChecklistSummaryCount';
            lockUI();
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
            $location.url('app/MstCadDocumentChecklistSummary');
        }

        $scope.checker = function () {
            $location.url('app/MstCadDocChecklistCheckerSummary');
        }

        $scope.approval = function () {
            $location.url('app/MstCadDocChecklistApprovalSummary');
        }
        $scope.view = function (val) {
            $location.url('app/MstCadApplicationView?application_gid=' + val + '&lspage=CadDocumentChecklist');
        }

        $scope.approvalcompleted = function () {
            $location.url('app/MstDocChecklistApprovalCompleted');
        }

        $scope.process = function (val, val1, lsfollowup) {
            localStorage.setItem('TaggedBy', 'Checker');
            $location.url('app/MstCadGuarantorDetails?application_gid=' + val + '&lsemployee_gid=' + val1 + '&lspage=CadDocumentChecklist&lspath=checker&lsfollowup=' + lsfollowup);
        }
    }
})();
