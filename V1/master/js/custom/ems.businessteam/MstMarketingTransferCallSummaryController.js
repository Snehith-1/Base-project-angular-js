(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstMarketingTransferCallSummaryController', MstMarketingTransferCallSummaryController);

    MstMarketingTransferCallSummaryController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams', 'cmnfunctionService'];

    function MstMarketingTransferCallSummaryController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams, cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstMarketingTransferCallSummaryController';
        activate();

        function activate() {
            var url = 'api/Marketing/GetEmpTransferredMarketingCallSummary';
            SocketService.get(url).then(function (resp) {
                $scope.ibcalltransfer_list = resp.data.MarketingCall_list;
                unlockUI();
            });           

            var url = "api/Marketing/EmployeeMarketingCallCount";
            SocketService.get(url).then(function (resp) {
                $scope.assignedcall_count = resp.data.assignedcall_count;
                $scope.transfercall_count = resp.data.transfercall_count;
                $scope.completedcall_count = resp.data.completedcall_count;
                $scope.inprogresscall_count = resp.data.inprogresscall_count;
                $scope.tagmember_count = resp.data.taggedcall_count;
                $scope.followupcall_count = resp.data.followupcall_count;
                $scope.rejectedcall_count = resp.data.rejectedcall_count;
                $scope.closedcall_count = resp.data.closedcall_count;


                unlockUI();
            });
        }

        $scope.closedcall= function() {
            $location.url("app/MstMarketingMyleadsClosedCall");
        }
      
        $scope.myassigned_calls = function () {
            $location.url("app/MstMarketingMyAssignedCallSummary");
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
            $location.url("app/MstMarketingAssignedCallView?hash=" + cmnfunctionService.encryptURL("marketingcall_gid=" + marketingcall_gid + '&lspage=TransferCall'));
        }
        $scope.edit = function (marketingcall_gid) {
            $location.url("app/MstMarketingEdit?hash=" + cmnfunctionService.encryptURL("marketingcall_gid=" + marketingcall_gid));
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
        $scope.transfer = function (marketingcall_gid) {

            var modalInstance = $modal.open({
                templateUrl: '/transferContent.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });

            ModalInstanceCtrl.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', '$route', 'ngTableParams'];
            function ModalInstanceCtrl($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, $route, ngTableParams) {


                var params = {
                    marketingcall_gid: marketingcall_gid
                }
                var url = 'api/Marketing/MarketingCallDetailsForTransfer';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.marketingcall_gid = resp.data.marketingcall_gid;
                    $scope.ticket_refid = resp.data.ticket_refid;
                    $scope.assignemployee_gid = resp.data.assignemployee_gid;
                    $scope.assignemployee_name = resp.data.assignemployee_name;
                    $scope.ibcalltransfer_list = resp.data.MarketingCalltransfer_list;
                });

                var url = 'api/OsdTrnCustomerQueryMgmt/TransferEmployee';
                SocketService.get(url).then(function (resp) {
                    $scope.TransferEmployeeList = resp.data.TransferEmployeeList;
                    unlockUI();
                });

                /*      $scope.marketingcall_gid = marketingcall_gid;
                      $scope.ticketref_no = ticketref_no;
                      $scope.assigned_to = assigned_to; */

                $scope.transfer_call = function () {

                    if ($scope.cboTransferTo == undefined) {
                        modalInstance.close('closed');
                        Notify.alert('Kindly Select the Transfer to Person', 'warning');
                        return;
                    }

                    var params = {
                        marketingcall_gid: $scope.marketingcall_gid,
                        ticket_refid: $scope.ticket_refid,
                        transferfrom_gid: $scope.assignemployee_gid,
                        transferfrom_name: $scope.assignemployee_name,
                        transferto_gid: $scope.cboTransferTo,
                        transferto_name: $('#transfer_to :selected').text(),
                        transfer_remarks: $scope.transfer_remarks
                    }

                    var url = "api/Marketing/MarketingCallTransferEmployee";
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
                /*     var url = 'api/MstTelecalling/TransferLog';
                     var params = {
                         marketingcall_gid:marketingcall_gid
                     }
                     SocketService.getparams(url, params).then(function (resp) {
                             $scope.TransferLog = resp.data.TransferLog;
                     });  */
                $scope.close = function () {
                    modalInstance.close('closed');
                };
            }
        }
    }
})();