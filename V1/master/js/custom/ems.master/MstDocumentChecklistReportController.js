(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstDocumentChecklistReportController', MstDocumentChecklistReportController);

    MstDocumentChecklistReportController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', '$sce','DownloaddocumentService'];

    function MstDocumentChecklistReportController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, $sce, DownloaddocumentService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstDocumentChecklistReportController';

        activate();
      
        function activate() {
            lockUI();
            var url = 'api/MstApplicationReport/GetCADDocChecklistReportSummary';
            SocketService.get(url).then(function (resp) {
               
                if (resp.data.status == true) {
                    $scope.sanctionmaker_list = resp.data.cadapplicationlist;
                    unlockUI();
                }
                else if (resp.data.status == false)
                    unlockUI();
                console.log('makerlist', $scope.sanctionmaker_list);
            });

            var url = 'api/MstApplicationReport/GetDocChecklistPendingCount';
            
            SocketService.get(url).then(function (resp) {
              
                $scope.cadmaker_count = resp.data.cadmaker_count;
                $scope.cadchecker_count = resp.data.cadchecker_count;
                $scope.cadcheckerapproval_count = resp.data.cadcheckerapproval_count;
                $scope.cadapproval_count = resp.data.cadapproved_count;
            });

        }       


        $scope.appreport = function () {
            lockUI();
            var url = 'api/MstApplicationReport/ExportDocCheckMakerPendingReport';
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
     
    }
})();
