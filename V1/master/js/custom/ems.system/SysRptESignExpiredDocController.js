(function () {
    'use strict';

    angular
        .module('angle')
        .controller('SysRptESignExpiredDocController', SysRptESignSignedDocController);

    SysRptESignSignedDocController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', '$modal', 'DownloaddocumentService'];

    function SysRptESignSignedDocController($rootScope, $scope, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, $modal, DownloaddocumentService) {

        var vm = this;
        vm.title = 'SysRptESignExpiredDocController';

        activate();

        function activate() {
            var url = 'api/SysMstHRDocument/GetESignExpiredSummary';
            SocketService.get(url).then(function (resp) {
                $scope.document_list = resp.data.hrdoc;
            });

            var url = 'api/SysMstHRDocument/GetESignReportSummaryCount';
            SocketService.get(url).then(function (resp) {
                $scope.pendingesign_count = resp.data.pendingesign_count;
                $scope.completedesign_count = resp.data.completedesign_count;
                $scope.expiredesign_count = resp.data.expiredesign_count;
            });

        }
        $scope.UnSigned = function () {
            $state.go('app.SysRptEsign');
        }

        $scope.Expired = function () {
            $state.go('app.SysRptESignExpiredDoc');
        }

        $scope.Signed = function () {
            $state.go('app.SysRptESignSignedDoc');
        }
        
        $scope.esignreporthrdoc = function () {


            var url = 'api/SysMstHRDocument/GetESignReportHRDocExcelExport';
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

