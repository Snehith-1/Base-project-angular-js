(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstMarketingRejectedCallSummaryController', MstMarketingRejectedCallSummaryController);

    MstMarketingRejectedCallSummaryController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams'];

    function MstMarketingRejectedCallSummaryController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstMarketingRejectedCallSummaryController';
        activate();
        function activate() {
            var url = "api/Marketing/MarketingCallCount";
            SocketService.get(url).then(function (resp) {
                $scope.unassignedcall_count = resp.data.unassignedcall_count;
                $scope.assignedcall_count = resp.data.assignedcall_count;
                $scope.completedcall_count = resp.data.completedcall_count;
                $scope.followupcall_count = resp.data.followupcall_count;
                $scope.closedcall_count = resp.data.closedcall_count;
                $scope.rejectedcall_count = resp.data.rejectedcall_count;
                unlockUI();
            });

            var url = 'api/Marketing/GetRejectedMarketingCallSummary';
            SocketService.get(url).then(function (resp) {
                $scope.ibrejectedcall_list = resp.data.MarketingCall_list;
                unlockUI();
            });
        }
        $scope.addinbound = function () {
            $location.url("app/MstMarketingAdd");
        }
        $scope.closedcall = function () {
            $location.url("app/MstMarketingClosedCall");
        }
        $scope.followupcall = function () {
            $location.url("app/MstMarketingFollowupCall");
        }
        $scope.assignedcall = function () {
            $location.url("app/MstMarketingSummary");
        }
        $scope.unassignedcall= function() {
            $location.url("app/MstMarketingUnassignedLeadSummary");
        }
        $scope.completedcall = function () {
            $location.url("app/MstMarketingCompletedCall");
        }

        $scope.view = function (marketingcall_gid) {
            $location.url('app/MstMarketingRejectedCallView?marketingcall_gid=' + marketingcall_gid);
        }

        $scope.rejectedcall = function () {
            $location.url("app/MstMarketingRejectedCallSummary");
        }
    }
})();
