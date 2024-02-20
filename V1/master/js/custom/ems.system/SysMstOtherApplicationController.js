
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
