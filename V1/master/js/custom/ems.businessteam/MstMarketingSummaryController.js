(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstMarketingSummaryController', MstMarketingSummaryController);

    MstMarketingSummaryController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams', 'cmnfunctionService'];

    function MstMarketingSummaryController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams, cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstMarketingSummaryController';
        var selectedIndex = $location.search().selectedIndex;
        activate();
        lockUI();
        function activate() {
           
            var url = "api/Marketing/MarketingCallCount";
            SocketService.get(url).then(function (resp) {
                $scope.unassignedcall_count = resp.data.unassignedcall_count;
                $scope.assignedcall_count = resp.data.assignedcall_count;
                $scope.completedcall_count = resp.data.completedcall_count;
                $scope.followupcall_count = resp.data.followupcall_count;
                $scope.closedcall_count = resp.data.closedcall_count;
                $scope.rejectedcall_count = resp.data.rejectedcall_count;
                unlockUI();
            });

            var url = 'api/Marketing/GetMarketingCallSummary';
            SocketService.get(url).then(function (resp) {
                $scope.ibcall_list = resp.data.MarketingCall_list;
                unlockUI();
            });
        }
        $scope.addinbound = function () {
            $location.url("app/MstMarketingAdd");
        }
        $scope.closedcall= function() {
            $location.url("app/MstMarketingClosedCall");
        }
        $scope.followupcall= function() {
            $location.url("app/MstMarketingFollowupCall");
        }
        $scope.assignedcall= function() {
            $location.url("app/MstMarketingSummary");
        }
        $scope.unassignedcall= function() {
            $location.url("app/MstMarketingUnassignedLeadSummary");
        }
        $scope.completedcall= function() {
            $location.url("app/MstMarketingCompletedCall");
        }
        //$scope.applcreation_view = function (application_gid) {
        //    $location.url('app/AgrMstCustomerOnboardingApproval?hash=' + cmnfunctionService.encryptURL('application_gid=' + application_gid + '&selectedIndex=' + $scope.selectedIndex + '&lsApp=N&FromRM=Y'));
        //}
        $scope.view_inboundcall = function (marketingcall_gid) {
            $location.url('app/MstMarketingAssignView?hash=' + cmnfunctionService.encryptURL('lsmarketingcall_gid=' + marketingcall_gid + '&lspage=MarketingAddCall'));
        }
        $scope.edit_inboundcall = function (marketingcall_gid) {
            $location.url('app/MstMarketingEdit?hash=' + cmnfunctionService.encryptURL('lsmarketingcall_gid=' + marketingcall_gid));
        }

        $scope.rejectedcall = function () {
            $location.url("app/MstMarketingRejectedCallSummary");
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
        $scope.closed_call = function (marketingcall_gid) {

            var modalInstance = $modal.open({
                templateUrl: '/closedContent.html',
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
                
                $scope.close_call = function () {                   
                    var params = {
                        marketingcall_gid: marketingcall_gid,                       
                        closed_remarks: $scope.closed_remarks
                    }

                    var url = "api/Marketing/MarketingCallAssignedClosed";
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
                
                $scope.close = function () {
                    modalInstance.close('closed');
                };
            }
        }
    }
})();