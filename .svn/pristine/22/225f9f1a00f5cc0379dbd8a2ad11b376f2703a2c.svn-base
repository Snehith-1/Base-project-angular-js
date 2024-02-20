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

