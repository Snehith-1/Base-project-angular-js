
(function () {
    'use strict';

    angular
        .module('angle')
        .controller('BrsTrnUnreconAssignedSummaryController', BrsTrnUnreconAssignedSummaryController);

    BrsTrnUnreconAssignedSummaryController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', 'DownloaddocumentService', 'cmnfunctionService'];

    function BrsTrnUnreconAssignedSummaryController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, DownloaddocumentService, cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;

        vm.title = 'BrsTrnUnreconAssignedSummaryController';
        // console.log('test');
        activate();

        function activate() {

            var url = 'api/UnreconciliationManagement/GetBrsUnReconciliationSummary';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.BrsUnreconciliation_list = resp.data.BrsUnreconciliation_list;
                unlockUI();
            });
            var url = 'api/UnreconciliationManagement/GetAllocatedCount';
            lockUI();
            SocketService.get(url).then(function (resp) {               
                $scope.unreconciliation_count = resp.data.unreconciliation_count;
                unlockUI();
            });

        }


        $scope.unreconciliationassignedsummary = function () {


            var url = 'api/UnreconciliationManagement/GetUnreconciliationAssignedSummaryExcelExport';
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
        $scope.UnreconciliationView = function (banktransc_gid) {
            //    $location.url('app/brsTrnUnreconcillationTag?banktransc_gid=' + banktransc_gid);
            $location.url("app/BrsTrnUnreconTransactionDetails?hash=" + cmnfunctionService.encryptURL("banktransc_gid=" + banktransc_gid));
        }
       

        $scope.unreconclosed = function () {
            $state.go('app.brsTrnUnReconCreditClosedManagement');
        }

        $scope.Manualmatch_Update = function (banktransc_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/manualknockoff.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                $scope.banktransc_gid = banktransc_gid;

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.update_status = function () {

                    var params =
                    {
                        banktransc_gid: banktransc_gid,
                        // ticket_source: $scope.ticket_source,
                        manualknockoff_remarks: $scope.txtremarks
                    }
                    var url = 'api/UnreconciliationManagement/PostManualMatch';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            unlockUI();
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000

                            });
                            activate();
                            $modalInstance.close('closed');
                            $state.go('app.brsTrnUnreconCreditSummaryManagement');

                        }
                        else {
                            $modalInstance.close('closed');
                            Notify.alert(resp.data.message, {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                        }
                        unlockUI();

                    });

                }


            }
        }

        $scope.Manualmatch = function (banktransc_gid) {

            var params =
            {
                banktransc_gid: banktransc_gid,
            }
            var url = 'api/UnreconciliationManagement/PostManualMatch';
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    unlockUI();
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    activate();
                    $state.go('app.brsTrnUnreconCreditSummaryManagement');


                }
                else {

                    Notify.alert(resp.data.message, {
                        status: 'warning',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }


            });



        }


    }
})();
