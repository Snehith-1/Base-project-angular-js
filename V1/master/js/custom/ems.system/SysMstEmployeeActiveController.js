(function () {
    'use strict';

    angular
        .module('angle')
        .controller('SysMstEmployeeactiveController', SysMstEmployeeactiveController);

    SysMstEmployeeactiveController.$inject = ['$rootScope', '$modal', '$scope', '$state', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngDialog', 'SweetAlert', 'DownloaddocumentService'];

    function SysMstEmployeeactiveController($rootScope, $modal, $scope, $state, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngDialog, SweetAlert, DownloaddocumentService) {

        activate();
        var vm = this;
        vm.title = 'SysMstEmployeeactiveController';
        var lstab = 'active';

        function activate() {

            var url = 'api/ManageEmployee/EmployeeActiveSummary';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.employeeactive_list = resp.data.employee;
                unlockUI();
            });

        }

        $scope.employee_edit = function (employee_gid) {
            $location.url('app/SysMstEmployeeEdit?employee_gid=' + employee_gid + '&lstab=' + lstab);
        };

        $scope.assigntask = function (employee_gid) {
            $location.url('app/SysMstTaskInitiate?employee_gid=' + employee_gid + '&lstab=' + lstab);
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

        $scope.hrdoc = function (employee_gid) {
            $location.url('app/SysMstEmployeeHRDocument?employee_gid=' + employee_gid + '&lstab=' + lstab);
        };

        $scope.employee_resetpwd = function (employee_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/empresetpassowrd.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
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
                    $scope.txtentity = resp.data.entity_gid;
                    $scope.employee_code = resp.data.user_code;
                    $scope.employee_name = resp.data.employee_name;
                    $scope.user_gid = resp.data.user_gid;
                });

                var url = 'api/ManageEmployee/PopEntityActive';
                SocketService.get(url).then(function (resp) {

                    $scope.entityList = resp.data.entity;

                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.passwordSubmit = function () {
                    var params = {
                        entity_gid: $scope.txtentity, 
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
        $scope.relive = function (employee_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/relivingdetails.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {


                $scope.reliveSubmit = function () {
                    var params = {
                        employee_gid: employee_gid,
                        remarks: $scope.txtrelivereason,
                        relive_date: $scope.txtrelive_date
                    }
                    console.log(params);
                    var url = 'api/ManageEmployee/EmployeeRelievingAdd';
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

        $scope.importhrmigrationexcel = function () {
            var modalInstance = $modal.open({
                templateUrl: '/importexcel.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.excelupload = function (val, val1, name) {

                    var fileInput = document.getElementById('fileimport');
                    var filePath = fileInput.value;

                    $scope.fileinputvalue = filePath;

                    // Allowing file type
                    var allowedExtensions = /(\.xls|\.xlsx|\.csv)$/i;

                    if (!allowedExtensions.exec(filePath)) {
                        Notify.alert('File Format Not Supported!', 'warning')
                        $modalInstance.close('closed');
                        //fileInput.value = '';
                    }
                    /*  else if (filePath.includes("ImportExcelSample") == false) {
                          Notify.alert('File Name / Template Not Supported!', 'warning')
                          $modalInstance.close('closed');
                      }  */
                    else {
                        var item = {
                            name: val[0].name,
                            file: val[0]
                        };
                        var frm = new FormData();
                        frm.append('fileupload', item.file);
                        frm.append('file_name', item.name);
                        $scope.uploadfrm = frm;
                    }
                }

                $scope.uploadexcel = function () {

                    if ($scope.fileinputvalue == '' || $scope.fileinputvalue == undefined || $scope.fileinputvalue == null) {
                        Notify.alert('Kindly Select the Excel file', 'warning')
                    }
                    else {
                        var url = 'api/SysMstHRDocument/ImportHRdocumentData';
                        lockUI();
                        SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {

                            $modalInstance.close('closed');
                            if (resp.data.status == true) {
                                unlockUI();
                                Notify.alert(resp.data.message, {
                                    status: 'success',
                                    pos: 'top-center',
                                    timeout: 3000
                                });

                            }
                            else {
                                unlockUI();
                                Notify.alert(resp.data.message, {
                                    status: 'warning',
                                    pos: 'top-center',
                                    timeout: 3000
                                });
                                $modalInstance.close('closed');
                            }
                            $("#fileimport").val('');
                        });
                    }

                }

                $scope.uploadexcelcancel = function () {
                    $("#fileimport").val('');
                };
            }
        }

      /*  $scope.posttoerp = function (employee_externalid) {
            var params = {
                employee_externalid: employee_externalid
            }
            var url = 'api/SamAgroHBAPIConn/PostEmployeeToERP';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
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
                    if (resp.data.error_response != null) {
                        var error_message = resp.data.message;
                        error_message += " - NetSuite Response: " + resp.data.error_response;
                        Notify.alert(error_message, {
                            status: 'warning',
                            pos: 'top-center',
                            timeout: 10000
                        });
                    }
                    else {
                        Notify.alert(resp.data.message, {
                            status: 'warning',
                            pos: 'top-center',
                            timeout: 3000
                        });
                    }
                }
            });
        } */
    }

})();
