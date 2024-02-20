(function () {
    'use strict';

    angular
        .module('angle')
        .controller('TrnHRLoanHRPaymentApprovalsViewController', TrnHRLoanHRPaymentApprovalsViewController);

    TrnHRLoanHRPaymentApprovalsViewController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams','DownloaddocumentService','cmnfunctionService'];

    function TrnHRLoanHRPaymentApprovalsViewController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams,DownloaddocumentService,cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'TrnHRLoanHRPaymentApprovalsViewController';
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
                $scope.approvedtenure_startdate = resp.data.approvedtenure_startdate;
                $scope.approvedtenure_enddate = resp.data.approvedtenure_enddate;

            });

            // var url = 'api/MstHRLoanRequest/GetEmployeeDetails';
            // lockUI();
            // SocketService.get(url).then(function (resp) {               
            //     $scope.txtentityname = resp.data.entity_name;                
            //     unlockUI();
            // });
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
            var url = 'api/MstHRLoanApprovalsApproved/GetHRloanApprovalSummary';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.Approvalsummary = resp.data.ApprovedApprovalsummary;
            });
            var url = 'api/MstHRLoanApprovalsRejected/GetHRloanApprovalSummary';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.Approvalsummary = resp.data.RejectedApprovalsummary;
            });
            var param =
            {
                request_gid: request_gid
            }            
            var url = 'api/TrnHRLoanHRVerifications/GetUploadList';
               SocketService.getparams(url, param).then(function (resp) {
                $scope.HRDocument_list = resp.data.HRDocument_list;
                });
                var url = 'api/MstHRLoanDrmApproval/GetUploadDocumentsList';
                SocketService.getparams(url, params).then(function (resp) {
                   $scope.hrReuploadDocument_list = resp.data.HrReuploadDocument_list;
                    });
            var params =
                {
                    request_gid: $scope.request_gid
                }
            var url = 'api/TrnHRLoanHRVerifications/HRLoanPaymentDocumentList';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.PaymentDocument_list = resp.data.PaymentDocument_list;
            });  
            var params =
            {
                request_gid: $scope.request_gid
            }
            var url = 'api/MstHRLoanRequest/GetUploadDocumentsList';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.upload_list = resp.data.upload_list;
            });

        }
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
        //Document Multiple Add
        $scope.doc_downloads = function (val1, val2) {
            DownloaddocumentService.Downloaddocument(val1, val2);
        }

        $scope.documentviewer = function (val1, val2) {
            lockUI();
            var IsValidExtension = cmnfunctionService.fnCheckValidDocType(val2, "DocumentViewerFormat");
            if (IsValidExtension == false) {
                Notify.alert("View is not supported for this format..!", {
                    status: 'danger',
                    pos: 'top-center',
                    timeout: 3000
                });
            unlockUI();
            return false;
            }
            DownloaddocumentService.DocumentViewer(val1, val2);
        }

        $scope.Back = function () {

            $state.go('app.TrnHRLoanHRPaymentSummary');
        }
    }
})();


