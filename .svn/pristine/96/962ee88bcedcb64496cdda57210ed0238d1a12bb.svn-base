(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstSalutationController', MstSalutationController);

        MstSalutationController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function MstSalutationController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstSalutationController';
        $scope.lsoneapipage = $location.search().lsoneapipage;
        var lsoneapipage = $scope.lsoneapipage;
        activate();

        function activate() { 
           
            var url = 'api/MstApplication360/Getsalutation';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.salutation_list = resp.data.application_list;
                unlockUI();
            });
        }

        $scope.addsalutation = function () {
            var modalInstance = $modal.open({
                templateUrl: '/addsalutation.html',
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
                        salutation_name: $scope.txtsalutation_name,
                        lms_code: $scope.txtlms_code,
                        bureau_code: $scope.txtbureau_code
                    
                    }
                    var url = 'api/MstApplication360/Createsalutation';
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

        $scope.editsalutation = function (salutation_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/editsalutation.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    salutation_gid: salutation_gid
                }
                var url = 'api/MstApplication360/Editsalutation';
                SocketService.getparams(url, params).then(function (resp) {

                    $scope.txteditsalutation_name = resp.data.salutation_name;
                    $scope.txteditlms_code = resp.data.lms_code;
                    $scope.txteditbureau_code = resp.data.bureau_code;
                    $scope.salutation_gid = resp.data.salutation_gid;
                });
                
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                                              
                $scope.update = function () {

                    var url = 'api/MstApplication360/Updatesalutation';
                    var params = {
                        salutation_name: $scope.txteditsalutation_name,
                        lms_code: $scope.txteditlms_code,
                        bureau_code: $scope.txteditbureau_code,
                        salutation_gid: $scope.salutation_gid
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

        $scope.Status_update = function (salutation_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/statussalutation.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                                
                var params = {
                    salutation_gid: salutation_gid
                }
                var url = 'api/MstApplication360/Editsalutation';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.salutation_gid = resp.data.salutation_gid
                    $scope.txtsalutation_name = resp.data.salutation_name;
                    $scope.rbo_status = resp.data.Status;
                });
                           
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.update_status = function () {

                    var params = {
                        salutation_name: $scope.txtsalutation_name,
                        salutation_gid: $scope.salutation_gid,
                        remarks: $scope.txtremarks,
                        rbo_status: $scope.rbo_status
                    
                    }
                    var url = 'api/MstApplication360/Inactivesalutation';
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
                    salutation_gid: salutation_gid
                }

                var url = 'api/MstApplication360/InactivesalutationHistory';
                lockUI();
                SocketService.getparams(url, param).then(function (resp) {
                    $scope.salutationinactivelog_list = resp.data.inactivehistory_list;
                    unlockUI();
                });
            }
        }

        $scope.delete = function (salutation_gid) {
            var params = {
                salutation_gid: salutation_gid
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
                    var url = 'api/MstApplication360/Deletesalutation';
                    SocketService.getparams(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            activate();
                        }
                        else {
                            Notify.alert('Error Occurred While Deleting Party Type!', {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            activate();
                        }
                    }); 
                    SweetAlert.swal('Deleted Successfully!');
                }

            });
        };
    }
})();

