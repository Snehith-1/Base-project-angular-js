(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstBuyerReportController', MstBuyerReportController);

    MstBuyerReportController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams', 'DownloaddocumentService'];

    function MstBuyerReportController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams, DownloaddocumentService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstBuyerReportController';
        activate();

        function activate() { 
            var url = 'api/MstApplicationReport/GetBuyerReportSummary';
              SocketService.get(url).then(function (resp) {
               $scope.buyerreport_list = resp.data.BuyerReport_list;
           }); 
       }

        
        $scope.exportbuyerreport = function () {
            lockUI();
            var url = 'api/MstApplicationReport/ExportBuyerReport';
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

        // $scope.buyerreportview = function (buyer_gid, buyer_code) {
        //     $location.url('app/MstBuyerView?buyer_gid=' + buyer_gid + '&buyer_code=' + buyer_code);
        // }

       
    }
})();
