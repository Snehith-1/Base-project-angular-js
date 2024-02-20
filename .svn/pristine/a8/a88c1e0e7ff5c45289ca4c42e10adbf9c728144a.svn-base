


(function () {
    'use strict';

    angular
        .module('angle')
        .controller('brsTrnUnReconCreditAssignedManagementController', brsTrnUnReconCreditAssignedManagementController);

    brsTrnUnReconCreditAssignedManagementController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', 'DownloaddocumentService','cmnfunctionService'];

    function brsTrnUnReconCreditAssignedManagementController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, DownloaddocumentService, cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'brsTrnUnReconCreditAssignedManagementController';
        // console.log('test');
        activate();

        function activate() {

            // var url = 'api/UnreconciliationManagement/GetTransferreddisablestatus';
            // lockUI();
            // SocketService.get(url).then(function (resp) {
            //    $scope.brs_status = resp.data.brs_status;
            //    unlockUI();

            // });
            var url = 'api/UnreconciliationManagement/GetUnReconciliationAssigned';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.banktransc_gid = resp.data.banktransc_gid;
                $scope.BankAlertUnreconciliationcredit_list = resp.data.BankAlertUnreconciliationcredit_list;
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
                $scope.fin_count = resp.data.unreconfin_count;               

                //$scope.unreconpendingdebit_count = resp.data.unreconpendingdebit_count;
                //$scope.unreconpendingcredit_count = resp.data.unreconpendingcredit_count;
                //$scope.unreconpending_count = resp.data.unreconpending_count;
                //$scope.unreconcompdebit_count = resp.data.unreconcompdebit_count;
                //$scope.unreconcompcredit_count = resp.data.unreconcompcredit_count;
                //$scope.unreconcomp_count = resp.data.unreconcomp_count;

                unlockUI();
            });

        }
        $scope.unreconcreditassignedsummarymanagement = function () {


            var url = 'api/UnreconciliationManagement/GetUnreconCreditAssignedManagementExcelExport';
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

        $scope.unreconpending = function () {
            $state.go('app.brsTrnUnreconCreditSummaryManagement');
        }

        $scope.unreconCRreassigned = function () {
            $state.go('app.brsTrnUnreconCreditReassignpendingSummaryManagement');
        }

        $scope.unreconassigned = function () {
            $state.go('app.brsTrnUnReconCreditAssignedManagement');
        }
        $scope.finpending = function () {
            $state.go('app.brsTrnUnreconCreditFinancePendingManagement');
        }
        $scope.unreconclosed = function () {
            $state.go('app.brsTrnUnReconCreditClosedManagement');
        }
       
        $scope.view = function (banktransc_gid) {
            /* $location.url('app/brsTrnUnreconTagViewAssignedHistory?banktransc_gid=' + banktransc_gid);*/
            $location.url("app/brsTrnUnreconTagViewAssignedHistory?hash=" + cmnfunctionService.encryptURL("banktransc_gid=" + banktransc_gid+ '&lspage=CreditAssign'));
        }

        $scope.compunrecon = function () {
            $state.go('app.brsTrnUnReconcillationCompletedManagement');
        }
        $scope.unreconcompdebit = function () {
            $state.go('app.brsTrnUnReconcillationCompleteddebitManagement');
        }

        $scope.Transfer = function (banktransc_gid) {
            /*   $location.url('app/brsTrnUnreconcillationTransfer?banktransc_gid=' + banktransc_gid);*/
            $location.url("app/brsTrnUnreconcillationTransfer?hash=" + cmnfunctionService.encryptURL("banktransc_gid=" + banktransc_gid + '&lspage=Credit'));
        }
    }
})();
