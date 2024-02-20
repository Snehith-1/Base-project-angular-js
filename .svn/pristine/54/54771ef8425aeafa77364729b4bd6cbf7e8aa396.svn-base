(function () {
    'use strict';

    angular
        .module('angle')
        .controller('TrnHRLoanHRPaymentApprovalsController', TrnHRLoanHRPaymentApprovalsController);

    TrnHRLoanHRPaymentApprovalsController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams','cmnfunctionService'];

    function TrnHRLoanHRPaymentApprovalsController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams,cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'TrnHRLoanHRPaymentApprovalsController';
        $scope.request_gid = cmnfunctionService.decryptURL($location.search().hash).request_gid;
        var request_gid = $scope.request_gid;

        lockUI();
        activate();

        function activate() {
            lockUI();
            var params = {
                request_gid: $scope.request_gid,
            }

            var url = 'api/MstHRLoanDrmApproval/DaGetHRloanRequestviewDetails';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.lblapplref_no = resp.data.request_refno;
                $scope.lblEmployee_name = resp.data.employee_name;
                $scope.lblRole = resp.data.employee_role;
                $scope.lblDepartment = resp.data.department_name;
                $scope.lblDRM = resp.data.reporting_mgr;
                $scope.lblFH = resp.data.functional_head;
                $scope.lblHR = resp.data.hr_head;
                $scope.cbofintype = resp.data.fintype_name;
                $scope.cbopurpose = resp.data.purpose_name;
                $scope.cboseverity = resp.data.severity_name;
                $scope.cbotenure = resp.data.tenure;
                $scope.txtamount = resp.data.amount;
                $scope.request_status = resp.data.request_status;
                $scope.txtinterest = resp.data.interest;
                $scope.txtentityname = resp.data.entity_name;
                unlockUI();
            });
            var url = 'api/TrnHRLoanHRVerifications/HRLoanGetApprovedInterest';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.interest = resp.data.approved_interest;
                $scope.approvedtenure = resp.data.approved_tenure;
            });
            
            var url = 'api/MstHRLoanHRMappingApprovals/GetApproverName';
            SocketService.get(url).then(function (resp) {
                unlockUI();                  
                $scope.lblapprover_name = resp.data.employee_name;                
            });
            var params = {
                request_gid: $scope.request_gid,
            }
            var url = 'api/MstHRLoanDrmApproval/GetHRloanApprovalSummary';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.Approvalsummary = resp.data.Approvalsummary;
            });
            
        }


        $scope.Back = function () {
            //if (lspage == 'HRApproval') {
            //    $state.go('app.AprHRLoanHRAdvanceApprovalsSummmary');
            //}
            //else if (lspage == 'HRApproved') {
            //    $state.go('app.AprHRLoanHRAdvanceApprovedSummmary');
            //} 
            //else if (lspage == 'HRReject') {
            //    $state.go('app.AprHRLoanHRAdvanceRejectedSummmary');
            //}           
            //else {

            //} 
            $state.go('app.TrnHRLoanHRPaymentSummary');
        }


        $scope.hrpayment_approve = function () {

            var params = {

                hrpayment_remarks: $scope.txthrpayment_remarks,
                request_gid: $scope.request_gid,
            }
            var url = 'api/TrnHRLoanHRPayment/PostHrLoanHRPaymentUpdate';
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {

                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    $state.go('app.TrnHRLoanHRPaymentSummary');
                    activate();
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

