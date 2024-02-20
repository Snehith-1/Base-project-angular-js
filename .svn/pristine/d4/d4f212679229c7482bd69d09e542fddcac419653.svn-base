(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstCreditSubmittedtoCCSummaryController', MstCreditSubmittedtoCCSummaryController);

        MstCreditSubmittedtoCCSummaryController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function MstCreditSubmittedtoCCSummaryController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstCreditSubmittedtoCCSummaryController';
        lockUI();
        activate();
        function activate() {

            var url = 'api/MstCreditApproval/GetMyAppcreditSubmittedtoccSummary';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.appsubmittedtocc_list = resp.data.applicaition_list;
            });
            var url = 'api/MstCreditApproval/CreditApplicationCount';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.upcomingcreditapplication_count = resp.data.upcomingcreditapplication_count;
                $scope.newcreditapplication_count = resp.data.newcreditapplication_count;
                $scope.approvedapplication_count = resp.data.approvedapplication_count;
                $scope.rejectholdapplication_count = resp.data.rejectholdapplication_count;
                $scope.submitted2ccapp_count = resp.data.submitted2ccapp_count;
                $scope.lstotalcount = resp.data.lstotalcount;
            });
        }

        $scope.applcreation_view = function (val) {
            $location.url('app/MstCcCommitteeApplicationView?application_gid=' + val + '&lspage=CreditSubmittedtoCC');
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
            $location.url('app/MstCreditApproval?application_gid=' + application_gid + '&employee_gid=' + employee_gid + '&applicationapproval_gid=' + applicationapproval_gid + '&lspage=CreditSubmittedtoCC' +  '&initiate_flag=N' + '&lsflag=N');
        }
        
    }
})();

