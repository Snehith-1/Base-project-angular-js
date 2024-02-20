(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AgrTrnDocChecklistApprovalCompletedController', AgrTrnDocChecklistApprovalCompletedController);

    AgrTrnDocChecklistApprovalCompletedController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', '$sce'];

    function AgrTrnDocChecklistApprovalCompletedController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, $sce) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AgrTrnDocChecklistApprovalCompletedController';

        activate();
        lockUI();
        function activate() {
            lockUI();
            var url = 'api/AgrTrnCAD/GetDocChecklistApprovalCompletedSummary';
            SocketService.get(url).then(function (resp) {
                $scope.approvercompleted_list = resp.data.cadapplicationlist;
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
            $location.url('app/AgrMstCadApplicationView?application_gid=' + val + '&lspage=CadDocCompletedChecklist');
        }
        
        $scope.approved_process = function (val, val1, lsfollowup) {
            localStorage.setItem('TaggedBy', 'After Approval');
            $location.url('app/AgrTrnCadGuarantorApproval?application_gid=' + val + '&lsemployee_gid=' + val1 + '&lspage=CadDocumentChecklist&lspath=Completedapproval&lsfollowup=' + lsfollowup);
        }
    }
})();
