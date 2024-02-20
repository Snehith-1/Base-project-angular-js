(function () {
    'use strict';

    angular
        .module('angle')
        .controller('SysMstEmployeeRelievingController', SysMstEmployeeRelievingController);

        SysMstEmployeeRelievingController.$inject = ['$rootScope', '$modal','$scope', '$state', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route','ngDialog', 'SweetAlert','DownloaddocumentService'];

    function SysMstEmployeeRelievingController($rootScope, $modal, $scope, $state, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngDialog, SweetAlert, DownloaddocumentService) {

        activate();
        var vm = this;
        vm.title = 'SysMstEmployeeRelievingController';
        var lstab='relieving';

        function activate() {
            
            var url = 'api/ManageEmployee/EmployeeRelievedSummary';
            lockUI();
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.employeerelieving_list = resp.data.employee;
                
            });

            $scope.today = new Date();
            var today = $scope.today;
            var checktoday = new Date();
            checktoday = ("0" + today.getDate()+1).slice(-2) + '-' + ("0" + (today.getMonth() + 1)).slice(-2) + '-' + today.getFullYear();
            $scope.checktoday = checktoday;
            var checktodaynew = new Date();
            checktodaynew = ("0" + today.getDate()).slice(-2) + '-' + ("0" + (today.getMonth() + 1)).slice(-2) + '-' + today.getFullYear();
            $scope.checktodaynew = checktodaynew;
        }

        

        $scope.employee_view = function (employee_gid) {
            $location.url('app/SysMstEmployeeView?employee_gid=' + employee_gid + '&lstab=' + lstab);
        }
        $scope.pendingSummary = function () {
            $location.url('app/SysMstEmployeePendingSummary');
        }
        $scope.ActiveSummary = function () {
            $location.url('app/SysMstEmployeeActiveUserSummary');
        }
        $scope.InactiveSummary = function () {
            $location.url('app/SysMstEmployeeInactiveSummary');
        }
        
        $scope.RelieveingSummary = function () {
            $location.url('app/SysMstEmployeeRelievingSummary');
        }

        $scope.employee_add = function () {
            $location.url('app/SysMstEmployeeAdd?lstab=' + lstab);
            };


            $scope.employee_deactive = function (employee_gid) {
                $location.url('app/SysMstEmployeeDeactivate?employee_gid=' + employee_gid + '&lstab=' + lstab);
            };
            $scope.employee_history = function (employee_gid) {
                $location.url('app/SysMstEmployeeHistory?employee_gid=' + employee_gid + '&lstab=' + lstab);
            };

            
            $scope.relive_edit = function (employee_gid) {
                var modalInstance = $modal.open({
                    templateUrl: '/relivingdetails.html',
                    controller: ModalInstanceCtrl,
                    backdrop: 'static',
                    keyboard: false,
                    size: 'md'
                });
                ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
                function ModalInstanceCtrl($scope, $modalInstance) {
                   
                    var params = {
                        employee_gid: employee_gid,
                    }
                    var url = 'api/ManageEmployee/EmployeeRelievingView';
                    SocketService.getparams(url, params).then(function (resp) {

                        $scope.txtrelivereason = resp.data.remarks;
                        $scope.txtrelive_date = resp.data.relive_date;
                    });
                    $scope.reliveSubmit = function () {
                        var params = {
                            employee_gid: employee_gid,
                            remarks :$scope.txtrelivereason,
                            relive_date:$scope.txtrelive_date
                        }
                        console.log(params);
                        var url = 'api/ManageEmployee/EmployeeRelievingEdit';
                        SocketService.post(url, params).then(function (resp) {
                            if (resp.data.status == true) {
                                activate();
                                $modalInstance.close('closed');
                                Notify.alert(resp.data.message, 'success')
                                $location.url('app/SysMstEmployeeRelievingSummary');
                            }
                            else {
                                Notify.alert(resp.data.message, 'warning')
                                $modalInstance.close('closed');
                                activate();
                            }
                        });
                    }
    
                    $scope.ok = function () {
                        $modalInstance.close('closed');
                    };
    
                    $scope.calender03 = function ($event) {
                        $event.preventDefault();
                        $event.stopPropagation();
    
                        $scope.open03 = true;
                    };
                    $scope.formats = ['dd-MM-yyyy'];
                    $scope.format = $scope.formats[0];
                    $scope.dateOptions = {
                        formatYear: 'yy',
                        startingDay: 1
                    };
                }
            } 

            $scope.employee_updatecode = function (employee_gid) {
                var modalInstance = $modal.open({
                    templateUrl: '/empcodeupdate.html',
                    controller: ModalInstanceCtrl,
                    backdrop: 'static',
                    keyboard: false,
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

        $scope.exportemployee = function () {
            lockUI();
            var url = 'api/ManageEmployee/EmployeeExport';
            SocketService.get(url).then(function (resp) {
                if (resp.data.status == true) {
                    unlockUI();
                    DownloaddocumentService.Downloaddocument(resp.data.lspath, resp.data.lsname); 
                    // var phyPath = resp.data.lspath;
                    // var relPath = phyPath.split("EMS");
                    // var relpath1 = relPath[1].replace("\\", "/");
                    // var hosts = window.location.host;
                    // var prefix = location.protocol + "//";
                    // var str = prefix.concat(hosts, relpath1);
                    // var link = document.createElement("a");
                    // var name = resp.data.lsname.split('.');
                    // link.download = name[0];
                    // var uri = str;
                    // link.href = uri;
                    // link.click();
                }
                else {
                    unlockUI();
                    Notify.alert('Error Occurred While Export !')
                }

            });
        }
        // $scope.employee_history = function (employee_gid) {
        //     var modalInstance = $modal.open({
        //         templateUrl: '/employee_history.html',
        //         controller: ModalInstanceCtrl,
        //         backdrop: 'static',
        //         keyboard: false,
        //         size: 'md'
        //     });
        //     ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
        //     function ModalInstanceCtrl($scope, $modalInstance) {
        //         var params = {
        //             employee_gid: employee_gid,
        //         }
        //         var url = 'api/ManageEmployee/ResetPswdEdit';
        //         lockUI();
        //         SocketService.getparams(url, params).then(function (resp) {
        //         unlockUI();
        //             $scope.employeerelieving_list = resp.data.employee;
        //         });
        //         $scope.ok = function () {
        //             $modalInstance.close('closed');
        //         };
        //     }
        // }
    }  
})();
