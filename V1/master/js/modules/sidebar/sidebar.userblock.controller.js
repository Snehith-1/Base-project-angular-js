(function() {
    'use strict';

    angular
        .module('app.sidebar')
        .controller('UserBlockController', UserBlockController);

    UserBlockController.$inject = ['$rootScope', '$scope', '$state', '$cookies', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route'];
    function UserBlockController($rootScope, $scope, $state, $cookies, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route) {

        activate();

        ////////////////

        function activate() {
            $rootScope.user = {
                picture:'app/img/user/user-avatar.png'
            };
            var user_gid = localStorage.getItem('user_gid');
            var url = apiManage.apiList['userData'].api;
            SocketService.get(url+'?user_gid='+ user_gid).then(function (resp) {
                if (resp.data.status == true) {
                    $scope.userData = resp.data;
                }
            });

          // Hides/show user avatar on sidebar
          $rootScope.toggleUserBlock = function(){
            $rootScope.$broadcast('toggleUserBlock');
          };

          $rootScope.userBlockVisible = false;
          
          $rootScope.$on('toggleUserBlock', function(/*event, args*/) {

            $rootScope.userBlockVisible = ! $rootScope.userBlockVisible;
            
          });
        }
    }
})();
