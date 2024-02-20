(function () {
    'use strict';

    angular
        .module('angle')
        .controller('rmTransfercontroller', rmTransfercontroller);

    rmTransfercontroller.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', '$route', '$cookies', '$filter', 'ngTableParams', '$timeout'];

    function rmTransfercontroller($rootScope, $scope, $state, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, $route, $cookies, $filter, ngTableParams, $timeout) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'rmTransfercontroller';

        activate();

        function activate() {
            var url = "api/allocationManagement/getRMallocationList";
            SocketService.get(url).then(function (resp) {
                $scope.RMallocation = resp.data.mappingdtl;
            });

        }
        $scope.transferRMdtl = function (assignedRM_gid) {
            localStorage.setItem('assignedRM_gid', assignedRM_gid);
            $state.go('app.rmAllocationTransfer');
        }
    }
})();
