(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstMarketingMBDRejectedCallSummaryController', MstMarketingMBDRejectedCallSummaryController);

    MstMarketingMBDRejectedCallSummaryController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams'];

    function MstMarketingMBDRejectedCallSummaryController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstMarketingMBDRejectedCallSummaryController';
        activate();
        lockUI();
        function activate() {
            var url = 'api/Marketing/GetEmpRejectedMarketingCallSummary';
            SocketService.get(url).then(function (resp) {
                $scope.marketingcallcompleted_list = resp.data.MarketingCall_list;
                unlockUI();
            });

            var url = "api/Marketing/EmployeeMarketingCallCount";
            SocketService.get(url).then(function (resp) {
                $scope.assignedcall_count = resp.data.assignedcall_count;
                $scope.transfercall_count = resp.data.transfercall_count;
                $scope.completedcall_count = resp.data.completedcall_count;
                $scope.rejectedcall_count = resp.data.rejectedcall_count
                $scope.inprogresscall_count = resp.data.inprogresscall_count;
                $scope.tagmember_count = resp.data.taggedcall_count;
                $scope.followupcall_count = resp.data.followupcall_count;
                $scope.closedcall_count = resp.data.closedcall_count;
                unlockUI();
            });
        }
        $scope.myassigned_calls = function () {
            $location.url("app/MstMarketingMyAssignedCallSummary");
        }
        $scope.closedcall= function() {
            $location.url("app/MstMarketingMyleadsClosedCall");
        }
        $scope.tag_member = function () {
            $location.url("app/MstMarketingTaggedMemberSummary");
        }
        $scope.transfer_calls = function () {
            $location.url("app/MstMarketingTransferCallSummary");
        }
        $scope.completed_calls = function () {
            $location.url("app/MstMarketingCompletedCallSummary");
        }
        $scope.view = function (marketingcall_gid) {
            $location.url("app/MstMarketingMBDRejectedCallView?marketingcall_gid=" + marketingcall_gid);
        }
        $scope.edit = function (marketingcall_gid) {
            $location.url("app/MstMarketingEdit?marketingcall_gid=" + marketingcall_gid);
        }
        $scope.work_inprogress = function () {
            $location.url("app/MstMarketingWorkInprogressCallSummary");
        }
        $scope.followup_call = function () {
            $location.url("app/MstMarketingFollowUpCallSummary");
        }
        $scope.mbdrejected_calls = function () {
            $location.url("app/MstMarketingMBDRejectedCallSummary");
        }
        $scope.transfer = function (marketingcall_gid, ticketref_no, assigned_to) {

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

                $scope.marketingcall_gid = marketingcall_gid;
                $scope.ticketref_no = ticketref_no;
                $scope.assigned_to = assigned_to;

                $scope.transfercall = function () {

                    if ($scope.transfer_to == undefined) {
                        modalInstance.close('closed');
                        Notify.alert('Kindly Select the Assign to Person', 'warning');
                        return;
                    }

                    var params = {
                        marketingcall_gid: $scope.marketingcall_gid,
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