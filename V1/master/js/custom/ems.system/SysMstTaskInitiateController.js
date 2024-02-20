(function () {
    'use strict';

    angular
        .module('angle')
        .controller('SysMstTaskInitiateController', SysMstTaskInitiateController);

        SysMstTaskInitiateController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function SysMstTaskInitiateController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'SysMstTaskInitiateController';
        var employee_gid = $location.search().employee_gid;
        var lstab = $location.search().lstab;
        $scope.lstab = lstab;
        // var user_gid = $location.search().user_gid;
        lockUI();
        activate();
        function activate() {
           

            var param = {
                employee_gid: employee_gid
            };
            // var url = 'api/ManageEmployee/DeleteTaskInitiatedUnsaved';
            // SocketService.getparams(url, param).then(function (resp) {
            // unlockUI();
            // });
            var url = 'api/ManageEmployee/EmployeePendingEditView';
            lockUI();
            SocketService.getparams(url, param).then(function (resp) {
                $scope.employee_details = resp.data;
                unlockUI();
            });
           

            var url = 'api/ManageEmployee/GetEmployeeSubmit';
            SocketService.getparams(url, param).then(function (resp) {
                unlockUI();
              $scope.initiate_flag = resp.data.initiate_flag;
             
            });

            var url = 'api/ManageEmployee/GetTaskSummary';
            SocketService.getparams(url, param).then(function (resp) {
                unlockUI();
              $scope.tasksummarylist = resp.data.tasksummarylist;
            });

            var url = 'api/ManageEmployee/GetTaskList';
            SocketService.getparams(url, param).then(function (resp) {
                unlockUI();
              $scope.task_list = resp.data.tasklists;
            });
           
            var url = 'api/ManageEmployee/EmployeePendingSummary';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.employeepending_list = resp.data.employee;
                unlockUI();
            });

            var url = 'api/ManageEmployee/DeleteTaskInitiatedUnsaved';
            SocketService.get(url).then(function (resp) {
            unlockUI();
            });

            // var url = 'api/ManageEmployee/GetTempTaskList';
            // SocketService.getparams(url, param).then(function (resp) {
            // unlockUI();
            // $scope.taskinitiate_gid = resp.data.taskinitiate_gid;
                          
            //  });
        }
        
        function initiateactivate() {

            
            var param = {
                employee_gid: employee_gid
            };
           
            var url = 'api/ManageEmployee/GetEmployeeSubmit';
            SocketService.getparams(url, param).then(function (resp) {
                unlockUI();
              $scope.initiate_flag = resp.data.initiate_flag;
             
            });

            var url = 'api/ManageEmployee/GetTempTaskList';
            SocketService.getparams(url, param).then(function (resp) {
            unlockUI();
            $scope.tasksummarylist = resp.data.tasksummarylist;                         
            });

            var url = 'api/ManageEmployee/GetTaskList';
            SocketService.getparams(url, param).then(function (resp) {
                unlockUI();
              $scope.task_list = resp.data.tasklists;
            });
        }
      
   
        $scope.addtask = function () {
            if (($scope.cbotaskassign == undefined) ||  ($scope.cbotaskassign == null) ||  ($scope.cbotaskassign == '') ||  ($scope.txttask_remarks == undefined) ||  ($scope.txttask_remarks == null) ||  ($scope.txttask_remarks == '')) {
                Notify.alert('Select Task / Enter Remarks','warning');
            }
            else {
               var params = {
                    task_gid: $scope.cbotaskassign.task_gid,
                    task_name: $scope.cbotaskassign.task_name,               
                    task_remarks: $scope.txttask_remarks,
                    employee_gid : employee_gid
               }
                  var url = 'api/ManageEmployee/PostTask';
                lockUI();
                SocketService.post(url, params).then(function (resp) {
                    unlockUI();
                    if (resp.data.status == true) {
                        // $scope.tasksummary_list = resp.data.tasksummary_list;
                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        initiateactivate();
                        // var param = {
                        //     employee_gid: employee_gid
                        // };
                        // var url = 'api/ManageEmployee/GetTempTaskList';
                        // SocketService.getparams(url, param).then(function (resp) {
                        //     unlockUI();
                        //   $scope.tasksummarylist = resp.data.tasksummarylist;
                          
                        // });
                       
                      
                    }
                    else {
                        Notify.alert(resp.data.message, {
                            status: 'warning',
                            pos: 'top-center',
                            timeout: 3000
                        });
                    }
                     $scope.cbotaskassign = '';
                     $scope.txttask_remarks = '';
                   
                }); 
            }
        }

        $scope.back = function () {
            if (lstab == 'pending') {
                $location.url('app/SysMstEmployeePendingSummary');
            }
            else if (lstab == 'active') {
                $state.go('app.SysMstEmployeeActiveUserSummary');
            }           
            else {
                 
            }
        };

        $scope.initiatetask = function (taskinitiate_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/getReApprovalmodal.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params =
                {
                    taskinitiate_gid: taskinitiate_gid,
                    employee_gid: employee_gid

                }
                var url = 'api/ManageEmployee/GetTaskInitiate';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                   
                    $scope.lbltask_name = resp.data.task_name;
                    $scope.task_gid = resp.data.task_gid;                                  

                });
               
                $scope.ok = function () {
                    modalInstance.close('closed');
                };

                $scope.getreapprovalclick = function () {
                    var params = {
                        task_name: $scope.lbltask_name,
                        task_gid: $scope.task_gid,                     
                        taskinitiate_gid: taskinitiate_gid,
                        employee_gid: employee_gid
                       
                    }
                    lockUI();
                    var url = "api/ManageEmployee/InitiateTask";
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {
                            modalInstance.close('closed');
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            $state.go('app.SysMstTaskInitiate');
                            // activate();
                            initiateactivate();
                        }
                        else {
                            modalInstance.close('closed');
                            Notify.alert(resp.data.message, {
                                status: 'danger',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            unlockUI();

                        }
                    });
                }
            }
        }

        $scope.canceltask = function (taskinitiate_gid) {
            var params = {
                taskinitiate_gid: taskinitiate_gid,
                employee_gid: employee_gid
            }
            SweetAlert.swal({
                title: 'Are you sure?',
                text: 'Do You Want To Cancel the Approval Initiated ?',
                showCancelButton: true,
                cancelButtonText: 'Close',
                confirmButtonColor: '#DD6B55',
                confirmButtonText: 'Yes, Cancel it!',
                closeOnConfirm: false
            }, function (isConfirm) {
                if (isConfirm) {
                    var url = "api/ManageEmployee/CancelTaskInitiate"
                    lockUI();
                    SocketService.getparams(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                           
                            unlockUI();
                            initiateactivate();
                            // activate();
                            
                            // $state.go('app.MstStartScheduledMeeting');
                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'danger',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            unlockUI();
                            // activate();
                        }
                    });
                    SweetAlert.swal('Cancelled Successfully!');
                }

            });
        };

        $scope.task_delete = function (taskinitiate_gid) {
            var params = {
                taskinitiate_gid: taskinitiate_gid
            }
            var url = 'api/ManageEmployee/DeleteTaskInitiated';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {

                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    initiateactivate();
                   
                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'warning',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }


            });
        }

        $scope.submit = function (val2) {
            // if (($scope.employee == undefined) ||  ($scope.employee == null)) {
            //     Notify.alert('Add At Lest One Task','warning');
            // }
            // else {
               var params = {
                   
                    employee_gid : employee_gid
               }
                  var url = 'api/ManageEmployee/PostOverallTask';
                lockUI();
                SocketService.post(url, params).then(function (resp) {
                    unlockUI();
                    if (resp.data.status == true) {
                        // $scope.tasksummary_list = resp.data.tasksummary_list;
                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });

                        if (lstab == 'pending') {
                            $location.url('app/SysMstEmployeePendingSummary');
                        }
                        else if (lstab == 'active') {
                            $state.go('app.SysMstEmployeeActiveUserSummary');
                        }           
                        else {
                             
                        }
                    }
                    else {
                        Notify.alert(resp.data.message, {
                            status: 'warning',
                            pos: 'top-center',
                            timeout: 3000
                        });
                    }
                    // $scope.cboGeneticCode = '';
                    // $scope.txtgenetic_remarks = '';
                   
                }); 
            // }
        }
        $scope.completedremarks= function (task_completeremarks){
            var modalInstance = $modal.open({
                templateUrl: '/completedremarks.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.task_completeremarks=task_completeremarks;
                $scope.back = function () {
                    $modalInstance.close('closed');
                }; 
            }
        }

    }
})();