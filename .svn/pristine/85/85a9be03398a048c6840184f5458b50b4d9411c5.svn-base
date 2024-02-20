(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AgrMstNatureFormStateofCommodityController', AgrMstNatureFormStateofCommodityController);

        AgrMstNatureFormStateofCommodityController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function AgrMstNatureFormStateofCommodityController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AgrMstNatureFormStateofCommodityController';
        $scope.lsoneapipage = $location.search().lsoneapipage;
        var lsoneapipage = $scope.lsoneapipage;

        activate();

        function activate() {
            var url = 'api/AgrMstApplication360/GetNatureFormStateofCommodity';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.natureformstateofcommodity_data = resp.data.application_list;
                unlockUI();
            });
        }

        $scope.addnatureformstateofcommodity = function () {
            var modalInstance = $modal.open({
                templateUrl: '/addnatureformstateofcommodity.html',
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
                        natureformstateofcommodity_name: $scope.txtnatureform_stateofcommodity,
                        lms_code: $scope.txtlms_code,
                        bureau_code: $scope.txtbureau_code
                    }
                    var url = 'api/AgrMstApplication360/CreatetNatureFormStateofCommodity';
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
                }

            }
        }

        $scope.editnatureformstateofcommodity = function (natureformstateofcommodity_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/editnatureformstateofcommodity.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                var params = {
                    natureformstateofcommodity_gid: natureformstateofcommodity_gid
                }
                var url = 'api/AgrMstApplication360/EditNatureFormStateofCommodity';
                SocketService.getparams(url, params).then(function (resp) {

                    $scope.txteditnatureform_stateofcommodity = resp.data.natureformstateofcommodity_name;
                    $scope.txteditlms_code = resp.data.lms_code;
                    $scope.txteditbureau_code = resp.data.bureau_code;
                    $scope.natureformstateofcommodity_gid = resp.data.natureformstateofcommodity_gid;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.update = function () {
                    var url = 'api/AgrMstApplication360/UpdateNatureFormStateofCommodity';
                    var params = {
                        natureformstateofcommodity_name: $scope.txteditnatureform_stateofcommodity,
                        lms_code: $scope.txteditlms_code,
                        bureau_code: $scope.txteditbureau_code,
                        natureformstateofcommodity_gid: $scope.natureformstateofcommodity_gid
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

        $scope.Status_update = function (natureformstateofcommodity_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/statusnatureformstateofcommodity.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    natureformstateofcommodity_gid: natureformstateofcommodity_gid
                }
                var url = 'api/AgrMstApplication360/EditNatureFormStateofCommodity';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.natureformstateofcommodity_gid = resp.data.natureformstateofcommodity_gid
                    $scope.txtnatureform_stateofcommodity = resp.data.natureformstateofcommodity_name;
                    $scope.rbo_status = resp.data.Status;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.update_status = function () {

                    var params = {
                        natureformstateofcommodity_gid: $scope.natureformstateofcommodity_gid,
                        natureformstateofcommodity_name: $scope.txtnatureform_stateofcommodity,
                        remarks: $scope.txtremarks,
                        rbo_status: $scope.rbo_status

                    }
                    var url = 'api/AgrMstApplication360/InactiveNatureFormStateofCommodity';
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
                    natureformstateofcommodity_gid: natureformstateofcommodity_gid
                }

                var url = 'api/AgrMstApplication360/NatureFormStateofCommodityInactiveLogview';
                lockUI();
                SocketService.getparams(url, param).then(function (resp) {
                    $scope.natureformstateofcommodityinactivelog_data = resp.data.application_list;
                    unlockUI();
                });
            }
        }

        $scope.delete = function (natureformstateofcommodity_gid) {
            var params = {
                natureformstateofcommodity_gid: natureformstateofcommodity_gid
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
                    var url = 'api/AgrMstApplication360/DeleteNatureFormStateofCommodity';
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

