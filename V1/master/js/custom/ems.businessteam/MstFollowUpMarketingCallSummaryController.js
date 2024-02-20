(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstFollowUpMarketingSummaryController', MstFollowUpMarketingSummaryController);

    MstFollowUpMarketingSummaryController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams', 'cmnfunctionService'];

    function MstFollowUpMarketingSummaryController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams, cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstFollowUpMarketingSummaryController';

        activate();

        function activate() {
            var url = "api/Marketing/MarketingAssignedCallCount";
            SocketService.get(url).then(function (resp) {
                $scope.assignedcall_count = resp.data.assignedcall_count;
                $scope.completedcall_count = resp.data.completedcall_count;
                $scope.followupcall_count = resp.data.followupcall_count;
                $scope.closedcall_count = resp.data.closedcall_count;
                $scope.rejectedcall_count = resp.data.rejectedcall_count;
                unlockUI();
            });

            var url = 'api/Marketing/GetFollowUpCallSummary';
            SocketService.get(url).then(function (resp) {
                $scope.ibfollowupcall_list = resp.data.ibcall_list;
                unlockUI();
            });
        }


        $scope.closed_call = function () {
            $location.url("app/MstClosedMarketingSummary");
        }
        $scope.followup_call = function () {
            $location.url("app/MstFollowUpMarketingSummary");
        }
        $scope.assigned_call = function () {
            $location.url("app/MstAssignedMarketingSummary");
        }
        $scope.completed_call = function () {
            $location.url("app/MstCompletedMarketingSummary");
        }

        $scope.view = function (marketingcall_gid) {
            $location.url('app/MstAssignedMarketingView?hash=' + cmnfunctionService.encryptURL('marketingcall_gid=' + marketingcall_gid + '&lspage=FollowUpMarketing'));
        }

        $scope.rejected_call = function () {
            $location.url("app/MstRejectedRejectedCallSummary");
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
                    $scope.ibcalltransfer_list = resp.data.ibcalltransfer_list;
                });

                var url = 'api/OsdTrnCustomerQueryMgmt/TransferEmployee';
                SocketService.get(url).then(function (resp) {
                    $scope.TransferEmployeeList = resp.data.TransferEmployeeList;
                    unlockUI();
                });

                /*      $scope.inboundcall_gid = inboundcall_gid;
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
                         inboundcall_gid:inboundcall_gid
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
