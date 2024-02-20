(function () {
    'use strict';

    angular
        .module('angle')
        .controller('brsTrnMyUnReconAlloactedPendingSummaryController', brsTrnMyUnReconAlloactedPendingSummaryController);

    brsTrnMyUnReconAlloactedPendingSummaryController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams','DownloaddocumentService'];

    function brsTrnMyUnReconAlloactedPendingSummaryController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams, DownloaddocumentService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'brsTrnMyUnReconAlloactedPendingSummaryController';
        activate();
        function activate() {
            var url = 'api/MyUnreconciliationManagement/GetAllocatedPendingReportSummary';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.unreconallocaterpt_list = resp.data.unreconallocaterpt_list;
                unlockUI();
            });
            var url = "api/MyUnreconciliationManagement/GetMyunreConciliationSummaryCount";
            lockUI();
            SocketService.get(url).then(function (resp) {

                $scope.pendingrpt_count = resp.data.pendingrpt_count;
                $scope.closedrpt_count = resp.data.closedrpt_count;
                $scope.allocatependingrpt_count = resp.data.allocatependingrpt_count;

                unlockUI();
            });

        }
        $scope.pending_rpt = function () {
            $state.go('app.brsTrnMyUnReconciliationSummary');
        }

        $scope.closed_rpt = function () {
            $state.go('app.brsTrnMyUnReconClosedSummary');
        }
        $scope.alloacted_rpt = function () {
            $state.go('app.brsTrnMyUnReconAlloactedPendingSummary');
        }

        $scope.brsallocate_rpt = function () {
            lockUI();
            var url = 'api/MyUnreconciliationManagement/AllocatedPendingExport';
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