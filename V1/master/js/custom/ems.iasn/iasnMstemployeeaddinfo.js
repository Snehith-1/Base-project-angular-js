(function () {
    'use strict';

    angular
        .module('angle')
        .controller('iasnMstemployeeaddinfoController', iasnMstemployeeaddinfoController);

        iasnMstemployeeaddinfoController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', '$route', 'ngTableParams'];

    function iasnMstemployeeaddinfoController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'iasnMstemployeeaddinfoController';

        activate();

        function activate() {
            var url = 'api/employee/employee';
            SocketService.get(url).then(function (resp) {
                $scope.employee_list = resp.data.employee_list;
                $scope.employee_code = resp.data.criticallity;
                $scope.employee_mailid = resp.data.comments;
            });
        }

        $scope.onselectedchangeemployee = function (employee) {
            $scope.employee_gid = localStorage.setItem('onchangeemployee_gid', employee);
            var params = {
                employee_gid: $scope.employeegid.employee_gid

            }

        }

        $scope.employeeback = function () {
            $state.go('app.isanMstemployeeadd');
        }
    }
})();
