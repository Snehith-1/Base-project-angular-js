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
