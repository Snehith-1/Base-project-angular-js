// JavaScript source code
(function () {
    'use strict';

    angular
        .module('angle')
        .controller('BrsTrnUnreconRepaymentMatchedViewController', BrsTrnUnreconRepaymentMatchedViewController);

    BrsTrnUnreconRepaymentMatchedViewController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', 'cmnfunctionService'];

    function BrsTrnUnreconRepaymentMatchedViewController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, cmnfunctionService) {

        var vm = this;
        var searchObject = cmnfunctionService.decryptURL($location.search().hash);
        $scope.lspage = searchObject.lspage;
        var lspage = $scope.lspage;
        vm.title = 'BrsTrnUnreconRepaymentMatchedViewController';

        /*    var banktransc_gid = $location.search().banktransc_gid;*/

        var searchObject = cmnfunctionService.decryptURL($location.search().hash);
        var banktransc_gid = searchObject.banktransc_gid;

        activate();
        function activate() {


            var url = 'api/UnreconciliationManagement/GetAllocatedDetail';
            var param = {
                banktransc_gid: banktransc_gid

            }

            SocketService.getparams(url, param).then(function (resp) {
                $scope.lblbanktransc_gid = banktransc_gid;
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
                $scope.manualknockoff_remarks = resp.data.manualknockoff_remarks;
                $scope.lblchq_no = resp.data.chq_no;
                $scope.lblrm_remarks = resp.data.rm_remarks;
                $scope.lbltransact_particulars = resp.data.transact_particulars;
                $scope.rmsendback_on = resp.data.rmsendback_on;
                $scope.sendback_reason = resp.data.sendback_reason;
                $scope.assignedrm_gid = resp.data.assignedrm_gid;
                $scope.assigned_rm = resp.data.assigned_rm;
                $scope.lblremainingamount = resp.data.remaining_amount;

            });
            var url = 'api/UnreconciliationManagement/GetReassignemployeeLog';
            lockUI();
            SocketService.getparams(url, param).then(function (resp) {
                $scope.reassignemployee_list = resp.data.reassignemployee_list;
                unlockUI();
            });
            var url = 'api/UnreconciliationManagement/GetAssignedHistory';
            var param = {
                banktransc_gid: banktransc_gid
            }
            SocketService.getparams(url, param).then(function (resp) {
                $scope.assignedlist = resp.data.assignedlist;
            });

            var url = 'api/UnreconciliationManagement/GetTransferredHistory';
            var param = {
                banktransc_gid: banktransc_gid
            }
            SocketService.getparams(url, param).then(function (resp) {
                $scope.transferlist = resp.data.transferlist;
            });
            var url = 'api/UnreconciliationManagement/GetUnReconciliationClosed';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.banktransc_gid = resp.data.banktransc_gid;
                $scope.BankAlertUnreconciliationcredit_list = resp.data.BankAlertUnreconciliationcredit_list;
                // $scope.Manualknockoff_remarks = $scope.BankAlertUnreconciliationcredit_list.manualknockoff_remarks;
                unlockUI();
            });
            var params = {
                banktransc_gid: banktransc_gid
            }
            var url = 'api/UnreconciliationManagement/GetUnreconBankTransactionList';
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
            $state.go('app.brsTrnRepaymentReconcillation');


        }
    }
})();