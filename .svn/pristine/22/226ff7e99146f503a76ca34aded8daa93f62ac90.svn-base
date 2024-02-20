(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AgrMstBusinessRevokedApplSummaryController', AgrMstBusinessRevokedApplSummaryController);

    AgrMstBusinessRevokedApplSummaryController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', '$sce', 'DownloaddocumentService','cmnfunctionService'];

    function AgrMstBusinessRevokedApplSummaryController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, $sce, DownloaddocumentService, cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AgrMstBusinessRevokedApplSummaryController';
        lockUI();
        activate();
        function activate() {
            var url = 'api/AgrAdminApplication/GetBusinessRevokedApplSummary';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.revokedappl_list = resp.data.revokedappl_list;
            });

            var url = 'api/AgrAdminApplication/ApplicationRevokeCount';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.businessreject_count = resp.data.businessreject_count;
                $scope.businesshold_count = resp.data.businesshold_count;
                $scope.businessrevoked_count = resp.data.businessrevoked_count;
                $scope.lstotalcount = resp.data.lstotalcount;
            });
        }

        $scope.applcreation_view = function (application_gid) {
            $location.url('app/AgrApplicationCreationView?application_gid=' + application_gid + '&lstab=BusinessRevokedApplication');
        //    $location.url("app/AgrApplicationCreationView?hash=" + cmnfunctionService.encryptURL("application_gid=" + application_gid + '&lspage=BusinessRevokedApplication'));
        }

        //$scope.Rejected_revoke = function (application_gid, created_by) {
        //    $location.url('app/AgrMstBusinessRejectRevoke?application_gid=' + application_gid + '&employee_gid=' + created_by + '&lspage=BusinessRevokedApplication');
        //}

        $scope.hold_applications = function () {
            $state.go('app.AgrMstBusinessHoldRevokeSummary');
        }

        $scope.rejected_applications = function () {
            $state.go('app.AgrMstBusinessRevokeSummary');
        }

        $scope.business_revoked = function () {
            $state.go('app.AgrMstBusinessRevokedApplSummary');
        }

        $scope.revoke_history = function (application_gid) {
        //    $location.url('app/AgrMstBusinessRevokeHistory?application_gid=' + application_gid + '&lspage=BusinessRevokedApplication');
            $location.url("app/AgrMstBusinessRevokeHistory?hash=" + cmnfunctionService.encryptURL("application_gid=" + application_gid + '&lspage=BusinessRevokedApplication'));
        }
    }
})();
