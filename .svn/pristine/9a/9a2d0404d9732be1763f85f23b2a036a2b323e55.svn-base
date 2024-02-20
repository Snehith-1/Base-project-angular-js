(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstCreditCCSkippedSummaryController', MstCreditCCSkippedSummaryController);

        MstCreditCCSkippedSummaryController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function MstCreditCCSkippedSummaryController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstCreditCCSkippedSummaryController';
        //lockUI();
        activate();
        function activate() {
            //var url = 'api/MstApplicationAdd/GetAppAssignmentSummary';
            //SocketService.get(url).then(function (resp) {
            //    unlockUI();
            //    $scope.appassignment_list = resp.data.applicationadd_list;
            //});
        }

        $scope.applcreation_view = function (val) {
            $location.url('app/MstCreditCCSkippedSummary?application_gid=' + val + '&lstab=CreditCCSkipped');
        }

        $scope.start_creditunderwriting = function (application_gid) {
            $location.url('app/MstStartCreditUnderwriting?application_gid=' + application_gid);
        }

        $scope.GradingTool_add = function (val) {
            $location.url('app/MstCreditAssessedScoreAdd?application_gid=' + val + '&lstab=MyApplications');          
        }

        $scope.VisitReport_add = function (val) {
            $location.url('app/MstCreditVisitReportAdd?application_gid=' + val  + '&lstab=MyApplications');          
        }

        $scope.inprogress_appl = function () {
            $location.url('app/MstCreditApprovalSummary');
        }

        $scope.upcomingprogress_appl = function () {
            $location.url('app/MstUpcomingCreditApprovalSummary');
        }

        $scope.approved_application = function () {
            $location.url('app/MstCreditApprovedSummary');
        }

        $scope.submittedto_cc = function () {
            $location.url('app/MstCreditSubmittedtoCCSummary');
        }

        $scope.ccskipped_appl = function () {
            $location.url('app/MstCreditCCSkippedSummary');
        }

        $scope.rejected_holdappl = function () {
            $location.url('app/MstCreditRejectandHoldSummary');
        }  
        
        $scope.credit_approval = function (application_gid, employee_gid, applicationapproval_gid,initiate_flag) {
            $location.url('app/MstCreditApproval?application_gid=' + application_gid + '&employee_gid=' + employee_gid +'&applicationapproval_gid=' + applicationapproval_gid+ '&lspage=CreditCCSkipped'+'&initiate_flag=' + initiate_flag);         
        }
        
    }
})();

