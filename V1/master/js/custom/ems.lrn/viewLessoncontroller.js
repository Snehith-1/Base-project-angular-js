(function () {
    'use strict';

    angular
        .module('angle')
        .controller('viewLessoncontroller', viewLessoncontroller);

    viewLessoncontroller.$inject = ['$rootScope', '$scope', '$state', '$modal', 'SweetAlert', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams'];

    function viewLessoncontroller($rootScope, $scope, $state, $modal, SweetAlert, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'viewLessoncontroller';

        activate();

        function activate() { }
        $scope.viewlessonback = function () {
            $state.go('app.lessonSummary')
        }
    }
})();
