


(function () {
    'use strict';

    angular
        .module('angle')
        .controller('brsTrnUnreconDebitFinancePendingManagementController', brsTrnUnreconDebitFinancePendingManagementController);

    brsTrnUnreconDebitFinancePendingManagementController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', 'DownloaddocumentService', 'cmnfunctionService'];

    function brsTrnUnreconDebitFinancePendingManagementController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, DownloaddocumentService, cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'brsTrnUnreconDebitFinancePendingManagementController';
        // console.log('test');

        activate();

        function activate() {

            var url = 'api/UnreconciliationManagement/GetunreConcillationFinanceSummary';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.unrecocillationfinpendingdebit_list = resp.data.unrecocillationfinpendingdebit_list;
                unlockUI();

            });
            var url = "api/UnreconciliationManagement/GetunreConcillationCount";
            SocketService.get(url).then(function (resp) {
                $scope.creditsum_count = resp.data.creditsum_count;
                $scope.debitsum_count = resp.data.debitsum_count;
                $scope.unreconpendingcredit_count = resp.data.unreconpendingcredit_count;
                $scope.unreconassigncredit_count = resp.data.unreconassigncredit_count;
                $scope.unreconclosecredit_count = resp.data.unreconclosecredit_count;
                $scope.unreconpendingdebit_count = resp.data.unreconpendingdebit_count;
                $scope.unreconassigndebit_count = resp.data.unreconassigndebit_count;
                $scope.unreconclosedebit_count = resp.data.unreconclosedebit_count;
                $scope.unreconreassignpendingcredit_count = resp.data.unreconreassignpendingcredit_count;
                $scope.unreconreassignpendingdebit_count = resp.data.unreconreassignpendingdebit_count;
                $scope.findebit_count = resp.data.unrecondebitfin_count;


                unlockUI();
            });

        }

        $scope.unrecondebitfinancependingsummarymanagement = function () {


            var url = 'api/UnreconciliationManagement/GetUnreconDebitFinancePendingManagementExcelExport';
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
        $scope.Unreconciliationtag = function (banktransc_gid) {

            $location.url("app/brsTrnUnreconcillationTag?hash=" + cmnfunctionService.encryptURL("banktransc_gid=" + banktransc_gid + '&lspage=Debit'));
        }
        $scope.Credit_Summary = function () {
            $state.go('app.brsTrnUnreconCreditSummaryManagement');
        }
        $scope.Debit_Summary = function () {
            $state.go('app.brsTrnUnreconDebitSummaryManagement');
        }

        $scope.unreconpending = function () {
            $state.go('app.brsTrnUnreconDebitSummaryManagement');
        }

        $scope.findebitpending = function () {
            $state.go('app.brsTrnUnreconDebitFinancePendingManagement');
        }
        $scope.unreconDBreassigned = function () {
            $state.go('app.brsTrnUnreconDebitReassignpendingSummaryManagement');
        }

        $scope.unreconassigned = function () {
            $state.go('app.brsTrnUnReconDebitAssignedManagement');
        }

        $scope.unreconclosed = function () {
            $state.go('app.brsTrnUnReconDebitCloseManagement');
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
                            $state.go('app.brsTrnUnreconDebitSummaryManagement');

                        }
                        else {
                            $modalInstance.close('closed');
                            Notify.alert(resp.data.message, {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                        }


                    });
                    unlockUI();

                }
                $modalInstance.close('closed');

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



                }
                else {
                    unlockUI();
                    Notify.alert(resp.data.message, {
                        status: 'warning',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
                $state.go('app.brsTrnUnreconDebitSummaryManagement');

            });
            activate();


        }


    }
})();
