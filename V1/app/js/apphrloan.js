(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AprHRLoanHRAdvanceApprovalsController', AprHRLoanHRAdvanceApprovalsController);

        AprHRLoanHRAdvanceApprovalsController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams','cmnfunctionService'];

    function AprHRLoanHRAdvanceApprovalsController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams,cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AprHRLoanHRAdvanceApprovalsController';
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
            var url = 'api/MstHRLoanApprovalsApproved/GetHRloanApprovalSummary';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.Approvalsummary = resp.data.ApprovedApprovalsummary;
            });
            var url = 'api/MstHRLoanApprovalsRejected/GetHRloanApprovalSummary';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.Approvalsummary = resp.data.RejectedApprovalsummary;
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
            $state.go('app.AprHRLoanHRAdvanceApprovalsSummary');
        }

        $scope.create_raisequery = function (request_status) {
            var modalInstance = $modal.open({
                templateUrl: '/raisequery.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                
                $scope.raisequery_add = function () {
                   /* alert(request_status );*/
                    if (request_status == 'DRM Pending' || request_status == 'Query Raised By DRM'){
                   var params = {
                        query_title: $scope.txtquery_title,
                        query_description: $scope.txtquery_desc,                                            
                        request_gid: request_gid,                       
                        
                    }
                    var url = 'api/MstHRLoanDrmApproval/PostDRMRaiseQuery';
                    } else if (request_status == 'FH Pending' || request_status == 'Query Raised By FH') {
                    var params = {
                        query_title: $scope.txtquery_title,
                        query_description: $scope.txtquery_desc,                                             
                        request_gid: request_gid,                                           
                     }
                     var url = 'api/MstHRLoanDrmApproval/PostFHRaiseQuery';
                    }
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            activate();
                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'info',
                                pos: 'top-center',
                                timeout: 3000
                            });
                        }
                    });

                    $modalInstance.close('closed');
                }           
            }
        }

        $scope.view_drmquerydesc = function (query_description, queryresponse_remarks, query_responseby) {
            var modalInstance = $modal.open({
                templateUrl: '/drmqueryDescriptionView.html',
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
                templateUrl: '/fhqueryDescriptionView.html',
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

        $scope.approve = function () {
       
               var params = {

                   drm_remarks: $scope.txtdrm_remarks,
                   request_gid: $scope.request_gid,                 
               }
               var url = 'api/MstHRLoanDrmApproval/PostHrLoanDRMApprovalUpdate';
               lockUI();
               SocketService.post(url, params).then(function (resp) {
                   unlockUI();
                   if (resp.data.status == true) {

                       Notify.alert(resp.data.message, {
                           status: 'success',
                           pos: 'top-center',
                           timeout: 3000
                       });
                       $state.go('app.AprHRLoanHRAdvanceApprovalsSummary');
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

        $scope.reject = function () {

            var params = {

                drm_remarks: $scope.txtdrm_remarks,
                request_gid: $scope.request_gid,
            }
            var url = 'api/MstHRLoanDrmApproval/PostHrLoanDRMRejectUpdate';
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {

                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    $state.go('app.AprHRLoanHRAdvanceApprovalsSummary');
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
        $scope.fh_approve = function () {

            var params = {

                fh_remarks: $scope.txtdrm_remarks,
                request_gid: $scope.request_gid,
            }
            var url = 'api/MstHRLoanDrmApproval/PostHrLoanFHApprovalUpdate';
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {

                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    $state.go('app.AprHRLoanHRAdvanceApprovalsSummary');
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

        $scope.fh_reject = function () {

            var params = {

                fh_remarks: $scope.txtdrm_remarks,
                request_gid: $scope.request_gid,
            }
            var url = 'api/MstHRLoanDrmApproval/PostHrLoanFHRejectUpdate';
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {

                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    $state.go('app.AprHRLoanHRAdvanceApprovalsSummary');
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
       
        $scope.view_querydesc = function (appcreditquery_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/queryDescriptionView.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params =
                 {
                     appcreditquery_gid: appcreditquery_gid
                 }
                //var url = '';
                //lockUI();
                //SocketService.getparams(url, params).then(function (resp) {
                //    unlockUI();
                //    $scope.lblquery_desc = resp.data.querydesc;

                //});

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };


            }

        }

    }
})();


(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AprHRLoanHRAdvanceApprovalsSummaryController', AprHRLoanHRAdvanceApprovalsSummaryController);

        AprHRLoanHRAdvanceApprovalsSummaryController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams','cmnfunctionService'];

    function AprHRLoanHRAdvanceApprovalsSummaryController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams,cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AprHRLoanHRAdvanceApprovalsSummaryController';
       
        activate();

        function activate() {
            lockUI();
            var url = 'api/MstHRLoanDrmApproval/GetHRloanRequestDetails';
               SocketService.get(url).then(function (resp) {               
                   $scope.requestdetails_list = resp.data.requestsummary;
                   unlockUI();
               });
      
            var url = 'api/MstHRLoanDrmApproval/GetHRloanRequestDetailscount';
              SocketService.get(url).then(function (resp) {
                  unlockUI();
                  $scope.lspendingapprovals_count = resp.data.pendingapprovals_count;
                  $scope.lsapprovedapprovals_count = resp.data.approvedapprovals_count;                  
                  $scope.lsrejectedapprovals_count = resp.data.rejectedapprovals_count;                  
            });
        }
       
        $scope.loan_approval = function (request_gid, employee_gid) {
            $location.url('app/AprHRLoanHRAdvanceApprovals?hash=' + cmnfunctionService.encryptURL('request_gid=' + request_gid + '&employee_gid=' + employee_gid ));
        }

        $scope.myapproval_loans = function () {
            $state.go('app.AprHRLoanHRAdvanceApprovalsSummary');
        }

        $scope.approved_loans = function () {
            $state.go('app.AprHRLoanHRAdvanceApprovedSummary');
        }

        $scope.rejected_loans = function () {
            $state.go('app.AprHRLoanHRAdvanceRejectedSummary');
        }        
        $scope.viewrequests = function (request_gid) {
            $location.url('app/AprHRLoanHRAdvanceApprovalsView?hash=' + cmnfunctionService.encryptURL('request_gid=' + request_gid));
        }
       
    }
})();

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AprHRLoanHRAdvanceApprovalsViewController', AprHRLoanHRAdvanceApprovalsController);

    AprHRLoanHRAdvanceApprovalsController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams','DownloaddocumentService','cmnfunctionService'];

    function AprHRLoanHRAdvanceApprovalsController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams,DownloaddocumentService,cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AprHRLoanHRAdvanceApprovalsViewController';
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
            var url = 'api/MstHRLoanApprovalsApproved/GetHRloanApprovalSummary';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.Approvalsummary = resp.data.ApprovedApprovalsummary;
            });
            var url = 'api/MstHRLoanApprovalsRejected/GetHRloanApprovalSummary';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.Approvalsummary = resp.data.RejectedApprovalsummary;
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
            var params =
            {
                request_gid: request_gid
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

        $scope.Back = function () {
          
            $state.go('app.AprHRLoanHRAdvanceApprovalsSummary');
        }

        $scope.view_fhquerydesc = function (query_description, queryresponse_remarks, query_responseby) {
            var modalInstance = $modal.open({
                templateUrl: '/fhqueryDescriptionView.html',
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

        $scope.view_drmquerydesc = function (query_description, queryresponse_remarks, query_responseby) {
            var modalInstance = $modal.open({
                templateUrl: '/drmqueryDescriptionView.html',
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

 }
})();


        
(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AprHRLoanHRAdvanceApprovedSummaryController', AprHRLoanHRAdvanceApprovedSummaryController);

        AprHRLoanHRAdvanceApprovedSummaryController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams','cmnfunctionService'];

    function AprHRLoanHRAdvanceApprovedSummaryController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams,cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AprHRLoanHRAdvanceApprovedSummaryController';
        lockUI();
        activate();

        function activate() {
        
            //var url = '';
            //   SocketService.get(url).then(function (resp) {                   
            //       $scope.applicationadd_list = resp.data.applicationadd_list;
            //   });
              
            //var url = '';
            //   SocketService.get(url).then(function (resp) {
            //       unlockUI();
            //       $scope.newhrapprovals_count = resp.data.newhrapprovals_count;
            //       $scope.rejectedapprovals_count = resp.data.rejectedapprovals_count;                  
            //       $scope.approvedapprovals_count = resp.data.approvedapprovals_count;
            //       $scope.lstotalcount = resp.data.lstotalcount;                   
            //});
        }       
      
        $scope.loan_approval = function (request_gid, employee_gid) {
            $location.url('app/AprHRLoanHRAdvanceApprovals?hash=' + cmnfunctionService.encryptURL('request_gid=' + request_gid + '&employee_gid=' + employee_gid + '&lspage=HRApproved' + '&lsflag=N'));         
        }

        $scope.myapproval_loans = function () {
            $state.go('app.AprHRLoanHRAdvanceApprovalsSummary');
        }

        $scope.approved_loans = function () {
            $state.go('app.AprHRLoanHRAdvanceApprovedSummary');
        }


        $scope.rejected_loans = function () {
            $state.go('app.AprHRLoanHRAdvanceRejectedSummary');
        }
        
        // $scope.applcreation_view = function (application_gid) {
        //     $location.url('app/MstApplicationCreationView?application_gid=' + application_gid + '&lstab=BusinessApproved');
        // }
       
    }
})();


(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AprHRLoanHRAdvanceApprovedViewController', AprHRLoanHRAdvanceApprovalsController);

    AprHRLoanHRAdvanceApprovalsController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams','DownloaddocumentService','cmnfunctionService'];

    function AprHRLoanHRAdvanceApprovalsController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, DownloaddocumentService,cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AprHRLoanHRAdvanceApprovedViewController';
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
            var url = 'api/MstHRLoanApprovalsApproved/GetHRloanApprovalSummary';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.Approvalsummary = resp.data.ApprovedApprovalsummary;
            });
            var url = 'api/MstHRLoanApprovalsRejected/GetHRloanApprovalSummary';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.Approvalsummary = resp.data.RejectedApprovalsummary;
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
            var params =
            {
                request_gid: request_gid
            }
            var url = 'api/MstHRLoanRequest/GetUploadDocumentsList';
                    SocketService.getparams(url, params).then(function (resp) {
                        $scope.upload_list = resp.data.upload_list;
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
            $state.go('app.AprHRLoanHRAdvanceApprovedSummary');
        }

        $scope.view_fhquerydesc = function (query_description, queryresponse_remarks, query_responseby) {
            var modalInstance = $modal.open({
                templateUrl: '/fhqueryDescriptionView.html',
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

        $scope.view_drmquerydesc = function (query_description, queryresponse_remarks, query_responseby) {
            var modalInstance = $modal.open({
                templateUrl: '/drmqueryDescriptionView.html',
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



(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AprHRLoanHRAdvanceAprrovedSummaryController', AprHRLoanHRAdvanceAprrovedSummaryController);

        AprHRLoanHRAdvanceAprrovedSummaryController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams','cmnfunctionService'];

    function AprHRLoanHRAdvanceAprrovedSummaryController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams,cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AprHRLoanHRAdvanceAprrovedSummaryController';
        lockUI();
        activate();

        function activate() {
            lockUI();
            var url = 'api/MstHRLoanApprovalsApproved/GetHRloanRequestDetails';
            SocketService.get(url).then(function (resp) {
                $scope.requestdetails_list = resp.data.Approvedrequestsummary;
                unlockUI();
            });
            var url = 'api/MstHRLoanDrmApproval/GetHRloanRequestDetailscount';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.lspendingapprovals_count = resp.data.pendingapprovals_count;
                $scope.lsapprovedapprovals_count = resp.data.approvedapprovals_count;                  
                $scope.lsrejectedapprovals_count = resp.data.rejectedapprovals_count;               
          });
        }       
      
        $scope.loan_approval = function (request_gid, employee_gid) {
            $location.url('app/AprHRLoanHRAdvanceApprovals?hash=' + cmnfunctionService.encryptURL('request_gid=' + request_gid + '&employee_gid=' + employee_gid));         
        }

        $scope.myapproval_loans = function () {
            $state.go('app.AprHRLoanHRAdvanceApprovalsSummary');
        }

        $scope.approved_loans = function () {
            $state.go('app.AprHRLoanHRAdvanceApprovedSummary');
        }


        $scope.rejected_loans = function () {
            $state.go('app.AprHRLoanHRAdvanceRejectedSummary');
        }
        $scope.viewrequests = function (request_gid) {
            $location.url('app/AprHRLoanHRAdvanceApprovedView?hash=' + cmnfunctionService.encryptURL('request_gid=' + request_gid));
        }
       
    }
})();


(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AprHRLoanHRAdvanceRejectedSummaryController', AprHRLoanHRAdvanceRejectedSummaryController);

        AprHRLoanHRAdvanceRejectedSummaryController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams','cmnfunctionService'];

    function AprHRLoanHRAdvanceRejectedSummaryController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams,cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AprHRLoanHRAdvanceRejectedSummaryController';
        lockUI();
        activate();

        function activate() { 
            lockUI();
            var url = 'api/MstHRLoanApprovalsRejected/GetHRloanRequestDetails';
            SocketService.get(url).then(function (resp) {
                $scope.requestdetails_list = resp.data.Rejectedrequestsummary;
                unlockUI();
            });
            var url = 'api/MstHRLoanDrmApproval/GetHRloanRequestDetailscount';
              SocketService.get(url).then(function (resp) {
                  unlockUI();
                  $scope.lspendingapprovals_count = resp.data.pendingapprovals_count;
                  $scope.lsapprovedapprovals_count = resp.data.approvedapprovals_count;                  
                  $scope.lsrejectedapprovals_count = resp.data.rejectedapprovals_count;                 
            });
        }       
      
        $scope.loan_approval = function (request_gid, employee_gid) {
            $location.url('app/AprHRLoanHRAdvanceApprovals?hash=' + cmnfunctionService.encryptURL('request_gid=' + request_gid + '&employee_gid=' + employee_gid));         
        }

        $scope.myapproval_loans = function () {
            $state.go('app.AprHRLoanHRAdvanceApprovalsSummary');
        }
        $scope.approved_loans = function () {
            $state.go('app.AprHRLoanHRAdvanceApprovedSummary');
        }

        $scope.rejected_loans = function () {
            $state.go('app.AprHRLoanHRAdvanceRejectedSummary');
        }       
        $scope.viewrequests = function (request_gid) {
            $location.url('app/AprHRLoanHRAdvanceRejectedView?hash=' + cmnfunctionService.encryptURL('request_gid=' + request_gid));
        }
       
    }
})();


(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AprHRLoanHRAdvanceRejectedViewController', AprHRLoanHRAdvanceApprovalsController);

    AprHRLoanHRAdvanceApprovalsController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams','DownloaddocumentService','cmnfunctionService'];

    function AprHRLoanHRAdvanceApprovalsController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams,DownloaddocumentService,cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AprHRLoanHRAdvanceRejectedViewController';
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
            var url = 'api/MstHRLoanApprovalsApproved/GetHRloanApprovalSummary';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.Approvalsummary = resp.data.ApprovedApprovalsummary;
            });
            var url = 'api/MstHRLoanApprovalsRejected/GetHRloanApprovalSummary';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.Approvalsummary = resp.data.RejectedApprovalsummary;
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
            var params =
            {
                request_gid: request_gid
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

            $state.go('app.AprHRLoanHRAdvanceRejectedSummary');
        }

        $scope.view_fhquerydesc = function (query_description, queryresponse_remarks, query_responseby) {
            var modalInstance = $modal.open({
                templateUrl: '/fhqueryDescriptionView.html',
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

        $scope.view_drmquerydesc = function (query_description, queryresponse_remarks, query_responseby) {
            var modalInstance = $modal.open({
                templateUrl: '/drmqueryDescriptionView.html',
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



(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AprHRLoanHRHeadApprovalsController', AprHRLoanHRHeadApprovalsController);

        AprHRLoanHRHeadApprovalsController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams','cmnfunctionService'];

    function AprHRLoanHRHeadApprovalsController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams,cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AprHRLoanHRHeadApprovalsController';
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
            $state.go('app.AprHRLoanHRHeadApprovalsSummary');
        }

        $scope.create_raisequery = function () {
            var modalInstance = $modal.open({
                templateUrl: '/raisequery.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                
                $scope.raisequery_add = function () {                  
                   var params = {
                        query_title: $scope.txtquery_title,
                        query_description: $scope.txtquery_desc,
                        // close_remarks: $scope.txtclose_remarks,                       
                        request_gid: request_gid,                       
                        
                    }
                    var url = 'api/MstHRLoanDrmApproval/PostHRRaiseQuery';               
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            activate();
                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'info',
                                pos: 'top-center',
                                timeout: 3000
                            });
                        }
                    });

                    $modalInstance.close('closed');
                }           
            }
        }
        $scope.view_drmquerydesc = function (query_description, queryresponse_remarks, query_responseby) {
            var modalInstance = $modal.open({
                templateUrl: '/drmqueryDescriptionView.html',
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
                templateUrl: '/fhqueryDescriptionView.html',
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
                templateUrl: '/hrqueryDescriptionView.html',
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
       

        $scope.hr_approve = function () {
       
               var params = {

                   hrhead_remarks: $scope.txthrhead_remarks,
                   request_gid: $scope.request_gid,                 
               }
               var url = 'api/MstHRLoanDrmApproval/PostHrLoanHRApprovalUpdate';
               lockUI();
               SocketService.post(url, params).then(function (resp) {
                   unlockUI();
                   if (resp.data.status == true) {

                       Notify.alert(resp.data.message, {
                           status: 'success',
                           pos: 'top-center',
                           timeout: 3000
                       });
                       $state.go('app.AprHRLoanHRHeadApprovalsSummary');
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

        $scope.hr_reject = function () {

            var params = {

                hrhead_remarks: $scope.txthrhead_remarks,
                request_gid: $scope.request_gid,
            }
            var url = 'api/MstHRLoanDrmApproval/PostHrLoanHRRejectUpdate';
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {

                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    $state.go('app.AprHRLoanHRHeadApprovalsSummary');
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


(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AprHRLoanHRHeadApprovalsSummaryController', AprHRLoanHRHeadApprovalsSummaryController);

        AprHRLoanHRHeadApprovalsSummaryController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams','cmnfunctionService'];

    function AprHRLoanHRHeadApprovalsSummaryController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams,cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AprHRLoanHRHeadApprovalsSummaryController';
       
        activate();

        function activate() {
            lockUI();
            var url = 'api/MstHRLoanDrmApproval/GetHRloanHRheadRequestDetails';
               SocketService.get(url).then(function (resp) {               
                   $scope.requestdetails_list = resp.data.requestsummary;
                   unlockUI();
               });
      
               var url = 'api/MstHRLoanDrmApproval/GetHRloanHRheadRequestDetailscount';
               SocketService.get(url).then(function (resp) {
                   unlockUI();
                   $scope.lshrpendingapprovals_count = resp.data.hrpendingapprovals_count;
                   $scope.lshrapprovedapprovals_count = resp.data.hrapprovedapprovals_count;                  
                   $scope.lshrrejectedapprovals_count = resp.data.hrrejectedapprovals_count;                  
             });
        }
       
        $scope.loan_approval = function (request_gid, employee_gid) {
            $location.url('app/AprHRLoanHRHeadApprovals?hash=' + cmnfunctionService.encryptURL('request_gid=' + request_gid + '&employee_gid=' + employee_gid ));
        }

        $scope.myapproval_loans = function () {
            $state.go('app.AprHRLoanHRHeadApprovalsSummary');
        }

        $scope.approved_loans = function () {
            $state.go('app.AprHRLoanHRHeadApprovedSummary');
        }

        $scope.rejected_loans = function () {
            $state.go('app.AprHRLoanHRHeadRejectedSummary');
        }        

        $scope.viewrequests = function (request_gid) {
            $location.url('app/AprHRLoanHRHeadApprovalsView?hash=' + cmnfunctionService.encryptURL('request_gid=' + request_gid));
        }
    }
})();

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AprHRLoanHRHeadApprovalsViewController', AprHRLoanHRAdvanceApprovalsController);

    AprHRLoanHRAdvanceApprovalsController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams','DownloaddocumentService','cmnfunctionService'];

    function AprHRLoanHRAdvanceApprovalsController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams,DownloaddocumentService,cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AprHRLoanHRHeadApprovalsViewController';
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
            var url = 'api/MstHRLoanApprovalsApproved/GetHRloanApprovalSummary';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.Approvalsummary = resp.data.ApprovedApprovalsummary;
            });
            var url = 'api/MstHRLoanApprovalsRejected/GetHRloanApprovalSummary';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.Approvalsummary = resp.data.RejectedApprovalsummary;
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
            var params =
            {
                request_gid: request_gid
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

            $state.go('app.AprHRLoanHRHeadApprovalsSummary');
        }

        $scope.view_fhquerydesc = function (query_description, queryresponse_remarks, query_responseby) {
            var modalInstance = $modal.open({
                templateUrl: '/fhqueryDescriptionView.html',
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

        $scope.view_drmquerydesc = function (query_description, queryresponse_remarks, query_responseby) {
            var modalInstance = $modal.open({
                templateUrl: '/drmqueryDescriptionView.html',
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
                templateUrl: '/hrqueryDescriptionView.html',
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



(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AprHRLoanHRHeadApprovedSummaryController', AprHRLoanHRHeadApprovedSummaryController);

        AprHRLoanHRHeadApprovedSummaryController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams','cmnfunctionService'];

    function AprHRLoanHRHeadApprovedSummaryController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams,cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AprHRLoanHRHeadApprovedSummaryController';
        lockUI();
        activate();

        function activate() {
            lockUI();
            var url = 'api/MstHRLoanApprovalsApproved/GetHRloanHRheadRequestDetails';
            SocketService.get(url).then(function (resp) {
                $scope.requestdetails_list = resp.data.Approvedrequestsummary;
                unlockUI();
            });
            var url = 'api/MstHRLoanDrmApproval/GetHRloanHRheadRequestDetailscount';
               SocketService.get(url).then(function (resp) {
                   unlockUI();
                   $scope.lshrpendingapprovals_count = resp.data.hrpendingapprovals_count;
                   $scope.lshrapprovedapprovals_count = resp.data.hrapprovedapprovals_count;                  
                   $scope.lshrrejectedapprovals_count = resp.data.hrrejectedapprovals_count;                   
             });
        }       
      
        $scope.loan_approval = function (request_gid, employee_gid) {
            $location.url('app/AprHRLoanHRHeadApprovals?hash=' + cmnfunctionService.encryptURL('request_gid=' + request_gid + '&employee_gid=' + employee_gid  + '&lspage=HRApproved' + '&lsflag=N'));         
        }

        $scope.myapproval_loans = function () {
            $state.go('app.AprHRLoanHRHeadApprovalsSummary');
        }

        $scope.approved_loans = function () {
            $state.go('app.AprHRLoanHRHeadApprovedSummary');
        }


        $scope.rejected_loans = function () {
            $state.go('app.AprHRLoanHRHeadRejectedSummary');
        }
        
        $scope.viewrequests = function (request_gid) {
            $location.url('app/AprHRLoanHRHeadApprovedView?hash=' + cmnfunctionService.encryptURL('request_gid=' + request_gid));
        }
       
    }
})();


(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AprHRLoanHRHeadApprovedViewController', AprHRLoanHRAdvanceApprovalsController);

    AprHRLoanHRAdvanceApprovalsController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams','DownloaddocumentService','cmnfunctionService'];

    function AprHRLoanHRAdvanceApprovalsController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams,DownloaddocumentService,cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AprHRLoanHRHeadApprovedViewController';
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
            var url = 'api/MstHRLoanApprovalsApproved/GetHRloanApprovalSummary';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.Approvalsummary = resp.data.ApprovedApprovalsummary;
            });
            var url = 'api/MstHRLoanApprovalsRejected/GetHRloanApprovalSummary';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.Approvalsummary = resp.data.RejectedApprovalsummary;
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
            var params =
            {
                request_gid: request_gid
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

            $state.go('app.AprHRLoanHRHeadApprovedSummary');
        }

        $scope.view_fhquerydesc = function (query_description, queryresponse_remarks, query_responseby) {
            var modalInstance = $modal.open({
                templateUrl: '/fhqueryDescriptionView.html',
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

        $scope.view_drmquerydesc = function (query_description, queryresponse_remarks, query_responseby) {
            var modalInstance = $modal.open({
                templateUrl: '/drmqueryDescriptionView.html',
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
                templateUrl: '/hrqueryDescriptionView.html',
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



(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AprHRLoanHRHeadRejectedSummaryController', AprHRLoanHRHeadRejectedSummaryController);

        AprHRLoanHRHeadRejectedSummaryController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams','cmnfunctionService'];

    function AprHRLoanHRHeadRejectedSummaryController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams,cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AprHRLoanHRHeadRejectedSummaryController';
        lockUI();
        activate();

        function activate() { 
            lockUI();
            var url = 'api/MstHRLoanApprovalsRejected/GetHRloanHRheadRequestDetails';
            SocketService.get(url).then(function (resp) {
                $scope.requestdetails_list = resp.data.Rejectedrequestsummary;
                unlockUI();
            });
            var url = 'api/MstHRLoanDrmApproval/GetHRloanHRheadRequestDetailscount';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.lshrpendingapprovals_count = resp.data.hrpendingapprovals_count;
                $scope.lshrapprovedapprovals_count = resp.data.hrapprovedapprovals_count;                  
                $scope.lshrrejectedapprovals_count = resp.data.hrrejectedapprovals_count;                
          });
        }       
      
        $scope.loan_approval = function (request_gid, employee_gid) {
            $location.url('app/AprHRLoanHRHeadApprovals?hash=' + cmnfunctionService.encryptURL('request_gid=' + request_gid + '&employee_gid=' + employee_gid + '&lspage=MyTeamApprovals'));         
        }

        $scope.myapproval_loans = function () {
            $state.go('app.AprHRLoanHRHeadApprovalsSummary');
        }
        $scope.approved_loans = function () {
            $state.go('app.AprHRLoanHRHeadApprovedSummary');
        }

        $scope.rejected_loans = function () {
            $state.go('app.AprHRLoanHRHeadRejectedSummary');
        }       
        $scope.viewrequests = function (request_gid) {
            $location.url('app/AprHRLoanHRHeadRejectedView?hash=' + cmnfunctionService.encryptURL('request_gid=' + request_gid));
        }
       
    }
})();


(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AprHRLoanHRHeadRejectedViewController', AprHRLoanHRAdvanceApprovalsController);

    AprHRLoanHRAdvanceApprovalsController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams','DownloaddocumentService','cmnfunctionService'];

    function AprHRLoanHRAdvanceApprovalsController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams,DownloaddocumentService,cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AprHRLoanHRHeadRejectedViewController';
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
            var url = 'api/MstHRLoanApprovalsApproved/GetHRloanApprovalSummary';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.Approvalsummary = resp.data.ApprovedApprovalsummary;
            });
            var url = 'api/MstHRLoanApprovalsRejected/GetHRloanApprovalSummary';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.Approvalsummary = resp.data.RejectedApprovalsummary;
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
            var params =
            {
                request_gid: request_gid
            }
            var url = 'api/MstHRLoanRequest/GetUploadDocumentsList';
                    SocketService.getparams(url, params).then(function (resp) {
                        $scope.upload_list = resp.data.upload_list;
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
            $state.go('app.AprHRLoanHRHeadRejectedSummary');
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
        $scope.view_fhquerydesc = function (query_description, queryresponse_remarks, query_responseby) {
            var modalInstance = $modal.open({
                templateUrl: '/fhqueryDescriptionView.html',
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

        $scope.view_drmquerydesc = function (query_description, queryresponse_remarks, query_responseby) {
            var modalInstance = $modal.open({
                templateUrl: '/drmqueryDescriptionView.html',
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
                templateUrl: '/hrqueryDescriptionView.html',
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



(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstHRLoanAddRequestController', MstHRLoanAddRequestController);

    MstHRLoanAddRequestController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', 'DownloaddocumentService', 'cmnfunctionService'];

    function MstHRLoanAddRequestController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, DownloaddocumentService, cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstHRLoanAddRequestController';

        var lsFunctionalhead_gid, lsdepartment_gid, lsreportingmgr_gid,lshrhead_gid,lsemployee,lsuser_gid,lsentity_gid;
       

        activate();
        function activate() {
             var url = 'api/MstHRLoanRequest/tempdelete';
             SocketService.get(url).then(function (resp) {
             });

            var url = 'api/MstHRLoanRequest/GetFinType';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.fintype_list = resp.data.fintype_list;
                unlockUI();
            });
            var url = 'api/MstHRLoanRequest/GetSeverity';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.severity_list = resp.data.severity_list;
                unlockUI();
            });
            var url = 'api/MstHRLoanRequest/GetPurpose';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.purpose_list = resp.data.purpose_list;
                unlockUI();
            });

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
                lsemployee = resp.data.employee_gid;
                lsuser_gid = resp.data.user_gid; 
                $scope.txtinterest = 0;
                unlockUI();
            });                      
        }



        //Number in words
        function inWords(num) {
            var a = ['', 'One ', 'Two ', 'Three ', 'Four ', 'Five ', 'Six ', 'Seven ', 'Eight ', 'Nine ', 'Ten ', 'Eleven ', 'Twelve ', 'Thirteen ', 'Fourteen ', 'Fifteen ', 'Sixteen ', 'Seventeen ', 'Eighteen ', 'Nineteen '];
            var b = ['', '', 'Twenty', 'Thirty', 'Forty', 'Fifty', 'Sixty', 'Seventy', 'Eighty', 'Ninety'];
            var s = num.toString();
            s = s.replace(/[\, ]/g, '');
            if (s != parseFloat(s)) return '';
            if ((num = num.toString()).length > 9) return 'Overflow';
            var n = ('000000000' + num).substr(-9).match(/^(\d{2})(\d{2})(\d{2})(\d{1})(\d{2})$/);
            if (!n) return; var str = '';
            str += (n[1] != 0) ? (a[Number(n[1])] || b[n[1][0]] + ' ' + a[n[1][1]]) + 'Crore ' : '';
            str += (n[2] != 0) ? (a[Number(n[2])] || b[n[2][0]] + ' ' + a[n[2][1]]) + 'Lakh ' : '';
            str += (n[3] != 0) ? (a[Number(n[3])] || b[n[3][0]] + ' ' + a[n[3][1]]) + 'Thousand ' : '';
            str += (n[4] != 0) ? (a[Number(n[4])] || b[n[4][0]] + ' ' + a[n[4][1]]) + 'Hundred ' : '';
            str += (n[5] != 0) ? ((str != '') ? 'and ' : '') + (a[Number(n[5])] || b[n[5][0]] + ' ' + a[n[5][1]]) + 'only ' : '';
            return str;
        }

       
        $scope.annual_turnoverChange = function () {
            var input = document.getElementById('annual_turnover').value;
            var str = input.replace(/,/g, '');
            var output = Number(str).toLocaleString('en-IN');
            var lswords_annualturnover = cmnfunctionService.fnConvertNumbertoWord(str);
            if (output == "NaN") {
                Notify.alert('Accept number format only..!', {
                    status: 'warning',
                    pos: 'top-center',
                    timeout: 3000
                });
                $scope.txtamount = "";
            }
            else {
                $scope.txtamount = output;
                document.getElementById('words_annualturnover').innerHTML = lswords_annualturnover;
            }
        }

        // $scope.Gettenure = function (fintype_name) {
        //     lockUI();
        //     var params = {
        //         // fintype_gid:'',
        //         fintype_name: fintype_name
        //     }
        //     var url = 'api/MstHRLoanRequest/GetTenureName';
        //     SocketService.getparams(url, params).then(function (resp) {
        //         if(resp.data.tenure == null || resp.data.tenure == undefined || resp.data.tenure == '')
        //         {                    
        //             Notify.alert('Kindly add tenure for the financial assistance', 'warning');
        //             $scope.txttenure = ''; 
        //         }
        //         else if(resp.data.tenure != null || resp.data.tenure != undefined || resp.data.tenure != '')
        //         {                 
        //             $scope.txttenure = resp.data.tenure;                 
        //         }
                
        //         unlockUI();
        //     });
        // }
        $scope.loanamount = function (fintype_gid) {
            for (var i = 0; i < $scope.fintype_list.length; i++) {
                if (fintype_gid == $scope.fintype_list[i].fintype_gid){
                $scope.selectinterest = $scope.fintype_list[i].fintype_name.replace(" ","").toLowerCase();                
                }
            }
        }
        
        $scope.purposeNote = function(purpose_gid){
            for (var i = 0; i < $scope.purpose_list.length; i++) {
                if (purpose_gid == $scope.purpose_list[i].purpose_gid){
                // $scope.selectNote = $scope.purpose_list[i].purpose_note;
                $scope.selectMandatory = $scope.purpose_list[i].mandatory;
                document.getElementById('selectNote').innerHTML = $scope.purpose_list[i].purpose_note;               
                }            
            }
        }

        $scope.download_allenquiry = function () {
            for (var i = 0; i < $scope.upload_list.length; i++) {
                //  console.log(array[i]);
                DownloaddocumentService.Downloaddocument($scope.upload_list[i].document_path, $scope.upload_list[i].document_name);
            }
        }
     
                // Document Multiple Add
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
        
            $scope.UploadcompanyDocument = function (val, val1, name) {
                if (($scope.txtdocument_id == null) || ($scope.txtdocument_id == '') || ($scope.txtdocument_id == undefined) || ($scope.txtdocument_title == null) || ($scope.txtdocument_title == '') || ($scope.txtdocument_title == undefined)) {
                    $("#file").val('');
                    Notify.alert('Kindly Enter the Document Title/ID', 'warning');
                }
                else {
                    var frm = new FormData();  
                    for (var i = 0; i < val.length; i++) {
                        var item = {
                            name: val[i].name,
                            file: val[i]
                        };
                        frm.append('fileupload', item.file);
                        frm.append('file_name', item.name);
                        var IsValidExtension = cmnfunctionService.fnCheckValidDocType(val[i].name, "");

                        if (IsValidExtension == false) {
                            Notify.alert("File format is not supported..!", {
                                status: 'danger',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            return false;
                        }
                    }
                //     var item = {
                //         name: val[0].name,
                //         file: val[0]
                //     };
                //     var IsValidExtension = cmnfunctionService.fnCheckValidDocType(val[0].name, "");

                //     if (IsValidExtension == false) {
                //         Notify.alert("File format is not supported..!", {
                //             status: 'danger',
                //             pos: 'top-center',
                //             timeout: 3000
                //         });
                //         return false;
                //     }
                
                // frm.append('fileupload', item.file);
                // frm.append('file_name', item.name);
                frm.append('document_name', $scope.documentname);
                frm.append('document_title', $scope.txtdocument_title);             
                frm.append('document_id', $scope.txtdocument_id);
                frm.append('request_gid',$scope.request_gid);
                frm.append('project_flag', "Default");
                $scope.uploadfrm = frm;
                if ($scope.uploadfrm != undefined) {
                    var url = 'api/MstHRLoanRequest/RequestDocumentUpload';
                    SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {
                        $scope.upload_list = resp.data.upload_list;
                        unlockUI();
                        $("#file").val('');
                        $scope.txtdocument_title = "";
                        $scope.txtdocument_id = "";
                        $scope.uploadfrm = undefined;
                         
                        if (resp.data.status == true) {
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                       
                        var url = 'api/MstHRLoanRequest/GetAddUploadDocumentsList';
                                SocketService.get(url).then(function (resp) {
                                    $scope.upload_list = resp.data.upload_list;
                                });                           
                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                        }
                        unlockUI();
                    });
                }
                else {
                    alert('Document is not Available..!');
                    return;
                }
               }
        }

       
        
            $scope.delete_companydocument = function (hrreqdocument_gid) {
                lockUI();
                var params = {
                    hrreqdocument_gid: hrreqdocument_gid
                }
                var url = 'api/MstHRLoanRequest/UploadDocumentsDelete';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.upload_list = resp.data.upload_list;
                    if (resp.data.status == true) {
        
                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });
                    }
                    else {
                        Notify.alert(resp.data.message, {
                            status: 'danger',
                            pos: 'top-center',
                            timeout: 3000
                        });
                    }
                    var url = 'api/MstHRLoanRequest/GetAddUploadDocumentsList';
                                SocketService.get(url).then(function (resp) {
                                    $scope.upload_list = resp.data.upload_list;
                                });                  
                    unlockUI();
                });
            }
        
            
        $scope.submit_request = function () {
           
            var params = {                 
                fintype_gid: $scope.cbofintype.fintype_gid,
                fintype_name: $scope.cbofintype.fintype_name,
                employee_gid: lsemployee,
                employee_name: $scope.txtemp,
                employee_role: $scope.txtrole,
                department_gid: lsdepartment_gid,
                department_name: $scope.txtdept,
                user_gid: lsuser_gid,
                reporting_mgr: $scope.txtreporting_manager,
                reportingmgr_gid: lsreportingmgr_gid,
                functional_head: $scope.txtfunctional_head,
                functionalhead_gid: lsFunctionalhead_gid,
                hr_head: $scope.txthr_head,
                hrhead_gid: lshrhead_gid,
                amount: $scope.txtamount,
                purpose_gid: $scope.cbopurpose.purpose_gid,
                purpose_name: $scope.cbopurpose.purpose_name,
                severity_gid: $scope.cboseverity.severity_gid,
                severity_name: $scope.cboseverity.severity_name,
                tenure: $scope.cbotenure,
                interest: $scope.txtinterest,
                request_reason: $scope.txtreason,
                entity_name : $scope.txtentityname,
                entity_gid: lsentity_gid
            }


            var url = "api/MstHRLoanRequest/PostHrloanRequest"
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    unlockUI();
                    $state.go('app.MstHRLoanRaiseRequest');
                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'danger',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    unlockUI();
                }
            });

        }
        
        $scope.saveas_draft = function () {
            var lsfintype_gid = '';
            var lsfintype_name = '';
            if ($scope.cbofintype != undefined || $scope.cbofintype != null) {
                lsfintype_gid = $scope.cbofintype.fintype_gid;
                lsfintype_name = $scope.cbofintype.fintype_name;
            }

            var lspurpose_gid = '';
            var lspurpose_name = '';
            if ($scope.cbopurpose != undefined || $scope.cbopurpose != null) {
                lspurpose_gid = $scope.cbopurpose.purpose_gid;
                lspurpose_name = $scope.cbopurpose.purpose_name;
            }

            var lsseverity_gid = '';
            var lsseverity_name = '';
            if ($scope.cboseverity != undefined || $scope.cboseverity != null) {
                lsseverity_gid = $scope.cboseverity.severity_gid;
                lsseverity_name = $scope.cboseverity.severity_name;
            }            
            
            
            var params = {
                fintype_gid: lsfintype_gid,
                fintype_name: lsfintype_name,
                purpose_gid: lspurpose_gid,
                purpose_name: lspurpose_name,
                severity_gid: lsseverity_gid,
                severity_name: lsseverity_name,
                employee_gid: lsemployee,
                employee_name: $scope.txtemp,
                employee_role: $scope.txtrole,
                department_gid: lsdepartment_gid,
                department_name: $scope.txtdept,
                user_gid: lsuser_gid,
                reporting_mgr: $scope.txtreporting_manager,
                reportingmgr_gid: lsreportingmgr_gid,
                functional_head: $scope.txtfunctional_head,
                functionalhead_gid: lsFunctionalhead_gid,
                hr_head: $scope.txthr_head,
                hrhead_gid: lshrhead_gid,
                amount: $scope.txtamount,                
                tenure: $scope.cbotenure,
                interest: $scope.txtinterest,
                request_reason: $scope.txtreason,
                entity_name : $scope.txtentityname,
                entity_gid: lsentity_gid
               
            }


            var url = "api/MstHRLoanRequest/HRLoanRequestSaveasdraft"
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });                    
                    $state.go('app.MstHRLoanRaiseRequest');
                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'danger',
                        pos: 'top-center',
                        timeout: 3000
                    });
                   
                }
            });

        }

        $scope.back = function () {
            $state.go('app.MstHRLoanRaiseRequest');
        }
    }

})();
(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstHRLoanEditRequestController', MstHRLoanEditRequestController);

    MstHRLoanEditRequestController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', 'DownloaddocumentService', 'cmnfunctionService'];

    function MstHRLoanEditRequestController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, DownloaddocumentService, cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstHRLoanEditRequestController';

        var lsFunctionalhead_gid, lsdepartment_gid, lsreportingmgr_gid,lsentity_gid;
        $scope.request_gid = cmnfunctionService.decryptURL($location.search().hash).request_gid;
        var request_gid = $scope.request_gid;
        var hrreqdocument_gid = $scope.hrreqdocument_gid;
        
        activate();
        function activate() {
            // var url = 'api/MstHRLoanRequest/tempdelete';
            // SocketService.get(url).then(function (resp) {
            // });

            var url = 'api/MstHRLoanRequest/GetFinType';
            lockUI();
            SocketService.get(url).then(function (resp) {
               $scope.fintype_list = resp.data.fintype_list;
               unlockUI();
            });
            var url = 'api/MstHRLoanRequest/GetSeverity';
            lockUI();
            SocketService.get(url).then(function (resp) {
               $scope.severity_list = resp.data.severity_list;
               unlockUI();
            });
            var url = 'api/MstHRLoanRequest/GetPurpose';
            lockUI();
            SocketService.get(url).then(function (resp) {
               $scope.purpose_list = resp.data.purpose_list;
               unlockUI();
            });
            
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
                $scope.cbofintype = resp.data.fintype_gid;     
                $scope.fintype_name = resp.data.fintype_name;
                $scope.txtamount = resp.data.amount;
                $scope.cbopurpose = resp.data.purpose_gid;                
                $scope.cboseverity = resp.data.severity_gid;                
                $scope.cbotenure = resp.data.tenure;
                $scope.txtinterest = resp.data.interest;
                $scope.txtreason = resp.data.request_reason;
                $scope.lsrequest_status = resp.data.request_status;
                $scope.selectMandatory = resp.data.mandatory;
                document.getElementById('selectNote').innerHTML = resp.data.purpose_note;              
            });

             var params = {
                request_gid: request_gid
            }
            var url = 'api/MstHRLoanRequest/EditHRLoanRequest';           
            SocketService.getparams(url, params).then(function (resp) { 
                if($scope.cbofintype = resp.data.fintype_gid){
                  
                    $scope.selectinterest = resp.data.fintype_name.replace(" ", "").toLowerCase();
                   
                    if ($scope.selectinterest == 'salaryadvance') {
                        $scope.selectinterest = 'salaryadvance';
                        }
                   
                }
            });

            var params =
            {
                request_gid: request_gid
            }
            var url = 'api/MstHRLoanRequest/GetUploadDocumentsList';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.upload_list = resp.data.upload_list;
            });  
            
            
        }

        //Number in words
        function inWords(num) {
            var a = ['', 'One ', 'Two ', 'Three ', 'Four ', 'Five ', 'Six ', 'Seven ', 'Eight ', 'Nine ', 'Ten ', 'Eleven ', 'Twelve ', 'Thirteen ', 'Fourteen ', 'Fifteen ', 'Sixteen ', 'Seventeen ', 'Eighteen ', 'Nineteen '];
            var b = ['', '', 'Twenty', 'Thirty', 'Forty', 'Fifty', 'Sixty', 'Seventy', 'Eighty', 'Ninety'];
            var s = num.toString();
            s = s.replace(/[\, ]/g, '');
            if (s != parseFloat(s)) return '';
            if ((num = num.toString()).length > 9) return 'Overflow';
            var n = ('000000000' + num).substr(-9).match(/^(\d{2})(\d{2})(\d{2})(\d{1})(\d{2})$/);
            if (!n) return; var str = '';
            str += (n[1] != 0) ? (a[Number(n[1])] || b[n[1][0]] + ' ' + a[n[1][1]]) + 'Crore ' : '';
            str += (n[2] != 0) ? (a[Number(n[2])] || b[n[2][0]] + ' ' + a[n[2][1]]) + 'Lakh ' : '';
            str += (n[3] != 0) ? (a[Number(n[3])] || b[n[3][0]] + ' ' + a[n[3][1]]) + 'Thousand ' : '';
            str += (n[4] != 0) ? (a[Number(n[4])] || b[n[4][0]] + ' ' + a[n[4][1]]) + 'Hundred ' : '';
            str += (n[5] != 0) ? ((str != '') ? 'and ' : '') + (a[Number(n[5])] || b[n[5][0]] + ' ' + a[n[5][1]]) + 'only ' : '';
            return str;
        }
        $scope.annual_turnoverChange = function () {
            var input = document.getElementById('annual_turnover').value;
            var str = input.replace(/,/g, '');
            var output = Number(str).toLocaleString('en-IN');
            var lswords_annualturnover = cmnfunctionService.fnConvertNumbertoWord(str);
            if (output == "NaN") {
                Notify.alert('Accept number format only..!', {
                    status: 'warning',
                    pos: 'top-center',
                    timeout: 3000
                });
                $scope.txtamount = "";
            }
            else {
                $scope.txtamount = output;
                document.getElementById('words_annualturnover').innerHTML = lswords_annualturnover;
            }
        }

        $scope.download_all = function () {
            for (var i = 0; i < $scope.upload_list.length; i++) {
                //  console.log(array[i]);
                DownloaddocumentService.Downloaddocument($scope.upload_list[i].document_path, $scope.upload_list[i].document_name);
            }
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
        
        $scope.UploadDocument = function (val, val1, name) {
            if (($scope.txtdocument_id == null) || ($scope.txtdocument_id == '') || ($scope.txtdocument_id == undefined) || ($scope.txtdocument_title == null) || ($scope.txtdocument_title == '') || ($scope.txtdocument_title == undefined)) {
                $("#file").val('');
                Notify.alert('Kindly Enter the Document Title/ID', 'warning');
            }
            else {
                var frm = new FormData();
                for (var i = 0; i < val.length; i++) {
                var item = {
                name: val[i].name,
                file: val[i]
            };
            frm.append('fileupload', item.file);
            frm.append('file_name', item.name);
            var IsValidExtension = cmnfunctionService.fnCheckValidDocType(val[i].name, "");

            if (IsValidExtension == false) {
                Notify.alert("File format is not supported..!", {
                    status: 'danger',
                    pos: 'top-center',
                    timeout: 3000
                });
                return false;
            }
        }
            //     var item = {
            //         name: val[0].name,
            //         file: val[0]
            //     };
            //     var IsValidExtension = cmnfunctionService.fnCheckValidDocType(val[0].name, "");

            //     if (IsValidExtension == false) {
            //         Notify.alert("File format is not supported..!", {
            //             status: 'danger',
            //             pos: 'top-center',
            //             timeout: 3000
            //         });
            //         return false;
            //     }
            // var frm = new FormData();
            // frm.append('fileupload', item.file);
            // frm.append('file_name', item.name);
            frm.append('document_name', $scope.documentname);
            frm.append('document_title', $scope.txtdocument_title);         
            frm.append('document_id', $scope.txtdocument_id);
            frm.append('request_gid',$scope.request_gid);
            frm.append('project_flag', "Default");
            $scope.uploadfrm = frm;
            if ($scope.uploadfrm != undefined) {
                var url = 'api/MstHRLoanRequest/RequestDocumentUpload';
                SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {
                    $scope.upload_list = resp.data.upload_list;
                    unlockUI();
                    $("#file").val('');
                    $scope.txtdocument_title = "";
                    $scope.txtdocument_id = "";
                    $scope.uploadfrm = undefined;
                    if (resp.data.status == true) {
                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        var params =
                        {
                            request_gid: request_gid
                        }
                        var url = 'api/MstHRLoanRequest/GetAddUploadDocumentsList';
                                SocketService.getparams(url, params).then(function (resp) {
                                    $scope.upload_list = resp.data.upload_list;
                                });
    
                    }
                    else {
                        Notify.alert(resp.data.message, {
                            status: 'warning',
                            pos: 'top-center',
                            timeout: 3000
                        });
                    }
                    unlockUI();
                });
            }
            else {
                alert('Document is not Available..!');
                return;
            }
       }
    }
                        
                    
    $scope.delete_document = function (hrreqdocument_gid) {
        lockUI();
        var params = {
            hrreqdocument_gid: hrreqdocument_gid
        }
        var url = 'api/MstHRLoanRequest/UploadDocumentsDelete';
        SocketService.getparams(url, params).then(function (resp) {
            $scope.upload_list = resp.data.upload_list;
            if (resp.data.status == true) {

                Notify.alert(resp.data.message, {
                    status: 'success',
                    pos: 'top-center',
                    timeout: 3000
                });
            }
            else {
                Notify.alert(resp.data.message, {
                    status: 'danger',
                    pos: 'top-center',
                    timeout: 3000
                });               
            }
            var params =
                {
                    request_gid: request_gid
                }
                var url = 'api/MstHRLoanRequest/GetAddUploadDocumentsList';
                        SocketService.getparams(url, params).then(function (resp) {
                            $scope.upload_list = resp.data.upload_list;
                        });            
            unlockUI();
        });
    }

        // $scope.Gettenure = function (fintype_gid) {
        //     lockUI();
        //     var params = {               
        //         fintype_gid: fintype_gid,
        //         // fintype_name: '',

        //     }
        //     var url = 'api/MstHRLoanRequest/GetTenureNameEdit';
        //     SocketService.getparams(url, params).then(function (resp) {
        //         if(resp.data.tenure == null || resp.data.tenure == undefined || resp.data.tenure == '')
        //         {                    
        //             Notify.alert('Kindly add tenure for the financial assistance', 'warning');
        //             $scope.txttenure = ''; 
        //         }
        //         else if(resp.data.tenure != null || resp.data.tenure != undefined || resp.data.tenure != '')
        //         {                 
        //             $scope.txttenure = resp.data.tenure;                 
        //         }
        //         unlockUI();
        //     });
        // }

        $scope.loanamount = function (cbofintype) {
            for (var i = 0; i < $scope.fintype_list.length; i++) {
                if (cbofintype == $scope.fintype_list[i].fintype_gid)
                $scope.selectinterest = $scope.fintype_list[i].fintype_name.replace(" ","").toLowerCase();
                $scope.txtinterest= 0;
                }
            }

        $scope.purposeNote = function(cbopurpose){
            for (var i = 0; i < $scope.purpose_list.length; i++) {
                if (cbopurpose == $scope.purpose_list[i].purpose_gid){
                // $scope.selectNote = $scope.purpose_list[i].purpose_note;
                $scope.selectMandatory = $scope.purpose_list[i].mandatory;
                document.getElementById('selectNote').innerHTML = $scope.purpose_list[i].purpose_note;               
                }            
            }
        }

        $scope.saveas_draft = function () {
            var fintype_name = $('#fin :selected').text();
            var purpose_name = $('#purpose :selected').text();
            var severity_name = $('#severity :selected').text(); 
            

            lockUI();
            var url = 'api/MstHRLoanRequest/UpdateHRLoanSaveasdraft';
            var params = {               
                request_gid: request_gid,
                fintype_gid: $scope.cbofintype,
                fintype_name: fintype_name,                
                amount: $scope.txtamount,
                purpose_gid: $scope.cbopurpose,
                purpose_name: purpose_name,
                severity_gid: $scope.cboseverity,
                severity_name: severity_name,
                tenure: $scope.cbotenure,
                interest: $scope.txtinterest,
                request_reason: $scope.txtreason
               

            }
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    $state.go('app.MstHRLoanRaiseRequest');
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
        $scope.saveasdraft_submit = function () {

            var fintype_name = $('#fin :selected').text();
            var purpose_name = $('#purpose :selected').text();
            var severity_name = $('#severity :selected').text(); 
            

            lockUI();
            var url = 'api/MstHRLoanRequest/PostHRLoanSaveasdraft';
            var params = {               
                request_gid: request_gid,
                fintype_gid: $scope.cbofintype,
                fintype_name: fintype_name,                
                amount: $scope.txtamount,
                purpose_gid: $scope.cbopurpose,
                purpose_name: purpose_name,
                severity_gid: $scope.cboseverity,
                severity_name: severity_name,
                tenure: $scope.cbotenure,
                interest: $scope.txtinterest,
                request_reason: $scope.txtreason
                             
            }
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    $state.go('app.MstHRLoanRaiseRequest');
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
        $scope.update = function () {

            var fintype_name = $('#fin :selected').text();
            var purpose_name = $('#purpose :selected').text();
            var severity_name = $('#severity :selected').text(); 
            

            lockUI();
            var url = 'api/MstHRLoanRequest/UpdateHRLoanRequest';
            var params = {               
                request_gid: request_gid,
                fintype_gid: $scope.cbofintype,
                fintype_name: fintype_name,                
                amount: $scope.txtamount,
                purpose_gid: $scope.cbopurpose,
                purpose_name: purpose_name,
                severity_gid: $scope.cboseverity,
                severity_name: severity_name,
                tenure: $scope.cbotenure,
                interest: $scope.txtinterest,
                request_reason: $scope.txtreason
                            
            }
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    $state.go('app.MstHRLoanRaiseRequest');
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

        $scope.back = function () {
            $state.go('app.MstHRLoanRaiseRequest');
        }
    }
    
})();
(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstHRLoanEditRequestController360', MstHRLoanEditRequestController360);

    MstHRLoanEditRequestController360.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', 'DownloaddocumentService', 'cmnfunctionService'];

    function MstHRLoanEditRequestController360($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, DownloaddocumentService, cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstHRLoanEditRequestController360';

        var lsFunctionalhead_gid, lsdepartment_gid, lsreportingmgr_gid, lsentity_gid;
        $scope.request_gid = cmnfunctionService.decryptURL($location.search().hash).request_gid;
        var request_gid = $scope.request_gid;
        var hrreqdocument_gid = $scope.hrreqdocument_gid;

        activate();
        function activate() {
            // var url = 'api/MstHRLoanRequest/tempdelete';
            // SocketService.get(url).then(function (resp) {
            // });
            // var url = 'api/MstHRLoanRequest/GetHRloanDetails';
            // lockUI();
            // SocketService.get(url).then(function (resp) {
            //     $scope.hrloanrequestSummary_list = resp.data.hrloanrequest;               
            //     unlockUI();
            // });

            var url = 'api/MstHRLoanRequest/GetFinType';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.fintype_list = resp.data.fintype_list;
                unlockUI();
            });
            var url = 'api/MstHRLoanRequest/GetSeverity';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.severity_list = resp.data.severity_list;
                unlockUI();
            });
            var url = 'api/MstHRLoanRequest/GetPurpose';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.purpose_list = resp.data.purpose_list;
                unlockUI();
            });

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
            var url = 'api/MstHRLoanDrmApproval/GetHRloanApprovalSummary';
            SocketService.getparams(url, params).then(function (resp) {

                $scope.Approvalsummary = resp.data.Approvalsummary;
            });
            var params = {
                request_gid: request_gid
            }
            var url = 'api/MstHRLoanApprovalsApproved/GetHRloanApprovalSummary';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.Approvalsummary = resp.data.ApprovedApprovalsummary;
            });
            var params = {
                request_gid: request_gid
            }
            var url = 'api/MstHRLoanApprovalsRejected/GetHRloanApprovalSummary';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.Approvalsummary = resp.data.RejectedApprovalsummary;
            });
            var params = {
                request_gid: request_gid
            }
            var url = 'api/MstHRLoanRequest/EditHRLoanRequest';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                $scope.fintype = resp.data.fintype_name;
                $scope.amount = resp.data.amount;
                $scope.purpose = resp.data.purpose_name;
                $scope.severity = resp.data.severity_name;
                $scope.tenure = resp.data.tenure;
                $scope.reason = resp.data.request_reason;
                $scope.txtinterest = resp.data.interest;
                document.getElementById('selectNote').innerHTML = resp.data.purpose_note;

                unlockUI();
            });
            var url = 'api/TrnHRLoanHRVerifications/HRLoanGetApprovedInterest';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.approvedinterest = resp.data.approved_interest;
                $scope.approvedtenure = resp.data.approved_tenure;
                $scope.approvedtenure_startdate = resp.data.approvedtenure_startdate;
                $scope.approvedtenure_enddate = resp.data.approvedtenure_enddate;

            });
            var params =
            {
                request_gid: request_gid
            }
            var url = 'api/TrnHRLoanHRVerifications/termsandcondtnview';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.termsandcont_list = resp.data.hrtermsandconditions_list;
            });
            var url = 'api/MstHRLoanRequest/GetUploadDocumentsList';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.upload_list = resp.data.upload_list;
            });

            var url = 'api/MstHRLoanRequest/GetHRLoanStatusRequest';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.request_status = resp.data.request_status;
                $scope.raisequery_status = resp.data.raisequery_status;
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
     

        //  Document Multiple Add
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

        //$scope.download = function (val1, val2) {
        //          DownloaddocumentService.Downloaddocument(val1, val2);
        //      } 
        $scope.download = function () {
            var filename = "Salary Advance form.pdf";
            //var phyPath = resp.data.file_path;
            var phyPath = "F:\\Web\\EMS\\templates\\Salary Advance form.pdf";
            var relPath = phyPath.split("EMS");
            var relpath1 = relPath[1].replace("\\", "/");
            var hosts = window.location.host;
            var prefix = "http://"
            var str = prefix.concat(hosts, relpath1);
            var link = document.createElement("a");
            var name = filename.split('.');
            link.download = name[0];
            var uri = str;
            link.href = uri;
            link.click();
        }
        $scope.download1 = function () {
            var filename = "Employee Loan Application Form.pdf";
            //var phyPath = resp.data.file_path;
            var phyPath = "F:\\Web\\EMS\\templates\\Employee Loan Application Form.pdf";
            var relPath = phyPath.split("EMS");
            var relpath1 = relPath[1].replace("\\", "/");
            var hosts = window.location.host;
            var prefix = "http://"
            var str = prefix.concat(hosts, relpath1);
            var link = document.createElement("a");
            var name = filename.split('.');
            link.download = name[0];
            var uri = str;
            link.href = uri;
            link.click();
        }

        //$scope.download1 = function (val1, val2) {
        //          DownloaddocumentService.Downloaddocument(val1, val2);
        //      } 

        $scope.download_allenquiry = function () {
            for (var i = 0; i < $scope.hrReuploadDocument_list.length; i++) {
                //  console.log(array[i]);
                DownloaddocumentService.Downloaddocument($scope.hrReuploadDocument_list[i].document_path, $scope.hrReuploadDocument_list[i].document_name);
            }
        }

        $scope.download_all = function () {
            for (var i = 0; i < $scope.PaymentDocument_list.length; i++) {
                //  console.log(array[i]);
                DownloaddocumentService.Downloaddocument($scope.PaymentDocument_list[i].document_path, $scope.PaymentDocument_list[i].document_name);
            }
        }

        $scope.UploadHrDocument = function (val, val1, name) {
            if (($scope.cbodocument_id == null) || ($scope.cbodocument_id == '') || ($scope.cbodocument_id == undefined) || ($scope.cbodocumentname == null) || ($scope.cbodocumentname == '') || ($scope.cbodocumentname == undefined)) {
                $("#hrloanfile").val('');
                Notify.alert('Kindly Enter the Document Title/ID', 'warning');
            }
            else {

                var frm = new FormData();
                for (var i = 0; i < val.length; i++) {
                var item = {
                name: val[i].name,
                file: val[i]
            };
            frm.append('fileupload', item.file);
            frm.append('file_name', item.name);
            var IsValidExtension = cmnfunctionService.fnCheckValidDocType(val[i].name, "documentformatonly");

            if (IsValidExtension == false) {
                Notify.alert("File format is not supported..!", {
                    status: 'danger',
                    pos: 'top-center',
                    timeout: 3000
                });
                return false;
            }
        }
                // var item = {
                //     name: val[0].name,
                //     file: val[0]
                // };
                // var IsValidExtension = cmnfunctionService.fnCheckValidDocType(val[0].name, "");

                // if (IsValidExtension == false) {
                //     Notify.alert("File format is not supported..!", {
                //         status: 'danger',
                //         pos: 'top-center',
                //         timeout: 3000
                //     });
                //     return false;
                // }
                // var frm = new FormData();


                // $scope.uploadfrm = frm;
                // var frm = new FormData();
                // frm.append('fileupload', item.file);
                // frm.append('file_name', item.name);
                frm.append('document_name', $scope.documentname);
                frm.append('document_title', $scope.cbodocumentname.hrdocumentlist_name);
                frm.append('hrdocument_name', $scope.cbodocumentname.hrdocumentlist_gid);
                frm.append('document_id', $scope.cbodocument_id);
                frm.append('request_gid', $scope.request_gid);
                frm.append('project_flag', "Default");
                $scope.uploadfrm = frm;
                if ($scope.uploadfrm != undefined) {
                    var url = 'api/MstHRLoanDrmApproval/HRLoanReUpload';
                    SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {
                        $scope.hrReuploadDocument_list = resp.data.HrReuploadDocument_list;
                        unlockUI();
                        $("#hrloanfile").val('');
                        $scope.cbodocumentname = "";
                        $scope.cbodocument_id = "";
                        $scope.uploadfrm = undefined;
                        if (resp.data.status == true) {
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            var params =
                            {
                                request_gid: request_gid
                            }
                            var url = 'api/MstHRLoanDrmApproval/GetUploadDocumentsList';
                            SocketService.getparams(url, params).then(function (resp) {
                                $scope.hrReuploadDocument_list = resp.data.HrReuploadDocument_list;
                            });

                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                        }
                        unlockUI();
                    });
                }
                else {
                    alert('Document is not Available..!');
                    return;
                }
            }
        }
        $scope.delete_hrdocument = function (hrreuploaddocument_gid) {
            lockUI();
            var params = {
                hrreuploaddocument_gid: hrreuploaddocument_gid
            }
            var url = 'api/MstHRLoanDrmApproval/UploadDocumentsDelete';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.hrReuploadDocument_list = resp.data.HrReuploadDocument_list;
                if (resp.data.status == true) {

                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'danger',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
                var params =
                {
                    request_gid: request_gid
                }
                var url = 'api/MstHRLoanDrmApproval/GetUploadDocumentsList';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.hrReuploadDocument_list = resp.data.HrReuploadDocument_list;
                });


                unlockUI();
            });
        }
        $scope.allChecked = function () {
            return $scope.termsandcont_list.filter(function (obj) { return obj.checked; }).length === $scope.termsandcont_list.length;
         //return $scope.termsandcont_list.filter(function (obj) { return obj.checked; }).length === $scope.termsandcont_list.length;
        }
        
        $scope.checkAll = function () {
            angular.forEach($scope.termsandcont_list, function (val) {
                val.checked = $scope.check ;

            });
        };
        $scope.tremsandcondtn_acknwlg = function () {
            if ($scope.hrReuploadDocument_list == null) {
                Notify.alert('Kindly reupload the document!', 'warning')
            }
            else {
                if ($scope.termsandcont_list.filter(function (obj) { return obj.checked; }).length === $scope.termsandcont_list.length == 0) {
                    Notify.alert('Please select all the terms and conditions!', 'warning');
                }
                else {

                    var params = {
                        request_gid: request_gid,
                    }
                    var url = 'api/MstHRLoanDrmApproval/PostHrLoantremsandconditionacknwlg';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            $state.go("app.MstHRLoanRaiseRequest");
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
        }

       

        //$scope.checkall = function (selected) {
        //    angular.forEach($scope.termsandcont_list, function (val) {
        //        val.checked = selected;
        //    });
        //}

        //$scope.tremsandcondtn_acknwlg = function () {
        //    if ($scope.HRDocument_list != null && $scope.hrReuploadDocument_list == null) {
        //        Notify.alert('Kindly Reupload the Document!', 'warning')
        //    }
        //    else {
        //        var hrloantermsandconditions_gid;
        //        var termsandconditionslistGId = [];
        //        var check_flag = 0;
        //        angular.forEach($scope.termsandcont_list, function (val) {

        //            if (val.checked == true) {
        //                var termsandconditionslist_gid = val.hrloantermsandconditions_gid;
        //                hrloantermsandconditions_gid = val.hrloantermsandconditions_gid;
        //                termsandconditionslistGId.push(termsandconditionslist_gid);
        //                var params = {

        //                    request_gid: request_gid,
        //                    termsandconditionslist_gid: termsandconditionslistGId
        //                }
        //                if (hrloantermsandconditions_gid != undefined) {
        //                    var url = 'api/MstHRLoanDrmApproval/PostHrLoantremsandconditionacknwlg';
        //                    lockUI();
        //                    SocketService.post(url, params).then(function (resp) {
        //                        unlockUI();
        //                        if (resp.data.status == true) {
        //                            Notify.alert(resp.data.message, {
        //                                status: 'success',
        //                                pos: 'top-center',
        //                                timeout: 3000
        //                            });
        //                            $state.go("app.MstHRLoanRaiseRequest");
        //                        }
        //                        else {
        //                            Notify.alert(resp.data.message, {
        //                                status: 'warning',
        //                                pos: 'top-center',
        //                                timeout: 3000
        //                            });
        //                        }

        //                    });
        //                }
        //            }
        //            else {
        //                check_flag = 1;

        //            }

        //        });
        //        if (check_flag != 0) {
        //            Notify.alert('Select All the Terms and Conditions!', 'warning')
        //        }
        //    }

        //}



        $scope.back = function () {
            $state.go('app.MstHRLoanRaiseRequest');
        }

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

        $scope.drmquery_close = function (drmraisequery_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/drmqueryClose.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.submit = function () {
                    var params = {
                        drmraisequery_gid: drmraisequery_gid,
                        queryresponse_remarks: $scope.txtcloseremarks,
                        request_gid: cmnfunctionService.decryptURL($location.search().hash).request_gid
                    }
                    var url = 'api/MstHRLoanDrmApproval/PostDRMresponsequery';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
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
                    $modalInstance.close('closed');
                }

            }
        }
        $scope.fhquery_close = function (fhraisequery_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/fhqueryClose.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.submit = function () {
                    var params = {
                        fhraisequery_gid: fhraisequery_gid,
                        queryresponse_remarks: $scope.txtcloseremarks,
                        request_gid: cmnfunctionService.decryptURL($location.search().hash).request_gid
                    }
                    var url = 'api/MstHRLoanDrmApproval/PostFHresponsequery';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
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
                    $modalInstance.close('closed');
                }

            }
        }
        $scope.hrquery_close = function (hrraisequery_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/hrqueryClose.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.submit = function () {
                    var params = {
                        hrraisequery_gid: hrraisequery_gid,
                        queryresponse_remarks: $scope.txtcloseremarks,
                        request_gid: cmnfunctionService.decryptURL($location.search().hash).request_gid
                    }
                    var url = 'api/MstHRLoanDrmApproval/PostHRresponsequery';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
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
                    $modalInstance.close('closed');
                }

            }
        }
        $scope.mangquery_close = function (mangraisequery_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/mangqueryClose.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.submit = function () {
                    var params = {
                        mangraisequery_gid: mangraisequery_gid,
                        queryresponse_remarks: $scope.txtcloseremarks,
                        request_gid: cmnfunctionService.decryptURL($location.search().hash).request_gid
                    }
                    var url = 'api/TrnHRLoanHRVerifications/PostManagerresponsequery';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
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
                    $modalInstance.close('closed');
                }

            }
        }


    }

})();
(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstHRLoanHRDocumentAddController', MstHRLoanHRDocumentAddController);

    MstHRLoanHRDocumentAddController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function MstHRLoanHRDocumentAddController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstHRLoanHRDocumentAddController';

        activate();

        function activate() {
            
            var url = 'api/MstHRLoanHRDocument/HRDocumentCheckListTempClear';
            SocketService.get(url).then(function (resp) {
            });
            
            var url = 'api/MstHRLoanHRDocument/GetHRDocumentDropDown';
            SocketService.get(url).then(function (resp) {
                $scope.document_list = resp.data.document_list;
                $scope.hrdocumentseverity_list = resp.data.hrdocumentseverity_list;

            });

        }
       
        $scope.ok = function () {
            $modalInstance.close('closed');
        };
      
        $scope.Back = function () {
            $state.go('app.MstHRLoanHRDocument');

        }
        $scope.submit = function () {
            var lshrloantypeoffinancialassistance_gid = '';
            var lshrloantypeoffinancialassistance_name = '';
            if ($scope.cbohrdocument != undefined || $scope.cbohrdocument != null) {
                lshrloantypeoffinancialassistance_gid = $scope.cbohrdocument.hrloantypeoffinancialassistance_gid;
                lshrloantypeoffinancialassistance_name = $scope.cbohrdocument.hrloantypeoffinancialassistance_name;
            }

            var lshrloanseverity_gid = '';
            var lshrloanseverity_name = '';
            if ($scope.cbohrdocumentseverity != undefined || $scope.cbohrdocumentseverity != null) {
                lshrloanseverity_gid = $scope.cbohrdocumentseverity.hrloanseverity_gid;
                lshrloanseverity_name = $scope.cbohrdocumentseverity.hrloanseverity_name;
            }
            var params = {
                hrdocument_name: $scope.txthrdocument_name,
                hrloantypeoffinancialassistance_gid: lshrloantypeoffinancialassistance_gid,
                hrloantypeoffinancialassistance_name: lshrloantypeoffinancialassistance_name,
                hrloanseverity_gid: lshrloanseverity_gid,
                hrloanseverity_name: lshrloanseverity_name,
                lms_code: $scope.txtlms_code,
                bureau_code: $scope.txtbureau_code,
                // covenant_type: $scope.covenant_type 
            }
            lockUI();
            var url = 'api/MstHRLoanHRDocument/CreateHRDocument';
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    $state.go('app.MstHRLoanHRDocument');
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


        $scope.checklist_add = function () {
            if (($scope.txtchecklist == undefined) || ($scope.txtchecklist == '')) {
                Notify.alert('Enter Check List', {
                    status: 'warning',
                    pos: 'top-center',
                    timeout: 3000
                });
            }
            else {

                var params = {
                    hrdocumentchecklist_name: $scope.txtchecklist,
                  
                }
                lockUI();
                var url = 'api/MstHRLoanHRDocument/CreateHRDocumentCheckList';
                SocketService.post(url, params).then(function (resp) {
                    unlockUI();
                    if (resp.data.status == true) {
                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });
                    }
                    else {
                        Notify.alert(resp.data.message, {
                            status: 'warning',
                            pos: 'top-center',
                            timeout: 3000
                        });
                    }
                    checklist();
                    $scope.txtchecklist = '';
                    
                });
            }
        }

        function checklist() {
            var url = 'api/MstHRLoanHRDocument/GetHRDocumentCheckList';            
            SocketService.get(url).then(function (resp) {
                $scope.checklist_list = resp.data.hrloanhrdocument_list;

            });
        }

        $scope.checklist_delete = function (hrdocumentchecklist_gid) {
            var params =
                {
                    hrdocumentchecklist_gid: hrdocumentchecklist_gid
                }
            lockUI();
            var url = 'api/MstHRLoanHRDocument/DeleteHRDocumentCheckList';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'warning',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
                checklist();
            });
        }

    }
})();

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstHRLoanHRDocumentController', MstHRLoanHRDocumentController);

        MstHRLoanHRDocumentController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams','cmnfunctionService'];

    function MstHRLoanHRDocumentController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams,cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstHRLoanHRDocumentController';

        activate();

        function activate() { 
            var url = 'api/MstHRLoanHRDocument/GetHRDocument';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.hrdocument_data = resp.data.hrloanhrdocument_list;
                $scope.document_list = resp.data.document_list;
                $scope.hrdocumentseverity_list = resp.data.hrdocumentseverity_list;
                unlockUI();
            });
        }
        $scope.addhrdocument = function () {
            $state.go('app.MstHRLoanHRDocumentAdd');
        }
       

        $scope.viewhrdocument = function (hrdocument_gid) {
            $location.url('app/MstHRLoanHRDocumentView?hash=' + cmnfunctionService.encryptURL('lshrdocument_gid=' + hrdocument_gid));
        }

        $scope.edithrdocument = function (hrdocument_gid) {
            $location.url('app/MstHRLoanHRDocumentEdit?hash=' + cmnfunctionService.encryptURL('lshrdocument_gid=' + hrdocument_gid));
        }
       
        $scope.Status_update = function (hrdocument_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/statushrdocument.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                var params = {
                    hrdocument_gid: hrdocument_gid
                }
                var url = 'api/MstHRLoanHRDocument/EditHRDocument';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.hrdocument_gid = resp.data.hrdocument_gid
                    $scope.hrdocument_name = resp.data.hrdocument_name;
                    $scope.rbo_status = resp.data.Status;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.update_status = function () {

                    var params = {
                        hrdocument_gid: hrdocument_gid,
                        hrdocument_name: $scope.hrdocument_name,
                        remarks: $scope.txtremarks,
                        rbo_status: $scope.rbo_status

                    }
                    var url = 'api/MstHRLoanHRDocument/InactiveHRDocument';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {

                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });

                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'info',
                                pos: 'top-center',
                                timeout: 3000
                            });
                        }
                        activate();
                    });

                    $modalInstance.close('closed');

                }

                var param = {
                    hrdocument_gid: hrdocument_gid
                }

                var url = 'api/MstHRLoanHRDocument/InactiveHRDocumentHistory';
                lockUI();
                SocketService.getparams(url, param).then(function (resp) {
                    $scope.hrdocumentinactivelog_data = resp.data.hrdocumentinactivehistory_list;
                    unlockUI();
                });

            }
        }

        $scope.delete = function (hrdocument_gid) {
            var params = {
                hrdocument_gid: hrdocument_gid
                   }
            
                    SweetAlert.swal({
                        title: 'Are you sure?',
                        text: 'Do You Want To Delete the Record ?',
                        showCancelButton: true,
                        confirmButtonColor: '#DD6B55',
                        confirmButtonText: 'Yes, delete it!',
                        closeOnConfirm: false
                    }, function (isConfirm) {
                        if (isConfirm) {
                            lockUI();
                            var url = 'api/MstHRLoanHRDocument/DeleteHRDocument';
                            SocketService.getparams(url, params).then(function (resp) {
                                unlockUI();
                                if (resp.data.status == true) {
                                    SweetAlert.swal('Deleted Successfully!');
                                    activate();
                                }
                                else {
                                    Notify.alert(resp.data.message, {
                                        status: 'warning',
                                        pos: 'top-center',
                                        timeout: 3000
                                    });
                                    activate();
                                    unlockUI();
                                }
                            });
                            }
                    });
        }
        }
})();


(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstHRLoanHRDocumentEditController', MstHRLoanHRDocumentEditController);

    MstHRLoanHRDocumentEditController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams','cmnfunctionService'];

    function MstHRLoanHRDocumentEditController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams,cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstHRLoanHRDocumentEditController';
        $scope.hrdocument_gid = cmnfunctionService.decryptURL($location.search().hash).lshrdocument_gid;
        var hrdocument_gid = $scope.hrdocument_gid;

        activate();
        lockUI();
        function activate() {

            var url = 'api/MstHRLoanHRDocument/HRDocumentCheckListTempClear';
            SocketService.get(url).then(function (resp) {
            });

            var url = 'api/MstHRLoanHRDocument/GetHRDocumentDropDown';
            SocketService.get(url).then(function (resp) {
                $scope.document_list = resp.data.document_list;
                $scope.hrdocumentseverity_list = resp.data.hrdocumentseverity_list;

            });

            var param = {

                hrdocument_gid: hrdocument_gid
            };
            var url = 'api/MstHRLoanHRDocument/GetHRDocumentCheckListEditList';

            SocketService.getparams(url, param).then(function (resp) {
                $scope.checklistedit_list = resp.data.checklist_list;
                unlockUI();
            });
            var params = {
                hrdocument_gid: hrdocument_gid
            }
            var url = 'api/MstHRLoanHRDocument/EditHRDocument';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.txtedithrdocument_name = resp.data.hrdocument_name;
                $scope.cbohrdocument = resp.data.hrloantypeoffinancialassistance_gid;
                $scope.cbohrdocumentseverity = resp.data.hrloanseverity_gid;
                $scope.txtlms_code = resp.data.lms_code;
                $scope.txtbureau_code = resp.data.bureau_code;
                // $scope.rdbCovenant_type = resp.data.covenant_type;
                $scope.Status = resp.data.Status;
                unlockUI();
            });

           
        }
       

        $scope.ok = function () {
            $modalInstance.close('closed');
        };
      
        $scope.Back = function () {
            $state.go('app.MstHRLoanHRDocument');

        }
        $scope.update = function () {

            var hrloantypeoffinancialassistance_name = $('#Document :selected').text();
            var hrloanseverity_name = $('#Severity :selected').text();

            lockUI();
            var url = 'api/MstHRLoanHRDocument/UpdateHRDocument';
            var params = {

                hrdocument_gid: hrdocument_gid,
                hrdocument_name: $scope.txtedithrdocument_name,
                hrloantypeoffinancialassistance_gid: $scope.cbohrdocument,
                hrloantypeoffinancialassistance_name: hrloantypeoffinancialassistance_name,
                hrloanseverity_gid: $scope.cbohrdocumentseverity,
                hrloanseverity_name: hrloanseverity_name,
                lms_code: $scope.txtlms_code,
                bureau_code: $scope.txtbureau_code,
                // covenant_type: $scope.rdbCovenant_type,
                Status: $scope.Status
            }
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    $state.go('app.MstHRLoanHRDocument');
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

        $scope.checklist_edit = function () {
            if (($scope.txtchecklist == undefined) || ($scope.txtchecklist == '')) {
                Notify.alert('Enter Check List', 'warning');
            }
            else {

                var params = {
                    hrdocumentchecklist_name: $scope.txtchecklist,

                }
                lockUI();
                var url = 'api/MstHRLoanHRDocument/CreateHRDocumentCheckList';
                SocketService.post(url, params).then(function (resp) {
                    unlockUI();
                    if (resp.data.status == true) {
                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });
                    }
                    else {
                        Notify.alert(resp.data.message, {
                            status: 'warning',
                            pos: 'top-center',
                            timeout: 3000
                        });
                    }
                    checklist();
                    $scope.txtchecklist = '';

                });
            }
        }

        function checklist() {
            var params = {

                hrdocument_gid: hrdocument_gid
            };
            var url = 'api/MstHRLoanHRDocument/GetHRDocumentCheckListTempEditList';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.checklistedit_list = resp.data.hrloanhrdocument_list;
                unlockUI();
            });
        }

        $scope.checklist_delete = function (hrdocumentchecklist_gid) {
            var params =
                {
                    hrdocumentchecklist_gid: hrdocumentchecklist_gid
                }
            lockUI();
            var url = 'api/MstHRLoanHRDocument/DeleteHRDocumentCheckList';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'warning',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
                checklist();
            });
        }

    }
    //    }
    //}
})();

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstHRLoanHRDocumentViewController', MstHRLoanHRDocumentViewController);

    MstHRLoanHRDocumentViewController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams','cmnfunctionService'];

    function MstHRLoanHRDocumentViewController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams,cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstHRLoanHRDocumentViewController';
        $scope.hrdocument_gid = cmnfunctionService.decryptURL($location.search().hash).lshrdocument_gid;
        var hrdocument_gid = $scope.hrdocument_gid;

        activate();

        function activate() {

            var param = {

                hrdocument_gid: hrdocument_gid
            };
            var url = 'api/MstHRLoanHRDocument/GetHRDocumentCheckListEditList';

            SocketService.getparams(url, param).then(function (resp) {
                $scope.checklistview_list = resp.data.checklist_list;
                unlockUI();
            });
            var params = {
                hrdocument_gid: hrdocument_gid
            }
            var url = 'api/MstHRLoanHRDocument/EditHRDocument';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.lblhrdocument_name = resp.data.hrdocument_name;
                $scope.lblhrdocument = resp.data.hrloantypeoffinancialassistance_name;
                $scope.lblhrdocumentseverity = resp.data.hrloanseverity_name;
                $scope.lbllms_code = resp.data.lms_code;
                $scope.lblbureau_code = resp.data.bureau_code;
                // $scope.lblCovenanttype = (resp.data.covenant_type=='N') ? 'No' :'Yes';
                unlockUI();
            });
        }
       
        $scope.Back = function () {
            $state.go('app.MstHRLoanHRDocument');

        }
        }
})();

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstHRLoanHRMappingApprovalsController',MstHRLoanHRMappingApprovalsController );

        MstHRLoanHRMappingApprovalsController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function MstHRLoanHRMappingApprovalsController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstHRLoanHRMappingApprovalsController';

        activate();

        function activate() {
            var url = 'api/MstHRLoanHRMappingApprovals/GetHRMapping';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.hrmapping_list = resp.data.hrmapping_list;
                unlockUI();
            });
            var url = 'api/MstHRLoanHRMappingApprovals/GetEmployeelist';
                lockUI();
                SocketService.get(url).then(function (resp) {
                    $scope.employee_list = resp.data.employeelist;
                    unlockUI();
                });
           
        }

        $scope.hrmappingadd = function () {
            var modalInstance = $modal.open({
                templateUrl: '/addhrmapping.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });

            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                var url = 'api/MstHRLoanHRMappingApprovals/GetEmployeelist';
                lockUI();
                SocketService.get(url).then(function (resp) {
                    $scope.employee_list = resp.data.employeelist;
                    unlockUI();
                });               

                $scope.submit = function () {
                    if (($scope.hrmapping_name == 'Approver') && ($scope.cboemployee_apr == '' || $scope.cboemployee_apr == null) )
                {                 
                   Notify.alert('Kindly Fill Appover details', 'warning')
                       
            }
            else if (($scope.hrmapping_name == 'Manager') && ($scope.cboemployee == '' || $scope.cboemployee == null)) {
                Notify.alert('Kindly Fill Manager details', 'warning')
            }else{
                if($scope.hrmapping_name == 'Approver'){
                    var params = {
                       
                        employee_gid: $scope.cboemployee_apr.employee_gid,
                        employee_name: $scope.cboemployee_apr.employee_name,
                        hrmapping_name: $scope.hrmapping_name,
                        hrmapping_code: $scope.txthrmapping_code,
                        lms_code: $scope.txtlms_code,
                        bureau_code: $scope.txtbureau_code
                    }
                    console.log( $scope.cboemployee_apr.employee_gid);
                }else if($scope.hrmapping_name == 'Manager'){
                    var params = {
                       
                        employee: $scope.cboemployee,
                        hrmapping_name: $scope.hrmapping_name,
                        hrmapping_code: $scope.txthrmapping_code,
                        lms_code: $scope.txtlms_code,
                        bureau_code: $scope.txtbureau_code
                    }
                }
                    var url = 'api/MstHRLoanHRMappingApprovals/CreateHRMapping';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {

                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            $modalInstance.close('closed');
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

                    $modalInstance.close('closed');
                }
                }

            }
        }

        $scope.edithrmapping = function (hrmapping_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/edithrmapping.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });

            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                
                var url = 'api/MstHRLoanHRMappingApprovals/GetEmployeelist';
                lockUI();
                SocketService.get(url).then(function (resp) {
                    $scope.employee_list = resp.data.employeelist;
                    unlockUI();
                });
               
                var params = {
                    hrmapping_gid: hrmapping_gid
                }
                var url = 'api/MstHRLoanHRMappingApprovals/EditHRMapping';
                SocketService.getparams(url, params).then(function (resp) {
                   
                   
                    $scope.txtedithrmapping_code = resp.data.hrmapping_code;                    
                    $scope.hrmapping_name = resp.data.hrmapping_name;
                    $scope.hrmapping_gid = resp.data.hrmapping_gid;
                    $scope.txteditlms_code = resp.data.lms_code;
                    $scope.txteditbureau_code = resp.data.bureau_code;
                    $scope.cboemployee_editapr = resp.data.employee_name;
                    $scope.cboemployee_editlist = resp.data.employee;
                    $scope.employeeem_list = resp.data.employeeem_list;
                    $scope.cboemployee_edit = [];
                    if (resp.data.employeeem_list != null) {
                        var count = resp.data.employeeem_list.length;
                        for (var i = 0; i < count; i++) {
                            var Index = $scope.cboemployee_editlist.map(function (x) { return x.employee_gid; }).indexOf(resp.data.employeeem_list[i].employee_gid);                           
                            $scope.cboemployee_edit.push($scope.cboemployee_editlist[Index]);                           
                        }
                    }
               
                });


                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.update = function () {
                    
                    //var employee_name;
                    //var employee_index = $scope.employee_list.map(function (e) { return e.employee_gid }).indexOf($scope.cboemployee_editapr);
                    //if (employee_index == -1) { employee_name = ''; } else { employee_name = $scope.employee_list[employee_index].employee_name; };
                    if (($scope.hrmapping_name == 'Approver') && ($scope.cboemployee_editapr == '' || $scope.cboemployee_editapr == null) )
                    {                 
                       Notify.alert('Kindly Fill Appover details', 'warning')
                           
                }
                else if (($scope.hrmapping_name == 'Manager') && ($scope.cboemployee_edit == '' || $scope.cboemployee_edit == null)) {
                    Notify.alert('Kindly Fill Manager details', 'warning')
                }else{
                    if($scope.hrmapping_name == 'Approver'){
                    
                    var params = {
                        employee_gid: $scope.cboemployee_editapr.employee_gid, 
                        employee_name: $scope.cboemployee_editapr.employee_name,                       
                        hrmapping_name: $scope.hrmapping_name,
                        hrmapping_code: $scope.txtedithrmapping_code,
                        lms_code: $scope.txteditlms_code,
                        bureau_code: $scope.txteditbureau_code,
                        hrmapping_gid: $scope.hrmapping_gid,                     
                       
                    }
                }else if($scope.hrmapping_name == 'Manager'){
                    var params = {
                        employee: $scope.cboemployee_edit,                        
                        hrmapping_name: $scope.hrmapping_name,
                        hrmapping_code: $scope.txtedithrmapping_code,
                        lms_code: $scope.txteditlms_code,
                        bureau_code: $scope.txteditbureau_code,
                        hrmapping_gid: $scope.hrmapping_gid,            
                       
                    }
                }
                var url = 'api/MstHRLoanHRMappingApprovals/UpdateHRMapping';
                    SocketService.post(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            $modalInstance.close('closed');
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            activate();

                        }
                        else {
                            $modalInstance.close('closed');
                            Notify.alert(resp.data.message, {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });

                        }
                    });
                    $modalInstance.close('closed');
                }
                }
            }
        }
        $scope.showPopover = function (hrmapping_gid, hrmapping_name) {
            var modalInstance = $modal.open({
                templateUrl: '/showemployee.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    hrmapping_gid: hrmapping_gid
                }
                lockUI();
                var url = 'api/MstHRLoanHRMappingApprovals/GetEmployeeName';
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();                  
                    $scope.employee_name = resp.data.employee_name;
                    $scope.hrmapping_name = resp.data.hrmapping_name;
                });
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
            }
        }
        $scope.Status_update = function (hrmapping_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/statushrmapping.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                var params = {
                    hrmapping_gid: hrmapping_gid
                }
                var url = 'api/MstHRLoanHRMappingApprovals/EditHRMapping';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.hrmapping_gid = resp.data.hrmapping_gid
                    $scope.hrmapping_name = resp.data.hrmapping_name;
                    $scope.rbo_status = resp.data.Status;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.update_status = function () {

                    var params = {
                        hrmapping_gid: hrmapping_gid,
                        hrmapping_name: $scope.hrmapping_name,
                        remarks: $scope.txtremarks,
                        rbo_status: $scope.rbo_status

                    }
                    var url = 'api/MstHRLoanHRMappingApprovals/InactiveHRMapping';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {

                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });

                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                        }
                        activate();
                    });

                    $modalInstance.close('closed');

                }

                var param = {
                    hrmapping_gid: hrmapping_gid
                }

                var url = 'api/MstHRLoanHRMappingApprovals/HRMappingInactiveLogview';
                lockUI();
                SocketService.getparams(url, param).then(function (resp) {
                    $scope.hrmappinginactivelog_list = resp.data.hrmapping_list;
                    unlockUI();
                });

            }
        }

        $scope.deletehrmapping = function (hrmapping_gid,hrmapping_name) {
            var params = {
                hrmapping_gid: hrmapping_gid,
                hrmapping_name:hrmapping_name
            }


            SweetAlert.swal({
                title: 'Are you sure?',
                text: 'Do You Want To Delete the Record ?',
                showCancelButton: true,
                confirmButtonColor: '#DD6B55',
                confirmButtonText: 'Yes, delete it!',
                closeOnConfirm: false
            }, function (isConfirm) {
                if (isConfirm) {
                    lockUI();
                    var url = 'api/MstHRLoanHRMappingApprovals/DeleteHRMapping';
                    SocketService.getparams(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {
                            SweetAlert.swal('Deleted Successfully!');
                            activate();
                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            activate();
                            unlockUI();
                        }
                    });                    
                }

            });
        };

    }
})();
(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstHRLoanPurposeController', MstHRLoanPurposeController);

    MstHRLoanPurposeController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function MstHRLoanPurposeController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams)
    {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstHRLoanPurposeController';

        activate();

        function activate() {
            var url = 'api/MstHRLoanPurpose/GetHRLoanPurpose';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.purpose_data = resp.data.hrloanpurpose_list;
                unlockUI();
            });
        }
        $scope.addpurpose = function () {
            var modalInstance = $modal.open({
                templateUrl: '/addpurpose.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.submit = function () {

                    var params = {
                        hrloanpurpose_name: $scope.txtpurpose_name,
                        purpose_note: $scope.txtpurpose_note,
                        mandatory: $scope.pur_mandatory,
                        lms_code: $scope.txtlms_code,
                        bureau_code: $scope.txtbureau_code

                    }
                    var url = 'api/MstHRLoanPurpose/CreateHRLoanPurpose';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {

                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            $modalInstance.close('closed');
                            activate();
                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            $modalInstance.close('closed');
                            activate();
                        }
                    });
                }

            }
        }
        $scope.editpurpose = function (hrloanpurpose_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/editpurpose.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                var params = {
                    hrloanpurpose_gid: hrloanpurpose_gid
                }
                var url = 'api/MstHRLoanPurpose/EditHRLoanPurpose';
                SocketService.getparams(url, params).then(function (resp) {

                    $scope.txteditpurpose_name = resp.data.hrloanpurpose_name;
                    $scope.txteditpurpose_note = resp.data.purpose_note;
                    $scope.editpur_mandatory = resp.data.mandatory;
                    $scope.txteditlms_code = resp.data.lms_code;
                    $scope.txteditbureau_code = resp.data.bureau_code;
                    $scope.hrloanpurpose_gid = resp.data.hrloanpurpose_gid;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.update = function () {

                    var url = 'api/MstHRLoanPurpose/UpdateHRLoanPurpose';
                    var params = {
                        hrloanpurpose_name: $scope.txteditpurpose_name,
                        purpose_note: $scope.txteditpurpose_note,
                        mandatory: $scope.editpur_mandatory,
                        lms_code: $scope.txteditlms_code,
                        bureau_code: $scope.txteditbureau_code,
                        hrloanpurpose_gid: $scope.hrloanpurpose_gid
                    }
                    SocketService.post(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            $modalInstance.close('closed');
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            activate();

                        }
                        else {
                            $modalInstance.close('closed');
                            Notify.alert(resp.data.message, {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            activate();
                        }
                    });
                }

            }
        }

        $scope.Status_update = function (hrloanpurpose_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/statuspurpose.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    hrloanpurpose_gid: hrloanpurpose_gid
                }
                var url = 'api/MstHRLoanPurpose/EditHRLoanPurpose';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.hrloanpurpose_gid = resp.data.hrloanpurpose_gid
                    $scope.txtpurpose_name = resp.data.hrloanpurpose_name;
                    $scope.txteditpurpose_note = resp.data.purpose_note;
                    $scope.editpur_mandatory = resp.data.mandatory;
                    $scope.rbo_status = resp.data.Status;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.update_status = function () {

                    var params = {
                        hrloanpurpose_gid: hrloanpurpose_gid,
                        hrloanpurpose_name: $scope.txtpurpose_name,
                        purpose_note: $scope.txtpurpose_note,
                        mandatory: $scope.pur_mandatory,
                        remarks: $scope.txtremarks,
                        rbo_status: $scope.rbo_status

                    }
                    var url = 'api/MstHRLoanPurpose/InactiveHRLoanPurpose';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {

                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });

                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                        } activate();
                    });

                    $modalInstance.close('closed');

                }
                var params = {
                    hrloanpurpose_gid: hrloanpurpose_gid
                }

                var url = 'api/MstHRLoanPurpose/InactiveHRLoanPurposeHistory';

                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.purposeinactivelog_data = resp.data.purposeinactivehistory_list;
                    unlockUI();
                });
            }
        }

        $scope.delete = function (hrloanpurpose_gid) {
            var params = {
                hrloanpurpose_gid: hrloanpurpose_gid
            }
            SweetAlert.swal({
                title: 'Are you sure?',
                text: 'Do You Want To Delete the Record ?',
                showCancelButton: true,
                confirmButtonColor: '#DD6B55',
                confirmButtonText: 'Yes, delete it!',
                closeOnConfirm: false
            }, function (isConfirm) {
                if (isConfirm) {
                    lockUI();
                    var url = 'api/MstHRLoanPurpose/DeleteHRLoanPurpose';
                    SocketService.getparams(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {
                            SweetAlert.swal('Deleted Successfully!');
                            activate();
                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            activate();
                            unlockUI();
                        }
                    });
                }
            });
        }
    }
})();
(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstHRLoanRaiseRequestCompletedController', MstHRLoanRaiseRequestCompletedController);

        MstHRLoanRaiseRequestCompletedController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams','cmnfunctionService'];

    function MstHRLoanRaiseRequestCompletedController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams,cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstHRLoanRaiseRequestCompletedController';

        activate();
        lockUI();
        function activate() {
            var url = 'api/MstHRLoanRequestCompleted/GetHRloanDetailsCompleted';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.hrloanrequestSummary_list = resp.data.hrloanrequestCompleted;               
                unlockUI();
            });
            var url = 'api/MstHRLoanRequest/GetHRloanDetailscount';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.lspendingrequests_count = resp.data.pendingrequests_count;
                $scope.lscompletedrequests_count = resp.data.completedrequests_count;
                $scope.lsrejectedrequests_count = resp.data.rejectedrequests_count;
                $scope.lswithdrawn_count = resp.data.withdrawn_count;                               
                $scope.lstotalcount = resp.data.totalcount;
          }); 
        }

        $scope.PendingRequests = function () {
            $state.go('app.MstHRLoanRaiseRequest');
        }

        $scope.completed_Requests = function () {
            $state.go('app.MstHRLoanRaiseRequestCompleted');
        }

        $scope.Rejected_Requests = function(){
            $state.go('app.MstHRLoanRaiseRequestRejected');
        }

        $scope.withdrawn_Requests = function () {
            $state.go('app.MstHRLoanRaiseRequestWithdrawn');
        } 
       
        $scope.viewrequests = function (request_gid) {
            $location.url('app/MstHRLoanViewRequest?hash=' + cmnfunctionService.encryptURL('request_gid=' + request_gid  ));
        }
        // $scope.viewrequests = function () {           
        //     $state.go('app.MstHRLoanViewRequest');
        // }
      
        $scope.addrequest = function () {
            $state.go('app.MstHRLoanAddRequest');

        }
    }
})();

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstHRLoanRaiseRequestController', MstHRLoanRaiseRequestController);

    MstHRLoanRaiseRequestController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams','cmnfunctionService'];

    function MstHRLoanRaiseRequestController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams,cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstHRLoanRaiseRequestController';

        activate();
        lockUI();
        function activate() {
            var url = 'api/MstHRLoanRequest/GetHRloanDetails';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.hrloanrequestSummary_list = resp.data.hrloanrequest;               
                unlockUI();
            });
            var url = 'api/MstHRLoanRequest/GetHRloanDetailscount';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.lspendingrequests_count = resp.data.pendingrequests_count;
                $scope.lscompletedrequests_count = resp.data.completedrequests_count;
                $scope.lsrejectedrequests_count = resp.data.rejectedrequests_count;
                $scope.lswithdrawn_count = resp.data.withdrawn_count;               
                $scope.lstotalcount = resp.data.totalcount;
          }); 
        }

        $scope.PendingRequests = function () {
            $state.go('app.MstHRLoanRaiseRequest');
        }

        $scope.completed_Requests = function () {
            $state.go('app.MstHRLoanRaiseRequestCompleted');
        }

        $scope.Rejected_Requests = function(){
            $state.go('app.MstHRloanRaiseRequestRejected');
        }

        $scope.withdrawn_Requests = function () {
            $state.go('app.MstHRLoanRaiseRequestWithdrawn');
        } 

        $scope.editrequests = function (request_gid,employee_gid) {
            $location.url('app/MstHRLoanEditRequest?hash=' + cmnfunctionService.encryptURL('request_gid=' + request_gid + '&employee_gid=' + employee_gid));
        }

        $scope.view360 = function (request_gid,employee_gid) {
            $location.url('app/MstHRLoanEditRequest360?hash=' + cmnfunctionService.encryptURL('request_gid=' + request_gid + '&employee_gid=' + employee_gid));
        }
        // $scope.editrequests = function () {
        //     $state.go('app.MstHRLoanEditRequest');
        // }
        // $scope.viewrequests = function (request_gid) {
        //     $location.url('app/MstHRLoanViewRequest?request_gid=' + request_gid  );
        // }
        // $scope.viewrequests = function () {           
        //     $state.go('app.MstHRLoanViewRequest');
        // }
      
        $scope.addrequest = function () {
            $state.go('app.MstHRLoanAddRequest');

        }

        $scope.withdraw = function (request_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/withdraw.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                               
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                var params = {
                    request_gid: request_gid
                }
                var url = 'api/MstHRLoanRequest/EditHRLoanRequest';
                SocketService.getparams(url, params).then(function (resp) { 
                    $scope.lsrequest_refno = resp.data.request_refno;
                    unlockUI();
                });
                $scope.submit_withdraw = function () {

                    var params = {
                        request_gid: request_gid,
                        withdraw_remarks: $scope.txtwithdraw_remarks,                       
                    }
                    var url = 'api/MstHRLoanRequest/PostHrLoanwithdrawUpdate';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {

                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            $modalInstance.close('closed');
                            activate();
                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            $modalInstance.close('closed');
                            activate();
                        }
                    });
                }
                
            }
        }

    }
})();

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstHRLoanRaiseRequestRejectedController', MstHRLoanRaiseRequestRejectedController);

        MstHRLoanRaiseRequestRejectedController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams','cmnfunctionService'];

    function MstHRLoanRaiseRequestRejectedController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams,cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstHRLoanRaiseRequestRejectedController';

        activate();
        lockUI();
        function activate() {
            var url = 'api/MstHRLoanRequestCompleted/GetHRloanDetailsRejected';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.hrloanrequestSummary_list = resp.data.hrloanrequestRejected;               
                unlockUI();
            });
            var url = 'api/MstHRLoanRequest/GetHRloanDetailscount';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.lspendingrequests_count = resp.data.pendingrequests_count;
                $scope.lscompletedrequests_count = resp.data.completedrequests_count;
                $scope.lsrejectedrequests_count = resp.data.rejectedrequests_count;
                $scope.lswithdrawn_count = resp.data.withdrawn_count;               
                $scope.lstotalcount = resp.data.totalcount;
          }); 
        }

        $scope.PendingRequests = function () {
            $state.go('app.MstHRLoanRaiseRequest');
        }

        $scope.completed_Requests = function () {
            $state.go('app.MstHRLoanRaiseRequestCompleted');
        }

        $scope.Rejected_Requests = function() {
            $state.go('app.MstHRLoanRaiseRequestRejected');            
        }

        $scope.withdrawn_Requests = function () {
            $state.go('app.MstHRLoanRaiseRequestWithdrawn');
        } 
       
        $scope.viewrequests = function (request_gid) {
            $location.url('app/MstHRLoanViewRequest?hash=' + cmnfunctionService.encryptURL('request_gid=' + request_gid  ));
        }
        // $scope.viewrequests = function () {           
        //     $state.go('app.MstHRLoanViewRequest');
        // }
      
        $scope.addrequest = function () {
            $state.go('app.MstHRLoanAddRequest');

        }
    }
})();

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstHRLoanRaiseRequestWithdrawnController', MstHRLoanRaiseRequestWithdrawnController);

        MstHRLoanRaiseRequestWithdrawnController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams','cmnfunctionService'];

    function MstHRLoanRaiseRequestWithdrawnController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams,cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstHRLoanRaiseRequestWithdrawnController';

        activate();
        lockUI();
        function activate() {
            var url = 'api/MstHRLoanRequestWithdrawn/GetHRloanDetailsWithdrawn';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.hrloanrequestSummary_list = resp.data.hrloanrequestWithdrawn;               
                unlockUI();
            });
            var url = 'api/MstHRLoanRequest/GetHRloanDetailscount';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.lspendingrequests_count = resp.data.pendingrequests_count;
                $scope.lscompletedrequests_count = resp.data.completedrequests_count;
                $scope.lsrejectedrequests_count = resp.data.rejectedrequests_count;
                $scope.lswithdrawn_count = resp.data.withdrawn_count;               
                $scope.lstotalcount = resp.data.totalcount;
          }); 
        }

        $scope.PendingRequests = function () {
            $state.go('app.MstHRLoanRaiseRequest');
        }

        $scope.completed_Requests = function () {
            $state.go('app.MstHRLoanRaiseRequestCompleted');
        }

        $scope.Rejected_Requests = function() {
            $state.go('app.MstHRLoanRaiseRequestRejected');
        }

        $scope.withdrawn_Requests = function () {
            $state.go('app.MstHRLoanRaiseRequestWithdrawn');
        } 
       
        $scope.viewrequests = function (request_gid) {
            $location.url('app/MstHRLoanViewRequest?hash=' + cmnfunctionService.encryptURL('request_gid=' + request_gid  ));
        }
        // $scope.viewrequests = function () {           
        //     $state.go('app.MstHRLoanViewRequest');
        // }
      
        $scope.addrequest = function () {
            $state.go('app.MstHRLoanAddRequest');

        }
    }
})();

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstHRLoanSeverityController', MstHRLoanSeverityController);

    MstHRLoanSeverityController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function MstHRLoanSeverityController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstHRLoanSeverityController';

        activate();

        function activate() {
            var url = 'api/MstHRLoanSeverity/GetHRLoanSeverity';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.severity_data = resp.data.hrloanseverity_list;
                unlockUI();
            });
        }
        $scope.addseverity = function () {
            var modalInstance = $modal.open({
                templateUrl: '/addseverity.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.submit = function () {

                    var params = {
                        hrloanseverity_name: $scope.txtseverity_name,
                        lms_code: $scope.txtlms_code,
                        bureau_code: $scope.txtbureau_code

                    }
                    var url = 'api/MstHRLoanSeverity/CreateHRLoanSeverity';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {

                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            $modalInstance.close('closed');
                            activate();
                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            $modalInstance.close('closed');
                            activate();
                        }
                    });
                }

            }
        }
        $scope.editseverity = function (hrloanseverity_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/editseverity.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                var params = {
                    hrloanseverity_gid: hrloanseverity_gid
                }
                var url = 'api/MstHRLoanSeverity/EditHRLoanSeverity';
                SocketService.getparams(url, params).then(function (resp) {

                    $scope.txteditseverity_name = resp.data.hrloanseverity_name;
                    $scope.txteditlms_code = resp.data.lms_code;
                    $scope.txteditbureau_code = resp.data.bureau_code;
                    $scope.hrloanseverity_gid = resp.data.hrloanseverity_gid;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.update = function () {

                    var url = 'api/MstHRLoanSeverity/UpdateHRLoanSeverity';
                    var params = {
                        hrloanseverity_name: $scope.txteditseverity_name,
                        lms_code: $scope.txteditlms_code,
                        bureau_code: $scope.txteditbureau_code,
                        hrloanseverity_gid: $scope.hrloanseverity_gid
                    }
                    SocketService.post(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            $modalInstance.close('closed');
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            activate();

                        }
                        else {
                            $modalInstance.close('closed');
                            Notify.alert(resp.data.message, {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            activate();
                        }
                    });
                }

            }
        }

        $scope.Status_update = function (hrloanseverity_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/statusseverity.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    hrloanseverity_gid: hrloanseverity_gid
                }
                var url = 'api/MstHRLoanSeverity/EditHRLoanSeverity';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.hrloanseverity_gid = resp.data.hrloanseverity_gid
                    $scope.txtseverity_name = resp.data.hrloanseverity_name;
                    $scope.rbo_status = resp.data.Status;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.update_status = function () {

                    var params = {
                        hrloanseverity_gid: hrloanseverity_gid,
                        hrloanseverity_name: $scope.txtseverity_name,
                        remarks: $scope.txtremarks,
                        rbo_status: $scope.rbo_status

                    }
                    var url = 'api/MstHRLoanSeverity/InactiveHRLoanSeverity';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {

                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });

                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                        } activate();
                    });

                    $modalInstance.close('closed');

                }
                var params = {
                    hrloanseverity_gid: hrloanseverity_gid
                }

                var url = 'api/MstHRLoanSeverity/InactiveHRLoanSeverityHistory';

                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.severityinactivelog_data = resp.data.severityinactivehistory_list;
                    unlockUI();
                });
            }
        }

        $scope.delete = function (hrloanseverity_gid) {
            var params = {
                hrloanseverity_gid: hrloanseverity_gid
            }
            SweetAlert.swal({
                title: 'Are you sure?',
                text: 'Do You Want To Delete the Record ?',
                showCancelButton: true,
                confirmButtonColor: '#DD6B55',
                confirmButtonText: 'Yes, delete it!',
                closeOnConfirm: false
            }, function (isConfirm) {
                if (isConfirm) {
                    lockUI();
                    var url = 'api/MstHRLoanSeverity/DeleteHRLoanSeverity';
                    SocketService.getparams(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {
                            SweetAlert.swal('Deleted Successfully!');
                            activate();
                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            activate();
                            unlockUI();
                        }
                    });
                }
            });
        }
    }
})();
(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstHRLoanTenureAddController', MstHRLoanTenureAddController);

        MstHRLoanTenureAddController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function MstHRLoanTenureAddController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstHRLoanTenureAddController';

        activate();

        function activate() { 
            var url = 'api/MstHRLoanTenure/GetHRLoanTenureDropDown';
            SocketService.get(url).then(function (resp) {
                $scope.typeofdocument_list = resp.data.typeofdocument_list;          

            });
        }
       
        $scope.ok = function () {
            $modalInstance.close('closed');
        };
      
        $scope.Back = function () {
            $state.go('app.MstHRLoanTenure');

        }
        $scope.submit = function () {
            var lshrloantypeoffinancialassistance_gid = '';
            var lshrloantypeoffinancialassistance_name = '';
            if ($scope.cbohrdocument != undefined || $scope.cbohrdocument != null) {
                lshrloantypeoffinancialassistance_gid = $scope.cbohrdocument.hrloantypeoffinancialassistance_gid;
                lshrloantypeoffinancialassistance_name = $scope.cbohrdocument.hrloantypeoffinancialassistance_name;
            }

            var params = {
                hrloantenure_name: $scope.txttenure_name,
                hrloantypeoffinancialassistance_gid: lshrloantypeoffinancialassistance_gid,
                hrloantypeoffinancialassistance_name: lshrloantypeoffinancialassistance_name,               
                lms_code: $scope.txtlms_code,
                bureau_code: $scope.txtbureau_code,
                 
            }
            lockUI();
            var url = 'api/MstHRLoanTenure/CreateHRLoanTenure';
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    $state.go('app.MstHRLoanTenure');
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

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstHRLoanTenureController', MstHRLoanTenureController);

        MstHRLoanTenureController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams','cmnfunctionService'];

    function MstHRLoanTenureController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams,cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstHRLoanTenureController';

        activate();

        function activate() {
           
            var url = 'api/MstHRLoanTenure/GetHRLoanTenure';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.tenure_data = resp.data.hrloantenure_list;
                $scope.typeofdocument_list = resp.data.typeofdocument_list;                
                unlockUI();
            });
        }
        $scope.addtenure = function () {
            $state.go('app.MstHRLoanTenureAdd');
        }
       

        $scope.viewtenure = function (hrloantenure_gid) {
            $location.url('app/MstHRLoanTenureView?hash=' + cmnfunctionService.encryptURL('lshrloantenure_gid=' + hrloantenure_gid));
        }

        $scope.edittenure = function (hrloantenure_gid) {
            $location.url('app/MstHRLoanTenureEdit?hash=' + cmnfunctionService.encryptURL('lshrloantenure_gid=' + hrloantenure_gid));
        }
       
        $scope.Status_update = function (hrloantenure_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/statustenure.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                var params = {
                    hrloantenure_gid: hrloantenure_gid
                }
                var url = 'api/MstHRLoanTenure/EditHRLoanTenure';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.hrloantenure_gid = resp.data.hrloantenure_gid
                    $scope.hrloantenure_name = resp.data.hrloantenure_name;
                    $scope.rbo_status = resp.data.Status;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.update_status = function () {

                    var params = {
                        hrloantenure_gid: hrloantenure_gid,
                        hrloantenure_name: $scope.hrloantenure_name,
                        remarks: $scope.txtremarks,
                        rbo_status: $scope.rbo_status

                    }
                    var url = 'api/MstHRLoanTenure/InactiveHRLoanTenure';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {

                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });

                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'info',
                                pos: 'top-center',
                                timeout: 3000
                            });
                        }
                        activate();
                    });

                    $modalInstance.close('closed');

                }

                var param = {
                    hrloantenure_gid: hrloantenure_gid
                }

                var url = 'api/MstHRLoanTenure/InactiveHRLoanTenureHistory';
                lockUI();
                SocketService.getparams(url, param).then(function (resp) {
                    $scope.tenureinactivelog_data = resp.data.tenureinactivehistory_list;
                    unlockUI();
                });

            }
        }

        $scope.Add_tenure = function (hrloantenure_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/addtenure.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.open1 = function ($event) {
                    $event.preventDefault();
                    $event.stopPropagation();
                    $scope.opened1 = true;
                };
            
                $scope.minDate = new Date();

                $scope.formats = ['dd-MM-yyyy'];
                $scope.format = $scope.formats[0];
                $scope.dateOptions = {
                    formatYear: 'yy',
                    startingDay: 1
                };              
                

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.add_tenure = function () {
                    
                    var params = {
                        hrloantenure_gid: hrloantenure_gid, 
                        hrloantenure_name: $scope.txttenure_name,                        
                        hrloantenurestart_date: $scope.txttenurestart_date,                  
                         
                    }
                    var url = 'api/MstHRLoanTenure/CreateHRLoanTenureUpdate';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {

                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });

                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'info',
                                pos: 'top-center',
                                timeout: 3000
                            });
                        }
                        activate();
                    });

                    $modalInstance.close('closed');
                

                }
                              

            }
        }


        $scope.delete = function (hrloantenure_gid,hrloantypeoffinancialassistance_gid) {
            var params = {
                hrloantenure_gid: hrloantenure_gid,
                hrloantypeoffinancialassistance_gid: hrloantypeoffinancialassistance_gid
                   }
            
                    SweetAlert.swal({
                        title: 'Are you sure?',
                        text: 'Do You Want To Delete the Record ?',
                        showCancelButton: true,
                        confirmButtonColor: '#DD6B55',
                        confirmButtonText: 'Yes, delete it!',
                        closeOnConfirm: false
                    }, function (isConfirm) {
                        if (isConfirm) {
                            lockUI();
                            var url = 'api/MstHRLoanTenure/DeleteHRLoanTenure';
                            SocketService.getparams(url, params).then(function (resp) {
                                unlockUI();
                                if (resp.data.status == true) {
                                    SweetAlert.swal('Deleted Successfully!');
                                    activate();
                                }
                                else {
                                    Notify.alert(resp.data.message, {
                                        status: 'warning',
                                        pos: 'top-center',
                                        timeout: 3000
                                    });
                                    activate();
                                    unlockUI();
                                }
                            });
                            }
                    });
        }
        }
})();


(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstHRLoanTenureEditController', MstHRLoanTenureEditController);

    MstHRLoanTenureEditController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams','cmnfunctionService'];

    function MstHRLoanTenureEditController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams,cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstHRLoanTenureEditController';
        $scope.hrloantenure_gid = cmnfunctionService.decryptURL($location.search().hash).lshrloantenure_gid;
        var hrloantenure_gid = $scope.hrloantenure_gid;

        activate();
        lockUI();
        function activate() {
            

            var url = 'api/MstHRLoanTenure/GetHRLoanTenureDropDown';
            SocketService.get(url).then(function (resp) {
                $scope.typeofdocument_list = resp.data.typeofdocument_list;
                // $scope.hrdocumentseverity_list = resp.data.hrdocumentseverity_list;

            });

            var params = {
                hrloantenure_gid: hrloantenure_gid
            }
            var url = 'api/MstHRLoanTenure/EditHRLoanTenure';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.txtedittenure_name = resp.data.hrloantenure_name;
                $scope.cbohrdocument = resp.data.hrloantypeoffinancialassistance_gid;
                $scope.txttenurestart_date = resp.data.hrloantenurestart_date;
                // $scope.txttenureend_date = resp.data.hrloantenureend_date;
                $scope.txtlms_code = resp.data.lms_code;
                $scope.txtbureau_code = resp.data.bureau_code;                
                $scope.Status = resp.data.Status;
                unlockUI();
            });

           
        }
       

        $scope.ok = function () {
            $modalInstance.close('closed');
        };
      
        $scope.Back = function () {
            $state.go('app.MstHRLoanTenure');

        }
        $scope.update = function () {

            var hrloantypeoffinancialassistance_name = $('#Document :selected').text();
            // var hrloanseverity_name = $('#Severity :selected').text();

            lockUI();
            var url = 'api/MstHRLoanTenure/UpdateHRLoanTenure';
            var params = {

                hrloantenure_gid: hrloantenure_gid,
                hrloantenure_name: $scope.txtedittenure_name,
                hrloantypeoffinancialassistance_gid: $scope.cbohrdocument,
                hrloantypeoffinancialassistance_name: hrloantypeoffinancialassistance_name,
                hrloantenurestart_date: $scope.txttenurestart_date,
                // hrloantenureend_date: $scope.txttenureend_date,
                lms_code: $scope.txtlms_code,
                bureau_code: $scope.txtbureau_code,               
                Status: $scope.Status
            }
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    $state.go('app.MstHRLoanTenure');
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

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstHRLoanTenureViewController', MstHRLoanTenureViewController);

    MstHRLoanTenureViewController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams','cmnfunctionService'];

    function MstHRLoanTenureViewController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams,cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstHRLoanTenureViewController';
        $scope.hrloantenure_gid = cmnfunctionService.decryptURL($location.search().hash).lshrloantenure_gid;
        var hrloantenure_gid = $scope.hrloantenure_gid;

        activate();

        function activate() {

            var params = {
                hrloantenure_gid: hrloantenure_gid
            }
            var url = 'api/MstHRLoanTenure/EditHRLoanTenure';
            SocketService.getparams(url, params).then(function (resp) {                
                $scope.lblhrdocument = resp.data.hrloantypeoffinancialassistance_name;
                $scope.lbltenure = resp.data.hrloantenure_name;
                $scope.lbltenurestartdate = resp.data.hrloantenurestart_date;               
                $scope.lbllms_code = resp.data.lms_code;
                $scope.lblbureau_code = resp.data.bureau_code;               
                unlockUI();
            });

            var url = 'api/MstHRLoanTenure/Gettenurelog';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                $scope.tenurelog_details = resp.data.hrloantenure_list;                              
                unlockUI();
            });
        }
       
        $scope.Back = function () {
            $state.go('app.MstHRLoanTenure');

        }
        }
})();

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstHRLoanTermsandConditionsController', MstHRLoanTermsandConditionsController);

    MstHRLoanTermsandConditionsController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function MstHRLoanTermsandConditionsController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstHRLoanTermsandConditionsController';

        activate();

        function activate() {
            var url = 'api/MstHRLoanTermsandConditions/GetHRLoanTermsandConditions';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.termsandconditions_data = resp.data.hrloantermsandconditions_list;
                unlockUI();
            });
        }
        $scope.addtermsandconditions = function () {
            var modalInstance = $modal.open({
                templateUrl: '/addtermsandconditions.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.submit = function () {

                    var params = {
                        hrloantermsandconditions_name: $scope.txttermsandconditions_name,
                        lms_code: $scope.txtlms_code,
                        bureau_code: $scope.txtbureau_code

                    }
                    var url = 'api/MstHRLoanTermsandConditions/CreateHRLoanTermsandConditions';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {

                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            $modalInstance.close('closed');
                            activate();
                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            $modalInstance.close('closed');
                            activate();
                        }
                    });
                }

            }
        }
        $scope.edittermsandconditions = function (hrloantermsandconditions_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/edittermsandconditions.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                var params = {
                    hrloantermsandconditions_gid: hrloantermsandconditions_gid
                }
                var url = 'api/MstHRLoanTermsandConditions/EditHRLoanTermsandConditions';
                SocketService.getparams(url, params).then(function (resp) {

                    $scope.txtedittermsandconditions_name = resp.data.hrloantermsandconditions_name;
                    $scope.txteditlms_code = resp.data.lms_code;
                    $scope.txteditbureau_code = resp.data.bureau_code;
                    $scope.hrloantermsandconditions_gid = resp.data.hrloantermsandconditions_gid;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.update = function () {

                    var url = 'api/MstHRLoanTermsandConditions/UpdateHRLoanTermsandConditions';
                    var params = {
                        hrloantermsandconditions_name: $scope.txtedittermsandconditions_name,
                        lms_code: $scope.txteditlms_code,
                        bureau_code: $scope.txteditbureau_code,
                        hrloantermsandconditions_gid: $scope.hrloantermsandconditions_gid
                    }
                    SocketService.post(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            $modalInstance.close('closed');
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            activate();

                        }
                        else {
                            $modalInstance.close('closed');
                            Notify.alert(resp.data.message, {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            activate();
                        }
                    });
                }

            }
        }

        $scope.Status_update = function (hrloantermsandconditions_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/statustermsandconditions.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    hrloantermsandconditions_gid: hrloantermsandconditions_gid
                }
                var url = 'api/MstHRLoanTermsandConditions/EditHRLoanTermsandConditions';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.hrloantermsandconditions_gid = resp.data.hrloantermsandconditionsgid
                    $scope.txttermsandconditions_name = resp.data.hrloantermsandconditions_name;
                    $scope.rbo_status = resp.data.Status;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.update_status = function () {

                    var params = {
                        hrloantermsandconditions_gid: hrloantermsandconditions_gid,
                        hrloantermsandconditions_name: $scope.txttermsandconditions_name,
                        remarks: $scope.txtremarks,
                        rbo_status: $scope.rbo_status

                    }
                    var url = 'api/MstHRLoanTermsandConditions/InactiveHRLoanTermsandConditions';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {

                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });

                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                        } activate();
                    });

                    $modalInstance.close('closed');

                }
                var params = {
                    hrloantermsandconditions_gid: hrloantermsandconditions_gid
                }

                var url = 'api/MstHRLoanTermsandConditions/InactiveHRLoanTermsandConditionsHistory';

                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.termsandconditionsinactivelog_data = resp.data.termsandconditionsinactivehistory_list;
                    unlockUI();
                });
            }
        }

        $scope.delete = function (hrloantermsandconditions_gid) {
            var params = {
                hrloantermsandconditions_gid: hrloantermsandconditions_gid
            }
            SweetAlert.swal({
                title: 'Are you sure?',
                text: 'Do You Want To Delete the Record ?',
                showCancelButton: true,
                confirmButtonColor: '#DD6B55',
                confirmButtonText: 'Yes, delete it!',
                closeOnConfirm: false
            }, function (isConfirm) {
                if (isConfirm) {
                    lockUI();
                    var url = 'api/MstHRLoanTermsandConditions/DeleteHRLoanTermsandConditions';
                    SocketService.getparams(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {
                            SweetAlert.swal('Deleted Successfully!');
                            activate();
                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            activate();
                            unlockUI();
                        }
                    });
                }
            });
        }
    }
})();
(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstHRLoanTypeofFinancialAssistanceController', MstHRLoanTypeofFinancialAssistanceController);

        MstHRLoanTypeofFinancialAssistanceController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function MstHRLoanTypeofFinancialAssistanceController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstHRLoanTypeofFinancialAssistanceController';

        activate();

        function activate() {          
            var url = 'api/MstHRLoanTypeofFinancialAssistance/GetHRLoanTypeofFinancialAssistance';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.typeoffinancialassistance_data = resp.data.typeoffinancialassistance_list;
                unlockUI();
            });
        }
        $scope.addtypeoffinancialassistance = function () {
            var modalInstance = $modal.open({
                templateUrl: '/addfinancial.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                               
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.submit = function () {

                    var params = {
                        hrloantypeoffinancialassistance_name: $scope.txtfinancial_name,
                        lms_code: $scope.txtlms_code,
                        bureau_code: $scope.txtbureau_code
                    
                    }
                    var url = 'api/MstHRLoanTypeofFinancialAssistance/CreateHRLoanTypeofFinancialAssistance';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {

                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            $modalInstance.close('closed');
                            activate();
                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            $modalInstance.close('closed');
                            activate();
                        }
                    });
                }
                
            }
        }
        $scope.edittypeoffinancialassistance = function (hrloantypeoffinancialassistance_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/editfinancial.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                var params = {
                    hrloantypeoffinancialassistance_gid: hrloantypeoffinancialassistance_gid
                }
                var url = 'api/MstHRLoanTypeofFinancialAssistance/EditHRLoanTypeofFinancialAssistance';
                SocketService.getparams(url, params).then(function (resp) {

                    $scope.txteditfinancial_name = resp.data.hrloantypeoffinancialassistance_name;
                    $scope.txteditlms_code = resp.data.lms_code;
                    $scope.txteditbureau_code = resp.data.bureau_code;
                    $scope.hrloantypeoffinancialassistance_gid = resp.data.hrloantypeoffinancialassistance_gid;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.update = function () {

                    var url = 'api/MstHRLoanTypeofFinancialAssistance/UpdateHRLoanTypeofFinancialAssistance';
                    var params = {
                        hrloantypeoffinancialassistance_name: $scope.txteditfinancial_name,
                        lms_code: $scope.txteditlms_code,
                        bureau_code: $scope.txteditbureau_code,
                        hrloantypeoffinancialassistance_gid: $scope.hrloantypeoffinancialassistance_gid
                    }
                    SocketService.post(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            $modalInstance.close('closed');
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            activate();

                        }
                        else {
                            $modalInstance.close('closed');
                            Notify.alert(resp.data.message, {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            activate();
                        }
                    });
                }

            }
        }

        $scope.Status_update = function (hrloantypeoffinancialassistance_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/statusfinancial.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    hrloantypeoffinancialassistance_gid: hrloantypeoffinancialassistance_gid
                }            
                var url = 'api/MstHRLoanTypeofFinancialAssistance/EditHRLoanTypeofFinancialAssistance';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.hrloantypeoffinancialassistance_gid = resp.data.hrloantypeoffinancialassistance_gid
                    $scope.txtfinancial_name = resp.data.hrloantypeoffinancialassistance_name;
                    $scope.rbo_status = resp.data.Status;
                });    
                           
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.update_status = function () {

                    var params = {
                        hrloantypeoffinancialassistance_gid: hrloantypeoffinancialassistance_gid,
                        hrloantypeoffinancialassistance_name: $scope.txtfinancial_name,
                        remarks: $scope.txtremarks,
                        rbo_status:$scope.rbo_status
                    
                    }
                    var url = 'api/MstHRLoanTypeofFinancialAssistance/InactiveHRLoanTypeofFinancialAssistance';
                     lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {

                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            
                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                        }activate();
                    }); 

                    $modalInstance.close('closed');

                }
                var params = {
                    hrloantypeoffinancialassistance_gid: hrloantypeoffinancialassistance_gid
                }

                var url = 'api/MstHRLoanTypeofFinancialAssistance/InactiveHRLoanTypeofFinancialAssistanceHistory';

                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.typeoffinancialassistanceinactivelog_data = resp.data.inactivehistory_list;
                    unlockUI();
                }); 
            }
        }

        $scope.delete = function (hrloantypeoffinancialassistance_gid) {
            var params = {
                hrloantypeoffinancialassistance_gid: hrloantypeoffinancialassistance_gid
            }
           SweetAlert.swal({
                        title: 'Are you sure?',
                        text: 'Do You Want To Delete the Record ?',
                        showCancelButton: true,
                        confirmButtonColor: '#DD6B55',
                        confirmButtonText: 'Yes, delete it!',
                        closeOnConfirm: false
                    }, function (isConfirm) {
                        if (isConfirm) {
                            lockUI();
                            var url = 'api/MstHRLoanTypeofFinancialAssistance/DeleteHRLoanTypeofFinancialAssistance';
                            SocketService.getparams(url, params).then(function (resp) {
                                unlockUI();
                                if (resp.data.status == true) {
                                SweetAlert.swal('Deleted Successfully!');
                                activate();
                                }
                                else {
                                Notify.alert(resp.data.message, {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                                });
                                activate();
                                unlockUI();
                                }
                            });
                        }
                    });
        }
    }
})();
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
(function () {
    'use strict';

    angular
        .module('angle')
        .controller('RptEmployeeLoanReportController', RptEmployeeLoanReportController);

    RptEmployeeLoanReportController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams', 'DownloaddocumentService'];

    function RptEmployeeLoanReportController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams, DownloaddocumentService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'RptEmployeeLoanReportController';
        activate();
        function activate() {
            var url = 'api/RptEmployeeLoanReport/GetEmployeeLoanReportSummary';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.Report_list = resp.data.SummaryReport_list;
                unlockUI();
            });
        }

        var url = 'api/RptEmployeeLoanReport/GetReportCounts';
        SocketService.get(url).then(function (resp) {
            $scope.totalcount = resp.data.total_count;
            $scope.pendingcount = resp.data.pending_count;
            $scope.rejectedcount = resp.data.rejected_count;
            $scope.completedcount = resp.data.completed_count; 
            $scope.WithdrawnCount = resp.data.WithdrawnCount;            
        });       
        $scope.Loanreport = function () {
            lockUI();
            var url = 'api/RptEmployeeLoanReport/ExportLoanReport';
            SocketService.get(url).then(function (resp) {
                if (resp.data.status == true) {
                    unlockUI();
                    DownloaddocumentService.Downloaddocument(resp.data.lscloudpath, resp.data.lsname);
                 
                }
                else {
                    unlockUI();
                    Notify.alert('Error Occurred While Export !', 'success')

                }

            });
        }
    }
})();
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



(function () {
    'use strict';

    angular
        .module('angle')
        .controller('TrnHRLoanHRPaymentApprovedSummaryController', TrnHRLoanHRPaymentApprovedSummaryController);

    TrnHRLoanHRPaymentApprovedSummaryController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams','cmnfunctionService'];

    function TrnHRLoanHRPaymentApprovedSummaryController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams,cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'TrnHRLoanHRPaymentApprovedSummaryController';
        lockUI();
        activate();

        function activate() {
            lockUI();
            var url = 'api/TrnHRLoanHRPayment/GetHRloanDetailsApprovedPayment';
            SocketService.get(url).then(function (resp) {
                $scope.Approveddetails_list = resp.data.payment_summary;
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
            $location.url('app/TrnHRLoanHRPaymentSummary?hash=' + cmnfunctionService.encryptURL('request_gid=' + request_gid + '&employee_gid=' + employee_gid + '&lspage=HRApproved' + '&lsflag=N'));
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

            $location.url('app/TrnHRLoanHRPaymentApprovedView?hash=' + cmnfunctionService.encryptURL('request_gid=' + request_gid));

        }
        // $scope.applcreation_view = function (application_gid) {
        //     $location.url('app/MstApplicationCreationView?application_gid=' + application_gid + '&lstab=BusinessApproved');
        // }

    }
})();


(function () {
    'use strict';

    angular
        .module('angle')
        .controller('TrnHRLoanHRPaymentApprovedViewController', TrnHRLoanHRPaymentApprovedViewController);

    TrnHRLoanHRPaymentApprovedViewController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams','DownloaddocumentService','cmnfunctionService'];

    function TrnHRLoanHRPaymentApprovedViewController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams,DownloaddocumentService,cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'TrnHRLoanHRPaymentApprovedViewController';
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
       
        $scope.Back = function () {

            $state.go('app.TrnHRLoanHRPaymentApprovedSummary');
        }
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


    }
})();



(function () {
    'use strict';

    angular
        .module('angle')
        .controller('TrnHRLoanHRPaymentRejectedSummaryController', TrnHRLoanHRPaymentRejectedSummaryController);

    TrnHRLoanHRPaymentRejectedSummaryController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams','cmnfunctionService'];

    function TrnHRLoanHRPaymentRejectedSummaryController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams,cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'TrnHRLoanHRPaymentRejectedSummaryController';
        lockUI();
        activate();

        function activate() {
            lockUI();
            var url = 'api/MstHRLoanApprovalsRejected/GetHRloanHRheadRequestDetails';
            SocketService.get(url).then(function (resp) {
                $scope.requestdetails_list = resp.data.Rejectedrequestsummary;
                unlockUI();
            });
            //var url = '';
            //SocketService.get(url).then(function (resp) {
            //    unlockUI();
            //    $scope.newhrapprovals_count = resp.data.newhrapprovals_count;
            //    $scope.rejectedapprovals_count = resp.data.rejectedapprovals_count;
            //    $scope.approvedapprovals_count = resp.data.approvedapprovals_count;
            //    $scope.lstotalcount = resp.data.lstotalcount;
            //});
        }

        $scope.loan_approval = function (request_gid, employee_gid) {
            $location.url('app/AprHRLoanHRHeadApprovals?hash=' + cmnfunctionService.encryptURL('request_gid=' + request_gid + '&employee_gid=' + employee_gid + '&lspage=MyTeamApprovals'));
        }

        $scope.myapproval_loans = function () {
            $state.go('app.TrnHRLoanHRPaymentSummary');
        }
        $scope.approved_loans = function () {
            $state.go('app.TrnHRLoanHRPaymentApprovedSummary');
        }

        $scope.rejected_loans = function () {
            $state.go('app.TrnHRLoanHRPaymentRejectedSummaryController');
        }
        $scope.viewrequests = function (request_gid) {

            $location.url('app/TrnHRLoanHRPaymentRejectedView?hash=' + cmnfunctionService.encryptURL('request_gid=' + request_gid));

        }
        // $scope.applcreation_view = function (application_gid) {
        //     $location.url('app/MstApplicationCreationView?application_gid=' + application_gid + '&lstab=BusinessReject');
        // }

        // $scope.loan_approval = function (application_gid, employee_gid, applicationapproval_gid) {
        //     $location.url('app/AprHRLoanHRHeadApprovals?application_gid=' + application_gid + '&employee_gid=' + employee_gid +'&applicationapproval_gid=' + applicationapproval_gid + '&lspage=HRReject' + '&lsflag=N');         
        // }

    }
})();


(function () {
    'use strict';

    angular
        .module('angle')
        .controller('TrnHRLoanHRPaymentRejectedViewController', TrnHRLoanHRPaymentRejectedViewController);

    TrnHRLoanHRPaymentRejectedViewController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams','DownloaddocumentService','cmnfunctionService'];

    function TrnHRLoanHRPaymentRejectedViewController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams,DownloaddocumentService,cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'TrnHRLoanHRPaymentRejectedViewController';
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
            var url = 'api/MstHRLoanApprovalsApproved/GetHRloanApprovalSummary';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.Approvalsummary = resp.data.ApprovedApprovalsummary;
            });
            var url = 'api/MstHRLoanApprovalsRejected/GetHRloanApprovalSummary';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.Approvalsummary = resp.data.RejectedApprovalsummary;
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
            $state.go('app.TrnHRLoanHRPaymentRejectedSummary');
        }
        //Document Multiple Add
        $scope.doc_downloads = function (val1, val2) {
            DownloaddocumentService.Downloaddocument(val1, val2);
        }
        $scope.view_hrquerydesc = function (query_description, queryresponse_remarks, query_responseby) {
            var modalInstance = $modal.open({
                templateUrl: '/hrqueryDescriptionView.html',
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
                templateUrl: '/fhqueryDescriptionView.html',
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

        $scope.view_drmquerydesc = function (query_description, queryresponse_remarks, query_responseby) {
            var modalInstance = $modal.open({
                templateUrl: '/drmqueryDescriptionView.html',
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

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('TrnHRLoanHRPaymentViewController', AprHRLoanHRAdvanceApprovalsController);

    AprHRLoanHRAdvanceApprovalsController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams','cmnfunctionService'];

    function AprHRLoanHRAdvanceApprovalsController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams,cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'TrnHRLoanHRPaymentViewController';
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
            var url = 'api/MstHRLoanApprovalsApproved/GetHRloanApprovalSummary';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.Approvalsummary = resp.data.ApprovedApprovalsummary;
            });
            var url = 'api/MstHRLoanApprovalsRejected/GetHRloanApprovalSummary';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.Approvalsummary = resp.data.RejectedApprovalsummary;
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

        $scope.Back = function () {

            $state.go('app.AprHRLoanHRHeadApprovalsSummary');
        }

        $scope.view_hrquerydesc = function (query_description, queryresponse_remarks, query_responseby) {
            var modalInstance = $modal.open({
                templateUrl: '/hrqueryDescriptionView.html',
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
                templateUrl: '/fhqueryDescriptionView.html',
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

        $scope.view_drmquerydesc = function (query_description, queryresponse_remarks, query_responseby) {
            var modalInstance = $modal.open({
                templateUrl: '/drmqueryDescriptionView.html',
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



(function () {
    'use strict';

    angular
        .module('angle')
        .controller('TrnHRLoanHRVerificationsApprovedSummaryController', TrnHRLoanHRVerificationsApprovedSummaryController);

        TrnHRLoanHRVerificationsApprovedSummaryController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams','cmnfunctionService'];

    function TrnHRLoanHRVerificationsApprovedSummaryController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams,cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'TrnHRLoanHRVerificationsApprovedSummaryController';
        lockUI();
        activate();

        function activate() {
        
            var url = 'api/TrnHRLoanHRVerifications/GetHRloanHRheadVerificationsDetailsApproved';
               SocketService.get(url).then(function (resp) {               
                   $scope.verificationsdetails_list = resp.data.verifications_summary;
                   unlockUI();
               });
              
               var url = 'api/TrnHRLoanHRVerifications/GetHRloanHRheadVerificationsDetailscount';
               SocketService.get(url).then(function (resp) {
                   unlockUI();
                   $scope.lspendinghrVerify_count = resp.data.pendinghrVerify_count;
                   $scope.lsapprovedhrVerify_count = resp.data.approvedhrVerify_count;                  
                   $scope.lsrejectedhrVerify_count = resp.data.rejectedhrVerify_count;                   
             });
        }       
      

        $scope.myverifications = function () {
            $state.go('app.TrnHRLoanHRVerificationsSummary');
        }

        $scope.approved_verifications = function () {
            $state.go('app.TrnHRLoanHRVerificationsApprovedSummary');
        }


        $scope.rejected_verifications = function () {
            $state.go('app.TrnHRLoanHRVerificationsRejectedSummary');
        }
        
        $scope.viewrequests = function (request_gid) {
            $location.url('app/TrnHRLoanHRVerificationsView?hash=' + cmnfunctionService.encryptURL('request_gid=' + request_gid));
        }       
       
       
    }
})();


(function () {
    'use strict';

    angular
        .module('angle')
        .controller('TrnHRLoanHRVerificationsController', TrnHRLoanHRVerificationsController);

    TrnHRLoanHRVerificationsController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', 'DownloaddocumentService', 'cmnfunctionService'];

    function TrnHRLoanHRVerificationsController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, DownloaddocumentService, cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'TrnHRLoanHRVerificationsController';       
        $scope.request_gid = cmnfunctionService.decryptURL($location.search().hash).request_gid;
        var request_gid = $scope.request_gid;
      
       
        lockUI();
        activate();

        function activate() { 
            vm.calender1 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();

                vm.open1 = true;
            };
            // Calender Popup... //

            vm.calender2 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();

                vm.open2 = true;
            };

           

            // Calender Popup... //                   
            vm.formats = ['MM-yyyy'];
            vm.format = 'MMM - yyyy';
            vm.dateOptions = {
                //formatYear: 'yy',
                //startingDay: 1
               
                minMode: 'month'
              

            };

            $scope.checkErr = function (approvedtenure_startdate, approvedtenure_enddate) {
                $scope.errMessage = '';
                var curDate = new Date();

                if (new Date(approvedtenure_startdate) > new Date(approvedtenure_enddate)) {
                    $scope.errMessage = 'End month should be greater than start month';
                    $scope.approvedtenure_startdate = '';
                    $scope.approvedtenure_enddate = ''
                    return false;
                }
                //if (new Date(startDate) < curDate) {
                //    $scope.errMessage = 'Start date should not be before today.';
                //    return false;
                //}
            };
                   
            lockUI();
            gettcflag();
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

                if(resp.data.request_status== "HRVerify Pending"){
                    $scope.txtapprovedinterest = resp.data.interest;
                    $scope.cboapprovedtenure = resp.data.tenure; 
                }
                else if(resp.data.request_status== "HR Approved"){
                    $scope.txtapprovedinterest = resp.data.interest;
                    $scope.cboapprovedtenure = resp.data.tenure;                       
                }
                else{
                    $scope.txtapprovedinterest = resp.data.approved_interest;
                    $scope.cboapprovedtenure = resp.data.approved_tenure;
                    $scope.approvedtenure_startdate = resp.data.approvedtenure_startdate;
                    $scope.approvedtenure_enddate = resp.data.approvedtenure_enddate;
                }
              
                unlockUI();
            });
            
            var url = 'api/TrnHRLoanHRVerifications/GetHRLoanDropDown';
            SocketService.get(url).then(function (resp) {                
                $scope.lshrtermsandconditions_list = resp.data.hrtermsandconditions_list;
                $scope.hrdocumentname_list = resp.data.hrdocumentname_list;

            });
            var url = 'api/TrnHRLoanHRVerifications/TempDocumentsList';
            SocketService.get(url).then(function (resp) {
            });
            var param = {
                request_gid: request_gid,
            }
            var url = 'api/TrnHRLoanHRVerifications/GetUploadList';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.HRDocument_list = resp.data.HRDocument_list;
                });
            var url = 'api/MstHRLoanDrmApproval/GetUploadDocumentsList';
            SocketService.getparams(url, params).then(function (resp) {
               $scope.hrReuploadDocument_list = resp.data.HrReuploadDocument_list;
                });           

            var url = 'api/TrnHRLoanHRVerifications/GetManagerRaiseQuery';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.mangraisequery_list = resp.data.mangraisequery_list;
            });
            
            lockUI();
            var url = 'api/MstHRLoanHRMappingApprovals/GetManagerName';
            SocketService.get(url).then(function (resp) {
                unlockUI();                  
                $scope.lblemployee_name = resp.data.employee_name;                
            });
            var params =
                {
                    request_gid: $scope.request_gid
                }
            var url = 'api/TrnHRLoanHRVerifications/HRLoanPaymentDocumentList';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.PaymentDocument_list = resp.data.PaymentDocument_list;
                });
                var url = 'api/MstHRLoanDrmApproval/GetHRloanApprovalSummary';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.Approvalsummary = resp.data.Approvalsummary;
            });      
            
        }

        $scope.checkDate = function (startDate, endDate) {
            $scope.errMessage = '';          

            if (new Date(startDate) > new Date(endDate)) {
                $scope.errMessage = 'End Date should be greater than start date';
                $scope.approvedtenure_startdate = '';
                $scope.approvedtenure_enddate= ''
                return false;
            }
           
        };        
        function gettcflag(){
            var param= {
                request_gid: $scope.request_gid
            }
            var url = 'api/TrnHRLoanHRVerifications/GetTCflag';
            lockUI();
            SocketService.getparams(url,param).then(function (resp) {
                $scope.tc_flag = resp.data.tc_flag;
                unlockUI();
            });
        }

        // $scope.Back = function () {
        //     if (lspage == 'HRVerifications') {
        //         $state.go('app.TrnHRLoanHRVerificationsSummary');
        //     }
        //     else if (lspage == 'HRApproved') {
        //         $state.go('app.TrnHRLoanHRVerificationsApprovedSummmary');
        //     } 
        //     else if (lspage == 'HRReject') {
        //         $state.go('app.TrnHRLoanHRVerificationsRejectedSummary');
        //     }           
        //     else {
               
        //     } 
        // }

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

        $scope.UploadhrDocument = function (val, val1, name) {
            if (($scope.txtdocument_id == null) || ($scope.txtdocument_id == '') || ($scope.txtdocument_id == undefined) || ($scope.cbodocumentname == null) || ($scope.cbodocumentname == '') || ($scope.cbodocumentname == undefined)) {
                $("#hrfile").val('');
                Notify.alert('Kindly Enter the Document Title/ID', 'warning');
            }
            else {
                var item = {
                    name: val[0].name,
                    file: val[0]
                };
                var IsValidExtension = cmnfunctionService.fnCheckValidDocType(val[0].name, "");

                if (IsValidExtension == false) {
                    Notify.alert("File format is not supported..!", {
                        status: 'danger',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    return false;
                }
                var frm = new FormData();
                frm.append('fileupload', item.file);
                frm.append('file_name', item.name);
                frm.append('document_name', $scope.documentname);
                frm.append('document_title', $scope.cbodocumentname.hrdocument_name);
                frm.append('hrdocument_name', $scope.cbodocumentname.hrdocument_gid);
                frm.append('document_id', $scope.txtdocument_id);
                frm.append('request_gid',$scope.request_gid);
                frm.append('project_flag', "Default");
                $scope.uploadfrm = frm;
                if ($scope.uploadfrm != undefined) {
                    var url = 'api/TrnHRLoanHRVerifications/HRLoanDocumentUpload';
                    SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {
                        $scope.HRDocument_list = resp.data.HRDocument_list;
                        unlockUI();
                        $("#hrfile").val('');
                        $scope.cbodocumentname = "";
                        $scope.txtdocument_id = "";
                        $scope.uploadfrm = undefined;
                        if (resp.data.status == true) {
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            var params =
                            {
                                request_gid: $scope.request_gid
                            }
                            var url = 'api/TrnHRLoanHRVerifications/HRLoanDocumentList';
                                    SocketService.getparams(url, params).then(function (resp) {
                                        $scope.HRDocument_list = resp.data.HRDocument_list;
                                    });
                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                        }
                        unlockUI();
                    });
                }
                else {
                    alert('Document is not Available..!');
                    return;
                }
            }
        }
        $scope.delete_hrspcldocument = function (hrspecialdocument_gid) {
            lockUI();
            var params = {
                hrspecialdocument_gid: hrspecialdocument_gid
            }
            var url = 'api/TrnHRLoanHRVerifications/UploadDocumentsDelete';
            SocketService.getparams(url, params).then(function (resp) {
              $scope.HRDocument_list = resp.data.HRDocument_list;
                if (resp.data.status == true) {

                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    var param =
                         {
                            request_gid: request_gid
                         }
                    var url = 'api/TrnHRLoanHRVerifications/GetUploadList';
                    SocketService.getparams(url, param).then(function (resp) {
                        $scope.HRDocument_list = resp.data.HRDocument_list;
                    });
                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'danger',
                        pos: 'top-center',
                        timeout: 3000
                    });
                 
                }
                unlockUI();
            });
        }

        $scope.allChecked = function () {
            return $scope.termsandcont_list.filter(function (obj) { return obj.checked; }).length === $scope.termsandcont_list.length;
        //    return $scope.lshrtermsandconditions_list.filter(obj => obj.checked).length == $scope.lshrtermsandconditions_list.length;
        }
      
        $scope.checkAll = function (selected) {
            angular.forEach($scope.lshrtermsandconditions_list, function (val) {
                val.checked = $scope.selectAll;

            });
        };

        $scope.checkall = function (selected) {
            angular.forEach($scope.lshrtermsandconditions_list, function (val) {
                val.checked = selected;
            });
        }
                     
        $scope.tremsandcondtn = function () {
          
            var hrloantermsandconditions_gid;
            var termsandconditionslistGId = [];
           
            angular.forEach($scope.lshrtermsandconditions_list, function (val){

                if (val.checked == true) {
                    
                    var termsandconditionslist_gid = val.hrloantermsandconditions_gid;
                    hrloantermsandconditions_gid = val.hrloantermsandconditions_gid;
                    termsandconditionslistGId.push(termsandconditionslist_gid);
                  
                }

            });
            if (($scope.cbofintype != 'Salary Advance' && $scope.PaymentDocument_list ==null)) {
                Notify.alert('Please Select Import Excel Document!  ', 'warning')
            }
            else {


                var params = {
                    request_gid: $scope.request_gid,
                    termsandconditionslist_gid: termsandconditionslistGId,
                    approved_interest: $scope.txtapprovedinterest,
                    approved_tenure: $scope.cboapprovedtenure,
                    approvedtenure_startdate: $scope.approvedtenure_startdate,
                    approvedtenure_enddate: $scope.approvedtenure_enddate

                }

                if (hrloantermsandconditions_gid != undefined) {
                    var url = 'api/TrnHRLoanHRVerifications/PostHrLoantermsandcondtn';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {

                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            $state.go('app.TrnHRLoanHRVerificationsSummary');
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

                else {
                    Notify.alert('Select Atleast One Terms and Conditions!', 'warning')
                }

            }
        }
       
        $scope.hrverify_approve = function () {
       
            var params = {

                hrverify_remarks: $scope.txtverify_remarks,
                request_gid: $scope.request_gid,
                                
            }
            var url = 'api/TrnHRLoanHRVerifications/PostHrLoanHRVerifyApprovalUpdate';
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {

                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    $state.go('app.TrnHRLoanHRVerificationsSummary');
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

     $scope.hrverify_reject = function () {

         var params = {

            hrverify_remarks: $scope.txtverify_remarks,
            request_gid: $scope.request_gid,
         }
         var url = 'api/TrnHRLoanHRVerifications/PostHrLoanHRVerifyRejectUpdate';
         lockUI();
         SocketService.post(url, params).then(function (resp) {
             unlockUI();
             if (resp.data.status == true) {

                 Notify.alert(resp.data.message, {
                     status: 'success',
                     pos: 'top-center',
                     timeout: 3000
                 });
                 $state.go('app.TrnHRLoanHRVerificationsSummary');
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
     $scope.verify = function () {
       
        var params = {

            hrdocverify_remarks: $scope.txtverify_remarks,
            request_gid: $scope.request_gid,                 
        }
        var url = 'api/TrnHRLoanHRVerifications/PostHrLoanVerifyApprovalUpdate';
        lockUI();
        SocketService.post(url, params).then(function (resp) {
            unlockUI();
            if (resp.data.status == true) {

                Notify.alert(resp.data.message, {
                    status: 'success',
                    pos: 'top-center',
                    timeout: 3000
                });
                $scope.txtverify_remarks = '';
                $state.go('app.TrnHRLoanHRVerifications');
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

     $scope.create_raisequery = function () {
        var modalInstance = $modal.open({
            templateUrl: '/raisequery.html',
            controller: ModalInstanceCtrl,
            backdrop: 'static',
            keyboard: false,
            size: 'md'
        });
        ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
        function ModalInstanceCtrl($scope, $modalInstance) {
            $scope.ok = function () {
                $modalInstance.close('closed');
            };
            
            $scope.raisequery_add = function () {                  
               var params = {
                    query_title: $scope.txtquery_title,
                    query_description: $scope.txtquery_desc,                                          
                    request_gid: request_gid,                       
                    
                }
                var url = 'api/TrnHRLoanHRVerifications/PostManagerRaiseQuery';               
                lockUI();
                SocketService.post(url, params).then(function (resp) {
                    unlockUI();
                    if (resp.data.status == true) {
                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        activate();
                    }
                    else {
                        Notify.alert(resp.data.message, {
                            status: 'info',
                            pos: 'top-center',
                            timeout: 3000
                        });
                    }
                });

                $modalInstance.close('closed');
            }           
        }
    }

     $scope.view_mangquerydesc = function (query_description, queryresponse_remarks, query_responseby) {
        var modalInstance = $modal.open({
            templateUrl: '/mangqueryDescriptionView.html',
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

    $scope.UploadDocument = function (val, val1, name) {      
            var item = {
                name: val[0].name,
                file: val[0]
            };
            var IsValidExtension = cmnfunctionService.fnCheckValidDocType(val[0].name, "");

            if (IsValidExtension == false) {
                Notify.alert("File format is not supported..!", {
                    status: 'danger',
                    pos: 'top-center',
                    timeout: 3000
                });
                return false;
            }
            var frm = new FormData();
            frm.append('fileupload', item.file);
            frm.append('file_name', item.name);
            frm.append('document_name', $scope.documentname);           
            frm.append('request_gid',$scope.request_gid);
            frm.append('project_flag', "Default");
            $scope.uploadfrm = frm;
            if ($scope.uploadfrm != undefined) {
                var url = 'api/TrnHRLoanHRVerifications/HRLoanPaymentDocumentUpload';
                SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {
                    $scope.PaymentDocument_list = resp.data.PaymentDocument_list;
                    unlockUI();
                    $("#Paymentfile").val('');                  
                    $scope.uploadfrm = undefined;
                    if (resp.data.status == true) {
                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
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
                    else {
                        Notify.alert(resp.data.message, {
                            status: 'warning',
                            pos: 'top-center',
                            timeout: 3000
                        });
                    }
                    unlockUI();
                });
            }
            else {
                alert('Document is not Available..!');
                return;
            }
      
    }
    $scope.delete_document = function (hrrepaymentdocument_gid) {
        lockUI();
        var params = {
            hrrepaymentdocument_gid: hrrepaymentdocument_gid
        }
        var url = 'api/TrnHRLoanHRVerifications/UploadPaymentDocumentsDelete';
        SocketService.getparams(url, params).then(function (resp) {
          $scope.PaymentDocument_list = resp.data.PaymentDocument_list;
            if (resp.data.status == true) {

                Notify.alert(resp.data.message, {
                    status: 'success',
                    pos: 'top-center',
                    timeout: 3000
                });
                var param =
                     {
                        request_gid: request_gid
                     }
                var url = 'api/TrnHRLoanHRVerifications/HRLoanPaymentDocumentList';
                SocketService.getparams(url, param).then(function (resp) {
                    $scope.PaymentDocument_list = resp.data.PaymentDocument_list;
                });
            }
            else {
                Notify.alert(resp.data.message, {
                    status: 'danger',
                    pos: 'top-center',
                    timeout: 3000
                });
             
            }
            unlockUI();
        });
    }
        $scope.Back = function () {
            $state.go('app.TrnHRLoanHRVerificationsSummary');
        }
       

    }
})();






(function () {
    'use strict';

    angular
        .module('angle')
        .controller('TrnHRLoanHRVerificationsRejectedSummaryController', TrnHRLoanHRVerificationsRejectedSummaryController);

        TrnHRLoanHRVerificationsRejectedSummaryController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams','cmnfunctionService'];

    function TrnHRLoanHRVerificationsRejectedSummaryController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams,cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'TrnHRLoanHRVerificationsRejectedSummaryController';
        lockUI();
        activate();

        function activate() { 
            var url = 'api/TrnHRLoanHRVerifications/GetHRloanHRheadVerificationsDetailsRejected';
               SocketService.get(url).then(function (resp) {               
                   $scope.verificationsdetails_list = resp.data.verifications_summary;
                   unlockUI();
               });
               var url = 'api/TrnHRLoanHRVerifications/GetHRloanHRheadVerificationsDetailscount';
               SocketService.get(url).then(function (resp) {
                   unlockUI();
                   $scope.lspendinghrVerify_count = resp.data.pendinghrVerify_count;
                   $scope.lsapprovedhrVerify_count = resp.data.approvedhrVerify_count;                  
                   $scope.lsrejectedhrVerify_count = resp.data.rejectedhrVerify_count;                   
             });
        }       
           

        $scope.myverifications = function () {
            $state.go('app.TrnHRLoanHRVerificationsSummary');
        }
        $scope.approved_verifications = function () {
            $state.go('app.TrnHRLoanHRVerificationsApprovedSummary');
        }

        $scope.rejected_verifications = function () {
            $state.go('app.TrnHRLoanHRVerificationsRejectedSummary');
        }       
        $scope.viewrequests = function (request_gid) {
            $location.url('app/TrnHRLoanHRVerificationsView?hash=' + cmnfunctionService.encryptURL('request_gid=' + request_gid));
        }   
       
    }
})();


(function () {
    'use strict';

    angular
        .module('angle')
        .controller('TrnHRLoanHRVerificationsSummaryController', TrnHRLoanHRVerificationsSummaryController);

        TrnHRLoanHRVerificationsSummaryController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams','cmnfunctionService'];

    function TrnHRLoanHRVerificationsSummaryController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams,cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'TrnHRLoanHRVerificationsSummaryController';
        lockUI();
        activate();

        function activate() {
            lockUI();
            var url = 'api/TrnHRLoanHRVerifications/GetHRloanHRheadVerificationsDetails';
               SocketService.get(url).then(function (resp) {               
                   $scope.verificationsdetails_list = resp.data.verifications_summary;
                   unlockUI();
               });
               var url = 'api/TrnHRLoanHRVerifications/GetHRloanHRheadVerificationsDetailscount';
               SocketService.get(url).then(function (resp) {
                   unlockUI();
                   $scope.lspendinghrVerify_count = resp.data.pendinghrVerify_count;
                   $scope.lsapprovedhrVerify_count = resp.data.approvedhrVerify_count;                  
                   $scope.lsrejectedhrVerify_count = resp.data.rejectedhrVerify_count;                   
             });
        }
       
        $scope.verifications = function (request_gid, employee_gid) {
            $location.url('app/TrnHRLoanHRVerifications?hash=' + cmnfunctionService.encryptURL('request_gid=' + request_gid + '&employee_gid=' + employee_gid ));
        }

        $scope.myverifications = function () {
            $state.go('app.TrnHRLoanHRVerificationsSummary');
        }

        $scope.approved_verifications = function () {
            $state.go('app.TrnHRLoanHRVerificationsApprovedSummary');
        }

        $scope.rejected_verifications = function () {
            $state.go('app.TrnHRLoanHRVerificationsRejectedSummary');
        } 
               
        $scope.viewrequests = function (request_gid) {
            $location.url('app/TrnHRLoanHRVerificationsView?hash=' + cmnfunctionService.encryptURL('request_gid=' + request_gid));
        }       
       
    }
})();

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('TrnHRLoanHRVerificationsViewController', TrnHRLoanHRVerificationsViewController);

        TrnHRLoanHRVerificationsViewController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', 'DownloaddocumentService','cmnfunctionService'];

    function TrnHRLoanHRVerificationsViewController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, DownloaddocumentService,cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'TrnHRLoanHRVerificationsViewController';
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
            
            var params = {
                request_gid: $scope.request_gid,
            }
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
            var url = 'api/TrnHRLoanHRVerifications/termsandcondtnview';
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.termsandcont_list = resp.data.hrtermsandconditions_list;
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
                lockUI();
            var url = 'api/MstHRLoanHRMappingApprovals/GetManagerName';
            SocketService.get(url).then(function (resp) {
                unlockUI();                  
                $scope.lblemployee_name = resp.data.employee_name;                
            });
            var params =
            {
                request_gid: request_gid
            }
            var url = 'api/MstHRLoanRequest/GetUploadDocumentsList';
                    SocketService.getparams(url, params).then(function (resp) {
                        $scope.upload_list = resp.data.upload_list;
                    });
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
            var params =
                {
                    request_gid: $scope.request_gid
                }
            var url = 'api/TrnHRLoanHRVerifications/HRLoanPaymentDocumentList';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.PaymentDocument_list = resp.data.PaymentDocument_list;
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

        $scope.Back = function () {

            $state.go('app.TrnHRLoanHRVerificationsSummary');
        }
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
        

        $scope.view_drmquerydesc = function (query_description, queryresponse_remarks, query_responseby) {
            var modalInstance = $modal.open({
                templateUrl: '/drmqueryDescriptionView.html',
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
                templateUrl: '/fhqueryDescriptionView.html',
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
                templateUrl: '/hrqueryDescriptionView.html',
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
                templateUrl: '/mangqueryDescriptionView.html',
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


