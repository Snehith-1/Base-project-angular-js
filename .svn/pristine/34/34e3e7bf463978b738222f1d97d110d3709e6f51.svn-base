(function () {
    'use strict';

    angular
        .module('angle')
        .controller('osdCqmAssignCloseSummaryController', osdCqmAssignCloseSummaryController);

        osdCqmAssignCloseSummaryController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', '$route', 'ngTableParams', '$sce','cmnfunctionService'];

    function osdCqmAssignCloseSummaryController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, $route, ngTableParams, $sce,cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'osdCqmAssignCloseSummaryController';

        activate();
        function activate() {

            var url = 'api/OsdTrnCustomerQueryMgmt/CustomerAssignQueryCloseSummary';
            lockUI(); 
            SocketService.get(url).then(function (resp) {
                $scope.QueryAssignedCloseList = resp.data.QueryAssignedCloseList;
                unlockUI();
            });

            var url = "api/OsdTrnCustomerQueryMgmt/AssignedQueryCount";
            SocketService.get(url).then(function (resp) {
                $scope.assigned_count = resp.data.assigned_count;
                $scope.reply_count = resp.data.reply_count;
                $scope.forward_count = resp.data.forward_count;
                $scope.transfer_count = resp.data.transfer_count;
                $scope.close_count = resp.data.close_count;
                unlockUI();
            });
        }
        
        $scope.CloseView = function (email_gid) {
       
            $location.url('app/osdCqmAssignCloseView?hash=' + cmnfunctionService.encryptURL('email_gid=' + email_gid));
        }

        $scope.Assigned = function () {
            $state.go('app.osdCqmAssignedQuery');
        }
        
        $scope.Closed = function () {
            $state.go('app.osdCqmAssignCloseSummary');
        }

        $scope.Transfer = function () {
            $state.go('app.osdCqmTransferSummary');
        }

        $scope.Replay = function () {
            $state.go('app.osdCqmReplaySummary');
        }

        $scope.Forward = function () {
            $state.go('app.osdCqmForwardSummary');
        }
        $scope.ClosedView = function () {
            $state.go('app.osdCqmAssignCloseView');
        }
    }
})();
