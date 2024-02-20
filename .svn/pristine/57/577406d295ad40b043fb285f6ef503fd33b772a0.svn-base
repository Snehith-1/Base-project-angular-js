(function () {
    'use strict';

    angular
        .module('vcx')
        .controller('vendorlogin', vendorlogin);

    vendorlogin.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', '$cookies'];

    function vendorlogin($rootScope, $scope, $state, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, $cookies) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'vendorlogin';

        activate();

        function activate() {
            document.cookie = "token=; expires=Thu, 01 Jan 1970 00:00:00 UTC; path=/vp;"
            localStorage.removeItem('user_name');
        }

        $scope.login = function () {
            var params = {
                app_code: $scope.userCode,
                password: $scope.passWord
            }
            var url = apiManage.apiList['vendorLogin'].api;
            lockUI();
            SocketService.postlogin(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.message == 'usercode') {
                    Notify.alert('User Code InValid', {
                        status: 'warning',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
                else if (resp.data.message == 'password') {
                    Notify.alert('Password InValid', {
                        status: 'danger',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
                else if (resp.data.message == 'success') {
                    $cookies.putObject('token', resp.data.token);
                    localStorage.setItem('user_name', resp.data.username);
                    $state.go('app.welcome');
                }
                else if(resp.data.message=='appcode') {
                    Notify.alert('You are Not allowed to login', {
                        status: 'warning',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
                else if (resp.data.message == 'userstatus') {
                    Notify.alert('Application Deactivated, You are Not allowed to login', {
                        status: 'warning',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
                else {
                    window.alert(resp.data.message)
                }
            });
        }

        
    }
})();
