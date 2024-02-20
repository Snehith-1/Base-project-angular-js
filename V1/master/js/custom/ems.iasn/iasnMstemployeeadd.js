(function () {
    'use strict';

    angular
        .module('angle')
        .controller('iasnMstemployeeaddController', iasnMstemployeeaddController);

        iasnMstemployeeaddController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', '$route', 'ngTableParams'];

    function iasnMstemployeeaddController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'iasnMstemployeeaddController';

        activate();

        function activate() { }

        $scope.addemployee = function () {
            $state.go('app.isanMstemployeeaddinfo');
        }

        $scope.back = function () {
            $state.go('app.isanMstTeamManagement');
        }
    }
})();
