(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AtmMstAuditMappingAddController',AtmMstAuditMappingAddController );

        AtmMstAuditMappingAddController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function AtmMstAuditMappingAddController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AtmMstAuditMappingAddController';

        activate();

        function activate() {
            var url = 'api/AtmMstAuditMapping/GetAuditMapping';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.auditmapping_list = resp.data.auditmapping_list;
                unlockUI();
            });
           
           
        }

        $scope.auditmappingadd = function () {
            var modalInstance = $modal.open({
                templateUrl: '/addauditmapping.html',
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

                var url = 'api/SystemMaster/GetEmployeelist';
                lockUI();
                SocketService.get(url).then(function (resp) {
                    $scope.employee_list = resp.data.employeelist;
                    unlockUI();
                });

                $scope.submit = function () {

                    var params = {
                        //employee_gid: $scope.employee_gid,
                        employee: $scope.cboemployee,
                        auditmapping_name: $scope.audit_mapping,
                        auditmapping_code: $scope.txtauditmapping_code,
                        lms_code: $scope.txtlms_code,
                        bureau_code: $scope.txtbureau_code
                    }

                    var url = 'api/AtmMstAuditMapping/CreateAuditMapping';
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
        $scope.editauditmapping = function (auditmapping_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/editauditmapping.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });

            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                
                var params = {
                    auditmapping_gid: auditmapping_gid
                }
                var url = 'api/AtmMstAuditMapping/EditAuditMapping';
                SocketService.getparams(url, params).then(function (resp) {
                   
                   
                    $scope.txteditauditmapping_code = resp.data.auditmapping_code;
                    $scope.txteditlms_code = resp.data.lms_code;
                    $scope.txteditbureau_code = resp.data.bureau_code;
                    $scope.audit_mapping = resp.data.auditmapping_name;
                    $scope.auditmapping_gid = resp.data.auditmapping_gid;
                    $scope.cboemployee_editlist = resp.data.employeeem_list;
                    $scope.employee = resp.data.employee;
                    $scope.cboemployee_edit = [];
                    if (resp.data.employee != null) {
                        var count = resp.data.employee.length;
                        for (var i = 0; i < count; i++) {
                            var workerIndex = $scope.cboemployee_editlist.map(function (x) { return x.employee_gid; }).indexOf(resp.data.employee[i].employee_gid);
                            //var indexs = $scope.cboemployee_editlist.findIndex(x => x.employee_gid === resp.data.employee[i].employee_gid);
                            $scope.cboemployee_edit.push($scope.cboemployee_editlist[workerIndex]);
                            $scope.$parent.cboSecondaryValueChain = $scope.cboSecondaryValueChain;
                        }
                    }
               
                });


                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.update = function () {
                    
                    //var employeename;
                    //var employee_index = $scope.employee_list.map(function (e) { return e.employee_gid }).indexOf($scope.cboemployeeedit);
                    //if (employee_index == -1) { employeename = ''; } else { employeename = $scope.employee_list[employee_index].employee_name; };

                    var url = 'api/AtmMstAuditMapping/UpdateAuditMapping';
                    var params = {
                        auditmapping_code: $scope.txteditauditmapping_code,
                        auditmapping_name: $scope.audit_mapping,
                        lms_code: $scope.txteditlms_code,
                        bureau_code: $scope.txteditbureau_code,
                        auditmapping_gid: $scope.auditmapping_gid,
                        employee: $scope.cboemployee_edit,
                        
                       
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
        $scope.showPopover = function (auditmapping_gid, auditmapping_name) {
            var modalInstance = $modal.open({
                templateUrl: '/showemployee.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    auditmapping_gid: auditmapping_gid
                }
                lockUI();
                var url = 'api/AtmMstAuditMapping/GetEmployeeName';
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();                  
                    $scope.employee_name = resp.data.employee_name;
                    $scope.auditmapping_name = resp.data.auditmapping_name;
                });
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
            }
        }
        $scope.Status_update = function (auditmapping_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/statusauditmapping.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                var params = {
                    auditmapping_gid: auditmapping_gid
                }
                var url = 'api/AtmMstAuditMapping/EditAuditMapping';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.auditmapping_gid = resp.data.auditmapping_gid
                    $scope.audit_mapping = resp.data.auditmapping_name;
                    $scope.rbo_status = resp.data.Status;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.update_status = function () {

                    var params = {
                        auditmapping_gid: auditmapping_gid,
                        auditmapping_name: $scope.txtauditmapping_name,
                        remarks: $scope.txtremarks,
                        rbo_status: $scope.rbo_status

                    }
                    var url = 'api/AtmMstAuditMapping/InactiveAuditMapping';
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

                var param = {
                    auditmapping_gid: auditmapping_gid
                }

                var url = 'api/AtmMstAuditMapping/AuditMappingInactiveLogview';
                lockUI();
                SocketService.getparams(url, param).then(function (resp) {
                    $scope.auditmappinginactivelog_list = resp.data.auditmapping_list;
                    unlockUI();
                });

            }
        }

        $scope.deleteauditmapping = function (auditmapping_gid) {
            var params = {
                auditmapping_gid: auditmapping_gid
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
                   
                    var url = 'api/AtmMstAuditMapping/DeleteAuditMapping';
                    SocketService.getparams(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            activate();
                        }
                        else {
                            Notify.alert('Error Occurred While Deleting Audit Mapping !!!', {
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