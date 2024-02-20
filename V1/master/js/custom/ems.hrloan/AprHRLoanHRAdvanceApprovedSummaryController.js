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

