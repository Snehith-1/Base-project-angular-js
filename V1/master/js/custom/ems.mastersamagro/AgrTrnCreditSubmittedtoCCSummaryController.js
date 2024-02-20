(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AgrTrnCreditSubmittedtoCCSummaryController', AgrTrnCreditSubmittedtoCCSummaryController);

    AgrTrnCreditSubmittedtoCCSummaryController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function AgrTrnCreditSubmittedtoCCSummaryController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AgrTrnCreditSubmittedtoCCSummaryController';
        lockUI();
        activate();
        function activate() {

            var url = 'api/AgrTrnCreditApproval/GetMyAppcreditSubmittedtoccSummary';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.appsubmittedtocc_list = resp.data.applicaition_list;
            });
            var url = 'api/AgrTrnCreditApproval/CreditApplicationCount';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.newcreditapplication_count = resp.data.newcreditapplication_count;
                $scope.upcomingcreditapplication_count = resp.data.upcomingcreditapplication_count;
                $scope.approvedapplication_count = resp.data.approvedapplication_count;
                $scope.rejectholdapplication_count = resp.data.rejectholdapplication_count;
                $scope.submitted2ccapp_count = resp.data.submitted2ccapp_count;
                $scope.autoapproval_count = resp.data.autoapprovalappl_count;
                $scope.lstotalcount = resp.data.lstotalcount;
            });
        }

        $scope.applcreation_view = function (val) {
            $location.url('app/AgrTrnCcCommitteeApplicationView?application_gid=' + val + '&lstab=CreditSubmittedtoCC');
        }

        $scope.start_creditunderwriting = function (application_gid) {
            $location.url('app/AgrTrnStartCreditUnderwriting?application_gid=' + application_gid);
        }

        $scope.GradingTool_add = function (val) {
            $location.url('app/AgrTrnCreditAssessedScoreAdd?application_gid=' + val + '&lstab=MyApplications');          
        }

        $scope.VisitReport_add = function (val) {
            $location.url('app/AgrTrnCreditVisitReportAdd?application_gid=' + val  + '&lstab=MyApplications');          
        }

        $scope.inprogress_appl = function () {
            $location.url('app/AgrTrnCreditApprovalSummary');
        }

        $scope.approved_application = function () {
            $location.url('app/AgrTrnCreditApprovedSummary');
        }

        $scope.submittedto_cc = function () {
            $location.url('app/AgrTrnCreditSubmittedtoCCSummary');
        }

        $scope.ccskipped_appl = function () {
            $location.url('app/AgrTrnCreditCCSkippedSummary');
        }

        $scope.rejected_holdappl = function () {
            $location.url('app/AgrTrnCreditRejectandHoldSummary');
        }  
        
        $scope.credit_approval = function (application_gid, employee_gid, applicationapproval_gid,initiate_flag) {
            $location.url('app/AgrTrnCreditApproval?application_gid=' + application_gid + '&employee_gid=' + employee_gid + '&applicationapproval_gid=' + applicationapproval_gid + '&lspage=CreditSubmittedtoCC' +  '&initiate_flag=N' + '&lsflag=N');
        }

        $scope.advance_appl = function () {
            $location.url('app/AgrTrnCreditAutoApprovalSummary');
        }
        $scope.upcomingprogress_appl = function () {
            $location.url('app/AgrTrnUpcomingCreditApprovalSummary');
        }

    }
})();

