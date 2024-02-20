(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AtmMstAuditFrequencyController', AtmMstAuditFrequencyController);

    AtmMstAuditFrequencyController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams'];

    function AtmMstAuditFrequencyController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AtmMstAuditFrequencyController';
        activate();


        function activate() {

            var url = 'api/AtmMstAuditFrequency/GetAuditFrequency';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.auditfrequency_data = resp.data.auditfrequency_list;
                unlockUI();
            });
        }

        $scope.popupauditfrequency = function () {   
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

                $scope.auditfrequencySubmit = function () {
                    var params = {
                        auditfrequency_gid: $scope.auditfrequency_gid,
                        auditfrequency_name: $scope.txtaudit_frequency,
                        auditfrequency_code: $scope.txtauditfrequency_code,
                        lms_code: $scope.txtlms_code,
                        bureau_code: $scope.txtbureau_code
                    }

                    var url = 'api/AtmMstAuditFrequency/CreateAuditFrequency';
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

        $scope.editauditfrequency = function (auditfrequency_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/editauditfrequency.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                var params = {
                    auditfrequency_gid: auditfrequency_gid
                }
                var url = 'api/AtmMstAuditFrequency/EditAuditFrequency';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.txteditauditfrequency_code = resp.data.auditfrequency_code;
                    $scope.txteditlms_code = resp.data.lms_code;
                    $scope.txteditbureau_code = resp.data.bureau_code;
                    $scope.txteditaudit_frequency = resp.data.auditfrequency_name;
                    $scope.auditfrequency_gid = resp.data.auditfrequency_gid;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.auditfrequencyUpdate = function () {

                    var url = 'api/AtmMstAuditFrequency/UpdateAuditFrequency';
                    var params = {
                        lms_code: $scope.txteditlms_code,
                        bureau_code: $scope.txteditbureau_code,
                        auditfrequency_code: $scope.txteditauditfrequency_code,
                        auditfrequency_name: $scope.txteditaudit_frequency,
                        auditfrequency_gid: $scope.auditfrequency_gid
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

        $scope.Status_update = function (auditfrequency_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/statusauditfrequency.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                var params = {
                    auditfrequency_gid: auditfrequency_gid
                }
                var url = 'api/AtmMstAuditFrequency/EditAuditFrequency';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.auditfrequency_gid = resp.data.auditfrequency_gid
                    $scope.txtaudit_frequency = resp.data.auditfrequency_name;
                    $scope.rbo_status = resp.data.Status;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.update_status = function () {

                    var params = {
                        auditfrequency_name: $scope.txtaudit_frequency,
                        auditfrequency_gid: $scope.auditfrequency_gid,
                        remarks: $scope.txtremarks,
                        rbo_status: $scope.rbo_status

                    }
                    var url = 'api/AtmMstAuditFrequency/InactiveAuditFrequency';
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
                    auditfrequency_gid: auditfrequency_gid
                }

                var url = 'api/AtmMstAuditFrequency/AuditFrequencyInactiveLogview';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.auditfrequencyinactivelog_data = resp.data.auditfrequency_list;
                    unlockUI();
                });
            }
        }

        //$scope.deleteauditfrequency = function (auditfrequency_gid) {
        //    var params = {
        //        auditfrequency_gid: auditfrequency_gid
        //    }
        //    //var url = 'api/AtmMstAuditFrequency/DeleteAuditFrequency';
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
        $scope.deleteauditfrequency = function (auditfrequency_gid) {
                var params = {
                    auditfrequency_gid: auditfrequency_gid
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
                    var url = 'api/AtmMstAuditFrequency/DeleteAuditFrequency';
                    SocketService.getparams(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            activate();
                        }
                        else {
                            Notify.alert('Error Occurred While Deleting Audit Frequency !!!', {
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