(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstSentbackcctoCreditController', MstSentbackcctoCreditController);

    MstSentbackcctoCreditController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function MstSentbackcctoCreditController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstSentbackcctoCreditController';

        lockUI();
        activate();
        function activate() {
            var url = 'api/MstCC/GetCCtoCreditSummary';
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
            $location.url('app/MstCcCommitteeApplicationView?application_gid=' + application_gid + '&lspage=SentBackToCredit');
        }
        $scope.schedule_meeting = function (application_gid) {
            $location.url('app/MstCCMeetingSchedule?application_gid=' + application_gid + '&lstab=Pending');
        }
        $scope.scheduled_summary = function () {
            $location.url('app/MstCCscheduledSummary');
        }
        $scope.pending_summary = function () {
            $location.url('app/MstCreditCommitteeSummary');
        }
        $scope.completed_summary = function () {
            $location.url('app/MstCCCompletedSummary');
        }
        $scope.sentbackto_credit = function () {
            $location.url('app/MstSentbackcctoCredit');
        }
        $scope.view_reason = function (application_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/Reasonview.html',
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
                var url = 'api/MstCC/GetAppRevertReasonRemarks';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.txtreason = resp.data.cctocredit_reason;
                });               

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };


            }

        }

        $scope.history = function (application_gid) {
            $location.url('app/MstSentbackCadtoCcHistory?application_gid=' + application_gid + '&lspage=SentbackcctoCredit');
        }
        $scope.ccmeeting_skiptab = function () {
            $location.url('app/MstCcMeetingSkipSummary');
        }
    }
})();
