(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AgrTrnCcMeetingSkipSummaryController', AgrTrnCcMeetingSkipSummaryController);

    AgrTrnCcMeetingSkipSummaryController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function AgrTrnCcMeetingSkipSummaryController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AgrTrnCcMeetingSkipSummaryController';

        lockUI();
        activate();
        function activate() {
            var url = 'api/AgrTrnCC/GetCCMeetingSkipSummary';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.ccmeetingskip_list = resp.data.ccmeetingskip_list;
            });
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

        }

        //$scope.application_view = function (application_gid) {
        //    $location.url('app/MstCcCommitteeApplicationView?application_gid=' + application_gid + '&lspage=ScheduleMeeting');
        //}
        //$scope.schedule_meeting = function (application_gid) {
        //    $location.url('app/MstCCMeetingSchedule?application_gid=' + application_gid + '&lstab=Pending');
        //}
        $scope.scheduled_summary = function () {
            $location.url('app/AgrTrnCCscheduledSummary');
        }
        $scope.ccmeeting_skiptab = function () {
            $location.url('app/AgrTrnCcMeetingSkipSummary');
        }
        $scope.pending_summary = function () {
            $location.url('app/AgrTrnCreditCommitteeSummary');
        }
        $scope.completed_summary = function () {
            $location.url('app/AgrTrnCCCompletedSummary');
        }
        $scope.sentbackto_credit = function () {
            $location.url('app/AgrTrnSentbackcctoCredit');
        }
        $scope.ccmeetingskip_history = function (application_gid) {
            $location.url('app/AgrTrnCcMeetingSkipHistory?application_gid=' + application_gid + '&lspage=CCMeetingSkip');
        }

        $scope.ccskipped_reason = function (ccmeetingskip_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/ccskippedreason.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                var params = {
                    ccmeetingskip_gid: ccmeetingskip_gid
                }
                var url = 'api/AgrTrnCC/GetCCMeetingSkippedReason';
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.lblccmeetingskipreason = resp.data.reason;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
            }
        }
        $scope.advance_appl = function () {
            $location.url('app/AgrTrnACAutoApprovalSummary?lsparent=CCMeetingSkip');
        }

    }
})();
