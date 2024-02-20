(function () {
    'use strict';

    angular
        .module('angle')
        .controller('rmAllocationcontroller', rmAllocationcontroller);

    rmAllocationcontroller.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', '$route', '$cookies', '$filter', 'ngTableParams', '$timeout'];

    function rmAllocationcontroller($rootScope, $scope, $state, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, $route, $cookies, $filter, ngTableParams, $timeout) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'rmAllocationcontroller';

        activate();

        function activate() {

            var url = "api/allocationManagement/getRMallocateddetails";
            SocketService.get(url).then(function (resp) {
                $scope.allocatedList = resp.data.mappingdtl;
                console.log(resp);
            });
        }

        $scope.viewallocation = function (allocationdtl_gid, customer_gid) {
            localStorage.setItem('allocationdtl_gid', allocationdtl_gid);
            localStorage.setItem('allocation_customer_gid', customer_gid);
            localStorage.setItem('MyAllocation', 'Y');
            $state.go('app.allocationView');
        }

        $scope.genereteallocation = function (allocationdtl_gid, customer_gid)
        {
            localStorage.setItem('allocationdtl_gid', allocationdtl_gid);
            localStorage.setItem('allocation_customer_gid', customer_gid);
            $state.go('app.visitReportGenerate');
        }
    }
})();
