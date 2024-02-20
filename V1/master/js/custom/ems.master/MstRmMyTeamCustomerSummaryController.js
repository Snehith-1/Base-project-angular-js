(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstRmMyTeamCustomerSummaryController', MstRmMyTeamCustomerSummaryController);

    MstRmMyTeamCustomerSummaryController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'SweetAlert', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', '$cookies', '$filter', 'ngTableParams', '$resource', '$timeout', 'ngTableDataService'];

    function MstRmMyTeamCustomerSummaryController($rootScope, $scope, $modal, $state, SweetAlert, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, $cookies, $filter, ngTableParams, $resource, $timeout, ngTableDataService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstRmMyTeamCustomerSummaryController';

        activate();

        function activate() {
            var url = 'api/MstScannedDocument/GetMyTeamRMSummary';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.myteamcustomer_data = resp.data.customerRMsummary;
            });
        }

        $scope.mycustomerlist_summary = function () {
            $location.url('app/MstRMCustomerSummary');
        }

    }
})();
