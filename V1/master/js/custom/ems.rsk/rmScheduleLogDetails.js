(function () {
    'use strict';

    angular
        .module('angle')
        .controller('rmScheduleLogDetails', rmScheduleLogDetails);

    rmScheduleLogDetails.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', 'ScopeValueService', '$modal', '$http', 'SocketService', 'Notify', '$location', '$route', '$cookies', '$filter', 'ngTableParams', '$timeout'];

    function rmScheduleLogDetails($rootScope, $scope, $state, AuthenticationService, ScopeValueService, $modal, $http, SocketService, Notify, $location, $route, $cookies, $filter, ngTableParams, $timeout) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'rmScheduleLogDetails';
        var  allocationdtl_gid=$location.search().allocationdtl_gid;
      
        activate();

        function activate() {

            var params = {
                allocationdtl_gid: allocationdtl_gid
            }
            var url = "api/visitReport/GetAllocationLogDetail";
            SocketService.getparams(url, params).then(function (resp) {
                $scope.customer_name = resp.data.customer_name;
                $scope.customer_gid = resp.data.customer_gid;
                $scope.customer_urn = resp.data.customer_urn;
                $scope.allocated_date = resp.data.allocated_date;
                $scope.scheduleList = resp.data.schedulelogdtl;
                $scope.calllogdtlList = resp.data.calllogdtl;
            });
        }

        $scope.callLogDetails = function (customer_gid) {

            var modalInstance = $modal.open({
                templateUrl: '/callLogDetailsModal.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.callLogsubmit = function () {
                    var params = {
                        allocationdtl_gid: allocationdtl_gid,
                        customer_gid: customer_gid,
                        dialed_number: $scope.txtdialed_number,
                        call_response: $scope.txtcall_response,
                        call_remarks: $scope.txtcall_remarks
                    }

                    var url = "api/visitReport/PostCallLog"
                    SocketService.post(url, params).then(function (resp) {

                        if (resp.data.status == true) {

                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            unlockUI();
                            $modalInstance.close('closed');
                            activate();
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
            }
        }

        $scope.callLogDetailsedit = function (calllog_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/callLogEditDetails.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    calllog_gid: calllog_gid
                }
                var url = 'api/visitReport/GetCallLogDetails';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.txtdialed_number = resp.data.dialed_number;
                    $scope.txtcall_response = resp.data.call_response;
                    $scope.txtcall_remarks = resp.data.call_remarks;
                });
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.editcallLogsubmit = function () {

                    var params = {
                        calllog_gid: calllog_gid,
                        dialed_number: $scope.txtdialed_number,
                        call_response: $scope.txtcall_response,
                        call_remarks: $scope.txtcall_remarks
                    }

                    var url = 'api/visitReport/patchCallLogUpdate';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {
                            $modalInstance.close('closed');
                            Notify.alert(resp.data.message, 'success')
                        }
                        else {
                            $modalInstance.close('closed');
                            Notify.alert(resp.data.message, 'Warning')
                        }
                        activate();
                    });
                }
            }

        }

        $scope.scheduleLogDetails = function (customer_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/schedule.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                vm.mytime = new Date();
                vm.hstep = 1;
                vm.mstep = 15;
                vm.ismeridian = false;
                vm.calender1 = function ($event) {
                    $event.preventDefault();
                    $event.stopPropagation();

                    vm.open1 = true;
                };
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };


                $scope.scheduleappointment = function () {
                    var scheduletime, selectedtime;
                    if ($scope.txtappointment_time == undefined) {
                        var today = new Date();
                        scheduletime = today.getHours() + ":" + today.getMinutes();
                    }
                    else {
                        selectedtime = $scope.txtappointment_time;
                        scheduletime = selectedtime.getHours() + ":" + selectedtime.getMinutes();
                    }
                    var appointment_date = new Date();
                    appointment_date.setFullYear($scope.txtappointmentdate.getFullYear());
                    appointment_date.setMonth($scope.txtappointmentdate.getMonth());
                    appointment_date.setDate($scope.txtappointmentdate.getDate());
                    var params = {
                        allocationdtl_gid:allocationdtl_gid,
                        customer_gid: customer_gid,
                        appointment_date: appointment_date,
                        appointment_time: scheduletime,
                        appointment_status: $scope.txtappointmentstatus,
                        appointment_remarks: $scope.txtremarks,
                    }

                    var url = "api/visitReport/PostScheduleLog"
                    SocketService.post(url, params).then(function (resp) {

                        if (resp.data.status == true) {

                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            unlockUI();
                            $modalInstance.close('closed');
                            activate();
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
            }
        }


        $scope.scheduleLogDetailsEdit = function (schedulelog_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/Editschedule.html',
                controller: ModalInstanceCtrl,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                vm.mytime = new Date();
                vm.hstep = 1;
                vm.mstep = 15;
                vm.ismeridian = false;
                vm.calender1 = function ($event) {
                    $event.preventDefault();
                    $event.stopPropagation();

                    vm.open1 = true;
                };
                var params = {
                    schedulelog_gid: schedulelog_gid
                }

                var url = 'api/visitReport/GetScheduleLogDetails';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.txtappointmentdate = new Date(resp.data.appointment_Date);
                    $scope.txtappointment_time = new Date(resp.data.appointment_Time);
                    $scope.txtappointmentstatus = resp.data.appointment_status;
                    $scope.txtremarks = resp.data.appointment_remarks;
                });
                
                var url = "api/visitReport/GetScheduleLogHistory";
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.scheduleList = resp.data.schedulelogdtl;
                });
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.scheduleappointmentedit = function () {

                    var params = {
                        schedulelog_gid: schedulelog_gid,
                        appointment_date: $scope.txtappointmentdate,
                        appointment_time: $scope.txtappointment_time,
                        appointment_status: $scope.txtappointmentstatus,
                        appointment_remarks: $scope.txtremarks,
                    }
                    
                    var url = 'api/visitReport/patchScheduleLogUpdate';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                       
                        if (resp.data.status == true) {

                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            unlockUI();
                            var url = "api/visitReport/GetScheduleLogHistory";
                            SocketService.getparams(url, params).then(function (resp) {
                                $scope.scheduleList = resp.data.schedulelogdtl;
                            });
                            $scope.current = $state.current.name;
                            ScopeValueService.store("dataldCtrl", $scope);
                            $location.url('app/rmScheduleLogDetails?allocationdtl_gid='+allocationdtl_gid);
                           // $state.go('app.pageredirect');
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
            }

        }

        $scope.scheduleLoghistory = function (schedulelog_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/SchedulehistoryModal.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    schedulelog_gid: schedulelog_gid
                }
                var url = "api/visitReport/GetScheduleLogHistory";
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.scheduleList = resp.data.schedulelogdtl;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
            
            }
        }

        $scope.ScheduleStatus = function (schedulelog_gid, schedule_status) {

            var modalInstance = $modal.open({
                templateUrl: '/ScheduleStatusModal.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
 
                $scope.cboschedule_status = schedule_status;
                $scope.ScheduleStatusUpdate = function () {
                    lockUI();

                    var params = {
                        schedule_status: $scope.cboschedule_status,
                        schedulelog_gid: schedulelog_gid,
                    }

                    var url = "api/visitReport/PostScheduleStatus";
                    SocketService.post(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            unlockUI();
                            $modalInstance.close('closed');
                            $scope.current = $state.current.name;
                            ScopeValueService.store("dataldCtrl", $scope);
                            $location.url('app/rmScheduleLogDetails?allocationdtl_gid='+allocationdtl_gid);
                            activate();
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
            }
        }
 
    }
})();
