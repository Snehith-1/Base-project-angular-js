(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstResidenceTypeController', MstResidenceTypeController);

        MstResidenceTypeController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function MstResidenceTypeController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstResidenceTypeController';
        $scope.lsoneapipage = $location.search().lsoneapipage;
        var lsoneapipage = $scope.lsoneapipage;
        activate();

        function activate() { 
            var url = 'api/MstApplication360/GetResidenceType';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.residencetype_data = resp.data.application_list;
                unlockUI();
            });
        }

        $scope.addResidenceType = function () {
            var modalInstance = $modal.open({
                templateUrl: '/addResidenceType.html',
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
                        residencetype_name: $scope.txtResidenceType_name,
                        lms_code: $scope.txtlms_code,
                        bureau_code: $scope.txtbureau_code

                    }
                    var url = 'api/MstApplication360/CreateResidenceType';
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

        $scope.editResidenceType = function (residencetype_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/editResidenceType.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    residencetype_gid: residencetype_gid
                }
                var url = 'api/MstApplication360/EditResidenceType';
                SocketService.getparams(url, params).then(function (resp) {

                    $scope.txteditResidenceType_name = resp.data.residencetype_name;
                    $scope.txteditlms_code = resp.data.lms_code;
                    $scope.txteditbureau_code = resp.data.bureau_code;
                    $scope.residencetype_gid = resp.data.residencetype_gid;
                });
                
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.update = function () {

                    var url = 'api/MstApplication360/UpdateResidenceType';
                    var params = {
                        residencetype_name: $scope.txteditResidenceType_name,
                        lms_code: $scope.txteditlms_code,
                        bureau_code: $scope.txteditbureau_code,
                        residencetype_gid: $scope.residencetype_gid
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

        $scope.Status_update = function (residencetype_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/statusResidenceType.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                var params = {
                    residencetype_gid: residencetype_gid
                }
                var url = 'api/MstApplication360/EditResidenceType';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.residencetype_gid = resp.data.residencetype_gid
                    $scope.residencetype_name = resp.data.residencetype_name;
                    $scope.rbo_status = resp.data.Status;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.update_status = function () {

                    var params = {
                        residencetype_gid: residencetype_gid,
                        residencetype_name: $scope.residencetype_name,
                        remarks: $scope.txtremarks,
                        rbo_status: $scope.rbo_status

                    }
                    var url = 'api/MstApplication360/InactiveResidenceType';
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
                    residencetype_gid: residencetype_gid
                }

                var url = 'api/MstApplication360/InactiveResidenceTypeHistory';
                lockUI();
                SocketService.getparams(url, param).then(function (resp) {
                    $scope.residencetypeinactivelog_data = resp.data.inactivehistory_list;
                    unlockUI();
                });

            }
        }

        $scope.delete = function (residencetype_gid) {
            var params = {
                residencetype_gid: residencetype_gid
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
                            var url = 'api/MstApplication360/DeleteResidenceType';
                            SocketService.getparams(url, params).then(function (resp) {
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
                                }
                            });
                            }
                    });
        }
    }
})();

