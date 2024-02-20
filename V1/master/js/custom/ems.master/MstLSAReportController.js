(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstLSAReportController', MstLSAReportController);

    MstLSAReportController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', '$modal', 'DownloaddocumentService'];

    function MstLSAReportController($rootScope, $scope, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, $modal, DownloaddocumentService) {
     
        var vm = this;
        vm.title = 'MstLSAReportController';

        activate();

        function activate() {
            var url = 'api/MstLSA/GetLSAReportSummary';
            SocketService.get(url).then(function (resp) {
                $scope.LSAReportSummary = resp.data.MdlLSAReportSummary;
            });

        }

       

        $scope.view = function (val) {
            $location.url('app/MstCadApplicationView?application_gid=' + val + '&lspage=CadLsaReport');
        }

        $scope.LSApdf = function (lsgeneratelsa_gid) {
            lockUI();
            var params = {
                generatelsa_gid: lsgeneratelsa_gid
            }
            var url = 'api/MstLSA/GetLSApdf';
            SocketService.getparams(url, params).then(function (resp) {

                if (resp.data.status == true) {
                    var filepath = resp.data.file_path;
                    var filename = resp.data.file_name;
                    DownloaddocumentService.Downloaddocument(filepath, filename);
                    Notify.alert('LSA Report Downloaded Successfully', 'success')
                    unlockUI();
                }
                else {
                    unlockUI();
                    Notify.alert('Error Occurred While Export PDF !', 'warning');
                }

            });

        }

        $scope.maker_process = function (val, val1, followup) {
            $location.url('app/MstCadLSADtlSummary?application_gid=' + val + '&application2sanction_gid=' + val1 + '&lspage=CadLsaReport&lsfollowup=' + followup);
        }

        $scope.lsareportexport = function () {


            var url = 'api/MstLSA/GetLSAReportExcelExport';
            SocketService.get(url).then(function (resp) {
                if (resp.data.status == true) {
                    DownloaddocumentService.Downloaddocument(resp.data.lscloudpath, resp.data.lsname);
                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'warning',
                        pos: 'top-center',
                        timeout: 3000
                    }); activate();
                }

            });
        }
    }
})();
