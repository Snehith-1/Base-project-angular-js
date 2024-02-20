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
