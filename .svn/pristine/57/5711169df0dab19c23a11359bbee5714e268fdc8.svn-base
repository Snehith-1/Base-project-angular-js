(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstMarketingMyleadsClosedCallController', MstMarketingMyleadsClosedCallController);

    MstMarketingMyleadsClosedCallController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams', 'cmnfunctionService'];

    function MstMarketingMyleadsClosedCallController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams, cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstMarketingMyleadsClosedCallController';
        activate();
        lockUI();
        function activate() {
            var url = 'api/Marketing/GetClosedMyleadsMarketingCallSummary';
            SocketService.get(url).then(function (resp) {
                $scope.ibcall_list = resp.data.MarketingCall_list;
                unlockUI();
            });           

            var url = "api/Marketing/EmployeeMarketingCallCount";
            SocketService.get(url).then(function (resp) {
                $scope.assignedcall_count = resp.data.assignedcall_count;
                $scope.transfercall_count = resp.data.transfercall_count;
                $scope.completedcall_count = resp.data.completedcall_count;
                $scope.inprogresscall_count = resp.data.inprogresscall_count;
                $scope.tagmember_count = resp.data.taggedcall_count;
                $scope.followupcall_count = resp.data.followupcall_count;
                $scope.rejectedcall_count = resp.data.rejectedcall_count; 
                $scope.closedcall_count = resp.data.closedcall_count;
                unlockUI();
            });

        }
        $scope.myassigned_calls = function () {
            $location.url("app/MstMarketingMyAssignedCallSummary");
        }
        $scope.tag_member = function () {
            $location.url("app/MstMarketingTaggedMemberSummary");
        }
        $scope.transfer_calls = function () {
            $location.url("app/MstMarketingTransferCallSummary");
        }
        $scope.completed_calls = function () {
            $location.url("app/MstMarketingCompletedCallSummary");
        }
        $scope.call_response = function (marketingcall_gid) {
            $location.url("app/MstMarketingCallResponse?hash=" + cmnfunctionService.encryptURL("marketingcall_gid=" + marketingcall_gid));
        }
        $scope.view = function (marketingcall_gid) {
            $location.url("app/MstMarketingAssignedFollowupLeadsView?hash=" + cmnfunctionService.encryptURL("marketingcall_gid=" + marketingcall_gid));
        }
        $scope.edit = function (marketingcall_gid) {
            $location.url("app/MstMarketingEdit?hash=" + cmnfunctionService.encryptURL("marketingcall_gid=" + marketingcall_gid));
        }
        $scope.work_inprogress = function () {
            $location.url("app/MstMarketingWorkInprogressCallSummary");
        }
        $scope.followup_call = function () {
            $location.url("app/MstMarketingFollowUpCallSummary");
        }
        $scope.mbdrejected_calls = function () {
            $location.url("app/MstMarketingMBDRejectedCallSummary");
        }
        $scope.closedcall= function() {
            $location.url("app/MstMarketingMyleadsClosedCall");
        }
      
        $scope.edit_inboundcall = function (marketingcall_gid) {
            $location.url("app/MstMarketingEdit?hash=" + cmnfunctionService.encryptURL("lsmarketingcall_gid=" + marketingcall_gid));
        }
        $scope.view_inboundcall = function (marketingcall_gid) {
            $location.url("app/MstMarketingClosedView?hash=" + cmnfunctionService.encryptURL("lsmarketingcall_gid=" + marketingcall_gid + '&lspage=MarketingCloseMyLead'));
        }
        $scope.rejectedcall = function () {
            $location.url("app/MstMarketingRejectedCallSummary");
        }
    }
})();