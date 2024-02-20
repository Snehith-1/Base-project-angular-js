(function () {
    'use strict';

    angular
        .module('angle')
        .controller('SysMstClientRoleController', SysMstClientRoleController);

    SysMstClientRoleController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function SysMstClientRoleController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'SysMstClientRoleController';
        $scope.lsoneapipage = $location.search().lsoneapipage;
        var lsoneapipage = $scope.lsoneapipage;

        activate();

        function activate() {
            var url = 'api/SystemMaster/GetSubFunction';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.subfunction_list = resp.data.master_list;
                unlockUI();
            });
        }

        $scope.addclientrole = function () {
            var modalInstance = $modal.open({
                templateUrl: '/addclientrole.html',
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
                        subfunction_name: $scope.txtsubfunction,
                        lms_code: $scope.txtlms_code,
                        bureau_code: $scope.txtbureau_code

                    }
                    var url = 'api/SystemMaster/CreateSubFunction';
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
        $scope.editclientrole = function (subfunction_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/editclientrole.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    subfunction_gid: subfunction_gid
                }
                var url = 'api/SystemMaster/EditSubFunction';
                SocketService.getparams(url, params).then(function (resp) {

                    $scope.txteditsubfunction = resp.data.subfunction_name;
                    $scope.txteditlms_code = resp.data.lms_code;
                    $scope.txteditbureau_code = resp.data.bureau_code;
                    $scope.subfunction_gid = resp.data.subfunction_gid;
                });


                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.update = function () {

                    var url = 'api/SystemMaster/UpdateSubFunction';
                    var params = {
                        subfunction_gid: subfunction_gid,
                        subfunction_name: $scope.txteditsubfunction,
                        bureau_code: $scope.txteditbureau_code,
                        lms_code:$scope.txteditlms_code
                       
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

        $scope.Status_update = function (subfunction_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/statusclientrole.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                var params = {
                    subfunction_gid: subfunction_gid
                }
                var url = 'api/SystemMaster/EditSubFunction';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.subfunction_gid = resp.data.subfunction_gid;
                    $scope.txteditsubfunction = resp.data.subfunction_name;
                    $scope.rbo_status = resp.data.Status;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.update_status = function () {

                    var params = {
                        subfunction_gid: subfunction_gid,
                        remarks: $scope.txtremarks,
                        rbo_status: $scope.rbo_status,
                        subfunction_name: $scope.txteditsubfunction
                    }
                    var url = 'api/SystemMaster/InactiveSubFunction';
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
                    subfunction_gid: subfunction_gid
                }

                var url = 'api/SystemMaster/SubFunctionInactiveLogview';
                lockUI();
                SocketService.getparams(url, param).then(function (resp) {
                    $scope.subfunctioninactivelog_list = resp.data.master_list;
                    unlockUI();
                });

            }
        }

        $scope.delete = function (subfunction_gid) {
            var params = {
                subfunction_gid: subfunction_gid
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
                    var url = 'api/SystemMaster/DeleteSubFunction';
                    SocketService.getparams(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            activate();
                        }
                        else {
                            Notify.alert('Error Occurred While Deleting Client Role!', {
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

