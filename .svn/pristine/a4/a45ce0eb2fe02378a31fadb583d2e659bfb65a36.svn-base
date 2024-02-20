


(function () {
    'use strict';

    angular
        .module('angle')
        .controller('brsTrnUnReconDebitClosedManagementController', brsTrnUnReconDebitClosedManagementController);

    brsTrnUnReconDebitClosedManagementController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', 'DownloaddocumentService', 'cmnfunctionService'];

    function brsTrnUnReconDebitClosedManagementController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, DownloaddocumentService, cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'brsTrnUnReconDebitClosedManagementController';
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

        $scope.Credit_Summary = function () {
            $state.go('app.brsTrnUnreconCreditSummaryManagement');
        }
        $scope.Debit_Summary = function () {
            $state.go('app.brsTrnUnreconDebitSummaryManagement');
        }

        $scope.unreconpending = function () {
            $state.go('app.brsTrnUnreconDebitSummaryManagement');
        }

        $scope.unreconassigned = function () {
            $state.go('app.brsTrnUnReconDebitAssignedManagement');
        }

        $scope.unreconclosed = function () {
            $state.go('app.brsTrnUnReconDebitClosedManagement');
        }

        $scope.view = function (banktransc_gid) {
            /* $location.url('app/brsTrnUnreconTagViewAssignedHistory?banktransc_gid=' + banktransc_gid);*/
            $location.url("app/brsTrnUnreconTagViewAssignedHistory?hash=" + cmnfunctionService.encryptURL("banktransc_gid=" + banktransc_gid + '&lspage=DebitClose'));
        }

        //$scope.compunrecon = function () {
        //    $state.go('app.brsTrnUnReconcillationCompletedManagement');
        //}
        //$scope.unreconcompdebit = function () {
        //    $state.go('app.brsTrnUnReconcillationCompleteddebitManagement');
        //}

        $scope.Transfer = function (banktransc_gid) {
            /*   $location.url('app/brsTrnUnreconcillationTransfer?banktransc_gid=' + banktransc_gid);*/
            $location.url("app/brsTrnUnreconcillationTransfer?hash=" + cmnfunctionService.encryptURL("banktransc_gid=" + banktransc_gid));
        }
    }
})();
