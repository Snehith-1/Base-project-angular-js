(function () {
    'use strict';

    angular
        .module('angle')
        .controller('viewCoursecontroller', viewCoursecontroller);

    viewCoursecontroller.$inject = ['$rootScope', '$scope', '$state', '$modal', 'SweetAlert', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams'];

    function viewCoursecontroller($rootScope, $scope, $state, $modal, SweetAlert, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'viewCoursecontroller';

        activate();

        function activate() { }
        $scope.viewcourseback = function () {
            $state.go('app.lessonSummary')
        }

    }
})();
