(function () {
    'use strict';

    angular
        .module('angle')
        .controller('idasTrnNocAndNdcReportController', idasTrnNocAndNdcReportController);

    idasTrnNocAndNdcReportController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', 'DownloaddocumentService'];

    function idasTrnNocAndNdcReportController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, DownloaddocumentService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'idasTrnNocAndNdcReportController';
        activate();
        function activate() {
            lockUI();
            var url = 'api/IdasNocAndNdc/GetIdasNocReportSummary';
            SocketService.get(url).then(function (resp) {
                $scope.nocandndc_list = resp.data.nocandndc_list;

                unlockUI();
            });
        }
        $scope.excelnocandndc = function () {
            lockUI();
            var url = 'api/IdasNocAndNdc/ExportExcelNoc';
            SocketService.get(url).then(function (resp) {
                if (resp.data.status == true) {
                    unlockUI();
                    DownloaddocumentService.Downloaddocument(resp.data.lspath, resp.data.lsname);
                    // var phyPath = resp.data.lspath;
                    // var relPath = phyPath.split("EMS");
                    // var relpath1 = relPath[1].replace("\\", "/");
                    // var hosts = window.location.host;
                    // var prefix = location.protocol + "//";
                    // var str = prefix.concat(hosts, relpath1);
                    // var link = document.createElement("a");
                    // var name = resp.data.lsname.split('.');
                    // link.download = name[0];
                    // var uri = str;
                    // link.href = uri;
                    // link.click();
                }
                else {
                    unlockUI();
                    Notify.alert('Error Occurred While Export !', 'warning')
                }

            });
        }
    }
})();

