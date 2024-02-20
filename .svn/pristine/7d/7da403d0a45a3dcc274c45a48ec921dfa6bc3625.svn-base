(function () {
    'use strict';
    angular
        .module('angle')
        .controller('osdTrnCancelledRequestSummaryController', osdTrnCancelledRequestSummaryController);

        osdTrnCancelledRequestSummaryController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams','cmnfunctionService'];

    function osdTrnCancelledRequestSummaryController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams,cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'osdTrnCancelledRequestSummaryController';
        var lstab = $location.search().lstab;
        activate();
        lockUI();
        function activate() {

            var url = "api/OsdTrnServiceRequest/GetCancelledSummary";
            SocketService.get(url).then(function (resp) {
                $scope.cancelledsummary = resp.data.cancelledlist;
                unlockUI();
            });
                      

            var url = "api/OsdTrnServiceRequest/GetServiceRequestCount";
            SocketService.get(url).then(function (resp) {
                $scope.request_count = resp.data.request_count;
                $scope.tagged_count = resp.data.tagged_count;
                $scope.forward_count = resp.data.forward_count;
                $scope.reopen_count = resp.data.reopen_count;
                $scope.close_count = resp.data.close_count;
                $scope.Reject_count = resp.data.reject_count;
                $scope.cancel_count = resp.data.cancel_count;
                
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
       
        $scope.raiserequest = function () {
            $location.url('app/osdTrnServiceRequestAdd?hash=' + cmnfunctionService.encryptURL('lspage=myrequest'));
        }
        $scope.viewservicerequest = function (servicerequest_gid, request_status) {

            $location.url('app/osdTrnServiceRequestView?hash=' + cmnfunctionService.encryptURL('servicerequest_gid=' + servicerequest_gid + '&lspage=cancelledrequest'));
        }

    }
})();