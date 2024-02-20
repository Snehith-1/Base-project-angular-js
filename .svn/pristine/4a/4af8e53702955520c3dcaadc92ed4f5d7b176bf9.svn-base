(function () {
    'use strict';

    angular
        .module('angle')
        .controller('allocationManagementcontroller', allocationManagementcontroller);

    allocationManagementcontroller.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function allocationManagementcontroller($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'allocationManagementcontroller';

        activate();

        function activate() {
            var url = "api/allocationManagement/getallocationSummary";
            SocketService.get(url).then(function (resp) {
                $scope.allocationList = resp.data.mappingdtl;
            });
        }

        $scope.createAllocation = function () {
            $state.go('app.allocationCreate');
        }

        $scope.viewallocation = function (allocationdtl_gid, customer_gid) {
            console.log(allocationdtl_gid, customer_gid);
            localStorage.setItem('allocationdtl_gid', allocationdtl_gid);
            localStorage.setItem('allocation_customer_gid', customer_gid)
            $state.go('app.allocationView');
        }
    }
})();
