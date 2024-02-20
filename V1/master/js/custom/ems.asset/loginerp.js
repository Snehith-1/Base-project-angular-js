(function () {
    'use strict';

    angular
        .module('angle')
        .controller('loginerp', loginerp);

    loginerp.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$cookies', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route'];

    function loginerp($rootScope, $scope, $state, AuthenticationService, $cookies, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'loginerp';

        activate();

        function activate() {
            var params = {
                user_code: getCookie("user_code"),
                company_code: getCookie("company_code")
            }
            console.log(params);
            var url = 'api/Login/LoginERP';
            lockUI();
            SocketService.postlogin(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status = true) {
                    $cookies.putObject('token', resp.data.token);
                    localStorage.setItem('user_gid', resp.data.user_gid)
                    $state.go('app.welcome');
                }
            })
        }
        function getCookie(cname) {
            var name = cname + "=";
            var decodedCookie = decodeURIComponent(document.cookie);
            var ca = decodedCookie.split(';');
            for (var i = 0; i < ca.length; i++) {
                var c = ca[i];
                while (c.charAt(0) == ' ') {
                    c = c.substring(1);
                }
                if (c.indexOf(name) == 0) {
                    return c.substring(name.length, c.length);
                }
            }
            return "";
        }
    }
})();
