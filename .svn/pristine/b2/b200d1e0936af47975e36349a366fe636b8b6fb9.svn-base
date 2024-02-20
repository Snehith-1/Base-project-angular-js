(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstAssignedDisbursementSummaryController', MstAssignedDisbursementSummaryController);

    MstAssignedDisbursementSummaryController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams'];

    function MstAssignedDisbursementSummaryController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstAssignedDisbursementSummaryController';

        activate();

        function activate() {
            var url = 'api/MstCreditOpsApplication/GetDisbursementAssignedSummary';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.disbursementassigned_list = resp.data.disbursementassigned_list;
            });

            var url = 'api/MstCreditOpsApplication/DisbursementAssignCount';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.pending_count = resp.data.pending_count;
                $scope.assigned_count = resp.data.assigned_count;
                $scope.lstotalcount = resp.data.lstotalcount;
                $scope.rejected_count = resp.data.rejected_count;
            });
        }

        $scope.assigned_disbursement = function () {
            $location.url('app/MstAssignedDisbursementSummary');
        }

        $scope.pending_disbursement = function () {
            $location.url('app/MstDisbursementAssignmentSummary');
        }

        $scope.view = function (val) {
            $location.url('app/MstCadApplicationView?application_gid=' + val + '&lspage=AssignedDisbursement');
        }

        $scope.rejected_disbursement = function () {
            $location.url('app/MstDisbursementRejectedSummary');
        }

    }
})();
