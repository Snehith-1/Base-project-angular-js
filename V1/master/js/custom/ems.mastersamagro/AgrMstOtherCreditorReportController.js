(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AgrMstOtherCreditorReportController', AgrMstOtherCreditorReportController);

    AgrMstOtherCreditorReportController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams','DownloaddocumentService'];

    function AgrMstOtherCreditorReportController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams,DownloaddocumentService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AgrMstOtherCreditorReportController';
        activate();
        function activate() {
            var url = 'api/AgrMstApplicationReport/GetOtherCreditorSummary';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.othercreditor_list = resp.data.MstOtherCreditorSummaryList;
                unlockUI();
            });
        }      

        $scope.creditorReport = function () {
            lockUI();
            var url = 'api/AgrMstApplicationReport/ExportMstOtherCreditorReport';
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