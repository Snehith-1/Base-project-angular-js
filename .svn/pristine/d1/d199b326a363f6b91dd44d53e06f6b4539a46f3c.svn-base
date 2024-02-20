(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AgrTrnCcScheduledMeetingSummarycontroller', AgrTrnCcScheduledMeetingSummarycontroller);

    AgrTrnCcScheduledMeetingSummarycontroller.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function AgrTrnCcScheduledMeetingSummarycontroller($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AgrTrnCcScheduledMeetingSummarycontroller';

        activate();
        lockUI();
        function activate() {
            var url = 'api/AgrTrnCC/GetCCMeetingCalenderView';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.applicationadd_list = resp.data.applicationadd_list;
            });

            var url = 'api/AgrTrnCC/CCApplicationCount';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.lblmy_approval_count = resp.data.my_approval;
                $scope.lblscheduled_meeting_count = resp.data.scheduled_meeting;
                $scope.lblcc_tagged_count = resp.data.cc_tagged;
                $scope.lblcc_completed_count = resp.data.cc_completed;
                $scope.lblapproval_pending_count = resp.data.approval_pending;
            });

            localStorage.setItem('AgrCC', 'Y');
            localStorage.setItem('CC', 'N');
            localStorage.setItem('AgrSuprCC', 'N');

        }

        $scope.ScheduledMeeting = function () {
            lockUI();
            var url = 'api/AgrTrnCC/GetCCSummary';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.scheduledmeeting_list = resp.data.applicationadd_list;
            });
        }

        $scope.AdminScheduledMeeting = function () {
            lockUI();
            var url = 'api/AgrTrnCC/GetAdminCCSummary';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.adminscheduledmeeting_list = resp.data.applicationadd_list;
            });
        }

        $scope.CCApprovalPending = function () {
            lockUI();
            var url = 'api/AgrTrnCC/GetCCApprovalPendingSummary';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.ccapprovalpending_list = resp.data.applicationadd_list;
            });
        }

        $scope.CCApprovalsummary = function () {
            lockUI();
            var url = 'api/AgrTrnCC/GetCCApprovalSummary';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.ccapproval_list = resp.data.applicationadd_list;
            });
        }

        $scope.Start_meeting = function (application_gid) {
            $location.url('app/AgrTrnStartScheduledMeeting?application_gid=' + application_gid);
        }

        $scope.application_view = function (application_gid) {
            $location.url('app/AgrTrnCcCommitteeApplicationView?application_gid=' + application_gid + '&lspage=CCMmeeting');
        }

        $scope.completed_summary = function () {
            $location.url('app/AgrTrnCcCompletedScheduledMeeting');
        }

        $scope.history = function (application_gid) {
            $location.url('app/AgrTrnSentbackCadtoCcHistory?application_gid=' + application_gid + '&lspage=CCScheduledMeetingSummary');
        }

        $scope.assignment_view = function (application_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/approvalstatus_view.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params =
                  {
                      application_gid: application_gid
                  }
                var url = 'api/AgrTrnCC/GetScheduleMeeting';

                SocketService.getparams(url, params).then(function (resp) {
                    $scope.ccmember_list = resp.data.ccmember_list;
                    $scope.otheruser_list = resp.data.otheruser_list;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };


            }

        }
    }
})();
