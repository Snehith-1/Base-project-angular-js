(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AgrTrnSuprCcCompletedScheduledMeetingcontroller', AgrTrnSuprCcCompletedScheduledMeetingcontroller);

    AgrTrnSuprCcCompletedScheduledMeetingcontroller.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function AgrTrnSuprCcCompletedScheduledMeetingcontroller($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AgrTrnSuprCcCompletedScheduledMeetingcontroller';

        lockUI();
        activate();
        function activate() {
            var url = 'api/AgrTrnSuprCC/GetCCCompletedSummary';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.applicationadd_list = resp.data.applicationadd_list;
            });

        }

        $scope.Start_meeting = function (application_gid) {
            $location.url('app/AgrTrnSuprStartScheduledMeeting?application_gid=' + application_gid + '&lspage=CCMmeetingScheduledcompleted');
        }

        $scope.application_view = function (application_gid) {
            $location.url('app/AgrTrnSuprCcCommitteeApplicationView?application_gid=' + application_gid + '&lspage=CCMmeetingScheduledcompleted');
        }

        $scope.history = function (application_gid) {
            $location.url('app/AgrTrnSuprSentbackCadtoCcHistory?application_gid=' + application_gid + '&lspage=CCCompletedScheduledMeetingSummary');
        }
    }
})();
