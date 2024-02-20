(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstCallReceivedNumberController', MstCallReceivedNumberController);

    MstCallReceivedNumberController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function MstCallReceivedNumberController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstCallReceivedNumberController';
        $scope.lsoneapipage = $location.search().lsoneapipage;
        var lsoneapipage = $scope.lsoneapipage;

        activate();

        function activate() {          
            var url = 'api/MstApplication360/GetCallReceivedNumber';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.callreceivednumber_list = resp.data.application_list;
                unlockUI();
            });
        }
        $scope.addcallreceivednumber = function () {
            var modalInstance = $modal.open({
                templateUrl: '/addcallreceivednumber.html',
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
                        callreceivednumber_name: $scope.txtcallreceivednumber_name,
                        lms_code: $scope.txtlms_code,
                        bureau_code: $scope.txtbureau_code
                    }
                    var url = 'api/MstApplication360/CreateCallReceivedNumber'; 
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
        $scope.editcallreceivednumber = function (callreceivednumber_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/editcallreceivednumber.html',
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
                    callreceivednumber_gid: callreceivednumber_gid
                }
                var url = 'api/MstApplication360/EditCallReceivedNumber';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.txteditcallreceivednumber_name = resp.data.callreceivednumber_name;
                    $scope.txteditlms_code = resp.data.lms_code;
                    $scope.txteditbureau_code = resp.data.bureau_code;
                    $scope.callreceivednumber_gid = resp.data.callreceivednumber_gid;
                });
                $scope.back = function () {
                    $modalInstance.close('closed');
                };
                $scope.update = function () {
                    var url = 'api/MstApplication360/UpdateCallReceivedNumber';
                    var params = {
                        callreceivednumber_name: $scope.txteditcallreceivednumber_name,
                        lms_code: $scope.txteditlms_code,
                        bureau_code: $scope.txteditbureau_code,
                        callreceivednumber_gid: $scope.callreceivednumber_gid
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
        $scope.Status_update = function (callreceivednumber_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/statuscallreceivednumber.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    callreceivednumber_gid: callreceivednumber_gid
                }
                var url = 'api/MstApplication360/EditCallReceivedNumber';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.callreceivednumber_gid = resp.data.callreceivednumber_gid
                    $scope.txtcallreceivednumber_name = resp.data.callreceivednumber_name;
                    $scope.rbo_status = resp.data.Status;
                });
                $scope.back = function () {
                    $modalInstance.close('closed');
                };
                $scope.update_status = function () {
                    var params = {
                        callreceivednumber_gid: callreceivednumber_gid,
                        remarks: $scope.txtremarks,
                        rbo_status: $scope.rbo_status
                    }
                    var url = 'api/MstApplication360/InactiveCallReceivedNumber';
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
                    callreceivednumber_gid: callreceivednumber_gid
                }
                var url = 'api/MstApplication360/InactiveCallReceivedNumberHistory';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.callreceivednumberinactivelog_list = resp.data.inactivehistory_list;
                    unlockUI();
                });
            }
        }

        $scope.delete = function (callreceivednumber_gid) {
            var params = {
                callreceivednumber_gid: callreceivednumber_gid
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
                    var url = 'api/MstApplication360/DeleteCallReceivedNumber';
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