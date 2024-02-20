(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AtmMstAuditTypeController', AtmMstAuditTypeController);

    AtmMstAuditTypeController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams'];

    function AtmMstAuditTypeController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AtmMstAuditTypeController';
        activate();


        function activate() {

            var url = 'api/AtmMstAuditType/GetAuditType';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.audittype_data = resp.data.audittype_list;
                unlockUI();
            });
        }
       
        $scope.popupaudittype = function () {
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

                $scope.audittypeSubmit = function () {
                    var params = {
                        audittype_gid: $scope.audittype_gid,
                        audittype_name: $scope.txtaudit_type,
                        audittype_code: $scope.txtaudittype_code,
                        lms_code: $scope.txtlms_code,
                        bureau_code: $scope.txtbureau_code
                    }

                    var url = 'api/AtmMstAuditType/CreateAuditType';
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

        $scope.editaudittype = function (audittype_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/editaudittype.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                var params = {
                    audittype_gid: audittype_gid
                }
                var url = 'api/AtmMstAuditType/EditAuditType';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.txteditaudittype_code = resp.data.audittype_code;
                    $scope.txteditlms_code = resp.data.lms_code;
                    $scope.txteditbureau_code = resp.data.bureau_code;
                    $scope.txteditaudit_type = resp.data.audittype_name;                  
                    $scope.audittype_gid = resp.data.audittype_gid;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.audittypeUpdate = function () {

                    var url = 'api/AtmMstAuditType/UpdateAuditType';
                    var params = {
                        lms_code: $scope.txteditlms_code,
                        bureau_code: $scope.txteditbureau_code,
                        audittype_code: $scope.txteditaudittype_code,
                        audittype_name: $scope.txteditaudit_type,                      
                        audittype_gid: $scope.audittype_gid
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

        $scope.Status_update = function (audittype_gid) {
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
                    audittype_gid: audittype_gid
                }
                var url = 'api/AtmMstAuditType/EditAuditType';
                SocketService.getparams(url,params).then(function (resp) {
                    $scope.audittype_gid = resp.data.audittype_gid
                    $scope.txtaudit_type = resp.data.audittype_name;
                    $scope.rbo_status = resp.data.Status;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.update_status = function () {

                    var params = {
                        audittype_name: $scope.txtaudit_type,
                        audittype_gid: $scope.audittype_gid,
                        remarks: $scope.txtremarks,
                        rbo_status: $scope.rbo_status

                    }
                    var url = 'api/AtmMstAuditType/InactiveAuditType';
                    lockUI();
                    SocketService.post(url,params).then(function (resp) {
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
                    audittype_gid: audittype_gid
                }

               var url = 'api/AtmMstAuditType/AuditTypeInactiveLogview';
                lockUI();
                SocketService.getparams(url,params).then(function (resp) {
                    $scope.audittypeinactivelog_data = resp.data.audittype_list;
                    unlockUI();
                });
            }
        }

        //$scope.deleteaudittype = function (audittype_gid) {
        //    var params = {
        //        audittype_gid: audittype_gid
        //    }
        //    var url = 'api/AtmMstAuditType/DeleteAuditType'; 
        //    SocketService.getparams(url,params).then(function (resp) {
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
            $scope.deleteaudittype = function (audittype_gid) {
                var params = {
                    audittype_gid: audittype_gid
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
                    var url = 'api/AtmMstAuditType/DeleteAuditType';
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