(function () {
    'use strict';

    angular
        .module('angle')
        .controller('legalSLNmgmt', legalSLNmgmt);

    legalSLNmgmt.$inject = ['$rootScope', '$scope', '$modal', '$state', 'SweetAlert', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', '$cookies', '$filter', 'ngTableParams', '$resource', '$timeout', 'ngTableDataService'];

    function legalSLNmgmt($rootScope, $scope, $modal, $state, SweetAlert, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, $cookies, $filter, ngTableParams, $resource, $timeout, ngTableDataService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'legalSLNmgmt';

        activate();


        function activate() {
            var url = 'api/raiseLegalSR/getlegalSRAppovemgmtSummary';
            lockUI();
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.mdlMisdataimport = resp.data.RaiselegalSR_list;
              console.log(resp.data);
            });

        }

        $scope.raiseapprove = function (legalsr_gid, customer_gid, auth_status) {
            $scope.legalsr_gid = localStorage.setItem('legalsr_gid', legalsr_gid);
            $scope.customer_gid = localStorage.setItem('customer_gid', customer_gid);
            $scope.auth_status = localStorage.setItem('auth_status', auth_status);
            $state.go('app.legalSR360');
        }
    }
})();
