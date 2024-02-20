(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AgrTrnCCCompletedSummarycontroller', AgrTrnCCCompletedSummarycontroller);

    AgrTrnCCCompletedSummarycontroller.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function AgrTrnCCCompletedSummarycontroller($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AgrTrnCCCompletedSummarycontroller';

        lockUI();
        activate();
        function activate() {
            var url = 'api/AgrTrnCC/GetCCcount';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.lblscheduled_count = resp.data.scheduled_count;
                $scope.lblpending_count = resp.data.pending_count;
                $scope.lblcompleted_count = resp.data.completed_count;
                $scope.lblsentbackcc_count = resp.data.cctocredit_count;
                $scope.advance_count = resp.data.advance_count;
                $scope.lblccmeetingskip_count = resp.data.ccmeetingskip_count;
            });
            var url = 'api/AgrTrnCC/GetMeetingCompletedSummary';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.applicationadd_list = resp.data.applicationadd_list;
            });
        }

        $scope.application_view = function (application_gid) {
            $location.url('app/AgrTrnCcCommitteeApplicationView?application_gid=' + application_gid + '&lspage=CompletedMeetingsummary');
        }

        $scope.Pending_summary = function () {
            $location.url('app/AgrTrnCreditCommitteeSummary');
        }
        $scope.view_meeting = function (application_gid) {
            $location.url('app/AgrTrnCCMeetingSchedule?application_gid=' + application_gid + '&lstab=Scheduled');
        }
        $scope.scheduled_summary = function () {
            $location.url('app/AgrTrnCCscheduledSummary');
        }
        $scope.viewschedule_details = function (application_gid) {
            $location.url('app/AgrTrnCcScheduledMeetingDtlView?application_gid=' + application_gid + '&lspage=CCCompletedView');
        }
        $scope.sentbackto_credit = function () {
            $location.url('app/AgrTrnSentbackcctoCredit');
        }
        $scope.history = function (application_gid) {
            $location.url('app/AgrTrnSentbackCadtoCcHistory?application_gid=' + application_gid + '&lspage=CCCompletedMeeting');
        }

        $scope.advance_appl = function () {
            $location.url('app/AgrTrnACAutoApprovalSummary?lsparent=CCCompleted');
        }
        $scope.ccmeeting_skiptab = function () {
            $location.url('app/AgrTrnCcMeetingSkipSummary');
        }

    }
})();
