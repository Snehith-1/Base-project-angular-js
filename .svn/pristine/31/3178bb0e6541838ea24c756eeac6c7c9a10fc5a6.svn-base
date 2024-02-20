(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstCallTypeController', MstCallTypeController);

    MstCallTypeController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function MstCallTypeController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstCallTypeController';
        $scope.lsoneapipage = $location.search().lsoneapipage;
        var lsoneapipage = $scope.lsoneapipage;

        activate();

        function activate() {          
            var url = 'api/MstApplication360/GetCallType';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.calltype_list = resp.data.application_list;
                unlockUI();
            });
        }
        $scope.addcalltype = function () {
            var modalInstance = $modal.open({
                templateUrl: '/addcalltype.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                               
                $scope.back = function () {
                    $modalInstance.close('closed');
                };
                $scope.submit = function () {
                    var params = {
                        calltype_name: $scope.txtcalltype_name,
                        lms_code: $scope.txtlms_code,
                        bureau_code: $scope.txtbureau_code
                    }
                    var url = 'api/MstApplication360/CreateCallType'; 
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
        $scope.editcalltype = function (calltype_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/editcalltype.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.back = function () {
                    $modalInstance.close('closed');
                };
                var params = {
                    calltype_gid: calltype_gid
                }
                var url = 'api/MstApplication360/EditCallType';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.txteditcalltype_name = resp.data.calltype_name;
                    $scope.txteditlms_code = resp.data.lms_code;
                    $scope.txteditbureau_code = resp.data.bureau_code;
                    $scope.calltype_gid = resp.data.calltype_gid;
                });
                $scope.back = function () {
                    $modalInstance.close('closed');
                };
                $scope.update = function () {
                    var url = 'api/MstApplication360/UpdateCallType';
                    var params = {
                        calltype_name: $scope.txteditcalltype_name,
                        lms_code: $scope.txteditlms_code,
                        bureau_code: $scope.txteditbureau_code,
                        calltype_gid: $scope.calltype_gid
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
        $scope.Status_update = function (calltype_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/statuscalltype.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    calltype_gid: calltype_gid
                }
                var url = 'api/MstApplication360/EditCallType';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.calltype_gid = resp.data.calltype_gid
                    $scope.txtcalltype_name = resp.data.calltype_name;
                    $scope.rbo_status = resp.data.Status;
                });
                $scope.back = function () {
                    $modalInstance.close('closed');
                };
                $scope.update_status = function () {
                    var params = {
                        calltype_gid: calltype_gid,
                        remarks: $scope.txtremarks,
                        rbo_status: $scope.rbo_status
                    }
                    var url = 'api/MstApplication360/InactiveCallType';
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
                        } activate();
                    });
                    $modalInstance.close('closed');
                }
                var params = {
                    calltype_gid: calltype_gid
                }
                var url = 'api/MstApplication360/InactiveCallTypeHistory';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.calltypeinactivelog_list = resp.data.inactivehistory_list;
                    unlockUI();
                });
            }
        }
        $scope.delete = function (calltype_gid) {
            var params = {
                calltype_gid: calltype_gid
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
                    var url = 'api/MstApplication360/DeleteCallType';
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
        }
    }
})();