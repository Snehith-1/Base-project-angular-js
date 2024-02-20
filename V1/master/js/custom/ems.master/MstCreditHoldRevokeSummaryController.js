(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstCreditHoldRevokeSummaryController', MstCreditHoldRevokeSummaryController);

    MstCreditHoldRevokeSummaryController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', '$sce', 'DownloaddocumentService'];

    function MstCreditHoldRevokeSummaryController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, $sce, DownloaddocumentService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstCreditHoldRevokeSummaryController';

        activate();
        lockUI();      
            function activate() {
                var url = 'api/MstAdminApplication/GetCreditHoldApplSummary';
                SocketService.get(url).then(function (resp) {
                    unlockUI();
                    $scope.creditholdappl_list = resp.data.creditholdappl_list;
                });

                var url = 'api/MstAdminApplication/CreditApplicationRevokeCount';
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
            $location.url('app/MstCcCommitteeApplicationView?application_gid=' + application_gid + '&lspage=CreditHoldRevokeAppl');
        }

        $scope.Rejected_revoke = function (application_gid, created_by) {
            $location.url('app/MstCreditRejectHoldRevoke?application_gid=' + application_gid + '&employee_gid=' + created_by + '&lspage=CreditHoldRevokeAppl');
        }

        $scope.revoke_history = function (application_gid) {
            $location.url('app/MstCreditRejectHoldRevokeHistory?application_gid=' + application_gid + '&lspage=CreditHoldRevokeAppl');
        }

        $scope.hold_applications = function () {
            $state.go('app.MstCreditHoldRevokeSummary');
        }

        $scope.rejected_applications = function () {
            $state.go('app.MstCreditRevokeSummary');
        }

        $scope.business_revoked = function () {
            $state.go('app.MstCreditRevokedApplSummary');
        }

        $scope.creditmanagerreject_applications = function () {
            $state.go('app.MstCreditManagerRejectRevokeSummary');
        }
    }
})();
