(function () {
    'use strict';

    angular
        .module('angle')
        .controller('applyLeavecontroller', applyLeavecontroller);

    applyLeavecontroller.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams'];

    function applyLeavecontroller($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'applyLeavecontroller';

        activate();

        function activate() {

            var url = "api/applyLeave/leavetype"
            SocketService.get(url).then(function (resp) {
                $scope.leavetype_list = resp.data.leavetype_list;
                
            });
        }

        $scope.applyleave = function (leavetype_gid) {
            var params = {
                leavetype_gid: leavetype_gid
            }
            
            var url = 'api/applyLeave/leavetype';

        SocketService.post(url, params).then(function (resp) {
            leavetype_gid = leavetype_gid;
            console.log(leavetype_gid);
            if (resp.data.status == true) {
                $modalInstance.close('closed');
                Notify.alert('Leave Applied Successfully!!', 'success')
                activate();

            }
            else {
                Notify.alert('Error Occurred!', 'warning')
                activate();
            }
        });
   
            
            var modalInstance = $modal.open({
                templateUrl: '/myModalContent.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.fullclick = function () {
                    $scope.leavefull = true;
                    $scope.leavehalf = false;
                   
                }
                $scope.halfclick = function () {
                    $scope.leavefull = false;
                    $scope.leavehalf = true;
                }
                $scope.fullleavesubmit = function () {
                    var params = {
                        leave_from: $scope.fromdate,
                        leave_to: $scope.todate,
                        leave_reason: $scope.leave_reason,
                        leavetype_name: $scope.leavetypeName
                        
                    }
                    var url = 'api/applyLeave/applyleavesummary';

                    SocketService.post(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            $modalInstance.close('closed');
                            Notify.alert('Leave Applied Successfully!!', 'success')
                            activate();

                        }
                        else {
                            Notify.alert('Error Occurred!', 'warning')
                            activate();
                        }
                       
                        
                    });
                    $state.go('app.applyLeave');
                }
               
               
            }
        }

    }
})();

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('approveleavecontroller', approveleavecontroller);

    approveleavecontroller.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', 'Colors', 'ChartData', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams', 'SweetAlert'];

    function approveleavecontroller($rootScope, $scope, $state, AuthenticationService, Colors, ChartData, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams, SweetAlert) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'approveleavecontroller';


        activate();

        function activate() {
            var url = "api/approveLeave/getapproval_count";
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.count_approval = resp.data.count_approval;
                $scope.count_rejected = resp.data.count_rejected;
                $scope.count_history = resp.data.count_history;
                $scope.count_approvalpending = resp.data.count_approvalpending;

                $scope.pending_leave = resp.data.pending_leave;
                $scope.pending_login = resp.data.pending_login;
                $scope.pending_logout = resp.data.pending_logout;
                $scope.pending_onduty = resp.data.pending_onduty;
                $scope.pending_compoff = resp.data.pending_compoff;
                $scope.pending_permission = resp.data.pending_permission;

                $scope.approved_leave = resp.data.approved_leave;
                $scope.approved_login = resp.data.approved_login;
                $scope.approved_logout = resp.data.approved_logout;
                $scope.approved_onduty = resp.data.approved_onduty;
                $scope.approved_compoff = resp.data.approved_compoff;
                $scope.approved_permission = resp.data.approved_permission;

                $scope.rejected_leave = resp.data.rejected_leave;
                $scope.rejected_login = resp.data.rejected_login;
                $scope.rejected_logout = resp.data.rejected_logout;
                $scope.rejected_onduty = resp.data.rejected_onduty;
                $scope.rejected_compoff = resp.data.rejected_compoff;
                $scope.rejected_permission = resp.data.rejected_permission;

            });

            // Leave Approval Details...//

            var url = "api/approveLeave/getleaveapprovependingdetails";
            SocketService.get(url).then(function (resp) {
                $scope.leavepending_list = resp.data.leave_list;
                console.log(resp.data.leave_list);
            });

            // Login Approval Details...//

            var url = "api/approveLeave/getloginsummarydetails";
            SocketService.get(url).then(function (resp) {
                $scope.loginapproval_pendinglist = resp.data.loginpending_list;

            });

            //// Logout Approval Details...//

            var url = "api/approveLeave/getlogoutsummarydetails";
            SocketService.get(url).then(function (resp) {
                $scope.logoutapproval_pendinglist = resp.data.logoutpending_list;

            });

            // OD Approval Details...// 

            var url = "api/approveLeave/getODsummarydetails";
            SocketService.get(url).then(function (resp) {
                $scope.ODapproval_pendinglist = resp.data.ODpending_list;

            });

            // Compoff Approval Details...//

            var url = "api/approveLeave/getCompoffsummarydetails";
            SocketService.get(url).then(function (resp) {
                $scope.compoffpending_list = resp.data.compoffpending_list;

            });

            // Permission Approval Details...//

            var url = "api/approveLeave/getPermissionsummarydetails";
            SocketService.get(url).then(function (resp) {
                $scope.permissionapproval_pendinglist = resp.data.permissionpending_list;

            });

            //.....login approved  details......//

            var url = "api/approveLeave/getloginleaveapprovedetails";
            SocketService.get(url).then(function (resp) {
                $scope.loginsummary_list = resp.data.login_list;
            });

            //.....login approved  details......//

            var url = "api/approveLeave/getlogoutleaveapprovedetails";
            SocketService.get(url).then(function (resp) {
                $scope.logoutsummary_list = resp.data.logout_list;

            });


            //.....od approved  details......//

            var url = "api/approveLeave/getodleaveapprovedetails";
            SocketService.get(url).then(function (resp) {
                $scope.onduty_details = resp.data.od_list;
            });

            //.....compoff approved  details......//

            var url = "api/approveLeave/getcompoffleaveapprovedetails";
            SocketService.get(url).then(function (resp) {
                $scope.compoff_details = resp.data.compoffdtl_list;
            });

            //.....Permission approved  details......//

            var url = "api/approveLeave/getpermissionleaveapprovedetails";
            SocketService.get(url).then(function (resp) {
                $scope.permission_details = resp.data.permission_list;
            });


            //.....login rejected  details......//

            var url = "api/approveLeave/getloginleaverejectdetails";
            SocketService.get(url).then(function (resp) {
                $scope.loginsummaryrej_list = resp.data.login_list;
            });

            //.....logout rejected  details......//

            var url = "api/approveLeave/getlogoutleaverejectdetails";
            SocketService.get(url).then(function (resp) {
                $scope.logoutsummaryrej_list = resp.data.logout_list;
            });

            //.....od rejected  details......//

            var url = "api/approveLeave/getodleaverejectdetails";
            SocketService.get(url).then(function (resp) {
                $scope.ondutyrej_details = resp.data.od_list;
            });

            //.....compoff rejected  details......//

            var url = "api/approveLeave/getcompoffleaverejectdetails";
            SocketService.get(url).then(function (resp) {
                $scope.compoffrej_details = resp.data.compoffdtl_list;
            });

            //.....Permission rejected  details......//

            var url = "api/approveLeave/getpermissionleaverejectdetails";
            SocketService.get(url).then(function (resp) {
                $scope.permissionrej_details = resp.data.permission_list;
            });


            // Approved Summary ...//

            //$scope.getleaveapprovedetails = function () {
            var url = "api/approveLeave/getleaveapprovedetails";
            //lockUI();
            SocketService.get(url).then(function (resp) {
                //unlockUI();
                $scope.leave_list = resp.data.leave_list;
                //console.log(resp.data.leave_list);
                //$scope.action = false;
            });
            //};

            // Rejected Summary ...//


            //$scope.getleaverejectdetails = function () {
            var url = "api/approveLeave/getleaverejectdetails";
            //lockUI();
            SocketService.get(url).then(function (resp) {
                //unlockUI();
                $scope.leavereject_list = resp.data.leave_list;
                //console.log(resp.data.leave_list);
                //$scope.action = false;
            });
            unlockUI();
        }
        //};

        //};

        // Approve Leave Click ......//

        $scope.approveleave = function (leave_gid) {
            var params = {
                leave_gid: leave_gid
            }
            var url = 'api/approveLeave/approveleaveclick';
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {
                    var url = "api/approveLeave/getleaveapprovependingdetails";
                    SocketService.get(url).then(function (resp) {
                        $scope.leavepending_list = resp.data.leave_list;
                    });

                    var url = "api/approveLeave/getapproval_count";
                    SocketService.get(url).then(function (resp) {
                        $scope.count_approvalpending = resp.data.count_approvalpending;
                        $scope.count_history = resp.data.count_history;
                        $scope.pending_leave = resp.data.pending_leave;
                    });
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    //SweetAlert.swal('leave Approved Successfully...!');

                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'warning',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    //SweetAlert.swal('Error Occured...!');
                }

            });

        }

        // Reject Leave Click ......//

        $scope.rejectleave = function (leave_gid) {
            var params = {
                leave_gid: leave_gid
            }
            console.log(params);
            var url = 'api/approveLeave/rejectleaveclick';
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {
                    var url = "api/approveLeave/getleaveapprovependingdetails";
                    SocketService.get(url).then(function (resp) {
                        $scope.leavepending_list = resp.data.leave_list;
                    });

                    var url = "api/approveLeave/getapproval_count";
                    SocketService.get(url).then(function (resp) {
                        $scope.count_rejected = resp.data.count_rejected;
                        $scope.count_approvalpending = resp.data.count_approvalpending;
                        $scope.count_history = resp.data.count_history;
                    });
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    //SweetAlert.swal('Leave rejected Successfully...!');
                    activate();
                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'warning',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    //SweetAlert.swal('Error Occured...!');
                }
                activate();

            });
        }

        // Document Leave Click .....//

        $scope.documentleave = function (leave_gid) {
            var params = {
                leave_gid: leave_gid
            }
            console.log(params);
            var url = 'api/approveLeave/getleavedocument';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {
                   
                    var modalInstance = $modal.open({
                        templateUrl: '/myModalContent.html',
                        controller: ModalInstanceCtrl,
                        size: 'md'
                    });
                    ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
                    function ModalInstanceCtrl($scope, $modalInstance) {
                        $scope.filename_list = resp.data.filename_list;
                        $scope.ok = function () {
                            $modalInstance.close('closed');
                        };

                        // Document Download .....//

                        $scope.document_downloadclick = function (val1, val2) {

                            var phyPath = val1;
                            var relPath = phyPath.split("EMS");
                            var relpath1 = relPath[1].replace("\\", "/");
                            var hosts = window.location.host;
                            var prefix = "http://"
                            var str = prefix.concat(hosts, relpath1);
                            var link = document.createElement("a");
                            var name = val2.split(".")
                            link.download = name[0];
                            var uri = str;
                            link.href = uri;
                            link.click();
                        }
                    }
                }
                else {
                    SweetAlert.swal('No Documents...!');
                }
            });

        }



        // Approve Login Click ......//

        $scope.approvelogin = function (attendancelogintmp_gid, apply_employeegid, loginattendence_date) {
            var params = {
                attendancelogintmp_gid: attendancelogintmp_gid,
                loginattendence_date: loginattendence_date,
                apply_employeegid: apply_employeegid
            }
            var url = "api/approveLeave/approvelogin";
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {
                    var url = "api/approveLeave/getloginsummarydetails";
                    SocketService.get(url).then(function (resp) {
                        $scope.loginapproval_pendinglist = resp.data.loginpending_list;
                    });
                    var url = "api/approveLeave/getapproval_count";
                    SocketService.get(url).then(function (resp) {
                        $scope.count_approvalpending = resp.data.count_approvalpending;
                        $scope.pending_login = resp.data.pending_login;
                        $scope.count_history = resp.data.count_history;
                    });
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    //SweetAlert.swal('Login Approved Successfully...!');
                    activate();
                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'warning',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    //SweetAlert.swal('Error Occured...!');
                }
                activate();
            });
        }

        // Reject Login Click ......//

        $scope.rejectlogin = function (attendancelogintmp_gid) {
            var params = {
                attendancelogintmp_gid: attendancelogintmp_gid
            }
            var url = "api/approveLeave/rejectlogin";
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {
                    var url = "api/approveLeave/getloginsummarydetails";
                    SocketService.get(url).then(function (resp) {
                        $scope.loginapproval_pendinglist = resp.data.loginpending_list;
                    });
                    var url = "api/approveLeave/getapproval_count";
                    SocketService.get(url).then(function (resp) {
                        $scope.count_rejected = resp.data.count_rejected;
                        $scope.count_approvalpending = resp.data.count_approvalpending;
                        $scope.count_history = resp.data.count_history;
                    });
                    //SweetAlert.swal('Login Rejected Successfully...!');
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
                    //SweetAlert.swal('Error Occured...!');
                }
                activate();
            });
        }

        // Approve Logout Click ......//

        $scope.approvelogout = function (attendancelogouttmp_gid, logoutattendence_date, apply_employeegid) {

            var params = {
                attendancelogouttmp_gid: attendancelogouttmp_gid,
                logoutattendence_date: logoutattendence_date,
                apply_employeegid: apply_employeegid
            }
            var url = "api/approveLeave/approvelogout";
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {
                    var url = "api/approveLeave/getlogoutsummarydetails";
                    SocketService.get(url).then(function (resp) {
                        $scope.logoutapproval_pendinglist = resp.data.logoutpending_list;
                    });
                    var url = "api/approveLeave/getapproval_count";
                    SocketService.get(url).then(function (resp) {
                        $scope.count_approvalpending = resp.data.count_approvalpending;
                        $scope.count_history = resp.data.count_history;
                        $scope.pending_logout = resp.data.pending_logout;
                    });
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    //SweetAlert.swal('Logout Approved Successfully...!');
                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'warning',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    //SweetAlert.swal('Error Occured...!');
                }
                activate();
            });
        }

        // Reject Logout Click ......//

        $scope.rejectlogout = function (attendancelogouttmp_gid) {
            var params = {
                attendancelogouttmp_gid: attendancelogouttmp_gid
            }
            var url = "api/approveLeave/rejectlogout";
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {
                    var url = "api/approveLeave/getlogoutsummarydetails";
                    SocketService.get(url).then(function (resp) {
                        $scope.logoutapproval_pendinglist = resp.data.logoutpending_list;
                    });
                    var url = "api/approveLeave/getapproval_count";
                    SocketService.get(url).then(function (resp) {
                        $scope.count_history = resp.data.count_history;
                        $scope.count_rejected = resp.data.count_rejected;
                        $scope.count_approvalpending = resp.data.count_approvalpending;
                    });
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    //SweetAlert.swal('Logout Rejected Successfully...!');
                }
                else {
                    //SweetAlert.swal('Error Occured...!');
                    Notify.alert(resp.data.message, {
                        status: 'warning',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
                activate();
            });
        }


        // Approve OD Click ......//

        $scope.approveOD = function (ondutytracker_gid) {
            var params = {
                ondutytracker_gid: ondutytracker_gid
            }
            var url = "api/approveLeave/approveOD";
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {
                    var url = "api/approveLeave/getODsummarydetails";
                    SocketService.get(url).then(function (resp) {
                        $scope.ODapproval_pendinglist = resp.data.ODpending_list;
                    });
                    var url = "api/approveLeave/getapproval_count";
                    SocketService.get(url).then(function (resp) {
                        $scope.count_history = resp.data.count_history;
                        $scope.count_approvalpending = resp.data.count_approvalpending;
                        $scope.pending_onduty = resp.data.pending_onduty;
                    });
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    //SweetAlert.swal('OD Approved Successfully...!');
                }

                else {
                    //SweetAlert.swal('Error Occured...!');
                    Notify.alert(resp.data.message, {
                        status: 'warning',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
                activate();
            });
        }

        // Reject OD Click ......//

        $scope.rejectOD = function (ondutytracker_gid) {
            var params = {
                ondutytracker_gid: ondutytracker_gid
            }
            var url = "api/approveLeave/rejectOD";
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {
                    var url = "api/approveLeave/getODsummarydetails";
                    SocketService.get(url).then(function (resp) {
                        $scope.ODapproval_pendinglist = resp.data.ODpending_list;
                    });
                    var url = "api/approveLeave/getapproval_count";
                    SocketService.get(url).then(function (resp) {
                        $scope.count_history = resp.data.count_history;
                        $scope.count_rejected = resp.data.count_rejected;
                        $scope.count_approvalpending = resp.data.count_approvalpending;
                    });
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    //SweetAlert.swal('OD Rejected Successfully...!');
                }
                else {
                    //SweetAlert.swal('Error Occured...!');
                    Notify.alert(resp.data.message, {
                        status: 'warning',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
                activate();
            });
        }


        // Approve Compoff Click ......//

        $scope.approvecompoff = function (compensatoryoff_gid) {
            var params = {
                compensatoryoff_gid: compensatoryoff_gid
            }

            var url = "api/approveLeave/approvecompoff";
            lockUI();
            SocketService.post(url, params).then(function (resp) {

                if (resp.data.status == true) {

                    var url = "api/approveLeave/getCompoffsummarydetails";
                    SocketService.get(url).then(function (resp) {
                        $scope.compoffpending_list = resp.data.compoffpending_list;
                    });

                    var url = "api/approveLeave/getapproval_count";
                    SocketService.get(url).then(function (resp) {
                        $scope.count_history = resp.data.count_history;
                        $scope.count_approvalpending = resp.data.count_approvalpending;
                        $scope.pending_compoff = resp.data.pending_compoff;
                    });
                    //unlockUI();
                    //SweetAlert.swal('Compoff Approved Successfully...!');
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
                else {
                    //SweetAlert.swal('Error Occured...!');
                    Notify.alert(resp.data.message, {
                        status: 'warning',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
                activate();
            });
        }

        // Reject Compoff Click ......//

        $scope.rejectcompoff = function () {
            var params = {

            }
            var url = "api/approveLeave/rejectcompoff";
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {
                    var url = "api/approveLeave/getCompoffsummarydetails";
                    SocketService.get(url).then(function (resp) {
                        $scope.compoffpending_list = resp.data.compoffpending_list;

                    });
                    var url = "api/approveLeave/getapproval_count";
                    SocketService.get(url).then(function (resp) {
                        $scope.count_rejected = resp.data.count_rejected;
                        $scope.count_history = resp.data.count_history;
                        $scope.count_approvalpending = resp.data.count_approvalpending;
                    });
                    //SweetAlert.swal('Compoff Rejected Successfully...!');
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
                else {
                    //SweetAlert.swal('Error Occured...!');
                    Notify.alert(resp.data.message, {
                        status: 'warning',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
                activate();
            });
        }

        // Approve Permission //

        $scope.approvepermission = function (permissiondtl_gid) {
            $scope.permissiondtl_gid = permissiondtl_gid;
            var params = {
                permissiondtl_gid: $scope.permissiondtl_gid
            }
            console.log(params);
            //var params = {
            //    permission_gid : $scope.permission_gid
            //};
            var url = 'api/approveLeave/approvepermission';
            SocketService.getparams(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    var url = "api/approveLeave/getPermissionsummarydetails";
                    SocketService.get(url).then(function (resp) {
                        $scope.permissionapproval_pendinglist = resp.data.permissionapproval_pendinglist;

                    });
                    var url = "api/approveLeave/getapproval_count";
                    SocketService.get(url).then(function (resp) {
                        $scope.count_rejected = resp.data.count_rejected;
                        $scope.count_history = resp.data.count_history;
                        $scope.count_approvalpending = resp.data.count_approvalpending;
                    });
                    //SweetAlert.swal('Permission Approved Successfully...!');
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
                else {
                    //SweetAlert.swal('Error Occured...!');
                    Notify.alert(resp.data.message, {
                        status: 'warning',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
                activate();
            });

        }

        // Reject Permission //

        $scope.rejectpermission = function (permissiondtl_gid) {
            $scope.permissiondtl_gid = permissiondtl_gid;
            var params = {
                permissiondtl_gid: $scope.permissiondtl_gid
            }

            var url = 'api/approveLeave/rejectpermission';
            SocketService.getparams(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    var url = "api/approveLeave/getPermissionsummarydetails";
                    SocketService.get(url).then(function (resp) {
                        $scope.permissionapproval_pendinglist = resp.data.permissionapproval_pendinglist;

                    });
                    var url = "api/approveLeave/getapproval_count";
                    SocketService.get(url).then(function (resp) {
                        $scope.count_rejected = resp.data.count_rejected;
                        $scope.count_history = resp.data.count_history;
                        $scope.count_approvalpending = resp.data.count_approvalpending;
                    });
                    //SweetAlert.swal('Permission Rejected Successfully...!');
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
                else {
                    //SweetAlert.swal('Error Occured...!');
                    Notify.alert(resp.data.message, {
                        status: 'warning',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
                activate();
            });

        }

        // Reject Compoff //

        $scope.rejectcompoff = function (compensatoryoff_gid) {
            $scope.compensatoryoff_gid = compensatoryoff_gid;
            var params = {
                compensatoryoff_gid: $scope.compensatoryoff_gid
            }

            var url = 'api/approveLeave/rejectcompoff';
            SocketService.getparams(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    var url = "api/approveLeave/getCompoffsummarydetails";
                    SocketService.get(url).then(function (resp) {
                        $scope.compoffpending_list = resp.data.compoffpending_list;

                    });
                    var url = "api/approveLeave/getapproval_count";
                    SocketService.get(url).then(function (resp) {
                        $scope.count_rejected = resp.data.count_rejected;
                        $scope.count_history = resp.data.count_history;
                        $scope.count_approvalpending = resp.data.count_approvalpending;
                    });
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
    }
})();

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('companypoliciescontroller', companypoliciescontroller);

    companypoliciescontroller.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams','$sce'];

    function companypoliciescontroller($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams,$sce) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'companypoliciescontroller';

        activate();

        function activate() {
            var url = "api/companyPolicy/policy";
            lockUI();
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.policy_list = resp.data.policy_list;
            });
        }
        $scope.trustAsHtml = function (string) { return $sce.trustAsHtml(string); };
    }
})();
(function () {
    'use strict';

    angular
        .module('angle')
        .controller('geo', geo);

    geo.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', '$cookies'];
    function geo($rootScope, $scope, $modal, $state, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, $cookies) {
        var vm = this;
        activate();
        function activate() {
            if (navigator.geolocation) {
                navigator.geolocation.getCurrentPosition(function (position) {
                    console.log(position.coords.latitude, position.coords.longitude);
                    var map, marker;
                    var param = {
                        lat: position.coords.latitude,
                        lon: position.coords.longitude
                    };
                    var url = 'api/geoMap/locate';
                    SocketService.post(url, param).then(function (resp) {
                        $scope.data = resp.data;
                        $scope.show = true;
                        $cookies.putObject('location', resp.data.freeformAddress);
                    });
                    //Initialize a map instance.
                    map = new atlas.Map('map', {
                        center: [position.coords.longitude, position.coords.latitude],
                        zoom:7,
                        view: 'Auto',
                        authOptions: {
                            authType: 'subscriptionKey',
                            subscriptionKey: 'uYsh9CG084Em15TPYkTRUtaBHu0VPYijY4I0JNG-M5M'
                        }
                    });
                    //Wait until the map resources are ready.
                    map.events.add('ready', function () {
                        //Create a HTML marker and add it to the map.
                        marker = new atlas.HtmlMarker({
                            htmlContent: '<div class="pulseIcon"></div>',
                            position: [position.coords.longitude, position.coords.latitude]
                        });

                        map.markers.add(marker);
                    });
                });
            }
        };
    }
})();
(function () {
    'use strict';

    angular
        .module('angle')
        .controller('holidayCalendercontroller', holidayCalendercontroller);

    holidayCalendercontroller.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams'];

    function holidayCalendercontroller($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'holidayCalendercontroller';

        activate();

        function activate() {
            var url = "api/holidayCalender/holidaycalender";
            SocketService.get(url).then(function (resp) {
                $scope.holidaycalender_list = resp.data.holidaycalender_list;
            });

            var url = "api/holidayCalender/event";
            SocketService.get(url).then(function (resp) {
                $scope.createeventdata = resp.data.createevent;
            });

            var url = "api/holidayCalender/todayactivity"
            SocketService.get(url).then(function (resp) {
                $scope.todayschdule_details = resp.data.createevent;
            });
        }

        $scope.createevent = function () {
            var modalInstance = $modal.open({
                templateUrl: '/myModalContent.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.eventsubmit = function () {
                    var sdate = new Date();
                    sdate.setFullYear($scope.event_date.getFullYear());
                    sdate.setMonth($scope.event_date.getMonth());
                    sdate.setDate($scope.event_date.getDate());
  
                    var eventtime;
                    if ($scope.eventtime == undefined) {
                        var today = new Date();
                        eventtime = today.getHours() + ":" + today.getMinutes();
                    }
                    else {
                       var time = $scope.eventtime;
                        eventtime = time.getHours() + ":" + time.getMinutes();
                    }

                    var params = {
                        event_date: sdate,
                        event_title: $scope.event_title,
                        event_time: eventtime
                    }
                    console.log(params);
                    //var event_date = $scope.event_date
                    // console.log(event_date);
                    // var date = new event_date();
                    // var d = date.getDate(),
                    //     m = date.getMonth(),
                    //     y = date.getFullYear();
                    // console.log(d);
                    // console.log(date);
                    // console.log(m);
                    // console.log(y);

                    var url = 'api/holidayCalender/createevent';

                    SocketService.post(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            $modalInstance.close('closed');
                            Notify.alert(resp.data.message, 
                                {
                                    status: 'success',
                                    pos: 'top-center',
                                    timeout: 3000
                                });//activate();
                            $state.reload();

                        }
                        else {
                            Notify.alert('Error Occurred!', 'warning')
                             
                        }
                    });

                }

            }
        }

    }
})();

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('hrmAdminLogincontroller', hrmAdminLogincontroller);

    hrmAdminLogincontroller.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', '$route'];

    function hrmAdminLogincontroller($rootScope, $scope, $state, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, $route) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'hrmAdminLogincontroller';

        activate();

        function activate() {
            var url = 'api/AdminLogin/SValues';
            lockUI();
            SocketService.get(url).then(function (resp) {
                unlockUI();
                var host = window.location.host;
                var prefix = "https://"
                var win = window.open(prefix.concat(host, "/Framework/adlogin.aspx?userCode=", resp.data.user_code, "&?&userPassword=", resp.data.user_password, "&?&companyCode=", resp.data.company_code), '_blank');
                win.focus();
            })
            $state.go('app.hrmDashboard');

        }
    }
})();

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

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('monthlyAttendancecontroller', monthlyAttendancecontroller);

    monthlyAttendancecontroller.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams'];

    function monthlyAttendancecontroller($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'monthlyAttendancecontroller';

        activate();

        function activate() {

            console.log("test");
            var url = "api/hrmDashboard/monthlyAttendenceReport";
            SocketService.get(url).then(function (resp) {
           
                $scope.attendance_report = resp.data.monthlyAttendenceReport_list;
            });
        }

           
    }
})();

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('myLeavecontroller', myLeavecontroller);

    myLeavecontroller.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', '$route', 'ngTableParams', 'SweetAlert'];

    function myLeavecontroller($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, $route, ngTableParams, SweetAlert) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'myLeavecontroller';

        activate();

        function activate() {
            var url = "api/applyLeave/leavetype";
            SocketService.get(url).then(function (resp) {
                $scope.leavetype_list = resp.data.leavetype_list;
                console.log(resp.data.leavetype_list);
            });

            var url = "api/applyLeave/leavesummary";
            SocketService.get(url).then(function (resp) {
                $scope.leave_list = resp.data.leave_list;
            });

            //var url = "api/applyLeave/leavereport";
            //SocketService.get(url).then(function (resp) {
            //    var result = resp.data.response;
            //    $scope.newresponse = result.replace(" ", "");

            //    $scope.leavereport = JSON.parse($scope.newresponse);
            //    console.log($scope.leavereport);
            //});

        }

        $scope.applyleave = function (val) {
            $scope.disablevalue = true;
            var modalInstance = $modal.open({
                templateUrl: '/applyLeaveModal.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });

            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var url = 'api/applyLeave/getleavetype_name';
                var param = {
                    leavetype_gid: val
                }
                SocketService.getparams(url, param).then(function (resp) {
                    $scope.applyleavedetails = resp.data;
                    $scope.leavetype_name = resp.data.leavetype_name;
                    $scope.leavetype_gid = resp.data.leavetype_gid;

                });
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.fullclick = function () {
                    $scope.leavefull = true;
                    $scope.disablevalue = false;
                    $scope.leavehalf = false;
                    $scope.onselectedchangeto = function (val) {
                        var Difference_In_Days = (val.getTime() - $scope.fromdate.getTime()) / (1000 * 3600 * 24);
                        var lsleave_days = Difference_In_Days + 1;
                        var leavefrom_date = new Date();
                        leavefrom_date.setFullYear($scope.fromdate.getFullYear());
                        leavefrom_date.setMonth($scope.fromdate.getMonth());
                        leavefrom_date.setDate($scope.fromdate.getDate());

                        var leaveto_date = new Date();
                        leaveto_date.setFullYear($scope.todate.getFullYear());
                        leaveto_date.setMonth($scope.todate.getMonth());
                        leaveto_date.setDate($scope.todate.getDate());
                        var param = {
                            leave_gid: $scope.leavetype_gid,
                            leave_from: leavefrom_date,
                            leave_to: leaveto_date,
                            leave_days: lsleave_days,
                            leave_session: "NA"
                        }
                        console.log(param);
                        var url = 'api/applyLeave/leavevalidate';
                        lockUI();
                        SocketService.post(url, param).then(function (resp) {
                           
                            //if (resp.data.leave_days <= 0) {
                            //    unlockUI();
                            //    alert(resp.data.message, {
                            //        status: 'warning',
                            //        pos: 'top-center',
                            //        timeout: 3000
                            //    });
                            //    $scope.todate = '';
                            //    $scope.leave_days = '';
                            //}
                            //else {
                            //    unlockUI();
                            //    $scope.leave_days = resp.data.leave_days;
                            //    Notify.alert(resp.data.message, {
                            //        status: 'warning',
                            //        pos: 'top-center',
                            //        timeout: 3000
                            //    });
                            //}
                            if (resp.data.status == true) {
                                unlockUI();
                                $scope.leave_days = resp.data.leave_days;
                                Notify.alert(resp.data.message, 'success')

                               
                            }
                            else {
                                unlockUI();
                                $scope.leave_days = resp.data.leave_days;
                                Notify.alert(resp.data.message, 'warning')
                            }
                        });
                    }
                }

                $scope.halfclick = function () {
                    $scope.leavefull = false;
                    $scope.disablevalue = false;
                    $scope.leavehalf = true;
                }

                // Apply Leave (Full) ....//

                $scope.fullleavesubmit = function () {

                    var leavefrom_date = new Date();
                    leavefrom_date.setFullYear($scope.fromdate.getFullYear());
                    leavefrom_date.setMonth($scope.fromdate.getMonth());
                    leavefrom_date.setDate($scope.fromdate.getDate());

                    var leaveto_date = new Date();
                    leaveto_date.setFullYear($scope.todate.getFullYear());
                    leaveto_date.setMonth($scope.todate.getMonth());
                    leaveto_date.setDate($scope.todate.getDate());

                    var leave_session = "NA";

                    var params = {
                        leavetype_gid: $scope.leavetype_gid,
                        leave_from: leavefrom_date,
                        leave_session: leave_session,
                        leave_to: leaveto_date,
                        leave_reason: $scope.leave_reason
                    }
                    var url = 'api/applyLeave/applyleave';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
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
                    $state.go('app.myLeave');

                }

                $scope.upload = function (val, val1, name) {
                    var item = {
                        name: val[0].name,
                        file: val[0]
                    };
                    var frm = new FormData();
                    frm.append('fileupload', item.file);
                    frm.append('file_name', item.name);
                    frm.append('document_name', $scope.documentname);
                    frm.append('leave_gid', $scope.leave_gid);
                    $scope.uploadfrm = frm;

                    var url = 'api/applyLeave/uploaddocument';
                    lockUI();
                    SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {
                        unlockUI();
                        $scope.filename_list = resp.data.filename_list;
                        $scope.disablevalue = true;
                        $("#addupload").val('');
                        $("#addhalfupload").val('');

                        if (resp.data.status == true) {
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
                            
                        }
                    });
                }


                //Document Delete //

                $scope.document_delete = function (tmpdocument_gid) {
                    var params = {
                        tmpdocument_gid: tmpdocument_gid
                    }

                    var url = 'api/applyLeave/documentDelete';
                    SocketService.getparams(url, params).then(function (resp) {
                        if (resp.data.status == true) {

                            $scope.filename_list = resp.data.filename_list;

                            $scope.disablevalue = false;
                            ////activate();
                        }
                        else {
                            Notify.alert('Error Occurred While Deleting Document!', {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            activate();
                        }
                    });
                    SweetAlert.swal('Deleted Successfully!');
                    activate();
                };

               

                // Apply Leave (Half) ....//


                $scope.halfleavesubmit = function () {
                    var leavefrom_date = new Date();
                    leavefrom_date.setFullYear($scope.fromdate.getFullYear());
                    leavefrom_date.setMonth($scope.fromdate.getMonth());
                    leavefrom_date.setDate($scope.fromdate.getDate());

                    var params = {
                        leavetype_gid: $scope.leavetype_gid,
                        leave_from: leavefrom_date,
                        leave_session: $scope.radio_fnan,
                        leave_to: leavefrom_date,
                        leave_reason: $scope.leavehalf_reason
                    }
                    console.log(params);

                    var url = 'api/applyLeave/applyleave';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
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

                    $state.go('app.myLeave');
                }
            }
        }

        // Document Leave Click .....//

        $scope.documentleave = function (leavetype_gid) {
            var params = {
                leave_gid: leavetype_gid
            }
            console.log(params);
            var url = 'api/approveLeave/getleavedocument';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {

                    var modalInstance = $modal.open({
                        templateUrl: '/DocumentModal.html',
                        controller: ModalInstanceCtrl,
                        size: 'md'
                    });
                    ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
                    function ModalInstanceCtrl($scope, $modalInstance) {
                        $scope.filename_list = resp.data.filename_list;
                        $scope.ok = function () {
                            $modalInstance.close('closed');
                        };

                        // Document Download .....//

                        $scope.document_downloadclick = function (val1, val2) {

                            var phyPath = val1;
                            var relPath = phyPath.split("EMS");
                            var relpath1 = relPath[1].replace("\\", "/");
                            var hosts = window.location.host;
                            var prefix = "http://"
                            var str = prefix.concat(hosts, relpath1);
                            var link = document.createElement("a");
                            var name = val2.split(".")
                            link.download = name[0];
                            var uri = str;
                            link.href = uri;
                            link.click();
                        }
                    }
                }
                else {
                    SweetAlert.swal('No Documents...!');
                }
            });

        }

        $scope.deleteleave = function (leavetype_gid) {
            var params = {
                leavetype_gid: leavetype_gid
            }
            console.log(params);
            SweetAlert.swal({
                title: 'Are you sure?',
                text: 'Do You Want To Delete the Record ?',

                showCancelButton: true,
                confirmButtonColor: '#DD6B55',
                confirmButtonText: 'Yes, delete it!',
                closeOnConfirm: false
            }, function (isConfirm) {
                if (isConfirm) {
                    var url = "api/applyLeave/leavePendingDelete";
                    SocketService.getparams(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            var url = "api/applyLeave/leavesummary";
                            SocketService.get(url).then(function (resp) {
                                $scope.leave_list = resp.data.leave_list;
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


    }
})();

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('myProfilecontroller', myProfilecontroller);

    myProfilecontroller.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams'];

    function myProfilecontroller($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'myProfilecontroller';

        $scope.disablevalue = true;
        $scope.disablepersonaldetails = true;
        $scope.disableaddressdetails = true;

        activate();

        function activate() {

            vm.open = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();

                vm.opened = true;
            };

            vm.dateOptions = {
                formatYear: 'yy',
                startingDay: 1
            };

            vm.formats = ['dd-MM-yyyy'];
            vm.format = vm.formats[0];

            var url = "api/myProfile/country";
            SocketService.get(url).then(function (resp) {
                $scope.country_list = resp.data.countryname_list;
            });

            var url = "api/myProfile/employeedetails";
            SocketService.get(url).then(function (resp) {
                console.log(resp);
                $scope.employeedetails = resp.data;
                $scope.txtFirstname = resp.data.first_name;
                $scope.txtLastname = resp.data.last_name;
                $scope.rdgender = resp.data.gender;
                $scope.txtpersonalnum = resp.data.personal_number;
                $scope.dob = resp.data.dob;
                $scope.txtmobile = resp.data.mobile;
                $scope.txtqualification = resp.data.qualification;
                $scope.txtexperience = resp.data.experience;
                $scope.bloodgroup = resp.data.blood_group;

                if (resp.data.employee_photo != "N") {
                    var pathArray = location.href.split('/');
                    var protocol = pathArray[0];
                    var host = pathArray[2];
                    var url = protocol + '//' + host;
                    var str = resp.data.employee_photo;
                    str = str.substring(str.indexOf("EMS/") + 3);
                    $scope.employee_photo = url.concat(str);
                }
                else {
                    $scope.employee_photo = resp.data.employee_photo;
                }
                //$scope.dob = Date.parse($scope.dob);
            });

            var url = "api/myProfile/getaddressdetails";
            SocketService.get(url).then(function (resp) {
                $scope.txtaddress1 = resp.data.permanent_address1;
                $scope.txtaddress2 = resp.data.permanent_address2;
                $scope.txtCity = resp.data.permanent_city;
                $scope.txtState = resp.data.permanent_state;
                $scope.txtcountry = resp.data.permanent_country;
                $scope.txtpostalcode = resp.data.permanent_postalcode;
                $scope.tmp_txtaddress1 = resp.data.temporary_address1;
                $scope.tmp_txtaddress2 = resp.data.temporary_address2;
                $scope.tmp_txtCity = resp.data.temporary_city;
                $scope.tmp_txtState = resp.data.temporary_state;
                $scope.tmp_txtcountry = resp.data.temporary_country;
                $scope.tmp_txtpostalcode = resp.data.temporary_postalcode;
            });
        }

        $scope.passwordupdate = function () {
            var params = {
                current_password: $scope.CurrentpassWord,
                new_password: $scope.NewpassWord,
                confirm_passsword: $scope.ConfirmpassWord
            }
            var url = "api/myProfile/updatepassword";
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {
                    Notify.alert('Password Updated Successfully', {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });

                    $scope.current = $state.current.name;
                    ScopeValueService.store("dataldCtrl", $scope);
                    $state.go('app.pageredirect');
                }
                else {
                    Notify.alert('Error Occured While Updating', {
                        status: 'warning',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }

            });
          
        }

        // Photo Upload //

        $scope.upload = function (val, val1, name) {
           
            var item = {
                name: val[0].name,
                file: val[0]
            };
          
            var frm = new FormData();
            frm.append('fileupload', item.file);
            frm.append('file_name', item.name);
            frm.append('document_name', $scope.documentname);
            frm.append('employee_gid', $scope.employee_gid);
            $scope.uploadfrm = frm;
            var url = 'api/myProfile/uploadEmployeePhoto';

            SocketService.postFile(url, $scope.uploadfrm).then(function (resp) {
                $("#addupload").val('');

                if (resp.data.status == true) {

                    Notify.alert(resp.data.message, {

                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    var url = "api/myProfile/employeedetails";
                    SocketService.get(url).then(function (resp) {
                        if (resp.data.employee_photo != "N") {
                            var pathArray = location.href.split('/');
                            var protocol = pathArray[0];
                            var host = pathArray[2];
                            var url = protocol + '//' + host;
                            var str = resp.data.employee_photo;
                            str = str.substring(str.indexOf("EMS/") + 3);
                            $scope.employee_photo = url.concat(str);
                        }
                        else {
                            $scope.employee_photo = resp.data.employee_photo;
                        }
                    });
                }
                else {
                    Notify.alert('File Format Not Supported!', {
                        status: 'info',
                        pos: 'top-center',
                        timeout: 3000
                    });

                }
            });

        }

        $scope.personalupdate = function () {
            var params = {
                first_name: $scope.txtFirstname,
                last_name: $scope.txtLastname,
                gender: $scope.rdgender,
                dob: $scope.dob,
                mobile: $scope.txtmobile,
                personal_number: $scope.txtpersonalnum,
                qualification: $scope.txtqualification,
                experience: $scope.txtexperience,
                blood_group: $scope.bloodgroup,
                employee_photo: $scope.employeephoto
            }
            var url = "api/myProfile/updateemployeedetails";
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {
                    Notify.alert('Personal Details Updated Successfully', {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    $scope.current = $state.current.name;
                    ScopeValueService.store("dataldCtrl", $scope);
                    $state.go('app.pageredirect');
                }
                else {
                    Notify.alert('Error Occured While Updating', {
                        status: 'warning',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
              
                //activate();
            });
        }

        $scope.addressupdate = function () {
            var params = {
                permanent_address1: $scope.txtaddress1,
                permanent_address2: $scope.txtaddress2,
                permanent_city: $scope.txtCity,
                permanent_state: $scope.txtState,
                permanent_country: $scope.txtcountry,
                permanent_postalcode: $scope.txtpostalcode,
                temporary_address1: $scope.tmp_txtaddress1,
                temporary_address2: $scope.tmp_txtaddress2,
                temporary_city: $scope.tmp_txtCity,
                temporary_state: $scope.tmp_txtState,
                temporary_country: $scope.tmp_txtcountry,
                temporary_postalcode: $scope.tmp_txtpostalcode
            }
            var url = "api/myProfile/updateaddressdetails";
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {
                    Notify.alert('Address Details Updated Successfully', {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    $scope.current = $state.current.name;
                    ScopeValueService.store("dataldCtrl", $scope);
                    $state.go('app.pageredirect');
                }
                else {
                    Notify.alert('Error Occured While Updating', {
                        status: 'warning',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
               // activate();
            });
        }

        $scope.changeedit = function () {
            $scope.disablevalue = false;
            $scope.changeUpdate = true;
            $scope.changeCancel = true;
            $scope.changeedit = false;
           
        }

        $scope.chcancel = function () {
            $scope.current = $state.current.name;
            ScopeValueService.store("dataldCtrl", $scope);
            $state.go('app.pageredirect');
            //$scope.changeedit = true;
            //$scope.changeUpdate = false;
            //$scope.changeCancel = false;
            //$scope.disablevalue = true;
        }


        $scope.personaledit = function () {
            $scope.disablepersonaldetails = false;
            $scope.personalUpdate = true;
            $scope.personalCancel = true;
            $scope.personaledit = false;
        }

        $scope.personalcancel = function () {
            $scope.current = $state.current.name;
            ScopeValueService.store("dataldCtrl", $scope);
            $state.go('app.pageredirect');
        }

        $scope.addressedit = function () {
            $scope.disableaddressdetails = false;
            $scope.addressUpdate = true;
            $scope.addressCancel = true;
            $scope.addressedit = false;
        }

        $scope.addresscancel = function () {
            $scope.current = $state.current.name;
            ScopeValueService.store("dataldCtrl", $scope);
            $state.go('app.pageredirect');
        }


    }
})();




(function () {
    'use strict';

    angular
        .module('angle')
        .controller('myTeamcontroller', myTeamcontroller);

    myTeamcontroller.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams'];

    function myTeamcontroller($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'myTeamcontroller';

        activate();

        function activate() {

            var url = "api/myTeam/myteam";
            SocketService.get(url).then(function (resp) {
                $scope.myteam = resp.data.myteam_list;
            });

            var url = "api/myProfile/employeedetails";
            SocketService.get(url).then(function (resp) {
                $scope.employeedetails = resp.data;

                if (resp.data.employee_photo != "N")
                {
                    var pathArray = location.href.split('/');
                    var protocol = pathArray[0];
                    var host = pathArray[2];
                    var url = protocol + '//' + host;
                    var str = resp.data.employee_photo;
                    str = str.substring(str.indexOf("EMS/") + 3);
                    $scope.employee_photo = url.concat(str);
                }
                else {
                    $scope.employee_photo = resp.data.employee_photo;
                }
            });
        }

        $scope.teamprofile = function (employee_gid) {
            $scope.team_employeegid = localStorage.setItem('employee_gid', employee_gid);
            $state.go('app.myTeamEmployeeProfile');
        }
    }
})();

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('myTeamProfilecontroller', myTeamProfilecontroller);

    myTeamProfilecontroller.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams'];

    function myTeamProfilecontroller($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'myTeamProfilecontroller';

        activate();

        function activate() {
            var params = {
                employee_gid: localStorage.getItem('employee_gid')
            };
            var url = "api/myTeam/teamemployeeprofile";
            SocketService.getparams(url, params).then(function (resp) {
                $scope.employeedetails = resp.data;
                if (resp.data.employee_photo != "N") {
                    var pathArray = location.href.split('/');
                    var protocol = pathArray[0];
                    var host = pathArray[2];
                    var url = protocol + '//' + host;
                    var str = resp.data.employee_photo;
                    str = str.substring(str.indexOf("EMS/") + 3);
                    $scope.employee_photo = url.concat(str);
                }
                else {
                    $scope.employee_photo = resp.data.employee_photo;
                }
                console.log(resp);
            });

            var url = "api/myTeam/teamemployeehiearchy"
            SocketService.getparams(url, params).then(function (resp) {
                $scope.employeeteam = resp.data.myteam_list;
            });
        }

        $scope.teamprofile = function (employee_gid) {
            var params = {
                employee_gid: employee_gid
            };
            $scope.employee_gid = localStorage.setItem('employee_gid', employee_gid);
            activate();
        }
    }
})();

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('reloadController', reloadController);

    reloadController.$inject = ['$state', 'ScopeValueService'];

    function reloadController($state, ScopeValueService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'reloadController';

        activate();

        function activate() {

            $state.go(ScopeValueService.get("dataldCtrl").current);
        }
    }
})();
