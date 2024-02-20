(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstCcCompletedScheduledMeetingController', MstCcCompletedScheduledMeetingController);

        MstCcCompletedScheduledMeetingController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function MstCcCompletedScheduledMeetingController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstCcCompletedScheduledMeetingController';
        
        lockUI();
        activate();
        function activate() {
            var url = 'api/MstCC/GetCCCompletedSummary';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.applicationadd_list = resp.data.applicationadd_list;
            });  
                    
        }       
        
        $scope.Start_meeting = function (application_gid) {
            $location.url('app/MstStartScheduledMeeting?application_gid=' + application_gid + '&lspage=CCMmeetingScheduledcompleted');
        }

        $scope.application_view = function (application_gid) {
            $location.url('app/MstCcCommitteeApplicationView?application_gid=' + application_gid + '&lspage=CCMmeetingScheduledcompleted');
        }

        $scope.history = function (application_gid) {
            $location.url('app/MstSentbackCadtoCcHistory?application_gid=' + application_gid + '&lspage=CCCompletedScheduledMeetingSummary');
        }
    }
})();
