(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AgrMstTypeofSupplyNatureController', AgrMstTypeofSupplyNatureController);

        AgrMstTypeofSupplyNatureController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function AgrMstTypeofSupplyNatureController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AgrMstTypeofSupplyNatureController';
        $scope.lsoneapipage = $location.search().lsoneapipage;
        var lsoneapipage = $scope.lsoneapipage;


        activate();

        function activate() { 
            
            var url = 'api/AgrMstSamAgroMaster/Gettypeofsupplynature';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.typeofsupplynature_data = resp.data.applicationmst_list;
                unlockUI();
            });
        }

        $scope.addTypeofSupplyNature = function () {
            var modalInstance = $modal.open({
                templateUrl: '/addTypeofSupplyNature.html',
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
                        typeofsupplynature_name: $scope.txttypeofsupplynature_name,
                        lms_code: $scope.txtlms_code,
                        bureau_code: $scope.txtbureau_code

                    }
                    var url = 'api/AgrMstSamAgroMaster/Createtypeofsupplynature';
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

                    // $modalInstance.close('closed');

                }
                
            }
        }

        $scope.editTypeofSupplyNature = function (typeofsupplynature_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/editTypeofSupplyNature.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    typeofsupplynature_gid: typeofsupplynature_gid
                }
                var url = 'api/AgrMstSamAgroMaster/Edittypeofsupplynature';
                SocketService.getparams(url, params).then(function (resp) {

                    $scope.txtedittypeofsupplynature_name = resp.data.typeofsupplynature_name;
                    $scope.txteditlms_code = resp.data.lms_code;
                    $scope.txteditbureau_code = resp.data.bureau_code;
                    $scope.typeofsupplynature_gid = resp.data.typeofsupplynature_gid;
                });
                
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.update = function () {

                    var url = 'api/AgrMstSamAgroMaster/Updatetypeofsupplynature';
                    var params = {
                        typeofsupplynature_name: $scope.txtedittypeofsupplynature_name,
                        lms_code: $scope.txteditlms_code,
                        bureau_code: $scope.txteditbureau_code,
                        typeofsupplynature_gid: $scope.typeofsupplynature_gid
                    }
                    SocketService.post(url, params).then(function (resp) {
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
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                        }
                    });$modalInstance.close('closed');

                }
            }
        }

        $scope.Status_update = function (typeofsupplynature_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/statusTypeofSupplyNature.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                var params = {
                    typeofsupplynature_gid: typeofsupplynature_gid
                }
                var url = 'api/AgrMstSamAgroMaster/Edittypeofsupplynature';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.typeofsupplynature_gid = resp.data.typeofsupplynature_gid
                    $scope.typeofsupplynature_name = resp.data.typeofsupplynature_name;
                    $scope.rbo_status = resp.data.Status;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.update_status = function () {

                    var params = {
                        typeofsupplynature_name :$scope.typeofsupplynature_name,
                        typeofsupplynature_gid: typeofsupplynature_gid,
                        remarks: $scope.txtremarks,
                        rbo_status: $scope.rbo_status

                    }
                    var url = 'api/AgrMstSamAgroMaster/Inactivetypeofsupplynature';
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
                    typeofsupplynature_gid: typeofsupplynature_gid
                }

                var url = 'api/AgrMstSamAgroMaster/InactivetypeofsupplynatureHistory';
                lockUI();
                SocketService.getparams(url, param).then(function (resp) {
                    $scope.typeofsupplynatureinactivelog_data = resp.data.inactivehistory_list;
                    unlockUI();
                });

            }
        }

        $scope.delete = function (typeofsupplynature_gid) {
            var params = {
                typeofsupplynature_gid: typeofsupplynature_gid
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
                    var url = 'api/AgrMstSamAgroMaster/Deletetypeofsupplynature';
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

