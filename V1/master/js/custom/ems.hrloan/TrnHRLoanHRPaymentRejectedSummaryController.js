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

