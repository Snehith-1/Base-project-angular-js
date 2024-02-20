(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstSanctionMISReportCheckerController', MstSanctionMISReportCheckerController);

        MstSanctionMISReportCheckerController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams', 'DownloaddocumentService'];

    function MstSanctionMISReportCheckerController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams, DownloaddocumentService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstSanctionMISReportCheckerController';
        activate();

        function activate() {
            var url = 'api/MstApplicationReport/GetSanctionMISCheckerSummary';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.SanctionMISdtl_list = resp.data.SanctionMISdtl_list;
                unlockUI();
            });
            
        }

        $scope.sanctionmisreportview = function (application2sanction_gid, application_gid) {
            $location.url('app/MstSanctionMISReportView?application2sanction_gid=' + application2sanction_gid + '&application_gid=' + application_gid + '&lsreportpage=Checker');
        }

       

        $scope.exportsanctionreport = function () {
            lockUI();
            var url = 'api/MstApplicationReport/ExportSanctionMISCheckerReport';
            SocketService.get(url).then(function (resp) {
                if (resp.data.status == true) {
                    unlockUI();
                    DownloaddocumentService.Downloaddocument(resp.data.lscloudpath, resp.data.lsname);
                }
                else {
                    unlockUI();
                    Notify.alert('Error Occurred While Export !')

                }

            });
        }
    }
})();
