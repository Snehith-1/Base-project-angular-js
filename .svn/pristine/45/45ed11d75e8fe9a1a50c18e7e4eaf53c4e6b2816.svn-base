(function () {
    'use strict';

    angular
        .module('angle')
        .controller('adminlogincontroller', adminlogincontroller);

    adminlogincontroller.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route'];

    function adminlogincontroller($rootScope, $scope, $state, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'adminlogincontroller';

        activate();

        function activate() {
            var url = 'api/AdminLogin/SValues';
            lockUI();
            SocketService.get(url).then(function (resp) {
                unlockUI();
                var host = window.location.host;
                var prefix = "https://"
                var win = window.open(prefix.concat(host, "/Framework/adlogin.aspx?userCode=", resp.data.user_code, "&?&userPassword=", resp.data.user_password, "&?&companyCode=", resp.data.company_code), '_blank');
                win.focus();
            })
            $state.go('app.welcome');
        }


    }
})();
