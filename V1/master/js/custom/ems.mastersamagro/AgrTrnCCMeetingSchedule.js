(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AgrTrnCCMeetingScheduleController', AgrTrnCCMeetingScheduleController);

    AgrTrnCCMeetingScheduleController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', '$sce', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function AgrTrnCCMeetingScheduleController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, $sce, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AgrTrnCCMeetingScheduleController';
        $scope.application_gid = $location.search().application_gid;
        var application_gid = $scope.application_gid;

        $scope.lstab = $location.search().lstab;
        var lstab = $scope.lstab;
      
        activate();
        function activate() {
            $scope.trustAsHtml = function (string) { return $sce.trustAsHtml(string); };
            vm.open = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();
                vm.opened = true;
            };
            vm.close = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();
                vm.closed = true;
            };
            vm.formats = ['dd-MM-yyyy'];
            vm.format = vm.formats[0];
            vm.dateOptions = {
                formatYear: 'yy',
                startingDay: 1
            }; 
            //var today = new Date();
            //console.log($scope.cdate);
            //var date = today.getDate();
            //var month = today.getMonth();
            //var year = today.getFullYear();

            //var current_date = date + '-' + month + '-' + year;
            //return moment(current_date).format('DD-MM-YYYY');
            //$scope.txtccmeeting_no = current_date;

            //console.log(current_date);
            var today = new Date();
            var yyyy = today.getFullYear();
            var mm = today.getMonth() + 1; // Months start at 0!
            var dd = today.getDate();

            if (dd < 10) dd = '0' + dd;
            if (mm < 10) mm = '0' + mm;

            var formattedToday = dd + '-' + mm + '-' + yyyy;

            //              

            //   var today = new Date();
            //  var date =  0+today.getDate()+'-'+(today.getMonth()+1)+'-'+today.getFullYear();
            $scope.txtccmeeting_no = formattedToday;
           
           var application_gid = $location.search().application_gid;
            var params = {
                application_gid: application_gid
            }
            lockUI();
            var url = 'api/AgrTrnCC/GetApplicantInfo';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.lblapplication_no = resp.data.application_no;
                $scope.lblcustomer_name = resp.data.customer_name;
                $scope.lblsubmitted_date = resp.data.submitted_date;
                $scope.lblsubmitted_by = resp.data.submitted_by;
                $scope.lblccsubmitted_date = resp.data.ccsubmitted_date;
                $scope.lblccsubmitted_by = resp.data.ccsubmitted_by;
                $scope.lblvertical_name = resp.data.vertical_name;
            });
            if (lstab == 'Pending')
            {
                var url = 'api/AgrMstCCMember/Getccgroup';
                SocketService.get(url).then(function (resp) {
                    $scope.ccgroup_list = resp.data.ccgroup_list;
                });
                var url = 'api/employee/employee';
                SocketService.get(url).then(function (resp) {
                    $scope.employee_list = resp.data.employee_list;
                });
            }
            else {
               
                lockUI();
                var url = 'api/AgrTrnCC/GetScheduleMeeting';
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.lblccmeeting_no = resp.data.ccmeeting_no;
                    $scope.lblccmeeting_title = resp.data.ccmeeting_title;
                    $scope.lblmeeting_time = resp.data.ccmeeting_time;
                  //  $scope.lblend_time = resp.data.end_time;
                    $scope.lblccmeeting_mode = resp.data.ccmeeting_mode;
                    $scope.lblccgroup_name = resp.data.ccgroup_name;
                    $scope.lbldescription = resp.data.description;
                    $scope.lblccmeeting_date = resp.data.ccmeeting_date;
                    $scope.lblccmember_list = resp.data.ccmember_list;
                    $scope.lblotheruser_name = resp.data.otheruser_name;
                    $scope.lblccadmin_name = resp.data.ccadmin_name;
                    $scope.ccschedulemeeting_gid = resp.data.ccschedulemeeting_gid
                });
                var url = 'api/AgrTrnCC/GetScheduleMeetingLog';
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.lblccschedule_list = resp.data.ccschedule_list;
                });
            }

            var params = {
                application_gid: application_gid
            };

            var url = 'api/AgrTrnApplicationApproval/Getapplicationdetails';
            SocketService.getparams(url, params).then(function (resp) {
              
                $scope.shortclosing_reason = resp.data.shortclosing_reason;
                $scope.expired_flag = resp.data.expired_flag;
            });
          
        }
        $scope.onchangeccgroup=function()
        {
            var ccgroup_list;
            var params = {
                ccgroup_list: $scope.cboccgroup_name,
            }
            var url = 'api/AgrTrnCC/Getcc2member';
            SocketService.post(url, params).then(function (resp) {
                $scope.ccmember_list = resp.data.ccmember_list;
            });
            var url = 'api/AgrTrnCC/OtherEmployee';
            SocketService.post(url, params).then(function (resp) {
                $scope.employee_list = resp.data.otheremployee_list;
            });
            $scope.memberlist = true;

            if ($scope.cboccgroup_name == undefined || $scope.cboccgroup_name == null || $scope.cboccgroup_name == '') {
                $scope.txtccmeeting_title = '';
            }
            else {
                //$scope.txtccmeeting_title = $scope.cboccgroup_name.join(",");
                $scope.txtccmeeting_title = $scope.cboccgroup_name.map(function (el) { return el.ccgroup_name }).join(",");
                /* $scope.txtccmeeting_title = $scope.cboccgroup_name .join(",");*/
            }
        }
        $scope.onchangetime=function(){
            var g1 = new Date($scope.txtstart_time);
            var g2 = new Date($scope.txtend_time);
            console.log(g1)
            console.log(g2)
            if (g1.getTime() == g2.getTime())
                Notify.alert("Both are equal", 'warning');
            else if (g1.getTime() > g2.getTime())
                Notify.alert("Start Time is greater than End Time", 'warning');
         
        }
        
      
        $scope.schedule = function () {
            if ((($scope.txtstart_time != undefined) || ($scope.txtend_time != undefined)) && ($scope.cboccadmin_name == undefined)) {
                Notify.alert('Select CC Admin', 'warning');
            }
            else {
                if (($scope.txtstart_time == undefined)|| ($scope.txtend_time == undefined))
                {
                    lockUI();
                    var params = {
                        application_gid: application_gid,
                        ccmeeting_title: $scope.txtccmeeting_title,
                        ccmeeting_no: $scope.txtccmeeting_no,
                        ccmeeting_date: $scope.txtccmeeting_date,
                        start_time: $scope.txtstart_time,
                        end_time: $scope.txtend_time,
                        ccmeeting_mode: $scope.rdbccmeeting_mode,
                        ccschedule_list: $scope.cboccgroup_name,
                        otheremployee_list: $scope.cboother_user,
                        description: $scope.txtdescription,
                        non_users: $scope.txtnon_users,
                        ccadmin_list: $scope.cboccadmin_name,
                    }
                    var url = 'api/AgrTrnCC/PostScheduleMeeting';

                    SocketService.post(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            unlockUI();

                            Notify.alert(resp.data.message, 'success')
                            $location.url('app/AgrTrnCreditCommitteeSummary');

                        }
                        else {
                            unlockUI();
                            Notify.alert(resp.data.message)
                        }
                        activate();
                    });
                }
                else {

                
                var g1 = new Date($scope.txtstart_time);
                var g2 = new Date($scope.txtend_time);
                if (g1.getTime() == g2.getTime())
                {
                    Notify.alert("Both are equal", 'warning');
                }
                else if (g1.getTime() > g2.getTime())
                {
                    Notify.alert("Start Time is greater than End Time", 'warning');
                }
                else if (g1.getTime() < g2.getTime())
                {
                    lockUI();
                    var params = {
                        application_gid: application_gid,
                        ccmeeting_title: $scope.txtccmeeting_title,
                        ccmeeting_no: $scope.txtccmeeting_no,
                        ccmeeting_date: $scope.txtccmeeting_date,
                        start_time: $scope.txtstart_time,
                        end_time: $scope.txtend_time,
                        ccmeeting_mode: $scope.rdbccmeeting_mode,
                        ccschedule_list: $scope.cboccgroup_name,
                        otheremployee_list: $scope.cboother_user,
                        description: $scope.txtdescription,
                        non_users: $scope.txtnon_users,
                        ccadmin_list: $scope.cboccadmin_name,
                    }
                    var url = 'api/AgrTrnCC/PostScheduleMeeting';

                    SocketService.post(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            unlockUI();

                            Notify.alert(resp.data.message, 'success')
                            $location.url('app/AgrTrnCreditCommitteeSummary');

                        }
                        else {
                            unlockUI();
                            Notify.alert(resp.data.message)
                        }
                        activate();
                    });
                }
                }
               
          
        }
        }
       
        $scope.back = function () {
            $location.url('app/AgrTrnCreditCommitteeSummary');
        }
        $scope.schedulesback = function () {
            $location.url('app/AgrTrnCCscheduledSummary');
        }
        $scope.reschedule = function (application_gid) {
            $location.url('app/AgrTrnCCMeetingReschedule?application_gid=' + application_gid + '&ccschedulemeeting_gid=' + $scope.ccschedulemeeting_gid);
        }

        $scope.cancel = function (application_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/cancelmeeting.html',
                controller: ModalInstanceCtrl,
                size: 'md',
                backdrop: 'static',
                keyboard: false,
            });

            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                
                var params = {
                    application_gid: application_gid,
                };
              
                var url = "api/AgrTrnCC/GetScheduleMeeting";
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.txteditccmeeting_title = resp.data.ccmeeting_title;
                    $scope.txteditccmeeting_no = resp.data.ccmeeting_no;
                    $scope.cboeditccgroup_name = resp.data.ccgroupname_gid;
                    if (resp.data.meeting_date != '0001-01-01T00:00:00') {
                        $scope.txteditccmeeting_date = new Date(resp.data.meeting_date);
                    }
                    $scope.txteditstart_time = new Date(resp.data.Tstart_time);
                    $scope.txteditend_time = new Date(resp.data.Tend_time);
                    $scope.rdbeditccmeeting_mode = resp.data.ccmeeting_mode;
                    $scope.txteditdescription = resp.data.description;
                });
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.submit = function () {

                    lockUI();
                   
                    var params = {
                        application_gid: application_gid,
                        cancel_remarks: $scope.txtcancel_remarks,
                    }
                    var url = 'api/AgrTrnCC/CancelMeeting';
                    SocketService.post(url, params).then(function (resp) {


                        if (resp.data.status == true) {

                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            $modalInstance.close('closed');

                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'Warning',
                                pos: 'top-center',
                                timeout: 3000
                            });

                        }
                        activate();
                    });


                }
            }
        }

        $scope.pastdatecheck = function (val) {
            var params = {
                date: val.toDateString()
            }
            var url = 'api/AgrTrnCC/PastDateCheck';
            SocketService.getparams(url, params).then(function (resp) {
                if (resp.data.status == false) {
                    Notify.alert(resp.data.message, 'warning')
                    $scope.txtccmeeting_date = '';
                }
            });
        }

    }
})();
