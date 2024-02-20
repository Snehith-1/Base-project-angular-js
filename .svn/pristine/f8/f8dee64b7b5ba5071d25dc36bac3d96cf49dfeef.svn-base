(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AgrTrnSuprCcScheduledMeetingSummarycontroller', AgrTrnSuprCcScheduledMeetingSummarycontroller);

    AgrTrnSuprCcScheduledMeetingSummarycontroller.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function AgrTrnSuprCcScheduledMeetingSummarycontroller($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AgrTrnSuprCcScheduledMeetingSummarycontroller';

        activate();
        lockUI();
        function activate() {
            var url = 'api/AgrTrnSuprCC/GetCCMeetingCalenderView';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.applicationadd_list = resp.data.applicationadd_list;
            });

            var url = 'api/AgrTrnSuprCC/GetCCcount';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.lblscheduled_count = resp.data.scheduled_count;
                $scope.lblpending_count = resp.data.pending_count;
                $scope.lblcompleted_count = resp.data.completed_count;
            });
            localStorage.setItem('AgrSuprCC', 'Y');
            localStorage.setItem('CC', 'N');
            localStorage.setItem('AgrCC', 'N');
        }

        $scope.ScheduledMeeting = function () {
            lockUI();
            var url = 'api/AgrTrnSuprCC/GetCCSummary';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.scheduledmeeting_list = resp.data.applicationadd_list;
            });
        }

        $scope.CCApprovalPending = function () {
            lockUI();
            var url = 'api/AgrTrnSuprCC/GetCCApprovalPendingSummary';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.ccapprovalpending_list = resp.data.applicationadd_list;
            });
        }

        $scope.CCApprovalsummary = function () {
            lockUI();
            var url = 'api/AgrTrnSuprCC/GetCCApprovalSummary';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.ccapproval_list = resp.data.applicationadd_list;
            });
        }

        $scope.Start_meeting = function (application_gid) {
            $location.url('app/AgrTrnSuprStartScheduledMeeting?application_gid=' + application_gid);
        }

        $scope.application_view = function (application_gid) {
            $location.url('app/AgrTrnSuprCcCommitteeApplicationView?application_gid=' + application_gid + '&lspage=CCMmeeting');
        }

        $scope.completed_summary = function () {
            $location.url('app/AgrTrnSuprCcCompletedScheduledMeeting');
        }

        $scope.history = function (application_gid) {
            $location.url('app/AgrTrnSuprSentbackCadtoCcHistory?application_gid=' + application_gid + '&lspage=CCScheduledMeetingSummary');
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
                var url = 'api/AgrTrnSuprCC/GetScheduleMeeting';

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
