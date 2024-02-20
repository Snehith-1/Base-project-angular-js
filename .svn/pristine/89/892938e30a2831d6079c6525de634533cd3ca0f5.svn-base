(function () {
    'use strict';

    angular
        .module('angle')
        .controller('legalapprovalmgmt', legalapprovalmgmt);

    legalapprovalmgmt.$inject = ['$rootScope', '$scope', '$modal', '$state', 'SweetAlert', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', '$cookies', '$filter', 'ngTableParams', '$resource', '$timeout', 'ngTableDataService'];

    function legalapprovalmgmt($rootScope, $scope, $modal, $state, SweetAlert, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, $cookies, $filter, ngTableParams, $resource, $timeout, ngTableDataService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'legalapprovalmgmt';

        activate();

        function activate() {
            var url = 'api/raiseLegalSR/legalSRAppovesummary';
            lockUI();
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.mdlMisdataimport = resp.data.RaiselegalSR_list;
                
            });
        }
        $scope.approve360 = function (legalsr_gid, customer_gid, approval_status) {
            $scope.legalsr_gid = localStorage.setItem('legalsr_gid', legalsr_gid);
            $scope.customer_gid = localStorage.setItem('customer_gid', customer_gid);
            $scope.approval_status = localStorage.setItem('approval_status', approval_status);
            $state.go('app.legalSRapprovemgmt360');
            console.log(customer_gid);
        }
    }
})();
