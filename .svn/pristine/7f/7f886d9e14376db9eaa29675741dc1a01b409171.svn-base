(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AgrTrnSuprCCCompletedSummarycontroller', AgrTrnSuprCCCompletedSummarycontroller);

    AgrTrnSuprCCCompletedSummarycontroller.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function AgrTrnSuprCCCompletedSummarycontroller($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AgrTrnSuprCCCompletedSummarycontroller';

        lockUI();
        activate();
        function activate() {
            var url = 'api/AgrTrnSuprCC/GetCCcount';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.lblscheduled_count = resp.data.scheduled_count;
                $scope.lblpending_count = resp.data.pending_count;
                $scope.lblcompleted_count = resp.data.completed_count;
                $scope.lblsentbackcc_count = resp.data.cctocredit_count;
            });
            var url = 'api/AgrTrnSuprCC/GetMeetingCompletedSummary';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.applicationadd_list = resp.data.applicationadd_list;
            });
        }

        $scope.application_view = function (application_gid) {
            $location.url('app/AgrTrnSuprCcCommitteeApplicationView?application_gid=' + application_gid + '&lspage=CompletedMeetingsummary');
        }

        $scope.Pending_summary = function () {
            $location.url('app/AgrTrnSuprCreditCommitteeSummary');
        }
        $scope.view_meeting = function (application_gid) {
            $location.url('app/AgrTrnSuprCCMeetingSchedule?application_gid=' + application_gid + '&lstab=Scheduled');
        }
        $scope.scheduled_summary = function () {
            $location.url('app/AgrTrnSuprCCscheduledSummary');
        }
        $scope.viewschedule_details = function (application_gid) {
            $location.url('app/AgrTrnSuprCcScheduledMeetingDtlView?application_gid=' + application_gid + '&lspage=CCCompletedView');
        }
        $scope.sentbackto_credit = function () {
            $location.url('app/AgrTrnSuprSentbackcctoCredit');
        }
        $scope.history = function (application_gid) {
            $location.url('app/AgrTrnSuprSentbackCadtoCcHistory?application_gid=' + application_gid + '&lspage=CCCompletedMeeting');
        }
    }
})();
