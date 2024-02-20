(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstMyDisbursementCompletedSummaryController', MstMyDisbursementCompletedSummaryController);

    MstMyDisbursementCompletedSummaryController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams'];

    function MstMyDisbursementCompletedSummaryController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstMyDisbursementCompletedSummaryController';

        activate();
        lockUI();
        function activate() {
            var url = 'api/MstCreditOpsApplication/GetDisbursementCompletedSummary';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.disbursementcompleted_list = resp.data.disbursementcompleted_list;
            });
            var url = 'api/MstCreditOpsApplication/DisbursementCount';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.makerpending_count = resp.data.makerpending_count;
                $scope.makerfollowup_count = resp.data.makerfollowup_count;
                $scope.checker_count = resp.data.checker_count;
                $scope.approvedcompleted_count = resp.data.approvedcompleted_count;
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

        $scope.maker = function () {
            $location.url('app/MstMyDisbursementSummary');
        }

        $scope.checker = function () {
            $location.url('app/MstMyDisbursementCheckerSummary');
        }

        $scope.approvalcompleted = function () {
            $location.url('app/MstMyDisbursementCompletedSummary');
        }

        $scope.disbursementrejected = function () {
            $location.url('app/MstCreditOpsDisbursementRejectedSummary');
        }

        $scope.view = function (val) {
            $location.url('app/MstCadApplicationView?application_gid=' + val + '&lspage=MyDisbursementCompleted');
        }

        $scope.completed_process = function (application_gid, application2sanction_gid, application2loan_gid, customer_urn, rmdisbursementrequest_gid, lsareference_gid) {
            $location.url('app/MstApprovedDisbursementView?application_gid=' + application_gid + '&application2sanction_gid=' + application2sanction_gid + '&application2loan_gid=' + application2loan_gid + '&customer_urn=' + customer_urn + '&rmdisbursementrequest_gid=' + rmdisbursementrequest_gid + '&lsareference_gid=' + lsareference_gid + '&lspage=MyDisbursementCompleted');
        }
    }
})();
