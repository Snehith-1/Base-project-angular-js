(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AgrMstBusinessRevokeSummaryController', AgrMstBusinessRevokeSummaryController);

    AgrMstBusinessRevokeSummaryController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', '$sce', 'DownloaddocumentService','cmnfunctionService'];

    function AgrMstBusinessRevokeSummaryController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, $sce, DownloaddocumentService, cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AgrMstBusinessRevokeSummaryController';
        lockUI();
        activate();
        function activate() {
            var url = 'api/AgrAdminApplication/GetBusinessRejectedApplSummary';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.rejectedappl_list = resp.data.rejectedappl_list;
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
            $location.url('app/AgrApplicationCreationView?application_gid=' + application_gid + '&lstab=RejectRevokeApplication');
            //$location.url("app/AgrApplicationCreationView?hash=" + cmnfunctionService.encryptURL("application_gid=" + application_gid + '&lspage=RejectRevokeApplication'));

        }

        $scope.Rejected_revoke = function (application_gid, created_by) {
            /* $location.url('app/AgrMstBusinessRejectRevoke?application_gid=' + application_gid + '&employee_gid=' + created_by + '&lspage=RejectRevokeApplication');*/
            $location.url("app/AgrMstBusinessRejectRevoke?hash=" + cmnfunctionService.encryptURL("application_gid=" + application_gid + '&employee_gid=' + created_by + '&lspage=RejectRevokeApplication'));


        }

        $scope.revoke_history = function (application_gid) {
        //    $location.url('app/AgrMstBusinessRevokeHistory?application_gid=' + application_gid + '&lspage=RejectRevokeApplication');
            $location.url("app/AgrMstBusinessRevokeHistory?hash=" + cmnfunctionService.encryptURL("application_gid=" + application_gid + '&lspage=RejectRevokeApplication'));

        }

        $scope.hold_applications = function () {
            $state.go('app.AgrMstBusinessHoldRevokeSummary');
        }

        $scope.rejected_applications = function () {
            $state.go('app.AgrMstBusinessRevokeSummary');
        }

        $scope.business_revoked = function () {
            $state.go('app.AgrMstBusinessRevokedApplSummary');
        }

    }
})();