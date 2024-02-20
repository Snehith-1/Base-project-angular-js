(function () {
    'use strict';

    angular
        .module('angle')
        .controller('osdCqmCloseSummaryController', osdCqmCloseSummaryController);

        osdCqmCloseSummaryController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', '$route', 'ngTableParams', '$sce','cmnfunctionService'];

    function osdCqmCloseSummaryController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, $route, ngTableParams, $sce,cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'osdCqmCloseSummaryController';

        activate();
        function activate() {
            var url = 'api/OsdTrnCustomerQueryMgmt/CustomerQueryCloseSummary';
            lockUI(); 
            SocketService.get(url).then(function (resp) {
                $scope.QueryCloseList = resp.data.QueryCloseList;
                unlockUI();
            });

            var url = "api/OsdTrnCustomerQueryMgmt/QueryAssignmentCount";
            SocketService.get(url).then(function (resp) {
                $scope.pending_count = resp.data.pending_count;
                $scope.assign_count = resp.data.assign_count;
                $scope.close_count = resp.data.close_count;
                unlockUI();
            });
        }
        
        $scope.Pending = function () {
            $state.go('app.osdCqmQueryAssignment');
        }

        $scope.Assigned = function (){
            $state.go('app.osdCqmAssignToQuery');
        }
        $scope.Close = function (){
            $state.go('app.osdCqmCloseSummary');
        }
        $scope.CloseView = function (email_gid) {
           
            $location.url('app/osdCqmCloseView?hash=' + cmnfunctionService.encryptURL('email_gid=' + email_gid));
        }
    }
})();
