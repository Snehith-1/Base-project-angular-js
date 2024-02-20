(function () {
    'use strict';

    angular
        .module('angle')
        .controller('customerAlertcontroller', customerAlertcontroller);

    customerAlertcontroller.$inject = ['$rootScope', '$scope', '$modal', '$state', 'SweetAlert', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', '$cookies', '$filter', 'ngTableParams', '$resource', '$timeout', 'ngTableDataService'];

    function customerAlertcontroller($rootScope, $scope, $modal, $state, SweetAlert, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, $cookies, $filter, ngTableParams, $resource, $timeout, ngTableDataService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'customerAlertcontroller';

        activate();


        function activate() {

            var url = 'api/customer/CustomerAlert';
            lockUI();
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.customerlist = resp.data.customer_list;
                //console.log($scope.customerlist);
                $scope.customer_list = resp.data.customer_list;
                var length = $scope.customer_list.length;
                $scope.count_customer = length;
            });
        }

        $scope.deferraldetails = function (customer_gid, id) {
            var params = {
                customer_gid: customer_gid
            };
            var url = 'api/customer/deferraldetails';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.customer_list[id][customer_gid] = resp.data.customerdeferral_list;
            });
        }
        $scope.rdGenerate = function (val) {
            $scope.customer_gid = val;
            $scope.customer_gid = localStorage.setItem('customer_gid', val);
            $state.go('app.customerAlertGenerate');

        }

        $scope.mailHistory = function (val) {
            $scope.customer_gid = val;
            $scope.customer_gid = localStorage.setItem('customer_gid', val);
            $scope.pageNavigation = localStorage.setItem('mailManagement', 'CustomerAlert');
            $state.go('app.customerAlertHistory');

        }
        $scope.onselectedchangecustomer = function ()
        {
            var url = 'api/customer/CustomerAlertSearch';
            var params = {
                customer_gid: $scope.customer.customer_gid
            };
            //console.log(params);
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
             
                if (resp.data.customer_list != null)
                {
                    $scope.customer_list = resp.data.customer_list;
                }
                else
                {
                    $scope.customer = "";
                    alert("No Record Found");
                    activate();
                   
                }
               
            });
        }
        $scope.refresh=function()
        {
            $scope.customer = "";
            activate();
        }

    }
})();