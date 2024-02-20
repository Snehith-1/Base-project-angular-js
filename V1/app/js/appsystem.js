(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstHardCodedTypeController', MstHardCodedTypeController);

        MstHardCodedTypeController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams','DownloaddocumentService'];

    function MstHardCodedTypeController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, DownloaddocumentService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstHardCodedTypeController';
        $scope.lsoneapihardcodevalue = $location.search().lsoneapihardcodevalue;
        var lsoneapihardcodevalue = $scope.lsoneapihardcodevalue;
        $scope.lsoneapipage = $location.search().lsoneapipage;
        var lsoneapipage = $scope.lsoneapipage;
        activate();

        function activate() { 

            var url = 'api/SystemMaster/GetCityList';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.getcity_list = resp.data.getcity_list;
                unlockUI();
            });

        }
        

       
    }
})();


(function () {
    'use strict';

    angular
        .module('angle')
        .controller('OtherApplicationDashboardController', OtherApplicationDashboardController);

        OtherApplicationDashboardController.$inject = ['$rootScope', '$scope', '$state', '$modal', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams'];

    function OtherApplicationDashboardController($rootScope, $scope, $state, $modal, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'OtherApplicationDashboardController';

        activate();

        function activate() {
            
            var url = 'api/OtherApplication/GetOtherApplication';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.otherapplication_list = resp.data.otherapplication_list;
            });
            var url = 'api/OtherApplication/Assignedlinks';
            SocketService.get(url).then(function (resp) {
                $scope.otherapplicationemployee_list = resp.data.otherapplication_list;
            });
            unlockUI();
        }   
    }
})();
(function () {
    'use strict';

    angular
        .module('angle')
        .controller('StdOneApiDashboardController', StdOneApiDashboardController);

        StdOneApiDashboardController.$inject = ['$rootScope', '$scope', '$state', '$modal', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', '$route', '$cookies'];

    function StdOneApiDashboardController($rootScope, $scope, $state, $modal, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, $route, $cookies) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'StdOneApiDashboardController';

      // activate();

        function activate() {

           
        }
    }
})();

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


(function () {
    'use strict';

    angular
        .module('angle')
        .controller('SysMstBaseLocationController', SysMstBaseLocationController);

    SysMstBaseLocationController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams','DownloaddocumentService'];

    function SysMstBaseLocationController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams,DownloaddocumentService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'SysMstBaseLocationController';
        $scope.lsoneapipage = $location.search().lsoneapipage;
        var lsoneapipage = $scope.lsoneapipage;

        activate();

        function activate() {
            var url = 'api/SystemMaster/GetBaseLocation';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.baselocation_list = resp.data.master_list;
                unlockUI();
            });
        }

        $scope.addbaselocation = function () {
            var modalInstance = $modal.open({
                templateUrl: '/addbaselocation.html',
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
                $scope.submit = function () {

                    var params = {
                        baselocation_name: $scope.txtbase_location,
                        lms_code: $scope.txtlms_code,
                        bureau_code: $scope.txtbureau_code

                    }
                    var url = 'api/SystemMaster/CreateBaseLocation';
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
        $scope.editbaselocation = function (baselocation_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/editbaselocation.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    baselocation_gid: baselocation_gid
                }
                var url = 'api/SystemMaster/EditBaseLocation';
                SocketService.getparams(url, params).then(function (resp) {

                    $scope.txteditbase_location = resp.data.baselocation_name;
                    $scope.txteditlms_code = resp.data.lms_code;
                    $scope.txteditbureau_code = resp.data.bureau_code;
                    $scope.baselocation_gid = resp.data.baselocation_gid;
                });


                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.update = function () {

                    var url = 'api/SystemMaster/UpdateBaseLocation';
                    var params = {
                        baselocation_name: $scope.txteditbase_location,
                        lms_code: $scope.txteditlms_code,
                        bureau_code: $scope.txteditbureau_code,
                        baselocation_gid: $scope.baselocation_gid
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

        $scope.Status_update = function (baselocation_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/statusbaselocation.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                var params = {
                    baselocation_gid: baselocation_gid
                }
                var url = 'api/SystemMaster/EditBaseLocation';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.baselocation_gid = resp.data.baselocation_gid
                    $scope.txtbase_location = resp.data.baselocation_name;
                    $scope.rbo_status = resp.data.Status;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.update_status = function () {

                    var params = {
                        baselocation_gid: baselocation_gid,
                        remarks: $scope.txtremarks,
                        rbo_status: $scope.rbo_status

                    }
                    var url = 'api/SystemMaster/InactiveBaseLocation';
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
                    baselocation_gid: baselocation_gid
                }

                var url = 'api/SystemMaster/BaseLocationInactiveLogview';
                lockUI();
                SocketService.getparams(url, param).then(function (resp) {
                    $scope.baselocationinactivelog_list = resp.data.master_list;
                    unlockUI();
                });

            }
        }

        $scope.delete = function (baselocation_gid) {
            var params = {
                baselocation_gid: baselocation_gid
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
                    var url = 'api/SystemMaster/DeleteBaseLocation';
                    SocketService.getparams(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            activate();
                        }
                        else {
                            Notify.alert('Error Occurred While Deleting Base Location!', {
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

        $scope.exportreport = function () {
            lockUI();
            var url = 'api/SystemMaster/BaselocationReportExcel';
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
                    Notify.alert('Error Occurred While Export !', 'warning')
                }
            });
        }

    }
})();


(function () {
    'use strict';

    angular
        .module('angle')
        .controller('SysMstBloodGroupController', SysMstBloodGroupController);

    SysMstBloodGroupController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function SysMstBloodGroupController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'SysMstBloodGroupController';
        $scope.lsoneapipage = $location.search().lsoneapipage;
        var lsoneapipage = $scope.lsoneapipage;

        activate();

        function activate() {
            var url = 'api/SystemMaster/GetBloodGroup';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.bloodgroup_list = resp.data.master_list;
                unlockUI();
            });
        }

        $scope.addbloodgroup = function () {
            var modalInstance = $modal.open({
                templateUrl: '/addbloodgroup.html',
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
                $scope.submit = function () {

                    var params = {
                        bloodgroup_name: $scope.txtblood_group,
                        lms_code: $scope.txtlms_code,
                        bureau_code: $scope.txtbureau_code

                    }
                    var url = 'api/SystemMaster/CreateBloodGroup';
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
        $scope.editbloodgroup = function (bloodgroup_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/editbloodgroup.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    bloodgroup_gid: bloodgroup_gid
                }
                var url = 'api/SystemMaster/EditBloodGroup';
                SocketService.getparams(url, params).then(function (resp) {

                    $scope.txteditblood_group = resp.data.bloodgroup_name;
                    $scope.txteditlms_code = resp.data.lms_code;
                    $scope.txteditbureau_code = resp.data.bureau_code;
                    $scope.bloodgroup_gid = resp.data.bloodgroup_gid;
                });


                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.update = function () {

                    var url = 'api/SystemMaster/UpdateBloodGroup';
                    var params = {
                        bloodgroup_name: $scope.txteditblood_group,
                        lms_code: $scope.txteditlms_code,
                        bureau_code: $scope.txteditbureau_code,
                        bloodgroup_gid: $scope.bloodgroup_gid
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

        $scope.Status_update = function (bloodgroup_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/statusbloodgroup.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                var params = {
                    bloodgroup_gid: bloodgroup_gid
                }
                var url = 'api/SystemMaster/EditBloodGroup';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.bloodgroup_gid = resp.data.bloodgroup_gid
                    $scope.txtblood_group = resp.data.bloodgroup_name;
                    $scope.rbo_status = resp.data.Status;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.update_status = function () {

                    var params = {
                        bloodgroup_gid: bloodgroup_gid,
                        remarks: $scope.txtremarks,
                        rbo_status: $scope.rbo_status

                    }
                    var url = 'api/SystemMaster/InactiveBloodGroup';
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
                    bloodgroup_gid: bloodgroup_gid
                }

                var url = 'api/SystemMaster/BloodGroupInactiveLogview';
                lockUI();
                SocketService.getparams(url, param).then(function (resp) {
                    $scope.bloodgroupinactivelog_list = resp.data.master_list;
                    unlockUI();
                });

            }
        }

        $scope.delete = function (bloodgroup_gid) {
            var params = {
                bloodgroup_gid: bloodgroup_gid
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
                    var url = 'api/SystemMaster/DeleteBloodGroup';
                    SocketService.getparams(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            activate();
                        }
                        else {
                            Notify.alert('Error Occurred While Deleting Blood Group!', {
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


(function () {
    'use strict';

    angular
        .module('angle')
        .controller('SysMstBranchSummaryController', SysMstBranchSummaryController);

    SysMstBranchSummaryController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function SysMstBranchSummaryController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'SysMstBranchSummaryController';

        activate();

        function activate() {
            var url = 'api/SystemMaster/GetBranchSummary';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.branch_data = resp.data.master_list;
                unlockUI();
            });
        }

       
    }
})();


(function () {
    'use strict';

    angular
        .module('angle')
        .controller('SysMstBusinessHeadController', SysMstBusinessHeadController);

    SysMstBusinessHeadController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function SysMstBusinessHeadController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'SysMstBusinessHeadController';
        $scope.lsoneapipage = $location.search().lsoneapipage;
        var lsoneapipage = $scope.lsoneapipage;

        activate();

        function activate() {
            var url = 'api/SystemMaster/GetBusinessHeadSummary';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.businesshead_list = resp.data.businesshead_list;
                unlockUI();
            });
        }

        $scope.addBusinessHead = function () {
            var modalInstance = $modal.open({
                templateUrl: '/addbusinesshead.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });

            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                
                $scope.OnchangeVertical = function (cbovertical, cbocluster) {
                    if (cbovertical != "" && cbocluster != "") {
                        var params = {
                            vertical_gid: cbovertical,
                            lstype: 'business',
                            lstypegid: cbocluster
                        }
                        var url = 'api/SystemMaster/GetVerticalProgramList';
                        lockUI();
                        SocketService.getparams(url, params).then(function (resp) {
                            $scope.program_list = resp.data.program_list;
                            unlockUI();
                        });
                    }
                }

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                var url = 'api/SystemMaster/GetZoneList';
                lockUI();
                SocketService.get(url).then(function (resp) {
                    $scope.zone_list = resp.data.zone_list;
                    unlockUI();
                });
                var url = 'api/SystemMaster/GetVerticallist';
                lockUI();
                SocketService.get(url).then(function (resp) {
                    $scope.vertical_list = resp.data.vertical_list;
                    unlockUI();
                });
                var url = 'api/SystemMaster/GetEmployeelist';
                lockUI();
                SocketService.get(url).then(function (resp) {
                    $scope.employee_list = resp.data.employeelist;
                    unlockUI();
                });
                $scope.submit = function () {
                    var VerticalName = "";
                    if ($scope.vertical_list && $scope.vertical_list.length > 0) {
                        //VerticalName = $scope.vertical_list.filter(e => e.vertical_gid == $scope.cbovertical);
                        VerticalName = $scope.vertical_list.filter(function (e) { return e.vertical_gid == $scope.cbovertical });
                        VerticalName = VerticalName[0].vertical_name
                    }
                    var zone_name = "";
                    if ($scope.zone_list && $scope.zone_list.length > 0) {
                        //zone_name = $scope.zone_list.filter(e => e.zone_gid == $scope.cbozone);
                        zone_name = $scope.zone_list.filter(function (e) { return e.zone_gid == $scope.cbozone });
                        zone_name = zone_name[0].zone_name
                    }
                    var params = {
                        zone_gid: $scope.cbozone,
                        zone_name: zone_name,
                        vertical_gid: $scope.cbovertical,
                        vertical_name: VerticalName,
                        employee_gid: $scope.cboemployee.employee_gid,
                        employee_name: $scope.cboemployee.employee_name,
                        program_gid: $scope.cboprogram.program_gid,
                        program_name: $scope.cboprogram.program_name,
                    }

                    var url = 'api/SystemMaster/PostBusinessHeadAdd';
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

        $scope.editbusinesshead = function (businesshead_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/editbusinesshead.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });

            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    businesshead_gid: businesshead_gid
                }
                var url = 'api/SystemMaster/GetBusinessHeadEdit';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.cbozoneedit = resp.data.zone_gid,
                    $scope.cboverticaledit = resp.data.vertical_gid,
                    $scope.cboemployeeedit = resp.data.employee_gid,
                    $scope.zone_list = resp.data.zone_list,
                    $scope.cboprogramedit = resp.data.program_gid,
                    $scope.vertical_list = resp.data.vertical_list,
                    $scope.employee_list = resp.data.employeelist
                     if (resp.data.vertical_gid != "") {
                         var params = {
                             vertical_gid: resp.data.vertical_gid,
                             lstype: 'business',
                             lstypegid: resp.data.zone_gid,
                             lsmaster_gid: businesshead_gid
                         }
                         var url = 'api/SystemMaster/GetEditVerticalProgramList';
                         SocketService.getparams(url, params).then(function (resp) {
                             $scope.program_list = resp.data.program_list;
                         });
                     }
                });

                $scope.OnchangeVertical = function (cbovertical, cboclusteredit) {
                    if (cbovertical != "" && cboclusteredit != "") {
                        var params = {
                            vertical_gid: cbovertical,
                            lstype: 'business',
                            lstypegid: cboclusteredit,
                            //lsmaster_gid: clusterhead_gid
                            lsmaster_gid: businesshead_gid
                        }
                        var url = 'api/SystemMaster/GetEditVerticalProgramList';
                        lockUI();
                        SocketService.getparams(url, params).then(function (resp) {
                            $scope.program_list = resp.data.program_list;
                            unlockUI();
                        });
                    }
                }

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.update = function () {
                    var zonename;
                    var zone_index = $scope.zone_list.map(function (e) { return e.zone_gid }).indexOf($scope.cbozoneedit);
                    if (zone_index == -1) { zonename = ''; } else { zonename = $scope.zone_list[zone_index].zone_name; };

                    var verticalname;
                    var vertical_index = $scope.vertical_list.map(function (e) { return e.vertical_gid }).indexOf($scope.cboverticaledit);
                    if (vertical_index == -1) { verticalname = ''; } else { verticalname = $scope.vertical_list[vertical_index].vertical_name; };

                    var employeename;
                    var employee_index = $scope.employee_list.map(function (e) { return e.employee_gid }).indexOf($scope.cboemployeeedit);
                    if (employee_index == -1) { employeename = ''; } else { employeename = $scope.employee_list[employee_index].employee_name; };

                    var programName = "";
                    if ($scope.program_list && $scope.program_list.length > 0) {
                        //programName = $scope.program_list.filter(e => e.program_gid == $scope.cboprogramedit);
                        programName = $scope.program_list.filter(function (e) { return e.program_gid == $scope.cboprogramedit });
                        programName = programName[0].program_name
                    }

                    var url = 'api/SystemMaster/PostBusinessHeadUpdate';
                    var params = {
                        zone_gid: $scope.cbozoneedit,
                        zone_name: zonename,
                        vertical_gid: $scope.cboverticaledit,
                        vertical_name: verticalname,
                        employee_gid: $scope.cboemployeeedit,
                        employee_name: employeename,
                        businesshead_gid: businesshead_gid,
                        program_gid: $scope.cboprogramedit,
                        program_name: programName,
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

        $scope.Status_update = function (businesshead_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/businessheadstatus.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                var params = {
                    businesshead_gid: businesshead_gid
                }
                var url = 'api/SystemMaster/GetBusinessHeadEdit';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.businesshead_gid = resp.data.businesshead_gid
                    $scope.txtemployee_name = resp.data.employee_name;
                    $scope.txtemployee_gid = resp.data.employee_gid;
                    $scope.rbo_status = resp.data.businesshead_status;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.update_status = function () {

                    var params = {
                        businesshead_gid: businesshead_gid,
                        employee_name: $scope.txtemployee_name,
                        employee_gid: $scope.txtemployee_gid,
                        remarks: $scope.txtremarks,
                        rbo_status: $scope.rbo_status

                    }
                    var url = 'api/SystemMaster/PostBusinessHeadInactive';
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
                    businesshead_gid: businesshead_gid
                }

                var url = 'api/SystemMaster/GetBusinessHeadInactiveLogview';
                lockUI();
                SocketService.getparams(url, param).then(function (resp) {
                    $scope.businessheadinactivelog_list = resp.data.master_list;
                    unlockUI();
                });

            }
        }


    }
})();


(function () {
    'use strict';

    angular
        .module('angle')
        .controller('SysMstCalendarGroupController', SysMstCalendarGroupController);

    SysMstCalendarGroupController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function SysMstCalendarGroupController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'SysMstCalendarGroupController';
        $scope.lsoneapipage = $location.search().lsoneapipage;
        var lsoneapipage = $scope.lsoneapipage;

        activate();

        function activate() {
            var url = 'api/SystemMaster/GetCalendarGroup';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.calendargroup_list = resp.data.master_list;
                unlockUI();
            });
        }

        $scope.addcalendargroup = function () {
            var modalInstance = $modal.open({
                templateUrl: '/addcalendargroup.html',
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
                $scope.submit = function () {

                    var params = {
                        calendargroup_name: $scope.txtcalendar_group,
                        lms_code: $scope.txtlms_code,
                        bureau_code: $scope.txtbureau_code

                    }
                    var url = 'api/SystemMaster/CreateCalendarGroup';
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
        $scope.editcalendargroup = function (calendargroup_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/editcalendargroup.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    calendargroup_gid: calendargroup_gid
                }
                var url = 'api/SystemMaster/EditCalendarGroup';
                SocketService.getparams(url, params).then(function (resp) {

                    $scope.txteditcalendar_group = resp.data.calendargroup_name;
                    $scope.txteditlms_code = resp.data.lms_code;
                    $scope.txteditbureau_code = resp.data.bureau_code;
                    $scope.calendargroup_gid = resp.data.calendargroup_gid;
                });


                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.update = function () {

                    var url = 'api/SystemMaster/UpdateCalendarGroup';
                    var params = {
                        calendargroup_name: $scope.txteditcalendar_group,
                        lms_code: $scope.txteditlms_code,
                        bureau_code: $scope.txteditbureau_code,
                        calendargroup_gid: $scope.calendargroup_gid
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

        $scope.Status_update = function (calendargroup_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/statuscalendargroup.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                var params = {
                    calendargroup_gid: calendargroup_gid
                }
                var url = 'api/SystemMaster/EditCalendarGroup';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.calendargroup_gid = resp.data.calendargroup_gid
                    $scope.txtcalendar_group = resp.data.calendargroup_name;
                    $scope.rbo_status = resp.data.status_calendargroup;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.update_status = function () {

                    var params = {
                        calendargroup_gid: calendargroup_gid,
                        remarks: $scope.txtremarks,
                        rbo_status: $scope.rbo_status

                    }
                    var url = 'api/SystemMaster/InactiveCalendarGroup';
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
                    calendargroup_gid: calendargroup_gid
                }

                var url = 'api/SystemMaster/CalendarGroupInactiveLogview';
                lockUI();
                SocketService.getparams(url, param).then(function (resp) {
                    $scope.calendargroupinactivelog_list = resp.data.master_list;
                    unlockUI();
                });

            }
        }

        $scope.delete = function (calendargroup_gid) {
            var params = {
                calendargroup_gid: calendargroup_gid
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
                    var url = 'api/SystemMaster/DeleteCalendarGroup';
                    SocketService.getparams(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            activate();
                        }
                        else {
                            Notify.alert('Error Occurred While Deleting Calendar Group!', {
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


(function () {
    'use strict';

    angular
        .module('angle')
        .controller('SysMstClientRoleController', SysMstClientRoleController);

    SysMstClientRoleController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function SysMstClientRoleController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'SysMstClientRoleController';
        $scope.lsoneapipage = $location.search().lsoneapipage;
        var lsoneapipage = $scope.lsoneapipage;

        activate();

        function activate() {
            var url = 'api/SystemMaster/GetSubFunction';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.subfunction_list = resp.data.master_list;
                unlockUI();
            });
        }

        $scope.addclientrole = function () {
            var modalInstance = $modal.open({
                templateUrl: '/addclientrole.html',
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
                $scope.submit = function () {

                    var params = {
                        subfunction_name: $scope.txtsubfunction,
                        lms_code: $scope.txtlms_code,
                        bureau_code: $scope.txtbureau_code

                    }
                    var url = 'api/SystemMaster/CreateSubFunction';
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
        $scope.editclientrole = function (subfunction_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/editclientrole.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    subfunction_gid: subfunction_gid
                }
                var url = 'api/SystemMaster/EditSubFunction';
                SocketService.getparams(url, params).then(function (resp) {

                    $scope.txteditsubfunction = resp.data.subfunction_name;
                    $scope.txteditlms_code = resp.data.lms_code;
                    $scope.txteditbureau_code = resp.data.bureau_code;
                    $scope.subfunction_gid = resp.data.subfunction_gid;
                });


                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.update = function () {

                    var url = 'api/SystemMaster/UpdateSubFunction';
                    var params = {
                        subfunction_gid: subfunction_gid,
                        subfunction_name: $scope.txteditsubfunction,
                        bureau_code: $scope.txteditbureau_code,
                        lms_code:$scope.txteditlms_code
                       
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

        $scope.Status_update = function (subfunction_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/statusclientrole.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                var params = {
                    subfunction_gid: subfunction_gid
                }
                var url = 'api/SystemMaster/EditSubFunction';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.subfunction_gid = resp.data.subfunction_gid;
                    $scope.txteditsubfunction = resp.data.subfunction_name;
                    $scope.rbo_status = resp.data.Status;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.update_status = function () {

                    var params = {
                        subfunction_gid: subfunction_gid,
                        remarks: $scope.txtremarks,
                        rbo_status: $scope.rbo_status,
                        subfunction_name: $scope.txteditsubfunction
                    }
                    var url = 'api/SystemMaster/InactiveSubFunction';
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
                    subfunction_gid: subfunction_gid
                }

                var url = 'api/SystemMaster/SubFunctionInactiveLogview';
                lockUI();
                SocketService.getparams(url, param).then(function (resp) {
                    $scope.subfunctioninactivelog_list = resp.data.master_list;
                    unlockUI();
                });

            }
        }

        $scope.delete = function (subfunction_gid) {
            var params = {
                subfunction_gid: subfunction_gid
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
                    var url = 'api/SystemMaster/DeleteSubFunction';
                    SocketService.getparams(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            activate();
                        }
                        else {
                            Notify.alert('Error Occurred While Deleting Client Role!', {
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


(function () {
    'use strict';

    angular
        .module('angle')
        .controller('SysMstClusterHeadController', SysMstClusterHeadController);

    SysMstClusterHeadController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function SysMstClusterHeadController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'SysMstClusterHeadController';
        $scope.lsoneapipage = $location.search().lsoneapipage;
        var lsoneapipage = $scope.lsoneapipage;

        activate();

        function activate() {

            var url = 'api/SystemMaster/GetClusterHeadSummary';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.clusterhead_list = resp.data.clusterhead_list;
                unlockUI();
            });
        }



        $scope.addclusterhead = function () {
            var modalInstance = $modal.open({
                templateUrl: '/addclusterhead.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });

            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.OnchangeVertical = function (cbovertical, cbocluster) {
                    if (cbovertical != "" && cbocluster != "") {
                        var params = {
                            vertical_gid: cbovertical,
                            lstype: 'cluster',
                            lstypegid: cbocluster
                        }
                        var url = 'api/SystemMaster/GetVerticalProgramList';
                        lockUI();
                        SocketService.getparams(url, params).then(function (resp) {
                            $scope.program_list = resp.data.program_list;
                            unlockUI();
                        });
                    } 
                }
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                var url = 'api/SystemMaster/GetClusterslist';
                lockUI();
                SocketService.get(url).then(function (resp) {
                    $scope.cluster_list = resp.data.cluster_list;
                    unlockUI();
                });
                var url = 'api/SystemMaster/GetVerticallist';
                lockUI();
                SocketService.get(url).then(function (resp) {
                    $scope.vertical_list = resp.data.vertical_list;
                    unlockUI();
                });
                var url = 'api/SystemMaster/GetEmployeelist';
                lockUI();
                SocketService.get(url).then(function (resp) {
                    $scope.employee_list = resp.data.employeelist;
                    unlockUI();
                });

                $scope.submit = function () {
                    var VerticalName = "";
                    if ($scope.vertical_list && $scope.vertical_list.length > 0) {
                        //VerticalName = $scope.vertical_list.filter(e => e.vertical_gid == $scope.cbovertical);
                        VerticalName = $scope.vertical_list.filter(function (e) { return e.vertical_gid == $scope.cbovertical });
                        VerticalName = VerticalName[0].vertical_name
                    }
                    var cluster_name = "";
                    if ($scope.cluster_list && $scope.cluster_list.length > 0) {
                        //cluster_name = $scope.cluster_list.filter(e => e.cluster_gid == $scope.cbocluster);
                        cluster_name = $scope.cluster_list.filter(function (e) { return e.cluster_gid == $scope.cbocluster });
                        cluster_name = cluster_name[0].cluster_name
                    }
                    var params = {
                        cluster_gid: $scope.cbocluster,
                        cluster_name: cluster_name,
                        vertical_gid: $scope.cbovertical,
                        vertical_name: VerticalName,
                        employee_gid: $scope.cboemployee.employee_gid,
                        employee_name: $scope.cboemployee.employee_name,
                        program_gid: $scope.cboprogram.program_gid,
                        program_name: $scope.cboprogram.program_name,
                    }

                    var url = 'api/SystemMaster/PostClusterHeadAdd';
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
        $scope.editcluster = function (clusterhead_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/editclusterhead.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });

            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    clusterhead_gid: clusterhead_gid
                }
                lockUI();
                var url = 'api/SystemMaster/GetClusterHeadEdit';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.cboclusteredit = resp.data.cluster_gid,
                    $scope.cboverticaledit = resp.data.vertical_gid,
                    $scope.cboemployeeedit = resp.data.employee_gid,
                     $scope.cboprogramedit = resp.data.program_gid,
                    $scope.cluster_list = resp.data.cluster_list,
                    $scope.vertical_list = resp.data.vertical_list,
                    $scope.employee_list = resp.data.employeelist
                    if (resp.data.vertical_gid != "") {
                        var params = {
                            vertical_gid: resp.data.vertical_gid,
                            lstype: 'cluster',
                            lstypegid: resp.data.cluster_gid,
                            lsmaster_gid: clusterhead_gid
                        }
                        var url = 'api/SystemMaster/GetEditVerticalProgramList';
                        SocketService.getparams(url, params).then(function (resp) {
                            $scope.program_list = resp.data.program_list; 
                        });
                    }
                    unlockUI();
                });

                $scope.OnchangeVertical = function (cbovertical, cboclusteredit) {
                    if (cbovertical != "" && cboclusteredit != "") {
                        var params = {
                            vertical_gid: cbovertical,
                            lstype: 'cluster',
                            lstypegid: cboclusteredit,
                            lsmaster_gid: clusterhead_gid
                        }
                        var url = 'api/SystemMaster/GetEditVerticalProgramList';
                        lockUI();
                        SocketService.getparams(url, params).then(function (resp) {
                            $scope.program_list = resp.data.program_list;
                            unlockUI();
                        });
                    } 
                }

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.update = function () {
                    var clustername;
                    var cluster_index = $scope.cluster_list.map(function (e) { return e.cluster_gid }).indexOf($scope.cboclusteredit);
                    if (cluster_index == -1) { clustername = ''; } else { clustername = $scope.cluster_list[cluster_index].cluster_name; };

                    var verticalname;
                    var vertical_index = $scope.vertical_list.map(function (e) { return e.vertical_gid }).indexOf($scope.cboverticaledit);
                    if (vertical_index == -1) { verticalname = ''; } else { verticalname = $scope.vertical_list[vertical_index].vertical_name; };

                    var employeename;
                    var employee_index = $scope.employee_list.map(function (e) { return e.employee_gid }).indexOf($scope.cboemployeeedit);
                    if (employee_index == -1) { employeename = ''; } else { employeename = $scope.employee_list[employee_index].employee_name; };

                    var programName = "";
                    if ($scope.program_list && $scope.program_list.length > 0) {
                        //programName = $scope.program_list.filter(e => e.program_gid == $scope.cboprogramedit);
                        programName = $scope.program_list.filter(function (e) { return e.program_gid == $scope.cboprogramedit });
                        programName = programName[0].program_name
                    }
                    var url = 'api/SystemMaster/PostClusterHeadUpdate';
                    var params = {
                        cluster_gid: $scope.cboclusteredit,
                        cluster_name: clustername,
                        vertical_gid: $scope.cboverticaledit,
                        vertical_name: verticalname,
                        employee_gid: $scope.cboemployeeedit,
                        employee_name: employeename,
                        clusterhead_gid: clusterhead_gid,
                        program_gid: $scope.cboprogramedit,
                        program_name: programName,
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

        $scope.Status_update = function (clusterhead_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/statusclusterhead.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    clusterhead_gid: clusterhead_gid
                }
                var url = 'api/SystemMaster/GetClusterHeadEdit';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.txtclusterhead_name = resp.data.employee_name,
                    $scope.txtvertical_name = resp.data.vertical_name,
                    $scope.txtcluster_name = resp.data.cluster_name,
                    $scope.rbo_status = resp.data.clusterhead_status

                });



                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.update_status = function () {

                    var params = {
                        clusterhead_gid: clusterhead_gid,
                        employee_name: $scope.txtclusterhead_name,
                        remarks: $scope.txtremarks,
                        rbo_status: $scope.rbo_status

                    }
                    var url = 'api/SystemMaster/InactiveClusterhead';
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
                    clusterhead_gid: clusterhead_gid
                }

                var url = 'api/SystemMaster/ClusterheadInactiveLogview';
                lockUI();
                SocketService.getparams(url, param).then(function (resp) {
                    $scope.clusterheadinactivelog_list = resp.data.master_list;
                    unlockUI();
                });

            }
        }



    }
})();

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('SysMstClusterMappingController', SysMstClusterMappingController);

    SysMstClusterMappingController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams','DownloaddocumentService'];

    function SysMstClusterMappingController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams,DownloaddocumentService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'SysMstClusterMappingController';
        $scope.lsoneapipage = $location.search().lsoneapipage;
        var lsoneapipage = $scope.lsoneapipage;

        activate();

        function activate() {
            var url = 'api/SystemMaster/GetClusterSummary';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.cluster_list = resp.data.cluster_list;
                unlockUI();
            });
        }

        $scope.addcluster = function () {
            var modalInstance = $modal.open({
                templateUrl: '/addcluster.html',
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

                var url = 'api/SystemMaster/GetUnTaggedLocations';
                lockUI();
                SocketService.get(url).then(function (resp) {
                    $scope.location_list = resp.data.master_list;
                    unlockUI();
                });

                $scope.submit = function () {

                    var params = {
                        locationlist: $scope.cbolocation,
                        cluster_name: $scope.txtcluster_name
                    }

                    var url = 'api/SystemMaster/PostClusterAdd';
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
        $scope.editcluster = function (cluster_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/editclustermapping.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
           
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    cluster_gid: cluster_gid
                }
                var url = 'api/SystemMaster/GetUnTaggedLocationsEdit';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.location_listedit = resp.data.master_list;
                    unlockUI();
                });

                var params = {
                    cluster_gid: cluster_gid
                }
                var url = 'api/SystemMaster/GetClusterEdit';
                SocketService.getparams(url, params).then(function (resp) {

                    $scope.txteditcluster_name = resp.data.cluster_name;

                    $scope.locationlist = resp.data.locationlist;

                    $scope.cbolocation = [];
                    if (resp.data.locationlist != null) {
                        var count = resp.data.locationlist.length;
                        for (var i = 0; i < count; i++) {
                            //var indexs = $scope.location_listedit.findIndex(x => x.baselocation_gid === resp.data.locationlist[i].baselocation_gid);
                            var indexs = $scope.location_listedit.map(function (x) { return x.baselocation_gid; }).indexOf(resp.data.locationlist[i].baselocation_gid);
                            $scope.cbolocation.push($scope.location_listedit[indexs]);
                        }
                    }
                });


                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.update = function () {
                    lockUI();
                    var url = 'api/SystemMaster/PostClusterUpdate';
                    var params = {
                        cluster_gid: cluster_gid,
                        cluster_name: $scope.txteditcluster_name,
                        locationlist: $scope.cbolocation
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

        $scope.Status_update = function (cluster_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/statuscluster.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.errormsg = false;
                var params = {
                    cluster_gid: cluster_gid
                }
                var url = 'api/SystemMaster/GetClusterEdit';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.cluster_gid = resp.data.cluster_gid
                    $scope.txtcluster_name = resp.data.cluster_name;
                    $scope.rbo_status = resp.data.cluster_status;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.update_status = function () {

                    var params = {
                        cluster_gid: cluster_gid,
                        cluster_name: $scope.txtcluster_name,
                        remarks: $scope.txtremarks,
                        rbo_status: $scope.rbo_status

                    }
                    var url = 'api/SystemMaster/InactiveCluster';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {

                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            $scope.errormsg = false;
                            $modalInstance.close('closed');
                            activate(); 
                        }
                        else {
                            if (resp.data.message == "N") {
                                $scope.rbo_status ="Y";
                                $scope.ocs_pendingcount = resp.data.ocs_pendingcount;
                                $scope.agrbyr_pendingcount = resp.data.agrbyr_pendingcount;
                                $scope.agrsupr_pendingcount = resp.data.agrsupr_pendingcount;
                                $scope.errormsg = true;
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
                        } 
                    });

                   

                }

                var param = {
                    cluster_gid: cluster_gid
                }

                var url = 'api/SystemMaster/ClusterInactiveLogview';
                lockUI();
                SocketService.getparams(url, param).then(function (resp) {
                    $scope.clusterinactivelog_list = resp.data.master_list;
                    unlockUI();
                });

            }
        }

        $scope.showPopover = function (cluster_gid, cluster_name) {
            var modalInstance = $modal.open({
                templateUrl: '/showlocation.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    cluster_gid: cluster_gid
                }
                var url = 'api/SystemMaster/GetCluster2BaseLocation';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.locationlist = resp.data.locationlist;
                    $scope.cluster_name = cluster_name;
                });
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
            }
        }

        $scope.exportreport = function () {
            lockUI();
            var url = 'api/SystemMaster/ClusterReportExcel';
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
                    Notify.alert('Error Occurred While Export !', 'warning')

                }

            });
        }

    }
})();


(function () {
    'use strict';

    angular
        .module('angle')
        .controller('SysMstDepartmentSummaryController', SysMstDepartmentSummaryController);

    SysMstDepartmentSummaryController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function SysMstDepartmentSummaryController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'SysMstDepartmentSummaryController';

        activate();

        function activate() {
            var url = 'api/SystemMaster/GetDepartmentSummary';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.department_data = resp.data.master_list;
                unlockUI();
            });
        }

       
    }
})();


(function () {
    'use strict';

    angular
        .module('angle')
        .controller('SysMstBranchSummaryController', SysMstBranchSummaryController);

    SysMstBranchSummaryController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function SysMstBranchSummaryController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'SysMstBranchSummaryController';

        activate();

        function activate() {
            var url = 'api/SystemMaster/GetBranchSummary';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.branch_data = resp.data.master_list;
                unlockUI();
            });
        }

       
    }
})();


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

(function () {
    'use strict';
    
    angular
        .module('angle')
        .controller('SysMstEmployeeAddController', SysMstEmployeeAddController);

        SysMstEmployeeAddController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route','ngDialog', 'SweetAlert'];

    function SysMstEmployeeAddController($rootScope, $scope, $state, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngDialog,SweetAlert ) {
        
        var vm = this;
        vm.title = 'SysMstEmployeeAddController';
        $scope.employeeaccess_no = false;
        var lstab = $location.search().lstab;
        $scope.lstab = lstab;


        var perm_address1,perm_address2,perm_country,perm_state,perm_city,perm_postalcode;
        $scope.btncopy = function () {
            perm_address1 = $scope.txtperaddressline1;
            $scope.txttempaddressline1 = perm_address1;
            perm_address2 = $scope.txtperaddressline2;
            $scope.txttempaddressline2 = perm_address2;
            perm_country = $scope.txtpermcountry;
            $scope.txttempcountry = perm_country;
            perm_state = $scope.txtpermstate;
            $scope.txttermstate = perm_state;
            perm_city = $scope.txtpermcity;
            $scope.txttermcity = perm_city;
            perm_postalcode = $scope.txtpermpostalcode;
            $scope.txttermpostalcode = perm_postalcode;
        }
       
       
        $scope.isShowHide = function (param) {
            if (param == "show") {
                $scope.employeeaccess_no = true;

            }
            else if (param == "hide") {
                $scope.employeeaccess_no = false;
                
            }
            
        }
        activate();
        function activate() {
            lockUI();
           $scope.entitytext =false;
           $scope.entitydrop = false;
           $scope.txtpermcountry = 'CN06070099';
           $scope.txttempcountry = 'CN06070099';
            var url = 'api/ManageEmployee/EntityName';
            SocketService.get(url).then(function (resp) {
                $scope.txtentity = resp.data.entity_name;
                $scope.entity_flag = resp.data.entity_flag;
                if( resp.data.entity_flag =="Y"){
                    $scope.entitytext =true;
                    $scope.entitydrop = false;
                }
                else{
                    $scope.entitytext =false;
                    $scope.entitydrop = true;
                }
             }); 

            var url = 'api/ManageEmployee/PopEntityActive';
            SocketService.get(url).then(function (resp) {
             
                $scope.entityList = resp.data.entity;
                              
            });

            var url = 'api/ManageEmployee/PopRole';
            SocketService.get(url).then(function (resp) {
                $scope.roleList = resp.data.rolemaster;
               
            }); 
            var url = 'api/ManageEmployee/PopReportingTo';
            SocketService.get(url).then(function (resp) {
                $scope.reportingtoList = resp.data.reportingto;
              
            });  

             var url = 'api/ManageEmployee/PopBranch';
            SocketService.get(url).then(function (resp) {
                $scope.branchList = resp.data.employee;
              
            });
            var url = 'api/ManageEmployee/PopDepartment';
            SocketService.get(url).then(function (resp) {
                $scope.departmentList = resp.data.employee;
              
            });
            var url = 'api/ManageEmployee/PopDesignation';
            SocketService.get(url).then(function (resp) {
                $scope.designationList = resp.data.employee;
              
            }); 
            var url = 'api/ManageEmployee/PopCountry';
            SocketService.get(url).then(function (resp) {
                $scope.countryList = resp.data.country;
              
            });
            var url = "api/ManageEmployee/PopSubfunction";
            SocketService.get(url).then(function (resp) {
                $scope.subfunction_list = resp.data.employee
            });

            var url = 'api/SystemMaster/GetBaseLocationlistActive';
            SocketService.get(url).then(function (resp) {
                $scope.location_list = resp.data.location_list;

            });
            var url = 'api/MstApplication360/GetMaritalStatusActive';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.maritalstatus_data = resp.data.application_list;
                unlockUI();
            });
            var url = 'api/SystemMaster/GetBloodGroupActive';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.bloodgroup_list = resp.data.master_list;
                unlockUI();
            });
            unlockUI();
            vm.calender06 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();

                vm.open06 = true;
            };
            vm.formats = ['dd-MM-yyyy'];
            vm.format = vm.formats[0];
            vm.dateOptions = {
                formatYear: 'yy',
                startingDay: 1
            };
            unlockUI();
                   
        }

        $scope.add_cancel = function () {
            if (lstab == 'pending') {
                $location.url('app/SysMstEmployeePendingSummary');
            }
            else if (lstab == 'active') {
                $state.go('app.SysMstEmployeeActiveUserSummary');
            }
            else if (lstab == 'inactive') {
                $state.go('app.SysMstEmployeeInactiveSummary');
            }
            else if (lstab == 'relieving') {
                $state.go('app.SysMstEmployeeInactiveSummary');
            }
            else {
                $state.go('app.SysMstEmployeeSummary');
            }
           // $state.go('app.SysMstEmployeeSummary');   
            };

  /* Employee Add */

        $scope.addemployee_submit = function () {
            if($scope.txtgender == undefined){
                $scope.txtgender = 'Male' 
                console.log($scope.txtgender);
            }
            else
            {
                console.log($scope.txtgender);
            }
            if($scope.cbomarital_status == undefined){
                var maritalstatus_name = '';
                var maritalstatus_gid = '';
            }
            else{
                var maritalstatus_name = $scope.cbomarital_status.maritalstatus_name;
                var maritalstatus_gid = $scope.cbomarital_status.maritalstatus_gid;   
            }
            if($scope.cbobloodgroup_name == undefined){
                var bloodgroup_name = '';
                var bloodgroup_gid = '';
            }
            else{
                var bloodgroup_name = $scope.cbobloodgroup_name.bloodgroup_name;
                var bloodgroup_gid = $scope.cbobloodgroup_name.bloodgroup_gid;
            }
            if ($scope.txtemployeeacess == undefined) {
                $scope.txtemployeeacess = 'Y';
            }

            if ($scope.txtemployeeacess == "Y")
            {
                if ($scope.txtuserpassword == '' || $scope.txtuserpassword == null || $scope.txtuserpassword == undefined) {
                    alert('Enter User Password', 'warning');
                }
                else if ($scope.txtconfirmpassword == '' || $scope.txtconfirmpassword == null || $scope.txtconfirmpassword == undefined) {
                    alert('Enter Confirm Password', 'warning');
                }
                else {
                   
                    var params = {
                        company_name: $scope.txtentity,
                        entity_gid: $scope.txtentitydrop,
                        branch_gid: $scope.txtbranch.branch_gid,
                        department_gid: $scope.txtdepartment.department_gid,
                        designation_gid: $scope.txtdesignation.designation_gid,
                        useraccess: $scope.txtemployeeacess,
                        user_code: $scope.txtusercode,
                        user_password: $scope.txtuserpassword,
                        user_password: $scope.txtconfirmpassword,
                        role_gid: $scope.txtrole.role_gid,
                        employee_reportingto: $scope.txtreportingto.employee_gid,
                        employee_photo: $scope.txtuploadphoto,
                        user_firstname: $scope.txtfirstname,
                        user_lastname: $scope.txtlastname,
                        gender: $scope.txtgender,
                        employee_emailid: $scope.txtuseremail,
                        employee_mobileno: $scope.txtmobile,
                        per_address1: $scope.txtperaddressline1,
                        per_address2: $scope.txtperaddressline2,
                        per_country_gid: $scope.txtpermcountry.country_gid,
                        per_state: $scope.txtpermstate,
                        per_city: $scope.txtpermcity,
                        per_postal_code: $scope.txtpermpostalcode,
                        temp_address1: $scope.txttempaddressline1,
                        temp_address2: $scope.txttempaddressline2,
                        temp_country_gid: $scope.txttempcountry.country_gid,
                        temp_state: $scope.txttermstate,
                        temp_city: $scope.txttermcity,
                        temp_postal_code: $scope.txttermpostalcode,
                        baselocation_gid: $scope.cbobaselocation.baselocation_gid,
                        marital_status : maritalstatus_name,
                        marital_status_gid :maritalstatus_gid,
                        bloodgroup_name : bloodgroup_name,
                        bloodgroup_gid : bloodgroup_gid,
                        joining_date : $scope.cboemployee_joining_date,
                        personal_phone_no : $scope.txtpersonalphone_number,
                        personal_emailid: $scope.txtpersonalemail,
                        subfunction_gid: $scope.txtsubfunction

                    }

                    var url = 'api/ManageEmployee/EmployeeAdd';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {

                        if (resp.data.status == true) {
                            unlockUI();
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 4000
                            });
                            activate();
                            $location.url('app/SysMstEmployeePendingSummary');
                        }
                        else {

                            Notify.alert(resp.data.message, {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 4000
                            });
                            unlockUI();
                            //activate();
                            //if (lstab == 'pending') {
                            //    $location.url('app/SysMstEmployeePendingSummary');
                            //}
                            //else if (lstab == 'active') {
                            //    $state.go('app.SysMstEmployeeActiveUserSummary');
                            //}
                            //else if (lstab == 'inactive') {
                            //    $state.go('app.SysMstEmployeeInactiveSummary');
                            //}
                            //else if (lstab == 'relieving') {
                            //    $state.go('app.SysMstEmployeeRelievingSummary');
                            //}
                            //else {
                            //    $state.go('app.SysMstEmployeeSummary');
                            //}
                            
                        }
                    });
                }
            }

            else 
            {
                var params = {
                    company_name: $scope.txtentity,
                    entity_gid: $scope.txtentitydrop,
                    branch_gid: $scope.txtbranch.branch_gid,
                    department_gid: $scope.txtdepartment.department_gid,
                    designation_gid: $scope.txtdesignation.designation_gid,
                    useraccess: $scope.txtemployeeacess,
                    user_code: $scope.txtusercode,
                    role_gid: $scope.txtrole.role_gid,
                    employee_reportingto: $scope.txtreportingto.employee_gid,
                    employee_photo: $scope.txtuploadphoto,
                    user_firstname: $scope.txtfirstname,
                    user_lastname: $scope.txtlastname,
                    gender: $scope.txtgender,
                    employee_emailid: $scope.txtuseremail,
                    employee_mobileno: $scope.txtmobile,
                    per_address1: $scope.txtperaddressline1,
                    per_address2: $scope.txtperaddressline2,
                    per_country_gid: $scope.txtpermcountry.country_gid,
                    per_state: $scope.txtpermstate,
                    per_city: $scope.txtpermcity,
                    per_postal_code: $scope.txtpermpostalcode,
                    temp_address1: $scope.txttempaddressline1,
                    temp_address2: $scope.txttempaddressline2,
                    temp_country_gid: $scope.txttempcountry.country_gid,
                    temp_state: $scope.txttermstate,
                    temp_city: $scope.txttermcity,
                    temp_postal_code: $scope.txttermpostalcode,
                    baselocation_gid: $scope.cbobaselocation.baselocation_gid,
                    subfunction_gid: $scope.txtsubfunction.subfunction_gid
                }

                var url = 'api/ManageEmployee/EmployeeAdd';
                lockUI();
                SocketService.post(url, params).then(function (resp) {

                    if (resp.data.status == true) {
                        unlockUI();
                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 4000
                        });
                        activate();
                        $state.go('app.SysMstEmployeeSummary');
                    }
                    else {

                        Notify.alert(resp.data.message, {
                            status: 'warning',
                            pos: 'top-center',
                            timeout: 4000
                        });
                        //activate();
                        //$state.go('app.SysMstEmployeeSummary');
                        unlockUI();
                    }
                });
            }
        }  

        $scope.user_code_check = function (user_gid) {
            var params = {
                user_gid : user_gid
            }
            var url = 'api/ManageEmployee/UserCodeCheck';
            SocketService.getparams(url, params).then(function (resp) {
            $scope.user_message = resp.data.message;
            if( $scope.user_message == "User Code Already in Use")
            { $scope.message_color = 1}
            else
            { $scope.message_color = 2}
            });
        };

        /* $scope.upload = function (val, val1, name) {
            var item = {
                name: val[0].name,
                file: val[0]
            };
            var frm = new FormData();
            frm.append('fileupload', item.file);
            frm.append('file_name', item.name);
            frm.append('document_name', $scope.txtuploadphoto);
            
            $scope.uploadfrm = frm;
            var url = 'api/IdasTrnSanctionDoc/conversation docupload';
            lockUI();
            SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {
                

                $("#addupload").val('');
                $("#editupload").val('');
                if (resp.data.status == true) {
                    unlockUI();
                    Notify.alert('Document Uploaded Successfully..!!', 'success')
                    var params = {
                        employee_gid: $scope.employee_gid
                    }
                    var url = 'api/IdasTrnSanctionDoc/Getconversedoc';

                    SocketService.getparams(url, params).then(function (resp) {
                        if (resp.data.status == true) {

                            $scope.txtuploadphoto = resp.data;
                        }

                    });
                }
                else {
                    unlockUI();
                    Notify.alert('File Format Not Supported!')

                }
            });    
         } */
    } 
})();


(function () {
    'use strict';
    
    angular
        .module('angle')
        .controller('SysMstEmployeeDeactivateController', SysMstEmployeeDeactivateController);

        SysMstEmployeeDeactivateController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route','ngDialog', 'SweetAlert'];

    function SysMstEmployeeDeactivateController($rootScope, $scope, $state, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngDialog,SweetAlert ) {
        var employee_gid = $location.search().employee_gid;
        var lstab = $location.search().lstab;
        $scope.lstab = lstab;
        activate();
        var vm = this;
        vm.title = 'SysMstEmployeeDeactivateController';
        
        function activate() {
            lockUI();
            var url = 'api/ManageEmployee/PopRole';
            SocketService.get(url).then(function (resp) {
                $scope.roleList = resp.data.rolemaster;
                

                var params = {
                    employee_gid: employee_gid
                };
                if ($scope.lstab == 'pending') {
                    var url = 'api/ManageEmployee/EmployeePendingEditView';
                } 
                else {
                    var url = 'api/ManageEmployee/EmployeeEditView';
                }
                //var url = 'api/ManageEmployee/EmployeeEditView';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.employee_details = resp.data;
                    unlockUI();
                });
               
            });

            var params = {
                deactivateemployee_gid: employee_gid
            }
            var url = 'api/ManageEmployee/GetDeactivationCondition';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
               
                $scope.lblasset_status = resp.data.asset_status;
                $scope.lbltempasset_status = resp.data.tempasset_status;
                $scope.lblemployeereporting_to = resp.data.employeereporting_to;
                $scope.lblmodule_name = resp.data.module_name;
                $scope.lbsubmitbutton = resp.data.submitbutton;
                //$scope.creditapproval = resp.data.appcreditapproval_gid;


                if ((resp.data.asset_status == "" || resp.data.asset_status == null)) {
                    $scope.showasset_status = false;
                    if (resp.data.tempasset_status == "" || resp.data.tempasset_status == null) {
                        $scope.showtempasset_status = false;
                    }
                    else {
                        $scope.showtempasset_status = true;
                    }
                    
                }               
                else {
                    $scope.showasset_status = true;
                    
                    if (resp.data.tempasset_status == "" || resp.data.tempasset_status == null) {
                        $scope.showtempasset_status = false;
                    }
                    else {
                        $scope.showtempasset_status = true;
                      
                    }
                }

                if (resp.data.module_name == "" || resp.data.module_name == null) {
                    $scope.showmodule_name = false;
                }
                else {
                    $scope.showmodule_name = true;
                }

                if (resp.data.applicationapproval_gid == "" || resp.data.applicationapproval_gid == null) {
                   $scope.showapplicationapproval_gid = false;
                }
                else {
                    $scope.showapplicationapproval_gid = true;
                }
                if (resp.data.appcreditapproval_gid == "" || resp.data.appcreditapproval_gid == null) {
                    $scope.showappcreditapproval_gid = false;
                }
                else {
                    $scope.showappcreditapproval_gid = true;
                }
                if (resp.data.ccmeeting2members_gid == "" || resp.data.ccmeeting2members_gid == null) {
                    $scope.showccmeeting2members_gid = false;
                }
                else {
                    $scope.showccmeeting2members_gid = true;
                }
                if (resp.data.cadgroupmanager_gid == "" || resp.data.cadgroupmanager_gid == null) {
                    $scope.showcadgroupmanager_gid = false;
                }
                else {
                    $scope.showcadgroupmanager_gid = true;
                }
                if (resp.data.cadgroupmembers_gid == "" || resp.data.cadgroupmembers_gid == null) {
                    $scope.showcadgroupmembers_gid = false;
                }
                else {
                    $scope.showcadgroupmembers_gid = true;
                }
                if (resp.data.creditops2maker_gid == "" || resp.data.creditops2maker_gid == null) {
                    $scope.showcreditops2maker_gid = false;
                }
                else {
                    $scope.showcreditops2maker_gid = true;
                }
                if (resp.data.creditops2checker_gid == "" || resp.data.creditops2checker_gid == null) {
                    $scope.showcreditops2checker_gid = false;
                }
                else {
                    $scope.showcreditops2checker_gid = true;
                }
                if (resp.data.creditmapping_gid == "False") {
                    $scope.showcreditmapping_gid = false;
                }
                else {
                    $scope.showcreditmapping_gid = true;
                }
                if (resp.data.processtype_assign == "False") {
                    $scope.showprocesstype_assign = false;
                }
                else {
                    $scope.showprocesstype_assign = true;
                }
                if (resp.data.application_gid == "" || resp.data.application_gid == null) {
                    $scope.showapplication_gid = false;
                }
                else {
                    $scope.showapplication_gid = true;
                }

                //Samagro
                if (resp.data.agrapplicationapproval_gid == "" || resp.data.agrapplicationapproval_gid == null) {
                    $scope.showagrapplicationapproval_gid = false;
                }
                else {
                    $scope.showagrapplicationapproval_gid = true;
                }
                if (resp.data.agrappcreditapproval_gid == "" || resp.data.agrappcreditapproval_gid == null) {
                    $scope.showagrappcreditapproval_gid = false;
                }
                else {
                    $scope.showagrappcreditapproval_gid = true;
                }
                if (resp.data.agrccmeeting2members_gid == "" || resp.data.agrccmeeting2members_gid == null) {
                    $scope.showagrccmeeting2members_gid = false;
                }
                else {
                    $scope.showagrccmeeting2members_gid = true;
                }
                if (resp.data.agrcadgroupmanager_gid == "" || resp.data.agrcadgroupmanager_gid == null) {
                    $scope.showagrcadgroupmanager_gid = false;
                }
                else {
                    $scope.showagrcadgroupmanager_gid = true;
                }
                if (resp.data.agrprocesstype_assign == "False") {
                    $scope.showagrprocesstype_assign = false;
                }
                else {
                    $scope.showagrprocesstype_assign = true;
                }

                if (resp.data.ccmembermaster_gid == "" || resp.data.ccmembermaster_gid == null) {
                    $scope.showccmembermaster_gid = false;
                }
                else {
                    $scope.showccmembermaster_gid = true;
                }
                if (resp.data.agrcreditmapping_gid == "False") {
                    $scope.showagrcreditmapping_gid = false;
                }
                else {
                    $scope.showagrcreditmapping_gid = true;
                }





                if (resp.data.productdeskmanager_gid == "" || resp.data.productdeskmanager_gid == null) {
                    $scope.showproductdeskmanager_gid = false;
                }
                else {
                    $scope.showproductdeskmanager_gid = true;
                }
                if (resp.data.productdeskmember_gid == "" || resp.data.productdeskmember_gid == null) {
                    $scope.showproductdeskmember_gid = false;
                }
                else {
                    $scope.showproductdeskmember_gid = true;
                }
                if (resp.data.mstpmgapproval_gid == "" || resp.data.mstpmgapproval_gid == null) {
                    $scope.showmstpmgapproval_gid = false;
                }
                else {
                    $scope.showmstpmgapproval_gid = true;
                }
                if (resp.data.mstproductapproval_gid == "" || resp.data.mstproductapproval_gid == null) {
                    $scope.showmstproductapproval_gid = false;
                }
                else {
                    $scope.showmstproductapproval_gid = true;
                }


                if (resp.data.appproductapproval_gid == "" || resp.data.appproductapproval_gid == null) {
                    $scope.showappproductapproval_gid = false;
                }
                else {
                    $scope.showappproductapproval_gid = true;
                }

                if (resp.data.warehouse2approval_gid == "" || resp.data.warehouse2approval_gid == null) {
                    $scope.showwarehouse2approval_gid = false;
                }
                else {
                    $scope.showwarehouse2approval_gid = true;
                }

                if (resp.data.agrapplication_gid == "" || resp.data.agrapplication_gid == null) {
                    $scope.showagrapplication_gid = false;
                }
                else {
                    $scope.showagrapplication_gid = true;
                }


                // Audit
                if (resp.data.auditmapping2employee_gid == "" || resp.data.auditmapping2employee_gid == null) {
                    $scope.showauditmapping2employee_gid = false;
                }
                else {
                    $scope.showauditmapping2employee_gid = true;
                }
                if (resp.data.multipleauditee_gid == "" || resp.data.multipleauditee_gid == null) {
                    $scope.showmultipleauditee_gid = false;
                }
                else {
                    $scope.showmultipleauditee_gid = true;
                }
                if (resp.data.auditcreation_gid == "False") {
                    $scope.showauditcreation_gid = false;
                }
                else {
                    $scope.showauditcreation_gid = true;
                }
                // Foundation
                if (resp.data.customerapproving_gid == "" || resp.data.customerapproving_gid == null) {
                    $scope.showcustomerapproving_gid = false;
                }
                else {
                    $scope.showcustomerapproving_gid = true;
                }
                if (resp.data.campaign_gid == "" || resp.data.campaign_gid == null) {
                    $scope.showcampaign_gid = false;
                }
                else {
                    $scope.showcampaign_gid = true;
                }
                if (resp.data.finalcampaign_gid == "" || resp.data.finalcampaign_gid == null) {
                    $scope.showfinalcampaign_gid = false;
                }
                else {
                    $scope.showfinalcampaign_gid = true;
                }



                if (resp.data.campaignapproving2employee_gid == "" || resp.data.campaignapproving2employee_gid == null) {
                    $scope.showcampaignapproving2employee_gid = false;
                }
                else {
                    $scope.showcampaignapproving2employee_gid = true;
                }
                if (resp.data.appcustomerapproving_gid == "" || resp.data.appcustomerapproving_gid == null) {
                    $scope.showappcustomerapproving_gid = false;
                }
                else {
                    $scope.showappcustomerapproving_gid = true;
                }

                // Business Developement
                if (resp.data.marketingcall_gid == "False") {
                    $scope.showmarketingcall_gid = false;
                }
                else {
                    $scope.showmarketingcall_gid = true;
                }
                //Inbound
                if (resp.data.inboundcall_gid == "False") {
                    $scope.showinboundcall_gid = false;
                }
                else {
                    $scope.showinboundcall_gid = true;
                }
                //Sa_onboarding
                //if (resp.data.sacontactinstitution_gid == "False") {
                //    $scope.showsacontactinstitution_gid = false;
                //}
                //else {
                //    $scope.showsacontactinstitution_gid = true;
                //}
                //if (resp.data.sacontact_gid == "False") {
                //    $scope.showsacontact_gid = false;
                //}
                //else {
                //    $scope.showsacontact_gid = true;
                //}
          
                if (resp.data.makersacontactinstitution_gid == "" || resp.data.makersacontactinstitution_gid == null) {
                    $scope.showmakersacontactinstitution_gid = false;
                }
                else {
                    $scope.showmakersacontactinstitution_gid = true;
                }



                if (resp.data.checkersacontactinstitution_gid == "" || resp.data.checkersacontactinstitution_gid == null) {
                    $scope.showcheckersacontactinstitution_gid = false;
                }
                else {
                    $scope.showcheckersacontactinstitution_gid = true;
                }

                if (resp.data.finalsacontactinstitution_gid == "" || resp.data.finalsacontactinstitution_gid == null) {
                    $scope.showfinalsacontactinstitution_gid = false;
                }
                else {
                    $scope.showfinalsacontactinstitution_gid = true;
                }

                if (resp.data.makersacontact_gid == "" || resp.data.makersacontact_gid == null) {
                    $scope.showmakersacontact_gid = false;
                }
                else {
                    $scope.showmakersacontact_gid = true;
                }



                if (resp.data.checkersacontact_gid == "" || resp.data.checkersacontact_gid == null) {
                    $scope.showcheckersacontact_gid = false;
                }
                else {
                    $scope.showcheckersacontact_gid = true;
                }

                if (resp.data.finalsacontact_gid == "" || resp.data.finalsacontact_gid == null) {
                    $scope.showfinalsacontact_gid = false;
                }
                else {
                    $scope.showfinalsacontact_gid = true;
                }


                //Service Request
                if (resp.data.activedepartment2manager_gid == "" || resp.data.activedepartment2manager_gid == null) {
                    $scope.showactivedepartment2manager_gid = false;
                }
                else {
                    $scope.showactivedepartment2manager_gid = true;
                }
                if (resp.data.activedepartment2member_gid == "" || resp.data.activedepartment2member_gid == null) {
                    $scope.showactivedepartment2member_gid = false;
                }
                else {
                    $scope.showactivedepartment2member_gid = true;
                }
                if (resp.data.supportteam2member_gid == "" || resp.data.supportteam2member_gid == null) {
                    $scope.showsupportteam2member_gid = false;
                }
                else {
                    $scope.showsupportteam2member_gid = true;
                }
                if (resp.data.requestapproval_gid == "" || resp.data.requestapproval_gid == null) {
                    $scope.showrequestapproval_gid = false;
                }
                else {
                    $scope.showrequestapproval_gid = true;
                }
                if (resp.data.servicerequest_gid == "" || resp.data.servicerequest_gid == null) {
                    $scope.showservicerequest_gid = false;
                }
                else {
                    $scope.showservicerequest_gid = true;
                }
                if (resp.data.email_gid == "" || resp.data.email_gid == null) {
                    $scope.showemail_gid = false;
                }
                else {
                    $scope.showemail_gid = true;
                }
                //if (resp.data.maildetails_gid == "" || resp.data.maildetails_gid == null) {
                //    $scope.showmaildetails_gid = false;
                //}
                //else {
                //    $scope.showmaildetails_gid = true;
                //}
                unlockUI();
            });
                              
        }

        $scope. deactive_cancel = function () {
            if (lstab == 'pending') {
                $location.url('app/SysMstEmployeePendingSummary');
            }
            else if (lstab == 'active') {
                $state.go('app.SysMstEmployeeActiveUserSummary');
            }
            else if (lstab == 'inactive') {
                $state.go('app.SysMstEmployeeInactiveSummary');
            }
            else if (lstab == 'relieving') {
                $state.go('app.SysMstEmployeeInactiveSummary');
            }
            else {
                $state.go('app.SysMstEmployeeSummary');   
            }
            //$state.go('app.SysMstEmployeeSummary');   
            };  

       /* Employee Deactive */
        
        $scope.deactive_submit = function () {
            if ($scope.receipt_date == "" || $scope.receipt_date == null || $scope.receipt_date == undefined)
            {
                Notify.alert('Kindly Enter Deactivation Date', 'warning')
            }
            else if ($scope.txtremarks == "" || $scope.txtremarks == null || $scope.txtremarks == undefined) {
                Notify.alert('Kindly Enter Remarks', 'warning')
            }
            else {
                var url = 'api/ManageEmployee/EmployeeDeactivate';
                var params = {
                    exit_date: $scope.receipt_date,
                    employee_gid: employee_gid,
                }

                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    if (resp.data.status == true) {
                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 4000
                        });
                        activate();
                        if (lstab == 'pending') {
                            $location.url('app/SysMstEmployeePendingSummary');
                        }
                        else if (lstab == 'active') {
                            $state.go('app.SysMstEmployeeActiveUserSummary');
                        }
                        else if (lstab == 'inactive') {
                            $state.go('app.SysMstEmployeeInactiveSummary');
                        }
                        else if (lstab == 'relieving') {
                            $state.go('app.SysMstEmployeeInactiveSummary');
                        }
                        else {
                            $state.go('app.SysMstEmployeeSummary');
                        }
                    }
                    else {

                        Notify.alert(resp.data.message, {
                            status: 'warning',
                            pos: 'top-center',
                            timeout: 4000
                        });
                        activate();
                        if (lstab == 'pending') {
                            $location.url('app/SysMstEmployeePendingSummary');
                        }
                        else if (lstab == 'active') {
                            $state.go('app.SysMstEmployeeActiveUserSummary');
                        }
                        else if (lstab == 'inactive') {
                            $state.go('app.SysMstEmployeeInactiveSummary');
                        }
                        else if (lstab == 'relieving') {
                            $state.go('app.SysMstEmployeeInactiveSummary');
                        }
                        else {
                            $state.go('app.SysMstEmployeeSummary');
                        }
                    }
                });
            }
        }              
      
 }
    
})();


(function () {
    'use strict';
    
    angular
        .module('angle')
        .controller('SysMstEmployeeEditController', SysMstEmployeeEditController);

        SysMstEmployeeEditController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route','ngDialog', 'SweetAlert'];

      

    function SysMstEmployeeEditController($rootScope, $scope, $state, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngDialog,SweetAlert ) {
        var perm_address1,perm_address2,perm_country,perm_state,perm_city,perm_postalcode;
        $scope.btncopy = function () {
            perm_address1 = $scope.txtperaddressline1;
            $scope.txttempaddressline1 = perm_address1;
            perm_address2 = $scope.txtperaddressline2;
            $scope.txttempaddressline2 = perm_address2;
            perm_country = $scope.txtpermcountry;
            $scope.txttempcountry = perm_country;
            perm_state = $scope.txtpermstate;
            $scope.txttermstate = perm_state;
            perm_city = $scope.txtpermcity;
            $scope.txttermcity = perm_city;
            perm_postalcode = $scope.txtpermpostalcode;
            $scope.txttermpostalcode = perm_postalcode;
        }
        var employee_gid = $location.search().employee_gid;

        var lstab = $location.search().lstab;
        $scope.lstab = lstab;
      
        activate();
        var vm = this;
        vm.title = 'SysMstEmployeeEditController';
        vm.calender06 = function ($event) {
            $event.preventDefault();
            $event.stopPropagation();

            vm.open06 = true;
        };
        vm.formats = ['dd-MM-yyyy'];
        vm.format = vm.formats[0];
        vm.dateOptions = {
            formatYear: 'yy',
            startingDay: 1
        };

        function activate() {
            
            lockUI();
            if ($scope.lstab == 'pending') {
                var url = 'api/ManageEmployee/EmployeePendingEditView';
            } 
            else {
                var url = 'api/ManageEmployee/EmployeeEditView';
            }
            var param = {
                employee_gid: employee_gid
            };

            SocketService.getparams(url, param).then(function (resp) {
               
                $scope.employee_details = resp.data;
                $scope.txtentity = resp.data.company_name;
                $scope.txtbranch = resp.data.branch_gid;
                $scope.txtdepartment = resp.data.department_gid;
                $scope.txtsubfunction = resp.data.subfunction_gid;
                $scope.txtdesignation = resp.data.designation_gid;
                $scope.txtemployeeacess = resp.data.user_status;
                $scope.txtusercode = resp.data.user_code;
                $scope.cbobaselocation = resp.data.baselocation_gid;
                $scope.txtrole = resp.data.role_gid;
                $scope.txtreportingto = resp.data.employee_reportingto;
                $scope.txtuploadphoto = resp.data.employee_photo;
                $scope.txtfirstname = resp.data.user_firstname;
                $scope.txtlastname = resp.data.user_lastname;
                $scope.txtgender = resp.data.gender;
                $scope.txtuseremail = resp.data.employee_emailid;
                $scope.txtmobile = resp.data.employee_mobileno;
                $scope.txtperaddressline1 = resp.data.per_address1;
                $scope.txtperaddressline2 = resp.data.per_address2;
                $scope.txtpermcountry = resp.data.per_country_gid;
                $scope.txtpermstate = resp.data.per_state;
                $scope.txtpermcity = resp.data.per_city;
                $scope.txttermpostalcode = resp.data.temp_postal_code;
                $scope.txtpermpostalcode = resp.data.per_postal_code;
                $scope.txttempaddressline1 = resp.data.temp_address1;
                $scope.txttempaddressline2 = resp.data.temp_address2;
                $scope.txttempcountry = resp.data.temp_country_gid;
                $scope.txttermstate = resp.data.temp_state;
                $scope.txttermcity = resp.data.temp_city;
                $scope.cbomarital_status = resp.data.marital_status;
                $scope.cbomarital_status_gid = resp.data.marital_status_gid;
                $scope.cbobloodgroup_name = resp.data.bloodgroup_name;
                $scope.cbobloodgroup_gid = resp.data.bloodgroup_gid;
                $scope.cboemployee_joining_date = resp.data.joiningdate;
                $scope.txtpersonalphone_number = resp.data.personal_phone_no;
                $scope.txtpersonalemail = resp.data.personal_emailid;
            });

            var url = 'api/ManageEmployee/PopRole';
            SocketService.get(url).then(function (resp) {
                $scope.roleList = resp.data.rolemaster;
               
            }); 
            var url = 'api/ManageEmployee/PopReportingTo';
            SocketService.get(url).then(function (resp) {
                $scope.reportingtoList = resp.data.reportingto;
              
            });  

             var url = 'api/ManageEmployee/PopBranch';
            SocketService.get(url).then(function (resp) {
                $scope.branchList = resp.data.employee;
              
            });
            var url = 'api/ManageEmployee/PopDepartment';
            SocketService.get(url).then(function (resp) {
                $scope.departmentList = resp.data.employee;

              
            });

            var url = "api/ManageEmployee/PopSubfunction";
            SocketService.get(url).then(function (resp) {
                $scope.subfunction_list = resp.data.employee
            });

            var url = 'api/ManageEmployee/PopDesignation';
            SocketService.get(url).then(function (resp) {
                $scope.designationList = resp.data.employee;
              
            }); 
            var url = 'api/ManageEmployee/PopCountry';
            SocketService.get(url).then(function (resp) {
                $scope.countryList = resp.data.country;
              
            });
            var url = 'api/SystemMaster/GetBaseLocationlistActive';
            SocketService.get(url).then(function (resp) {
                $scope.location_list = resp.data.location_list;

            });
            var url = 'api/MstApplication360/GetMaritalStatusActive';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.maritalstatus_data = resp.data.application_list;
                unlockUI();
            });
            var url = 'api/SystemMaster/GetBloodGroupActive';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.bloodgroup_list = resp.data.master_list;
                unlockUI();
            });
            
            unlockUI();
           
                          
        }

        $scope.edit_cancel = function () {
            if (lstab == 'pending') {
                $location.url('app/SysMstEmployeePendingSummary');
            }
            else if (lstab == 'active') {
                $state.go('app.SysMstEmployeeActiveUserSummary');
            }
            else if (lstab == 'inactive') {
                $state.go('app.SysMstEmployeeInactiveSummary');
            }
            else if (lstab == 'relieving') {
                $state.go('app.SysMstEmployeeRelievingSummary');
            }
            else {
                $state.go('app.SysMstEmployeeSummary');   
            }
        };
        $scope.editemployee_update = function () {
        
        var maritalstatus_name = $('#cbomarital_status_gid :selected').text();
        var bloodgroup_name = $('#cbobloodgroup_gid :selected').text();

        var params = {
            employee_gid : employee_gid,            
            company_name : $scope.txtentity,
            branch_gid : $scope.txtbranch,
            department_gid: $scope.txtdepartment,
            subfunction_gid:$scope.txtsubfunction,
            designation_gid: $scope.txtdesignation,
            useraccess : $scope.txtemployeeacess,
            user_code : $scope.txtusercode,            
            role_gid: $scope.txtrole,
            employee_reportingto  : $scope.txtreportingto,
            employee_photo : $scope.txtuploadphoto,
            user_firstname : $scope.txtfirstname,
            user_lastname : $scope.txtlastname,
            gender : $scope.txtgender,
            employee_emailid : $scope.txtuseremail,
            employee_mobileno : $scope.txtmobile,
            per_address1: $scope.txtperaddressline1,
            per_address2 : $scope.txtperaddressline2,
            per_country_gid : $scope.txtpermcountry,
            per_state : $scope.txtpermstate,
            per_city : $scope.txtpermcity,
            per_postal_code : $scope.txtpermpostalcode,
            temp_address1 : $scope.txttempaddressline1,
            temp_address2 : $scope.txttempaddressline2,
            temp_country_gid : $scope.txttempcountry,
            temp_state : $scope.txttermstate,
            temp_city : $scope.txttermcity,
            temp_postal_code: $scope.txttermpostalcode,
            baselocation_gid: $scope.cbobaselocation,
            marital_status : maritalstatus_name,
            marital_status_gid : $scope.cbomarital_status_gid,
            bloodgroup_name : bloodgroup_name,
            bloodgroup_gid : $scope.cbobloodgroup_gid,
            joining_date : $scope.cboemployee_joining_date,
            personal_phone_no : $scope.txtpersonalphone_number,
            personal_emailid : $scope.txtpersonalemail

        }
        
        if ($scope.lstab == 'pending') {
            var url = 'api/ManageEmployee/EmployeePendingUpdate';
        } 
        else {
            var url = 'api/ManageEmployee/EmployeeUpdate';
        }
        lockUI();
        SocketService.post(url, params).then(function (resp) {
           
            if (resp.data.status == true) {
                 unlockUI();
                activate();
                if (lstab == 'pending') {
                    $location.url('app/SysMstEmployeePendingSummary');
                }
                else if (lstab == 'active') {
                    $state.go('app.SysMstEmployeeActiveUserSummary');
                }
                else if (lstab == 'inactive') {
                    $state.go('app.SysMstEmployeeInactiveSummary');
                }
                else if (lstab == 'relieving') {
                    $state.go('app.SysMstEmployeeInactiveSummary');
                }
                else {
                    $state.go('app.SysMstEmployeeSummary');   
                }
                Notify.alert(resp.data.message, {
                    status: 'success',
                    pos: 'top-center',
                    timeout: 4000
                });
            }
            

            else {
                Notify.alert(resp.data.message, {
                    status: 'warning',
                    pos: 'top-center',
                    timeout: 4000
                });
            }
            activate();
        });
                  
      
 }

 /* $scope.upload = function (val, val1, name) {
                var item = {
                    name: val[0].name,
                    file: val[0]
                };
                var frm = new FormData();
                frm.append('fileupload', item.file);
                frm.append('file_name', item.name);
                frm.append('document_name', $scope.txtuploadphoto);
                
                $scope.uploadfrm = frm;
                var url = 'api/IdasTrnSanctionDoc/conversation docupload';
                lockUI();
                SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {
                   
    
                    $("#addupload").val('');
                    $("#editupload").val('');
                    if (resp.data.status == true) {
                        unlockUI();
                        Notify.alert('Document Uploaded Successfully..!!', 'success')
                        var params = {
                            employee_gid: $scope.employee_gid
                        }
                        var url = 'api/IdasTrnSanctionDoc/Getconversedoc';
    
                        SocketService.getparams(url, params).then(function (resp) {
                            if (resp.data.status == true) {
    
                                $scope.txtuploadphoto = resp.data;
                            }
    
                        });
                    }
                    else {
                        unlockUI();
                        Notify.alert('File Format Not Supported!')
    
                    }
    
                });
           
      
 } */

// $scope.postto_erp = function () {
//    var params = {
//        employee_gid: employee_gid
//    }
//    var url = 'api/SamAgroHBAPIConn/PostEmployeeToERP';
//    lockUI();
//    SocketService.getparams(url, params).then(function (resp) {
//        unlockUI();
//        if (resp.data.status == true) {
//            Notify.alert("Posted to ERP Successfully!", {
//                status: 'success',
//                pos: 'top-center',
//                timeout: 3000
//            });
//        }
//        else {
//            Notify.alert("Error Occured in posting to ERP..!", {
//                status: 'warning',
//                pos: 'top-center',
//                timeout: 3000
//            });
//        }


//    });
//}

}
    
})();

(function () {
    'use strict';
    
    angular
        .module('angle')
        .controller('SysMstEmployeeHRDocumentController', SysMstEmployeeHRDocumentController);

        SysMstEmployeeHRDocumentController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route','ngDialog', 'SweetAlert','DownloaddocumentService'];

    function SysMstEmployeeHRDocumentController($rootScope, $scope, $state, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngDialog,SweetAlert,DownloaddocumentService ) {
        
        var vm = this;
        vm.title = 'SysMstEmployeeHRDocumentController';
        
        var lsemployee_gid = $location.search().employee_gid;
        $scope.employee_gid = lsemployee_gid;

        activate();
        function activate() {
            var url = 'api/SysMstHRDocument/UpdateExpiryDate';
            lockUI();
            var params = {
                employee_gid: $scope.employee_gid
            }
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
            }); 

            var url = 'api/SysMstHRDocument/GetSysHRDocumentDropDown';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.hrdocument_data = resp.data.hrdocument_list;
                unlockUI();
                
            }); 
            var url = 'api/ManageEmployee/GetHRDoclist';
            
            var params={
                employee_gid: $scope.employee_gid
            }
            SocketService.getparams(url, params).then(function (resp) {
                lockUI();
             
                    $scope.document_list = resp.data.hrdoc;
                    unlockUI();
            
            }); 
        }

        $scope.HRDocumentUpload = function () {
            lockUI();
            var fi = document.getElementById('file');
            if (fi.files.length > 0) {
                var frm = new FormData();
                for (var i = 0; i < fi.files.length; i++) {

                    frm.append(fi.files[i].name, fi.files[i]);
                    
                    var fname = fi.files.item(i).name;
                    var fsize = fi.files.item(i).size;
                }
                frm.append('document_name', $scope.cboHRDcument.hrdocument_name);
                frm.append('document_gid', $scope.cboHRDcument.hrdocument_gid);
                frm.append('employee_gid', $scope.employee_gid);
                frm.append('project_flag', "Default");
                $scope.uploadfrm = frm;
                var url = 'api/ManageEmployee/HRDocumentUpload';
                SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {
                    $("#file").val('');
                    
                    unlockUI();
                    if (resp.data.status == true) {
                        activate();
                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });
                    }
                    else {
                        activate();
                        Notify.alert(resp.data.message, {
                            
                            status: 'warning',
                            pos: 'top-center',
                            timeout: 3000
                        });
                    }
                    cboHRDcument = "";        
                    unlockUI();

                });
            }
            else {
                alert('Please select a file.')
            }
            activate();
            
        }


        $scope.download = function (val1, val2, val3) {
            if (val3 == 'N') {
                DownloaddocumentService.Downloaddocument(val1, val2);
            }
            else {
                DownloaddocumentService.OtherDownloaddocument(val1, val2, val3);
            } 
        }

        $scope.downloadallhr = function () {
            for (var i = 0; i < $scope.document_list.length; i++) {
                if ($scope.document_list[i].migration_flag == 'N') {
                    DownloaddocumentService.Downloaddocument($scope.document_list[i].hrdoc_path, $scope.document_list[i].hrdoc_name);
                }
                else {
                    DownloaddocumentService.OtherDownloaddocument($scope.document_list[i].hrdoc_path, $scope.document_list[i].hrdoc_name,"HRMigration");
                }
                
            }
        }

        $scope.proceedforesign = function (val1, val2, val3, migration_flag) {
            var params = {
                hrdoc_id: val1,
                file_name: val2,
                file_path: val3,
                migration_flag: migration_flag
            }
            var url = 'api/SysMstHRDocument/UploadDocumenttoDigio';
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
        }
            
        $scope.esignstatusenquiry = function (val1, val2, val3) {
            var params = {
                hrdoc_id: val1,
                file_name: val2,
                file_path: val3 
            }
            var url = 'api/SysMstHRDocument/GetDocumentDetails';
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
        }

        $scope.downloaddocfromdigio = function (val1, val2, val3) {
            var params = {
                hrdoc_id: val1,
                hrdoc_path: val2,
                hrdoc_name: val3
            }
            var url = 'api/SysMstHRDocument/DownloadDocfromDigio';
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true)
                    $rootScope.$emit('downloadEvent', resp);
                else {
                    return resp;
                }
            });
        }

        $scope.deleteDocument = function (val) {
            var params = {
                hrdoc_id: val
            }
            SweetAlert.swal({
                title: 'Are you sure?',
                text: 'Do You Want To Delete the Document List ?',

                showCancelButton: true,
                confirmButtonColor: '#DD6B55',
                confirmButtonText: 'Yes, delete it!',
                closeOnConfirm: false
            }, function (isConfirm) {
                if (isConfirm) {
                    lockUI();
                    var url = "api/ManageEmployee/HRDocDelete";
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
    } 
})();

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('SysMstEmployeeInactiveController', SysMstEmployeeInactiveController);

        SysMstEmployeeInactiveController.$inject = ['$rootScope', '$modal','$scope', '$state', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route','ngDialog', 'SweetAlert','DownloaddocumentService'];

    function SysMstEmployeeInactiveController($rootScope, $modal, $scope, $state, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngDialog, SweetAlert, DownloaddocumentService) {

        activate();
        var vm = this;
        vm.title = 'SysMstEmployeeInactiveController';
        var lstab='inactive';

        function activate() {

            var url = 'api/ManageEmployee/EmployeeInactiveSummary';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.employeeinactive_list = resp.data.employee;
                unlockUI();
            });
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

    }
    
    
})();

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('SysMstEmployeePendingController', SysMstEmployeePendingController);

        SysMstEmployeePendingController.$inject = ['$rootScope', '$modal','$scope', '$state', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route','ngDialog', 'SweetAlert','DownloaddocumentService'];

    function SysMstEmployeePendingController($rootScope, $modal, $scope, $state, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngDialog, SweetAlert, DownloaddocumentService) {

        activate();
        var vm = this;
        vm.title = 'SysMstEmployeePendingController';
        $scope.lsoneapipage = $location.search().lsoneapipage;
        var lsoneapipage = $scope.lsoneapipage;

        var lstab='pending';

        function activate() {

            var url = 'api/ManageEmployee/EmployeePendingSummary';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.employeepending_list = resp.data.employee;
                unlockUI();
            });

            $scope.today = new Date();
            var today = $scope.today;
            var checktoday = new Date();
            checktoday = ("0" + today.getDate()+1).slice(-2) + '-' + ("0" + (today.getMonth() + 1)).slice(-2) + '-' + today.getFullYear();
            $scope.checktoday = checktoday;

            var today = new Date();
            var dd = String(today.getDate()).padStart(2, '0');
            var mm = String(today.getMonth() + 1).padStart(2, '0'); //January is 0!
            var yyyy = today.getFullYear();

            today = mm + '-' + dd + '-' + yyyy;
            $scope.checktoday = today;
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
        /* Employee Active */
        $scope.employee_onboard = function (employee_gid) {
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
                    var url = 'api/ManageEmployee/EmployeePendingApproval';
                    $scope.employee_gid = localStorage.getItem('employee_gid');
                    var param = {
                        employee_gid: $scope.employee_gid
                    };
                    lockUI();
                    SocketService.getparams(url, param).then(function (resp) {
                        if (resp.data.status == true) {
                            SweetAlert.swal('Activated Successfully!');
                            activate();
                            $location.url('app/SysMstEmployeeActiveUserSummary');
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

        //Export Excel of Employee
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
    }
})();

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


(function () {
    'use strict';

    angular
        .module('angle')
        .controller('SysMstEmployeeViewController', SysMstEmployeeViewController);

        SysMstEmployeeViewController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function SysMstEmployeeViewController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
      
        var vm = this;
        vm.title = 'SysMstEmployeeViewController';
        var employee_gid = $location.search().employee_gid;
      
        var lstab = $location.search().lstab;
        $scope.lstab = lstab;
        activate();
        function activate() {
            lockUI();
            if ( $scope.lstab== 'pending') {
                var url = 'api/ManageEmployee/EmployeePendingEditView';
            } 
            else {
                var url = 'api/ManageEmployee/EmployeeEditView';
            }
                        
            var param = {
                employee_gid: employee_gid
            };

            SocketService.getparams(url, param).then(function (resp) {
                $scope.employee_details = resp.data;
            });

            var url = 'api/ManageEmployee/EmployeeRelievingView';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.relive_date = resp.data.relive_date;
                $scope.remarks = resp.data.remarks;
            });
            unlockUI();

            if (lstab == 'pending') {
                $scope.pending=1;
            }
            else if (lstab == 'active') {
                $scope.active=1;
            }
            else if (lstab == 'inactive') {
                $scope.inactive=1;
            }
            else if (lstab == 'relieving') {
                $scope.relieving=1;
            }
        }

        $scope.view_cancel = function () {
           
            if (lstab == 'pending') {
                $location.url('app/SysMstEmployeePendingSummary');
            }
            else if (lstab == 'active') {
                $state.go('app.SysMstEmployeeActiveUserSummary');
            }
            else if (lstab == 'inactive') {
                $state.go('app.SysMstEmployeeInactiveSummary');
            }
            else if (lstab == 'relieving') {
                $state.go('app.SysMstEmployeeRelievingSummary');
            }
            else {
                $state.go('app.SysMstEmployeeSummary');   
            }
        };
    }
})();

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('SysMstGroupBusinessHeadController', SysMstGroupBusinessHeadController);

    SysMstGroupBusinessHeadController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function SysMstGroupBusinessHeadController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'SysMstGroupBusinessHeadController';
        $scope.lsoneapipage = $location.search().lsoneapipage;
        var lsoneapipage = $scope.lsoneapipage;

        activate();

        function activate() {
            var url = 'api/SystemMaster/GetGroupBusinessHeadSummary';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.groupbusinesshead_list = resp.data.businesshead_list;
                unlockUI();
            });
        }

        $scope.addGroupBusinessHead = function () {
            var modalInstance = $modal.open({
                templateUrl: '/addgroupbusinesshead.html',
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

                var url = 'api/SystemMaster/GetZoneList';
                lockUI();
                SocketService.get(url).then(function (resp) {
                    $scope.zone_list = resp.data.zone_list;
                    unlockUI();
                });
                var url = 'api/SystemMaster/GetVerticallist';
                lockUI();
                SocketService.get(url).then(function (resp) {
                    $scope.vertical_list = resp.data.vertical_list;
                    unlockUI();
                });
                var url = 'api/SystemMaster/GetEmployeelist';
                lockUI();
                SocketService.get(url).then(function (resp) {
                    $scope.employee_list = resp.data.employeelist;
                    unlockUI();
                });
                $scope.submit = function () {

                    var params = {
                        zone_gid: $scope.cbozone.zone_gid,
                        zone_name: $scope.cbozone.zone_name,
                        vertical_gid: $scope.cbovertical.vertical_gid,
                        vertical_name: $scope.cbovertical.vertical_name,
                        employee_gid: $scope.cboemployee.employee_gid,
                        employee_name: $scope.cboemployee.employee_name
                    }

                    var url = 'api/SystemMaster/PostGroupBusinessHeadAdd';
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

        $scope.editgroupbusinesshead = function (groupbusinesshead_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/editgroupbusinesshead.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });

            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    groupbusinesshead_gid: groupbusinesshead_gid
                }
                var url = 'api/SystemMaster/GetGroupBusinessHeadEdit';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.cbozoneedit = resp.data.zone_gid,
                    $scope.cboverticaledit = resp.data.vertical_gid,
                    $scope.cboemployeeedit = resp.data.employee_gid,
                    $scope.zone_list = resp.data.zone_list,
                    $scope.vertical_list = resp.data.vertical_list,
                    $scope.employee_list = resp.data.employeelist
                });


                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.update = function () {
                    var zonename;
                    var zone_index = $scope.zone_list.map(function (e) { return e.zone_gid }).indexOf($scope.cbozoneedit);
                    if (zone_index == -1) { zonename = ''; } else { zonename = $scope.zone_list[zone_index].zone_name; };

                    var verticalname;
                    var vertical_index = $scope.vertical_list.map(function (e) { return e.vertical_gid }).indexOf($scope.cboverticaledit);
                    if (vertical_index == -1) { verticalname = ''; } else { verticalname = $scope.vertical_list[vertical_index].vertical_name; };

                    var employeename;
                    var employee_index = $scope.employee_list.map(function (e) { return e.employee_gid }).indexOf($scope.cboemployeeedit);
                    if (employee_index == -1) { employeename = ''; } else { employeename = $scope.employee_list[employee_index].employee_name; };

                    var url = 'api/SystemMaster/PostGroupBusinessHeadUpdate';
                    var params = {
                        zone_gid: $scope.cbozoneedit,
                        zone_name: zonename,
                        vertical_gid: $scope.cboverticaledit,
                        vertical_name: verticalname,
                        employee_gid: $scope.cboemployeeedit,
                        employee_name: employeename,
                        groupbusinesshead_gid: groupbusinesshead_gid
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

        $scope.Status_update = function (groupbusinesshead_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/groupbusinessheadstatus.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                var params = {
                    groupbusinesshead_gid: groupbusinesshead_gid
                }
                var url = 'api/SystemMaster/GetGroupBusinessHeadEdit';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.groupbusinesshead_gid = resp.data.groupbusinesshead_gid
                    $scope.txtemployee_name = resp.data.employee_name;
                    $scope.txtemployee_gid = resp.data.employee_gid;
                    $scope.rbo_status = resp.data.businesshead_status;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.update_status = function () {

                    var params = {
                        groupbusinesshead_gid: groupbusinesshead_gid,
                        employee_name: $scope.txtemployee_name,
                        employee_gid: $scope.txtemployee_gid,
                        remarks: $scope.txtremarks,
                        rbo_status: $scope.rbo_status

                    }
                    var url = 'api/SystemMaster/PostGroupBusinessHeadInactive';
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
                    groupbusinesshead_gid: groupbusinesshead_gid
                }

                var url = 'api/SystemMaster/GetGroupBusinessHeadInactiveLogview';
                lockUI();
                SocketService.getparams(url, param).then(function (resp) {
                    $scope.groupbusinessheadinactivelog_list = resp.data.master_list;
                    unlockUI();
                });

            }
        }


    }
})();


(function () {
    'use strict';

    angular
        .module('angle')
        .controller('SysMstHRDocumentController', SysMstHRDocumentController);

    SysMstHRDocumentController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function SysMstHRDocumentController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'SysMstHRDocumentController';
        $scope.lsoneapipage = $location.search().lsoneapipage;
        var lsoneapipage = $scope.lsoneapipage;

        activate();

        function activate() {
            var url = 'api/SysMstHRDocument/GetSysHRDocument';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.hrdocument_data = resp.data.hrdocument_list;
                unlockUI();
            });
        }
        $scope.addhrdocument = function () {
            var modalInstance = $modal.open({
                templateUrl: '/addhrdocument.html',
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
                $scope.submit = function () {

                    var params = {
                        hrdocument_name: $scope.txthrdocument_name,
                        lms_code: $scope.txtlms_code,
                        bureau_code: $scope.txtbureau_code

                    }
                    var url = 'api/SysMstHRDocument/CreateSysHRDocument';
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
        $scope.edithrdocument = function (hrdocument_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/edithrdocument.html',
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
                var params = {
                    hrdocument_gid: hrdocument_gid
                }
                var url = 'api/SysMstHRDocument/EditSysHRDocument';
                SocketService.getparams(url, params).then(function (resp) {

                    $scope.txtedithrdocument_name = resp.data.hrdocument_name;
                    $scope.txteditlms_code = resp.data.lms_code;
                    $scope.txteditbureau_code = resp.data.bureau_code;
                    $scope.hrdocument_gid = resp.data.hrdocument_gid;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.update = function () {

                    var url = 'api/SysMstHRDocument/UpdateSysHRDocument';
                    var params = {
                        hrdocument_name: $scope.txtedithrdocument_name,
                        lms_code: $scope.txteditlms_code,
                        bureau_code: $scope.txteditbureau_code,
                        hrdocument_gid: $scope.hrdocument_gid
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
                            activate();
                        }
                    });
                }

            }
        }

        $scope.Status_update = function (hrdocument_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/statushrdocument.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    hrdocument_gid: hrdocument_gid
                }
                var url = 'api/SysMstHRDocument/EditSysHRDocument';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.hrdocument_gid = resp.data.hrdocument_gid
                    $scope.txthrdocument_name = resp.data.hrdocument_name;
                    $scope.rbo_status = resp.data.Status;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.update_status = function () {

                    var params = {
                        hrdocument_gid: hrdocument_gid,
                        hrdocument_name: $scope.txthrdocument_name,
                        remarks: $scope.txtremarks,
                        rbo_status: $scope.rbo_status

                    }
                    var url = 'api/SysMstHRDocument/InactiveSysHRDocument';
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
                        } activate();
                    });

                    $modalInstance.close('closed');

                }
                var params = {
                    hrdocument_gid: hrdocument_gid
                }

                var url = 'api/SysMstHRDocument/InactiveSysHRDocumentHistory';

                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.hrdocumentinactivelog_data = resp.data.hrdocumentinactivehistory_list;
                    unlockUI();
                });
            }
        }

        $scope.delete = function (hrdocument_gid) {
            var params = {
                hrdocument_gid: hrdocument_gid
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
                    var url = 'api/SysMstHRDocument/DeleteSysHRDocument';
                    SocketService.getparams(url, params).then(function (resp) {
                        unlockUI();
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
                            unlockUI();
                        }
                    });
                }
            });
        }
    }
})();
(function () {
    'use strict';

    angular
        .module('angle')
        .controller('SysMstHRNotificationController', SysMstHRNotificationController);

        SysMstHRNotificationController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function SysMstHRNotificationController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'SysMstHRNotificationController';
        $scope.lsoneapipage = $location.search().lsoneapipage;
        var lsoneapipage = $scope.lsoneapipage;

        activate();

        function activate() {
            var url = 'api/SystemMaster/GetHRNotificationSummary';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.hrnotification_list = resp.data.master_list;
                unlockUI();
            }); 

            }
        


        $scope.addhrnotification = function () {
            var modalInstance = $modal.open({
                templateUrl: '/addhrnotification.html',
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
                    $scope.employee_list = resp.data.employeelist;
                    unlockUI();
                });
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.submit = function () {

                    var params = {
                        hrnotification_code: $scope.txthrnotification_code,
                        application_name: $scope.txtapplication_name,
                        notify_to: $scope.cbonotifyto_add
                    }
                    var url = 'api/SystemMaster/PostHRNotification';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
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
                    unlockUI();
                   
                    Notify.alert(resp.data.message, {
                        status: 'warning',
                        pos: 'top-center',
                        timeout: 3000
                    });
                        }
                        // activate();
                        // unlockUI;
                    });
                

                    $modalInstance.close('closed');

                }
                
            }
        }
        $scope.edithrnotification = function (hrnotification_gid) 
        {
            var modalInstance = $modal.open({
                templateUrl: '/edithrnotification.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                    

                var params = {
                    hrnotification_gid: hrnotification_gid
                }
                var url = 'api/SystemMaster/EditHRNotification';
                SocketService.getparams(url, params).then(function (resp) {

                    $scope.txtedithrnotification_code = resp.data.hrnotification_code;
                    $scope.txteditapplication_name = resp.data.application_name;
                    $scope.hrnotification_gid = resp.data.hrnotification_gid;
                    $scope.notifyto_general = resp.data.notifyto_general;
                $scope.cbonotifyto_edit = [];
                if (resp.data.notify_to != null) {
                    var count = resp.data.notify_to.length;
                    for (var i = 0; i < count; i++) {
                        //var indexs = $scope.notifyto_general.findIndex(x => x.employee_gid === resp.data.notify_to[i].employee_gid);
                        var indexs = $scope.notifyto_general.map(function (x) { return x.employee_gid; }).indexOf(resp.data.notify_to[i].employee_gid);
                        $scope.cbonotifyto_edit.push($scope.notifyto_general[indexs]);
                        $scope.$parent.cbonotifyto_edit = $scope.cbonotifyto_edit;
                    }
                }

                $scope.notifyto_general = resp.data.notifyto_general;
                $scope.cbonotifyto_edit = [];
                if (resp.data.notify_to != null) {
                    var count = resp.data.notify_to.length;
                    for (var i = 0; i < count; i++) {
                        //var indexs = $scope.notifyto_general.findIndex(x => x.employee_gid === resp.data.notify_to[i].employee_gid);
                        var indexs = $scope.notifyto_general.map(function (x) { return x.employee_gid; }).indexOf(resp.data.notify_to[i].employee_gid);
                        $scope.cbonotifyto_edit.push($scope.notifyto_general[indexs]);
                        $scope.$parent.cbonotifyto_edit = $scope.cbonotifyto_edit;
                    }
                }

                }); 
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.update = function () {

                    var url = 'api/SystemMaster/UpdateHRNotification';
                    var params = {
                        hrnotification_code: $scope.txtedithrnotification_code,
                        application_name: $scope.txteditapplication_name,
                        notify_to: $scope.cbonotifyto_edit,
                        hrnotification_gid: $scope.hrnotification_gid
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
        $scope.showPopover = function (hrnotification_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/shownotifytoemployeelist.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    hrnotification_gid: hrnotification_gid
                }
                lockUI();
                var url = 'api/SystemMaster/GetHRNotificationNotifyToList';
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();                  
                    $scope.notifyto_name = resp.data.notifyto_name;
                });
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
            }
        }
       
        $scope.Status_update = function (hrnotification_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/statushrnotification.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                var params = {
                    hrnotification_gid: hrnotification_gid
                }
                var url = 'api/SystemMaster/EditHRNotification';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.hrnotification_gid = resp.data.hrnotification_gid
                    $scope.txtapplication_name = resp.data.application_name;
                    $scope.rbo_status = resp.data.Status;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.update_status = function () {

                    var params = {
                        hrnotification_gid: hrnotification_gid,
                        application_name: $scope.txtapplication_name,
                        remarks: $scope.txtremarks,
                        rbo_status: $scope.rbo_status

                    }
                    var url = 'api/SystemMaster/InactiveHRNotification';
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
                    hrnotification_gid: hrnotification_gid
                }
                var url = 'api/SystemMaster/HRNotificationInactiveLogview';
                lockUI();
                SocketService.getparams(url, param).then(function (resp) {
                    $scope.hrnotificationinactivelog_list = resp.data.master_list;
                    unlockUI();
                });
            }
        } 
        $scope.DeleteHRNotification = function(hrnotification_gid) {
            var params = {
                hrnotification_gid: hrnotification_gid
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
                    var url = 'api/SystemMaster/DeleteHRNotification';
                    SocketService.getparams(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {
                            SweetAlert.swal('Deleted Successfully!');
                            activate();
                        }
                        else {
                            alert(resp.data.message, {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            activate();
                            unlockUI;
                        }
                    });
                    }
            


                });
            }

    }
})();    
(function () {
    'use strict';

    angular
        .module('angle')
        .controller('SysMstMenuMappingController', SysMstMenuMappingController);

    SysMstMenuMappingController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function SysMstMenuMappingController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'SysMstMenuMappingController';
        $scope.lsoneapipage = $location.search().lsoneapipage;
        var lsoneapipage = $scope.lsoneapipage;

        activate();

        function activate() {

            var url = 'api/SystemMaster/GetMenuMappingSummary';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.menusummary_list = resp.data.menusummary_list;
                unlockUI();
            });
        }

        $scope.addmenu = function () {
            var modalInstance = $modal.open({
                templateUrl: '/addmenu.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                var url = 'api/SystemMaster/GetFirstLevelMenu';
                lockUI();
                SocketService.get(url).then(function (resp) {
                    $scope.levelonemenu_list = resp.data.menu_list;
                    unlockUI();
                });
                $scope.getSecondlevel = function (levelone)
                {
                    var url = 'api/SystemMaster/GetSecondLevelMenu';
                    var params = {
                        module_gid_parent: levelone.module_gid
                    }
                        SocketService.getparams(url, params).then(function (resp) {
                            $scope.leveltwomenu_list = resp.data.menu_list;
                            unlockUI();
                    });
                }
                $scope.getThirdevel = function (leveltwo) {
                    var url = 'api/SystemMaster/GetThirdLevelMenu';
                    var params = {
                        module_gid_parent: leveltwo.module_gid
                    }
                    SocketService.getparams(url, params).then(function (resp) {
                        $scope.levelthreemenu_list = resp.data.menu_list;
                        unlockUI();
                    });
                }
                $scope.getFourthevel = function (levelthree) {
                    var url = 'api/SystemMaster/GetFourthLevelMenu';
                    var params = {
                        module_gid_parent: levelthree.module_gid
                    }
                    SocketService.getparams(url, params).then(function (resp) {
                        $scope.levelfourmenu_list = resp.data.menu_list;
                        unlockUI();
                    });
                }
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.submit = function () {

                    var params = {
                        module_gid: $scope.levelfour.module_gid,
                        module_name: $scope.levelfour.module_name

                    }
                    console.log(params);
                    var url = 'api/SystemMaster/PostMenudAdd';
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

        //$scope.editsaentitytype = function (saentitytype_gid) {
        //    var modalInstance = $modal.open({
        //        templateUrl: '/editsaentitytype.html',
        //        controller: ModalInstanceCtrl,
        //        backdrop: 'static',
        //        keyboard: false,
        //        size: 'md'
        //    });
        //    ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
        //    function ModalInstanceCtrl($scope, $modalInstance) {

        //        var url = 'api/MstApplication360/SATypeList';
        //        lockUI();
        //        SocketService.get(url).then(function (resp) {
        //            $scope.satype_list = resp.data.satype_list;
        //            unlockUI();
        //        });
        //        var params = {
        //            saentitytype_gid: saentitytype_gid
        //        }
        //        var url = 'api/MstApplication360/EditSAEntityType';
        //        SocketService.getparams(url, params).then(function (resp) {
        //            $scope.satype_Name = resp.data.satype_name;
        //            $scope.satype_Gid = resp.data.satype_gid;

        //            $scope.txteditsaentitytype_name = resp.data.saentitytype_name;
        //            $scope.txteditlms_code = resp.data.lms_code;
        //            $scope.txteditbureau_code = resp.data.bureau_code;
        //            $scope.saentitytype_gid = resp.data.saentitytype_gid;
        //        });
        //        $scope.titlename = function (string) {
        //            if (string.length >= 255) {
        //                $scope.message = "Allowed Only 255 Characters";
        //            }
        //            else {
        //                $scope.message = "";
        //            }
        //        }
        //        $scope.lmslength = function (string) {
        //            if (string.length >= 30) {
        //                $scope.lmsmessage = "Allowed Only 30 Characters";
        //            }
        //            else {
        //                $scope.lmsmessage = "";
        //            }
        //        }
        //        $scope.bureaulength = function (string) {
        //            if (string.length >= 10) {
        //                $scope.bureaumessage = "Allowed Only 10 Characters";
        //            }
        //            else {
        //                $scope.bureaumessage = "";
        //            }
        //        }
        //        $scope.ok = function () {
        //            $modalInstance.close('closed');
        //        };

        //        $scope.update = function () {
        //            var satype_name = $('#satype_Name :selected').text();
        //            var url = 'api/MstApplication360/UpdateSAEntityType';
        //            var params = {
        //                saentitytype_name: $scope.txteditsaentitytype_name,
        //                lms_code: $scope.txteditlms_code,
        //                bureau_code: $scope.txteditbureau_code,
        //                saentitytype_gid: $scope.saentitytype_gid,
        //                satype_name: satype_name,
        //                satype_gid: $scope.satype_Gid
        //            }
        //            SocketService.post(url, params).then(function (resp) {
        //                if (resp.data.status == true) {
        //                    $modalInstance.close('closed');
        //                    Notify.alert(resp.data.message, {
        //                        status: 'success',
        //                        pos: 'top-center',
        //                        timeout: 3000
        //                    });
        //                    activate();

        //                }
        //                else {
        //                    $modalInstance.close('closed');
        //                    Notify.alert(resp.data.message, {
        //                        status: 'warning',
        //                        pos: 'top-center',
        //                        timeout: 3000
        //                    });
        //                    activate();
        //                }
        //            });

        //        }
        //    }
        //}

        $scope.Status_update = function (menu_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/statusmenumapping.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    menu_gid: menu_gid
                }
                var url = 'api/SystemMaster/GetMenuMappingEdit';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.txtmodule_name = resp.data.module_name;
                    $scope.txtmodule_gid = resp.data.module_gid;
                    $scope.rbo_status = resp.data.Status;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.update_status = function () {

                    var params = {
                        module_name: $scope.txtmodule_name,
                        menu_gid: menu_gid,
                        remarks: $scope.txtremarks,
                        rbo_status: $scope.rbo_status

                    }
                    var url = 'api/SystemMaster/GetMenuMappingInactivate';
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

                var url = 'api/SystemMaster/GetMenuMappingInactivateview';

                var param = {
                    menu_gid: menu_gid
                }

                SocketService.getparams(url, param).then(function (resp) {
                   $scope.menuinactivelog_list = resp.data.menusummary_list;
                });
            }
        }

        $scope.delete = function (menu_gid) {
            var params = {
                menu_gid: menu_gid
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
                    var url = 'api/SystemMaster/GetMenuMappingDelete';
                    SocketService.getparams(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {
                            SweetAlert.swal(resp.data.message);
                            activate();
                        }
                        else {
                            alert(resp.data.message, {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            activate();
                            unlockUI;
                        }
                    });
                }
            });
        }
    }
})();



(function () {
    'use strict';

    angular
        .module('angle')
        .controller('SysMstMyOnboardingProcessController',SysMstMyOnboardingProcessController);

        SysMstMyOnboardingProcessController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function SysMstMyOnboardingProcessController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
      
        var vm = this;
        vm.title = 'SysMstMyOnboardingProcessController';

        $scope.employee_gid = $location.search().lsemployee_gid;
        var employee_gid = $scope.employee_gid;
        $scope.taskinitiate_gid = $location.search().lstaskinitiate_gid;
        var taskinitiate_gid = $scope.taskinitiate_gid;      
        $scope.lspage = $location.search().lspage;
        var lspage = $scope.lspage;


        activate();
        var lstab = $location.search().lstab;
        $scope.lstab = lstab;
        lockUI();
        function activate() {
          
            var param = {
                employee_gid: employee_gid
            };
            var url = 'api/ManageEmployee/EmployeePendingEditView';                                 
            SocketService.getparams(url, param).then(function (resp) {             
                $scope.employee_details = resp.data;           
            });
            unlockUI();
            var param = {
                employee_gid: employee_gid,
                taskinitiate_gid: taskinitiate_gid
            };
            var url = 'api/ManageEmployee/GetCompleteflag';
          
            SocketService.getparams(url, param).then(function (resp) { 
                $scope.complete_flag = resp.data.complete_flag;
                
                unlockUI();
            });
            
            var url = 'api/ManageEmployee/GetTaskDetails';
            lockUI();
            SocketService.getparams(url, param).then(function (resp) { 
                $scope.task_details = resp.data;
              
                unlockUI();
            });
          
            }

        // $scope.view_cancel = function () {                     
        //     $state.go('app.SysMstMyOnboardingTaskPending');                               
        //     };
            $scope.view_cancel = function () {
                if (lspage == 'TaskPending') {
                    $location.url('app/SysMstMyOnboardingTaskPending?employee_gid=' + employee_gid );
                }
                else if (lspage == 'TaskCompleted') {
                    $location.url('app/SysMstMyOnboardingTaskCompleted');
                }
                else {
    
                }
            }

            $scope.taskcompleted = function () {

                if ( ($scope.txttaskcompleteremarks == ''|| $scope.txttaskcompleteremarks == undefined)) {
                    Notify.alert(' Enter Complete Remarks','warning');
                }
                else {
                    var params = {
                        employee_gid: employee_gid,
                        task_completeremarks: $scope.txttaskcompleteremarks,
                        taskinitiate_gid: taskinitiate_gid
                    }
                    
                    var url = "api/ManageEmployee/CompleteTask";
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status = true) {
                            Notify.alert(resp.data.message, 'success');
                            $location.url('app/SysMstMyOnboardingTaskPending');
                        }
                        else {
                            Notify.alert(resp.data.message, 'warning');
                            activate();
                        }
                    });
        
                }
            }


    }
})();

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('SysMstMyOnboardingTaskCompletedController', SysMstMyOnboardingTaskCompletedController);

        SysMstMyOnboardingTaskCompletedController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function SysMstMyOnboardingTaskCompletedController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'SysMstMyOnboardingTaskCompletedController';
        var lspage = 'TaskCompleted'
        // var employee_gid = $location.search().employee_gid;
        activate();

        function activate() {
            var url = 'api/ManageEmployee/GetMyTaskCompleteSummary';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.mytask_list = resp.data.tasksummarylist;
                
                unlockUI();
            });

            var url = "api/ManageEmployee/GetMyTashCount";
            SocketService.get(url).then(function (resp) {               
                $scope.completed_count = resp.data.completed_count;              
                $scope.pending_count = resp.data.pending_count;
                unlockUI();
            });

            
        }
    //     $scope.process = function () {
    //         $state.go('app.SysMstMyOnboardingProcess');           
    // }


    $scope.PendingTask = function () {
        $state.go('app.SysMstMyOnboardingTaskPending');
    }      

    // Tagged Request
    $scope.Completed = function () {
        $state.go('app.SysMstMyOnboardingTaskCompleted');
    }


    $scope.process = function (employee_gid , taskinitiate_gid) {
        $location.url('app/SysMstMyOnboardingProcess?lsemployee_gid=' + employee_gid +  '&lstaskinitiate_gid=' + taskinitiate_gid + '&lspage='+ lspage);
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


(function () {
    'use strict';

    angular
        .module('angle')
        .controller('SysMstMyOnboardingTaskPendingController', SysMstMyOnboardingTaskPendingController);

        SysMstMyOnboardingTaskPendingController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function SysMstMyOnboardingTaskPendingController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'SysMstMyOnboardingTaskPendingController';
        $scope.lsoneapipage = $location.search().lsoneapipage;
        var lsoneapipage = $scope.lsoneapipage;

        var lspage = 'TaskPending'
        // var employee_gid = $location.search().employee_gid;
        activate();

        function activate() {
            var url = 'api/ManageEmployee/GetMyTaskPendingSummary';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.mytask_list = resp.data.tasksummarylist;
                
                unlockUI();
            });

            var url = "api/ManageEmployee/GetMyTashCount";
            SocketService.get(url).then(function (resp) {               
                $scope.completed_count = resp.data.completed_count;              
                $scope.pending_count = resp.data.pending_count;
                unlockUI();
            });
        }
    //     $scope.process = function () {
    //         $state.go('app.SysMstMyOnboardingProcess');           
    // }


    $scope.PendingTask = function () {
        $state.go('app.SysMstMyOnboardingTaskPending');
    }      

    // Tagged Request
    $scope.Completed = function () {
        $state.go('app.SysMstMyOnboardingTaskCompleted');
    }


    $scope.process = function (employee_gid, taskinitiate_gid) {
        $location.url('app/SysMstMyOnboardingProcess?lsemployee_gid=' + employee_gid +  '&lstaskinitiate_gid=' + taskinitiate_gid + '&lspage='+ lspage );
    }


  $scope.pslcsacompleted = function () {

            if ( ($scope.txtpslqueries == ''|| $scope.txtpslqueries == undefined)) {
                Notify.alert(' Enter Complete Remarks','warning');
            }
            else {
                var params = {
                    application_gid: application_gid,
                    pslcompleteremarks: $scope.txtpslqueries
                }
                
                var url = "api/MstCAD/UpdatePSLCompleted";
                lockUI();
                SocketService.post(url, params).then(function (resp) {
                    unlockUI();
                    if (resp.data.status = true) {
                        Notify.alert(resp.data.message, 'success');
                        $location.url('app/MstPSLCSAManagement');
                    }
                    else {
                        Notify.alert(resp.data.message, 'warning');
                        activate();
                    }
                });

            }
        }


    }
})();



(function () {
    'use strict';

    angular
        .module('angle')
        .controller('SysMstOtherApplicationController', SysMstOtherApplicationController);

        SysMstOtherApplicationController.$inject = ['$rootScope', '$scope', '$state', '$modal', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams'];

    function SysMstOtherApplicationController($rootScope, $scope, $state, $modal, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'SysMstOtherApplicationController';
        $scope.lsoneapipage = $location.search().lsoneapipage;
        var lsoneapipage = $scope.lsoneapipage;

        activate();

        function activate() {
            lockUI();
            var url = 'api/OtherApplication/GetOtherApplication';
            SocketService.get(url).then(function (resp) {
                $scope.otherapplication_list = resp.data.otherapplication_list;
            });
            unlockUI();
        } 
        $scope.addOtherApplication = function () {
            var modalInstance = $modal.open({
                templateUrl: '/addOtherApplication.html',
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
                $scope.submit = function () {
                    var params = {
                        otherapplication_name: $scope.txtOtherApplication_name,
                        url: $scope.txturl,
                        assign_status: $scope.rboassign_status,
                        description: $scope.txtdescription
                    }
                    var url = 'api/OtherApplication/CreateOtherApplication';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
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
                            Notify.alert(resp.data.message, {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            activate();
                        }
                    });
                    $modalInstance.close('closed');
                }
                
            }
        }
        $scope.editOtherApplication = function (otherapplication_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/editOtherApplication.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    otherapplication_gid: otherapplication_gid
                }
                var url = 'api/OtherApplication/EditOtherApplication';
                SocketService.getparams(url, params).then(function (resp) {

                    $scope.txteditotherapplication_name = resp.data.otherapplication_name;
                    $scope.txtediturl = resp.data.url;
                    $scope.txteditdescription = resp.data.description;
                    $scope.otherapplication_gid = resp.data.otherapplication_gid;
                });
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.update = function () {
                    var url = 'api/OtherApplication/UpdateOtherApplication';
                    var params = {
                        otherapplication_name: $scope.txteditotherapplication_name,
                        url: $scope.txtediturl,
                        description: $scope.txteditdescription,
                        otherapplication_gid: $scope.otherapplication_gid
                    }
                    SocketService.post(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
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
                    });$modalInstance.close('closed');
                }
            }
        }
        $scope.updatestatus = function (otherapplication_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/statusOtherApplication.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    otherapplication_gid: otherapplication_gid
                }
                var url = 'api/OtherApplication/EditOtherApplication';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.otherapplication_gid = resp.data.otherapplication_gid
                    $scope.otherapplication_name = resp.data.otherapplication_name;
                    $scope.rbo_status = resp.data.Status;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.update_status = function () {

                    var params = {
                        otherapplication_name :$scope.otherapplication_name,
                        otherapplication_gid: otherapplication_gid,
                        remarks: $scope.txtremarks,
                        rbo_status: $scope.rbo_status

                    }
                    var url = 'api/OtherApplication/InactiveOtherApplication';
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
                    otherapplication_gid: otherapplication_gid
                }

                var url = 'api/OtherApplication/InactiveOtherApplicationHistory';
                lockUI();
                SocketService.getparams(url, param).then(function (resp) {
                    $scope.otherapplicationinactivelog_data = resp.data.otherapplication_list;
                    unlockUI();
                });

            }
        }
        $scope.assignmember = function (otherapplication_gid) {
           
            var modalInstance = $modal.open({
                templateUrl: '/assignmembermodal.html',
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
                $scope.checkall = function (selected) {
                    angular.forEach($scope.employee_list, function (val) {
                        val.checked = selected;
                    });
                }
                $scope.checkallnew = function (selected) {
                    angular.forEach($scope.member_list, function (val) {
                        val.checked = selected;
                    });
                }
                var params = {
                    otherapplication_gid: otherapplication_gid
                }
                var url = 'api/OtherApplication/Employee';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.employee_list = resp.data.employeeasssign_list;
                });
                var url = 'api/OtherApplication/AssignedEmployee';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.member_list = resp.data.employeeasssign_list;
                });
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
                        otherapplication_gid: otherapplication_gid,
                        employeelist_gid: employeelistGId
                    }
                    unlockUI();
                    if (employee_gid != undefined) {
                        var url = 'api/OtherApplication/Assignmember';
                        lockUI();
                        SocketService.post(url, params).then(function (resp) {

                            if (resp.data.status == true) {
                              
                                var params = {
                                    otherapplication_gid: otherapplication_gid
                                }
                                var url = 'api/OtherApplication/Employee';
                                SocketService.getparams(url, params).then(function (resp) {
                                    $scope.employee_list = resp.data.employeeasssign_list;
                                });
                                var url = 'api/OtherApplication/AssignedEmployee';
                                SocketService.getparams(url, params).then(function (resp) {
                                $scope.member_list = resp.data.employeeasssign_list;
                                });
                                unlockUI();
                                Notify.alert(resp.data.message, {
                                    status: 'success',
                                    pos: 'top-center',
                                    timeout: 3000
                                });
                                alert('Member Assigned Successfully!', 'success');

                            }
                            else {
                                unlockUI();
                                alert(resp.data.message, 'warning');
                            }

                        });
                    }
                    else {
                        alert('Select Atleast One Employee!');
                    }
                }
                $scope.unassign = function () {
                    lockUI();
                    var employee_gid;
                    var employeelistGId = [];
                    angular.forEach($scope.member_list, function (val) {

                        if (val.checked == true) {
                            var employeelist_gid = val.employee_gid;
                            employee_gid = val.employee_gid;
                            employeelistGId.push(employeelist_gid);
                        }
                    });
                    unlockUI();
                    var url = "api/OtherApplication/GetAssignmemberDelete";
                    var params = {
                        employeelist_gid: employeelistGId,
                        otherapplication_gid: otherapplication_gid
                    };
                    lockUI();
                    if (employee_gid != undefined){
                        unlockUI();
                    SocketService.post(url, params).then(function (resp) {
        
                        if (resp.data.status == true) {
                           
                            var params = {
                                otherapplication_gid: otherapplication_gid   
                            }
                            var url = 'api/OtherApplication/Employee';
                            lockUI();
                            SocketService.getparams(url, params).then(function (resp) {
                                $scope.employee_list = resp.data.employeeasssign_list;
                            });
                            var url = 'api/OtherApplication/AssignedEmployee';
                            SocketService.getparams(url, params).then(function (resp) {
                                $scope.member_list = resp.data.employeeasssign_list;
                            });
                            unlockUI();
                            alert('Member UnAssigned Successfully!');
        
                        }
                        else {
                            unlockUI();
                            alert(resp.data.message);
                        }
        
                    });
                }
                else {
                    alert('Select Atleast One Employee!');
                }
                }
            }
        }
        $scope.description= function (description){
            var modalInstance = $modal.open({
                templateUrl: '/description.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.description=description;
                $scope.back = function () {
                    $modalInstance.close('closed');
                }; 
            }
        }
    }
})();

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('SysMstPhysicalStatusController', SysMstPhysicalStatusController);

    SysMstPhysicalStatusController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function SysMstPhysicalStatusController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'SysMstPhysicalStatusController';
        $scope.lsoneapipage = $location.search().lsoneapipage;
        var lsoneapipage = $scope.lsoneapipage;

        activate();

        function activate() {
            var url = 'api/SystemMaster/GetPhysicalStatus';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.physicalstatus_list = resp.data.master_list;
                unlockUI();
            });
        }

        $scope.addphysicalstatus = function () {
            var modalInstance = $modal.open({
                templateUrl: '/addphysicalstatus.html',
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
                $scope.submit = function () {

                    var params = {
                        physicalstatus_name: $scope.txtphysical_status,
                        lms_code: $scope.txtlms_code,
                        bureau_code: $scope.txtbureau_code

                    }
                    var url = 'api/SystemMaster/CreatePhysicalStatus';
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
        $scope.editphysicalstatus = function (physicalstatus_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/editphysicalstatus.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    physicalstatus_gid: physicalstatus_gid
                }
                var url = 'api/SystemMaster/EditPhysicalStatus';
                SocketService.getparams(url, params).then(function (resp) {

                    $scope.txteditphysical_status = resp.data.physicalstatus_name;
                    $scope.txteditlms_code = resp.data.lms_code;
                    $scope.txteditbureau_code = resp.data.bureau_code;
                    $scope.physicalstatus_gid = resp.data.physicalstatus_gid;
                });


                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.update = function () {

                    var url = 'api/SystemMaster/UpdatePhysicalStatus';
                    var params = {
                        physicalstatus_name: $scope.txteditphysical_status,
                        lms_code: $scope.txteditlms_code,
                        bureau_code: $scope.txteditbureau_code,
                        physicalstatus_gid: $scope.physicalstatus_gid
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

        $scope.Status_update = function (physicalstatus_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/statusphysicalstatus.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                var params = {
                    physicalstatus_gid: physicalstatus_gid
                }
                var url = 'api/SystemMaster/EditPhysicalStatus';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.physicalstatus_gid = resp.data.physicalstatus_gid
                    $scope.txtphysical_status = resp.data.physicalstatus_name;
                    $scope.rbo_status = resp.data.Status;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.update_status = function () {

                    var params = {
                        physicalstatus_gid: physicalstatus_gid,
                        remarks: $scope.txtremarks,
                        rbo_status: $scope.rbo_status

                    }
                    var url = 'api/SystemMaster/InactivePhysicalStatus';
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
                    physicalstatus_gid: physicalstatus_gid
                }

                var url = 'api/SystemMaster/PhysicalStatusInactiveLogview';
                lockUI();
                SocketService.getparams(url, param).then(function (resp) {
                    $scope.physicalstatusinactivelog_list = resp.data.master_list;
                    unlockUI();
                });

            }
        }

        $scope.delete = function (physicalstatus_gid) {
            var params = {
                physicalstatus_gid: physicalstatus_gid
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
                    var url = 'api/SystemMaster/DeletePhysicalStatus';
                    SocketService.getparams(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            activate();
                        }
                        else {
                            Notify.alert('Error Occurred While Deleting Physical Status!', {
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


(function () {
    'use strict';

    angular
        .module('angle')
        .controller('SysMstProductHeadController', SysMstProductHeadController);

    SysMstProductHeadController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function SysMstProductHeadController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'SysMstProductHeadController';
        $scope.lsoneapipage = $location.search().lsoneapipage;
        var lsoneapipage = $scope.lsoneapipage;

        activate();

        function activate() { 

            var url = 'api/SystemMaster/GetProductHeadSummary';
        lockUI();
        SocketService.get(url).then(function (resp) {
            $scope.producthead_list = resp.data.producthead_list;
            unlockUI();
        });
    }

    $scope.addproducthead = function () {
        var modalInstance = $modal.open({
            templateUrl: '/addproducthead.html',
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

            var url = 'api/SystemMaster/GetZonallist';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.zone_list = resp.data.zone_list;
                unlockUI();
            });
         
            var url = 'api/SystemMaster/GetEmployeelist';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.employee_list = resp.data.employeelist;
                unlockUI();
            });

            $scope.submit = function () {

                var params = {
                    product_gid: $scope.cboproduct.zone_gid,
                    product_name: $scope.cboproduct.zone_name,
                    employee_gid: $scope.cboemployee.employee_gid,
                    employee_name: $scope.cboemployee.employee_name
                }
               
                var url = 'api/SystemMaster/PostProductheadAdd';
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
    $scope.editproduct = function (producthead_gid) {
        var modalInstance = $modal.open({
            templateUrl: '/editproducthead.html',
            controller: ModalInstanceCtrl,
            backdrop: 'static',
            keyboard: false,
            size: 'md'
        });

        ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
        function ModalInstanceCtrl($scope, $modalInstance) {
            var params = {
                producthead_gid: producthead_gid
            }
            var url = 'api/SystemMaster/GetProductHeadEdit';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.cboproductedit = resp.data.product_gid,
                 $scope.cboemployeeedit = resp.data.employee_gid,
                $scope.zone_list = resp.data.zone_list,
                $scope.employee_list = resp.data.employeelist
            });


            $scope.ok = function () {
                $modalInstance.close('closed');
            };

            $scope.update = function () {
                var productname;
                var product_index = $scope.zone_list.map(function (e) { return e.zone_gid }).indexOf($scope.cboproductedit);
                if (product_index == -1) { productname = ''; } else { productname = $scope.zone_list[product_index].zone_name; };

              
                var employeename;
                var employee_index = $scope.employee_list.map(function (e) { return e.employee_gid }).indexOf($scope.cboemployeeedit);
                if (employee_index == -1) { employeename = ''; } else { employeename = $scope.employee_list[employee_index].employee_name; };

                var url = 'api/SystemMaster/PostProductHeadUpdate';
                var params = {
                    product_gid: $scope.cboproductedit,
                    product_name: productname,
                    employee_gid: $scope.cboemployeeedit,
                    employee_name: employeename,
                    producthead_gid: producthead_gid
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

    $scope.Status_update = function (producthead_gid) {
        var modalInstance = $modal.open({
            templateUrl: '/statusproducthead.html',
            controller: ModalInstanceCtrl,
            backdrop: 'static',
            keyboard: false,
            size: 'md'
        });
        ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
        function ModalInstanceCtrl($scope, $modalInstance) {
            var params = {
                producthead_gid: producthead_gid
            }
            var url = 'api/SystemMaster/GetProductHeadEdit';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.txtproducthead_name = resp.data.employee_name,
                $scope.rbo_status = resp.data.producthead_status

            });



            $scope.ok = function () {
                $modalInstance.close('closed');
            };
            $scope.update_status = function () {

                var params = {
                    producthead_gid: producthead_gid,
                    employee_name: $scope.txtclusterhead_name,
                    remarks: $scope.txtremarks,
                    rbo_status: $scope.rbo_status

                }
                var url = 'api/SystemMaster/InactiveProducthead';
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
                producthead_gid: producthead_gid
            }

            var url = 'api/SystemMaster/ProductheadInactiveLogview';
            lockUI();
            SocketService.getparams(url, param).then(function (resp) {
                $scope.productheadinactivelog_list = resp.data.master_list;
                unlockUI();
            });

        }
    }
}
})();

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('SysMstProjectsController', SysMstProjectsController);

    SysMstProjectsController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function SysMstProjectsController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'SysMstProjectsController';
        $scope.lsoneapipage = $location.search().lsoneapipage;
        var lsoneapipage = $scope.lsoneapipage;

        activate();

        function activate() {
            var url = 'api/SystemMaster/GetProject';
           
            SocketService.get(url).then(function (resp) {
                $scope.projects_list = resp.data.master_list;
                
            });
        }

    $scope.addprojects = function () {
            var modalInstance = $modal.open({
                templateUrl: '/addprojects.html',
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
                $scope.submit = function () {

                    var params = {
                        project_name: $scope.txtproject,
                        lms_code: $scope.txtlms_code,
                        bureau_code: $scope.txtbureau_code

                    }
                    var url = 'api/SystemMaster/CreateProject';
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
    $scope.editprojects = function (project_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/editprojects.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    project_gid: project_gid
                }
                var url = 'api/SystemMaster/EditProject';
                SocketService.getparams(url, params).then(function (resp) {

                    $scope.txteditproject = resp.data.project_name;
                    $scope.txteditlms_code = resp.data.lms_code;
                    $scope.txteditbureau_code = resp.data.bureau_code;
                    $scope.project_gid = resp.data.project_gid;
                });


                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.update = function () {

                    var url = 'api/SystemMaster/UpdateProject';
                    var params = {
                        project_name: $scope.txteditproject,
                        lms_code: $scope.txteditlms_code,
                        bureau_code: $scope.txteditbureau_code,
                        project_gid: $scope.project_gid
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

    $scope.Status_update = function (project_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/statusproject.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                var params = {
                    project_gid: project_gid
                }
                var url = 'api/SystemMaster/EditProject';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.project_gid = resp.data.project_gid
                    $scope.txtproject = resp.data.project_name;
                    $scope.rbo_status = resp.data.status_project;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.update_status = function () {

                    var params = {
                        project_gid: project_gid,
                        remarks: $scope.txtremarks,
                        rbo_status: $scope.rbo_status

                    }
                    var url = 'api/SystemMaster/InactiveProject';
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
                    project_gid: project_gid
                }

                var url = 'api/SystemMaster/ProjectInactiveLogview';
                lockUI();
                SocketService.getparams(url, param).then(function (resp) {
                    $scope.projectinactivelog_list = resp.data.master_list;
                    unlockUI();
                });

            }
        }

        $scope.delete = function (project_gid) {
            var params = {
                project_gid: project_gid
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
                    var url = 'api/SystemMaster/DeleteProject';
                    SocketService.getparams(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            activate();
                        }
                        else {
                            Notify.alert('Error Occurred While Deleting Project!', {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            activate();
                        }
                    });
                    SweetAlert.swal('Deleted Successfully');
                }

            });
        };

    }
})();


(function () {
    'use strict';

    angular
        .module('angle')
        .controller('SysMstRegionHeadController', SysMstRegionHeadController);

    SysMstRegionHeadController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function SysMstRegionHeadController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'SysMstRegionHeadController';
        $scope.lsoneapipage = $location.search().lsoneapipage;
        var lsoneapipage = $scope.lsoneapipage;

        activate();

        function activate() {
            var url = 'api/SystemMaster/GetRegionHeadSummary';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.regionhead_list = resp.data.regionhead_list;
                unlockUI();
            });
        }

        $scope.addRegionHead = function () {
            var modalInstance = $modal.open({
                templateUrl: '/addregionhead.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });

            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                $scope.OnchangeVertical = function (cbovertical, cbocluster) {
                    if (cbovertical != "" && cbocluster != "") {
                        var params = {
                            vertical_gid: cbovertical,
                            lstype: 'region',
                            lstypegid: cbocluster
                        }
                        var url = 'api/SystemMaster/GetVerticalProgramList';
                        lockUI();
                        SocketService.getparams(url, params).then(function (resp) {
                            $scope.program_list = resp.data.program_list;
                            unlockUI();
                        });
                    }
                }

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                var url = 'api/SystemMaster/GetRegionList';
                lockUI();
                SocketService.get(url).then(function (resp) {
                    $scope.region_list = resp.data.region_list;
                    unlockUI();
                });
                var url = 'api/SystemMaster/GetVerticallist';
                lockUI();
                SocketService.get(url).then(function (resp) {
                    $scope.vertical_list = resp.data.vertical_list;
                    unlockUI();
                });
                var url = 'api/SystemMaster/GetEmployeelist';
                lockUI();
                SocketService.get(url).then(function (resp) {
                    $scope.employee_list = resp.data.employeelist;
                    unlockUI();
                });
                $scope.submit = function () {
                    var VerticalName = "";
                    if ($scope.vertical_list && $scope.vertical_list.length > 0) {
                        //VerticalName = $scope.vertical_list.filter(e => e.vertical_gid == $scope.cbovertical);
                        VerticalName = $scope.vertical_list.filter(function (e) { return e.vertical_gid == $scope.cbovertical });
                        VerticalName = VerticalName[0].vertical_name
                    }
                    var region_name = "";
                    if ($scope.region_list && $scope.region_list.length > 0) {
                        //region_name = $scope.region_list.filter(e => e.region_gid == $scope.cboregion);
                        region_name = $scope.region_list.filter(function (e) { return e.region_gid == $scope.cboregion });
                        region_name = region_name[0].region_name
                    }
                    var params = {
                        region_gid: $scope.cboregion,
                        region_name: region_name,
                        vertical_gid: $scope.cbovertical,
                        vertical_name: VerticalName,
                        employee_gid: $scope.cboemployee.employee_gid,
                        employee_name: $scope.cboemployee.employee_name,
                        program_gid: $scope.cboprogram.program_gid,
                        program_name: $scope.cboprogram.program_name,
                    }

                    var url = 'api/SystemMaster/PostRegionHeadAdd';
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

        $scope.editregionhead = function (regionhead_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/editregionhead.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });

            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    regionhead_gid: regionhead_gid
                }
                var url = 'api/SystemMaster/GetRegionHeadEdit';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.cboregionedit = resp.data.region_gid,
                    $scope.cboverticaledit = resp.data.vertical_gid,
                    $scope.cboemployeeedit = resp.data.employee_gid,
                    $scope.cboprogramedit = resp.data.program_gid,
                    $scope.region_list = resp.data.region_list,
                    $scope.vertical_list = resp.data.vertical_list,
                    $scope.employee_list = resp.data.employeelist
                    if (resp.data.vertical_gid != "") {
                      var params = {
                            vertical_gid: resp.data.vertical_gid,
                            lstype: 'region',
                            lstypegid: resp.data.region_gid,
                            lsmaster_gid: regionhead_gid
                        }
                        var url = 'api/SystemMaster/GetEditVerticalProgramList';
                        SocketService.getparams(url, params).then(function (resp) {
                            $scope.program_list = resp.data.program_list; 
                        });
                    }
                });

                $scope.OnchangeVertical = function (cbovertical, cboclusteredit) {
                    if (cbovertical != "" && cboclusteredit != "") {
                        var params = {
                            vertical_gid: cbovertical,
                            lstype: 'region',
                            lstypegid: cboclusteredit,
                            lsmaster_gid: regionhead_gid
                        }
                        var url = 'api/SystemMaster/GetEditVerticalProgramList';
                        lockUI();
                        SocketService.getparams(url, params).then(function (resp) {
                            $scope.program_list = resp.data.program_list;
                            unlockUI();
                        });
                    }
                }

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.update = function () {
                    var regionname;
                    var region_index = $scope.region_list.map(function (e) { return e.region_gid }).indexOf($scope.cboregionedit);
                    if (region_index == -1) { regionname = ''; } else { regionname = $scope.region_list[region_index].region_name; };

                    var verticalname;
                    var vertical_index = $scope.vertical_list.map(function (e) { return e.vertical_gid }).indexOf($scope.cboverticaledit);
                    if (vertical_index == -1) { verticalname = ''; } else { verticalname = $scope.vertical_list[vertical_index].vertical_name; };

                    var employeename;
                    var employee_index = $scope.employee_list.map(function (e) { return e.employee_gid }).indexOf($scope.cboemployeeedit);
                    if (employee_index == -1) { employeename = ''; } else { employeename = $scope.employee_list[employee_index].employee_name; };

                    var programName = "";
                    if ($scope.program_list && $scope.program_list.length > 0) {
                        //programName = $scope.program_list.filter(e => e.program_gid == $scope.cboprogramedit);
                        programName = $scope.program_list.filter(function (e) { return e.program_gid == $scope.cboprogramedit });
                        programName = programName[0].program_name
                    }

                    var url = 'api/SystemMaster/PostRegionHeadUpdate';
                    var params = {
                        region_gid: $scope.cboregionedit,
                        region_name: regionname,
                        vertical_gid: $scope.cboverticaledit,
                        vertical_name: verticalname,
                        employee_gid: $scope.cboemployeeedit,
                        employee_name: employeename,
                        regionhead_gid: regionhead_gid,
                        program_gid: $scope.cboprogramedit,
                        program_name: programName,
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

        $scope.Status_update = function (regionhead_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/regionheadstatus.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                var params = {
                    regionhead_gid: regionhead_gid
                }
                var url = 'api/SystemMaster/GetRegionHeadEdit';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.regionhead_gid = resp.data.regionhead_gid
                    $scope.txtemployee_name = resp.data.employee_name;
                    $scope.txtemployee_gid = resp.data.employee_gid;
                    $scope.rbo_status = resp.data.regionhead_status;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.update_status = function () {

                    var params = {
                        regionhead_gid: regionhead_gid,
                        employee_name: $scope.txtemployee_name,
                        employee_gid: $scope.txtemployee_gid,
                        remarks: $scope.txtremarks,
                        rbo_status: $scope.rbo_status

                    }
                    var url = 'api/SystemMaster/PostRegionHeadInactive';
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
                    regionhead_gid: regionhead_gid
                }

                var url = 'api/SystemMaster/GetRegionHeadInactiveLogview';
                lockUI();
                SocketService.getparams(url, param).then(function (resp) {
                    $scope.regionheadinactivelog_list = resp.data.master_list;
                    unlockUI();
                });

            }
        }


    }
})();


(function () {
    'use strict';

    angular
        .module('angle')
        .controller('SysMstRegionMappingController', SysMstRegionMappingController);

    SysMstRegionMappingController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams','DownloaddocumentService'];

    function SysMstRegionMappingController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, DownloaddocumentService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'SysMstRegionMappingController';
        $scope.lsoneapipage = $location.search().lsoneapipage;
        var lsoneapipage = $scope.lsoneapipage;

        activate();

        function activate() {
            var url = 'api/SystemMaster/GetRegionSummary';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.region_list = resp.data.region_list;
                unlockUI();
            });
        }

        $scope.addregion = function () {
            var modalInstance = $modal.open({
                templateUrl: '/addregion.html',
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

                var url = 'api/SystemMaster/GetUnTaggedClusters';
                lockUI();
                SocketService.get(url).then(function (resp) {
                    $scope.cluster_list = resp.data.cluster_list;
                    unlockUI();
                });
                $scope.submit = function () {

                    var params = {
                        cluster_list: $scope.cbocluster,
                        region_name: $scope.txtregion_name

                    }
                    var url = 'api/SystemMaster/PostRegionAdd';
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
        $scope.editregion = function (region_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/editregionmapping.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });

            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    region_gid: region_gid
                }
                var url = 'api/SystemMaster/GetUnTaggedClustersEdit';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.cluster_listedit = resp.data.cluster_list;
                    unlockUI();
                });

                var params = {
                    region_gid: region_gid
                }
                var url = 'api/SystemMaster/GetRegionEdit';
                SocketService.getparams(url, params).then(function (resp) {

                    $scope.txteditregion_name = resp.data.region_name;
                    $scope.clusterlist = resp.data.cluster_list;
                    console.log(resp.data.cluster_list);
                    $scope.cbocluster = [];
                    if (resp.data.cluster_list != null) {
                        var count = resp.data.cluster_list.length;
                        for (var i = 0; i < count; i++) {
                            //var indexs = $scope.cluster_listedit.findIndex(x => x.cluster_gid === resp.data.cluster_list[i].cluster_gid);
                            var indexs = $scope.cluster_listedit.map(function (x) { return x.cluster_gid; }).indexOf(resp.data.cluster_list[i].cluster_gid);
                            $scope.cbocluster.push($scope.cluster_listedit[indexs]);
                        }
                    }
                });


                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.update = function () {

                    var url = 'api/SystemMaster/PostRegionUpdate';
                    var params = {
                        region_gid: region_gid,
                        region_name: $scope.txteditregion_name,
                        cluster_list: $scope.cbocluster
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
              $scope.Status_update = function (region_gid) {
                    var modalInstance = $modal.open({
                        templateUrl: '/statusregion.html',
                        controller: ModalInstanceCtrl,
                        backdrop: 'static',
                        keyboard: false,
                        size: 'md'
                    });
                    ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
                    function ModalInstanceCtrl($scope, $modalInstance) {
                        $scope.errormsg = false;
                        var params = {
                            region_gid: region_gid
                        }
                        var url = 'api/SystemMaster/GetRegionEdit';
                        SocketService.getparams(url, params).then(function (resp) {
                            $scope.region_gid = resp.data.region_gid
                            $scope.txtregion_name = resp.data.region_name;
                            $scope.rbo_status = resp.data.region_status;
                        });

                        $scope.ok = function () {
                            $modalInstance.close('closed');
                        };
                        $scope.update_status = function () {

                            var params = {
                                region_gid: region_gid,
                                region_name: $scope.txtregion_name,
                                remarks: $scope.txtremarks,
                                rbo_status: $scope.rbo_status

                            }
                            var url = 'api/SystemMaster/PostRegionInactive';
                            lockUI();
                            SocketService.post(url, params).then(function (resp) {
                                unlockUI();
                                if (resp.data.status == true) {

                                    Notify.alert(resp.data.message, {
                                        status: 'success',
                                        pos: 'top-center',
                                        timeout: 3000
                                    });
                                    $scope.errormsg = false;
                                    $modalInstance.close('closed');
                                    activate();

                                }
                                else {
                                    if (resp.data.message == "N") {
                                        $scope.rbo_status = "Y";
                                        $scope.ocs_pendingcount = resp.data.ocs_pendingcount;
                                        $scope.agrbyr_pendingcount = resp.data.agrbyr_pendingcount;
                                        $scope.agrsupr_pendingcount = resp.data.agrsupr_pendingcount;
                                        $scope.errormsg = true;
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
                                }
                            });


                        }

                        var param = {
                            region_gid: region_gid
                        }

                        var url = 'api/SystemMaster/GetRegionInactiveLogview';
                        lockUI();
                        SocketService.getparams(url, param).then(function (resp) {
                            $scope.regioninactivelog_list = resp.data.master_list;
                            unlockUI();
                        });

                    }
                }

                $scope.showPopover = function (region_gid, region_name) {
                    var modalInstance = $modal.open({
                        templateUrl: '/showclusters.html',
                        controller: ModalInstanceCtrl,
                        backdrop: 'static',
                        keyboard: false,
                        size: 'md'
                    });
                    ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
                    function ModalInstanceCtrl($scope, $modalInstance) {
                        var params = {
                            region_gid: region_gid
                        }
                        var url = 'api/SystemMaster/GetRegion2Cluster';
                        SocketService.getparams(url, params).then(function (resp) {
                            $scope.cluster_list = resp.data.cluster_list;
                            $scope.region_name = region_name;
                        });
                        $scope.ok = function () {
                            $modalInstance.close('closed');
                        };
                    }
                }

                $scope.exportreport = function () {
                    lockUI();
                    var url = 'api/SystemMaster/RegionalReportExcel';
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
                            Notify.alert('Error Occurred While Export !', 'warning')
        
                        }
        
                    });
                }
    }
})();



(function () {
    'use strict';

    angular
        .module('angle')
        .controller('SysMstRoleAddController', SysMstRoleAddController);

        SysMstRoleAddController.$inject = ['$rootScope', '$scope', '$state', '$modal', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams'];

    function SysMstRoleAddController($rootScope, $scope, $state, $modal, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'SysMstRoleAddController';

        activate();

        function activate() {
            var url = 'api/ManageRole/PopRoleReportingToAdd';
            SocketService.get(url).then(function (resp) {
                $scope.reportingtoList = resp.data.rolereporting_to;
              
            });
         }
        $scope.role_cancel = function () {
            $state.go('app.SysMstRoleSummary');   
            };
           

        /* Role Add */

        $scope.role_submit = function () {
            var params = {
                role_code: $scope.txtrolecode,
                role_name: $scope.txtroletitle,
                reportingto_gid: $scope.txtreportingto,
                probation_period: $scope.txtprobationperiod,
                job_description: $scope.txtjobdescription,
                role_responsible: $scope.txtroleresponsible,
                
            }
   
            var url = 'api/ManageRole/RoleAdd';
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {                  
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 4000
                    });
                    activate();
                    $state.go('app.SysMstRoleSummary');
                }
                else {
                
                    Notify.alert(resp.data.message, {
                        status: 'warning',
                        pos: 'top-center',
                        timeout: 4000
                    });
                    activate();
                    $state.go('app.SysMstRoleSummary');
                }
            });
           
        } 
    }
})();


(function () {
    'use strict';

    angular
        .module('angle')
        .controller('SysMstRoleEditController', SysMstRoleEditController);

        SysMstRoleEditController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route'];

    function SysMstRoleEditController($rootScope, $scope, $state, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'SysMstRoleEditController';
        activate();

        function activate() {

            var url = 'api/ManageRole/PopRoleReportingToEdit';
            var params = {
                role_gid: $scope.role_gid
            };
            SocketService.getparams(url,params).then(function (resp) {
                $scope.reportingtoListedit = resp.data.rolereporting_toEdit;
              console.log(resp.data);
            });
           
            $scope.role_gid = localStorage.getItem('role_gid');
            var url = 'api/ManageRole/RoleEdit';
            var param = {
                role_gid: $scope.role_gid
            };
         
            SocketService.getparams(url, param).then(function (resp) {

                $scope.txtrolecode = resp.data.role_code;
                $scope.txtroletitle = resp.data.role_name;
                $scope.txtreportingto = resp.data.reportingto_gid;
                $scope.txtprobationperiod = resp.data.probation_period;
                $scope.txtjobdescription = resp.data.job_description;
                $scope.txtroleresponsible = resp.data.role_responsible;
                              
            });

           
         
        }

        $scope.roleedit_cancel = function () {
            $state.go('app.SysMstRoleSummary');   
            };     
             
        $scope.role_update = function () {

            var params = {
                role_gid: $scope.role_gid,
                role_code: $scope.txtrolecode,
                role_name: $scope.txtroletitle,
                reportingto_gid: $scope.txtreportingto,
                probation_period: $scope.txtprobationperiod,
                job_description: $scope.txtjobdescription,
                role_responsible: $scope.txtroleresponsible,         
               
            }
         
            var url = 'api/ManageRole/RoleUpdate';
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {
                    activate();
                    $state.go('app.SysMstRoleSummary');
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 4000
                    });
                }

                else {
                    Notify.alert(resp.data.message, {
                        status: 'warning',
                        pos: 'top-center',
                        timeout: 4000
                    });
                }
                activate();
            });
        } 
    }
})();

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('SysMstRoleSummaryController', SysMstRoleSummaryController);

        SysMstRoleSummaryController.$inject = ['$rootScope', '$scope', '$state', '$modal', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'SweetAlert'];

        function SysMstRoleSummaryController($rootScope, $scope, $state, $modal, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route,SweetAlert) {
        
        activate();
        var vm = this;
        vm.title = 'SysMstRoleSummaryController';

        $scope.lsoneapipage = $location.search().lsoneapipage;
            var lsoneapipage = $scope.lsoneapipage;

        function activate() {
            
                var url = 'api/ManageRole/RoleSummary';
                lockUI();
                SocketService.get(url).then(function (resp) {
                    $scope.rolelist = resp.data.role;
                    unlockUI();
                });
                            
           }
            
            $scope.role_edit = function (role_gid) {   
            $scope.role_gid = role_gid;
            $scope.role_gid = localStorage.setItem('role_gid', role_gid);
            $state.go('app.SysMstRoleEdit');
        };

        $scope.role_add = function () {
            $state.go('app.SysMstRoleAdd');   
            };
 

            $scope.delete = function (role_gid) {
                $scope.role_gid = role_gid;
                $scope.role_gid = localStorage.setItem('role_gid', role_gid);
                
                SweetAlert.swal({
                    title: 'Are you sure ?',
                    text: 'Do You Want To Delete ?',
                    showCancelButton: true,
                    confirmButtonColor: '#DD6B55',
                    confirmButtonText: 'Yes, delete it!',
                    closeOnConfirm: false
                }, function (isConfirm) {
                    if (isConfirm) {
                       
                        var url = 'api/ManageRole/RoleDelete';
                        $scope.role_gid = localStorage.getItem('role_gid');
                        var param = {
                            role_gid: $scope.role_gid
                        };
                        SocketService.getparams(url, param).then(function (resp) {
                            lockUI();
                            if (resp.data.status == true) {
                                SweetAlert.swal('Role Deleted Successfully !');
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

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('SysMstSalutationController', SysMstSalutationController);

    SysMstSalutationController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function SysMstSalutationController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'SysMstSalutationController';
        $scope.lsoneapipage = $location.search().lsoneapipage;
        var lsoneapipage = $scope.lsoneapipage;

        activate();

        function activate() {
            var url = 'api/SystemMaster/GetSalutation';

            SocketService.get(url).then(function (resp) {
                $scope.salutation_list = resp.data.master_list;

            });
        }

        $scope.addsalutation = function () {
            var modalInstance = $modal.open({
                templateUrl: '/addsalutation.html',
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
                $scope.submit = function () {

                    var params = {
                        salutation_name: $scope.txtsalutation,
                        lms_code: $scope.txtlms_code,
                        bureau_code: $scope.txtbureau_code

                    }
                    var url = 'api/SystemMaster/CreateSalutation';
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
        $scope.editsalutation = function (salutation_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/editsalutation.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    salutation_gid: salutation_gid
                }
                var url = 'api/SystemMaster/EditSalutation';
                SocketService.getparams(url, params).then(function (resp) {

                    $scope.txtedit_salutation = resp.data.salutation_name;
                    $scope.txteditlms_code = resp.data.lms_code;
                    $scope.txteditbureau_code = resp.data.bureau_code;
                    $scope.salutation_gid = resp.data.salutation_gid;
                });


                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.update = function () {

                    var url = 'api/SystemMaster/UpdateSalutation';
                    var params = {
                        salutation_name: $scope.txtedit_salutation,
                        lms_code: $scope.txteditlms_code,
                        bureau_code: $scope.txteditbureau_code,
                        salutation_gid: $scope.salutation_gid
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

        $scope.Status_update = function (salutation_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/statussalutation.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                var params = {
                    salutation_gid: salutation_gid
                }
                var url = 'api/SystemMaster/EditSalutation';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.salutation_gid = resp.data.salutation_gid
                    $scope.txtsalutation = resp.data.salutation_name;
                    $scope.rbo_status = resp.data.Status;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.update_status = function () {

                    var params = {
                        salutation_gid: salutation_gid,
                        remarks: $scope.txtremarks,
                        rbo_status: $scope.rbo_status

                    }
                    var url = 'api/SystemMaster/InactiveSalutation';
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
                    salutation_gid: salutation_gid
                }

                var url = 'api/SystemMaster/SalutationInactiveLogview';
                lockUI();
                SocketService.getparams(url, param).then(function (resp) {
                    $scope.salutationinactivelog_list = resp.data.master_list;
                    unlockUI();
                });

            }
        }

        $scope.delete = function (salutation_gid) {
            var params = {
                salutation_gid: salutation_gid
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
                    var url = 'api/SystemMaster/DeleteSalutation';
                    SocketService.getparams(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            activate();
                        }
                        else {
                            Notify.alert('Error Occurred While Deleting Salutation!', {
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


(function () {
    'use strict';

    angular
        .module('angle')
        .controller('SysMstStateSummaryController', SysMstStateSummaryController);

    SysMstStateSummaryController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function SysMstStateSummaryController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'SysMstStateSummaryController';

        activate();

        function activate() {
            var url = 'api/SystemMaster/GetGstStateSummary';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.state_data = resp.data.master_list;
                unlockUI();
            });
        }

       
    }
})();


(function () {
    'use strict';

    angular
        .module('angle')
        .controller('SysMstTaskAddController', SysMstTaskAddController);

        SysMstTaskAddController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function SysMstTaskAddController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'SysMstTaskAddController';

        $scope.program_gid = $location.search().lsprogram_gid;
        var program_gid = $scope.program_gid;

        activate();

        function activate() {           

            vm.open1 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();
                vm.opened1 = true;
            };
            vm.formats = ['dd-MM-yyyy'];
            vm.format = vm.formats[0];
            vm.dateOptions = {
                formatYear: 'yy',
                startingDay: 1
            };


            var url = 'api/SystemMaster/GetEmployeelist';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.assignedto_list = resp.data.employeelist;
                $scope.escalationmailto_list = resp.data.employeelist;
                unlockUI();
            });
        }

        $scope.task_submit = function () {

            if (($scope.txttask_name == '') || ($scope.txttask_description == undefined) || ($scope.cboassigned_to == '') || ($scope.cboescalationmail_to == undefined)) {
                Notify.alert('Enter all mandatory values..!', 'warning');
            }
            else {
                var params = {
                    task_name: $scope.txttask_name,
                    lms_code: $scope.txtlms_code,
                    bureau_code: $scope.txtbureau_code,
                    task_description: $scope.txttask_description,
                    assigned_to: $scope.cboassigned_to,
                    escalationmail_to: $scope.cboescalationmail_to,
                    tat: $scope.txttat
                }
                var url = 'api/SystemMaster/PostTaskAdd';
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    unlockUI();

                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    $location.url('app/SysMstTask');
                    activate();
                }
                else {
                    unlockUI();
                   
                    Notify.alert(resp.data.message, {
                        status: 'warning',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
                
            });
            }
        }

        
        $scope.Back = function ()
        {
            $location.url('app/SysMstTask');
        }

    }
})();


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


(function () {
    'use strict';

    angular
        .module('angle')
        .controller('SysMstTaskEditController', SysMstTaskEditController);

        SysMstTaskEditController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function SysMstTaskEditController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'SysMstTaskEditController';

        $scope.task_gid = $location.search().lstask_gid;
        var task_gid = $scope.task_gid;

        activate();
        lockUI();
        function activate() {         

            vm.open1 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();
                vm.opened1 = true;
            };
            vm.formats = ['dd-MM-yyyy'];
            vm.format = vm.formats[0];
            vm.dateOptions = {
                formatYear: 'yy',
                startingDay: 1
            };

            var params = {
                task_gid: task_gid
            };
            var url = 'api/SystemMaster/EditTask';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.txttask_code = resp.data.task_code;
                $scope.txttask_name = resp.data.task_name;

                $scope.txtlms_code = resp.data.lms_code;
                $scope.txtbureau_code = resp.data.bureau_code;

                $scope.txttask_description = resp.data.task_description;
                $scope.txttat = resp.data.tat;
               
                $//scope.assignedto_editlist = resp.data.assigned_to;
                $scope.assignedto_general = resp.data.assignedto_general;
                $scope.cboassignedto_edit = [];
                if (resp.data.assigned_to != null) {
                    var count = resp.data.assigned_to.length;
                    for (var i = 0; i < count; i++) {
                        //var indexs = $scope.assignedto_general.findIndex(x => x.employee_gid === resp.data.assigned_to[i].employee_gid);
                        var indexs = $scope.assignedto_general.map(function (x) { return x.employee_gid; }).indexOf(resp.data.assigned_to[i].employee_gid);
                        $scope.cboassignedto_edit.push($scope.assignedto_general[indexs]);
                        $scope.$parent.cboassignedto_edit = $scope.cboassignedto_edit;
                    }
                }

                $scope.assignedto_general = resp.data.assignedto_general;
                $scope.cboassignedto_edit = [];
                if (resp.data.assigned_to != null) {
                    var count = resp.data.assigned_to.length;
                    for (var i = 0; i < count; i++) {
                        //var indexs = $scope.assignedto_general.findIndex(x => x.employee_gid === resp.data.assigned_to[i].employee_gid);
                        var indexs = $scope.assignedto_general.map(function (x) { return x.employee_gid; }).indexOf(resp.data.assigned_to[i].employee_gid);
                        $scope.cboassignedto_edit.push($scope.assignedto_general[indexs]);
                        $scope.$parent.cboassignedto_edit = $scope.cboassignedto_edit;
                    }
                }

                $scope.escalationmailto_general = resp.data.escalationmailto_general;
                $scope.cboescalationmailto_edit = [];
                if (resp.data.escalationmail_to != null) {
                    var count = resp.data.escalationmail_to.length;
                    for (var i = 0; i < count; i++) {
                        //var indexs = $scope.escalationmailto_general.findIndex(x => x.employee_gid === resp.data.escalationmail_to[i].employee_gid);
                        var indexs = $scope.escalationmailto_general.map(function (x) { return x.employee_gid; }).indexOf(resp.data.escalationmail_to[i].employee_gid);
                        $scope.cboescalationmailto_edit.push($scope.escalationmailto_general[indexs]);
                        $scope.$parent.cboescalationmailto_edit = $scope.cboescalationmailto_edit;
                    }
                }

            });
           
        }    

     

        

        $scope.back = function () {
            $state.go('app.SysMstTask');
        };

        $scope.update_task = function () {

                lockUI();

                var params = {
                    task_gid: $scope.task_gid,
                    task_code: $scope.txttask_code,
                    task_name: $scope.txttask_name,
                    lms_code: $scope.txtlms_code,
                    bureau_code: $scope.txtbureau_code,                   
                    task_description: $scope.txttask_description,
                    tat: $scope.txttat,
                    assigned_to: $scope.cboassignedto_edit,
                    escalationmail_to: $scope.cboescalationmailto_edit             
                }
                var url = 'api/SystemMaster/UpdateTask';
                SocketService.post(url, params).then(function (resp) {

                    unlockUI();
                    if (resp.data.status == true) {
                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        $state.go('app.SysMstTask');
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
        

    }
})();
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
(function () {
    'use strict';

    angular
        .module('angle')
        .controller('SysMstTaskViewController', SysMstTaskViewController);

        SysMstTaskViewController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function SysMstTaskViewController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'SysMstTaskViewController';

        $scope.task_gid = $location.search().lstask_gid;
        var task_gid = $scope.task_gid;

        activate();
        lockUI();
        function activate() {         

            vm.open1 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();
                vm.opened1 = true;
            };
            vm.formats = ['dd-MM-yyyy'];
            vm.format = vm.formats[0];
            vm.dateOptions = {
                formatYear: 'yy',
                startingDay: 1
            };

            var params = {
                task_gid: task_gid
            };
            var url = 'api/SystemMaster/EditTask';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.lbltask_code = resp.data.task_code;
                $scope.lbltask_name = resp.data.task_name;
                $scope.lbllms_code = resp.data.lms_code;
                $scope.lblbureau_code = resp.data.bureau_code;
                $scope.lbltask_description = resp.data.task_description;
                $scope.lbltat = resp.data.tat;              
            });
            var url = 'api/SystemMaster/GetTaskMultiselectList';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.lblassignedto_name = resp.data.assignedto_name;
                $scope.lblescalationmailto_name = resp.data.escalationmailto_name;
                          
            });
            
        }    

     

        

        $scope.back = function () {
            $state.go('app.SysMstTask');
        };

        $scope.update_task = function () {

                lockUI();

                var params = {
                    task_gid: $scope.task_gid,
                    task_code: $scope.txttask_code,
                    task_name: $scope.txttask_name,
                    lms_code: $scope.txtlms_code,
                    bureau_code: $scope.txtbureau_code,                   
                    task_description: $scope.txttask_description,
                    tat: $scope.txttat,
                    assigned_to: $scope.cboassignedto_edit,
                    escalationmail_to: $scope.cboescalationmailto_edit             
                }
                var url = 'api/SystemMaster/UpdateTask';
                SocketService.post(url, params).then(function (resp) {

                    unlockUI();
                    if (resp.data.status == true) {
                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        $state.go('app.SysMstTask');
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
        

    }
})();
(function () {
    'use strict';

    angular
        .module('angle')
        .controller('SysMstTriggerUserController', SysMstTriggerUserController);

    SysMstTriggerUserController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function SysMstTriggerUserController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'SysMstTriggerUserController';
        $scope.lsoneapipage = $location.search().lsoneapipage;
        var lsoneapipage = $scope.lsoneapipage;

        activate();

        function activate() {
            var url = 'api/TriggerUser/GetTriggerUser';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.trigger_list = resp.data.trigger_list;
                unlockUI();
            });
           
        }
       
        $scope.addtrigger = function () {
            var modalInstance = $modal.open({
                templateUrl: '/addtrigger.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var url = 'api/TriggerUser/employee';
                SocketService.get(url).then(function (resp) {
                    $scope.employee_list = resp.data.trigger_list;
                });
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.submit = function () {

                    var params = {
                        
                        remarks: $scope.txtremarks,
                        trigger_list: $scope.cbotrigger,
                    }
                    var url = 'api/TriggerUser/CreateTriggerUser';
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
        
        $scope.delete = function (triggeruser_gid) {
            var params = {
                triggeruser_gid: triggeruser_gid
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
                    var url = 'api/TriggerUser/DeleteTriggerUser';
                    SocketService.getparams(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            activate();
                        }
                        else {
                            Notify.alert('Error Occurred While Deleting Trigger User!', {
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
       
        $scope.Status_update = function (triggeruser_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/statustrigger.html',
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
                $scope.update_status = function () {

                    var params = {
                        triggeruser_gid: triggeruser_gid,
                        //triggeruser_name: $scope.cbotrigger,
                        remarks: $scope.txtremarks,
                        rbo_status: $scope.rbo_status

                    }
                    var url = 'api/TriggerUser/InactiveTrigger';
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
                    triggeruser_gid: triggeruser_gid
                }

                var url = 'api/TriggerUser/TriggerInactiveLogview';
                lockUI();
                SocketService.getparams(url, param).then(function (resp) {
                    $scope.Triggerinactivelog_list = resp.data.trigger_list;
                    unlockUI();
                });

            }
        }

    }
})();


(function () {
    'use strict';

    angular
        .module('angle')
        .controller('SysMstZonalHeadController', SysMstZonalHeadController);

    SysMstZonalHeadController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function SysMstZonalHeadController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'SysMstZonalHeadController';
        $scope.lsoneapipage = $location.search().lsoneapipage;
        var lsoneapipage = $scope.lsoneapipage;

        activate();

        function activate() {
            var url = 'api/SystemMaster/GetZonalHeadSummary';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.zonalhead_list = resp.data.zonalhead_list;
                unlockUI();
            });
        }

        $scope.addzonalhead = function () {
            var modalInstance = $modal.open({
                templateUrl: '/addzonalhead.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });

            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                $scope.OnchangeVertical = function (cbovertical, cbocluster) {
                    if (cbovertical != "" && cbocluster != "") {
                        var params = {
                            vertical_gid: cbovertical,
                            lstype: 'zonal',
                            lstypegid: cbocluster
                        }
                        var url = 'api/SystemMaster/GetVerticalProgramList';
                        lockUI();
                        SocketService.getparams(url, params).then(function (resp) {
                            $scope.program_list = resp.data.program_list;
                            unlockUI();
                        });
                    }
                }

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                var url = 'api/SystemMaster/GetZonallist';
                lockUI();
                SocketService.get(url).then(function (resp) {
                    $scope.zone_list = resp.data.zone_list;
                    unlockUI();
                });
                var url = 'api/SystemMaster/GetVerticallist';
                lockUI();
                SocketService.get(url).then(function (resp) {
                    $scope.vertical_list = resp.data.vertical_list;
                    unlockUI();
                });
                var url = 'api/SystemMaster/GetEmployeelist';
                lockUI();
                SocketService.get(url).then(function (resp) {
                    $scope.employee_list = resp.data.employeelist;
                    unlockUI();
                });

                $scope.submit = function () {
                    var VerticalName = "";
                    if ($scope.vertical_list && $scope.vertical_list.length > 0) {
                        //VerticalName = $scope.vertical_list.filter(e => e.vertical_gid == $scope.cbovertical);
                        VerticalName = $scope.vertical_list.filter(function (e) { return e.vertical_gid == $scope.cbovertical });
                        VerticalName = VerticalName[0].vertical_name
                    }
                    var zone_name = "";
                    if ($scope.zone_list && $scope.zone_list.length > 0) {
                        //zone_name = $scope.zone_list.filter(e => e.zone_gid == $scope.cbozone);
                        zone_name = $scope.zone_list.filter(function (e) { return e.zone_gid == $scope.cbozone });
                        zone_name = zone_name[0].zone_name
                    }
                    var params = {
                        zonal_gid: $scope.cbozone,
                        zonal_name: zone_name,
                        vertical_gid: $scope.cbovertical,
                        vertical_name: VerticalName,
                        employee_gid: $scope.cboemployee.employee_gid,
                        employee_name: $scope.cboemployee.employee_name,
                        program_gid: $scope.cboprogram.program_gid,
                        program_name: $scope.cboprogram.program_name,
                    }
                  
                    var url = 'api/SystemMaster/PostZonalheadAdd';
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
        $scope.editzonal = function (zonalhead_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/editzonalhead.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });

            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    zonalhead_gid: zonalhead_gid
                }
                var url = 'api/SystemMaster/GetZonalHeadEdit';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.cbozoneedit = resp.data.zonal_gid,
                    $scope.cboverticaledit = resp.data.vertical_gid,
                     $scope.cboemployeeedit = resp.data.employee_gid,
                      $scope.zone_list = resp.data.zone_list,
                     $scope.cboprogramedit = resp.data.program_gid,
                    $scope.vertical_list = resp.data.vertical_list,
                    $scope.employee_list = resp.data.employeelist
                    if (resp.data.vertical_gid != "") {
                        var params = {
                            vertical_gid: resp.data.vertical_gid,
                            lstype: 'zonal',
                            lstypegid: resp.data.zonal_gid,
                            lsmaster_gid: zonalhead_gid
                        }
                        var url = 'api/SystemMaster/GetEditVerticalProgramList';
                        SocketService.getparams(url, params).then(function (resp) {
                            $scope.program_list = resp.data.program_list;
                        });
                    }
                });

                $scope.OnchangeVertical = function (cbovertical, cboclusteredit) {
                    if (cbovertical != "" && cboclusteredit != "") {
                        var params = {
                            vertical_gid: cbovertical,
                            lstype: 'zonal',
                            lstypegid: cboclusteredit,
                            lsmaster_gid: zonalhead_gid
                        }
                        var url = 'api/SystemMaster/GetEditVerticalProgramList';
                        lockUI();
                        SocketService.getparams(url, params).then(function (resp) {
                            $scope.program_list = resp.data.program_list;
                            unlockUI();
                        });
                    }
                }

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.update = function () {
                    var zonename;
                    var zone_index = $scope.zone_list.map(function (e) { return e.zone_gid }).indexOf($scope.cbozoneedit);
                    if (zone_index == -1) { zonename = ''; } else { zonename = $scope.zone_list[zone_index].zone_name; };

                    var verticalname;
                    var vertical_index = $scope.vertical_list.map(function (e) { return e.vertical_gid }).indexOf($scope.cboverticaledit);
                    if (vertical_index == -1) { verticalname = ''; } else { verticalname = $scope.vertical_list[vertical_index].vertical_name; };

                    var employeename;
                    var employee_index = $scope.employee_list.map(function (e) { return e.employee_gid }).indexOf($scope.cboemployeeedit);
                    if (employee_index == -1) { employeename = ''; } else { employeename = $scope.employee_list[employee_index].employee_name; };

                    var programName = "";
                    if ($scope.program_list && $scope.program_list.length > 0) {
                        //programName = $scope.program_list.filter(e => e.program_gid == $scope.cboprogramedit);
                        programName = $scope.program_list.filter(function (e) { return e.program_gid == $scope.cboprogramedit });
                        programName = programName[0].program_name
                    }

                    var url = 'api/SystemMaster/PostZonalHeadUpdate';
                    var params = {
                        zonal_gid: $scope.cbozoneedit,
                        zonal_name: zonename,
                        vertical_gid: $scope.cboverticaledit,
                        vertical_name: verticalname,
                        employee_gid: $scope.cboemployeeedit,
                        employee_name: employeename,
                        zonalhead_gid: zonalhead_gid,
                        program_gid: $scope.cboprogramedit,
                        program_name: programName,
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

        $scope.Status_update = function (zonalhead_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/statuszonalhead.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    zonalhead_gid: zonalhead_gid
                }
                var url = 'api/SystemMaster/GetZonalHeadEdit';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.txtzonalhead_name = resp.data.employee_name,
                    $scope.rbo_status = resp.data.zonalhead_status
                    
                });

            

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.update_status = function () {

                    var params = {
                        zonalhead_gid: zonalhead_gid,
                        employee_name: $scope.txtclusterhead_name,
                        remarks: $scope.txtremarks,
                        rbo_status: $scope.rbo_status

                    }
                    var url = 'api/SystemMaster/InactiveZonalhead';
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
                    zonalhead_gid: zonalhead_gid
                }

                var url = 'api/SystemMaster/ZonalheadInactiveLogview';
                lockUI();
                SocketService.getparams(url, param).then(function (resp) {
                    $scope.zonalheadinactivelog_list = resp.data.master_list;
                    unlockUI();
                });

            }
        }
    }
})();
(function () {
    'use strict';

    angular
        .module('angle')
        .controller('SysMstZoneMappingController', SysMstZoneMappingController);

    SysMstZoneMappingController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams','DownloaddocumentService'];

    function SysMstZoneMappingController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, DownloaddocumentService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'SysMstZoneMappingController';
        $scope.lsoneapipage = $location.search().lsoneapipage;
        var lsoneapipage = $scope.lsoneapipage;

        activate();

        function activate() {
            var url = 'api/SystemMaster/GetZoneSummary';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.zone_list = resp.data.zone_list;
                unlockUI();
            });
        }

        $scope.addzone = function () {
            var modalInstance = $modal.open({
                templateUrl: '/addzone.html',
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

                var url = 'api/SystemMaster/GetUnTaggedRegions';
                lockUI();
                SocketService.get(url).then(function (resp) {
                    $scope.region_list = resp.data.region_list;
                    unlockUI();
                });
                $scope.submit = function () {

                    var params = {
                        region_list: $scope.cboregion,
                        zone_name: $scope.txtzone_name

                    }
                    var url = 'api/SystemMaster/PostZoneAdd';
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
        
        $scope.editzone = function (zone_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/editzonemapping.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });

            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    zone_gid: zone_gid
                }
                var url = 'api/SystemMaster/GetUnTaggedRegionsEdit';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.region_listedit = resp.data.region_list;
                    unlockUI();
                });

                var url = 'api/SystemMaster/GetZoneEdit';
                SocketService.getparams(url, params).then(function (resp) {

                    $scope.txteditzone_name = resp.data.zone_name;
                    $scope.region_list = resp.data.region_list;

                    $scope.cboregion = [];
                    if (resp.data.region_list != null) {
                        var count = resp.data.region_list.length;
                        for (var i = 0; i < count; i++) {
                            //var indexs = $scope.region_listedit.findIndex(x => x.region_gid === resp.data.region_list[i].region_gid);
                            var indexs = $scope.region_listedit.map(function (x) { return x.region_gid; }).indexOf(resp.data.region_list[i].region_gid);
                            $scope.cboregion.push($scope.region_listedit[indexs]);
                        }
                    }
                });


                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.update = function () {

                    var url = 'api/SystemMaster/PostZoneUpdate';
                    var params = {
                        zone_gid: zone_gid,
                        zone_name: $scope.txteditzone_name,
                        region_list: $scope.cboregion
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

        $scope.Status_update = function (zone_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/statuszone.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.errormsg = false;
                var params = {
                    zone_gid: zone_gid
                }
                var url = 'api/SystemMaster/GetZoneEdit';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.zone_gid = resp.data.zone_gid
                    $scope.txtzone_name = resp.data.zone_name;
                    $scope.rbo_status = resp.data.zone_status;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.update_status = function () {

                    var params = {
                        zone_gid: zone_gid,
                        zone_name: $scope.txtzone_name,
                        remarks: $scope.txtremarks,
                        rbo_status: $scope.rbo_status

                    }
                    var url = 'api/SystemMaster/PostZoneInactive';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {

                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            $scope.errormsg = false;
                            $modalInstance.close('closed');
                            activate();

                        }
                        else {
                            if (resp.data.message == "N") {
                                $scope.rbo_status = "Y";
                                $scope.ocs_pendingcount = resp.data.ocs_pendingcount;
                                $scope.agrbyr_pendingcount = resp.data.agrbyr_pendingcount;
                                $scope.agrsupr_pendingcount = resp.data.agrsupr_pendingcount;
                                $scope.errormsg = true;
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
                        }
                    });


                }

                var param = {
                    zone_gid: zone_gid
                }

                var url = 'api/SystemMaster/GetZoneInactiveLogview';
                lockUI();
                SocketService.getparams(url, param).then(function (resp) {
                    $scope.zoneinactivelog_list = resp.data.master_list;
                    unlockUI();
                });

            }
        }

        $scope.showPopover = function (zone_gid, zone_name) {
            var modalInstance = $modal.open({
                templateUrl: '/showregions.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    zone_gid: zone_gid
                }
                var url = 'api/SystemMaster/GetZone2Region';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.region_list = resp.data.region_list;
                    $scope.zone_name = zone_name;
                    angular.forEach($scope.region_list, function (value, key) {
                        var params = {
                            region_gid: value.region_gid
                        };
                        var url = 'api/SystemMaster/GetRegion2Cluster';
                        SocketService.getparams(url, params).then(function (resp) {
                            value.cluster_list = resp.data.cluster_list;
                        });
                    });
                });
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
            }
        }

        $scope.exportreport = function () {
            lockUI();
            var url = 'api/SystemMaster/ZonalReportExcel';
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
                    Notify.alert('Error Occurred While Export !', 'warning')

                }

            });
        }

    }
})();


(function () {
    'use strict';

    angular
        .module('angle')
        .controller('SysOneApiDashboardController', SysOneApiDashboardController);

        SysOneApiDashboardController.$inject = ['$rootScope', '$scope', '$state', '$modal', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', '$route', '$cookies'];

    function SysOneApiDashboardController($rootScope, $scope, $state, $modal, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, $route, $cookies) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'SysOneApiDashboardController';

      // activate();

        function activate() {

           
        }
    }
})();

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('SysprtAgriCommerceApplicationStatus', SysprtAgriCommerceApplicationStatus);

        SysprtAgriCommerceApplicationStatus.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', '$sce'];

    function SysprtAgriCommerceApplicationStatus($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, $sce) {
        var vm = this;
        vm.title = 'SysprtAgriCommerceApplicationStatus';


        activate();
        function fullscreenchanged() 
{

// if (document.fullscreenElement) {
// console.log(

// `Element: ${document.fullscreenElement.id} entered fullscreen mode.`
// );

//  } 
// else 
// {

// console.log("Leaving fullscreen mode.");

// }

}

const el = document.getElementById("fullscreen-div");

el.addEventListener("fullscreenchange", fullscreenchanged);

el.onfullscreenchange = fullscreenchanged;

// When the toggle button is clicked, enter/exit fullscreen

document

 .getElementById("toggle-fullscreen")

 .addEventListener("click", function(event)
  {

if (document.fullscreenElement) {

// exitFullscreen is only available on the Document object.
 document.exitFullscreen();
 }
 else
 { el.requestFullscreen();
 }

 });

        function activate() {
            const models = window['powerbi-client'].models;
            var url = 'api/SamRpt/Report1';
            var param = {
                report_id: 'cb00f40c-ab00-4677-937d-74d66d92d89d',
                workspace_Id : '364f27ad-9f3a-4046-bfdb-84f304cd9a2d',
                dataset:'d4db7aa0-7b09-4e51-b590-63ffb8303ace',
                roles:'RM,Manager,Superuser'
            }

            SocketService.getparams(url, param).then(function (resp) { 
                var response = JSON.parse(resp.data);
                console.log(resp.data);
                console.log(response.EmbedReports[0].EmbedUrl);
                console.log(response.EmbedToken.token);
                
                
                var embedConfiguration = {

                    type: 'report',
                    tokenType: models.TokenType.Embed,
                    accessToken: response.EmbedToken.token,
                    embedUrl: response.EmbedReports[0].EmbedUrl,
                    id: response.EmbedReports[0].ReportId,
                    permissions: models.Permissions.All,
                    settings: {
                        // Enable this setting to remove gray shoulders from embedded report
                        // background: models.BackgroundType.Transparent,
                        filterPaneEnabled: true,
                        navContentPaneEnabled: true
                    }
               
                }; 

                var $reportContainer = $('#dashboardContainer');

                var report = powerbi.embed($reportContainer.get(0), embedConfiguration);

                var element = $('#dashboardContainer');
 
                var report = powerbi.get(element);
            });
            var url = 'api/SamRpt/PostAuditView';

            var params = {
                page_name: 'AgriCommerce Application Status',
                page_head: 'Mobile'
            };
            SocketService.getparams(url, params).then(function (resp) {
                if (resp.data.status == true) {
                }
                else {

                }

            });


        };
    };
})();


(function () {
    'use strict';

    angular
        .module('angle')
        .controller('SysprtAgriCommerceSnapshot', SysprtAgriCommerceSnapshot);

        SysprtAgriCommerceSnapshot.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', '$sce'];

    function SysprtAgriCommerceSnapshot($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, $sce) {
        var vm = this;
        vm.title = 'SysprtAgriCommerceSnapshot';


        activate();

        function fullscreenchanged() 
{

// if (document.fullscreenElement) {
// console.log(

// `Element: ${document.fullscreenElement.id} entered fullscreen mode.`
// );

//  } 
// else 
// {

// console.log("Leaving fullscreen mode.");

// }

}

const el = document.getElementById("fullscreen-div");

el.addEventListener("fullscreenchange", fullscreenchanged);

el.onfullscreenchange = fullscreenchanged;

// When the toggle button is clicked, enter/exit fullscreen

document

 .getElementById("toggle-fullscreen")

 .addEventListener("click", function(event)
  {

if (document.fullscreenElement) {

// exitFullscreen is only available on the Document object.
 document.exitFullscreen();
 }
 else
 { el.requestFullscreen();
 }

 });


        function activate() {
            const models = window['powerbi-client'].models;
            var url = 'api/SamRpt/Report1';
            var param = {
                report_id: 'f32b5183-a915-4e33-bd9c-d145c619c5e0',
                workspace_Id : '364f27ad-9f3a-4046-bfdb-84f304cd9a2d',
                dataset:'a7412cb4-5111-4f15-9ebc-0c188ffa5e03',
                roles:'RM,Manager,Superuser'
            }

            SocketService.getparams(url, param).then(function (resp) { 
                var response = JSON.parse(resp.data);
                console.log(resp.data);
                console.log(response.EmbedReports[0].EmbedUrl);
                console.log(response.EmbedToken.token);
                
                
                var embedConfiguration = {

                    type: 'report',
                    tokenType: models.TokenType.Embed,
                    accessToken: response.EmbedToken.token,
                    embedUrl: response.EmbedReports[0].EmbedUrl,
                    id: response.EmbedReports[0].ReportId,
                    permissions: models.Permissions.All,
                    settings: {
                        // Enable this setting to remove gray shoulders from embedded report
                        // background: models.BackgroundType.Transparent,
                        filterPaneEnabled: true,
                        navContentPaneEnabled: true
                    }
               
                }; 

                var $reportContainer = $('#dashboardContainer');

                var report = powerbi.embed($reportContainer.get(0), embedConfiguration);

                var element = $('#dashboardContainer');
 
                var report = powerbi.get(element);
            });
            var url = 'api/SamRpt/PostAuditView';

            var params = {
                page_name: 'AgriCommerce Snapshot',
                page_head: 'Mobile'
            };
            SocketService.getparams(url, params).then(function (resp) {
                if (resp.data.status == true) {
                }
                else {

                }

            });


        };
    };
})();


(function () {
    'use strict';

    angular
        .module('angle')
        .controller('SysprtAgriCommerceTradeReceivablesQuality', SysprtAgriCommerceTradeReceivablesQuality);

        SysprtAgriCommerceTradeReceivablesQuality.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', '$sce'];

    function SysprtAgriCommerceTradeReceivablesQuality($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, $sce) {
        var vm = this;
        vm.title = 'SysprtAgriCommerceTradeReceivablesQuality';


        activate();
        function fullscreenchanged() 
        {
        
        // if (document.fullscreenElement) {
        // console.log(
        
        // `Element: ${document.fullscreenElement.id} entered fullscreen mode.`
        // );
        
        //  } 
        // else 
        // {
        
        // console.log("Leaving fullscreen mode.");
        
        // }
        
        }
        
        const el = document.getElementById("fullscreen-div");
        
        el.addEventListener("fullscreenchange", fullscreenchanged);
        
        el.onfullscreenchange = fullscreenchanged;
        
        // When the toggle button is clicked, enter/exit fullscreen
        
        document
        
         .getElementById("toggle-fullscreen")
        
         .addEventListener("click", function(event) 
          {
        
        if (document.fullscreenElement) {
        
        // exitFullscreen is only available on the Document object.
         document.exitFullscreen();
         }
         else
         { el.requestFullscreen();
         }
        
         });
        function activate() {
            const models = window['powerbi-client'].models;
            var url = 'api/SamRpt/Report1';
            var param = {
                report_id: 'd036339a-bf19-4117-9369-b973fa174d60',
                workspace_Id : '364f27ad-9f3a-4046-bfdb-84f304cd9a2d',
                dataset:'4ce8d6aa-305a-4500-b8ea-fa8332c911fb',
                roles:'RM,Manager,Superuser'
            }

            SocketService.getparams(url, param).then(function (resp) { 
                var response = JSON.parse(resp.data);
                console.log(resp.data);
                console.log(response.EmbedReports[0].EmbedUrl);
                console.log(response.EmbedToken.token);
                
                
                var embedConfiguration = {

                    type: 'report',
                    tokenType: models.TokenType.Embed,
                    accessToken: response.EmbedToken.token,
                    embedUrl: response.EmbedReports[0].EmbedUrl,
                    id: response.EmbedReports[0].ReportId,
                    permissions: models.Permissions.All,
                    settings: {
                        // Enable this setting to remove gray shoulders from embedded report
                        // background: models.BackgroundType.Transparent,
                        filterPaneEnabled: true,
                        navContentPaneEnabled: true
                    }
               
                }; 

                var $reportContainer = $('#dashboardContainer');

                var report = powerbi.embed($reportContainer.get(0), embedConfiguration);

                var element = $('#dashboardContainer');
 
                var report = powerbi.get(element);
            });
            var url = 'api/SamRpt/PostAuditView';

            var params = {
                page_name: 'AgriCommerce Trade Receivables Quality',
                page_head: 'Mobile'
            };
            SocketService.getparams(url, params).then(function (resp) {
                if (resp.data.status == true) {
                }
                else {

                }

            });


        };
    };
})();


(function () {
    'use strict';

    angular
        .module('angle')
        .controller('SysprtAgriCommerceTransactionProfile', SysprtAgriCommerceTransactionProfile);

        SysprtAgriCommerceTransactionProfile.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', '$sce'];

    function SysprtAgriCommerceTransactionProfile($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, $sce) {
        var vm = this;
        vm.title = 'SysprtAgriCommerceTransactionProfile';


        activate();
        function fullscreenchanged() 
{

// if (document.fullscreenElement) {
// console.log(

// `Element: ${document.fullscreenElement.id} entered fullscreen mode.`
// );

//  } 
// else 
// {

// console.log("Leaving fullscreen mode.");

// }

}

const el = document.getElementById("fullscreen-div");

el.addEventListener("fullscreenchange", fullscreenchanged);

el.onfullscreenchange = fullscreenchanged;

// When the toggle button is clicked, enter/exit fullscreen

document

 .getElementById("toggle-fullscreen")

 .addEventListener("click", function(event)
  {

if (document.fullscreenElement) {

// exitFullscreen is only available on the Document object.
 document.exitFullscreen();
 }
 else
 { el.requestFullscreen();
 }

 });

        function activate() {
            const models = window['powerbi-client'].models;
            var url = 'api/SamRpt/Report1';
            var param = {
                report_id: 'eb03ed57-1637-4680-a194-bd3d06c82b89',
                workspace_Id : '364f27ad-9f3a-4046-bfdb-84f304cd9a2d',
                dataset:'f0eb2208-0fd8-42ff-9c22-7f0d30667e37',
                roles:'RM,Manager,Superuser'
            }

            SocketService.getparams(url, param).then(function (resp) { 
                var response = JSON.parse(resp.data);
                console.log(resp.data);
                console.log(response.EmbedReports[0].EmbedUrl);
                console.log(response.EmbedToken.token);
                
                
                var embedConfiguration = {

                    type: 'report',
                    tokenType: models.TokenType.Embed,
                    accessToken: response.EmbedToken.token,
                    embedUrl: response.EmbedReports[0].EmbedUrl,
                    id: response.EmbedReports[0].ReportId,
                    permissions: models.Permissions.All,
                    settings: {
                        // Enable this setting to remove gray shoulders from embedded report
                        // background: models.BackgroundType.Transparent,
                        filterPaneEnabled: true,
                        navContentPaneEnabled: true
                    }
               
                }; 

                var $reportContainer = $('#dashboardContainer');

                var report = powerbi.embed($reportContainer.get(0), embedConfiguration);

                var element = $('#dashboardContainer');
 
                var report = powerbi.get(element);
            });
            var url = 'api/SamRpt/PostAuditView';

            var params = {
                page_name: 'AgriCommerce Transaction Profile',
                page_head: 'Mobile'
            };
            SocketService.getparams(url, params).then(function (resp) {
                if (resp.data.status == true) {
                }
                else {

                }

            });


        };
    };
})();


(function () {
    'use strict';

    angular
        .module('angle')
        .controller('SysprtAgriFinanceApplicationStatus', SysprtAgriFinanceApplicationStatus);

        SysprtAgriFinanceApplicationStatus.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', '$sce'];

    function SysprtAgriFinanceApplicationStatus($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, $sce) {
        var vm = this;
        vm.title = 'SysprtAgriFinanceApplicationStatus';


        activate();
        function fullscreenchanged() 
{

// if (document.fullscreenElement) {
// console.log(

// `Element: ${document.fullscreenElement.id} entered fullscreen mode.`
// );

//  } 
// else 
// {

// console.log("Leaving fullscreen mode.");

// }

}

const el = document.getElementById("fullscreen-div");

el.addEventListener("fullscreenchange", fullscreenchanged);

el.onfullscreenchange = fullscreenchanged;

// When the toggle button is clicked, enter/exit fullscreen

document

 .getElementById("toggle-fullscreen")

 .addEventListener("click", function(event)
  {

if (document.fullscreenElement) {

// exitFullscreen is only available on the Document object.
 document.exitFullscreen();
 }
 else
 { el.requestFullscreen();
 }

 });

        function activate() {
            const models = window['powerbi-client'].models;
            var url = 'api/SamRpt/Report1';
            var param = {
                report_id: '0e43eb1e-d8c1-494f-8149-98b7ec4e864d',
                workspace_Id : '364f27ad-9f3a-4046-bfdb-84f304cd9a2d',
                dataset:'7710581b-b5d3-42ef-ac67-823ad9352654',
                roles:'RM,Manager,Superuser'
            }

            SocketService.getparams(url, param).then(function (resp) { 
                var response = JSON.parse(resp.data);
                console.log(resp.data);
                console.log(response.EmbedReports[0].EmbedUrl);
                console.log(response.EmbedToken.token);
                
                
                var embedConfiguration = {

                    type: 'report',
                    tokenType: models.TokenType.Embed,
                    accessToken: response.EmbedToken.token,
                    embedUrl: response.EmbedReports[0].EmbedUrl,
                    id: response.EmbedReports[0].ReportId,
                    permissions: models.Permissions.All,
                    settings: {
                        // Enable this setting to remove gray shoulders from embedded report
                        // background: models.BackgroundType.Transparent,
                        filterPaneEnabled: true,
                        navContentPaneEnabled: true
                    }
               
                }; 

                var $reportContainer = $('#dashboardContainer');

                var report = powerbi.embed($reportContainer.get(0), embedConfiguration);

                var element = $('#dashboardContainer');
 
                var report = powerbi.get(element);
            });
            var url = 'api/SamRpt/PostAuditView';

            var params = {
                page_name: 'AgriFinance Application Status',
                page_head: 'Mobile'
            };
            SocketService.getparams(url, params).then(function (resp) {
                if (resp.data.status == true) {
                }
                else {

                }

            });


        };
    };
})();


(function () {
    'use strict';

    angular
        .module('angle')
        .controller('SysprtAgriFinancePortfolioQuality', SysprtAgriFinancePortfolioQuality);

        SysprtAgriFinancePortfolioQuality.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', '$sce'];

    function SysprtAgriFinancePortfolioQuality($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, $sce) {
        var vm = this;
        vm.title = 'SysprtAgriFinancePortfolioQuality';


        activate();
        function fullscreenchanged() 
{

// if (document.fullscreenElement) {
// console.log(

// `Element: ${document.fullscreenElement.id} entered fullscreen mode.`
// );

//  } 
// else 
// {

// console.log("Leaving fullscreen mode.");

// }

}

const el = document.getElementById("fullscreen-div");

el.addEventListener("fullscreenchange", fullscreenchanged);

el.onfullscreenchange = fullscreenchanged;

// When the toggle button is clicked, enter/exit fullscreen

document

 .getElementById("toggle-fullscreen")

 .addEventListener("click", function(event)
  {

if (document.fullscreenElement) {

// exitFullscreen is only available on the Document object.
 document.exitFullscreen();
 }
 else
 { el.requestFullscreen();
 }

 });

        function activate() {
            const models = window['powerbi-client'].models;
            var url = 'api/SamRpt/Report1';
            var param = {
                report_id: '60382324-103e-47e5-9d80-d36707ce59c4',
                workspace_Id : '364f27ad-9f3a-4046-bfdb-84f304cd9a2d',
                dataset:'8188433d-e606-431f-8fdb-e1fba7951c5e',
                roles:'RM,Manager,Superuser'
            }

            SocketService.getparams(url, param).then(function (resp) { 
                var response = JSON.parse(resp.data);
                console.log(resp.data);
                console.log(response.EmbedReports[0].EmbedUrl);
                console.log(response.EmbedToken.token);
                
                
                var embedConfiguration = {

                    type: 'report',
                    tokenType: models.TokenType.Embed,
                    accessToken: response.EmbedToken.token,
                    embedUrl: response.EmbedReports[0].EmbedUrl,
                    id: response.EmbedReports[0].ReportId,
                    permissions: models.Permissions.All,
                    settings: {
                        // Enable this setting to remove gray shoulders from embedded report
                        // background: models.BackgroundType.Transparent,
                        filterPaneEnabled: true,
                        navContentPaneEnabled: true
                    }
               
                }; 

                var $reportContainer = $('#dashboardContainer');

                var report = powerbi.embed($reportContainer.get(0), embedConfiguration);

                var element = $('#dashboardContainer');
 
                var report = powerbi.get(element);
            });
            var url = 'api/SamRpt/PostAuditView';

            var params = {
                page_name: 'AgriFinance Portfolio Quality ',
                page_head: 'Mobile'
            };
            SocketService.getparams(url, params).then(function (resp) {
                if (resp.data.status == true) {
                }
                else {

                }

            });


        };
    };
})();


(function () {
    'use strict';

    angular
        .module('angle')
        .controller('SysprtAgriFinanceSnapshot', SysprtAgriFinanceSnapshot);

        SysprtAgriFinanceSnapshot.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', '$sce'];

    function SysprtAgriFinanceSnapshot($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, $sce) {
        var vm = this;
        vm.title = 'SysprtAgriFinanceSnapshot';


        activate();

function fullscreenchanged() 
{

// if (document.fullscreenElement) {
// console.log(

// `Element: ${document.fullscreenElement.id} entered fullscreen mode.`
// );

//  } 
// else 
// {

// console.log("Leaving fullscreen mode.");

// }

}

const el = document.getElementById("fullscreen-div");

el.addEventListener("fullscreenchange", fullscreenchanged);

el.onfullscreenchange = fullscreenchanged;

// When the toggle button is clicked, enter/exit fullscreen

document

 .getElementById("toggle-fullscreen")

 .addEventListener("click", function(event)
  {

if (document.fullscreenElement) {

// exitFullscreen is only available on the Document object.
 document.exitFullscreen();
 }
 else
 { el.requestFullscreen();
 }

 });


        function activate() {
            const models = window['powerbi-client'].models;
            var url = 'api/SamRpt/Report1';
            var param = {
                report_id: '8c0b9403-ecf6-40c2-bbbc-f678f560acb9',
                workspace_Id : '364f27ad-9f3a-4046-bfdb-84f304cd9a2d',
                dataset:'756cfbfa-71a2-4639-8223-118ca508b6ef',
                roles:'RM,Manager,Superuser'
            }

            SocketService.getparams(url, param).then(function (resp) { 
                var response = JSON.parse(resp.data);
                console.log(resp.data);
                console.log(response.EmbedReports[0].EmbedUrl);
                console.log(response.EmbedToken.token);
                
                
                var embedConfiguration = {

                    type: 'report',
                    tokenType: models.TokenType.Embed,
                    accessToken: response.EmbedToken.token,
                    embedUrl: response.EmbedReports[0].EmbedUrl,
                    id: response.EmbedReports[0].ReportId,
                    permissions: models.Permissions.All,
                    settings: {
                        // Enable this setting to remove gray shoulders from embedded report
                        // background: models.BackgroundType.Transparent,
                        filterPaneEnabled: true,
                        navContentPaneEnabled: true
                    }
               
                }; 

                var $reportContainer = $('#dashboardContainer');

                var report = powerbi.embed($reportContainer.get(0), embedConfiguration);

                var element = $('#dashboardContainer');
 
                var report = powerbi.get(element);
            });
            var url = 'api/SamRpt/PostAuditView';

            var params = {
                page_name: 'AgriFinance Snapshot',
                page_head: 'Mobile'
            };
            SocketService.getparams(url, params).then(function (resp) {
                if (resp.data.status == true) {
                }
                else {

                }

            });


        };
    };
})();


(function () {
    'use strict';

    angular
        .module('angle')
        .controller('SysprtAgriFinanceSummary', SysprtAgriFinanceSummary);

        SysprtAgriFinanceSummary.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', '$sce'];

    function SysprtAgriFinanceSummary($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, $sce) {
        var vm = this;
        vm.title = 'SysprtAgriFinanceSummary';


        activate();

        function fullscreenchanged() 
{

// if (document.fullscreenElement) {
// console.log(

// `Element: ${document.fullscreenElement.id} entered fullscreen mode.`
// );

//  } 
// else 
// {

// console.log("Leaving fullscreen mode.");

// }

}

const el = document.getElementById("fullscreen-div");

el.addEventListener("fullscreenchange", fullscreenchanged);

el.onfullscreenchange = fullscreenchanged;

// When the toggle button is clicked, enter/exit fullscreen

document

 .getElementById("toggle-fullscreen")

 .addEventListener("click", function(event)
  {

if (document.fullscreenElement) {

// exitFullscreen is only available on the Document object.
 document.exitFullscreen();
 }
 else
 { el.requestFullscreen();
 }

 });

        function activate() {
            const models = window['powerbi-client'].models;
            var url = 'api/SamRpt/Report1';
            var param = {
                report_id: 'bda0ebff-d3d6-4b0d-8dde-a78835923395',
                workspace_Id : '364f27ad-9f3a-4046-bfdb-84f304cd9a2d',
                dataset:'a8de171b-916f-42a7-a0e1-675f11b3e33a',
                roles:'RM,Manager,Superuser'
            }

            SocketService.getparams(url, param).then(function (resp) { 
                var response = JSON.parse(resp.data);
                console.log(resp.data);
                console.log(response.EmbedReports[0].EmbedUrl);
                console.log(response.EmbedToken.token);
                
                
                var embedConfiguration = {

                    type: 'report',
                    tokenType: models.TokenType.Embed,
                    accessToken: response.EmbedToken.token,
                    embedUrl: response.EmbedReports[0].EmbedUrl,
                    id: response.EmbedReports[0].ReportId,
                    permissions: models.Permissions.All,
                    settings: {
                        // Enable this setting to remove gray shoulders from embedded report
                        // background: models.BackgroundType.Transparent,
                        filterPaneEnabled: true,
                        navContentPaneEnabled: true
                    }
               
                }; 

                var $reportContainer = $('#dashboardContainer');

                var report = powerbi.embed($reportContainer.get(0), embedConfiguration);

                var element = $('#dashboardContainer');
 
                var report = powerbi.get(element);
            });
            var url = 'api/SamRpt/PostAuditView';

            var params = {
                page_name: 'AgriFinance Summary ',
                page_head: 'Mobile'
            };
            SocketService.getparams(url, params).then(function (resp) {
                if (resp.data.status == true) {
                }
                else {

                }

            });


        };
    };
})();


(function () {
    'use strict';

    angular
        .module('angle')
        .controller('SysprtKnowYourCustomerCommerce', SysprtKnowYourCustomerCommerce);

        SysprtKnowYourCustomerCommerce.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', '$sce'];

    function SysprtKnowYourCustomerCommerce($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, $sce) {
        var vm = this;
        vm.title = 'SysprtKnowYourCustomerCommerce';


        activate();
        function fullscreenchanged() 
{

// if (document.fullscreenElement) {
// console.log(

// `Element: ${document.fullscreenElement.id} entered fullscreen mode.`
// );

//  } 
// else 
// {

// console.log("Leaving fullscreen mode.");

// }

}

const el = document.getElementById("fullscreen-div");

el.addEventListener("fullscreenchange", fullscreenchanged);

el.onfullscreenchange = fullscreenchanged;

// When the toggle button is clicked, enter/exit fullscreen

document

 .getElementById("toggle-fullscreen")

 .addEventListener("click", function(event)
  {

if (document.fullscreenElement) {

// exitFullscreen is only available on the Document object.
 document.exitFullscreen();
 }
 else
 { el.requestFullscreen();
 }

 });

        function activate() {
            const models = window['powerbi-client'].models;
            var url = 'api/SamRpt/Report1';
            var param = {
                report_id: 'f9b3223b-c519-4762-afa7-358e3b764211',
                workspace_Id : '364f27ad-9f3a-4046-bfdb-84f304cd9a2d',
                dataset:'b37f8ddf-0187-46ec-93c0-5041b3b6f45f',
                roles:'RM,Manager,Superuser'
            }

            SocketService.getparams(url, param).then(function (resp) { 
                var response = JSON.parse(resp.data);
                console.log(resp.data);
                console.log(response.EmbedReports[0].EmbedUrl);
                console.log(response.EmbedToken.token);
                
                
                var embedConfiguration = {

                    type: 'report',
                    tokenType: models.TokenType.Embed,
                    accessToken: response.EmbedToken.token,
                    embedUrl: response.EmbedReports[0].EmbedUrl,
                    id: response.EmbedReports[0].ReportId,
                    permissions: models.Permissions.All,
                    settings: {
                        // Enable this setting to remove gray shoulders from embedded report
                        // background: models.BackgroundType.Transparent,
                        filterPaneEnabled: true,
                        navContentPaneEnabled: true
                    }
               
                }; 

                var $reportContainer = $('#dashboardContainer');

                var report = powerbi.embed($reportContainer.get(0), embedConfiguration);

                var element = $('#dashboardContainer');
 
                var report = powerbi.get(element);
            });
            var url = 'api/SamRpt/PostAuditView';

            var params = {
                page_name: 'Know Your Customer - Commerce',
                page_head: 'Mobile'
            };
            SocketService.getparams(url, params).then(function (resp) {
                if (resp.data.status == true) {
                }
                else {

                }

            });


        };
    };
})();


(function () {
    'use strict';

    angular
        .module('angle')
        .controller('SysprtKnowYourCustomerFinance', SysprtKnowYourCustomerFinance);

        SysprtKnowYourCustomerFinance.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', '$sce'];

    function SysprtKnowYourCustomerFinance($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, $sce) {
        var vm = this;
        vm.title = 'SysprtKnowYourCustomerFinance';


        activate();

        function fullscreenchanged() 
{

// if (document.fullscreenElement) {
// console.log(

// `Element: ${document.fullscreenElement.id} entered fullscreen mode.`
// );

//  } 
// else 
// {

// console.log("Leaving fullscreen mode.");

// }

}

const el = document.getElementById("fullscreen-div");

el.addEventListener("fullscreenchange", fullscreenchanged);

el.onfullscreenchange = fullscreenchanged;

// When the toggle button is clicked, enter/exit fullscreen

document

 .getElementById("toggle-fullscreen")

 .addEventListener("click", function(event)
  {

if (document.fullscreenElement) {

// exitFullscreen is only available on the Document object.
 document.exitFullscreen();
 }
 else
 { el.requestFullscreen();
 }

 });

        function activate() {
            const models = window['powerbi-client'].models;
            var url = 'api/SamRpt/Report1';
            var param = {
                report_id: 'b411a6a7-1ffc-452a-ad5c-5288141d4e1f',
                workspace_Id : '364f27ad-9f3a-4046-bfdb-84f304cd9a2d',
                dataset:'e791089f-abc6-4af0-8b84-2b86ed96dcb3',
                roles:'RM,Manager,Superuser'
            }

            SocketService.getparams(url, param).then(function (resp) { 
                var response = JSON.parse(resp.data);
                console.log(resp.data);
                console.log(response.EmbedReports[0].EmbedUrl);
                console.log(response.EmbedToken.token);
                
                
                var embedConfiguration = {

                    type: 'report',
                    tokenType: models.TokenType.Embed,
                    accessToken: response.EmbedToken.token,
                    embedUrl: response.EmbedReports[0].EmbedUrl,
                    id: response.EmbedReports[0].ReportId,
                    permissions: models.Permissions.All,
                    settings: {
                        // Enable this setting to remove gray shoulders from embedded report
                        // background: models.BackgroundType.Transparent,
                        filterPaneEnabled: true,
                        navContentPaneEnabled: true
                    }
               
                }; 

                var $reportContainer = $('#dashboardContainer');

                var report = powerbi.embed($reportContainer.get(0), embedConfiguration);

                var element = $('#dashboardContainer');
 
                var report = powerbi.get(element);
            });
            var url = 'api/SamRpt/PostAuditView';

            var params = {
                page_name: 'Know Your Customer - Finance',
                page_head: 'Mobile'
            };
            SocketService.getparams(url, params).then(function (resp) {
                if (resp.data.status == true) {
                }
                else {

                }

            });


        };
    };
})();


(function () {
    'use strict';

    angular
        .module('angle')
        .controller('SysprtKnowYourOrganization', SysprtKnowYourOrganization);

        SysprtKnowYourOrganization.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', '$sce'];

    function SysprtKnowYourOrganization($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, $sce) {
        var vm = this;
        vm.title = 'SysprtKnowYourOrganization';


        activate();
        function fullscreenchanged() 
{

// if (document.fullscreenElement) {
// console.log(

// `Element: ${document.fullscreenElement.id} entered fullscreen mode.`
// );

//  } 
// else 
// {

// console.log("Leaving fullscreen mode.");

// }

}

const el = document.getElementById("fullscreen-div");

el.addEventListener("fullscreenchange", fullscreenchanged);

el.onfullscreenchange = fullscreenchanged;

// When the toggle button is clicked, enter/exit fullscreen

document

 .getElementById("toggle-fullscreen")

 .addEventListener("click", function(event)
  {

if (document.fullscreenElement) {

// exitFullscreen is only available on the Document object.
 document.exitFullscreen();
 }
 else
 { el.requestFullscreen();
 }

 });

        function activate() {
            const models = window['powerbi-client'].models;
            var url = 'api/SamRpt/Report1';
            var param = {
                report_id: '1595695e-50cd-4503-97a2-0aa71a36982d',
                workspace_Id : '364f27ad-9f3a-4046-bfdb-84f304cd9a2d',
                dataset:'186f70b3-ee68-47c2-b672-7b0a1437337d',
                roles:'RM,Manager,Superuser'
            }

            SocketService.getparams(url, param).then(function (resp) { 
                var response = JSON.parse(resp.data);
                console.log(resp.data);
                console.log(response.EmbedReports[0].EmbedUrl);
                console.log(response.EmbedToken.token);
                
                
                var embedConfiguration = {

                    type: 'report',
                    tokenType: models.TokenType.Embed,
                    accessToken: response.EmbedToken.token,
                    embedUrl: response.EmbedReports[0].EmbedUrl,
                    id: response.EmbedReports[0].ReportId,
                    permissions: models.Permissions.All,
                    settings: {
                        // Enable this setting to remove gray shoulders from embedded report
                        // background: models.BackgroundType.Transparent,
                        filterPaneEnabled: true,
                        navContentPaneEnabled: true
                    }
               
                }; 

                var $reportContainer = $('#dashboardContainer');

                var report = powerbi.embed($reportContainer.get(0), embedConfiguration);

                var element = $('#dashboardContainer');
 
                var report = powerbi.get(element);
            });
            var url = 'api/SamRpt/PostAuditView';

            var params = {
                page_name: 'Know Your Organization',
                page_head: 'Mobile'
            };
            SocketService.getparams(url, params).then(function (resp) {
                if (resp.data.status == true) {
                }
                else {

                }

            });


        };
    };
})();


(function () {
    'use strict';

    angular
        .module('angle')
        .controller('SysRptESignController', SysRptESignController);

    SysRptESignController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', '$modal', 'DownloaddocumentService'];

    function SysRptESignController($rootScope, $scope, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, $modal, DownloaddocumentService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'SysRptESignController';

        activate();

        function activate() {
            var url = 'api/SysMstHRDocument/GetESignUnsignedSummary';
            SocketService.get(url).then(function (resp) {
                $scope.document_list = resp.data.hrdoc;
            });

            var url = 'api/SysMstHRDocument/GetESignReportSummaryCount';
            SocketService.get(url).then(function (resp) {
                $scope.pendingesign_count = resp.data.pendingesign_count;
                $scope.completedesign_count = resp.data.completedesign_count;
                $scope.expiredesign_count = resp.data.expiredesign_count;
            });

        }

        $scope.UnSigned = function () {
            $state.go('app.SysRptEsign');
        }

        $scope.Signed = function () {
            $state.go('app.SysRptESignSignedDoc');
        }

        $scope.Expired = function () {
            $state.go('app.SysRptESignExpiredDoc');
        }

        $scope.esignreporthrdoc = function () {


            var url = 'api/SysMstHRDocument/GetESignReportHRDocExcelExport';
            SocketService.get(url).then(function (resp) {
                if (resp.data.status == true) {
                    DownloaddocumentService.Downloaddocument(resp.data.lscloudpath, resp.data.lsname);
                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'warning',
                        pos: 'top-center',
                        timeout: 3000
                    }); activate();
                }

            });
        }
    }
})();

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('SysRptESignExpiredDocController', SysRptESignSignedDocController);

    SysRptESignSignedDocController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', '$modal', 'DownloaddocumentService'];

    function SysRptESignSignedDocController($rootScope, $scope, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, $modal, DownloaddocumentService) {

        var vm = this;
        vm.title = 'SysRptESignExpiredDocController';

        activate();

        function activate() {
            var url = 'api/SysMstHRDocument/GetESignExpiredSummary';
            SocketService.get(url).then(function (resp) {
                $scope.document_list = resp.data.hrdoc;
            });

            var url = 'api/SysMstHRDocument/GetESignReportSummaryCount';
            SocketService.get(url).then(function (resp) {
                $scope.pendingesign_count = resp.data.pendingesign_count;
                $scope.completedesign_count = resp.data.completedesign_count;
                $scope.expiredesign_count = resp.data.expiredesign_count;
            });

        }
        $scope.UnSigned = function () {
            $state.go('app.SysRptEsign');
        }

        $scope.Expired = function () {
            $state.go('app.SysRptESignExpiredDoc');
        }

        $scope.Signed = function () {
            $state.go('app.SysRptESignSignedDoc');
        }
        
        $scope.esignreporthrdoc = function () {


            var url = 'api/SysMstHRDocument/GetESignReportHRDocExcelExport';
            SocketService.get(url).then(function (resp) {
                if (resp.data.status == true) {
                    DownloaddocumentService.Downloaddocument(resp.data.lscloudpath, resp.data.lsname);
                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'warning',
                        pos: 'top-center',
                        timeout: 3000
                    }); activate();
                }

            });
        }
    }
})();


(function () {
    'use strict';

    angular
        .module('angle')
        .controller('SysRptESignSignedDocController', SysRptESignSignedDocController);

    SysRptESignSignedDocController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', '$modal', 'DownloaddocumentService'];

    function SysRptESignSignedDocController($rootScope, $scope, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, $modal, DownloaddocumentService) {

        var vm = this;
        vm.title = 'SysRptESignSignedDocController';

        activate();

        function activate() {
            var url = 'api/SysMstHRDocument/GetESignSignedSummary';
            SocketService.get(url).then(function (resp) {
                $scope.document_list = resp.data.hrdoc;
            });

            var url = 'api/SysMstHRDocument/GetESignReportSummaryCount';
            SocketService.get(url).then(function (resp) {
                $scope.pendingesign_count = resp.data.pendingesign_count;
                $scope.completedesign_count = resp.data.completedesign_count; 
                $scope.expiredesign_count = resp.data.expiredesign_count;
            });

        }
        $scope.UnSigned = function () {
            $state.go('app.SysRptEsign');
        }

        $scope.Expired = function () {
            $state.go('app.SysRptESignExpiredDoc');
        }

        $scope.Expired = function () {
            $state.go('app.SysRptESignExpiredDoc');
        }

        $scope.esignreporthrdoc = function () {


            var url = 'api/SysMstHRDocument/GetESignReportHRDocExcelExport';
            SocketService.get(url).then(function (resp) {
                if (resp.data.status == true) {
                    DownloaddocumentService.Downloaddocument(resp.data.lscloudpath, resp.data.lsname);
                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'warning',
                        pos: 'top-center',
                        timeout: 3000
                    }); activate();
                }

            });
        }
    }
})();

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('SystemDashboardController', SystemDashboardController);

        SystemDashboardController.$inject = ['$rootScope', '$scope', '$state', '$modal', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', '$route', '$cookies'];

    function SystemDashboardController($rootScope, $scope, $state, $modal, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, $route, $cookies) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'SystemDashboardController';

        activate();

        function activate() {
            var user_gid = localStorage.getItem('user_gid');
            var url = 'api/user/privilegelevel3';
            SocketService.get(url + '?user_gid=' + user_gid).then(function (resp) {
                var BloodGroup = resp.data.privilegelevel3.map(function (e) { return e.project }).indexOf("SYSTRNBGM");
                var BaseLocation = resp.data.privilegelevel3.map(function (e) { return e.project }).indexOf("SYSTRNBLM");
                var BusinessHead = resp.data.privilegelevel3.map(function (e) { return e.project }).indexOf("SYSTRNBSH");
                var CalendarGroup = resp.data.privilegelevel3.map(function (e) { return e.project }).indexOf("SYSTRNCGM");
                var ClientRole = resp.data.privilegelevel3.map(function (e) { return e.project }).indexOf("SYSTRNCRM");
                var Employee = resp.data.privilegelevel3.map(function (e) { return e.project }).indexOf("SYSTRNEMP");
                var GroupBusinessHead = resp.data.privilegelevel3.map(function (e) { return e.project }).indexOf("SYSTRNGBH");
                var OtherApplications = resp.data.privilegelevel3.map(function (e) { return e.project }).indexOf("SYSTRNOTA");
                var ProductHead = resp.data.privilegelevel3.map(function (e) { return e.project }).indexOf("SYSTRNPRH");
                var PhysicalStatus = resp.data.privilegelevel3.map(function (e) { return e.project }).indexOf("SYSTRNPSM");
                var Projects = resp.data.privilegelevel3.map(function (e) { return e.project }).indexOf("SYSTRNPTM");                
                var Salutation = resp.data.privilegelevel3.map(function (e) { return e.project }).indexOf("SYSTRNSTM");
                var TriggerUser = resp.data.privilegelevel3.map(function (e) { return e.project }).indexOf("SYSTRNTUM");                
                var rolemanagement = resp.data.privilegelevel3.map(function (e) { return e.project }).indexOf("SYSSETRMN");

                if (BloodGroup != -1) {
                    $scope.BloodGroup_show = 'Y';
                }if (BaseLocation != -1) {
                    $scope.BaseLocation_show = 'Y';
                }if (BusinessHead != -1) {
                    $scope.BusinessHead_show = 'Y';
                }if (CalendarGroup != -1) {
                    $scope.CalendarGroup_show = 'Y';
                }if (ClientRole != -1) {
                    $scope.ClientRole_show = 'Y';
                }if (Employee != -1) {
                    $scope.Employee_show = 'Y';
                }if (GroupBusinessHead != -1) {
                    $scope.GroupBusinessHead_show = 'Y';
                }if (OtherApplications != -1) {
                    $scope.OtherApplications_show = 'Y';
                }if (ProductHead != -1) {
                    $scope.ProductHead_show = 'Y';
                }if (PhysicalStatus != -1) {
                    $scope.PhysicalStatus_show = 'Y';
                }if (Projects != -1) {
                    $scope.Projects_show = 'Y';
                }if (Salutation != -1) {
                    $scope.Salutation_show = 'Y';
                }if (TriggerUser != -1) {
                    $scope.TriggerUser_show = 'Y';
                }if (rolemanagement != -1) {
                    $scope.RoleManagement_show = 'Y';
                }
            });
            
        }
    }
})();
