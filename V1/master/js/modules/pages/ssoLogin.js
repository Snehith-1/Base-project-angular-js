(function () {
    'use strict';

    angular
        .module('app.pages')
        .controller('ssoLogin', ssoLogin);

    ssoLogin.$inject = ['$rootScope', '$scope', '$state', '$cookies', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route'];
    function ssoLogin($rootScope, $scope, $state, $cookies, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route) {
        var vm = this;

        activate();
        function activate() {

        };
        $scope.login = function () {
            $scope.authMs = false;

            var params = {
                user_code: $scope.user_code,
                user_password: $scope.user_password
            };
            var url = apiManage.apiList['login'].api;
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    $cookies.putObject('token', resp.data.token);
                    localStorage.setItem('user_gid', resp.data.user_gid);
                    $state.go('app.welcome');
                    unlockUI();
                }
                else {
                    unlockUI();               
                   alert('Invalid Credentials');
                }
            });
        };

        $scope.sso = function () {
            var url = "https://login.microsoftonline.com/d71b0d7f-10d7-46cf-80f6-b8dc3924a66a/oauth2/v2.0/authorize?client_id=62a06863-baa5-4adf-b13b-2efb32aac417&response_type=code&redirect_uri=http://localhost/v1/response.html&response_mode=query&scope=https://graph.microsoft.com/User.Read&state=12345"
            window.location = url;
        }

    }
})();