(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AgrMstUOMcontroller', AgrMstUOMcontroller);

    AgrMstUOMcontroller.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function AgrMstUOMcontroller($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AgrMstUOMcontroller';
        $scope.lsoneapipage = $location.search().lsoneapipage;
        var lsoneapipage = $scope.lsoneapipage;


        activate();

        function activate() {
            $scope.total = 0;
            $scope.totalDisplayed = 100;
            var url = 'api/AgrMstUom/Getuom';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.uom_list = resp.data.Uomdtl;
                unlockUI(); 
            });
        }

        $scope.adduom = function () {
            var modalInstance = $modal.open({
                templateUrl: '/adduom.html',
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
                        uom_name: $scope.txtuom_name,
                        lms_code: $scope.txtlms_code,
                        bureau_code: $scope.txtbureau_code

                    }
                    var url = 'api/AgrMstUom/Createuom';
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

        $scope.edituom = function (uom_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/edituom.html',
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
                    uom_gid: uom_gid
                }
                var url = 'api/AgrMstUom/Edituom';
                SocketService.getparams(url, params).then(function (resp) {

                    $scope.txtedituom_name = resp.data.uom_name;
                    $scope.txteditlms_code = resp.data.lms_code;
                    $scope.txteditbureau_code = resp.data.bureau_code;
                    $scope.uom_gid = resp.data.uom_gid;
                });

                $scope.update = function () {

                    var url = 'api/AgrMstUom/Updateuom';
                    var params = {
                        uom_name: $scope.txtedituom_name,
                        lms_code: $scope.txteditlms_code,
                        bureau_code: $scope.txteditbureau_code,
                        uom_gid: $scope.uom_gid
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

        $scope.Status_update = function (uom_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/statusuom.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                var params = {
                    uom_gid: uom_gid
                }
                var url = 'api/AgrMstUom/Edituom';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.uom_gid = resp.data.uom_gid
                    $scope.txtuom_name = resp.data.uom_name;
                    $scope.rbo_status = resp.data.uom_status;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.update_status = function () {

                    var params = {
                        uom_name: $scope.txtuom_name,
                        remarks: $scope.txtremarks,
                        rbo_status: $scope.rbo_status,
                        uom_gid: uom_gid

                    }

                    var url = 'api/AgrMstUom/Inactiveuom';
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
                    uom_gid: uom_gid
                }

                var url = 'api/AgrMstUom/uomInactiveLogview';
                lockUI();
                SocketService.getparams(url, param).then(function (resp) {
                    $scope.uominactivelog_data = resp.data.Uomdtl; 
                    unlockUI();
                });
            }
        }

        $scope.delete = function (uom_gid) {
            var params = {
                uom_gid: uom_gid
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
                    var url = 'api/AgrMstUom/Deleteuom';
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
