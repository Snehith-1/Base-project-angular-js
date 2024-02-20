(function () {
    'use strict';

    angular
        .module('angle')
        .controller('osdCqmQueryAssignmentController', osdCqmQueryAssignmentController);

    osdCqmQueryAssignmentController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', '$route', 'ngTableParams','$sce','cmnfunctionService'];

    function osdCqmQueryAssignmentController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, $route, ngTableParams,$sce,cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'osdCqmQueryAssignmentController';

        activate();
        function activate() {           
            var url = 'api/OsdTrnCustomerQueryMgmt/CustomerQueryPendingSummary';
            lockUI(); 
            SocketService.get(url).then(function (resp) {
                $scope.CustomerQuerypending_list = resp.data.QueryPendingList;
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

        $scope.Refresh = function () {  
            activate();
        }

        $scope.Assign = function (email_gid) {  
           
            var params = {
                email_gid: email_gid
            }
            var url = 'api/OsdTrnCustomerQueryMgmt/MailSeen';
            SocketService.getparams(url, params).then(function (resp) {
            });        
            $location.url('app/osdCqmQueryTicketAssignment?hash=' + cmnfunctionService.encryptURL('email_gid=' + email_gid ))
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
        
      
    }
})();