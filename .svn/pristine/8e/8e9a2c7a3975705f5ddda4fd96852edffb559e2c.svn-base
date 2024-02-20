(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AgrTrnCCMeetingRescheduleController', AgrTrnCCMeetingRescheduleController);

    AgrTrnCCMeetingRescheduleController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function AgrTrnCCMeetingRescheduleController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AgrTrnCCMeetingRescheduleController';
        $scope.application_gid = $location.search().application_gid;
        var application_gid = $scope.application_gid;
        $scope.ccschedulemeeting_gid = $location.search().ccschedulemeeting_gid;
        var ccschedulemeeting_gid = $scope.ccschedulemeeting_gid;
      
        activate();
        function activate() {
            $scope.cboeditccgroup_name = [];
            $scope.cboother_user = [];
            $scope.cboccadmin_name = [];

             // Calender Popup... //

             vm.calender2 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();

                vm.open2 = true;
            };
             vm.formats = ['dd-MM-yyyy'];
            vm.format = vm.formats[0];
            vm.dateOptions = {
                formatYear: 'yy',
                startingDay: 1
            };

           
            var params = {
                application_gid: application_gid
            }
            var url = 'api/AgrMstCCMember/Getccgroup';
            SocketService.get(url).then(function (resp) {
                $scope.ccgroup_list = resp.data.ccgroup_list;
            });
            var url = 'api/employee/employee';
            SocketService.get(url).then(function (resp) {
                $scope.employee_list = resp.data.employee_list;
            });
            var params = {
                application_gid: application_gid
            }
            var url = "api/AgrTrnCC/GetScheduleMeeting";
            SocketService.getparams(url, params).then(function (resp) {
                $scope.txteditccmeeting_title = resp.data.ccmeeting_title;
                $scope.txteditccmeeting_no = resp.data.ccmeeting_no;
                $scope.ccmember_list = resp.data.ccmember_list;
                $scope.txteditccmeeting_date = resp.data.meeting_date;
                $scope.txteditccmeeting_date = Date.parse($scope.txteditccmeeting_date);
                if (resp.data.Tstart_time == '0001-01-01T00:00:00')
                {
                    $scope.txteditstart_time = '';
                }
                else {
                    $scope.txteditstart_time = new Date(resp.data.Tstart_time);
                }
                if (resp.data.Tend_time == '0001-01-01T00:00:00') {
                    $scope.txteditend_time = '';
                }
                else {
                    $scope.txteditend_time = new Date(resp.data.Tend_time);
                }
               
                $scope.rdbeditccmeeting_mode = resp.data.ccmeeting_mode;
                $scope.txteditdescription = resp.data.description;
                $scope.txtnon_users = resp.data.non_users;
               if (resp.data.ccschedule_list != null) {
                    var count = resp.data.ccschedule_list.length;
                    for (var i = 0; i < count; i++) {
                        var indexs = $scope.ccgroup_list.map(function (x) { return x.ccgroupname_gid; }).indexOf(resp.data.ccschedule_list[i].ccgroupname_gid);
                        $scope.cboeditccgroup_name.push($scope.ccgroup_list[indexs]);
                        $scope.$parent.cboeditccgroup_name = $scope.cboeditccgroup_name;
                    }
                }
                if (resp.data.otheremployee_list != null) {
                    var count = resp.data.otheremployee_list.length;
                    for (var i = 0; i < count; i++) {
                        var indexs = $scope.employee_list.map(function (x) { return x.employee_gid; }).indexOf(resp.data.otheremployee_list[i].employee_gid);
                        $scope.cboother_user.push($scope.employee_list[indexs]);
                        $scope.$parent.cboother_user = $scope.cboother_user;
                    }
                }
               
                if (resp.data.ccadmin_list != null) {
                    var count = resp.data.ccadmin_list.length;
                    for (var i = 0; i < count; i++) {
                        var indexs = $scope.employee_list.map(function (x) { return x.employee_gid; }).indexOf(resp.data.ccadmin_list[i].employee_gid);
                        $scope.cboccadmin_name.push($scope.employee_list[indexs]);
                        $scope.$parent.cboccadmin_name = $scope.cboccadmin_name;
                    }
                }
            });
           
        }
        $scope.onchangeccgroup = function () {
            var params = {
                ccgroup_list: $scope.cboeditccgroup_name,
            }
            
            var url = 'api/AgrTrnCC/Getcc2member';
            SocketService.post(url, params).then(function (resp) {
                $scope.ccmember_list = resp.data.ccmember_list;
            });
            var url = 'api/AgrTrnCC/OtherEmployee';
            SocketService.post(url, params).then(function (resp) {
                $scope.employee_list = resp.data.otheremployee_list;
            });
            if ($scope.cboeditccgroup_name == undefined || $scope.cboeditccgroup_name == null || $scope.cboeditccgroup_name == '') {
                $scope.txteditccmeeting_title = '';
            }
            else {
                //$scope.txtccmeeting_title = $scope.cboccgroup_name.join(",");
                $scope.txteditccmeeting_title = $scope.cboeditccgroup_name.map(function (el) { return el.ccgroup_name }).join(",");
                /* $scope.txtccmeeting_title = $scope.cboccgroup_name .join(",");*/
            }
          /*  $scope.txteditccmeeting_title = $scope.cboeditccgroup_name.map(function (el) { return el.ccgroup_name }).join(",");*/
        }
        $scope.onchangetime = function () {
            var g1 = new Date($scope.txteditstart_time);
            var g2 = new Date($scope.txteditend_time);
            console.log(g1)
            console.log(g2)
            if (g1.getTime() == g2.getTime())
                Notify.alert("Both are equal",'warning');
            else if (g1.getTime() > g2.getTime())
                Notify.alert("Start Time is greater than End Time", 'warning');

        }
        $scope.reschedule = function () {
            if ((($scope.txteditstart_time != "") || ($scope.txteditend_time != "")) && 
            (($scope.cboccadmin_name == undefined) || ($scope.cboccadmin_name == '') || ($scope.cboccadmin_name == null)))
            {
                Notify.alert('Select CC Admin', 'warning');
            }
            else if (($scope.cboeditccgroup_name == undefined) || ($scope.cboeditccgroup_name == '') || ($scope.cboeditccgroup_name == null) ||
            ($scope.txteditccmeeting_title == undefined) || ($scope.txteditccmeeting_title == '') || ($scope.txteditccmeeting_title == null) ||
            ($scope.txteditccmeeting_no == undefined) || ($scope.txteditccmeeting_no == '') || ($scope.txteditccmeeting_no == null) ||
            ($scope.rdbeditccmeeting_mode == undefined) || ($scope.rdbeditccmeeting_mode == '') || ($scope.rdbeditccmeeting_mode == null) ||
            ($scope.txteditccmeeting_date == undefined) || ($scope.txteditccmeeting_date == '') || ($scope.txteditccmeeting_date == null))
            {
                Notify.alert('Enter All Mandatory Fields', 'warning');
            }
            else {
                if (($scope.txteditstart_time == undefined) || ($scope.txteditend_time == undefined)||($scope.txteditstart_time == "") || ($scope.txteditend_time == "")) {
                    var params = {
                        application_gid: application_gid,
                        ccmeeting_title: $scope.txteditccmeeting_title,
                        ccmeeting_no: $scope.txteditccmeeting_no,
                        meetingdate: $scope.txteditccmeeting_date,
                        start_time: $scope.txteditstart_time,
                        end_time: $scope.txteditend_time,
                        ccmeeting_mode: $scope.rdbeditccmeeting_mode,
                        ccschedule_list: $scope.cboeditccgroup_name,
                        otheremployee_list: $scope.cboother_user,
                        description: $scope.txteditdescription,
                        ccadmin_list: $scope.cboccadmin_name,
                        ccschedulemeeting_gid: ccschedulemeeting_gid
                    }
                    lockUI();
                    var url = 'api/AgrTrnCC/RecheduleMeeting';
                    SocketService.post(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            unlockUI();

                            Notify.alert(resp.data.message, 'success')
                            $location.url('app/AgrTrnCCMeetingSchedule?application_gid=' + application_gid + '&lstab=Scheduled');

                        }
                        else {
                            unlockUI();
                            Notify.alert(resp.data.message)
                        }
                        activate();
                    });
                }
                else {

                var g1 = new Date($scope.txteditstart_time);
                var g2 = new Date($scope.txteditend_time);
                if (g1.getTime() == g2.getTime())
                {
                    Notify.alert("Both are equal", 'warning');
                }
                else if (g1.getTime() > g2.getTime())
                {
                    Notify.alert("Start Time is greater than End Time", 'warning');
                }
                else if (g1.getTime() < g2.getTime()) {
                    var params = {
                        application_gid: application_gid,
                        ccmeeting_title: $scope.txteditccmeeting_title,
                        ccmeeting_no: $scope.txteditccmeeting_no,
                        meetingdate: $scope.txteditccmeeting_date,
                        start_time: $scope.txteditstart_time,
                        end_time: $scope.txteditend_time,
                        ccmeeting_mode: $scope.rdbeditccmeeting_mode,
                        ccschedule_list: $scope.cboeditccgroup_name,
                        otheremployee_list: $scope.cboother_user,
                        description: $scope.txteditdescription,
                        ccadmin_list: $scope.cboccadmin_name,
                        ccschedulemeeting_gid: ccschedulemeeting_gid
                    }
                    lockUI();
                    var url = 'api/AgrTrnCC/RecheduleMeeting';
                    SocketService.post(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            unlockUI();

                            Notify.alert(resp.data.message, 'success')
                            $location.url('app/AgrTrnCCMeetingSchedule?application_gid=' + application_gid + '&lstab=Scheduled');

                        }
                        else {
                            unlockUI();
                            Notify.alert(resp.data.message, 'warning')
                                
                             
                        }
                        activate();
                    });
                }

                }
        }
        }

        $scope.back = function () {
            $location.url('app/AgrTrnCCMeetingSchedule?application_gid=' + application_gid + '&lstab=Scheduled');
        }

        $scope.pastdatecheck = function (val) {
            var params = {
                date: val.toDateString()
            }
            var url = 'api/AgrTrnCC/PastDateCheck';
            SocketService.getparams(url, params).then(function (resp) {
                if (resp.data.status == false) {
                    Notify.alert(resp.data.message, 'warning')
                    $scope.txteditccmeeting_date = '';
                }
            });
        }
     
    }
})();
