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

