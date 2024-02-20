(function () {
    'use strict';

    angular
        .module('angle')
        .controller('Usertypecontroller', Usertypecontroller);

    Usertypecontroller.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams'];

    function Usertypecontroller($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'Usertypecontroller';
        $scope.lsoneapipage = $location.search().lsoneapipage;
        var lsoneapipage = $scope.lsoneapipage;
        activate();

        function activate() {
            $scope.totalDisplayed = 100;
            var url = 'api/UserType/GetUserType';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.usertype = resp.data.usertype_list;
                unlockUI();
              
            });
        }
       // Add Code Starts
        $scope.popupusertype = function () {
            var modalInstance = $modal.open({
                templateUrl: '/myModalContent.html',
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
                $scope.usertypeSubmit = function () {
                    var params = {
                        bureau_code: $scope.txtbureau_code,
                        lms_code: $scope.txtlms_code,
                        user_type: $scope.user_type
                    }
                    var url = 'api/UserType/CreateUserType';
                    lockUI()
                    SocketService.post(url, params).then(function (resp) {
                    unlockUI()
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
                            activate();
                        }
                    });

                    $modalInstance.close('closed');
                }
            }
        }
        // Add Code Ends

        // Edit Code Starts
        $scope.edit = function (usertype_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/myModaledit.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    usertype_gid: usertype_gid
                }
                var url = 'api/UserType/EditUserType';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.txtbureau_codeedit = resp.data.bureau_code;
                    $scope.txtlms_codeedit = resp.data.lms_code;
                    $scope.userTypeedit = resp.data.user_type;
                    $scope.usertype_gid = resp.data.usertype_gid;
                });
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.usertypeUpdate = function () {

                    var params = {
                        bureau_code: $scope.txtbureau_codeedit,
                        lms_code: $scope.txtlms_codeedit,
                        user_type: $scope.userTypeedit,
                        usertype_gid: $scope.usertype_gid
                    }
                    var url = 'api/UserType/UpdateUserType';
                    lockUI();
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
                            $modalInstance.close('closed');
                            Notify.alert(resp.data.message, {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            activate();

                        }
                        lockUI();
                    });
                    $modalInstance.close('closed');
                }
            }
        }
        // Edit Code Ends

        // Delete Code Starts
        $scope.delete = function (usertype_gid) {
            var params = {
                usertype_gid: usertype_gid
            }
            var url = 'api/UserType/DeleteUserType';
            SocketService.getparams(url, params).then(function (resp) {
                if (resp.data.status == true) {

                    SweetAlert.swal({
                        title: 'Are you sure?',
                        text: 'Do You Want To Delete the Record ?',
                        showCancelButton: true,
                        confirmButtonColor: '#DD6B55',
                        confirmButtonText: 'Yes, delete it!',
                        closeOnConfirm: false
                    }, function (isConfirm) {
                        if (isConfirm) {
                            SweetAlert.swal('Deleted Successfully!');
                            unlockUI();
                            activate();
                        }

                    });
                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'warning',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    unlockUI();
                    activate();
                }
            });
        };
        $scope.Status_update = function (usertype_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/statusCompanyDocument.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                var params = {
                    usertype_gid: usertype_gid
                }
                var url = 'api/UserType/EditUserType';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.txtbureau_codeedit = resp.data.bureau_code;
                    $scope.txtlms_codeedit = resp.data.lms_code;
                    $scope.userTypeedit = resp.data.user_type;
                    $scope.usertype_gid = resp.data.usertype_gid;
                    $scope.rbo_status = resp.data.status_log;
                });
                var url = 'api/UserType/GetActiveLog';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.usertype_list = resp.data.usertype_list;
                });
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.update_status = function () {

                    var params = {
                        remarks: $scope.txtremarks,
                        status_log: $scope.rbo_status,
                        usertype_gid: usertype_gid
                    }
                    var url = 'api/UserType/UserTypeStatusUpdate';
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
                        }
                    });

                    $modalInstance.close('closed');

                }
            }
        }
    }
})();
