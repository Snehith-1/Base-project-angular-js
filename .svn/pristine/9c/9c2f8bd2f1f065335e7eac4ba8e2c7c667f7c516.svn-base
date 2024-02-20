(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstCadPendingStageSummaryController', MstCadPendingStageSummaryController);

    MstCadPendingStageSummaryController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', '$sce', 'DownloaddocumentService'];

    function MstCadPendingStageSummaryController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, $sce, DownloaddocumentService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstCadPendingStageSummaryController';

        lockUI();
        activate();
        function activate() {
            var url = 'api/MstAdminApplication/GetCadPendingStageSummary';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.cadpendingstage_list = resp.data.cadpendingstage_list;
            });

            var url = 'api/MstAdminApplication/HierarchyUpdateApplCount';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.incompletestage_count = resp.data.incompletestage_count;
                $scope.businessstage_count = resp.data.businessstage_count;
                $scope.creditstage_count = resp.data.creditstage_count;
                $scope.ccstage_count = resp.data.ccstage_count;
                $scope.cadpendingstage_count = resp.data.cadpendingstage_count;
                $scope.cadacceptedstage_count = resp.data.cadacceptedstage_count;
                $scope.lstotalcount = resp.data.lstotalcount;
            });
        }

        $scope.incomplete_stage = function () {
            $state.go('app.MstIncompleteStageSummary');
        }

        $scope.business_stage = function () {
            $state.go('app.MstBusinessHierarchyUpdateSummary');
        }

        $scope.credit_stage = function () {
            $state.go('app.MstCreditStageSummary');
        }

        $scope.cc_stage = function () {
            $state.go('app.MstCcStageSummary');
        }

        $scope.cadpending_stage = function () {
            $state.go('app.MstCadPendingStageSummary');
        }

        $scope.cadaccepted_stage = function () {
            $state.go('app.MstCadAcceptedStageSummary');
        }

        $scope.hierarchy_update = function (application_gid, created_by) {
            $location.url('app/MstBusinessHierarchyUpdate?application_gid=' + application_gid + '&employee_gid=' + created_by + '&lspage=CadPendingStage');
        }

        $scope.hierarchyupdate_history = function (application_gid) {
            $location.url('app/MstBusinessHierarchyUpdateHistory?application_gid=' + application_gid + '&lspage=CadPendingStage');
        }

        $scope.applcreation_view = function (application_gid) {
            $location.url('app/MstCadPendingApplicationView?application_gid=' + application_gid + '&lspage=CadPendingStage');
        }

    }
})();
