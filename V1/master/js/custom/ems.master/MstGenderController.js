(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstGenderController', MstGenderController);

    MstGenderController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function MstGenderController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstGenderController';
        $scope.lsoneapipage = $location.search().lsoneapipage;
        var lsoneapipage = $scope.lsoneapipage;
        activate();

        function activate() {
            var url = 'api/MstApplication360/GetGender';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.gender_data = resp.data.application_list;
                unlockUI();
            });
        }


        $scope.addGender = function () {
            var modalInstance = $modal.open({
                templateUrl: '/addGender.html',
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
                        gender_name: $scope.txtGender_name,
                        lms_code: $scope.txtlms_code,
                        bureau_code: $scope.txtbureau_code

                    }
                    var url = 'api/MstApplication360/CreateGender';
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
                           
                       
                        }
                    });

                      $modalInstance.close('closed');

                }
                
            }
        }

        $scope.editGender = function (gender_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/editGender.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    gender_gid: gender_gid
                }
                var url = 'api/MstApplication360/EditGender';
                SocketService.getparams(url, params).then(function (resp) {

                    $scope.txteditGender_name = resp.data.gender_name;
                    $scope.txteditlms_code = resp.data.lms_code;
                    $scope.txteditbureau_code = resp.data.bureau_code;
                    $scope.gender_gid = resp.data.gender_gid;
                });

                
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.update = function () {

                    var url = 'api/MstApplication360/UpdateGender';
                    var params = {
                        gender_name: $scope.txteditGender_name,
                        lms_code: $scope.txteditlms_code,
                        bureau_code: $scope.txteditbureau_code,
                        gender_gid: $scope.gender_gid
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

        $scope.Status_update = function (gender_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/statusGender.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                var params = {
                    gender_gid: gender_gid
                }
                var url = 'api/MstApplication360/EditGender';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.gender_gid = resp.data.gender_gid
                    $scope.txtgender_name = resp.data.gender_name;
                    $scope.rbo_status = resp.data.Status;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.update_status = function () {

                    var params = {
                        gender_gid: gender_gid,
                        remarks: $scope.txtremarks,
                        rbo_status: $scope.rbo_status

                    }
                    var url = 'api/MstApplication360/InactiveGender';
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
                    gender_gid: gender_gid
                }

                var url = 'api/MstApplication360/GenderInactiveLogview';
                lockUI();
                SocketService.getparams(url, param).then(function (resp) {
                    $scope.genderinactivelog_data = resp.data.application_list;
                    unlockUI();
                });

            }
        }

        $scope.delete = function (gender_gid) {
            var params = {
                gender_gid: gender_gid
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
                            var url = 'api/MstApplication360/DeleteGender';
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
                                    unlockUI();
                                }
                            });
                        }
                    });
        }
    }
})();

