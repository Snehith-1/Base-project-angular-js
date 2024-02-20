(function () {
    'use strict';

    angular
        .module('angle')
        .controller('FndMstCustomerMasterController', FndMstCustomerMasterController);

    FndMstCustomerMasterController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams','cmnfunctionService'];

    function FndMstCustomerMasterController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams,cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'FndMstCustomerMasterController';

        activate();

        function activate() {
            var url = 'api/FndMstCustomerMasterAdd/GetcustomerSummary';
            SocketService.get(url).then(function (resp) {
                $scope.customer_list = resp.data.customer_list;
            });

            var url = 'api/FndMstCustomerMasterAdd/GetCustomerViewCounts';
            SocketService.get(url).then(function (resp) {
                unlockUI()
                $scope.customerpendingView_count = resp.data.customerpendingview_count;
                $scope.customerapprovedView_count = resp.data.customerapprovedview_count;
                $scope.customerrejectedView_count = resp.data.customerrejectedview_count;

            });
          
            //var url = 'api/FndTrnCampaign/GetCampaignCounts';
            //SocketService.get(url).then(function (resp) {
            //    unlockUI()
            //    $scope.campaignpending_count = resp.data.campaignpending_count;
            //    $scope.rejected_count = resp.data.rejected_count;
            //    $scope.approved_count = resp.data.approved_count;
            //    $scope.closed_count = resp.data.closed_count;

            //});
        }
        $scope.approvecustomerview = function () {
            $state.go('app.FndTrnCustomerApprovedView');
        }
        $scope.rejectcustomerview = function () {
            $state.go('app.FndTrnCustomerRejectedView');
        }
        $scope.addcustomer = function () {
            $state.go('app.FndMstCustomerMasterAdd');
        }

        $scope.editcustomer = function (val) {
            //localStorage.setItem('customer_gid', val);
            //$state.go('app.FndMstCustomerMasterEdit');
            $location.url('app/FndMstCustomerMasterEdit?hash=' + cmnfunctionService.encryptURL('lscustomer_gid=' + val));
        }

        $scope.viewcustomer = function (val) {
            //localStorage.setItem('customer_gid', val);
            //$state.go('app.FndMstCustomerMasterView');
            $location.url('app/FndMstCustomerMasterView?hash=' + cmnfunctionService.encryptURL('lscustomer_gid='+val));
        }
        $scope.Status_update = function (customer_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/statuscustomer.html',
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
                    $scope.rbo_status = resp.data.Status;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.update_status = function () {

                    var params = {

                        customer_gid: customer_gid,
                        remarks: $scope.txtremarks,
                        rbo_status: $scope.rbo_status

                    }
                    var url = 'api/FndMstCustomerMasterAdd/Inactivecustomer';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {

                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });

                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                        }
                        activate();
                    });

                    $modalInstance.close('closed');

                }
                var param = {
                    customer_gid: customer_gid
                }

                var url = 'api/FndMstCustomerMasterAdd/customerInactiveLogview';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.customerstatus_list = resp.data.customerstatus_list;
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

        $scope.delete = function (customer_gid) {
            var params = {
                customer_gid: customer_gid
            }
            SweetAlert.swal({
                title: 'Are you sure?',
                text: 'Do You Want To Delete the Record ?',
                showCancelButton: true,
                confirmButtonColor: '#DD6B55',
                confirmButtonText: 'Yes, delete it!',
                closeOnConfirm: false
            }, function (isConfirm) {
                if (isConfirm) {
                    var url = 'api/FndMstCustomerMasterAdd/Deletecustomer';
                    SocketService.getparams(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            activate();
                        }
                        else {
                            Notify.alert('Error Occurred While Deleting Customer!', {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            activate();
                        }
                    });
                    SweetAlert.swal('Deleted Successfully!');
                }

            });
        };


    }
})();

