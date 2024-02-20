(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstRejectedMarketingSummaryController', MstRejectedMarketingSummaryController);

    MstRejectedMarketingSummaryController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams'];

    function MstRejectedMarketingSummaryController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstRejectedMarketingSummaryController';

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

            var url = 'api/Marketing/GetRejectedCallSummary';
            SocketService.get(url).then(function (resp) {
                $scope.ibrejectedcall_list = resp.data.MarketingCall_list;
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
            $location.url('app/MstMarketingManageRejectedCallView?marketingcall_gid=' + marketingcall_gid);
        }
        $scope.rejected_call = function () {
            $location.url("app/MstRejectedMarketingSummary");
        }
    }
})();
