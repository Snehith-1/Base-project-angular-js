(function () {
    'use strict';

    angular
        .module('app.pages')
        .controller('responseController', responseController);
    responseController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService','$cookies', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route','$routeParams'];
    function responseController($rootScope, $scope, $state, AuthenticationService,$cookies, $http, SocketService, Notify, $location, apiManage, $route,$routeParams) {
        var param1 = $location.search();       
        lockUI();
        var url = 'api/Login/LoginReturn';
        SocketService.post(url, param1).then(function (resp) {
            if (resp.data.user_gid != null)
            {
                $cookies.putObject('token', resp.data.token);
                localStorage.setItem('user_gid', resp.data.user_gid);
                $state.go('app.welcome');
                unlockUI();
            }
            else if ((resp.data.user_gid ==null) || (resp.data.user_gid ==""))
            {
                unlockUI();
                Notify.alert('Error Occured.Try Again..!!', {
                    status: 'danger',
                    pos: 'top-center',
                    timeout: 3000
                });
                $state.go('page.login');
            }
        });
    }
    function load()
    {
        $state.go('app.welcome');
    }
}
)();