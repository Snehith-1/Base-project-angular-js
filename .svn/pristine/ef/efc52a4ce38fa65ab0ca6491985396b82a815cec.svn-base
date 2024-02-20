(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AtmMstAuditDepartmentController', AtmMstAuditDepartmentController);

    AtmMstAuditDepartmentController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams'];

    function AtmMstAuditDepartmentController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AtmMstAuditDepartmentController';
        activate();


        function activate() {
            var url = 'api/AtmMstAuditDepartment/GetAuditDepartment';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.auditdepartment_list = resp.data.auditdepartment_list;
                unlockUI();
            });

        }



        $scope.popupaddauditdepartment = function () {
            var modalInstance = $modal.open({
                templateUrl: '/addauditdepartment.html',
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

                $scope.auditdepartmentSubmit = function () {
                    var params = {
                        auditdepartment_gid: $scope.auditdepartment_gid,
                        auditdepartment_name: $scope.txtaudit_department,
                        department_code: $scope.txtdepartment_code,
                        lms_code: $scope.txtlms_code,
                        bureau_code: $scope.txtbureau_code

                    }

                    var url = 'api/AtmMstAuditDepartment/AddAuditDepartment';
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
                                status: 'Warning',
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
        $scope.edit = function (auditdepartment_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/editauditdepartment.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {


                var params = {
                    auditdepartment_gid: auditdepartment_gid
                }
                var url = 'api/AtmMstAuditDepartment/EditAuditDepartment';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.txteditaudit_department = resp.data.auditdepartment_name;
                    $scope.txtdepartment_code = resp.data.department_code;
                    $scope.auditdepartment_gid = resp.data.auditdepartment_gid;
                    $scope.txteditlms_code = resp.data.lms_code;
                    $scope.txteditbureau_code = resp.data.bureau_code;

                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.auditdepartmentUpdate = function () {

                    var url = 'api/AtmMstAuditDepartment/UpdateAuditDepartment';
                    var params = {
                        auditdepartment_gid: $scope.auditdepartment_gid,
                        auditdepartment_name: $scope.txteditaudit_department,
                        lms_code: $scope.txteditlms_code,
                        bureau_code: $scope.txteditbureau_code

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

        $scope.Status_update = function (auditdepartment_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/statusauditdepartment.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                var params = {
                    auditdepartment_gid: auditdepartment_gid
                }
                var url = 'api/AtmMstAuditDepartment/EditAuditDepartment';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.auditdepartment_gid = resp.data.auditdepartment_gid;
                    $scope.txtaudit_department = resp.data.auditdepartment_name;
                    $scope.txtremarks = resp.data.remarks;
                    $scope.rbo_status = resp.data.Status;


                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.update_status = function () {

                    var params = {
                        audit_department: $scope.lblaudit_department,
                        remarks: $scope.txtremarks,
                        rbo_status: $scope.rbo_status,
                        auditdepartment_gid: auditdepartment_gid
                    }
                    var url = 'api/AtmMstAuditDepartment/InactiveAuditDepartment';
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
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                        }
                    });

                    $modalInstance.close('closed');

                }

                var param = {
                    auditdepartment_gid: auditdepartment_gid
                }

                var url = 'api/AtmMstAuditDepartment/AuditDepartmentInactiveLogview';
                lockUI();
                SocketService.getparams(url, param).then(function (resp) {
                    $scope.auditdepartmentinactivelog_data = resp.data.auditdepartment_list;
                    unlockUI();
                });

            }
        }
        //    $scope.delete = function (auditdepartment_gid) {
        //        var params = {
        //            auditdepartment_gid: auditdepartment_gid
        //        }
        //        var url = 'api/AtmMstAuditDepartment/DeleteAuditDepartment';
        //        SocketService.getparams(url, params).then(function (resp) {
        //            if (resp.data.status == true) {

        //                SweetAlert.swal({
        //                    title: 'Are you sure?',
        //                    text: 'Do You Want To Delete the Record ?',
        //                    showCancelButton: true,
        //                    confirmButtonColor: '#DD6B55',
        //                    confirmButtonText: 'Yes, delete it!',
        //                    closeOnConfirm: false
        //                }, function (isConfirm) {
        //                    if (isConfirm) {
        //                        SweetAlert.swal('Deleted Successfully!');
        //                        unlockUI();
        //                        activate();
        //                    }

        //                });
        //            }
        //            else {
        //                Notify.alert(resp.data.message, {
        //                    status: 'warning',
        //                    pos: 'top-center',
        //                    timeout: 3000
        //                });
        //                unlockUI();
        //                activate();
        //            }
        //        });
        //    };

        //}
        $scope.delete = function (auditdepartment_gid) {
            var params = {
                auditdepartment_gid: auditdepartment_gid
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
                    var url = 'api/AtmMstAuditDepartment/DeleteAuditDepartment';
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