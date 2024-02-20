(function () {
    'use strict';

    angular
        .module('angle')
        .controller('editcollateralcontroller', editcollateralcontroller);

    editcollateralcontroller.$inject = ['$rootScope', '$scope', '$modal', '$state', 'SweetAlert', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', '$cookies', '$filter', 'ngTableParams', '$resource', '$timeout', 'ngTableDataService'];

    function editcollateralcontroller($rootScope, $scope, $modal, $state, SweetAlert, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, $cookies, $filter, ngTableParams, $resource, $timeout, ngTableDataService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'editcollateralcontroller';

        activate();

        function activate() {

            var url = 'api/customer/customer';
            SocketService.get(url).then(function (resp) {
                $scope.customer_list = resp.data.customer_list;
               
            });

            var url = 'api/security/getSecuritytype';
            SocketService.get(url).then(function (resp) {
                $scope.security_data = resp.data.securitytype_list;
            });
            var param = {
                collateral_gid: localStorage.getItem('collateral_gid')
            };
            var url = "api/collateral/getCollateralDetails";
            SocketService.getparams(url, param).then(function (resp) {
                $scope.customer = resp.data.customer_gid;
                $scope.security_type = resp.data.securitytype_gid;
                $scope.account_status = resp.data.account_status;
                $scope.security_description = resp.data.security_description;
                $scope.loan_data = resp.data.collateralloandetails_list;
              
            });

        }
        $scope.back=function()
        {
            $state.go('app.collateralsummary');
        }
        $scope.update=function(collateral_gid)
        {
           
            var customername = $('#customer :selected').text();
            var securitytype = $('#security_type :selected').text();

            var params = {
                customer_gid: $scope.customer,
                customer_name: customername,
                securitytype_gid: $scope.security_type,
                security_type: securitytype,
                account_status: $scope.account_status,
                security_description: $scope.security_description,
                collateral_gid: localStorage.getItem('collateral_gid')
            };
            var url = "api/collateral/UpdateCollateral";
            SocketService.post(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    console.log('test');
                    unlockUI()

                    Notify.alert('Collateral Updated Successfully..!!', 'success')

                    $state.go('app.collateralsummary');

                }

            });
        }
    }
})();
