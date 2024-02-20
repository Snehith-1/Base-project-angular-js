// JavaScript source code
// JavaScript source code
(function () {
    'use strict';

    angular
        .module('angle')
        .controller('StartupRequireController', StartupRequireController);

    StartupRequireController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams'];

    function StartupRequireController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'StartupRequireController';
        activate();


        function activate() {

            var url = 'api/MstStartupRequire/GetStartupRequire';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.startuprequire_list = resp.data.startuprequire_list;
                unlockUI();
            });
        }

        $scope.popupstartuprequire = function () {
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

                $scope.startuprequireSubmit = function () {
                    var params = {
                        startuprequire_name: $scope.txtstartuprequire,
                        lms_code: $scope.txtlms_code,
                        bureau_code: $scope.txtbureau_code
                    }

                    var url = 'api/MstStartupRequire/CreateStartupRequire';
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

        $scope.editstartuprequire = function (startuprequire_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/editstartuprequire.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                var params = {
                    startuprequire_gid: startuprequire_gid
                }
                var url = 'api/MstStartupRequire/EditStartupRequire';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.txteditlms_code = resp.data.lms_code;
                    $scope.txteditbureau_code = resp.data.bureau_code;
                    $scope.txtstartuprequire = resp.data.startuprequire_name;
                    $scope.startuprequire_gid = resp.data.startuprequire_gid;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.startuprequireUpdate = function () {

                    var url = 'api/MstStartupRequire/UpdateStartupRequire';
                    var params = {
                        lms_code: $scope.txteditlms_code,
                        bureau_code: $scope.txteditbureau_code,
                        startuprequire_name: $scope.txtstartuprequire,
                        startuprequire_gid: $scope.startuprequire_gid
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

        $scope.Status_update = function (startuprequire_gid) {
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
                    startuprequire_gid: startuprequire_gid
                }
                var url = 'api/MstStartupRequire/EditStartupRequire';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.startuprequire_gid = resp.data.startuprequire_gid
                    $scope.txtstartuprequire = resp.data.startuprequire_name;
                    $scope.rbo_status = resp.data.Status;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.update_status = function () {

                    var params = {
                        startuprequire_name: $scope.txtstartuprequire,
                        startuprequire_gid: $scope.startuprequire_gid,
                        remarks: $scope.txtremarks,
                        rbo_status: $scope.rbo_status

                    }
                    var url = 'api/MstStartupRequire/InactiveStartupRequire';
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
                    startuprequire_gid: startuprequire_gid
                }

                var url = 'api/MstStartupRequire/StartupRequireInactiveLogview';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.startuprequireinactivelog_data = resp.data.startuprequire_list;
                    unlockUI();
                });
            }
        }


        $scope.deletestartuprequire = function (startuprequire_gid) {
            var params = {
                startuprequire_gid: startuprequire_gid
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
                    var url = 'api/MstStartupRequire/DeleteStartupRequire';
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