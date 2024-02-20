(function () {
    'use strict';

    angular
        .module('angle')
        .controller('sidebarcontroller', sidebarcontroller);

    sidebarcontroller.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route'];

    function sidebarcontroller($rootScope, $scope, $state, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'sidebarcontroller';

        activate();

        function activate() {
        }
    }
})();
