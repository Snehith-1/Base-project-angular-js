(function () {
    'use strict';

    angular
        .module('angle')
        .controller('featuredCoursecontroller', featuredCoursecontroller);

    featuredCoursecontroller.$inject = ['$rootScope', '$scope', '$state', '$modal', 'SweetAlert', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams'];

    function featuredCoursecontroller($rootScope, $scope, $state, $modal, SweetAlert, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'featuredCoursecontroller';

        activate();

        function activate() { }
        $scope.featuredcourseback = function () {
            $state.go('app.course')
        }
    }
})();
