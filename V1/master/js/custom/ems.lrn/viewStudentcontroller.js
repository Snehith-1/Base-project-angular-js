(function () {
    'use strict';

    angular
        .module('angle')
        .controller('viewStudentcontroller', viewStudentcontroller);

    viewStudentcontroller.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function viewStudentcontroller($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'viewStudentcontroller';

        activate();

        function activate() {
            var params = {
                student_gid: localStorage.getItem('student_gid')
            };
    
            var url = 'api/student/studentUpdatedetails';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.student_details = resp.data;
            });
        }
        $scope.back = function () {
            $state.go('app.studentSummary');
        }
    }
})();
