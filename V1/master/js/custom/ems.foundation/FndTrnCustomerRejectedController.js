(function () {
    'use strict';

    angular
        .module('angle')
        .controller('FndTrnCustomerRejectedController', FndTrnCustomerRejectedController);

    FndTrnCustomerRejectedController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams','cmnfunctionService'];

    function FndTrnCustomerRejectedController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams,cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'FndTrnCustomerRejectedController';

        activate();

        function activate() {

            var url = 'api/FndMstCustomerMasterAdd/Getcustomerapprovalreject';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.customerrejected_list = resp.data.customerrejected_list;
                unlockUI();

            });


            var url = 'api/FndMstCustomerMasterAdd/GetCustomerCounts';
            SocketService.get(url).then(function (resp) {
                unlockUI()
                $scope.customerpending_count = resp.data.customerpending_count;
                $scope.customerapproved_count = resp.data.customerapproved_count;
                $scope.customerrejected_count = resp.data.customerrejected_count;

            });

            $scope.customersummary = function () {
                $state.go('app.FndTrnCustomerApproval');
            }
            $scope.approvecustomer = function () {
                $state.go('app.FndTrnCustomerApproved');
            }
            $scope.rejectcustomer = function () {
                $state.go('app.FndTrnCustomerRejected');
            }

            $scope.viewcustomer = function (val) {

                $location.url('app/FndTrnApprovalView?hash=' + cmnfunctionService.encryptURL('lscustomer_gid=' + val));
            }


            $scope.rejectcustomer = function () {

                var url = 'api/FndMstCustomerMasterAdd/Getcustomerreject';
                lockUI();
                SocketService.get(url).then(function (resp) {
                    $scope.customerrejected_list = resp.data.customerrejected_list;
                    unlockUI();

                });
            }




        }
        $scope.showsPopover = function (customer_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/showremarks.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                var params = {
                    customer_gid: customer_gid
                }
                var url = 'api/FndMstCustomerMasterAdd/Editcustomer';
                SocketService.getparams(url, params).then(function (resp) {

                    $scope.txtcustomer_code = resp.data.customer_code;
                    $scope.txtcustomer_name = resp.data.customer_name;
                    $scope.txteditremarks = resp.data.remarks;
                    $scope.rbo_status = resp.data.Status;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };


            }
        }
        //$scope.addcustomer = function () {
        //    $state.go('app.FndMstCustomerMasterAdd');
        //}




    }
})();

