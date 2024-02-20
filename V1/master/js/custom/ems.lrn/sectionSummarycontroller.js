(function () {
    'use strict';

    angular
        .module('angle')
        .controller('sectionSummarycontroller', sectionSummarycontroller);

    sectionSummarycontroller.$inject = ['$rootScope', '$scope', '$state', '$modal', 'SweetAlert', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams'];

    function sectionSummarycontroller($rootScope, $scope, $state, $modal, SweetAlert, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'sectionSummarycontroller';

        activate();

        function activate() { }
        $scope.lesson = function () {
            $state.go('app.lessonSummary')
        }
        $scope.section = function () {
            $state.go('app.addSection')
        }
        $scope.edit = function (val) {
            $state.go('app.editSection')
        }
    }
})();
