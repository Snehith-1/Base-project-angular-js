(function () {
    'use strict';

    angular
        .module('angle')
        .controller('user_logincontroller', user_logincontroller);

    user_logincontroller.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', '$cookies'];

    function user_logincontroller($rootScope, $scope, $state, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, $cookies) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'user_logincontroller';

        activate();

        function activate() {
        }

        $scope.submitclick = function () {
            var params = {
                user_code: $scope.user_code,
                user_password: $scope.user_password
            }
            var url = apiManage.apiList['user_login'].api;
            lockUI();
            SocketService.postlogin(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status = true) {
                    $cookies.putObject('token', resp.data.token);
                    localStorage.setItem('user_name', resp.data.username)
                    $state.go('app.landingpage');
                }
            })
        };
    }
})();
