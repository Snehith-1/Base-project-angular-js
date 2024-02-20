(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstPhysicalStatusSummaryController', MstPhysicalStatusSummaryController);

        MstPhysicalStatusSummaryController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function MstPhysicalStatusSummaryController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstPhysicalStatusSummaryController';
        $scope.lsoneapipage = $location.search().lsoneapipage;
        var lsoneapipage = $scope.lsoneapipage;
        activate();

        function activate() {
            
            var url = 'api/MstApplication360/GetPhysicalStatus';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.physicalstatus_data = resp.data.application_list;
                unlockUI();
              
            });
        }

        $scope.addphysicalstatus = function () {
            var modalInstance = $modal.open({
                templateUrl: '/addphysicalstatus.html',
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
                        physicalstatus_name: $scope.txtphysicalstatus_name,
                        lms_code: $scope.txtlms_code,
                        bureau_code: $scope.txtbureau_code

                    }
                    var url = 'api/MstApplication360/CreatePhysicalStatus';
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
                
            }
        }

        $scope.editphysicalstatus = function (physicalstatus_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/editphysicalstatus.html',
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

                var params = {
                    physicalstatus_gid: physicalstatus_gid
                }
                var url = 'api/MstApplication360/EditPhysicalStatus';
                SocketService.getparams(url, params).then(function (resp) {

                    $scope.txteditphysicalstatus_name = resp.data.physicalstatus_name;
                    $scope.txteditlms_code = resp.data.lms_code;
                    $scope.txteditbureau_code = resp.data.bureau_code;
                    $scope.physicalstatus_gid = resp.data.physicalstatus_gid;
                });

                $scope.update = function () {

                    var url = 'api/MstApplication360/UpdatePhysicalStatus';
                    var params = {
                        physicalstatus_name: $scope.txteditphysicalstatus_name,
                        lms_code: $scope.txteditlms_code,
                        bureau_code: $scope.txteditbureau_code,
                        physicalstatus_gid: $scope.physicalstatus_gid
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

        $scope.Status_update = function (physicalstatus_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/statusphysicalstatus.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                var params = {
                    physicalstatus_gid: physicalstatus_gid
                }
                var url = 'api/MstApplication360/EditPhysicalStatus';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.physicalstatus_gid = resp.data.physicalstatus_gid
                    $scope.txteditphysicalstatus_name = resp.data.physicalstatus_name;
                    $scope.rbo_status = resp.data.Status;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.update_status = function () {

                    var params = {
                        physicalstatus_name: $scope.txteditphysicalstatus_name,
                        remarks: $scope.txtremarks,
                        rbo_status: $scope.rbo_status,
                        physicalstatus_gid: $scope.physicalstatus_gid

                    }

                    var url = 'api/MstApplication360/InactivePhysicalStatus';
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
                    physicalstatus_gid: physicalstatus_gid
                }

                var url = 'api/MstApplication360/PhysicalStatusInactiveLogview';
                lockUI();
                SocketService.getparams(url, param).then(function (resp) {
                    $scope.physicalstatusinactivelog_data = resp.data.application_list;
                    
                    unlockUI();
                });
            }
        }

        $scope.delete = function (physicalstatus_gid) {
            var params = {
                physicalstatus_gid: physicalstatus_gid
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
                            var url = 'api/MstApplication360/DeletePhysicalStatus';
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

