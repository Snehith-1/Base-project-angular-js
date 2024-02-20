(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstDocumentChecklistApprovalReportController', MstDocumentChecklistApprovalReportController);

    MstDocumentChecklistApprovalReportController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', '$sce','DownloaddocumentService'];

    function MstDocumentChecklistApprovalReportController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, $sce, DownloaddocumentService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstDocumentChecklistApprovalReportController';

        activate();
      
        function activate() {
            lockUI();
            var url = 'api/MstApplicationReport/GetCADDocChecklistReportApprovalSummary';
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
            var url = 'api/MstApplicationReport/ExportDocCheckApprovalPendingReport';
            SocketService.get(url).then(function (resp) {
                if (resp.data.status == true) {
                    unlockUI();
                    DownloaddocumentService.Downloaddocument(resp.data.lscloudpath, resp.data.lsname);
                    //var phyPath = resp.data.lspath;
                    //var relPath = phyPath.split("EMS");
                    //var relpath1 = relPath[1].replace("\\", "/");
                    //var hosts = window.location.host;
                    //var prefix = location.protocol + "//";
                    //var str = prefix.concat(hosts, relpath1);
                    //var link = document.createElement("a");
                    //var name = resp.data.lsname.split('.');
                    //link.download = name[0];
                    //var uri = str;
                    //link.href = uri;
                    //link.click();
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
