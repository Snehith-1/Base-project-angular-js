(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstMyDisbursementSummaryController', MstMyDisbursementSummaryController);

    MstMyDisbursementSummaryController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams'];

    function MstMyDisbursementSummaryController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstMyDisbursementSummaryController';

        activate();
        lockUI();
        function activate() {
            var url = 'api/MstCreditOpsApplication/GetDisbursementMakerSummary';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.disbursementmeker_list = resp.data.disbursementmeker_list;
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

        $scope.makerPendingSummary = function () {
            var url = 'api/MstCreditOpsApplication/GetDisbursementMakerSummary';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.disbursementmeker_list = resp.data.disbursementmeker_list;
            });
            var url = 'api/MstCreditOpsApplication/DisbursementCount';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.makerpending_count = resp.data.makerpending_count;
                $scope.makerfollowup_count = resp.data.makerfollowup_count;
                $scope.checker_count = resp.data.checker_count;
                $scope.approvedcompleted_count = resp.data.approvedcompleted_count;
            });
        }

        $scope.makerfollowupSummary = function () {
            var url = 'api/MstCreditOpsApplication/GetDisbursementFollowupMakerSummary';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.disbursementmekerfollowup_list = resp.data.disbursementmekerfollowup_list;
            });
            var url = 'api/MstCreditOpsApplication/DisbursementCount';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.makerpending_count = resp.data.makerpending_count;
                $scope.makerfollowup_count = resp.data.makerfollowup_count;
                $scope.checker_count = resp.data.checker_count;
                $scope.approvedcompleted_count = resp.data.approvedcompleted_count;
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
            $location.url('app/MstCadApplicationView?application_gid=' + val + '&lspage=MyDisbursement');
        }

        $scope.maker_process = function (application_gid, application2sanction_gid, application2loan_gid, customer_urn, rmdisbursementrequest_gid, lsareference_gid) {
            $location.url('app/MstDisbursementMaker?application_gid=' + application_gid + '&application2sanction_gid=' + application2sanction_gid + '&application2loan_gid=' + application2loan_gid + '&customer_urn=' + customer_urn + '&rmdisbursementrequest_gid=' + rmdisbursementrequest_gid + '&lsareference_gid=' + lsareference_gid);
        }

        $scope.makerfollowup_process = function (application_gid, application2sanction_gid, application2loan_gid, customer_urn, rmdisbursementrequest_gid, lsareference_gid) {
            $location.url('app/MstApprovedDisbursementView?application_gid=' + application_gid + '&application2sanction_gid=' + application2sanction_gid + '&application2loan_gid=' + application2loan_gid + + '&customer_urn=' + customer_urn + '&rmdisbursementrequest_gid=' + rmdisbursementrequest_gid + '&lsareference_gid=' + lsareference_gid + '&lspage=followupmaker');
        }
    }
})();
