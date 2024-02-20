(function () {
    'use strict';

    angular
        .module('angle')
        .controller('FndTrnCustomerRejectedViewController', FndTrnCustomerRejectedViewController);

    FndTrnCustomerRejectedViewController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams','cmnfunctionService'];

    function FndTrnCustomerRejectedViewController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams,cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'FndTrnCustomerRejectedViewController';

        activate();

        function activate() {

            var url = 'api/FndMstCustomerMasterAdd/Getcustomerreject';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.customerrejected_list = resp.data.customerrejected_list;
                unlockUI();

            });
            var url = 'api/FndMstCustomerMasterAdd/GetCustomerViewCounts';
            SocketService.get(url).then(function (resp) {
                unlockUI()
                $scope.customerpendingView_count = resp.data.customerpendingview_count;
                $scope.customerapprovedView_count = resp.data.customerapprovedview_count;
                $scope.customerrejectedView_count = resp.data.customerrejectedview_count;

            });


            $scope.approvecustomerview = function () {
                $state.go('app.FndTrnCustomerApprovedView');
            }
            $scope.rejectcustomerview = function () {
                $state.go('app.FndTrnCustomerRejectedView');
            }
            $scope.addcustomer = function () {
                $state.go('app.FndMstCustomerMasterAdd');
            }
            $scope.pendingcustomerview = function () {
                $state.go('app.FndMstCustomerMaster');
            }
            $scope.viewcustomer = function (val) {
               
                $location.url('app/FndMstCustomerMasterView?hash=' + cmnfunctionService.encryptURL('lscustomer_gid=' + val));
            }
            $scope.rejectcustomerview = function () {

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

