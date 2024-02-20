(function () {
    'use strict';

    angular
        .module('angle')
        .controller('OsdTrnWorkMyTicketController', OsdTrnWorkMyTicketController);

    OsdTrnWorkMyTicketController.$inject = ['$rootScope', '$scope', '$sce', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', '$route', '$cookies', '$filter', 'ngTableParams', '$timeout','cmnfunctionService'];


    function OsdTrnWorkMyTicketController($rootScope, $scope, $sce, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, $route, $cookies, $filter, ngTableParams, $timeout,cmnfunctionService) {

        /* jshint validthis:true */
        var vm = this;
        vm.title = 'OsdTrnWorkMyTicketController';

        activate();

        function activate() {
            lockUI();
            var url = "api/OsdTrnMyTicket/GetActivityCount";
            SocketService.get(url).then(function (resp) {
                $scope.alloted_count = resp.data.lsallotedcount;
                $scope.workinprogress_count = resp.data.lsworkinprogress_count;
                $scope.completed_count = resp.data.completed_count;
                $scope.closed_count = resp.data.closed_count;
                $scope.transfer_count = resp.data.transfer_count;
                $scope.forward_count = resp.data.lsforward_count;
                $scope.approvalpending_count = resp.data.approvalpending_count;
                unlockUI();
            });
            lockUI();
          
            var url = "api/OsdTrnMyTicket/GetWorkInProgressSummary";
            SocketService.get(url).then(function (resp) {
                $scope.workinprogress_list = resp.data.workinprogressdtl;
                
                unlockUI();
            });
        }


        $scope.Refresh = function () {
            activate();
        }

        $scope.Allotted = function () {
            $state.go('app.OsdTrnAllotedMyTicket');
        }
        $scope.Work = function () {
            $state.go('app.OsdTrnWorkMyTicket');
        }
        $scope.Approval = function () {
            $state.go('app.OsdTrnApprovalMyTicket');
        }
        $scope.External = function () {
            $state.go('app.OsdTrnExternalMyTicket');
        }
        $scope.Internal = function () {
            $state.go('app.OsdTrnInternalMyTicket');
        }
        $scope.Completed = function () {
            $state.go('app.OsdTrnCompletedMyTicket');
        }
        $scope.Closed = function () {
            $state.go('app.OsdTrnClosedMyTicket');
        }


        $scope.workinprogressview360 = function (val, val2, val3, val4,val6,val7) {
            $scope.servicerequest_gid = val;
            $scope.bankalert_flag = val2;
            $scope.bankalert2allocated_gid = val3;
            $scope.customer_gid = val4;
            $scope.customer_urn = val6;
            $scope.request_refno = val7;
            var servicerequest_gid = val;
            var bankalert_flag = val2;
            var bankalert2allocated_gid = val3;
            var customer_gid = val4;
            var customer_urn = val6;
            var request_refno = val7;


            var param = {
                servicerequest_gid: servicerequest_gid,
                bankalert_flag: bankalert_flag,
                bankalert2allocated_gid: bankalert2allocated_gid,
                customer_gid: customer_gid
            }
            var url = 'api/OsdTrnMyTicket/GetServiceRequestForwardView360Update';
            lockUI()
            SocketService.getparams(url, param).then(function (resp) {
                if (resp.data.status == true) {
                    unlockUI()
                }
                else {
                    unlockUI();
                }
            });
            var val5 = "N";
            $location.url('app/osdTrnMyActivity360?hash=' + cmnfunctionService.encryptURL('servicerequest_gid=' + val + '&bankalert_flag=' + val2 + '&bankalert2allocated_gid=' + val3 + '&customer_gid=' + val4 + '&RequestCompletedFlag=' + val5 + '&lspage=Work-InProgress' + '&customer_urn=' + val6 + '&request_refno=' + val7));
        }

        $scope.TransferAllocationwithRemarks = function (servicerequest_gid, assigned_team, assigned_to, assigned_membergid, assigned_supportteamgid, department_gid, department_name,activitymaster_gid, activity_name ) {

            var modalInstance = $modal.open({
                templateUrl: '/transferallocationwithremarksmodal.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            var servicerequest_gid = servicerequest_gid;
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.department_name = department_name;
                $scope.cboactivity = activitymaster_gid;
                var params = {
                    department_gid: department_gid,
                    activitymaster_gid: activitymaster_gid,
                    servicerequest_gid: servicerequest_gid
                }
                var url = 'api/OsdMstActivity/GetTeamSummary';
                SocketService.getparams(url,params).then(function (resp) {
                    $scope.supportdtllist = resp.data.supportdtl;
                });
                var url = 'api/OsdMstActivity/GetActivity';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.activitylist = resp.data.activitydtl;
                    //activity_name = activity_name;
                    //$scope.cboactivity = activitymaster_gid;
                });
                var url = 'api/OsdTrnTicketManagement/GetActivityEdit';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.cboactivity.activity_name = resp.data.activity_name;
                });

                $scope.onselectedchangeteam = function (team_name) {

                    var params = {
                        supportteam_gid: $scope.cbosuppport_team.supportteam_gid
                    }
                    var url = 'api/OsdMstSupportTeam/GetTeamMemberExcept';
                    SocketService.getparams(url, params).then(function (resp) {
                        $scope.teammembers_list = resp.data.teammembers;
                    });
                }

                var params = {
                    servicerequest_gid: servicerequest_gid
                }
                var url = "api/OsdTrnTicketManagement/GetTransferMemberlist"

                SocketService.getparams(url, params).then(function (resp) {
                    $scope.transferlistdtllist = resp.data.transferlistdtl;
                    var transfer_flag = resp.data.transferlistdtl[0].transfer_flag;
                    if (transfer_flag == 'Y') {
                        $scope.transferlist = true;
                    }
                    else {
                        $scope.transferlist = false;
                    }
                    unlockUI();
                });
                // TransferAllocationremarks Submit Event
                $scope.teamSubmitremarks = function () {
                    lockUI();
                    var params = {
                        servicerequest_gid: servicerequest_gid,
                        assigned_supportteam: assigned_team,
                        assigned_member: assigned_to,
                        assigned_membergid: assigned_membergid,
                        activitymaster_gid: $scope.cboactivity,
                        assigned_supportteamgid: assigned_supportteamgid,
                        transferteam_gid: $scope.cbosuppport_team.supportteam_gid,
                        transferteam_name: $scope.cbosuppport_team.team_name,
                        transferemployee_gid: $scope.cboemployee_name.employee_gid,
                        transferemployee_name: $scope.cboemployee_name.employee_name,
                        //priority: $scope.priority,
                        remarks: $scope.team_description
                    }
                    var url = "api/OsdTrnTicketManagement/PostTransferAllocation";
                    SocketService.post(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            $modalInstance.close('closed');
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });

                            activate();                          
                            unlockUI();
                        }
                        else {
                            $modalInstance.close('closed');
                            Notify.alert(resp.data.message, {
                                status: 'danger',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            $modalInstance.close('closed');
                            unlockUI();
                        }
                    });
                }
                $scope.ok = function () {
                    modalInstance.close('closed');
                };

            }
        }

        $scope.Priority = function (servicerequest_gid, department_gid, department_name) {
            var modalInstance = $modal.open({
                templateUrl: '/prioritymodal.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.department_name = department_name;
                var params = {
                    department_gid: department_gid
                }
                var params = {
                    servicerequest_gid: servicerequest_gid
                }

                var url = "api/OsdTrnTicketManagement/GetPrioritylist"

                SocketService.getparams(url, params).then(function (resp) {
                    $scope.prioritylistdtllist = resp.data.prioritylistdtl;
                    //var transfer_flag = resp.data.transferlistdtl[0].transfer_flag;
                    //if (transfer_flag == 'Y') {
                    //    $scope.transferlist = true;
                    //}
                    //else {
                    //    $scope.transferlist = false;
                    //}
                    unlockUI();
                });

                // TransferAllocation Submit Event
                $scope.prioritySubmit = function () {
                    var lspriority;
                    lockUI();
                    if (($scope.priority == undefined) || ($scope.priority == "")) {
                        lspriority = "None";
                    }
                    else {
                        lspriority = $scope.priority;
                    }
                    var params = {
                        servicerequest_gid: servicerequest_gid,

                        priority: lspriority,


                    }

                    var url = "api/OsdTrnTicketManagement/PostPriority";
                    SocketService.post(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            $modalInstance.close('closed');
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            activate();
                            unlockUI();
                        }
                        else {
                            $modalInstance.close('closed');
                            Notify.alert(resp.data.message, {
                                status: 'danger',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            activate();
                            unlockUI();
                        }
                    });
                }

                $scope.ok = function () {
                    modalInstance.close('closed');
                };

            }
        }
        $scope.getApprovalRequest = function (servicerequest_gid, val) {
            var modalInstance = $modal.open({
                templateUrl: '/getApprovalmodal.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            var val = val;
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    servicerequest_gid: servicerequest_gid
                }
                var url = 'api/OsdTrnMyTicket/EmployeeNotIn';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.employee_list = resp.data.employeelist;
                });
                //var url = 'api/OsdMstActivity/GetTeamSummary';
                //SocketService.get(url).then(function (resp) {
                //    $scope.supportdtllist = resp.data.supportdtl;
                //});
                var url = 'api/OsdTrnMyTicket/TmpAllMembersDelete';
                SocketService.get(url).then(function (resp) {

                });

                $scope.ok = function () {
                    modalInstance.close('closed');
                };

                $scope.ApprovalMembercancel = function (tmpapprovalmember_gid) {
                    var params = {
                        tmpapprovalmember_gid: tmpapprovalmember_gid,
                        servicerequest_gid: servicerequest_gid,
                    }
                    var url = 'api/OsdTrnMyTicket/TmpApprovalMembersDelete';
                    SocketService.post(url, params).then(function (resp) {
                        $scope.approvalmember = resp.data.approvalmember;

                        var param = {
                            servicerequest_gid: servicerequest_gid
                        }
                        var url = 'api/OsdTrnMyTicket/EmployeeNotIn';
                        SocketService.getparams(url, param).then(function (resp) {
                            $scope.employee_list = resp.data.employeelist;
                        });


                    });



                }

                $scope.coreChanged = function (cboapproval_member) {
                    var params = {
                        approvalgid: $scope.cboapproval_member.employee_gid,
                        approvalname: $scope.cboapproval_member.employee_name,
                        servicerequest_gid: servicerequest_gid,
                    }

                    lockUI();
                    var url = "api/OsdTrnMyTicket/TempApprovalMember";
                    SocketService.post(url, params).then(function (resp) {

                        if (resp.data.status == true) {
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000

                            });
                            unlockUI();
                            $scope.current = $state.current.name;
                            ScopeValueService.store("dataldCtrl", $scope);
                            //$state.go('app.pageredirect');
                            $scope.cboapproval_member = "";
                            var params = {
                                servicerequest_gid: servicerequest_gid
                            }
                            var url = 'api/OsdTrnMyTicket/TmpApprovalMembersView';
                            SocketService.getparams(url, params).then(function (resp) {
                                $scope.approvalmember = resp.data.approvalmember;
                            });

                            var url = 'api/OsdTrnMyTicket/EmployeeNotIn';
                            SocketService.getparams(url, params).then(function (resp) {
                                $scope.employee_list = resp.data.employeelist;
                            });
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

                $scope.getapprovalclick = function () {
                    var params = {
                        approval_remarks: $scope.approval_remarks,
                        approval_type: $scope.approval_type,
                        approval_basedon: 'HA',
                        servicerequest_gid: servicerequest_gid
                    }

                    lockUI();
                    var url = "api/OsdTrnMyTicket/PostApprovalGet";
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {
                            modalInstance.close('closed');
                            activate();
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            unlockUI();
                           

                        }
                        else {
                            //modalInstance.close('closed');
                            alert(resp.data.message, {
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

        $scope.getReApprovalRequest = function (servicerequest_gid, val) {
            var modalInstance = $modal.open({
                templateUrl: '/getReApprovalmodal.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            var val = val;
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var url = 'api/employee/employee';
                SocketService.get(url).then(function (resp) {
                    $scope.employee_list = resp.data.employee_list;
                });
                //var url = 'api/OsdMstActivity/GetTeamSummary';
                //SocketService.get(url).then(function (resp) {
                //    $scope.supportdtllist = resp.data.supportdtl;
                //});
                $scope.ok = function () {
                    modalInstance.close('closed');
                };

                $scope.getreapprovalclick = function () {
                    var params = {
                        approvalname: $scope.cboapproval_member.employee_name,
                        approvalgid: $scope.cboapproval_member.employee_gid,
                        approval_remarks: $scope.approval_remarks,
                        approval_type: 'Approval',
                        approval_basedon: 'RA',
                        servicerequest_gid: servicerequest_gid
                    }
                    lockUI();
                    var url = "api/OsdTrnMyTicket/PostApprovalGet";
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {
                            activate();
                            modalInstance.close('closed');
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            unlockUI();

                            
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

    }
})();
