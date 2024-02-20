(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AgrMstCreditRevokeSummaryController', AgrMstCreditRevokeSummaryController);

    AgrMstCreditRevokeSummaryController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', '$sce', 'DownloaddocumentService','cmnfunctionService'];

    function AgrMstCreditRevokeSummaryController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, $sce, DownloaddocumentService, cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AgrMstCreditRevokeSummaryController';

        activate();
        lockUI();
        function activate() {
            var url = 'api/AgrAdminApplication/GetCreditRejectedApplSummary';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.creditrejectedappl_list = resp.data.creditrejectedappl_list;
            });

            var url = 'api/AgrAdminApplication/CreditApplicationRevokeCount';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.creditreject_count = resp.data.creditreject_count;
                $scope.credithold_count = resp.data.credithold_count;
                $scope.creditrevoked_count = resp.data.creditrevoked_count;
                $scope.creditmanagerreject_count = resp.data.creditmanagerreject_count;
                $scope.lstotalcount = resp.data.lstotalcount;
            });
        }

        $scope.applcreation_view = function (application_gid) {
            $location.url('app/AgrMstCcCommitteeApplicationView?application_gid=' + application_gid + '&lstab=CreditRejectRevokeAppl');
        }

        $scope.Rejected_revoke = function (application_gid, created_by) {
        //    $location.url('app/AgrMstCreditRejectHoldRevoke?application_gid=' + application_gid + '&employee_gid=' + created_by + '&lspage=CreditRejectRevokeAppl');
            $location.url("app/AgrMstCreditRejectHoldRevoke?hash=" + cmnfunctionService.encryptURL("application_gid=" + application_gid + '&employee_gid=' + created_by + '&lspage=CreditRejectRevokeAppl'));

        }

        $scope.revoke_history = function (application_gid) {
        //    $location.url('app/AgrMstCreditRejectHoldRevokeHistory?application_gid=' + application_gid + '&lspage=CreditRejectRevokeAppl');
            $location.url("app/AgrMstCreditRejectHoldRevokeHistory?hash=" + cmnfunctionService.encryptURL("application_gid=" + application_gid + '&lspage=CreditRejectRevokeAppl'));

        }

        $scope.hold_applications = function () {
            $state.go('app.AgrMstCreditHoldRevokeSummary');
        }

        $scope.rejected_applications = function () {
            $state.go('app.AgrMstCreditRevokeSummary');
        }

        $scope.business_revoked = function () {
            $state.go('app.AgrMstCreditRevokedApplSummary');
        }

        $scope.creditmanagerreject_applications = function () {
            $state.go('app.AgrMstCreditManagerRejectRevokeSummary');
        }

    }
})();
