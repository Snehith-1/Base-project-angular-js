(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AgrTrnSuprCreditCCSkippedSummaryController', AgrTrnSuprCreditCCSkippedSummaryController);

    AgrTrnSuprCreditCCSkippedSummaryController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function AgrTrnSuprCreditCCSkippedSummaryController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AgrTrnSuprCreditCCSkippedSummaryController';
        //lockUI();
        activate();
        function activate() {
            //var url = 'api/AgrTrnSuprApplicationAdd/GetAppAssignmentSummary';
            //SocketService.get(url).then(function (resp) {
            //    unlockUI();
            //    $scope.appassignment_list = resp.data.applicationadd_list;
            //});
            var url = 'api/AgrTrnSuprCreditApproval/CreditApplicationCount';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.newcreditapplication_count = resp.data.newcreditapplication_count;
                $scope.approvedapplication_count = resp.data.approvedapplication_count;
                $scope.rejectholdapplication_count = resp.data.rejectholdapplication_count;
                $scope.submitted2ccapp_count = resp.data.submitted2ccapp_count;
                $scope.lstotalcount = resp.data.lstotalcount;
            });
        }

        $scope.applcreation_view = function (val) {
            $location.url('app/AgrTrnSuprCreditCCSkippedSummary?application_gid=' + val + '&lstab=CreditCCSkipped');
        }

        $scope.start_creditunderwriting = function (application_gid) {
            $location.url('app/AgrTrnSuprStartCreditUnderwriting?application_gid=' + application_gid);
        }

        $scope.GradingTool_add = function (val) {
            $location.url('app/AgrTrnSuprCreditAssessedScoreAdd?application_gid=' + val + '&lstab=MyApplications');          
        }

        $scope.VisitReport_add = function (val) {
            $location.url('app/AgrTrnSuprCreditVisitReportAdd?application_gid=' + val  + '&lstab=MyApplications');          
        }

        $scope.inprogress_appl = function () {
            $location.url('app/AgrTrnSuprCreditApprovalSummary');
        }

        $scope.approved_application = function () {
            $location.url('app/AgrTrnSuprCreditApprovedSummary');
        }

        $scope.submittedto_cc = function () {
            $location.url('app/AgrTrnSuprCreditSubmittedtoCCSummary');
        }

        $scope.ccskipped_appl = function () {
            $location.url('app/AgrTrnSuprCreditCCSkippedSummary');
        }

        $scope.rejected_holdappl = function () {
            $location.url('app/AgrTrnSuprCreditRejectandHoldSummary');
        }  
        
        $scope.credit_approval = function (application_gid, employee_gid, applicationapproval_gid,initiate_flag) {
            $location.url('app/AgrTrnSuprCreditApproval?application_gid=' + application_gid + '&employee_gid=' + employee_gid +'&applicationapproval_gid=' + applicationapproval_gid+ '&lspage=CreditCCSkipped'+'&initiate_flag=' + initiate_flag);         
        }
        
    }
})();
