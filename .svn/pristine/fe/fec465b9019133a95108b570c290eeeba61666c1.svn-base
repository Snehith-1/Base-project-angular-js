(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstBusinessHoldRevokeSummaryController', MstBusinessHoldRevokeSummaryController);

    MstBusinessHoldRevokeSummaryController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', '$sce', 'DownloaddocumentService'];

    function MstBusinessHoldRevokeSummaryController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, $sce, DownloaddocumentService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstBusinessHoldRevokeSummaryController';
        lockUI();
        activate();
        function activate() {
            var url = 'api/MstAdminApplication/GetBusinessHoldApplSummary';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.holdappl_list = resp.data.holdappl_list;
            });

            var url = 'api/MstAdminApplication/ApplicationRevokeCount';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.businessreject_count = resp.data.businessreject_count;
                $scope.businesshold_count = resp.data.businesshold_count;
                $scope.businessrevoked_count = resp.data.businessrevoked_count;
                $scope.lstotalcount = resp.data.lstotalcount;
            });
        }

        $scope.applcreation_view = function (application_gid) {
            $location.url('app/MstApplicationCreationView?application_gid=' + application_gid + '&lstab=HoldRevokeApplication');
        }

        $scope.Rejected_revoke = function (application_gid, created_by) {
            $location.url('app/MstBusinessRejectRevoke?application_gid=' + application_gid + '&employee_gid=' + created_by + '&lspage=HoldRevokeApplication');
        }

        $scope.hold_applications = function () {
            $state.go('app.MstBusinessHoldRevokeSummary');
        }

        $scope.rejected_applications = function () {
            $state.go('app.MstBusinessRevokeSummary');
        }

        $scope.business_revoked = function () {
            $state.go('app.MstBusinessRevokedApplSummary');
        }

        $scope.revoke_history = function (application_gid) {
            $location.url('app/MstBusinessRevokeHistory?application_gid=' + application_gid + '&lstab=HoldRevokeApplication');
        }

        $scope.revoke_history = function (application_gid) {
            $location.url('app/MstBusinessRevokeHistory?application_gid=' + application_gid + '&lspage=HoldRevokeApplication');
        }
    }
})();
