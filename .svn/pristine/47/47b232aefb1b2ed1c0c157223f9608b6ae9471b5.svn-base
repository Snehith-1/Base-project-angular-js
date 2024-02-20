(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstTeleCallingClosedCallController', MstTeleCallingClosedCallController);

    MstTeleCallingClosedCallController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams', 'cmnfunctionService'];

    function MstTeleCallingClosedCallController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams, cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstTeleCallingClosedCallController';
        activate();
        lockUI();
        function activate() {
            var url = 'api/TeleCalling/GetClosedIBCallSummary';
            SocketService.get(url).then(function (resp) {
                $scope.ibcall_list = resp.data.ibcall_list;
                unlockUI();
            });           

            var url = "api/TeleCalling/IBCallCount";
            SocketService.get(url).then(function (resp) {
                $scope.assignedcall_count = resp.data.assignedcall_count;
                $scope.completedcall_count = resp.data.completedcall_count;
                $scope.followupcall_count = resp.data.followupcall_count;
                $scope.closedcall_count = resp.data.closedcall_count;
                $scope.rejectedcall_count = resp.data.rejectedcall_count;
                unlockUI();
            });

        }
        $scope.addinbound = function () {
            $location.url("app/MstInboundAdd");
        }
        $scope.closedcall= function() {
            $location.url("app/MstTeleCallingClosedCall");
        }
        $scope.followupcall= function() {
            $location.url("app/MstTeleCallingFollowupCall");
        }
        $scope.assignedcall= function() {
            $location.url("app/MstTelecallingSummary");
        }
        $scope.completedcall= function() {
            $location.url("app/MstTeleCallingCompletedCall");
        }
        $scope.edit_inboundcall = function (inboundcall_gid) {
            $location.url('app/MstInboundEdit?hash=' + cmnfunctionService.encryptURL('lsinboundcall_gid=' + inboundcall_gid));
        }
        $scope.view_inboundcall = function (inboundcall_gid) {
            $location.url('app/MstTeleCallingClosedView?hash=' + cmnfunctionService.encryptURL('lsinboundcall_gid=' + inboundcall_gid));
        }
        $scope.rejectedcall = function () {
            $location.url("app/MstRejectedCallSummary");
        }
    }
})();