(function () {
    'use strict';

    angular
        .module('angle')
        .controller('OsdBamRMCompletedSummaryController', OsdBamRMCompletedSummaryController);

    OsdBamRMCompletedSummaryController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', '$route', 'ngTableParams', '$sce','cmnfunctionService'];

    function OsdBamRMCompletedSummaryController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, $route, ngTableParams, $sce,cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'OsdBamRMCompletedSummaryController';

        activate();

        function activate() {
            var url = 'api/OsdTrnBankAlert/GetCompletedSummary';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.BankAlertCompleted_list = resp.data.BankAlertCompleted_list;
                unlockUI();
            });
        }

        $scope.Back = function () {
            $location.url('app/osdBamAllocatedToRM');
        }

        $scope.BankalertView = function (bankalert2allocated_gid, customer_gid,customer_urn) {
            $location.url('app/osdBamAllocatedToRMView?hash=' + cmnfunctionService.encryptURL('lsbankalert2allocated_gid=' + bankalert2allocated_gid + '&lscustomer_gid=' + customer_gid + '&lscustomer_urn=' + customer_urn + '&lspage=Completedsummary'));
        }

    }
})();
