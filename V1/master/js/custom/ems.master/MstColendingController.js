(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstColendingController', MstColendingController);

    MstColendingController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function MstColendingController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstColendingController';
        $scope.lsoneapipage = $location.search().lsoneapipage;
        var lsoneapipage = $scope.lsoneapipage;

        activate();

        function activate() {
            $scope.total = 0;
            $scope.totalDisplayed = 100;
            var url = 'api/MstApplication360/GetColendingMaster';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.colending_list = resp.data.application_list;
                unlockUI();
            });
        }

        $scope.addColending = function () {
            var modalInstance = $modal.open({
                templateUrl: '/addColending.html',
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
                        colendingmaster_name: $scope.txtColending_name,                       
                        lms_code: $scope.txtlms_code,
                        bureau_code: $scope.txtbureau_code,
                        description: $scope.txtdescription

                    }
                    var url = 'api/MstApplication360/CreateColendingMaster';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
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
                                status: 'info',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            activate();
                        }
                    });

                    $modalInstance.close('closed');
                    activate();
                }
                
            }
        }

        $scope.editcolending = function (colendingmaster_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/editColending.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                var params = {
                    colendingmaster_gid: colendingmaster_gid
                }
                console.log(params);
                var url = 'api/MstApplication360/EditColendingMaster';
                SocketService.getparams(url, params).then(function (resp) {

                    $scope.txteditColending_name = resp.data.colendingmaster_name;
                    $scope.txteditlms_code = resp.data.lms_code;
                    $scope.txteditbureau_code = resp.data.bureau_code;
                    $scope.txteditdescription = resp.data.description;
                    $scope.colendingmaster_gid = resp.data.colendingmaster_gid;
                    //console.log(resp.data.colendingmaster_gid);
                });
                
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.update = function () {

                    var url = 'api/MstApplication360/UpdateColendingMaster';
                    var params = {
                        colendingmaster_name: $scope.txteditColending_name,
                        lms_code: $scope.txteditlms_code,
                        bureau_code: $scope.txteditbureau_code,
                        description: $scope.txteditdescription,
                        colendingmaster_gid: $scope.colendingmaster_gid
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

        $scope.Status_update = function (colendingmaster_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/statusColending.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    colendingmaster_gid: colendingmaster_gid
                }
                var url = 'api/MstApplication360/EditColendingMaster';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.colendingmaster_gid = resp.data.colendingmaster_gid
                    $scope.txtcolending_name = resp.data.colendingmaster_name;
                    $scope.rbo_status = resp.data.Status;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.update_status = function () {

                    var params = {
                        colendingmaster_gid: colendingmaster_gid,                       
                        remarks: $scope.txtremarks,
                        rbo_status: $scope.rbo_status
                    }

                    var url = 'api/MstApplication360/InactiveColendingMaster';
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
                    colendingmaster_gid: colendingmaster_gid
                }

                var url = 'api/MstApplication360/InactiveColendingMasterHistory';
                lockUI();
                SocketService.getparams(url, param).then(function (resp) {
                    $scope.colendinginactivelog_data = resp.data.inactivehistory_list;
                    unlockUI();
                });
            }
        }

        $scope.delete = function (colendingmaster_gid) {
            var params = {
                colendingmaster_gid: colendingmaster_gid
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
                    var url = 'api/MstApplication360/DeleteColendingMaster';
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

