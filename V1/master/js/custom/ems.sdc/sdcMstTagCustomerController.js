(function () {
    'use strict';

    angular
        .module('angle')
        .controller('sdcMstTagCustomerController', sdcMstTagCustomerController);

    sdcMstTagCustomerController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams'];

    function sdcMstTagCustomerController($rootScope, $scope, $modal, $state, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'sdcMstTagCustomerController';

        activate();
        function activate() {

            //var url = 'api/SdcMstModule/GetCustomersList';
            //SocketService.get(url).then(function (resp) {
            //    $scope.customerlist = resp.data.customerlist;
            //});
             

            var params = {
                module_gid: localStorage.getItem('module_gid')
            };
            console.log(params);

            var url = 'api/SdcMstModule/GetCustomersList';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.customerlist = resp.data.customerlist;
            });


            var url = 'api/SdcMstModule/GetModuleView';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.module_gid = resp.data.module_gid,
                $scope.module_name = resp.data.module_name,
                $scope.module_code = resp.data.module_code;

            });
            //var url = 'api/ProductGroup/UnassignedVendor';
            //var param = {
            //    productgroup_gid: localStorage.getItem('productgroup_gid')
            //};
            //lockUI();
            //SocketService.getparams(url, param).then(function (resp) {
            //    unlockUI();
            //    $scope.UnassignedVendor_list = resp.data.UnassignedVendor_list;
            //});

            //var url = 'api/ProductGroup/EditProductGroup';
            //var param = {
            //    productgroup_gid: localStorage.getItem('productgroup_gid')
            //};

            //lockUI();
            //SocketService.getparams(url, param).then(function (resp) {
            //    $scope.productgroupdetails = resp.data;
            //    $scope.productgroup_code = resp.data.productgroup_code;
            //    $scope.productgroup_name = resp.data.productgroup_name;

            //    unlockUI();

            //});


        };



        $scope.checkall = function (selected) {
            //console.log(selected);
            angular.forEach($scope.customerlist, function (val) {
                val.checked = selected;
            });
        }

        $scope.back = function (val) {
            $state.go('app.sdcMstModuleSummary');
        }



        $scope.assign = function () {
            var assignList = [];

            angular.forEach($scope.customerlist, function (val) {

                if (val.checked == true) {
                    var customer_gid = val.customer_gid;
                    assignList.push(customer_gid);
                    console.log(assignList);
                }
            });
        
        


            var params = {
                customer_gid: assignList,
                module_gid: localStorage.getItem('module_gid')
                //productgroup_gid: $scope.productgroup_gid;
            }
            console.log(params);
            var url = 'api/SdcMstModule/PostCustomerAssign';
            SocketService.post(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    Notify.alert('Customer Assigned Successfully..!!', 'success');
                    $state.go('app.sdcMstModuleSummary');
                }
                else
                {
                    Notify.alert('Select Atleast One..!!', 'warning')
                }

            });

        }

    }
})();