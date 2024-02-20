(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstCCscheduleSummaryController', MstCCscheduleSummaryController);

    MstCCscheduleSummaryController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function MstCCscheduleSummaryController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstCCscheduleSummaryController';

        lockUI();
        activate();
        function activate() {

            var url = 'api/MstCC/GetCCScheduledSummary';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.applicationadd_list = resp.data.applicationadd_list;
            });
            var url = 'api/MstCC/GetCCcount';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.lblscheduled_count = resp.data.scheduled_count;
                $scope.lblpending_count = resp.data.pending_count;
                $scope.lblcompleted_count = resp.data.completed_count;
                $scope.lblsentbackcc_count = resp.data.cctocredit_count;
                $scope.lblccmeetingskip_count = resp.data.ccmeetingskip_count;
            });
        }

        $scope.application_view = function (application_gid) {
            $location.url('app/MstCcCommitteeApplicationView?application_gid=' + application_gid+ '&lspage=ScheduledMeetingsummary');
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
        $scope.completed_summary = function () {
            $location.url('app/MstCCCompletedSummary');
        }
        $scope.sentbackto_credit = function () {
            $location.url('app/MstSentbackcctoCredit');
        }
        $scope.history = function (application_gid) {
            $location.url('app/MstSentbackCadtoCcHistory?application_gid=' + application_gid + '&lspage=ScheduledMeeting');
        }

        $scope.ccmeeting_skiptab = function () {
            $location.url('app/MstCcMeetingSkipSummary');
        }
    }
})();
