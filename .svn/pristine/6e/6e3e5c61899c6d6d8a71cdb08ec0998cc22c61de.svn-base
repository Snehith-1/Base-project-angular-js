(function () {
    'use strict';

    angular
        .module('angle')
        .controller('myTeamProfilecontroller', myTeamProfilecontroller);

    myTeamProfilecontroller.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams'];

    function myTeamProfilecontroller($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'myTeamProfilecontroller';

        activate();

        function activate() {
            var params = {
                employee_gid: localStorage.getItem('employee_gid')
            };
            var url = "api/myTeam/teamemployeeprofile";
            SocketService.getparams(url, params).then(function (resp) {
                $scope.employeedetails = resp.data;
                if (resp.data.employee_photo != "N") {
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
                console.log(resp);
            });

            var url = "api/myTeam/teamemployeehiearchy"
            SocketService.getparams(url, params).then(function (resp) {
                $scope.employeeteam = resp.data.myteam_list;
            });
        }

        $scope.teamprofile = function (employee_gid) {
            var params = {
                employee_gid: employee_gid
            };
            $scope.employee_gid = localStorage.setItem('employee_gid', employee_gid);
            activate();
        }
    }
})();
