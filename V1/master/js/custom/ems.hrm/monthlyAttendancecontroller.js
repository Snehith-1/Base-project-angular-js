(function () {
    'use strict';

    angular
        .module('angle')
        .controller('monthlyAttendancecontroller', monthlyAttendancecontroller);

    monthlyAttendancecontroller.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams'];

    function monthlyAttendancecontroller($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'monthlyAttendancecontroller';

        activate();

        function activate() {

            console.log("test");
            var url = "api/hrmDashboard/monthlyAttendenceReport";
            SocketService.get(url).then(function (resp) {
           
                $scope.attendance_report = resp.data.monthlyAttendenceReport_list;
            });
        }

           
    }
})();
