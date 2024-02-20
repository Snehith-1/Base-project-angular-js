(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstClosedMarketingSummaryController', MstClosedMarketingSummaryController);

    MstClosedMarketingSummaryController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams', 'cmnfunctionService'];

    function MstClosedMarketingSummaryController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams, cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstClosedMarketingSummaryController';

        activate();

        function activate() {
            var url = "api/Marketing/MarketingAssignedCallCount";
            SocketService.get(url).then(function (resp) {
                $scope.assignedcall_count = resp.data.assignedcall_count;
                $scope.completedcall_count = resp.data.completedcall_count;
                $scope.followupcall_count = resp.data.followupcall_count;
                $scope.closedcall_count = resp.data.closedcall_count;
                $scope.rejectedcall_count = resp.data.rejectedcall_count;
                unlockUI();
            });
            var url = 'api/Marketing/GetClosedCallSummary';
            SocketService.get(url).then(function (resp) {
                $scope.marketingclosedcall_list = resp.data.MarketingCall_list;
                unlockUI();
            });
        }
       
        $scope.closed_call = function () {
            $location.url("app/MstClosedMarketingSummary");
        }
        $scope.followup_call = function () {
            $location.url("app/MstFollowUpMarketingSummary");
        }
        $scope.assigned_call = function () {
            $location.url("app/MstAssignedMarketingSummary");
        }
        $scope.completed_call = function () {
            $location.url("app/MstCompletedMarketingSummary");
        }
       
        $scope.view = function (marketingcall_gid) {
            $location.url('app/MstAssignedMarketingView?hash=' + cmnfunctionService.encryptURL('marketingcall_gid=' + marketingcall_gid + '&lspage=ClosedMarketing'));
        }

        $scope.rejected_call = function () {
            $location.url("app/MstRejectedMarketingSummary");
        }

    }
})();
