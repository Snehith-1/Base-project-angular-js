// JavaScript source code
(function () {
    'use strict';

    angular
        .module('angle')
        .controller('brsTrnPartialMatchedViewController', brsTrnPartialMatchedViewController);

    brsTrnPartialMatchedViewController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams','cmnfunctionService'];

    function brsTrnPartialMatchedViewController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, cmnfunctionService) {

        var vm = this;
        var searchObject = cmnfunctionService.decryptURL($location.search().hash);
        $scope.lspage = searchObject.lspage;
        var lspage = $scope.lspage;
        vm.title = 'brsTrnPartialMatchedViewController';

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
                $scope.lbltransact_particulars = resp.data.transact_particulars;
                $scope.lblchq_no = resp.data.chq_no;

            });
          
            var url = 'api/RepaymentReconcillation/GetLMSPartialhistory';
            var param = {
                banktransc_gid: banktransc_gid

            }
            lockUI();
            SocketService.getparams(url,param).then(function (resp) {
                $scope.repayment_lmshistory = resp.data.repayment_lmshistory;
                unlockUI();

            });
        }

       
        $scope.Back = function () {
            if (lspage == "CreditPartial") {
                $state.go('app.brsTrnCreditPartialMatched');
            }
            else if (lspage == "DebitPartial") {
                $state.go('app.brsTrnDebitPartialMatched');
            }
                     
        }
    }
})();