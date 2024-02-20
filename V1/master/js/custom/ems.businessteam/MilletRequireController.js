// JavaScript source code
(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MilletRequireController', MilletRequireController);

        MilletRequireController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams'];

    function MilletRequireController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MilletRequireController';
        activate();


        function activate() {

            var url = 'api/MarMstMilletRequire/GetMilletRequire';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.milletrequire_list = resp.data.milletrequire_list;
                unlockUI();
            });
        }

        $scope.popupmilletrequire = function () {
            var modalInstance = $modal.open({
                templateUrl: '/myModalContent.html',
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

                $scope.milletrequireSubmit = function () {
                    var params = {
                        milletrequire_name: $scope.txtmilletrequire,
                        lms_code: $scope.txtlms_code,
                        bureau_code: $scope.txtbureau_code
                    }

                    var url = 'api/MarMstMilletRequire/CreateMilletRequire';
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
                            $modalInstance.close('closed');
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

            }
        }

        $scope.editmilletrequire = function (milletrequire_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/editmilletrequire.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                var params = {
                    milletrequire_gid: milletrequire_gid
                }
                var url = 'api/MarMstMilletRequire/EditMilletRequire';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.txteditlms_code = resp.data.lms_code;
                    $scope.txteditbureau_code = resp.data.bureau_code;
                    $scope.txtmilletrequire = resp.data.milletrequire_name;
                    $scope.milletrequire_gid = resp.data.milletrequire_gid;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.milletrequireUpdate = function () {

                    var url = 'api/MarMstMilletRequire/UpdateMilletRequire';
                    var params = {
                        lms_code: $scope.txteditlms_code,
                        bureau_code: $scope.txteditbureau_code,
                        milletrequire_name: $scope.txtmilletrequire,
                        milletrequire_gid: $scope.milletrequire_gid
                    }
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

            }
        }

        $scope.Status_update = function (milletrequire_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/statusaudittype.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                var params = {
                    milletrequire_gid: milletrequire_gid
                }
                var url = 'api/MarMstMilletRequire/EditMilletRequire';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.milletrequire_gid = resp.data.milletrequire_gid
                    $scope.txtmilletrequire = resp.data.milletrequire_name;
                    $scope.rbo_status = resp.data.Status;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.update_status = function () {

                    var params = {
                        milletrequire_name: $scope.txtmilletrequire,
                        milletrequire_gid: $scope.milletrequire_gid,
                        remarks: $scope.txtremarks,
                        rbo_status: $scope.rbo_status

                    }
                    var url = 'api/MarMstMilletRequire/InactiveMilletRequire';
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
                    milletrequire_gid: milletrequire_gid
                }

                var url = 'api/MarMstMilletRequire/MilletRequireInactiveLogview';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.milletrequireinactivelog_data = resp.data.milletrequire_list;
                    unlockUI();
                });
            }
        }

     
        $scope.deletemilletrequire = function (milletrequire_gid) {
            var params = {
                milletrequire_gid: milletrequire_gid
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
                    var url = 'api/MarMstMilletRequire/DeleteMilletRequire';
                    SocketService.getparams(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            SweetAlert.swal('Deleted Successfully!');

                            activate();
                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            activate();
                        }
                    });
                }

            });
        };
    }

})();