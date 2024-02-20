(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstTypeofChargecreatedController', MstTypeofChargecreatedController);

        MstTypeofChargecreatedController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function MstTypeofChargecreatedController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstTypeofChargecreatedController';
        $scope.lsoneapipage = $location.search().lsoneapipage;
        var lsoneapipage = $scope.lsoneapipage;
        activate();

        function activate() { 
            
            var url = 'api/MstApplication360/GetTypeOfChargeCreated';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.TypeofChargecreated_list = resp.data.application_list;
                unlockUI();

            });
        }

        $scope.addTypeofChargecreate = function () {
            var modalInstance = $modal.open({
                templateUrl: '/addTypeofChargecreate.html',
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
                        typeofchargecreated_name: $scope.txtTypeofChargecreated_name,
                        lms_code: $scope.txtlms_code,
                        bureau_code: $scope.txtbureau_code
                    
                    }
                    var url = 'api/MstApplication360/CreateTypeOfChargeCreated';
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

        $scope.editTypeofChargecreated = function (typeofchargecreated_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/editTypeofChargecreated.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                var params = {
                    typeofchargecreated_gid: typeofchargecreated_gid
                }
                var url = 'api/MstApplication360/EditTypeOfChargeCreated';
                SocketService.getparams(url, params).then(function (resp) {

                    $scope.txteditTypeofChargecreated_name = resp.data.typeofchargecreated_name;
                    $scope.txteditlms_code = resp.data.lms_code;
                    $scope.txteditbureau_code = resp.data.bureau_code;
                    $scope.typeofchargecreated_gid = resp.data.typeofchargecreated_gid;
                });
                
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                                              
                    $scope.update = function () {

                        var url = 'api/MstApplication360/UpdateTypeOfChargeCreated';
                        var params = {
                            typeofchargecreated_name: $scope.txteditTypeofChargecreated_name,
                            lms_code: $scope.txteditlms_code,
                            bureau_code: $scope.txteditbureau_code,
                            typeofchargecreated_gid: $scope.typeofchargecreated_gid
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

            $scope.Status_update = function (typeofchargecreated_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/statusTypeofChargecreated.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                                
                var params = {
                    typeofchargecreated_gid: typeofchargecreated_gid
                }
                var url = 'api/MstApplication360/EditTypeOfChargeCreated';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.typeofchargecreated_gid = resp.data.typeofchargecreated_gid
                    $scope.txttypeofchargecreated_name = resp.data.typeofchargecreated_name;
                    $scope.rbo_status = resp.data.Status;
                });
                           
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.update_status = function () {

                    var params = {
                        typeofchargecreated_name: $scope.txttypeofchargecreated_name,
                        typeofchargecreated_gid: $scope.typeofchargecreated_gid,
                        remarks: $scope.txtremarks,
                        rbo_status:$scope.rbo_status
                    
                    }
                    var url = 'api/MstApplication360/InactiveTypeOfChargeCreated';
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
                    typeofchargecreated_gid: typeofchargecreated_gid
                }

                var url = 'api/MstApplication360/InactiveTypeOfChargeCreatedHistory';
                lockUI();
                SocketService.getparams(url, param).then(function (resp) {
                    $scope.typeofchargecreatedinactivelog_data = resp.data.inactivehistory_list;
                    unlockUI();
                });
            }
        }

            $scope.delete = function (typeofchargecreated_gid) {
             var params = {
                typeofchargecreated_gid: typeofchargecreated_gid
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
                    var url = 'api/MstApplication360/DeleteTypeOfChargeCreated';
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

