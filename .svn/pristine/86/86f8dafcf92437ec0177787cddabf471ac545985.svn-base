(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AgrMstSuprSubmittedtoCCSummaryController', AgrMstSuprSubmittedtoCCSummaryController);

    AgrMstSuprSubmittedtoCCSummaryController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function AgrMstSuprSubmittedtoCCSummaryController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AgrMstSuprSubmittedtoCCSummaryController';
        lockUI();
        activate();
        function activate() {
            var url = 'api/AgrTrnSuprCreditApproval/GetMyAppSubmittedtoccSummary';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.appsubmittedtocc_list = resp.data.applicaition_list;
            });
            var url = 'api/AgrTrnSuprCreditApproval/MyApplicationCount';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.newcreditapplication_count = resp.data.newcreditapplication_count;
                $scope.approvedapplication_count = resp.data.approvedapplication_count;
                $scope.rejectholdapplication_count = resp.data.rejectholdapplication_count;
                $scope.submitted2ccapp_count = resp.data.submitted2ccapp_count;
                $scope.ccapproval_count = resp.data.ccapproval_count;
                $scope.lstotalcount = resp.data.lstotalcount;
            });
        }

        $scope.applcreation_view = function (val) {
            $location.url('app/AgrMstSuprCcCommitteeApplicationView?application_gid=' + val + '&lstab=SubmittedToCC');
        }

        $scope.start_creditunderwriting = function (application_gid, appcreditapproval_gid) {
            $location.url('app/AgrTrnSuprStartCreditUnderwriting?application_gid=' + application_gid + '&appcreditapproval_gid=' + appcreditapproval_gid + '&lspage=submittocc');
        }

        $scope.creditcmd_statusupdate = function (application_gid, employee_gid, applicationapproval_gid, initiate_flag) {
            $location.url('app/AgrMstSuprCreditQueryStatus?application_gid=' + application_gid + '&employee_gid=' + employee_gid + '&lspage=SubmittedToCC');
        }

        $scope.submitedto_approval = function () {
            $location.url('app/AgrMstSuprSubmittedtoApprovalSummary');
        }

        $scope.submittedto_cc = function () {
            $location.url('app/AgrMstSuprSubmittedtoCCSummary');
        }

        $scope.ccskipped_appl = function () {
            $location.url('app/AgrMstSuprCCSkippedApplicationSummary');
        }

        $scope.rejected_holdappl = function () {
            $location.url('app/AgrMstSuprRejectandHoldSummary');
        }

        $scope.inprogress_appl = function () {
            $location.url('app/AgrMstSuprMyApplicationsSummary');
        }

        $scope.ccapproved = function () {
            $location.url('app/AgrMstSuprCCApprovedSummary');
        } 

    }
})();

