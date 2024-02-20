(function () {
    'use strict';

    angular
        .module('angle')
        .controller('sdcTrnUatReportController', sdcTrnUatReportController);

    sdcTrnUatReportController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams'];

    function sdcTrnUatReportController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'sdcTrnUatReportController';

        activate();

        function activate() {

            var url = 'api/SdcTrnReport/GetUatSummaryReport';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.uatsummary_list = resp.data.uatsummary_list;
                unlockUI();

            });
        }
        $scope.export = function () {
            lockUI();

            var url = 'api/SdcTrnReport/GetUatReportExport';
            SocketService.get(url).then(function (resp) {
                if (resp.data.status == true) {
                    unlockUI();
                    var phyPath = resp.data.lspath;
                    var relPath = phyPath.split("EMS");
                    var relpath1 = relPath[1].replace("\\", "/");
                    var hosts = window.location.host;
                    var prefix = location.protocol + "//";
                    var str = prefix.concat(hosts, relpath1);
                    var link = document.createElement("a");
                    var name = resp.data.lsname.split('.');
                    link.download = name[0];
                    var uri = str;
                    link.href = uri;
                    link.click();

                }
                else {
                    unlockUI();
                    Notify.alert('Error Occurred While Export !', 'success')
                    activate();

                }

            });
        }
    }
})();
