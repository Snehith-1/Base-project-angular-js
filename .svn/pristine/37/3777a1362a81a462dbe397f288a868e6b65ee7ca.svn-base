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

