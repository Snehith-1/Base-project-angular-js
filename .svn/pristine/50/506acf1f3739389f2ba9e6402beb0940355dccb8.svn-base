(function () {
    'use strict';
    angular
           .module('angle')
           .controller('AgrMstSuprRMCustomerSummary', AgrMstSuprRMCustomerSummary);

           AgrMstSuprRMCustomerSummary.$inject = ['$rootScope', '$scope', '$modal', '$state', 'SweetAlert', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', '$cookies', '$filter', 'ngTableParams', '$resource', '$timeout', 'ngTableDataService'];

    function AgrMstSuprRMCustomerSummary($rootScope, $scope, $modal, $state, SweetAlert, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, $cookies, $filter, ngTableParams, $resource, $timeout, ngTableDataService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AgrMstSuprRMCustomerSummary';

        activate();

        function activate() {
            lockUI();
            var url = 'api/AgrMstSuprScannedDocument/GetRMSummary';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.mycustomer_data = resp.data.customerRMsummary;
            });

        }
        $scope.myteamcustomer = function () {
            lockUI();
            var url = 'api/AgrMstSuprScannedDocument/GetMyTeamRMSummary';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.myteamcustomer_data = resp.data.customerRMsummary;
            });
        }


        $scope.customerView = function (val) {
           $location.url('app/AgrMstSuprCadApplicationView?application_gid=' + val + '&lspage=CadRMCustomer');
        }

        $scope.process = function (application_gid) {
           $location.url('app/AgrTrnSuprPostCcActivitiesRMView?application_gid=' + application_gid);
        }
    }
})();
