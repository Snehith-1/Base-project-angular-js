(function () {
    'use strict';

    angular
        .module('angle')
        .controller('osdTrnTicketForwardSummaryController', osdTrnTicketForwardSummaryController);

    osdTrnTicketForwardSummaryController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', '$route', 'ngTableParams','cmnfunctionService'];

    function osdTrnTicketForwardSummaryController($rootScope, $scope, $modal, $state, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, $route, ngTableParams,cmnfunctionService) {

        /* jshint validthis:true */
        var vm = this;
        vm.title = 'osdTrnTicketForwardSummaryController';

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
                unlockUI();
            });

            var url = 'api/OsdTrnTicketManagement/GetServiceRequestSummary';
            SocketService.get(url).then(function (resp) {
                $scope.count_newticket = resp.data.lsallotted_count;

                unlockUI();
            });
           
            var url = "api/OsdTrnTicketManagement/GetAllForwardSummary";
            SocketService.get(url).then(function (resp) {
                $scope.forwardactivitysummary = resp.data.forwarddtl;                
                unlockUI();
            });
            var url = 'api/OsdTrnBankAlert/GetBankalertNotification';
            SocketService.get(url).then(function (resp) {
               
                if (resp.data.display == "true") {
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
        $scope.RejectCancel = function () {
            $state.go('app.osdTrnTicketRejectCancelSummary');
        }
        $scope.Close = function () {
            $state.go('app.osdTrnTicketClosedSummary');
        }
        
        $scope.BankAlert = function () {
            $state.go('app.OsdTrnBankAlertManagementSummary');
        }

        $scope.viewforward = function (val, val2, val3, val4,val7) {
          
            $location.url('app/osdTrnActivityManagement360?hash=' + cmnfunctionService.encryptURL('servicerequest_gid=' + val + '&bankalert_flag=' + val2 + '&bankalert2allocated_gid=' + val3 + '&customer_gid=' + val4 + '&lspage=Forward-Ticket' + '&request_refno=' + val7));
           
        }

    }
})();
