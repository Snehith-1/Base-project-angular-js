(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstDocChecklistApprovalCompletedController', MstDocChecklistApprovalCompletedController);

    MstDocChecklistApprovalCompletedController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', '$sce'];

    function MstDocChecklistApprovalCompletedController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, $sce) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstDocChecklistApprovalCompletedController';

        activate();
        
        function activate() {
            lockUI();
            var url = 'api/MstCAD/GetDocChecklistApprovalCompletedSummary';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.approvercompleted_list = resp.data.cadapplicationlist;
            });

            var url = 'api/MstCAD/CADDocChecklistSummaryCount';
           /* lockUI();*/
            SocketService.get(url).then(function (resp) {
   /*             unlockUI();*/
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

        $scope.approvalcompleted = function () {
            $location.url('app/MstDocChecklistApprovalCompleted');
        }

        $scope.view = function (val) {
            $location.url('app/MstCadApplicationView?application_gid=' + val + '&lspage=CadDocumentApprovalCompletelist');
        }
        
        $scope.approved_process = function (val, val1, lsfollowup) {
            localStorage.setItem('TaggedBy', 'After Approval');
            $location.url('app/MstCadGuarantorApproval?application_gid=' + val + '&lsemployee_gid=' + val1 + '&lspage=CadDocumentChecklist&lspath=Completedapproval&lsfollowup=' + lsfollowup);
        }
    }
})();
