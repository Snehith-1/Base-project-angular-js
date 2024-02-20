(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AgrDocumentChecklistMakerReportController', AgrDocumentChecklistMakerReportController);

    AgrDocumentChecklistMakerReportController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', '$sce','DownloaddocumentService'];

    function AgrDocumentChecklistMakerReportController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, $sce, DownloaddocumentService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AgrDocumentChecklistMakerReportController';

        activate();
      
        function activate() {
            lockUI();
            var url = 'api/AgrMstApplicationReport/GetCADDocChecklistReportSummary';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.sanctionmaker_list = resp.data.documentpendinglist;
                console.log('makerlist', $scope.sanctionmaker_list);
            });

            var url = 'api/AgrMstApplicationReport/GetDocChecklistPendingCount';
            lockUI();
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.cadmaker_count = resp.data.cadmaker_count;
                $scope.cadchecker_count = resp.data.cadchecker_count;
                $scope.cadcheckerapproval_count = resp.data.cadcheckerapproval_count;
                $scope.cadapproval_count = resp.data.cadapproved_count
            });

        }       


        $scope.appreport = function () {
            lockUI();
            var url = 'api/AgrMstApplicationReport/ExportDocCheckMakerPendingReport';
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
            $location.url('app/AgrDocumentChecklistMakerReport');
        }

        $scope.checker = function () {
            $location.url('app/AgrDocumentChecklistCheckerReport');
        }

        $scope.approval = function () {
            $location.url('app/AgrDocumentChecklistApprovalReport');
        }
        $scope.approvalcompleted = function () {
            $location.url('app/AgrDocumentChecklistApprovalCompletedReport');
        }
     
    }
})();
