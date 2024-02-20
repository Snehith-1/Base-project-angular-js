(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstCCCompletedSummaryController', MstCCCompletedSummaryController);

        MstCCCompletedSummaryController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function MstCCCompletedSummaryController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstCCCompletedSummaryController';

        lockUI();
        activate();
        function activate() {
            var url = 'api/MstCC/GetCCcount';
            SocketService.get(url).then(function (resp) {
                //unlockUI();
                $scope.lblscheduled_count = resp.data.scheduled_count;
                $scope.lblpending_count = resp.data.pending_count;
                $scope.lblcompleted_count = resp.data.completed_count;
                $scope.lblsentbackcc_count = resp.data.cctocredit_count;
                $scope.lblccmeetingskip_count = resp.data.ccmeetingskip_count;
            });
            var url = 'api/MstCC/GetMeetingCompletedSummary';
            SocketService.get(url).then(function (resp) {
                if (resp.data.applicationadd_list != null && resp.data.applicationadd_list.length > 0) {
                $scope.applicationadd_list = resp.data.applicationadd_list;
                unlockUI();
            }
            else if (resp.data.status == false)
            unlockUI();
            });
        }

        $scope.application_view = function (application_gid) {
            $location.url('app/MstCcCommitteeApplicationView?application_gid=' + application_gid+ '&lspage=CompletedMeetingsummary');
        }
      
        $scope.Pending_summary = function () {
            $location.url('app/MstCreditCommitteeSummary');
        }
        $scope.view_meeting = function (application_gid) {
            $location.url('app/MstCCMeetingSchedule?application_gid=' + application_gid + '&lstab=Scheduled');
        }
        $scope.scheduled_summary = function () {
            $location.url('app/MstCCscheduledSummary');
        }
        $scope.viewschedule_details = function (application_gid) {
            $location.url('app/MstCcScheduledMeetingDtlView?application_gid=' + application_gid + '&lspage=CCCompletedView');
        }
        $scope.sentbackto_credit = function () {
            $location.url('app/MstSentbackcctoCredit');
        }
        $scope.history = function (application_gid) {
            $location.url('app/MstSentbackCadtoCcHistory?application_gid=' + application_gid + '&lspage=CCCompletedMeeting');
        }
        $scope.ccmeeting_skiptab = function () {
            $location.url('app/MstCcMeetingSkipSummary');
        }
    }
})();
