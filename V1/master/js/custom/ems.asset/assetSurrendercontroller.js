(function () {
    'use strict';

    angular
        .module('angle')
        .controller('assetSurrendercontroller', assetSurrendercontroller);

    assetSurrendercontroller.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route'];

    function assetSurrendercontroller($rootScope, $scope, $state, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'assetSurrendercontroller';
        activate();

        function activate() {
            var url = 'api/surrenderAsset/surrender';
            lockUI();
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.surrendersummary = resp.data.surrendersummary;
            });
        }

        $scope.surrender_submit = function (val1) {
            var params = { asset2custodian_gid: val1 }
            var url = 'api/surrenderAsset/submitsurrender';
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status) {
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    activate();
                }
            })
        };
    }
})();
