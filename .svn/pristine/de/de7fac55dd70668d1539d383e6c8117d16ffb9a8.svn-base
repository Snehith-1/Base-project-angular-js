(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstRejectedCallSummaryController', MstRejectedCallSummaryController);

    MstRejectedCallSummaryController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams', 'cmnfunctionService'];

    function MstRejectedCallSummaryController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams, cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstRejectedCallSummaryController';
        activate();
        function activate() {
            var url = "api/TeleCalling/IBCallCount";
            SocketService.get(url).then(function (resp) {
                $scope.assignedcall_count = resp.data.assignedcall_count;
                $scope.completedcall_count = resp.data.completedcall_count;
                $scope.followupcall_count = resp.data.followupcall_count;
                $scope.closedcall_count = resp.data.closedcall_count;
                $scope.rejectedcall_count = resp.data.rejectedcall_count;
                unlockUI();
            });

            var url = 'api/TeleCalling/GetRejectedIBCallSummary';
            SocketService.get(url).then(function (resp) {
                $scope.ibrejectedcall_list = resp.data.ibcall_list;
                unlockUI();
            });
        }
        $scope.addinbound = function () {
            $location.url("app/MstInboundAdd");
        }
        $scope.closedcall = function () {
            $location.url("app/MstTeleCallingClosedCall");
        }
        $scope.followupcall = function () {
            $location.url("app/MstTeleCallingFollowupCall");
        }
        $scope.assignedcall = function () {
            $location.url("app/MstTelecallingSummary");
        }
        $scope.completedcall = function () {
            $location.url("app/MstTeleCallingCompletedCall");
        }

        $scope.view = function (inboundcall_gid) {
            $location.url('app/MstRejectedCallView?hash=' + cmnfunctionService.encryptURL('inboundcall_gid=' + inboundcall_gid));
        }

        $scope.rejectedcall = function () {
            $location.url("app/MstRejectedCallSummary");
        }
    }
})();
