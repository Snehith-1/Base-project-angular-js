(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AgrMstCadAcceptedStageSummaryController', AgrMstCadAcceptedStageSummaryController);

    AgrMstCadAcceptedStageSummaryController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', '$sce', 'DownloaddocumentService','cmnfunctionService'];

    function AgrMstCadAcceptedStageSummaryController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, $sce, DownloaddocumentService, cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AgrMstCadAcceptedStageSummaryController';

        lockUI();
        activate();
        function activate() {
            var url = 'api/AgrAdminApplication/GetCadAcceptedStageSummary';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.cadacceptedstage_list = resp.data.cadacceptedstage_list;
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
                $scope.productstage_count =resp.data.productstage_count;
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

        $scope.product_stage = function () {
            $state.go('app.AgrProductStageSummary');
        }

        $scope.hierarchy_update = function (application_gid, created_by) {
        //    $location.url('app/AgrMstBusinessHierarchyUpdate?application_gid=' + application_gid + '&employee_gid=' + created_by + '&lspage=CadAcceptedStage');
            $location.url("app/AgrMstBusinessHierarchyUpdate?hash=" + cmnfunctionService.encryptURL("application_gid=" + application_gid + '&employee_gid=' + created_by + '&lspage=CadAcceptedStage'));

        }

        $scope.hierarchyupdate_history = function (application_gid) {
        //    $location.url('app/AgrMstBusinessHierarchyUpdateHistory?application_gid=' + application_gid + '&lspage=CadAcceptedStage');
            $location.url("app/AgrMstBusinessHierarchyUpdateHistory?hash=" + cmnfunctionService.encryptURL("application_gid=" + application_gid + '&lspage=CadAcceptedStage'));

        }

        $scope.applcreation_view = function (application_gid) {
            $location.url('app/AgrMstCadApplicationView?application_gid=' + application_gid + '&lspage=CadAcceptedStage');
        }
    }
})();
