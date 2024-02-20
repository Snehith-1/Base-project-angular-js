(function () {
    'use strict';

    angular
        .module('angle')
        .controller('coursecontroller', coursecontroller);

    coursecontroller.$inject = ['$rootScope', '$scope', '$state', '$modal', 'SweetAlert', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams'];

    function coursecontroller($rootScope, $scope, $state, $modal, SweetAlert, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'coursecontroller';

        activate();

        function activate() {

        }
        $scope.addcourse = function () {

            $state.go('app.addcourse');
        };
        $scope.viewfeaturedcourse = function () {

            $state.go('app.sectionsummary');
        };
        $scope.section = function () {
            $state.go('app.sectionsummary')
        }
        $scope.edit = function (val) {
            $state.go('app.editCourse')
        }
    }
})();
