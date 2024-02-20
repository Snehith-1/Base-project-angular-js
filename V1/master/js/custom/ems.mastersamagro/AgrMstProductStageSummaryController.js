(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AgrMstProductStageSummaryController', AgrMstProductStageSummaryController);

    AgrMstProductStageSummaryController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', '$sce', 'DownloaddocumentService'];

    function AgrMstProductStageSummaryController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, $sce, DownloaddocumentService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AgrMstProductStageSummaryController';
        lockUI();
        activate();
        function activate() {
            var url = 'api/AgrAdminApplication/GetCreditStageSummary';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.creditstage_list = resp.data.creditstage_list;
            });

            var url = 'api/AgrAdminApplication/HierarchyUpdateApplCount';
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
            $state.go('app.AgrIncompleteStageSummary');
        }

        $scope.business_stage = function () {
            $state.go('app.AgrMstBusinessHierarchyUpdateSummary');
        }

        $scope.credit_stage = function () {
            $state.go('app.AgrMstCreditStageSummary');
        }

        $scope.cc_stage = function () {
            $state.go('app.AgrMstCcStageSummary');
        }

        $scope.cadpending_stage = function () {
            $state.go('app.AgrMstCadPendingStageSummary');
        }

        $scope.cadaccepted_stage = function () {
            $state.go('app.AgrMstCadAcceptedStageSummary');
        }

        $scope.hierarchy_update = function (application_gid, created_by) {
            $location.url('app/AgrMstBusinessHierarchyUpdate?application_gid=' + application_gid + '&employee_gid=' + created_by + '&lspage=CreditStage');
        }

        $scope.hierarchyupdate_history = function (application_gid) {
            $location.url('app/AgrMstBusinessHierarchyUpdateHistory?application_gid=' + application_gid + '&lspage=CreditStage');
        }

        $scope.applcreation_view = function (application_gid) {
            $location.url('app/AgrMstCcCommitteeApplicationView?application_gid=' + application_gid + '&lstab=CreditStage');
        }

    }
})();
