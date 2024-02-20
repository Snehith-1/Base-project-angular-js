(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstRejectedSummaryController', MstRejectedSummaryController);

    MstRejectedSummaryController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams', 'cmnfunctionService'];

    function MstRejectedSummaryController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams, cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstRejectedSummaryController';
        activate();
        lockUI();
        function activate() {
            var url = 'api/TeleCalling/GetEmpRejectedIBCallSummary';
            SocketService.get(url).then(function (resp) {
                $scope.ibcallrejected_list = resp.data.ibcall_list;
                unlockUI();
            });           

            var url = "api/TeleCalling/EmployeeIBCallCount";
            SocketService.get(url).then(function (resp) {
                $scope.assignedcall_count = resp.data.assignedcall_count;
                $scope.transfercall_count = resp.data.transfercall_count;
                $scope.completedcall_count = resp.data.completedcall_count;
                $scope.inprogresscall_count = resp.data.inprogresscall_count;
                $scope.tagmember_count = resp.data.taggedcall_count;
                $scope.followupcall_count = resp.data.followupcall_count;
                $scope.rejectedcall_count = resp.data.rejectedcall_count;
                unlockUI();
            });
        }
        $scope.myassigned_calls = function () {
            $location.url("app/MstMyAssignedCallSummary");
        }
        $scope.tag_member = function () {
            $location.url("app/MstTaggedMemberSummary");
        }
        $scope.transfer_calls = function () {
            $location.url("app/MstTransferCallSummary");
        }
        $scope.completed_calls = function () {
            $location.url("app/MstCompletedCallSummary");
        }
        $scope.view = function (inboundcall_gid) {
            $location.url("app/MstAssignedCallView?hash=" + cmnfunctionService.encryptURL("inboundcall_gid=" + inboundcall_gid + '&lspage=CompletedCall'));
        }
        $scope.edit = function (inboundcall_gid) {
            $location.url("app/MstInboundEdit?hash=" + cmnfunctionService.encryptURL("inboundcall_gid=" + inboundcall_gid));
        }
        $scope.work_inprogress = function () {
            $location.url("app/MstWorkInprogressCallSummary");
        }
        $scope.followup_call = function () {
            $location.url("app/MstFollowUpCallSummary");
        }
        $scope.rejected_calls = function () {
            $location.url("app/MstRejectedSummary");
        }

        $scope.transfer = function (inboundcall_gid, ticketref_no, assigned_to) {

            var modalInstance = $modal.open({
                templateUrl: '/transferContent.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });

            ModalInstanceCtrl.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', '$route', 'ngTableParams'];
            function ModalInstanceCtrl($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, $route, ngTableParams) {
                var url = 'api/OsdTrnCustomerQueryMgmt/TransferEmployee';
                SocketService.get(url).then(function (resp) {
                    $scope.TransferEmployeeList = resp.data.TransferEmployeeList;
                    unlockUI();
                });

                $scope.inboundcall_gid = inboundcall_gid;
                $scope.ticketref_no = ticketref_no;
                $scope.assigned_to = assigned_to;

                $scope.transfercall = function () {

                    if ($scope.transfer_to == undefined) {
                        modalInstance.close('closed');
                        Notify.alert('Kindly Select the Assign to Person', 'warning');
                        return;
                    }

                    var params = {
                        inboundcall_gid: $scope.inboundcall_gid,
                        employee_gid: $scope.transfer_to,
                        employee_name: $('#transfer_to :selected').text(),
                        transfer_remarks: $scope.transfer_remarks
                    }

                    var url = "api/MstTelecalling/TicketTransfer";
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status = true) {
                            modalInstance.close('closed');
                            Notify.alert(resp.data.message, 'success');
                            activate();
                        }
                        else {
                            modalInstance.close('closed');
                            Notify.alert(resp.data.message, 'warning');
                            activate();
                        }
                    });


                }
                /* var url = 'api/MstTelecalling/TransferLog';
                 var params = {
                     inboundcall_gid:inboundcall_gid
                 }
                 SocketService.getparams(url, params).then(function (resp) {
                         $scope.TransferLog_list = resp.data.transferLog_list;
                 }); */
                $scope.close = function () {
                    modalInstance.close('closed');
                };
            }
        }
    }
})();