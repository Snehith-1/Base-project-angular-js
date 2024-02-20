(function () {
    'use strict';

    angular
        .module('angle')
        .controller('osdBamTicketCompletedSummary', osdBamTicketCompletedSummary);

    osdBamTicketCompletedSummary.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', '$route', 'ngTableParams', '$sce','cmnfunctionService'];

    function osdBamTicketCompletedSummary($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, $route, ngTableParams, $sce,cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'osdBamTicketCompletedSummary';

        activate();

        function activate() {
            var url = 'api/OsdTrnBankAlert/GetBAMOperationCompletedSummary';
            lockUI();
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.BankAlertCompleted_list = resp.data.BankAlertAllocated_list;
            });
        }

        //$scope.BankalertView = function (bankalert2allocated_gid, customer_gid) {
        //    $location.url('app/osdBamAssign2operation?hash=' + cmnfunctionService.encryptURL('lsbankalert2allocated_gid=' + bankalert2allocated_gid + '&lscustomer_gid=' + customer_gid + '&lspage=BamCompletedList'));
        //}
        $scope.BankalertView = function (bankalert2allocated_gid, customer_gid) {
            $location.url('app/osdBamAssign2operation?lsbankalert2allocated_gid=' + bankalert2allocated_gid + '&lscustomer_gid=' + customer_gid + '&lspage=BamCompletedList');
        }

        $scope.back = function () {
            $state.go('app.OsdTrnBankAlertManagementSummary');
        }
    }
})();
