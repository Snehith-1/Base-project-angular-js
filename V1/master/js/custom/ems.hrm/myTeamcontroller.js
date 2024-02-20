(function () {
    'use strict';

    angular
        .module('angle')
        .controller('myTeamcontroller', myTeamcontroller);

    myTeamcontroller.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams'];

    function myTeamcontroller($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'myTeamcontroller';

        activate();

        function activate() {

            var url = "api/myTeam/myteam";
            SocketService.get(url).then(function (resp) {
                $scope.myteam = resp.data.myteam_list;
            });

            var url = "api/myProfile/employeedetails";
            SocketService.get(url).then(function (resp) {
                $scope.employeedetails = resp.data;

                if (resp.data.employee_photo != "N")
                {
                    var pathArray = location.href.split('/');
                    var protocol = pathArray[0];
                    var host = pathArray[2];
                    var url = protocol + '//' + host;
                    var str = resp.data.employee_photo;
                    str = str.substring(str.indexOf("EMS/") + 3);
                    $scope.employee_photo = url.concat(str);
                }
                else {
                    $scope.employee_photo = resp.data.employee_photo;
                }
            });
        }

        $scope.teamprofile = function (employee_gid) {
            $scope.team_employeegid = localStorage.setItem('employee_gid', employee_gid);
            $state.go('app.myTeamEmployeeProfile');
        }
    }
})();
