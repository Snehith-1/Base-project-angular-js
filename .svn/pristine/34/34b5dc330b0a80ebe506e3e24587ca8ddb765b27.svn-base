(function () {
    'use strict';

    angular
        .module('angle')
        .controller('osdMstDepartmentManagementController', osdMstDepartmentManagementController);

    osdMstDepartmentManagementController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams'];

    function osdMstDepartmentManagementController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'osdMstDepartmentManagementController';


        activate();

        function activate() {
          
            var url = 'api/OsdMstDepartmentManagement/GetActivatedeptSummary';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.deptmasterlist = resp.data.acivatedept;
                
                unlockUI();
            });

          
        }
     
        // Add Code Starts
        $scope.popupactivatedept = function () {
            var modalInstance = $modal.open({
                templateUrl: '/activatedepartmentModalContent.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var url = 'api/OsdMstDepartmentManagement/GetDepartment';
                SocketService.get(url).then(function (resp) {
                    $scope.departmentList = resp.data.department;

                });
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.activityname = function (string) {
                    if (string.length >= 100) {
                        $scope.message = "Maximum 100 characters Length";
                    }
                    else {
                        $scope.message = ""
                    }
                }

             

                $scope.activatedeptSubmit = function () {
                    lockUI();
                    var params = {
                        department_gid: $scope.cbodepartment.department_gid,
                        department_name: $scope.cbodepartment.department_name,
                    }
                    var url = 'api/OsdMstDepartmentManagement/PostActivatedeptadd';
                    SocketService.post(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            $modalInstance.close('closed');
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            activate();
                            unlockUI();
                        }
                        else {
                            //$modalInstance.close('closed');
                            alert(resp.data.message, {
                                status: 'danger',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            activate();
                            unlockUI();
                        }
                    });
                }

            }
        }
        // Add Code Ends

        // Edit Code Starts
        $scope.edit = function (activedepartment_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/activatemanagementModaledit.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
             
                var params = {
                    activedepartment_gid: activedepartment_gid
                }
                var url = 'api/OsdMstDepartmentManagement/GetDepartmentEdit';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.departmentList = resp.data.department;

                });
                var url = 'api/OsdMstDepartmentManagement/GetActivatedeptView';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.activatedeptcodeedit = resp.data.department_code,
                    $scope.cbodepartmentedit = resp.data.department_gid;
                });
              
             
                $scope.ok = function () {
                    modalInstance.close('closed');
                };
                $scope.activatedeptUpdate = function () {
                    var departmentname;
                    var dept_index = $scope.departmentList.map(function (e) { return e.department_gid }).indexOf($scope.cbodepartmentedit);
                    if (dept_index == -1) { departmentname = ''; } else { departmentname = $scope.departmentList[dept_index].department_name; };

                    var params = {
                        activedepartment_gid: activedepartment_gid,
                        department_gid: $scope.cbodepartmentedit,
                        department_name: departmentname
                    }

                    var url = 'api/OsdMstDepartmentManagement/GetActivatedeptUpdate';
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
                            //$modalInstance.close('closed');
                            alert(resp.data.message, {
                                status: 'danger',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            activate();
                        }
                    });
                }
            }
        }
        // Edit Code Ends

        // Delete Code Starts
        $scope.delete = function (val) {
            var params = {
                activedepartment_gid: val
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
                    lockUI();
                    var url = "api/OsdMstDepartmentManagement/GetActivatedeptDelete";
                    SocketService.getparams(url, params).then(function (resp) {

                        if (resp.data.status == true) {
                            activate();
                            SweetAlert.swal('Deleted Successfully!');
                            unlockUI();
                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'danger',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            unlockUI();
                        }
                    });

                }

            });
        }
        // Delete Code Ends

        $scope.Status_update = function (activedepartment_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/departmentstatus.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    activedepartment_gid: activedepartment_gid
                }
                var url = 'api/OsdMstDepartmentManagement/GetActivatedeptView';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.rbo_status = resp.data.department_status;
                    $scope.department_name = resp.data.department_name;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.update_status = function () {

                    var params = {
                        activedepartment_gid: activedepartment_gid,
                        remarks: $scope.txtremarks,
                        departmentstatus: $scope.rbo_status

                    }
                    var url = 'api/OsdMstDepartmentManagement/Postdepartmentstatusupdate';
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
                                status: 'danger',
                                pos: 'top-center',
                                timeout: 3000
                            });
                        }
                        activate();
                    });

                  

                }

                var param = {
                    activedepartment_gid: activedepartment_gid
                }

                var url = 'api/OsdMstDepartmentManagement/DepartmentstatusHistory';
                lockUI();
                SocketService.getparams(url, param).then(function (resp) {
                    $scope.departmentstatusehistory = resp.data.departmentstatusehistory_list;
                    unlockUI();
                });

            }
        }



        
        $scope.assignmanager = function (activedepartment_gid) {
            lockUI();
            var modalInstance = $modal.open({
                templateUrl: '/assignmanagermodal.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    activedepartment_gid: activedepartment_gid
                }
                var url = 'api/OsdMstDepartmentManagement/GetActivatedeptView';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.rbo_status = resp.data.department_status;
                    $scope.department_name = resp.data.department_name;
                    $scope.department_gid = resp.data.department_gid;
                });
                var url = 'api/OsdMstDepartmentManagement/Employee';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.employee_list = resp.data.employeeasssign_list;
                });
                var url = 'api/OsdMstDepartmentManagement/Assignedemplyee';
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.manager_list = resp.data.managereasssigned_list;
                });
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.checkall = function (selected) {
                    angular.forEach($scope.employee_list, function (val) {
                        val.checked = selected;
                    });
                }
                $scope.assign = function () {
                    lockUI();
                    var employee_gid;
                    var employeelistGId = [];
                    angular.forEach($scope.employee_list, function (val) {

                        if (val.checked == true) {
                            var employeelist_gid = val.employee_gid;
                            employee_gid = val.employee_gid;
                            employeelistGId.push(employeelist_gid);
                        }

                    });

                    var params = {
                        activedepartment_gid: activedepartment_gid,
                        employeelist_gid: employeelistGId,
                        department_gid: $scope.department_gid,
                        department_name: $scope.department_name
                    }
                    unlockUI();
                    if (employee_gid != undefined) {
                        var url = 'api/OsdMstDepartmentManagement/AssignDeptmanager';
                        lockUI();
                        SocketService.post(url, params).then(function (resp) {

                            if (resp.data.status == true) {
                              
                                var params = {
                                    activedepartment_gid: activedepartment_gid
                                }
                                var url = 'api/OsdMstDepartmentManagement/Employee';
                                SocketService.getparams(url, params).then(function (resp) {
                                    $scope.employee_list = resp.data.employeeasssign_list;
                                });
                                var url = 'api/OsdMstDepartmentManagement/Assignedemplyee';
                                SocketService.getparams(url, params).then(function (resp) {
                                    $scope.manager_list = resp.data.managereasssigned_list;
                                });
                                unlockUI();
                                Notify.alert('Manager Assigned Successfully!', 'success');

                            }
                            else {
                                unlockUI();
                                Notify.alert(resp.data.message, 'warning');
                            }

                        });
                    }
                    else {
                        Notify.alert('Select Atleast One Employee!', 'warning')
                    }
                }

                $scope.unassign = function (activedepartment2manager_gid) {
                    lockUI();
                    var url = "api/OsdMstDepartmentManagement/GetAssignmangerDelete";
                    var params = {
                        activedepartment2manager_gid: activedepartment2manager_gid
                    };
                    SocketService.getparams(url, params).then(function (resp) {

                        if (resp.data.status == true) {
                          
                            var params = {
                                activedepartment_gid: activedepartment_gid
                            }
                            var url = 'api/OsdMstDepartmentManagement/Employee';
                            SocketService.getparams(url, params).then(function (resp) {
                                $scope.employee_list = resp.data.employeeasssign_list;
                            });
                            var url = 'api/OsdMstDepartmentManagement/Assignedemplyee';
                            SocketService.getparams(url, params).then(function (resp) {
                                $scope.manager_list = resp.data.managereasssigned_list;
                            });
                            unlockUI();
                            Notify.alert('Manager UnAssigned Successfully!', 'success');

                        }
                        else {
                            unlockUI();
                            Notify.alert(resp.data.message, 'warning');
                        }

                    });
                }

            }
        }

        $scope.assignmember = function (activedepartment_gid) {
            lockUI()
            var modalInstance = $modal.open({
                templateUrl: '/assignmembermodal.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    activedepartment_gid: activedepartment_gid
                }
                var url = 'api/OsdMstDepartmentManagement/GetActivatedeptView';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.rbo_status = resp.data.department_status;
                    $scope.department_name = resp.data.department_name;
                    $scope.department_gid = resp.data.department_gid;
                });
                var url = 'api/OsdMstDepartmentManagement/MemberEmployee';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.employee_list = resp.data.employeeasssign_list;
                });
                var url = 'api/OsdMstDepartmentManagement/Assignedmemberemplyee';
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.member_list = resp.data.membereasssigned_list;
                });
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.checkall = function (selected) {
                    angular.forEach($scope.employee_list, function (val) {
                        val.checked = selected;
                    });
                }
                $scope.assign = function () {
                    lockUI();
                    var employee_gid;
                    var employeelistGId = [];
                    angular.forEach($scope.employee_list, function (val) {

                        if (val.checked == true) {
                            var employeelist_gid = val.employee_gid;
                            employee_gid = val.employee_gid;
                            employeelistGId.push(employeelist_gid);
                        }

                    });

                    var params = {
                        activedepartment_gid: activedepartment_gid,
                        employeelist_gid: employeelistGId,
                        department_gid: $scope.department_gid,
                        department_name: $scope.department_name
                    }
                    unlockUI();
                    if (employee_gid != undefined) {
                        var url = 'api/OsdMstDepartmentManagement/AssignDeptmember';
                        lockUI();
                        SocketService.post(url, params).then(function (resp) {

                            if (resp.data.status == true) {
                              
                                var params = {
                                    activedepartment_gid: activedepartment_gid
                                }
                                var url = 'api/OsdMstDepartmentManagement/MemberEmployee';
                                SocketService.getparams(url, params).then(function (resp) {
                                    $scope.employee_list = resp.data.employeeasssign_list;
                                });
                                var url = 'api/OsdMstDepartmentManagement/Assignedmemberemplyee';
                                SocketService.getparams(url, params).then(function (resp) {
                                    $scope.member_list = resp.data.membereasssigned_list;
                                });
                                unlockUI();
                                Notify.alert('Member Assigned Successfully!', 'success');

                            }
                            else {
                                unlockUI();
                                Notify.alert(resp.data.message, 'warning');
                            }

                        });
                    }
                    else {
                        Notify.alert('Select Atleast One Employee!', 'warning')
                    }
                }

                $scope.unassign = function (activedepartment2member_gid) {
                    lockUI();
                    var url = "api/OsdMstDepartmentManagement/GetAssignmemberDelete";
                    var params = {
                        activedepartment2member_gid: activedepartment2member_gid
                    };
                    SocketService.getparams(url, params).then(function (resp) {

                        if (resp.data.status == true) {
                           
                            var params = {
                                activedepartment_gid: activedepartment_gid
                            }
                            var url = 'api/OsdMstDepartmentManagement/MemberEmployee';
                            SocketService.getparams(url, params).then(function (resp) {
                                $scope.employee_list = resp.data.employeeasssign_list;
                            });
                            var url = 'api/OsdMstDepartmentManagement/Assignedmemberemplyee';
                            SocketService.getparams(url, params).then(function (resp) {
                                $scope.member_list = resp.data.membereasssigned_list;
                            });
                            unlockUI();
                            Notify.alert('Member UnAssigned Successfully!', 'success');

                        }
                        else {
                            unlockUI();
                            Notify.alert(resp.data.message, 'warning');
                        }

                    });
                }

            }
        }

       
        $scope.showPopovermanager = function (activedepartment_gid) {
            lockUI();
            var modalInstance = $modal.open({
                templateUrl: '/showpopupmanagerModal.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    activedepartment_gid: activedepartment_gid
                }
               
                var url = 'api/OsdMstDepartmentManagement/Assignedemplyee';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.manager_list = resp.data.managereasssigned_list;
                });
                var url = 'api/OsdMstDepartmentManagement/GetActivatedeptView';
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.department_code = resp.data.department_code,
                    $scope.department_name = resp.data.department_name;
                });
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
            }
        }

        $scope.showPopovermember = function (activedepartment_gid) {
            lockUI();
            var modalInstance = $modal.open({
                templateUrl: '/showpopupmemberModal.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    activedepartment_gid: activedepartment_gid
                }

                var url = 'api/OsdMstDepartmentManagement/Assignedmemberemplyee';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.member_list = resp.data.membereasssigned_list;
                });
                var url = 'api/OsdMstDepartmentManagement/GetActivatedeptView';
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.department_code = resp.data.department_code,
                    $scope.department_name = resp.data.department_name;
                });
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
            }
        }

    }
})();
