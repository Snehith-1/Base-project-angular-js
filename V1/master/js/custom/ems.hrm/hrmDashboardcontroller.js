(function () {
    'use strict';

    angular
        .module('angle')
        .controller('hrmDashboardcontroller', hrmDashboardcontroller);

    hrmDashboardcontroller.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', 'Colors', 'ChartData', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', '$route', 'ngTableParams', '$timeout', 'SweetAlert', '$cookies'];

    function hrmDashboardcontroller($rootScope, $scope, $state, AuthenticationService, Colors, ChartData, $modal, ScopeValueService, $http, SocketService, Notify, $location, $route, ngTableParams, $timeout, SweetAlert, $cookies) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'hrmDashboardcontroller';

        activate();

        function activate() {
            var url = 'api/user/companydetails';
            var params = {
                user_gid: localStorage.getItem('user_gid')
            }
            SocketService.getparams(url, params).then(function (resp) {
                var pathArray = location.href.split('/');
                var url = pathArray[0] + '//' + pathArray[2];
                var str = resp.data.company_logo;
                str = str.substring(str.indexOf("EMS/") + 3);
                $scope.company_logo = url.concat(str);
                $scope.company_name = resp.data.company_name;
            });

            var url = "api/hrmDashboard/loginSummary"
            SocketService.get(url).then(function (resp) {
                $scope.loginsummary_list = resp.data.loginsummary_list;
                console.log($scope.loginsummary_list);
            });

            var url = "api/hrmDashboard/logoutSummary"
            SocketService.get(url).then(function (resp) {
                $scope.logoutsummary_list = resp.data.logoutsummary_list;
            });

            var url = "api/hrmDashboard/ondutySummary"
            SocketService.get(url).then(function (resp) {
                $scope.onduty_details = resp.data.onduty_details;
            });

            var url = "api/hrmDashboard/compOffSummary"
            SocketService.get(url).then(function (resp) {
                $scope.compoff_details = resp.data.compoffSummary_details;
                console.log($scope.compoff_details);
            });

            var url = "api/hrmDashboard/permissionSummary"
            SocketService.get(url).then(function (resp) {
                $scope.permission_details = resp.data.permissionSummary_details;
            });


            var url = "api/holidayCalender/todayactivity"
            SocketService.get(url).then(function (resp) {
                $scope.todayschdule_details = resp.data.createevent;
            });

            $scope.close = true;
            $scope.applylogininfodiv = true;
            $scope.applylogoutinfodiv = true;
            $scope.applyodinfodiv = true;
            $scope.applycompoffinfodiv = true;
            $scope.applypermissioninfodiv = true;

            // Pie Chart...//

            var url = "api/hrmDashboard/monthlyAttendence"
            SocketService.get(url).then(function (resp) {
                $scope.monthlyattendence = resp.data;
                $scope.last6monthattendence = resp.data.last6MonthAttendence_list; 
                $scope.countPresent = parseInt(resp.data.countPresent);
                $scope.countAbsent = parseInt(resp.data.countAbsent);
                $scope.countLeave = parseInt(resp.data.countLeave);
                $scope.countholiday = parseInt(resp.data.countholiday);
                $scope.countWeekOff = parseInt(resp.data.countWeekOff);
                monthlyAttendence_chart();
            });


            // TimePicker... //

            vm.mytime = new Date();
            vm.hstep = 1;
            vm.mstep = 15;
            vm.ismeridian = false;

            // Calender Popup... //

            vm.calender1 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();

                vm.open1 = true;
            };

            vm.calender2 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();

                vm.open2 = true;
            };

            vm.calender3 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();

                vm.open3 = true;
            };

            vm.calender4 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();

                vm.open4 = true;
            };

            vm.calender5 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();

                vm.open5 = true;
            };

            vm.calender6 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();

                vm.open6 = true;
            };

            vm.dateOptions = {
                formatYear: 'yy',
                startingDay: 1
            };

            vm.formats = ['dd-MM-yyyy'];
            vm.format = vm.formats[0];

            // Bar Chart....//

            var url = "api/hrmDashboard/monthlyAttendence"
            SocketService.get(url).then(function (resp) {
                var last6monthattendence = resp.data.last6MonthAttendence_list;
                if (last6monthattendence==null)
                {
                    vm.barData = {
                        labels: [],
                        datasets: [
                          {
                              fillColor: Colors.byName('green'),
                              strokeColor: Colors.byName('green'),
                              highlightFill: Colors.byName('info'),
                              highlightStroke: Colors.byName('info'),
                              data: []
                          },
                          {
                              fillColor: Colors.byName('danger'),
                              strokeColor: Colors.byName('danger'),
                              highlightFill: Colors.byName('primary'),
                              highlightStroke: Colors.byName('primary'),
                              data: []
                          }
                        ]
                    };
                }
                else {

                    vm.barData = {
                        labels: [last6monthattendence[4].monthname, last6monthattendence[3].monthname, last6monthattendence[2].monthname, last6monthattendence[1].monthname, last6monthattendence[0].monthname],
                        datasets: [
                          {
                              fillColor: Colors.byName('green'),
                              strokeColor: Colors.byName('green'),
                              highlightFill: Colors.byName('info'),
                              highlightStroke: Colors.byName('info'),
                              data: [last6monthattendence[4].countPresent, last6monthattendence[3].countPresent, last6monthattendence[2].countPresent, last6monthattendence[1].countPresent, last6monthattendence[0].countPresent]
                          },
                          {
                              fillColor: Colors.byName('danger'),
                              strokeColor: Colors.byName('danger'),
                              highlightFill: Colors.byName('primary'),
                              highlightStroke: Colors.byName('primary'),
                              data: [last6monthattendence[4].countAbsent, last6monthattendence[3].countAbsent, last6monthattendence[2].countAbsent, last6monthattendence[1].countAbsent, last6monthattendence[0].countAbsent]
                          }
                        ]
                    };
                }

            });
    

            vm.barOptions = {
                scaleBeginAtZero: true,
                scaleShowGridLines: true,
                scaleGridLineColor: 'rgba(0,0,0,.05)',
                scaleGridLineWidth: 1,
                barShowStroke: true,
                barStrokeWidth: 2,
                barValueSpacing: 5,
                barDatasetSpacing: 1,
                animationSteps: 100,
                animationEasing: 'easeInOutSine',
            };
            var url = "api/hrmDashboard/iattendence"
            SocketService.get(url).then(function (resp) {
                console.log(resp);
                $scope.punchstatus = resp.data.update_flag;
                $scope.login_time_audit = resp.data.login_time_audit;
                $scope.iattendence_privilege = resp.data.iattendence_privilege;
            });

            vm.open = function (size) {
                $modal.open({
                    templateUrl: '/myModalContent.html',
                    controller: ModalInstanceCtrl,
                    size: size
                });
            };

            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance', '$timeout', '$cookies', 'SocketService', 'SweetAlert', '$state'];
            function ModalInstanceCtrl($scope, $modalInstance, $timeout, $cookies, SocketService, SweetAlert, $state) {

                $modalInstance.opened.then(function () {
                    var url = "api/hrmDashboard/iattendence"
                    SocketService.get(url).then(function (resp) {
                        $scope.punchstatus = resp.data.update_flag;
                     
                    });
                    $scope.mapOptionsModal = {
                        zoom: 14,
                    };
                });

                $scope.punchinlocation = function () {
                    var location = $cookies.getObject('location');
                    var params = {
                        location: location
                    }
                    var url = 'api/hrmDashboard/punchIn';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {
                            $scope.ok();
                            iattendence();
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
                    });
                }

                $scope.punch_outlocation = function () {
                    var location = $cookies.getObject('location');
                    var params = {
                        location: location
                    }
                    var url = 'api/hrmDashboard/punchOut';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {
                            $scope.ok();
                            iattendence();
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
                    });
                }

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.cancel = function () {
                    $modalInstance.dismiss('cancel');
                };

            }
        }

        function iattendence()
        {
            var url = "api/hrmDashboard/iattendence"
            SocketService.get(url).then(function (resp) {
                $scope.punchstatus = resp.data.update_flag;
                $scope.login_time_audit = resp.data.login_time_audit;
                
            });
        }

        function monthlyAttendence_chart() {
            //alert($scope.countLeave);
            vm.pieData = [
              {
                  value: $scope.countPresent,
                  color: Colors.byName('green'),
                  highlight: Colors.byName('info'),
                  label: 'Present'
              },
              {
                  value: $scope.countAbsent,
                  color: Colors.byName('danger'),
                  highlight: Colors.byName('info'),
                  label: 'Absent'
              },
              {
                  value: $scope.countLeave,
                  color: Colors.byName('yellow'),
                  highlight: Colors.byName('info'),
                  label: 'Leave'
              },
              {
                  value: $scope.countholiday,
                  color: Colors.byName('purple'),
                  highlight: Colors.byName('info'),
                  label: 'Holiday'
              },
              {
                  value: $scope.countWeekOff,
                  color: Colors.byName('success'),
                  highlight: Colors.byName('info'),
                  label: 'WeekOff'
              }
            ];

        };
 
        vm.pieOptions = {
            segmentShowStroke: true,
            segmentStrokeColor: '#fff',
            segmentStrokeWidth: 2,
            percentageInnerCutout: 0,
            animationSteps: 100,
            animationEasing: 'easeInOutBack',
            animateRotate: true,
            animateScale: false
        };

        // Punch In ....//

        $scope.punchinclick = function () {
            $scope.punchin = true;
        }
        $scope.closeclick = function () {
            $scope.close = false;
            $scope.punchin = false;
        }

        // Login Time Requisition ....//

        $scope.applylogintime = function () {
            lockUI();
            $scope.applylogindiv = true;
            $scope.applylogininfodiv = false;
            $scope.applyloginbutton = true;           
            unlockUI();
        }

        $scope.logincloseclick = function () {
            lockUI();
            $scope.applylogininfodiv = true;
            $scope.applylogindiv = false;
            $scope.applyloginbutton = false;
            document.getElementById("logintimeform1").reset();
            unlockUI();
        }

        $scope.loginApply = function () {
            var logintime;
            if ($scope.logintime == undefined) {
                var today = new Date();
                logintime = today.getHours() + ":" + today.getMinutes();
            }
            else {
               var time = $scope.logintime;
                logintime = time.getHours() + ":" + time.getMinutes();
            }
            var date = new Date();
            date.setFullYear($scope.attendencedate.getFullYear());
            date.setMonth($scope.attendencedate.getMonth());
            date.setDate($scope.attendencedate.getDate());
            var params = {
                loginreq_date: date,
                login_date: date,
                logintime: logintime,
                loginreq_reason: $scope.txtlogintimereason
            }
            console.log(params);
            var url = 'api/hrmDashboard/applyLoginReq';
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                document.getElementById("logintimeform1").reset();
                unlockUI();
                if (resp.data.status == true) {
                    var url = "api/hrmDashboard/loginSummary"
                    SocketService.get(url).then(function (resp) {
                        $scope.loginsummary_list = resp.data.loginsummary_list;
                    });
                    $scope.logincloseclick();                    
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
            });
        }

        $scope.deletelogin = function (attendancelogintmp_gid)
        {
            var params = {
                attendancelogintmp_gid: attendancelogintmp_gid
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
                    var url = "api/hrmDashboard/loginPendingDelete";
                    SocketService.getparams(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            var url = "api/hrmDashboard/loginSummary"
                            SocketService.get(url).then(function (resp) {
                                $scope.loginsummary_list = resp.data.loginsummary_list;
                            });
                        }
                        else {
                            Notify.alert('Error Occurred !', {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            
                        }
                    });
                    SweetAlert.swal('Deleted Successfully!');
                }

            });
        }

        $scope.deletelogout = function (attendancetmp_gid) {
            var params = {
                attendancetmp_gid: attendancetmp_gid
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
                    var url = "api/hrmDashboard/logoutPendingDelete";
                    SocketService.getparams(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            var url = "api/hrmDashboard/logoutSummary"
                            SocketService.get(url).then(function (resp) {
                                $scope.logoutsummary_list = resp.data.logoutsummary_list;
                            });
                        }
                        else {
                            Notify.alert('Error Occurred !', {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });

                        }
                    });
                    SweetAlert.swal('Deleted Successfully!');
                }

            });
        }

        $scope.deleteonduty = function (ondutytracker_gid) {
            var params = {
                ondutytracker_gid: ondutytracker_gid
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
                    var url = "api/hrmDashboard/ODPendingDelete";
                    SocketService.getparams(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            var url = "api/hrmDashboard/ondutySummary"
                            SocketService.get(url).then(function (resp) {
                                $scope.onduty_details = resp.data.onduty_details;
                            });
                        }
                        else {
                            Notify.alert('Error Occurred !', {
                                status: 'warning',
                                pos: 'top-right',
                                timeout: 3000
                            });
                        }
                    });
                    SweetAlert.swal('Deleted Successfully!');
                }

            });
        }

        $scope.deletecompoff = function (compensatoryoff_gid) {
            var params = {
                compensatoryoff_gid: compensatoryoff_gid
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
                    var url = "api/hrmDashboard/compoffPendingDelete";
                    SocketService.getparams(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            var url = "api/hrmDashboard/compOffSummary"
                            SocketService.get(url).then(function (resp) {
                                $scope.compoff_details = resp.data.compoffSummary_details;
                            });
                        }
                        else {
                            Notify.alert('Error Occurred !', {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });

                        }
                    });
                    SweetAlert.swal('Deleted Successfully!');
                }

            });
        }

        $scope.deletepermission = function (permissiondtl_gid) {
            var params = {
                permissiondtl_gid: permissiondtl_gid
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
                    var url = "api/hrmDashboard/permissionPendingDelete";
                    SocketService.getparams(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            var url = "api/hrmDashboard/permissionSummary"
                            SocketService.get(url).then(function (resp) {
                                $scope.permission_details = resp.data.permissionSummary_details;
                            });
                        }
                        else {
                            Notify.alert('Error Occurred !', {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });

                        }
                    });
                    SweetAlert.swal('Deleted Successfully!');
                }

            });
        }

        // Logout Time Requisition....//

        $scope.applylogouttime = function () {
            lockUI();
            $scope.applylogoutinfodiv = false;
            $scope.applylogoutdiv = true;
            $scope.applylogoutbutton = true;
            unlockUI();
        }

        $scope.logoutcloseclick = function () {
            $scope.applylogoutinfodiv = true;
            $scope.applylogoutdiv = false;
            document.getElementById("logoutform").reset();
            $scope.applylogoutbutton = false;
        }

        $scope.logoutApply = function () {
            var logouttime, selectedtime;
            if ($scope.logouttime == undefined) {
                var today = new Date();
                logouttime = today.getHours() + ":" + today.getMinutes();
            }
            else {
                selectedtime = $scope.logouttime;
                logouttime = selectedtime.getHours() + ":" + selectedtime.getMinutes();
            }
            var date = new Date();
            date.setFullYear($scope.logoutattendencedate.getFullYear());
            date.setMonth($scope.logoutattendencedate.getMonth());
            date.setDate($scope.logoutattendencedate.getDate());
            var params = {
                logoutattendence_date: date,
                logouttime: logouttime,
                logouttime_reason: $scope.txtlogouttimereason
            }
            var url = 'api/hrmDashboard/applyLogOutReq';
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                document.getElementById("logoutform").reset();
                unlockUI();
                if (resp.data.status == true) {
                    var url = "api/hrmDashboard/logoutSummary"
                    SocketService.get(url).then(function (resp) {
                        $scope.logoutsummary_list = resp.data.logoutsummary_list;
                    });
                    $scope.logoutcloseclick();                    
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
            });
        }


        // Apply OD .....//

        $scope.applyod = function () {

            $scope.applyoddiv = true;
            $scope.applyodinfodiv = false;
            $scope.applyodbutton = true;
        }

        $scope.odcloseclick = function () {
            $scope.applyodinfodiv = true;
            $scope.applyoddiv = false;
            $scope.applyodbutton = false;
            document.getElementById("odform1").reset();
        }

        $scope.odhalf = function (param) {
            if (param == 'show')
            {
                $scope.session = true;
            }
            else {
                $scope.session = false;
                $scope.radio_FNAN = '';
            }
          
        }


        $scope.onselectedchangeto = function (val) {

            var Difference_In_Days = (val.getTime() - $scope.workingFrom_date.getTime()) / (1000 * 3600 * 24);
            var lscompoff_days= Difference_In_Days + 1;

            if (lscompoff_days <= 0)
            {
                alert('To Date should be greater than From Date');
                $scope.workingTo_date = '';
                $scope.leave_days = '';
            }
            else {
                $scope.leave_days = Difference_In_Days + 1;
            }
        }

        $scope.odApply = function () {

           
            var odfrom, od_to, odFromHour, odFromMin, odToHour, odToMin, timenow;
            if (($scope.od_from == undefined) && ($scope.od_to == undefined)) {
                timenow = new Date();
                odFromHour = odToHour = timenow.getHours();
                odFromMin = odToMin = timenow.getMinutes();
            }
            else if ($scope.od_from == undefined) {
                od_to = $scope.od_to;
                odToHour = od_to.getHours();
                odToMin = od_to.getMinutes();

                timenow = new Date();
                odFromHour = timenow.getHours();
                odFromMin = timenow.getMinutes();
            }
            else if ($scope.od_to == undefined) {

                odfrom = $scope.od_from;
                odFromHour = odfrom.getHours();
                odFromMin = odfrom.getMinutes();

                timenow = new Date();
                odToHour = timenow.getHours();
                odToMin = timenow.getMinutes();
            }
            else {
                odfrom = $scope.od_from;
                od_to = $scope.od_to;
                odFromHour = odfrom.getHours();
                odFromMin = odfrom.getMinutes();
                odToHour = od_to.getHours();
                odToMin = od_to.getMinutes();
            }

            if (odToHour < odFromHour)
            {
               var test = alert('To Hours should be greater than From Hours');
               return;
               
            }
            var ODdate = new Date();
            ODdate.setFullYear($scope.oddate.getFullYear());
            ODdate.setMonth($scope.oddate.getMonth());
            ODdate.setDate($scope.oddate.getDate());

            var params = {
                od_date: ODdate,
                od_fromhr: odFromHour,
                od_frommin: odFromMin,
                od_tohr: odToHour,
                od_tomin: odToMin,
                onduty_period: $scope.radio_halffull,
                od_session: $scope.radio_FNAN,
                od_reason: $scope.txt_odreason
            }
            console.log(params);
            var url = 'api/hrmDashboard/applyonduty';
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                document.getElementById("odform1").reset();
                unlockUI();
                if (resp.data.status == true) {
                    var url = "api/hrmDashboard/ondutySummary"
                    SocketService.get(url).then(function (resp) {
                        $scope.onduty_details = resp.data.onduty_details;
                    });
                    $scope.odcloseclick();           
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
            });
        }


        // Apply CompOff.....//


        $scope.applycompoff = function () {
            $scope.applyCompoffdiv = true;
            $scope.applycompoffinfodiv = false;
            $scope.applycompoffbutton = true;
        }

        $scope.Compoffcloseclick = function () {            
            $scope.applycompoffinfodiv = true;
            $scope.applyCompoffdiv = false;
            $scope.applycompoffbutton = false;
            document.getElementById("compoffform1").reset();
            $scope.leave_days = "";
        }

        $scope.applycompoffclick = function () {

            var compoff_date = new Date();
            compoff_date.setFullYear($scope.compoff_date.getFullYear());
            compoff_date.setMonth($scope.compoff_date.getMonth());
            compoff_date.setDate($scope.compoff_date.getDate());
  
            var actualFromdate = new Date();
            actualFromdate.setFullYear($scope.actualworking_date.getFullYear());
            actualFromdate.setMonth($scope.actualworking_date.getMonth());
            actualFromdate.setDate($scope.actualworking_date.getDate());

            var params = {
                actualworking_date: actualFromdate,
                compoff_date: compoff_date,
                compoff_reason: $scope.txtcompoffreason,
              
            }
 
            var url = 'api/hrmDashboard/applyCompoffReq';
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                document.getElementById("compoffform1").reset();
                $scope.leave_days = "";
                unlockUI();
                if (resp.data.status == true) {
                    var url = "api/hrmDashboard/compOffSummary"
                    SocketService.get(url).then(function (resp) {
                        $scope.compoff_details = resp.data.compoffSummary_details;
                    });
                    $scope.Compoffcloseclick();                   
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
            });
        }

        // Apply Permission....//

        $scope.applypermission = function () {
            $scope.applypermissiondiv = true;
            $scope.applypermissioninfodiv = false;
            $scope.applypermissionbutton = true;
        }

        $scope.permissioncloseclick = function () {
            $scope.applypermissiondiv = false;
            $scope.applypermissioninfodiv = true;
            $scope.applypermissionbutton = false;
            document.getElementById("permissionform1").reset();
        }

        $scope.applypermission_Click = function () {

            var permission_From, permissionTo, permissionFromHour, permissionFromMin, permissionToHour, permissionToMin, timenow;

            if (($scope.permissionFrom == undefined) && ($scope.permissionTo == undefined)) {
                timenow = new Date();
                permissionFromHour = permissionToHour = timenow.getHours();
                permissionFromMin = permissionToMin = timenow.getMinutes();
            }
            else if ($scope.permissionFrom == undefined) {
                permissionTo = $scope.permissionTo;
                permissionToHour = permissionTo.getHours();
                permissionToMin = permissionTo.getMinutes();

                timenow = new Date();
                permissionFromHour = timenow.getHours();
                permissionFromMin = timenow.getMinutes();
            }
            else if ($scope.permissionTo == undefined) {

                permission_From = $scope.permissionFrom;
                permissionFromHour = permission_From.getHours();
                permissionFromMin = permission_From.getMinutes();

                timenow = new Date();
                permissionToHour = timenow.getHours();
                permissionToMin = timenow.getMinutes();
            }
            else {
                permission_From = $scope.permissionFrom;
                permissionTo = $scope.permissionTo;
                permissionFromHour = permission_From.getHours();
                permissionFromMin = permission_From.getMinutes();
                permissionToHour = permissionTo.getHours();
                permissionToMin = permissionTo.getMinutes();
            }

            if (permissionToHour < permissionFromHour) {
                var test = alert('To Hours should be greater than From Hours');
                return;

            }

            var permission_date = new Date();
            permission_date.setFullYear($scope.permissiondate.getFullYear());
            permission_date.setMonth($scope.permissiondate.getMonth());
            permission_date.setDate($scope.permissiondate.getDate());

            var params = {
                permission_date:permission_date,
                permission_fromhr: permissionFromHour,
                permission_frommin: permissionFromMin,
                permission_tohr: permissionToHour,
                permission_tomin: permissionToMin,
                permission_reason: $scope.txtpermissionreason
            }

            var url = 'api/hrmDashboard/applyPermission';
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                document.getElementById("permissionform1").reset();
                unlockUI();
                if (resp.data.status == true) {
                    var url = "api/hrmDashboard/permissionSummary"
                    SocketService.get(url).then(function (resp) {
                        $scope.permission_details = resp.data.permissionSummary_details;
                    });
                    $scope.permissioncloseclick();                    
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
            });

        }

    }
})();
