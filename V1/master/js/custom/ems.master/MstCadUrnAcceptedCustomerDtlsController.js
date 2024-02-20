(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstCadUrnAcceptedCustomerDtlsController', MstCadUrnAcceptedCustomerDtlsController);

    MstCadUrnAcceptedCustomerDtlsController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function MstCadUrnAcceptedCustomerDtlsController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstCadUrnAcceptedCustomerDtlsController';
        $scope.application_gid = $location.search().application_gid;
        var application_gid = $scope.application_gid;
        $scope.lspage = $location.search().lspage;
        var lspage = $scope.lspage;
        $scope.customer_urn = $location.search().customer_urn;
        var customer_urn = $scope.customer_urn;

        activate();
       
        function activate() {
            var params = {
                customer_urn: customer_urn
            }

            var url = 'api/MstCAD/GetCADUrnGroupingDtlsSummary';
            lockUI();
            SocketService.getparams(url,params).then(function (resp) {
                unlockUI();
                $scope.urngrouping_list = resp.data.cadapplicationlist;
            });
        }

        $scope.view = function (val,val1) {
            $location.url('app/MstCadApplicationView?application_gid=' + val + '&customer_urn=' + val1 + '&lspage=CADUrnAcceptanceCustomerDtl');
        }

        $scope.Back = function () {
            if(lspage == 'CADUrnAcceptanceCustomer'){
                $location.url('app/MstCadUrnAcceptedCustomers');
            }
            else if (lspage == 'CADAcceptanceCustomers') {
                $location.url('app/MstCadAcceptedCustomers');
            }
            else {

            }
        }

    }
})();
