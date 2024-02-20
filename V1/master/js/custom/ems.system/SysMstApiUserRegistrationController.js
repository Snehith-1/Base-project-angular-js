(function () {
    'use strict';

    angular
        .module('angle')
        .controller('SysMstApiUserRegistrationController', SysMstApiUserRegistrationController);

    SysMstApiUserRegistrationController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function SysMstApiUserRegistrationController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'SysMstApiUserRegistrationController';
        $scope.lsoneapipage = $location.search().lsoneapipage;
        var lsoneapipage = $scope.lsoneapipage;
        activate();

        function activate() {

            var url = 'api/SystemMaster/GetExternalUser';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.user_list = resp.data.externaluser_list;
                unlockUI();
            });

        }
        $scope.adduser = function () {
            var modalInstance = $modal.open({
                templateUrl: '/adduser.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.txtpassword = "welcome@123";

                var url = 'api/SystemMaster/GetEmployeelist';
                lockUI();
                SocketService.get(url).then(function (resp) {
                    $scope.employee_list = resp.data.employeelist;
                    unlockUI();
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.submit = function () {

                    var params = {
                        externaluser_code: $scope.txtexternal_usercode,
                        externalsystem_name: $scope.txtexternalsystem_name,
                        email_id: $scope.txtemail_id,
                        externalsystem_ownernameList: $scope.txtexternalsystem_ownername,
                        externaluser_password: $scope.txtpassword,

                    }
                    var url = 'api/SystemMaster/CreateUserReg';
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
                            $modalInstance.close('closed');
                            activate();
                        }
                    });
                }

            }
        }

        $scope.showPopover = function (user2oneapi_gid, externalsystem_name, externalsystem_ownername) {
            var modalInstance = $modal.open({
                templateUrl: '/showpopupModal.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                    $scope.externalsystem_name = externalsystem_name;
                    $scope.externalsystem_ownername = externalsystem_ownername;
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
            }
        }
        $scope.Status_update = function (user2oneapi_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/statususer.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                var params = {
                    user2oneapi_gid: user2oneapi_gid
                }
                var url = 'api/SystemMaster/GetUserEdit';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.txtexternal_usercode = resp.data.externaluser_code;
                    $scope.txtexternalsystem_name = resp.data.externalsystem_name;
                    $scope.txtemail_id = resp.data.email_id;
                    $scope.txtpassword = resp.data.externaluser_password;
                    $scope.cboemployee_editlist = resp.data.employeeem_list;
                    $scope.rbo_status =resp.data.externaluser_status;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.update_status = function () {

                    var params = {
                        user2oneapi_gid: user2oneapi_gid,
                        externalsystem_name: $scope.txtexternalsystem_name,
                        remarks: $scope.txtremarks,
                        rbo_status: $scope.rbo_status

                    }
                    var url = 'api/SystemMaster/InactiveUserReg';
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
                    user2oneapi_gid: user2oneapi_gid
                }

                var url = 'api/SystemMaster/UserRegInactiveLogview';
                lockUI();
                SocketService.getparams(url, param).then(function (resp) {
                    $scope.userinactivelog_list = resp.data.userinactivelog_list;
                    unlockUI();
                });

            }
        }
        $scope.edituser = function (user2oneapi_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/edituser.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });

            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
              
                var url = 'api/SystemMaster/GetEmployeelist';
                lockUI();
                SocketService.get(url).then(function (resp) {
                    $scope.systemasssigned_edit = resp.data.employeelist;
                    unlockUI();
                });
                var params = {
                    user2oneapi_gid: user2oneapi_gid
                }
                var url = 'api/SystemMaster/GetUserEdit';
                SocketService.getparams(url, params).then(function (resp) {
                   
                    
                    $scope.txtexternal_usercode = resp.data.externaluser_code;
                    $scope.txtexternalsystem_name = resp.data.externalsystem_name;
                    $scope.txtemail_id = resp.data.email_id;
                    $scope.txtpassword = resp.data.externaluser_password; 
                    $scope.cboemployee_editlist = resp.data.employeeem_list;
                    $scope.cboemployee_edit = [];
                    if (resp.data.systemasssigned_list != null) {
                        var count = resp.data.systemasssigned_list.length;
                        for (var i = 0; i < count; i++) {
                            var workerIndex = $scope.cboemployee_editlist.map(function (x) { return x.employee_gid; }).indexOf(resp.data.systemasssigned_list[i].employee_gid);
                            //var indexs = $scope.cboemployee_editlist.findIndex(x => x.employee_gid === resp.data.employee[i].employee_gid);
                            $scope.cboemployee_edit.push($scope.cboemployee_editlist[workerIndex]);
                            
                        }
                    }
                   
                });


                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.update = function () {
                    lockUI();
                    var url = 'api/SystemMaster/PostUserUpdate';
                    var params = {
                        user2oneapi_gid: user2oneapi_gid,
                        externalsystem_name: $scope.txtexternalsystem_name,
                        email_id: $scope.txtemail_id,
                        systemasssigned_list: $scope.cboemployee_edit
                    }
                    SocketService.post(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            unlockUI();
                            $modalInstance.close('closed');
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            activate();

                        }
                        else {
                            unlockUI();
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
        $scope.WebAccess_Deactivate = function (user2oneapi_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/webaccessstatus.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {


                var params = {
                    user2oneapi_gid: user2oneapi_gid
                }
                var url = 'api/SystemMaster/GetOneApiCode';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.txtexternal_usercode = resp.data.user_code;
                    $scope.rbo_status = resp.data.web_active;
                });
                var params = {
                    user2oneapi_gid: user2oneapi_gid
                }
                var url = 'api/SystemMaster/GetWebAccessActiveLog';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.log_list = resp.data.userinactivelog_list;
                });
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.update_status = function () {
                    if ($scope.rbo_status == "Y") {
                        Notify.alert('Already status is active', {
                            status: 'warning',
                            pos: 'top-center',
                            timeout: 3000
                        });
                    }
                   
                    else {
                        var params = {
                            user2oneapi_gid: user2oneapi_gid,
                            remarks: $scope.txtremarks
                        }

                        var url = 'api/SystemMaster/WebDeActivation';
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
                }

            }
        }
        $scope.WebAccess_Activate = function (user2oneapi_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/webaccessstatus.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {


                var params = {
                    user2oneapi_gid: user2oneapi_gid
                }
                var url = 'api/SystemMaster/GetOneApiCode';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.txtexternal_usercode = resp.data.user_code;
                    $scope.rbo_status = resp.data.web_active;
                });
                var params = {
                    user2oneapi_gid: user2oneapi_gid
                }
                var url = 'api/SystemMaster/GetWebAccessActiveLog';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.log_list = resp.data.userinactivelog_list;
                });
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.update_status = function () {
                    if ($scope.rbo_status == "N") {
                        Notify.alert('Already status is Inactive', {
                            status: 'warning',
                            pos: 'top-center',
                            timeout: 3000
                        });
                    }
                    else {
                        var params = {
                            user2oneapi_gid: user2oneapi_gid,
                            remarks: $scope.txtremarks
                        }
                        var url = 'api/SystemMaster/WebActivation';
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
                }

            }
        }
        $scope.user_resetpwd = function (user2oneapi_gid) {

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
                    user2oneapi_gid: user2oneapi_gid
                }
                var url = 'api/SystemMaster/GetUserEdit';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.txtexternal_usercode = resp.data.externaluser_code;
                    $scope.txtexternalsystem_name = resp.data.externalsystem_name;
                    $scope.txtpassword = resp.data.externaluser_password;
                   
                });


                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.passwordSubmit = function () {

                    var params = {
                        user2oneapi_gid: user2oneapi_gid
                    }
                    var url = 'api/SystemMaster/GetUserEdit';
                    SocketService.getparams(url, params).then(function (resp) {
                        $scope.txtexternal_usercode = resp.data.externaluser_code;
                        $scope.txtexternalsystem_name = resp.data.externalsystem_name;
                        $scope.txtpassword = resp.data.externaluser_password;

                    });
                    var params = {
                        user_password: $scope.Password,
                        user2oneapi_gid: user2oneapi_gid,
                        externaluser_password: $scope.txtpassword,
                    }
                    console.log(params);
                    var url = 'api/SystemMaster/PasswordUpdate';
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

                var param = {
                    user2oneapi_gid: user2oneapi_gid
                }

                var url = 'api/SystemMaster/UserRegResetPwdLogview';
                lockUI();
                SocketService.getparams(url, param).then(function (resp) {
                    $scope.userresetpwdlog_list = resp.data.userinactivelog_list;
                    unlockUI();
                });
            }
        }
    }
})();

