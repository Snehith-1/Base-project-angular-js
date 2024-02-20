(function () {
    'use strict';

    angular
        .module('angle')
        .controller('iasnMstcheckeraddController', iasnMstcheckeraddController);

        iasnMstcheckeraddController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function iasnMstcheckeraddController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'iasnMstcheckeraddController';

        activate();

        function activate() { }

        $scope.addchecker = function () {
            $state.go('app.isanMstcheckeraddinfo');
        }

        $scope.back = function () {
            $state.go('app.isanMstTeamManagement');
        }
    }
})();
