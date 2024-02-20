(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstSanctionMISApprovalReportController', MstSanctionMISApprovalReportController);

        MstSanctionMISApprovalReportController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams', 'DownloaddocumentService'];

    function MstSanctionMISApprovalReportController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams, DownloaddocumentService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstSanctionMISApprovalReportController';
        activate();

        function activate() {
            var url = 'api/MstApplicationReport/GetSanctionMISApproverSummary';
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
            var url = 'api/MstApplicationReport/ExportSanctionMISApproverReport';
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
