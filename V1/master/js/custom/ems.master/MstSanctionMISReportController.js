(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstSanctionMISReportController', MstSanctionMISReportController);

    MstSanctionMISReportController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams', 'DownloaddocumentService'];

    function MstSanctionMISReportController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams, DownloaddocumentService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstSanctionMISReportController';
        activate();

        function activate() {
            var url = 'api/MstApplicationReport/GetSanctionMISSummary';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.SanctionMISdtl_list = resp.data.SanctionMISdtl_list;
                unlockUI();
            });
            
        }

        $scope.sanctionmisreportview = function (application2sanction_gid, application_gid) {
            $location.url('app/MstSanctionMISReportView?application2sanction_gid=' + application2sanction_gid + '&application_gid=' + application_gid);
        }

       

        $scope.exportsanctionreport = function () {
            lockUI();
            var url = 'api/MstApplicationReport/ExportSanctionMISReport';
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
                    Notify.alert('Error Occurred While Export !')

                }

            });
        }

       
       
      

       

    }
})();
