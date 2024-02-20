(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstDocumentChecklistApprovalCompletedReportController', MstDocumentChecklistApprovalCompletedReportController);

    MstDocumentChecklistApprovalCompletedReportController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', '$sce', 'DownloaddocumentService'];

    function MstDocumentChecklistApprovalCompletedReportController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, $sce, DownloaddocumentService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstDocumentChecklistApprovalCompletedReportController';

        activate();
        lockUI();
        function activate() {
            lockUI();
            var url = 'api/MstApplicationReport/GetCADDocChecklistReportApprovalCompletedSummary';
            SocketService.get(url).then(function (resp) {
                $scope.approvercompleted_list = resp.data.cadapplicationlist;
            });

            var url = 'api/MstApplicationReport/GetDocChecklistPendingCount';
            lockUI();
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.cadmaker_count = resp.data.cadmaker_count;
                $scope.cadchecker_count = resp.data.cadchecker_count;
                $scope.cadcheckerapproval_count = resp.data.cadcheckerapproval_count;
                $scope.cadapproval_count = resp.data.cadapproved_count;
            });

        }

        $scope.maker = function () {
            $location.url('app/MstDocumentChecklistReport');
        }

        $scope.checker = function () {
            $location.url('app/MstDocumentChecklistCheckerReport');
        }

        $scope.approval = function () {
            $location.url('app/MstDocumentChecklistApprovalReport');
        }

        $scope.approvalcompleted = function () {
            $location.url('app/MstDocumentChecklistApprovalCompletedReport');
        }


        $scope.appreport = function () {
            lockUI();
            var url = 'api/MstApplicationReport/ExportDocCheckApprovalCompletedReport';
            SocketService.get(url).then(function (resp) {
                if (resp.data.status == true) {
                    unlockUI();
                    DownloaddocumentService.Downloaddocument(resp.data.lscloudpath, resp.data.lsname);                 
                }
                else {
                    unlockUI();
                    Notify.alert('Error Occurred While Export !', 'success')

                }

            });
        }



    }
})();
