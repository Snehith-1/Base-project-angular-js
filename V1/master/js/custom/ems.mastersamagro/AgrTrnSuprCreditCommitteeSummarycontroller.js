(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AgrTrnSuprCreditCommitteeSummaryController', AgrTrnSuprCreditCommitteeSummaryController);

    AgrTrnSuprCreditCommitteeSummaryController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function AgrTrnSuprCreditCommitteeSummaryController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AgrTrnSuprCreditCommitteeSummaryController';
        lockUI();
        activate();
        function activate() {
            var url = 'api/AgrTrnSuprCC/GetCCPendingSummary';
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
                $scope.lblsentbackcc_count = resp.data.cctocredit_count;
            });

        }

        $scope.application_view = function (application_gid) {
            $location.url('app/AgrTrnSuprCcCommitteeApplicationView?application_gid=' + application_gid + '&lspage=ScheduleMeeting');
        }
        $scope.schedule_meeting = function (application_gid) {
            $location.url('app/AgrTrnSuprCCMeetingSchedule?application_gid=' + application_gid + '&lstab=Pending');
        }
        $scope.scheduled_summary = function () {
            $location.url('app/AgrTrnSuprCCscheduledSummary');
        }
        $scope.pending_summary = function () {
            $location.url('app/AgrTrnSuprCreditCommitteeSummary');
        }
        $scope.completed_summary = function () {
            $location.url('app/AgrTrnSuprCCCompletedSummary');
        }
        $scope.sentbackto_credit = function () {
            $location.url('app/AgrTrnSuprSentbackcctoCredit');
        }

        $scope.sentback_credit = function (application_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/sentbacktounderwriting.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.submit = function () {
                    var params = {
                        cctocredit_reason: $scope.txtreason,
                        application_gid: application_gid
                    }
                    var url = 'api/AgrTrnSuprCC/PostRevertCCtoCredit';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {

                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            activate();
                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                        }
                    });

                    $modalInstance.close('closed');
                }
            }
        }

        $scope.history = function (application_gid) {
            $location.url('app/AgrTrnSuprSentbackCadtoCcHistory?application_gid=' + application_gid + '&lspage=Pending');
        }
    }
})();
