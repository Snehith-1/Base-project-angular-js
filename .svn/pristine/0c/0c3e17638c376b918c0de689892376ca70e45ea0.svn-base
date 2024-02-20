(function () {
    'use strict';

    angular
        .module('angle')
        .controller('lessonSummarycontroller', lessonSummarycontroller);

    lessonSummarycontroller.$inject = ['$rootScope', '$scope', '$state', '$modal', 'SweetAlert', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams'];

    function lessonSummarycontroller($rootScope, $scope, $state, $modal, SweetAlert, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'lessonSummarycontroller';

        activate();

        function activate() { }
        $scope.addlesson = function () {
            $state.go('app.addlesson')
        }
        $scope.edit = function (val) {
            $state.go('app.editlesson')
        }
    }
})();
