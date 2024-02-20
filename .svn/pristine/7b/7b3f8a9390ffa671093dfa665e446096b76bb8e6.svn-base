(function () {
    'use strict';

    angular
        .module('angle')
        .controller('viewMyAssetcontroller', viewMyAssetcontroller);

    viewMyAssetcontroller.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route'];

    function viewMyAssetcontroller($rootScope, $scope, $state, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'viewMyAssetcontroller';

        activate();

        function activate() {
            //lockUI();
            var url = 'api/viewMyAsset/myasset';
            SocketService.get(url).then(function (resp) {
                //unlockUI();
                $scope.myassetsummary = resp.data.myassetsummary;
            });
        }
    }
})();
