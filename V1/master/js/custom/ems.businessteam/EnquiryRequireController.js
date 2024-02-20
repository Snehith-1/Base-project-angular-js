// JavaScript source code
// JavaScript source code
(function () {
    'use strict';

    angular
        .module('angle')
        .controller('EnquiryRequireController', EnquiryRequireController);

    EnquiryRequireController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams'];

    function EnquiryRequireController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'EnquiryRequireController';
        activate();


        function activate() {

            var url = 'api/MstEnquiryRequire/GetEnquiryRequire';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.enquiryrequire_list = resp.data.enquiryrequire_list;
                unlockUI();
            });
        }

        $scope.popupenquiryrequire = function () {
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

                $scope.enquiryrequireSubmit = function () {
                    var params = {
                        enquiryrequire_name: $scope.txtenquiryrequire,
                        lms_code: $scope.txtlms_code,
                        bureau_code: $scope.txtbureau_code
                    }

                    var url = 'api/MstEnquiryRequire/CreateEnquiryRequire';
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
                            $modalInstance.close('closed');
                            Notify.alert(resp.data.message, {
                                status: 'warning',
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

        $scope.editenquiryrequire = function (enquiryrequire_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/editenquiryrequire.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                var params = {
                    enquiryrequire_gid: enquiryrequire_gid
                }
                var url = 'api/MstEnquiryRequire/EditEnquiryRequire';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.txteditlms_code = resp.data.lms_code;
                    $scope.txteditbureau_code = resp.data.bureau_code;
                    $scope.txtenquiryrequire = resp.data.enquiryrequire_name;
                    $scope.enquiryrequire_gid = resp.data.enquiryrequire_gid;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.enquiryrequireUpdate = function () {

                    var url = 'api/MstEnquiryRequire/UpdateEnquiryRequire';
                    var params = {
                        lms_code: $scope.txteditlms_code,
                        bureau_code: $scope.txteditbureau_code,
                        enquiryrequire_name: $scope.txtenquiryrequire,
                        enquiryrequire_gid: $scope.enquiryrequire_gid
                    }
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
                                status: 'warning',
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

        $scope.Status_update = function (enquiryrequire_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/statusaudittype.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                var params = {
                    enquiryrequire_gid: enquiryrequire_gid
                }
                var url = 'api/MstEnquiryRequire/EditEnquiryRequire';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.enquiryrequire_gid = resp.data.enquiryrequire_gid
                    $scope.txtenquiryrequire = resp.data.enquiryrequire_name;
                    $scope.rbo_status = resp.data.Status;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.update_status = function () {

                    var params = {
                        enquiryrequire_name: $scope.txtenquiryrequire,
                        enquiryrequire_gid: $scope.enquiryrequire_gid,
                        remarks: $scope.txtremarks,
                        rbo_status: $scope.rbo_status

                    }
                    var url = 'api/MstEnquiryRequire/InactiveEnquiryRequire';
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
                    enquiryrequire_gid: enquiryrequire_gid
                }

                var url = 'api/MstEnquiryRequire/EnquiryRequireInactiveLogview';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.enquiryrequireinactivelog_data = resp.data.enquiryrequire_list;
                    unlockUI();
                });
            }
        }


        $scope.deleteenquiryrequire = function (enquiryrequire_gid) {
            var params = {
                enquiryrequire_gid: enquiryrequire_gid
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
                    var url = 'api/MstEnquiryRequire/DeleteEnquiryRequire';
                    SocketService.getparams(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            SweetAlert.swal('Deleted Successfully!');

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
                }

            });
        };
    }

})();