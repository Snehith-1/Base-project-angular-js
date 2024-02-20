(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AgrMstTypeofWarehouseController', AgrMstTypeofWarehouseController);

        AgrMstTypeofWarehouseController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function AgrMstTypeofWarehouseController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AgrMstTypeofWarehouseController';
        $scope.lsoneapipage = $location.search().lsoneapipage;
        var lsoneapipage = $scope.lsoneapipage;


        activate();

        function activate() { 
            
            var url = 'api/AgrMstSamAgroMaster/Gettypeofwarehouse';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.typeofwarehouse_data = resp.data.applicationmst_list;
                unlockUI();
            });
        }

        $scope.addTypeofWarehouse = function () {
            var modalInstance = $modal.open({
                templateUrl: '/addTypeofWarehouse.html',
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
                        typeofwarehouse_name: $scope.txttypeofwarehouse_name,
                        lms_code: $scope.txtlms_code,
                        bureau_code: $scope.txtbureau_code

                    }
                    var url = 'api/AgrMstSamAgroMaster/Createtypeofwarehouse';
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
                                status: 'info',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            $modalInstance.close('closed');
                            activate();
                        }
                    });

                    // $modalInstance.close('closed');

                }
                
            }
        }

        $scope.editTypeofWarehouse = function (typeofwarehouse_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/editTypeofWarehouse.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    typeofwarehouse_gid: typeofwarehouse_gid
                }
                var url = 'api/AgrMstSamAgroMaster/Edittypeofwarehouse';
                SocketService.getparams(url, params).then(function (resp) {

                    $scope.txtedittypeofwarehouse_name = resp.data.typeofwarehouse_name;
                    $scope.txteditlms_code = resp.data.lms_code;
                    $scope.txteditbureau_code = resp.data.bureau_code;
                    $scope.typeofwarehouse_gid = resp.data.typeofwarehouse_gid;
                });
                
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.update = function () {

                    var url = 'api/AgrMstSamAgroMaster/Updatetypeofwarehouse';
                    var params = {
                        typeofwarehouse_name: $scope.txtedittypeofwarehouse_name,
                        lms_code: $scope.txteditlms_code,
                        bureau_code: $scope.txteditbureau_code,
                        typeofwarehouse_gid: $scope.typeofwarehouse_gid
                    }
                    SocketService.post(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            activate();

                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                        }
                    });$modalInstance.close('closed');

                }
            }
        }

        $scope.Status_update = function (typeofwarehouse_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/statusTypeofWarehouse.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                var params = {
                    typeofwarehouse_gid: typeofwarehouse_gid
                }
                var url = 'api/AgrMstSamAgroMaster/Edittypeofwarehouse';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.typeofwarehouse_gid = resp.data.typeofwarehouse_gid
                    $scope.typeofwarehouse_name = resp.data.typeofwarehouse_name;
                    $scope.rbo_status = resp.data.Status;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.update_status = function () {

                    var params = {
                        typeofwarehouse_name :$scope.typeofwarehouse_name,
                        typeofwarehouse_gid: typeofwarehouse_gid,
                        remarks: $scope.txtremarks,
                        rbo_status: $scope.rbo_status

                    }
                    var url = 'api/AgrMstSamAgroMaster/Inactivetypeofwarehouse';
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
                                status: 'info',
                                pos: 'top-center',
                                timeout: 3000
                            });
                        }
                        activate();
                    });

                    $modalInstance.close('closed');

                }

                var param = {
                    typeofwarehouse_gid: typeofwarehouse_gid
                }

                var url = 'api/AgrMstSamAgroMaster/InactivetypeofwarehouseHistory';
                lockUI();
                SocketService.getparams(url, param).then(function (resp) {
                    $scope.typeofwarehouseinactivelog_data = resp.data.inactivehistory_list;
                    unlockUI();
                });

            }
        }

        $scope.delete = function (typeofwarehouse_gid) {
            var params = {
                typeofwarehouse_gid: typeofwarehouse_gid
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
                    var url = 'api/AgrMstSamAgroMaster/DeleteTypeofWarehouse';
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

