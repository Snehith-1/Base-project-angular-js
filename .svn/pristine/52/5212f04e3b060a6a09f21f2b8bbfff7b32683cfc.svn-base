(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AgrMstProductRevokeSummaryController', AgrMstProductRevokeSummaryController);

    AgrMstProductRevokeSummaryController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', '$sce', 'DownloaddocumentService','cmnfunctionService'];

    function AgrMstProductRevokeSummaryController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, $sce, DownloaddocumentService, cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AgrMstProductRevokeSummaryController';

        activate();
        lockUI();
        function activate() {
            var url = 'api/AgrAdminApplication/GetProductRejectedApplSummary';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.creditrejectedappl_list = resp.data.creditrejectedappl_list;
            });

            var url = 'api/AgrAdminApplication/ProductApplicationRevokeCount';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.creditreject_count = resp.data.creditreject_count;
                $scope.credithold_count = resp.data.credithold_count;
                $scope.creditrevoked_count = resp.data.creditrevoked_count;
                $scope.lstotalcount = resp.data.lstotalcount;
            });
        }

        $scope.applcreation_view = function (application_gid) {
            $location.url('app/AgrMstCcCommitteeApplicationView?application_gid=' + application_gid + '&lstab=ProductRejectRevokeAppl');
        }

        $scope.Rejected_revoke = function (application_gid, created_by) {
        //    $location.url('app/AgrMstProductRejectHoldRevoke?application_gid=' + application_gid + '&employee_gid=' + created_by + '&lspage=ProductRejectRevokeAppl');
            $location.url("app/AgrMstProductRejectHoldRevoke?hash=" + cmnfunctionService.encryptURL("application_gid=" + application_gid + '&employee_gid=' + created_by + '&lspage=ProductRejectRevokeAppl'));
        }

        $scope.revoke_history = function (application_gid) {
        //    $location.url('app/AgrMstProductRejectHoldRevokeHistory?application_gid=' + application_gid + '&lspage=ProductRejectRevokeAppl');
            $location.url("app/AgrMstProductRejectHoldRevokeHistory?hash=" + cmnfunctionService.encryptURL("application_gid=" + application_gid + '&lspage=ProductRejectRevokeAppl'));
        }

        $scope.hold_applications = function () {
            $state.go('app.AgrMstProductHoldRevokeSummary');
        }

        $scope.rejected_applications = function () {
            $state.go('app.AgrMstProductRevokeSummary');
        }

        $scope.business_revoked = function () {
            $state.go('app.AgrMstProductRevokedApplSummary');
        }

    }
})();
