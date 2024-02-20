(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstHRLoanViewRequestController', MstHRLoanViewRequestController);

    MstHRLoanViewRequestController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', 'DownloaddocumentService','cmnfunctionService'];

    function MstHRLoanViewRequestController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, DownloaddocumentService,cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstHRLoanViewRequestController';
        
        var lsFunctionalhead_gid, lsdepartment_gid, lsreportingmgr_gid,lsentity_gid;
        $scope.request_gid = cmnfunctionService.decryptURL($location.search().hash).request_gid;
        var request_gid = $scope.request_gid;
       
        activate();
        function activate() {          

            var url = 'api/MstHRLoanRequest/GetEmployeeDetails';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.txtemp = resp.data.employee_name;
                $scope.txtrole = resp.data.role;
                $scope.txtdept = resp.data.department;

                $scope.txtofficialmail = resp.data.official_mailid;
                $scope.txtofficialmobile = resp.data.official_mobileno;
                $scope.txtpersonalmail = resp.data.pers_mailid;
                $scope.txtpersonalmobile = resp.data.pers_mobileno;
                $scope.txtentityname = resp.data.entity_name;
                lsentity_gid = resp.data.entity_gid;

                $scope.txtreporting_manager = resp.data.reporting_manager;
                $scope.txtfunctional_head = resp.data.functional_head;
                lsFunctionalhead_gid = resp.data.functionalhead_gid;
                lsdepartment_gid = resp.data.department_gid;
                lsreportingmgr_gid = resp.data.reportingmgr_gid;

               unlockUI();
            });

            var params = {
                request_gid: request_gid
            }
            var url = 'api/MstHRLoanRequest/EditHRLoanRequest';
            SocketService.getparams(url, params).then(function (resp) {               
                $scope.fintype = resp.data.fintype_name;               
                $scope.amount = resp.data.amount;
                $scope.purpose = resp.data.purpose_name;                
                $scope.severity = resp.data.severity_name;                
                $scope.tenure = resp.data.tenure;
                $scope.reason = resp.data.request_reason;
                $scope.txtinterest = resp.data.interest;
                
                unlockUI();
            });
            var url = 'api/TrnHRLoanHRVerifications/HRLoanGetApprovedInterest';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.interest = resp.data.approved_interest;
                $scope.approvedtenure = resp.data.approved_tenure;
            });
            var params =
            {
                request_gid: request_gid
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
            var url = 'api/TrnHRLoanHRVerifications/termsandcondtnview';
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.termsandcont_list = resp.data.hrtermsandconditions_list;
                });
            var url = 'api/MstHRLoanRequest/GetUploadDocumentsList';
                    SocketService.getparams(url, params).then(function (resp) {
                        $scope.upload_list = resp.data.upload_list;
                    });
            var url = 'api/MstHRLoanDrmApproval/GetDRMRaiseQuery';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.drmraisequery_list = resp.data.drmraisequery_list;
            });
           
            var url = 'api/MstHRLoanDrmApproval/GetFHRaiseQuery';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.fhraisequery_list = resp.data.fhraisequery_list;
            });

            var url = 'api/MstHRLoanDrmApproval/GetHRRaiseQuery';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.hrraisequery_list = resp.data.hrraisequery_list;
            });
            var url = 'api/TrnHRLoanHRVerifications/GetManagerRaiseQuery';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.mangraisequery_list = resp.data.mangraisequery_list;
            });
            var url = 'api/MstHRLoanDrmApproval/GetUploadDocumentsList';
            SocketService.getparams(url, params).then(function (resp) {
           $scope.hrReuploadDocument_list = resp.data.HrReuploadDocument_list;
            });
            var url = 'api/MstHRLoanDrmApproval/GetDropDown';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.applicationadddoc_list = resp.data.hrdocument_list;
            });
            var param =
            {
                request_gid: request_gid
            }
            var url = 'api/TrnHRLoanHRVerifications/GetUploadList';
               SocketService.getparams(url, param).then(function (resp) {
                $scope.HRDocument_list = resp.data.HRDocument_list;
                });
            var params =
                {
                    request_gid: $scope.request_gid
                }
            var url = 'api/TrnHRLoanHRVerifications/HRLoanPaymentDocumentList';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.PaymentDocument_list = resp.data.PaymentDocument_list;
                });
            
        }  


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
       

        $scope.back = function () {
            $state.go('app.MstHRLoanRaiseRequest');
        }
        $scope.ok = function () {
            $modalInstance.close('closed');
        };
        $scope.view_drmquerydesc = function (query_description, queryresponse_remarks, query_responseby) {
            var modalInstance = $modal.open({
                templateUrl: '/drmqueryview.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.lblquery_desc = query_description;
                $scope.lblqueryresponse_remarks = queryresponse_remarks;
                $scope.lblquery_responseby = query_responseby;
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
            }
        }

        $scope.view_fhquerydesc = function (query_description, queryresponse_remarks, query_responseby) {
            var modalInstance = $modal.open({
                templateUrl: '/fhqueryview.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.lblquery_desc = query_description;
                $scope.lblqueryresponse_remarks = queryresponse_remarks;
                $scope.lblquery_responseby = query_responseby;
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
            }
        }

        $scope.view_hrquerydesc = function (query_description, queryresponse_remarks, query_responseby) {
            var modalInstance = $modal.open({
                templateUrl: '/hrqueryview.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.lblquery_desc = query_description;
                $scope.lblqueryresponse_remarks = queryresponse_remarks;
                $scope.lblquery_responseby = query_responseby;
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
            }
        }

        $scope.view_mangquerydesc = function (query_description, queryresponse_remarks, query_responseby) {
            var modalInstance = $modal.open({
                templateUrl: '/mangqueryview.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.lblquery_desc = query_description;
                $scope.lblqueryresponse_remarks = queryresponse_remarks;
                $scope.lblquery_responseby = query_responseby;
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
            }
        }
        
       
    }
})();