(function () {
    'use strict';

    angular
        .module('angle')
        .controller('SysMstStateSummaryController', SysMstStateSummaryController);

    SysMstStateSummaryController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function SysMstStateSummaryController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'SysMstStateSummaryController';

        activate();

        function activate() {
            var url = 'api/SystemMaster/GetGstStateSummary';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.state_data = resp.data.master_list;
                unlockUI();
            });
        }

       
    }
})();

