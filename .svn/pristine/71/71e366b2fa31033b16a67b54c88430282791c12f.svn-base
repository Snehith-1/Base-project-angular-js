(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstCompletedMarketingSummaryController', MstCompletedMarketingSummaryController);

    MstCompletedMarketingSummaryController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams', 'cmnfunctionService'];

    function MstCompletedMarketingSummaryController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams, cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstCompletedMarketingSummaryController';

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

            var url = 'api/Marketing/GetCompletedCallSummary';
            SocketService.get(url).then(function (resp) {
                $scope.marketingcompletedcall_list = resp.data.MarketingCall_list;
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
            $location.url('app/MstAssignedMarketingView?hash=' + cmnfunctionService.encryptURL('marketingcall_gid=' + marketingcall_gid + '&lspage=CompletedMarketing'));
        }
        $scope.rejected_call = function () {
            $location.url("app/MstRejectedMarketingSummary");
        }

    }
})();
