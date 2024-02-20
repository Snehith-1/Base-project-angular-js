(function () {
    'use strict';
    angular
           .module('angle')
           .controller('AgrMstRMcustomerSummary', AgrMstRMcustomerSummary);

    AgrMstRMcustomerSummary.$inject = ['$rootScope', '$scope', '$modal', '$state', 'SweetAlert', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', '$cookies', '$filter', 'ngTableParams', '$resource', '$timeout', 'ngTableDataService'];

    function AgrMstRMcustomerSummary($rootScope, $scope, $modal, $state, SweetAlert, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, $cookies, $filter, ngTableParams, $resource, $timeout, ngTableDataService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AgrMstRMcustomerSummary';

        activate();

        function activate() {
            lockUI();
            var url = 'api/AgrMstScannedDocument/GetRMSummary';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.mycustomer_data = resp.data.customerRMsummary;
            });

        }
        //$scope.myteamcustomer = function () {
        //    lockUI();
        //    var url = 'api/AgrMstScannedDocument/GetMyTeamRMSummary';
        //    SocketService.get(url).then(function (resp) {
        //        unlockUI();
        //        $scope.myteamcustomer_data = resp.data.customerRMsummary;
        //    });
        //}


        $scope.customerView = function (val) {
           $location.url('app/AgrMstCadApplicationView?application_gid=' + val + '&lspage=CadRMCustomer');
        }

        $scope.process = function (application_gid) {
           $location.url('app/AgrTrnPostCcActivitiesRMView?application_gid=' + application_gid);
        }
    }
})();
