(function () {
    'use strict';

    angular
        .module('angle')
        .controller('SysMstTaskController', SysMstTaskController);

        SysMstTaskController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function SysMstTaskController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'SysMstTaskController';
        $scope.lsoneapipage = $location.search().lsoneapipage;
        var lsoneapipage = $scope.lsoneapipage;

        activate();

        function activate() {
            var url = 'api/SystemMaster/GetTaskSummary';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.task_list = resp.data.master_list;
                unlockUI();
            });
        }


        $scope.addtask = function () {
            $location.url('app/SysMstTaskAdd');
        }
       
        $scope.edittask = function (task_gid) {
            $location.url('app/SysMstTaskEdit?lstask_gid=' + task_gid);
        }

        $scope.viewtask = function (task_gid) {
            $location.url('app/SysMstTaskView?lstask_gid=' + task_gid);
        }

        $scope.viewprogram = function (program_gid) {
            $location.url('app/MstProgramView?lsprogram_gid=' + program_gid);
        }
        
       
        $scope.Status_update = function (task_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/statustask.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                var params = {
                    task_gid: task_gid
                }
                var url = 'api/SystemMaster/EditTask';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.task_gid = resp.data.task_gid
                    $scope.txttask_name = resp.data.task_name;
                    $scope.rbo_status = resp.data.Status;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.update_status = function () {

                    var params = {
                        task_gid: task_gid,
                        task_name: $scope.txttask_name,
                        remarks: $scope.txtremarks,
                        rbo_status: $scope.rbo_status

                    }
                    var url = 'api/SystemMaster/InactiveTask';
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
                    task_gid: task_gid
                }

                var url = 'api/SystemMaster/TaskInactiveLogview';
                lockUI();
                SocketService.getparams(url, param).then(function (resp) {
                    $scope.taskinactivelog_list = resp.data.master_list;
                    unlockUI();
                });

            }
        }

        $scope.deletetask = function (task_gid) {
            var params = {
                task_gid: task_gid
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
                    var url = 'api/SystemMaster/DeleteTask';
                    SocketService.getparams(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            SweetAlert.swal('Deleted Successfully!');
                            // Notify.alert(resp.data.message, {
                            //     status: 'success',
                            //     pos: 'top-center',
                            //     timeout: 3000
                            // });
                            activate();
                        }
                        else {
                            alert(resp.data.message, {
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

