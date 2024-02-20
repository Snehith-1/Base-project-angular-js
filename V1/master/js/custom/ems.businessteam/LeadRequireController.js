// JavaScript source code
(function () {
    'use strict';

    angular
        .module('angle')
        .controller('LeadRequireController', LeadRequireController);

    LeadRequireController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams'];

    function LeadRequireController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'LeadRequireController';
        activate();


        function activate() {

            var url = 'api/MarMstLeadRequire/GetLeadRequire';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.leadrequire_list = resp.data.leadrequire_list;
                unlockUI();
            });
        }

        $scope.popupleadrequire = function () {
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

                $scope.leadrequireSubmit = function () {
                    var params = {
                        leadrequire_name: $scope.txtleadrequire,
                        lms_code: $scope.txtlms_code,
                        bureau_code: $scope.txtbureau_code
                    }

                    var url = 'api/MarMstLeadRequire/CreateLeadRequire';
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

        $scope.editleadrequire = function (leadrequire_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/editleadrequire.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                var params = {
                    leadrequire_gid: leadrequire_gid
                }
                var url = 'api/MarMstLeadRequire/EditLeadRequire';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.txteditlms_code = resp.data.lms_code;
                    $scope.txteditbureau_code = resp.data.bureau_code;
                    $scope.txtleadrequire = resp.data.leadrequire_name;
                    $scope.leadrequire_gid = resp.data.leadrequire_gid;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.leadrequireUpdate = function () {

                    var url = 'api/MarMstLeadRequire/UpdateLeadRequire';
                    var params = {
                        lms_code: $scope.txteditlms_code,
                        bureau_code: $scope.txteditbureau_code,
                        leadrequire_name: $scope.txtleadrequire,
                        leadrequire_gid: $scope.leadrequire_gid
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

        $scope.Status_update = function (leadrequire_gid) {
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
                    leadrequire_gid: leadrequire_gid
                }
                var url = 'api/MarMstLeadRequire/EditLeadRequire';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.leadrequire_gid = resp.data.leadrequire_gid
                    $scope.txtleadrequire = resp.data.leadrequire_name;
                    $scope.rbo_status = resp.data.Status;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.update_status = function () {

                    var params = {
                        leadrequire_name: $scope.txtleadrequire,
                        leadrequire_gid: $scope.leadrequire_gid,
                        remarks: $scope.txtremarks,
                        rbo_status: $scope.rbo_status

                    }
                    var url = 'api/MarMstLeadRequire/InactiveLeadRequire';
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
                    leadrequire_gid: leadrequire_gid
                }

                var url = 'api/MarMstLeadRequire/LeadRequireInactiveLogview';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.leadrequireinactivelog_data = resp.data.leadrequire_list;
                    unlockUI();
                });
            }
        }

     
        $scope.deleteleadrequire = function (leadrequire_gid) {
            var params = {
                leadrequire_gid: leadrequire_gid
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
                    var url = 'api/MarMstLeadRequire/DeleteLeadRequire';
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