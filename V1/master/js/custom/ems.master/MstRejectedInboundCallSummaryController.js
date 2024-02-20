(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstRejectedInboundCallSummaryController', MstRejectedInboundCallSummaryController);

    MstRejectedInboundCallSummaryController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams', 'cmnfunctionService'];

    function MstRejectedInboundCallSummaryController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams, cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstRejectedInboundCallSummaryController';

        activate();

        function activate() {
            var url = "api/TeleCalling/IBAssignedCallCount";
            SocketService.get(url).then(function (resp) {
                $scope.assignedcall_count = resp.data.assignedcall_count;
                $scope.completedcall_count = resp.data.completedcall_count;
                $scope.followupcall_count = resp.data.followupcall_count;
                $scope.closedcall_count = resp.data.closedcall_count;
                $scope.rejectedcall_count = resp.data.rejectedcall_count;
                unlockUI();
            });

            var url = 'api/TeleCalling/GetRejectedCallSummary';
            SocketService.get(url).then(function (resp) {
                $scope.ibrejectedcall_list = resp.data.ibcall_list;
                unlockUI();
            });
        }


        $scope.closed_call = function () {
            $location.url("app/MstClosedInboundCallSummary");
        }
        $scope.followup_call = function () {
            $location.url("app/MstFollowUpInboundCallSummary");
        }
        $scope.assigned_call = function () {
            $location.url("app/MstAssignedInboundCallSummary");
        }
        $scope.completed_call = function () {
            $location.url("app/MstCompletedInboundCallSummary");
        }

        $scope.view = function (inboundcall_gid) {
            $location.url('app/MstAssignedInboundCallView?hash=' + cmnfunctionService.encryptURL('inboundcall_gid=' + inboundcall_gid + '&lspage=RejectedInboundCall'));
        }
        $scope.rejected_call = function () {
            $location.url("app/MstRejectedInboundCallSummary");
        }
    }
})();
