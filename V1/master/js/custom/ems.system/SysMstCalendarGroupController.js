﻿(function () {
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

