(function () {
    'use strict';

    angular
        .module('angle')
        .controller('OtherApplicationDashboardController', OtherApplicationDashboardController);

        OtherApplicationDashboardController.$inject = ['$rootScope', '$scope', '$state', '$modal', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams'];

    function OtherApplicationDashboardController($rootScope, $scope, $state, $modal, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'OtherApplicationDashboardController';

        activate();

        function activate() {
            
            var url = 'api/OtherApplication/GetOtherApplication';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.otherapplication_list = resp.data.otherapplication_list;
            });
            var url = 'api/OtherApplication/Assignedlinks';
            SocketService.get(url).then(function (resp) {
                $scope.otherapplicationemployee_list = resp.data.otherapplication_list;
            });
            unlockUI();
        }   
    }
})();