(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AtmMstAuditPriorityController', AtmMstAuditPriorityController);

    AtmMstAuditPriorityController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams'];

    function AtmMstAuditPriorityController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AtmMstAuditPriorityController';
        activate();


        function activate() {

            var url = 'api/AtmMstAuditPriority/GetAuditPriority';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.auditpriority_data = resp.data.auditpriority_list;
                unlockUI();
            });
        }

        $scope.popupauditpriority = function () {
            var modalInstance = $modal.open({
                templateUrl: '/myModalContent.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.auditprioritySubmit = function () {
                    var params = {
                        auditpriority_gid: $scope.auditpriority_gid,
                        auditpriority_name: $scope.txtaudit_priority,
                        auditpriority_code: $scope.txtauditpriority_code,
                        lms_code: $scope.txtlms_code,
                        bureau_code: $scope.txtbureau_code
                    }

                    var url = 'api/AtmMstAuditPriority/CreateAuditPriority';
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

        $scope.editauditpriority = function (auditpriority_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/editauditpriority.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                var params = {
                    auditpriority_gid: auditpriority_gid
                }
                var url = 'api/AtmMstAuditPriority/EditAuditPriority';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.txteditauditpriority_code = resp.data.auditpriority_code;
                    $scope.txteditlms_code = resp.data.lms_code;
                    $scope.txteditbureau_code = resp.data.bureau_code;
                    $scope.txteditaudit_priority = resp.data.auditpriority_name;
                    $scope.auditpriority_gid = resp.data.auditpriority_gid;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.auditpriorityUpdate = function () {

                    var url = 'api/AtmMstAuditPriority/UpdateAuditPriority';
                    var params = {
                        lms_code: $scope.txteditlms_code,
                        bureau_code: $scope.txteditbureau_code,
                        auditpriority_code: $scope.txteditauditpriority_code,
                        auditpriority_name: $scope.txteditaudit_priority,
                        auditpriority_gid: $scope.auditpriority_gid
                    }
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {
                            $modalInstance.close('closed');
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

        $scope.Status_update = function (auditpriority_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/statusauditpriority.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                var params = {
                    auditpriority_gid: auditpriority_gid
                }
                var url = 'api/AtmMstAuditPriority/EditAuditPriority';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.auditpriority_gid = resp.data.auditpriority_gid
                    $scope.txtaudit_priority = resp.data.auditpriority_name
                    $scope.rbo_status = resp.data.Status;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.update_status = function () {

                    var params = {
                        auditpriority_name: $scope.txtaudit_priority,
                        auditpriority_gid: $scope.auditpriority_gid,
                        remarks: $scope.txtremarks,
                        rbo_status: $scope.rbo_status

                    }
                    var url = 'api/AtmMstAuditPriority/InactiveAuditPriority';
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
                    auditpriority_gid: auditpriority_gid
                }

                var url = 'api/AtmMstAuditPriority/AuditPriorityInactiveLogview';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.auditpriorityinactivelog_data = resp.data.auditpriority_list;
                    unlockUI();
                });
            }
        }

        //$scope.deleteauditpriority = function (auditpriority_gid) {
        //    var params = {
        //        auditpriority_gid: auditpriority_gid
        //    }
        //    //var url = 'api/AtmMstAuditPriority/DeleteAuditPriority';
        //    SocketService.getparams(url, params).then(function (resp) {
        //        if (resp.data.status == true) {

        //            SweetAlert.swal({
        //                title: 'Are you sure?',
        //                text: 'Do You Want To Delete the Record ?',
        //                showCancelButton: true,
        //                confirmButtonColor: '#DD6B55',
        //                confirmButtonText: 'Yes, delete it!',
        //                closeOnConfirm: false
        //            }, function (isConfirm) {
        //                if (isConfirm) {
        //                    SweetAlert.swal('Deleted Successfully!');
        //                    unlockUI();
        //                    activate();
        //                }

        //            });
        //        }
        //        else {
        //            Notify.alert(resp.data.message, {
        //                status: 'warning',
        //                pos: 'top-center',
        //                timeout: 3000
        //            });
        //            unlockUI();
        //            activate();
        //        }
        //    });
        //};

            $scope.deleteauditpriority = function (auditpriority_gid) {
                var params = {
                    auditpriority_gid: auditpriority_gid
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
                    var url = 'api/AtmMstAuditPriority/DeleteAuditPriority';
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