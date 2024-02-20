


(function () {
    'use strict';

    angular
        .module('angle')
        .controller('brsTrnUnReconDebitReassignSummaryManagementController', brsTrnUnReconDebitReassignSummaryManagementController);

    brsTrnUnReconDebitReassignSummaryManagementController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', 'DownloaddocumentService','cmnfunctionService'];

    function brsTrnUnReconDebitReassignSummaryManagementController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, DownloaddocumentService, cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;

        vm.title = 'brsTrnUnReconDebitReassignSummaryManagementController';
        // console.log('test');
        activate();

        function activate() {

            var url = 'api/UnreconciliationManagement/GetunreConreassignpendingSummary';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.unrecocillationpendingdebit_list = resp.data.unrecocillationpendingdebit_list;
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
                //$scope.unreconpendingdebit_count = resp.data.unreconpendingdebit_count;
                //$scope.unreconpendingcredit_count = resp.data.unreconpendingcredit_count;
                //$scope.unreconpending_count = resp.data.unreconpending_count;
                //$scope.unreconcompdebit_count = resp.data.unreconcompdebit_count;
                //$scope.unreconcompcredit_count = resp.data.unreconcompcredit_count;
                //$scope.unreconcomp_count = resp.data.unreconcomp_count;
                unlockUI();
            });

        }
        $scope.unrecondebitreassignpendingsummarymanagement = function () {


            var url = 'api/UnreconciliationManagement/GetUnreconDebitReassignPendingManagementExcelExport';
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
        //    $location.url('app/brsTrnUnreconcillationTag?banktransc_gid=' + banktransc_gid);
            $location.url("app/brsTrnUnreconcillationReassignTag?hash=" + cmnfunctionService.encryptURL("banktransc_gid=" + banktransc_gid + '&lspage=Debit'));
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
            $state.go('app.brsTrnUnReconDebitClosedManagement');
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
