(function () {
    'use strict';

    angular
        .module('vcx')
        .controller('vendorportal', vendorportal);

    vendorportal.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', '$cookies'];

    function vendorportal($rootScope, $scope, $state, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, $cookies) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'vendorportal';

        activate();

        function activate() {
            var url = apiManage.apiList['vendordetail'].api;
            lockUI();
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.data = resp.data;
            });

            $scope.go=function(){
                $state.go('app.dashboard');
            }

        }
    }
})();
