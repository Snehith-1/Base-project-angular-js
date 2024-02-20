(function(){

    'use strict';

    angular
    .module('angle')
    .controller('navbarctrl',navbarctrl);

    navbarctrl.$inject=['$rootScope', '$scope', '$modal', '$state',  'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', '$cookies', '$filter',  '$resource', '$timeout'];
    function navbarctrl($rootScope, $scope, $modal, $state, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, $cookies, $filter,  $resource, $timeout) {
        var vm =this;
        vm.title = 'Navbar Contrller';

        activate();

        function activate() {
         
            var user_gid = localStorage.getItem('user_gid');
            var url = apiManage.apiList['menu'].api;
            SocketService.get(url + '?user_gid=' + user_gid).then(function (resp) {
                $scope.menu_list = resp.data.menu_list;
                //console.log($scope.menu_list);
            });

            var user_gid = localStorage.getItem('user_gid');
            var url = 'api/user/userData';
            SocketService.get(url + '?user_gid=' + user_gid).then(function (resp) {
                if (resp.data.status == true) {     
                    $scope.userData = resp.data;
                    
                }
            });

        }   
    }
})();