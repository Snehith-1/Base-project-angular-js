(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstRMCustomerDashboardController', MstRMCustomerDashboardController);

    MstRMCustomerDashboardController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams'];

    function MstRMCustomerDashboardController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstRMCustomerDashboardController';
        var application_gid = $location.search().application_gid;
        $scope.customer_urn = $location.search().customer_urn;
        var customer_urn = $scope.customer_urn;       
        var lspage = $location.search().lspage;

        activate();

        function activate() { }

        $scope.Back = function () {
            if (lspage == 'MstRMCustomerSummary') {
                $location.url('app/MstRMCustomerSummary?application_gid=' + application_gid + '&lspage=RMMyCustomerList');
            }
            else if (lspage == 'RMCADUrnGrouping') {
                $location.url('app/MstRMCadUrnAcceptedCustomerDtls?application_gid=' + application_gid + '&customer_urn=' + customer_urn + '&lspage=RMCADUrnGrouping');
            }
            else {
                $location.url('app/MstRMCustomerSummary?application_gid=' + application_gid + '&lspage=RMMyCustomerList');
            }

        }

        $scope.GotoLoanDetails = function () {
            $location.url('app/MstRMLoanDetailsDtls?customer_urn=' + customer_urn + '&lspage=' + lspage);
        }
        
        $scope.GotoDisbursementRequest = function () {
            $location.url('app/MstRMDisbursementRequest?customer_urn=' + customer_urn);
        }

    }
})();
