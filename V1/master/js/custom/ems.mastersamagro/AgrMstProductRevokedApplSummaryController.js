(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AgrMstProductRevokedApplSummaryController', AgrMstProductRevokedApplSummaryController);

    AgrMstProductRevokedApplSummaryController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', '$sce', 'DownloaddocumentService','cmnfunctionService'];

    function AgrMstProductRevokedApplSummaryController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, $sce, DownloaddocumentService, cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AgrMstProductRevokedApplSummaryController';

        activate();
        lockUI();
        function activate() {
            var url = 'api/AgrAdminApplication/GetProductRevokedApplSummary';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.creditrevokedappl_list = resp.data.creditrevokedappl_list;
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
            $location.url('app/AgrMstCcCommitteeApplicationView?application_gid=' + application_gid + '&lstab=ProductRevokedAppl');
        }      

        $scope.revoke_history = function (application_gid) {
        //    $location.url('app/AgrMstProductRejectHoldRevokeHistory?application_gid=' + application_gid + '&lspage=ProductRevokedAppl');
            $location.url("app/AgrMstProductRejectHoldRevokeHistory?hash=" + cmnfunctionService.encryptURL("application_gid=" + application_gid + '&lspage=ProductRevokedAppl'));

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
