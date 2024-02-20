(function () {
    'use strict';

    angular
        .module('angle')
        .controller('TrnHRLoanHRPaymentSummaryController', TrnHRLoanHRPaymentSummaryController);

    TrnHRLoanHRPaymentSummaryController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams','cmnfunctionService'];

    function TrnHRLoanHRPaymentSummaryController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams,cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'TrnHRLoanHRPaymentSummaryController';

        activate();

        function activate() {
            lockUI();
            var url = 'api/TrnHRLoanHRPayment/GetHRloanHRheadPaymentDetails';
            SocketService.get(url).then(function (resp) {
                $scope.paymentdetails_list = resp.data.payment_summary;
                unlockUI();
            });
            var url = 'api/TrnHRLoanHRPayment/GetHRloanHRheadPaymentDetailscount';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.lspendinghrpayment_count = resp.data.pendinghrpayment_count;
                $scope.lscompletedhrpayment_count = resp.data.completedhrpayment_count;               
          });
        }

        $scope.loan_approval = function (request_gid, employee_gid) {
            $location.url('app/TrnHRLoanHRPaymentApprovals?hash=' + cmnfunctionService.encryptURL('request_gid=' + request_gid + '&employee_gid=' + employee_gid));
        }

        $scope.myapproval_loans = function () {
            $state.go('app.TrnHRLoanHRPaymentSummary');
        }

        $scope.approved_loans = function () {
            $state.go('app.TrnHRLoanHRPaymentApprovedSummary');
        }

        $scope.rejected_loans = function () {
            $state.go('app.TrnHRLoanHRPaymentRejectedSummary');
        }

        $scope.viewrequests = function (request_gid) {

            $location.url('app/TrnHRLoanHRPaymentApprovalsView?hash=' + cmnfunctionService.encryptURL('request_gid=' + request_gid));

        }
        // $scope.applcreation_view = function (application_gid) {
        //     $location.url('app/MstApplicationCreationView?application_gid=' + application_gid + '&lstab=BusinessApproval');
        // }

    }
})();
