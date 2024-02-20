


(function () {
    'use strict';

    angular
        .module('angle')
        .controller('brsTrnUnReconDebitCloseManagementController', brsTrnUnReconDebitCloseManagementController);

    brsTrnUnReconDebitCloseManagementController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', 'DownloaddocumentService', 'cmnfunctionService'];

    function brsTrnUnReconDebitCloseManagementController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, DownloaddocumentService, cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        var lspage = $location.search().lspage;
        vm.title = 'brsTrnUnReconDebitCloseManagementController';
        // console.log('test');
        activate();

        function activate() {

            //var url = 'api/KotakReconcillation/GetTransactionSummary';
            //lockUI();
            //SocketService.get(url).then(function (resp) {
            //    $scope.transaction_list = resp.data.transaction_list;
            //    unlockUI();

            //});
            var url = 'api/UnreconciliationManagement/GetUnReconciliationClosed';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.banktransc_gid = resp.data.banktransc_gid;
                $scope.BankAlertUnreconciliationdebit_list = resp.data.BankAlertUnreconciliationdebit_list;
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
        $scope.unrecondebitclosedsummarymanagement = function () {


            var url = 'api/UnreconciliationManagement/GetUnreconDebitClosedManagementExcelExport';
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
        $scope.Credit_Summary = function () {
            $state.go('app.brsTrnUnreconCreditSummaryManagement');
        }
        $scope.Debit_Summary = function () {
            $state.go('app.brsTrnUnreconDebitSummaryManagement');
        }
        $scope.findebitpending = function () {
            $state.go('app.brsTrnUnreconDebitFinancePendingManagement');
        }
        $scope.unreconpending = function () {
            $state.go('app.brsTrnUnreconDebitSummaryManagement');
        }

        $scope.unreconassigned = function () {
            $state.go('app.brsTrnUnReconDebitAssignedManagement');
        }
        $scope.unreconDBreassigned = function () {
            $state.go('app.brsTrnUnreconDebitReassignpendingSummaryManagement');
        }

        $scope.unreconclosed = function () {
            $state.go('app.brsTrnUnReconDebitCloseManagement');
        }

        $scope.view = function (banktransc_gid) {
            /* $location.url('app/brsTrnUnreconTagViewAssignedHistory?banktransc_gid=' + banktransc_gid);*/
            $location.url("app/brsTrnUnreconTagViewAssignedHistory?hash=" + cmnfunctionService.encryptURL("banktransc_gid=" + banktransc_gid + '&lspage=DebitClose'));
        }

        $scope.compunrecon = function () {
            $state.go('app.brsTrnUnReconcillationCompletedManagement');
        }
        $scope.unreconcompdebit = function () {
            $state.go('app.brsTrnUnReconcillationCompleteddebitManagement');
        }

        $scope.requestback = function () {
            if (lspage == "reopenactivity") {
                $location.url('app/osdTrnReopenRequestSummary');
            }
            else if (lspage == "closeactivity") {
                $location.url('app/osdTrnCloseRequestSummary');
            }
            else if (lspage == "rejectedrequest") {
                $location.url('app/osdTrnRejectedRequestSummary');
            }
            else {
                $location.url('app/osdTrnServiceRequestSummary');
            }
        }

        $scope.Transfer = function (banktransc_gid) {
            /*   $location.url('app/brsTrnUnreconcillationTransfer?banktransc_gid=' + banktransc_gid);*/
            $location.url("app/brsTrnUnreconcillationTransfer?hash=" + cmnfunctionService.encryptURL("banktransc_gid=" + banktransc_gid));
        }
    }
})();
