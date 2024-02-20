(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AgrMstBuyerSupplierTypeController', AgrMstBuyerSupplierTypeController);

        AgrMstBuyerSupplierTypeController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function AgrMstBuyerSupplierTypeController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AgrMstBuyerSupplierTypeController';
        $scope.lsoneapipage = $location.search().lsoneapipage;
        var lsoneapipage = $scope.lsoneapipage;


        activate();

        function activate() {
            var url = 'api/AgrMstSamAgroMaster/GetBuyerSupplierType';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.buyersuppliertype_data = resp.data.BuyerSupplierType_List;
                unlockUI();
            });
        }

        $scope.addbuyersuppliertype = function () {
            var modalInstance = $modal.open({
                templateUrl: '/addbuyersuppliertype.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.submit = function () {

                    var params = {
                        buyersuppliertype_name: $scope.txtbuyersuppliertype,
                        lms_code: $scope.txtlms_code,
                        bureau_code: $scope.txtbureau_code
                    }
                    var url = 'api/AgrMstSamAgroMaster/CreatetBuyerSupplierType';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {

                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            $modalInstance.close('closed');
                            activate();
                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            $modalInstance.close('closed');
                            activate();
                        }
                    });
                }

            }
        }

        $scope.editbuyersuppliertype = function (buyersuppliertype_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/editbuyersuppliertype.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                var params = {
                    buyersuppliertype_gid: buyersuppliertype_gid
                }
                var url = 'api/AgrMstSamAgroMaster/EditBuyerSupplierType';
                SocketService.getparams(url, params).then(function (resp) {

                    $scope.txteditbuyersuppliertype = resp.data.buyersuppliertype_name;
                    $scope.txteditlms_code = resp.data.lms_code;
                    $scope.txteditbureau_code = resp.data.bureau_code;
                    $scope.buyersuppliertype_gid = resp.data.buyersuppliertype_gid;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.update = function () {
                    var url = 'api/AgrMstSamAgroMaster/UpdateBuyerSupplierType';
                    var params = {
                        buyersuppliertype_name: $scope.txteditbuyersuppliertype,
                        lms_code: $scope.txteditlms_code,
                        bureau_code: $scope.txteditbureau_code,
                        buyersuppliertype_gid: $scope.buyersuppliertype_gid
                    }
                    SocketService.post(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            $modalInstance.close('closed');
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            activate();

                        }
                        else {
                            $modalInstance.close('closed');
                            Notify.alert(resp.data.message, {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            activate();
                        }
                    });
                }
            }
        }

        $scope.Status_update = function (buyersuppliertype_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/statusbuyersuppliertype.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    buyersuppliertype_gid: buyersuppliertype_gid
                }
                var url = 'api/AgrMstSamAgroMaster/EditBuyerSupplierType';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.buyersuppliertype_gid = resp.data.buyersuppliertype_gid
                    $scope.txtbuyersuppliertype = resp.data.buyersuppliertype_name;
                    $scope.rbo_status = resp.data.Status;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.update_status = function () {

                    var params = {
                        buyersuppliertype_gid: $scope.buyersuppliertype_gid,
                        buyersuppliertype_name: $scope.txtbuyersuppliertype,
                        remarks: $scope.txtremarks,
                        rbo_status: $scope.rbo_status

                    }
                    var url = 'api/AgrMstSamAgroMaster/InactiveBuyerSupplierType';
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
                    buyersuppliertype_gid: buyersuppliertype_gid
                }

                var url = 'api/AgrMstSamAgroMaster/BuyerSupplierTypeInactiveLogview';
                lockUI();
                SocketService.getparams(url, param).then(function (resp) {
                    $scope.buyersuppliertypeinactivelog_data = resp.data.BuyerSupplierType_List;
                    unlockUI();
                });
            }
        }

        $scope.delete = function (buyersuppliertype_gid) {
            var params = {
                buyersuppliertype_gid: buyersuppliertype_gid
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
                    lockUI();
                    var url = 'api/AgrMstSamAgroMaster/DeleteBuyerSupplierType';
                    SocketService.getparams(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {
                            SweetAlert.swal('Deleted Successfully!');
                            activate();
                        }
                        else {
                            alert(resp.data.message, {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            activate();
                            unlockUI;
                        }
                    });
                }
            });
        }
    }
})();

