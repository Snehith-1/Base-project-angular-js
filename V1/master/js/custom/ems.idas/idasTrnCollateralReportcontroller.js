(function () {
    'use strict';

    angular
        .module('angle')
        .controller('idasTrnCollateralReportcontroller', idasTrnCollateralReportcontroller);

    idasTrnCollateralReportcontroller.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams', 'DownloaddocumentService'];

    function idasTrnCollateralReportcontroller($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams, DownloaddocumentService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'idasTrnCollateralReportcontroller';

        activate();

        function activate() {
            var url = 'api/idasTrnLsaReport/GetColletarlSummary';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.customersecurity_list = resp.data.customersecurity_list;
                unlockUI();
            });
        }

        $scope.export = function () {
            lockUI();
            var url = 'api/idasTrnLsaReport/ColletralReportExcel';
            SocketService.get(url).then(function (resp) {
                if (resp.data.status == true) {
                    DownloaddocumentService.Downloaddocument(resp.data.lscloudpath, resp.data.lsname);
                    unlockUI();
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
