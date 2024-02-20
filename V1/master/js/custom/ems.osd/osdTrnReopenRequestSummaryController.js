(function () {
    'use strict';


    angular
        .module('angle')
        .controller('osdTrnReopenRequestSummaryController', osdTrnReopenRequestSummaryController);

        osdTrnReopenRequestSummaryController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams','cmnfunctionService'];

    function osdTrnReopenRequestSummaryController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams,cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'osdTrnReopenRequestSummaryController';
        var lstab = $location.search().lstab;
        activate();

        lockUI();
        function activate() {

            var url = "api/OsdTrnServiceRequest/GetServiceRequestCount";
            SocketService.get(url).then(function (resp) {
                $scope.request_count = resp.data.request_count;
                $scope.tagged_count = resp.data.tagged_count;
                $scope.forward_count = resp.data.forward_count;
                $scope.reopen_count = resp.data.reopen_count;
                $scope.Reject_count = resp.data.reject_count;
                $scope.close_count = resp.data.close_count;
                $scope.cancel_count = resp.data.cancel_count;
                unlockUI();
            });

         
            var url = "api/OsdTrnServiceRequest/GetReopenSummary";
            SocketService.get(url).then(function (resp) {
                $scope.reopenactivitysummary = resp.data.reopendtl;
                unlockUI();
            });

        }

         // My Request
         $scope.my_request = function () {
            $state.go('app.osdTrnServiceRequestSummary');
        }      

        // Tagged Request
        $scope.tagged_request = function () {
            $state.go('app.osdTrnTaggedRequestSummary');
        }

        // Forward Activity
        $scope.forward_request = function () {
            $state.go('app.osdTrnForwardTransferSummary');
        }

        // Reopen Activity
        $scope.Reopen_request = function () {
            $state.go('app.osdTrnReopenRequestSummary');
        }
        
        //Rejected Request
        $scope.Reject_request = function () {
            $state.go('app.osdTrnRejectedRequestSummary');
        }

        // Close Activity
        $scope.Close_request = function () {
            $state.go('app.osdTrnCloseRequestSummary');
        }
        

       //Cancel Activity
     $scope.Cancel_request = function () {
    $state.go('app.osdTrnCancelledRequestSummary');
     }

        $scope.raiserequest = function () {
            $location.url('app/osdTrnServiceRequestAdd?hash=' + cmnfunctionService.encryptURL('lspage=reopenactivity'));
        }
      
        $scope.viewreopenrequest = function (servicerequest_gid, request_status) {
            var param = {
                servicerequest_gid: servicerequest_gid
            }
            var url = 'api/OsdTrnServiceRequest/GetServiceRequestViewUpdate';
            lockUI()
            SocketService.getparams(url, param).then(function (resp) {
                if (resp.data.status == true) {
                    unlockUI()
                }
                else {
                    unlockUI();
                }
            });

            if (request_status == 'Completed') {
                var val = "Y";
                $location.url('app/osdTrnServiceRequestReopenView?hash=' + cmnfunctionService.encryptURL('servicerequest_gid=' + servicerequest_gid + '&CompletedFlag=' + val + '&lspage=reopenactivity'));
                
            }
            else if (request_status == 'Closed') {
                var val = "C";
                $location.url('app/osdTrnServiceRequestReopenView?hash=' + cmnfunctionService.encryptURL('servicerequest_gid=' + servicerequest_gid + '&CompletedFlag=' + val + '&lspage=reopenactivity'));
               
            }
            else {
                var val = "N";
                $location.url('app/osdTrnServiceRequestReopenView?hash=' + cmnfunctionService.encryptURL('servicerequest_gid=' + servicerequest_gid + '&CompletedFlag=' + val + '&lspage=reopenactivity'));
               
            }
          
           
        }
       
        $scope.getApprovalRequest = function (servicerequest_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/getApprovalmodal.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
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
                        servicerequest_gid: servicerequest_gid,
                        approvalreopen_flag: 'Y',

                    }

                    lockUI();
                    var url = "api/OsdTrnMyTicket/PostApprovalGet";
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {
                            modalInstance.close('closed');
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            unlockUI();
                            activate();

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

        $scope.getReApprovalRequest = function (servicerequest_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/getReApprovalmodal.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
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
                        servicerequest_gid: servicerequest_gid,
                        approvalreopen_flag: 'Y',
                    }
                    lockUI();
                    var url = "api/OsdTrnMyTicket/PostApprovalGet";
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {
                            modalInstance.close('closed');
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            unlockUI();
                            activate();

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