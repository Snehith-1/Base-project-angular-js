(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AgrMstWarehouseReportController', AgrMstWarehouseReportController);

    AgrMstWarehouseReportController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams','DownloaddocumentService'];

    function AgrMstWarehouseReportController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams,DownloaddocumentService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AgrMstWarehouseReportController';
        activate();
        function activate() {
            var url = 'api/AgrMstApplicationReport/GetWarehouseSummary';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.warehouse_list = resp.data.MstWarehouseSummaryList;
                unlockUI();
            });
        }      

        $scope.warehousereport = function () {
            lockUI();
            var url = 'api/AgrMstApplicationReport/ExportMstWarehouseReport';
            SocketService.get(url).then(function (resp) {
                if (resp.data.status == true) {
                    unlockUI();
                    DownloaddocumentService.Downloaddocument(resp.data.lscloudpath, resp.data.lsname);
                  
                }
                else {
                    unlockUI();
                    Notify.alert('Error Occurred While Export !', 'success')
                }

            });
        }
    }
})();