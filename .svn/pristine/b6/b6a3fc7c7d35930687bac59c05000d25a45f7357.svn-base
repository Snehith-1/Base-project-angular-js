(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstHRLoanRaiseRequestCompletedController', MstHRLoanRaiseRequestCompletedController);

        MstHRLoanRaiseRequestCompletedController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams','cmnfunctionService'];

    function MstHRLoanRaiseRequestCompletedController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams,cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstHRLoanRaiseRequestCompletedController';

        activate();
        lockUI();
        function activate() {
            var url = 'api/MstHRLoanRequestCompleted/GetHRloanDetailsCompleted';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.hrloanrequestSummary_list = resp.data.hrloanrequestCompleted;               
                unlockUI();
            });
            var url = 'api/MstHRLoanRequest/GetHRloanDetailscount';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.lspendingrequests_count = resp.data.pendingrequests_count;
                $scope.lscompletedrequests_count = resp.data.completedrequests_count;
                $scope.lsrejectedrequests_count = resp.data.rejectedrequests_count;
                $scope.lswithdrawn_count = resp.data.withdrawn_count;                               
                $scope.lstotalcount = resp.data.totalcount;
          }); 
        }

        $scope.PendingRequests = function () {
            $state.go('app.MstHRLoanRaiseRequest');
        }

        $scope.completed_Requests = function () {
            $state.go('app.MstHRLoanRaiseRequestCompleted');
        }

        $scope.Rejected_Requests = function(){
            $state.go('app.MstHRLoanRaiseRequestRejected');
        }

        $scope.withdrawn_Requests = function () {
            $state.go('app.MstHRLoanRaiseRequestWithdrawn');
        } 
       
        $scope.viewrequests = function (request_gid) {
            $location.url('app/MstHRLoanViewRequest?hash=' + cmnfunctionService.encryptURL('request_gid=' + request_gid  ));
        }
        // $scope.viewrequests = function () {           
        //     $state.go('app.MstHRLoanViewRequest');
        // }
      
        $scope.addrequest = function () {
            $state.go('app.MstHRLoanAddRequest');

        }
    }
})();
