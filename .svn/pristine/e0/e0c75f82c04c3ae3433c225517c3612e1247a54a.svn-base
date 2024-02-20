// JavaScript source code
(function () {
    'use strict';

    angular
        .module('angle')
        .controller('brsTrnMyUnReconciliationClosedViewController', brsTrnMyUnReconciliationClosedViewController);

    brsTrnMyUnReconciliationClosedViewController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', 'cmnfunctionService'];

    function brsTrnMyUnReconciliationClosedViewController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, cmnfunctionService) {

        var vm = this;
        vm.title = 'brsTrnMyUnReconciliationClosedViewController';

        /*    var banktransc_gid = $location.search().banktransc_gid;*/

        var searchObject = cmnfunctionService.decryptURL($location.search().hash);
        var banktransc_gid = searchObject.banktransc_gid;

        activate();
        function activate() {


            var url = 'api/MyUnreconciliationManagement/GetMyUnreconReportView';
            var param = {
                banktransc_gid: banktransc_gid

            }

            SocketService.getparams(url, param).then(function (resp) {
                $scope.banktransc_gid = banktransc_gid;
                $scope.banktransc_refno = resp.data.banktransc_refno;
                $scope.lblbank_name = resp.data.bank_name;
                $scope.lblcustomer_refno = resp.data.custref_no;
                $scope.lblbranch_name = resp.data.branch_name;
                $scope.lblcr_dr = resp.data.cr_dr;
                $scope.lblallocated_status = resp.data.allocated_status;
                $scope.lbltransc_value = resp.data.transact_val;
                $scope.lblremarks = resp.data.remarks;
                $scope.lbltransc_balance = resp.data.transc_balance;
                $scope.lblacc_no = resp.data.acc_no;
                $scope.lbltrn_date = resp.data.trn_date;

                $scope.knockoff_status = resp.data.knockoff_status;

                $scope.value_date = resp.data.value_date;
                $scope.payment_date = resp.data.payment_date;
                $scope.lbltransact_particulars = resp.data.transact_particulars;
                $scope.debit_amt = resp.data.debit_amt;
                $scope.credit_amt = resp.data.credit_amt;
                $scope.created_by = resp.data.created_by;
                $scope.lblchq_no = resp.data.chq_no;
                $scope.bankrepaytransc_gid = resp.data.bankrepaytransc_gid;
                $scope.repay_transaction_date = resp.data.repay_transaction_date;
                $scope.principal = resp.data.principal;
                $scope.lblremainingamount = resp.data.remaining_amount;

                $scope.normal_interest = resp.data.normal_interest;
                $scope.forfeiture_waiver = resp.data.forfeiture_waiver;
                $scope.repay_remarks = resp.data.repay_remarks;
                $scope.repayment_type = resp.data.repayment_type;
                $scope.penal_interest = resp.data.penal_interest;
                $scope.instrument = resp.data.instrument;
                $scope.old_dpd = resp.data.old_dpd;
                $scope.dpd = resp.data.dpd;
                $scope.account_code = resp.data.account_code;
                $scope.interest_tds = resp.data.interest_tds;
                $scope.penal_interest_tds = resp.data.penal_interest_tds;
                $scope.repay_knockoff_status = resp.data.repay_knockoff_status;
                $scope.manualknockoff_remarks = resp.data.manualknockoff_remarks;
            });

            var url = 'api/MyUnreconciliationManagement/GetAssignedHistoryView';
            var param = {
                banktransc_gid: banktransc_gid
            }
            SocketService.getparams(url, param).then(function (resp) {
                $scope.assignedview_list = resp.data.assignedview_list;
            });

            var url = 'api/MyUnreconciliationManagement/GetTransferredHistoryView';
            var param = {
                banktransc_gid: banktransc_gid
            }
            SocketService.getparams(url, param).then(function (resp) {
                $scope.transferview_list = resp.data.transferview_list;
            });
            var params = {
                banktransc_gid: banktransc_gid
            }
            var url = 'api/UnreconciliationManagement/GetUnreconTransactionList';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.unrecontransactionlist = resp.data.unrecontransactionlist;

            });
            
        }

        $scope.remarks = function (transaction_remarks) {
            var modalInstance = $modal.open({
                templateUrl: '/remarks.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.transaction_remarks = transaction_remarks;

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
            }
        }

        $scope.Back = function () {
            $state.go('app.brsTrnMyUnReconClosedSummary');
        }
    }
})();