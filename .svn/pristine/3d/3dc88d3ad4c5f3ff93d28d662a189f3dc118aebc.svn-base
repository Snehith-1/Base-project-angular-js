(function () {
    'use strict';

    angular
        .module('angle')
        .controller('osdTrnTicketAllotedSummaryController', osdTrnTicketAllotedSummaryController);

    osdTrnTicketAllotedSummaryController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', '$route', 'ngTableParams','cmnfunctionService'];

    function osdTrnTicketAllotedSummaryController($rootScope, $scope, $modal, $state, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, $route, ngTableParams,cmnfunctionService) {

        /* jshint validthis:true */
        var vm = this;
        vm.title = 'osdTrnTicketAllotedSummaryController';

        activate();

        function activate() {

            lockUI();

            var url = 'api/OsdTrnTicketManagement/GetCountSummary';
            SocketService.get(url).then(function (resp) {
                //$scope.count_newticket = resp.data.alloted_count;
                //$scope.count_transferticket = resp.data.workinprogress_count;
                $scope.count_completedticket = resp.data.completed_count;
                $scope.count_closedticket = resp.data.closed_count;
                $scope.forward_count = resp.data.forward_count;
                $scope.rejectcancel_count = resp.data.rejectcancel_count;
                $scope.approvalpending_count = resp.data.approvalpending_count;
            });
           
            var url = 'api/OsdTrnTicketManagement/GetMyWorkInProgressSummary';
            SocketService.get(url).then(function (resp) {
                $scope.count_transferticket = resp.data.lsworkinprogress_count;
                //unlockUI();
            });

            var url = 'api/OsdTrnTicketManagement/GetServiceRequestSummary';
            SocketService.get(url).then(function (resp) {
                $scope.servicerequestdtl = resp.data.servicerequestdtl;
                $scope.count_newticket = resp.data.lsallotted_count;

                unlockUI();
            });

            var url = 'api/OsdTrnBankAlert/GetBankalertNotification';
            SocketService.get(url).then(function (resp) {
                if(resp.data.display=="true")
                {
                    if (resp.data.allocated_new == "true" || resp.data.notallocated_new == "true" || resp.data.allocatedtransfer_new == "true") {
                        if (resp.data.privilege == "true") {
                            $scope.new = true;
                            $scope.old = false;
                        }
                        else {
                            $scope.new = false;
                            $scope.old = false;
                        }

                    }
                    else {
                        if (resp.data.privilege == "true") {
                            $scope.new = false;
                            $scope.old = true;
                        }
                        else {
                            $scope.new = false;
                            $scope.old = false;
                        }
                    }
                }
                else {
                    $scope.new = false;
                    $scope.old = false;
                }
            });
        }

        $scope.Refresh = function () {
            activate();
        }

        $scope.Alloted = function () {
            $state.go('app.osdTrnTicketAllotedSummary');
        }
        $scope.Workinprogress = function () {
            $state.go('app.osdTrnTicketWorkSummary');
        }
        $scope.approvalpending = function () {
            $state.go('app.osdTrnTicketApprovalSummary');
        }
        $scope.Forward = function () {
            $state.go('app.osdTrnTicketForwardSummary');
        }
        $scope.Completed = function () {
            $state.go('app.osdTrnTicketCompletedSummary');
        }
        $scope.Close = function () {
            $state.go('app.osdTrnTicketClosedSummary');
            //var URL = location.protocol + "//" + location.hostname + "/v1/#/app/osdTrnTicketClosedSummary";
            //window.open(URL, '_blank');
        }
        $scope.RejectCancel = function () {
            $state.go('app.osdTrnTicketRejectCancelSummary');
        }
        $scope.BankAlert = function () {
            $state.go('app.OsdTrnBankAlertManagementSummary');
        }

        $scope.viewNew = function (val, val2, val3, val4,val7) {           

            $location.url('app/osdTrnActivityManagement360?hash=' + cmnfunctionService.encryptURL('servicerequest_gid=' + val + '&bankalert_flag=' + val2 + '&bankalert2allocated_gid=' + val3 + '&customer_gid=' + val4 + '&lspage=New' + '&request_refno=' + val7));
          
        }


        $scope.TransferAllocation = function (servicerequest_gid, assigned_team, assigned_to, assigned_membergid, assigned_supportteamgid, department_gid, department_name,activitymaster_gid, activity_name ) {
           
            var modalInstance = $modal.open({
                templateUrl: '/transferallocationmodal.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            var servicerequest_gid = servicerequest_gid;
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.department_name = department_name;
                //$scope.activity_name = activity_name;
                $scope.cboactivity = activitymaster_gid;
                //$scope.cboactivity = activity_name;
                var params = {
                    department_gid: department_gid,
                    activitymaster_gid: activitymaster_gid,
                    servicerequest_gid : servicerequest_gid
                }
                var url = 'api/OsdMstActivity/GetTeamSummary';
                SocketService.getparams(url,params).then(function (resp) {
                    $scope.supportdtllist = resp.data.supportdtl;
                });
              
                var url = 'api/OsdMstActivity/GetActivity';
                SocketService.getparams(url,params).then(function (resp) {
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
                        supportteam_gid: $scope.cbosuppport_team.supportteam_gid,
                        servicerequest_gid: servicerequest_gid,
                    }
                    var url = 'api/OsdMstSupportTeam/PostTeamMemberExceptAssigned';
                    SocketService.post(url, params).then(function (resp) {
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

                // TransferAllocation Submit Event
                $scope.teamSubmit = function () {
                    lockUI();
                    var params = {
                        servicerequest_gid: servicerequest_gid,
                        assigned_supportteam: assigned_team,
                        assigned_member: assigned_to,
                        assigned_membergid: assigned_membergid,
                        ////activity_name: $scope.cboactivity,
                        activitymaster_gid: $scope.cboactivity,
                        ////activitymaster_gid: $scope.cboactivity.activitymaster_gid,
                        //activity_name: activity_name,
                        //priority: $scope.priority,
                        assigned_supportteamgid: assigned_supportteamgid,
                        transferteam_gid: $scope.cbosuppport_team.supportteam_gid,
                        transferteam_name: $scope.cbosuppport_team.team_name,
                        transferemployee_gid: $scope.cboemployee_name.employee_gid,
                        transferemployee_name: $scope.cboemployee_name.employee_name,
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
                    else
                    {
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
        $scope.SelfAllocation = function (servicerequest_gid, assigned_team, assigned_to, assigned_membergid, assigned_supportteamgid, department_gid, department_name,activitymaster_gid, activity_name ) {

            var modalInstance = $modal.open({
                templateUrl: '/selfallocatemodal.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            var servicerequest_gid = servicerequest_gid;
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.department_name = department_name;
                //$scope.activity_name = activity_name;
                $scope.cboactivity = activitymaster_gid;
                //$scope.cboactivity = activity_name;
                var params = {
                    department_gid: department_gid                  
                }
                var url = 'api/OsdTrnTicketManagement/GetManagerSummary';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.managerdtllist = resp.data.managerdtl;
                });
                              

            

                var params = {
                    servicerequest_gid: servicerequest_gid
                }

                var url = "api/OsdTrnTicketManagement/GetAllocateManagerlist"

                SocketService.getparams(url, params).then(function (resp) {
                    $scope.allocatedlist = resp.data.allocatelistdtl;
                    var transfer_flag = resp.data.allocatelistdtl[0].transfer_flag;
                    if (transfer_flag == 'Y') {
                        $scope.transferlist = true;
                    }
                    else {
                        $scope.transferlist = false;
                    }
                    unlockUI();
                });

                // SelfAllocation Submit Event
                $scope.allocateSubmit = function () {
                    lockUI();
                    var params = {
                        servicerequest_gid: servicerequest_gid,                      
                        assigned_membername: assigned_to,
                        assigned_membergid: assigned_membergid,                      
                        allocate_managergid: $scope.cbomanager.manager_gid,
                        allocate_managername: $scope.cbomanager.manager_name,
                        remarks: $scope.manager_description
                    }
                    var url = "api/OsdTrnTicketManagement/PostSelfAllocation";
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
    }
})();
