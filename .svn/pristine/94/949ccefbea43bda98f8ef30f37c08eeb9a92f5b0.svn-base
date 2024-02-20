(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstMarketingReportController', MstMarketingReportController);

    MstMarketingReportController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams', 'DownloaddocumentService', 'cmnfunctionService'];

    function MstMarketingReportController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams, DownloaddocumentService, cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstMarketingReportController';
        activate();
        function activate() {
            var url = 'api/Marketing/MarketingReportSummary';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.TeleCallingReportList = resp.data.MarketingReportList;
                unlockUI();
            }); 
             
        }
        
        $scope.TeleCallingReport = function () {
            lockUI();
            var url = 'api/Marketing/ExportMarketingReport';
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
                    Notify.alert('Error Occurred While Exporting !', 'warning')

                }

            });
        }
       

        $scope.view = function (val) {
            $location.url('app/MstMarketingReportView?hash=' + cmnfunctionService.encryptURL('marketingcall_gid=' + val));
        }
        
    }
})();