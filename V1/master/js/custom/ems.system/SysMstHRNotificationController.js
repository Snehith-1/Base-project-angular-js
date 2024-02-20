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