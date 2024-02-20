(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstTeleCallingSummaryController', MstTeleCallingSummaryController);

    MstTeleCallingSummaryController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams', 'cmnfunctionService'];

    function MstTeleCallingSummaryController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams, cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstTeleCallingSummaryController';
        activate();
        lockUI();
        function activate() {
           
            var url = "api/TeleCalling/IBCallCount";
            SocketService.get(url).then(function (resp) {
                $scope.assignedcall_count = resp.data.assignedcall_count;
                $scope.completedcall_count = resp.data.completedcall_count;
                $scope.followupcall_count = resp.data.followupcall_count;
                $scope.closedcall_count = resp.data.closedcall_count;
                $scope.rejectedcall_count = resp.data.rejectedcall_count;
                unlockUI();
            });

            var url = 'api/TeleCalling/GetIBCallSummary';
            SocketService.get(url).then(function (resp) {
                $scope.ibcall_list = resp.data.ibcall_list;
                unlockUI();
            });
        }
        $scope.addinbound = function () {
            $location.url("app/MstInboundAdd");
        }
        $scope.closedcall= function() {
            $location.url("app/MstTeleCallingClosedCall");
        }
        $scope.followupcall= function() {
            $location.url("app/MstTeleCallingFollowupCall");
        }
        $scope.assignedcall= function() {
            $location.url("app/MstTelecallingSummary");
        }
        $scope.completedcall= function() {
            $location.url("app/MstTeleCallingCompletedCall");
        }

        $scope.view_inboundcall = function (inboundcall_gid) {
            $location.url('app/MstTeleCallingAssignView?hash=' + cmnfunctionService.encryptURL('lsinboundcall_gid=' + inboundcall_gid + '&lspage=InboundAddCall'));
        }
        $scope.edit_inboundcall = function (inboundcall_gid) {
            $location.url('app/MstInboundEdit?hash=' + cmnfunctionService.encryptURL('lsinboundcall_gid=' + inboundcall_gid));
        }

        $scope.rejectedcall = function () {
            $location.url("app/MstRejectedCallSummary");
        }

        $scope.transfer = function (inboundcall_gid) {

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
                    inboundcall_gid: inboundcall_gid
                }
                var url = 'api/TeleCalling/IBCallDetailsForTransfer';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.inboundcall_gid = resp.data.inboundcall_gid;
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
                        inboundcall_gid: $scope.inboundcall_gid,
                        ticket_refid: $scope.ticket_refid,
                        transferfrom_gid: $scope.assignemployee_gid,
                        transferfrom_name: $scope.assignemployee_name,
                        transferto_gid: $scope.cboTransferTo,
                        transferto_name: $('#transfer_to :selected').text(),                       
                        transfer_remarks: $scope.transfer_remarks
                    }

                    var url = "api/TeleCalling/IBCallTransferEmployee";
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
        $scope.closed_call = function (inboundcall_gid) {

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
                    inboundcall_gid: inboundcall_gid
                }
                
                $scope.close_call = function () {                   
                    var params = {
                        inboundcall_gid: inboundcall_gid,                       
                        closed_remarks: $scope.closed_remarks
                    }

                    var url = "api/TeleCalling/IBCallAssignedClosed";
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