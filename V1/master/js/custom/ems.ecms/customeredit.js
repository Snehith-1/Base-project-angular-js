(function () {
    'use strict';

    angular
        .module('angle')
        .controller('customeredit', customeredit);

    customeredit.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route'];

    function customeredit($rootScope, $scope, $state, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'customeredit';

        activate();

        function activate() {

            var url = 'api/employee/employee';
            SocketService.get(url).then(function (resp) {
                $scope.employee_list = resp.data.employee_list;
            });

            var url = 'api/segment/segment';
            SocketService.get(url).then(function (resp) {
                $scope.segment_list = resp.data.segment_list;
            });

            var url = 'api/customer/state';
            SocketService.get(url).then(function (resp) {
                $scope.state_list = resp.data.state_list;
            });

            $scope.customer_gid = localStorage.getItem('customer_gid');
            var url = 'api/customer/Getcustomerupdatedetails';
            var params = {
                customer_gid: $scope.customer_gid
            };
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                $scope.customerCodeedit = resp.data.customerCodeedit;
                $scope.customerNameedit = resp.data.customerNameedit;
                $scope.contactpersonedit = resp.data.contactPersonedit;
                $scope.contactnumberedit = resp.data.contactnoedit;
                $scope.emailedit = resp.data.emailedit;
                $scope.mobileNoedit = resp.data.mobileNoedit;
                $scope.txtaddress1 = resp.data.addressline1edit;
                $scope.txtaddress2 = resp.data.addressline2edit;
                $scope.segment = resp.data.segment_gid;
                $scope.segment_name = resp.data.segment_name;
                $scope.statename = resp.data.state;
                $scope.state_gid = resp.data.state_gid;
                $scope.employee_gid = resp.data.employee_gid;
                $scope.employee_name = resp.data.employee_name;
                console.log(resp.data.employee_name);
            });
            unlockUI();
        }


        $scope.customereditback = function () {
            $state.go('app.customerMaster');
        }
    }
})();
