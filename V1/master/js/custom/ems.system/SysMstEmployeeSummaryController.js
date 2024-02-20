(function () {
    'use strict';

    angular
        .module('angle')
        .controller('SysMstEmployeeSummaryController', SysMstEmployeeSummaryController);

    SysMstEmployeeSummaryController.$inject = ['$rootScope', '$modal','$scope', '$state', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route','ngDialog', 'SweetAlert'];

    function SysMstEmployeeSummaryController($rootScope, $modal, $scope, $state, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngDialog, SweetAlert) {

        activate();
        var vm = this;
        vm.title = 'SysMstEmployeeSummaryController';
        $scope.lsoneapipage = $location.search().lsoneapipage;
        var lsoneapipage = $scope.lsoneapipage;

        function activate() {

            var url = 'api/ManageEmployee/EmployeeSummary';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.employeelist = resp.data.employee;
                unlockUI();
            });



        }

        $scope.employee_edit = function (employee_gid) {
            $location.url('app/SysMstEmployeeEdit?employee_gid=' + employee_gid);

        };

        $scope.employee_view = function (employee_gid) {
            $location.url('app/SysMstEmployeeView?employee_gid=' + employee_gid);
        }

            $scope.employee_add = function () {
                $state.go('app.SysMstEmployeeAdd');
            };


            $scope.employee_deactive = function (employee_gid) {
                $location.url('app/SysMstEmployeeDeactivate?employee_gid=' + employee_gid);
            };

            $scope.employee_resetpwd = function (employee_gid) {
                var modalInstance = $modal.open({
                    templateUrl: '/empresetpassowrd.html',
                    controller: ModalInstanceCtrl,
                    size: 'md'
                });
                ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
                function ModalInstanceCtrl($scope, $modalInstance) {
                    var params = {
                        employee_gid: employee_gid,
                    }
                    var url = 'api/ManageEmployee/ResetPswdEdit';
                    SocketService.getparams(url, params).then(function (resp) {

                        $scope.employee_code = resp.data.user_code;
                        $scope.employee_name = resp.data.employee_name;
                        $scope.user_gid = resp.data.user_gid;
                    });

                    $scope.ok = function () {
                        $modalInstance.close('closed');
                    };

                    $scope.passwordSubmit = function () {
                        var params = {
                            user_password: $scope.Password,
                            user_gid: $scope.user_gid
                        }
                        console.log(params);
                        var url = 'api/ManageEmployee/PasswordUpdate';
                        SocketService.post(url, params).then(function (resp) {
                            if (resp.data.status == true) {

                                activate();
                                $modalInstance.close('closed');
                                Notify.alert(resp.data.message, 'success')

                            }
                            else {

                                Notify.alert(resp.data.message, 'warning')
                                activate();

                            }
                        });
                    }
                }
            }


            $scope.employee_updatecode = function (employee_gid) {
                var modalInstance = $modal.open({
                    templateUrl: '/empcodeupdate.html',
                    controller: ModalInstanceCtrl,
                    size: 'md'
                });
                ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
                function ModalInstanceCtrl($scope, $modalInstance) {
                    var params = {
                        employee_gid: employee_gid,
                    }
                    var url = 'api/ManageEmployee/ResetPswdEdit';
                    SocketService.getparams(url, params).then(function (resp) {

                        $scope.employee_code = resp.data.user_code;
                        $scope.employee_name = resp.data.employee_name;
                        $scope.user_gid = resp.data.user_gid;
                    });

                    $scope.ok = function () {
                        $modalInstance.close('closed');
                    };

                    $scope.passwordSubmit = function () {
                        var params = {
                            user_code: $scope.newemp_code,
                            user_gid: $scope.user_gid
                        }
                        console.log(params);
                        var url = 'api/ManageEmployee/UserCodeUpdate';
                        SocketService.post(url, params).then(function (resp) {
                            if (resp.data.status == true) {

                                activate();
                                $modalInstance.close('closed');
                                Notify.alert(resp.data.message, 'success')

                            }
                            else {

                                Notify.alert(resp.data.message, 'warning')
                                $modalInstance.close('closed');
                                activate();

                            }
                        });
                    }
                }
            }

            /* Employee Active */


            $scope.employee_active = function (employee_gid) {
                $scope.employee_gid = employee_gid;
                $scope.employee_gid = localStorage.setItem('employee_gid', employee_gid);
                SweetAlert.swal({
                    title: 'Are you sure ?',
                    text: 'Do You Want To Activate ?',
                    showCancelButton: true,
                    confirmButtonColor: '#DD6B55',
                    confirmButtonText: 'Yes, Activate it!',
                    closeOnConfirm: false
                }, function (isConfirm) {
                    if (isConfirm) {
                        var url = 'api/ManageEmployee/EmployeeActivate';
                        $scope.employee_gid = localStorage.getItem('employee_gid');
                        var param = {
                            employee_gid: $scope.employee_gid
                        };
                        lockUI();
                        SocketService.getparams(url, param).then(function (resp) {
                            if (resp.data.status == true) {
                                SweetAlert.swal('Activated Successfully!');
                                activate();
                            }
                            else {
                                Notify.alert(resp.data.message, {
                                    status: 'warning',
                                    pos: 'top-center',
                                    timeout: 4000
                                });
                                activate();
                                unlockUI();
                            }

                        });

                    }

                });
            };

        }
    
    
})();
