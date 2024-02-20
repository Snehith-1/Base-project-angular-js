(function () {
    'use strict';

    angular
        .module('angle')
        .controller('itDashboardcontroller', itDashboardcontroller);

    itDashboardcontroller.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams'];

    function itDashboardcontroller($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'itDashboardcontroller';

        activate();

        function activate() {
            
            var user_gid = localStorage.getItem('user_gid');
            var url = 'api/user/privilegelevel3';
            SocketService.get(url + '?user_gid=' + user_gid).then(function (resp) {
                var newticket = resp.data.privilegelevel3.map(function (e) { return e.project }).indexOf("ITSSVTNEW");
                var viewticket = resp.data.privilegelevel3.map(function (e) { return e.project }).indexOf("ITSSVTVIW");
                if (newticket != -1) {                 
                    $scope.newticket = 'Y';
                }
                if (viewticket != -1) {
                    $scope.viewticket = 'Y';                   
                }

            });

        }
    }
})();
