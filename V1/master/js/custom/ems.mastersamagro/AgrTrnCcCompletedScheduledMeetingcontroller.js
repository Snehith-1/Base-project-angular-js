(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AgrTrnCcCompletedScheduledMeetingcontroller', AgrTrnCcCompletedScheduledMeetingcontroller);

    AgrTrnCcCompletedScheduledMeetingcontroller.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function AgrTrnCcCompletedScheduledMeetingcontroller($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AgrTrnCcCompletedScheduledMeetingcontroller';

        lockUI();
        activate();
        function activate() {
            var url = 'api/AgrTrnCC/GetCCCompletedSummary';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.applicationadd_list = resp.data.applicationadd_list;
            });

        }

        $scope.Start_meeting = function (application_gid) {
            $location.url('app/AgrTrnStartScheduledMeeting?application_gid=' + application_gid + '&lspage=CCMmeetingScheduledcompleted');
        }

        $scope.application_view = function (application_gid) {
            $location.url('app/AgrTrnCcCommitteeApplicationView?application_gid=' + application_gid + '&lspage=CCMmeetingScheduledcompleted');
        }

        $scope.history = function (application_gid) {
            $location.url('app/AgrTrnSentbackCadtoCcHistory?application_gid=' + application_gid + '&lspage=CCCompletedScheduledMeetingSummary');
        }
    }
})();
