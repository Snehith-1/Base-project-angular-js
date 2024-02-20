(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AgrMstSupplierSummaryController', AgrMstSupplierSummaryController);

    AgrMstSupplierSummaryController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function AgrMstSupplierSummaryController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AgrMstSupplierSummaryController';

        activate();

        function activate() {
            var url = 'api/AgrMstApplication360/GetSupplier';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.supplier_list = resp.data.application_list;
                unlockUI();
            });
        }


        $scope.addsupplier = function () {
            var modalInstance = $modal.open({
                templateUrl: '/addsupplier.html',
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
                        supplier_name: $scope.txtsupplier_name,
                        supplier_ref_no: $scope.txtsupplierRef_no,

                    }
                    var url = 'api/AgrMstApplication360/CreateSupplier';
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


                        }
                    });

                    $modalInstance.close('closed');

                }

            }
        }

        $scope.editsupplier = function (supplier_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/editsupplier.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    supplier_gid: supplier_gid
                }
                var url = 'api/AgrMstApplication360/EditSupplier';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.txteditsupplier_name = resp.data.supplier_name;
                    $scope.txteditsupplierRef_no = resp.data.supplier_ref_no;
                    $scope.supplier_gid = resp.data.supplier_gid;
                });


                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.update = function () {

                    var url = 'api/AgrMstApplication360/UpdateSupplier';
                    var params = {
                        supplier_name: $scope.txteditsupplier_name,
                        supplier_ref_no: $scope.txteditsupplierRef_no,
                        supplier_gid: $scope.supplier_gid
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

                        }
                    });
                    $modalInstance.close('closed');
                }
            }
        }

        $scope.Status_update = function (supplier_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/statussupplier.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                var params = {
                    supplier_gid: supplier_gid
                }
                var url = 'api/AgrMstApplication360/EditSupplier';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.supplier_gid = resp.data.supplier_gid
                    $scope.txtsupplier_name = resp.data.supplier_name;
                    $scope.rbo_status = resp.data.Status;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.update_status = function () {

                    var params = {
                        supplier_gid: supplier_gid,
                        remarks: $scope.txtremarks,
                        rbo_status: $scope.rbo_status

                    }
                    var url = 'api/AgrMstApplication360/InactiveSupplier';
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
                    supplier_gid: supplier_gid
                }

                var url = 'api/AgrMstApplication360/SuppierInactiveLogview';
                lockUI();
                SocketService.getparams(url, param).then(function (resp) {
                    $scope.Supplierinactivelog_list = resp.data.application_list;
                    unlockUI();
                });

            }
        }

        $scope.delete = function (supplier_gid) {
            var params = {
                supplier_gid: supplier_gid
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
                    var url = 'api/AgrMstApplication360/DeleteSupplier';
                    SocketService.getparams(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            activate();
                        }
                        else {
                            Notify.alert('Error Occurred While Deleting Supplier!', {
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

