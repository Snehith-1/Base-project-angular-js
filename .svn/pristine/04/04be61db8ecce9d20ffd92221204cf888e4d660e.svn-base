(function () {
    'use strict';
    angular
           .module('angle')
           .controller('Customerreportcontroller', Customerreportcontroller);

    Customerreportcontroller.$inject = ['$rootScope', '$scope', '$modal', '$state', 'SweetAlert', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', '$cookies', '$filter', 'ngTableParams', '$resource', '$timeout', 'ngTableDataService'];

    function Customerreportcontroller($rootScope, $scope, $modal, $state, SweetAlert, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, $cookies, $filter, ngTableParams, $resource, $timeout, ngTableDataService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'Customerreportcontroller';

        activate();

        function activate() {
            lockUI();
            //$scope.totalDisplayed = 50;
            var url = 'api/MstCustomerAdd/customerdetail';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                
                $scope.customer_data = resp.data.customer_list;
                // new code start   
                //if ($scope.customer_data == null) {
                //    $scope.total = 0;
                //    $scope.totalDisplayed = 0;
                //}
                //else {
                //    $scope.total = $scope.customer_data.length;
                //    if ($scope.customer_data.length < 100) {
                //        $scope.totalDisplayed = $scope.customer_data.length;
                //    }
                //}
                // new code end
                // $scope.total=$scope.customer_data.length;
            });
            var url = 'api/lglTrnDn2CustomerDetails/GetGuarantordetails';
            var param = {
                urn: $scope.urn
            };

            lockUI();
            SocketService.getparams(url, param).then(function (resp) {
                $scope.additional_list = resp.data.customer2userdtl_list;
            });
           
        }


       
        $scope.viewcustomerdtl = function (customer_gid, val) {
            $scope.urn = val;
            $scope.urn = localStorage.setItem('urn', val);
            localStorage.setItem('MyZonalAllocationHistory', 'N');
            localStorage.setItem('allocation_customer_gid', customer_gid);
            $scope.allocation_customer_gid = customer_gid;
            console.log(customer_gid);
            $state.go('app.customerReport360');
            //var URL = location.protocol + "//" + location.hostname + "/v1/#/app/customerReport360";
            //window.open(URL, '_blank');
        }
       
        //$scope.loadMore = function (pagecount) {
        //    if (pagecount == undefined) {
        //        Notify.alert("Enter the Total Summary Count", "warning");
        //        return;
        //    }
        //    lockUI();

        //    var Number = parseInt(pagecount);
        //    // new code start
        //    if ($scope.customer_data != null) {

        //        if (pagecount < $scope.customer_data.length) {
        //            $scope.totalDisplayed += Number;
        //            if ($scope.customer_data.length < $scope.totalDisplayed) {
        //                $scope.totalDisplayed = $scope.customer_data.length;
        //                Notify.alert(" Total Summary " + $scope.customer_data.length + " Records Only", "warning");
        //            }
        //            unlockUI();
        //        }
        //        else {
        //            unlockUI();
        //            Notify.alert(" Total Summary " + $scope.customer_data.length + " Records Only", "warning");
        //            return;
        //        }
        //    }
            
        //    unlockUI();
        //};
    }
})();
