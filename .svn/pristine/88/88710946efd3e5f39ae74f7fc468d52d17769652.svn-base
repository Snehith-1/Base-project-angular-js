(function () {
    'use strict';

    angular
        .module('angle')
        .controller('osdTrnTaggedRequestSummaryController', osdTrnTaggedRequestSummaryController);

        osdTrnTaggedRequestSummaryController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams','cmnfunctionService'];

    function osdTrnTaggedRequestSummaryController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams,cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'osdTrnTaggedRequestSummaryController';
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
            
            var url = "api/OsdTrnServiceRequest/GetTaggedSummary";
            SocketService.get(url).then(function (resp) {
                $scope.taggedactivitysummary = resp.data.taggeddtl;                
                unlockUI();
            });
            var url = 'api/OsdTrnBankAlert/GetCompletedSummary';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.BankAlertCompleted_list = resp.data.BankAlertCompleted_list;
                unlockUI();
            });
        }
        

        $scope.raiserequest = function () {
            $location.url('app/osdTrnServiceRequestAdd?hash=' + cmnfunctionService.encryptURL('lspage=taggedrequest'));
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
      
        $scope.viewtaggedrequest = function (servicerequest_gid, request_status, val2, val3, val4) {

            var param = {
                servicerequest_gid: servicerequest_gid
            }
            var url = 'api/OsdTrnServiceRequest/GetServiceRequestTagViewUpdate';
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
                var  val = "Y";
                $location.url('app/osdTrnServiceRequestTaggedView?hash=' + cmnfunctionService.encryptURL('servicerequest_gid=' + servicerequest_gid + '&CompletedFlag=' + val + '&bankalert_flag=' + val2 + '&bankalert2allocated_gid=' + val3 + '&customer_gid=' + val4 + '&lspage=New'));
              
            }
            else if (request_status == 'Closed') {
                var val = "C";
                $location.url('app/osdTrnServiceRequestTaggedView?hash=' + cmnfunctionService.encryptURL('servicerequest_gid=' + servicerequest_gid + '&CompletedFlag=' + val + '&bankalert_flag=' + val2 + '&bankalert2allocated_gid=' + val3 + '&customer_gid=' + val4 + '&lspage=New'));
               
            }
            else {
                var val = "N";
                $location.url('app/osdTrnServiceRequestTaggedView?hash=' + cmnfunctionService.encryptURL('servicerequest_gid=' + servicerequest_gid + '&CompletedFlag=' + val + '&bankalert_flag=' + val2 + '&bankalert2allocated_gid=' + val3 + '&customer_gid=' + val4 + '&lspage=New'));
               
            }
           
           
        }      
  
    }
})();