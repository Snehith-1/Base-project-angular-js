
(function () {
    'use strict';


    angular
        .module('angle')
        .controller('osdTrnRejectedRequestSummaryController', osdTrnRejectedRequestSummaryController);

        osdTrnRejectedRequestSummaryController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams','cmnfunctionService'];

    function osdTrnRejectedRequestSummaryController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams,cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'osdTrnRejectedRequestSummaryController';
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

         
            var url = "api/OsdTrnServiceRequest/GetRejectedSummary";
            SocketService.get(url).then(function (resp) {
                $scope.rejectedsummary = resp.data.rejectedlist;
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
            $location.url('app/osdTrnServiceRequestAdd?hash=' + cmnfunctionService.encryptURL('lspage=rejectrequest'));
        }
        
        $scope.viewservicerequest = function (servicerequest_gid, request_status) {

            $location.url('app/osdTrnServiceRequestView?hash=' + cmnfunctionService.encryptURL('servicerequest_gid=' + servicerequest_gid  + '&lspage=rejectedrequest'));
        }
    }
})();