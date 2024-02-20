(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AgrTrnProductDeskAutoApprovalSummaryController', AgrTrnProductDeskAutoApprovalSummaryController);

    AgrTrnProductDeskAutoApprovalSummaryController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', '$sce', 'DownloaddocumentService'];

    function AgrTrnProductDeskAutoApprovalSummaryController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, $sce, DownloaddocumentService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AgrTrnProductDeskAutoApprovalSummaryController';
        lockUI();
        activate();
        function activate() {
            var url = 'api/AgrTrnProductApproval/GetAutoApprovalSummary';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.appapproval_list = resp.data.applicationProduct_list;
            });
            var url = 'api/AgrTrnProductApproval/ProductApplicationCount';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.newcreditapplication_count = resp.data.newapplication_count;
                $scope.approvedapplication_count = resp.data.approvedapplication_count;
                $scope.rejectholdapplication_count = resp.data.rejectholdapplication_count;
                $scope.submitted2ccapp_count = resp.data.submitted2ccapp_count;
                $scope.Advanceapplication_count = resp.data.Advanceapplication_count;
                $scope.lstotalcount = resp.data.lstotalcount;
            });
        }

        $scope.applcreation_view = function (val) {
            $location.url('app/AgrTrnCcCommitteeApplicationView?application_gid=' + val + '&lspage=ProductDescAdvance');
        }

        $scope.inprogress_appl = function () {
            $location.url('app/AgrMstProductcDescApprovalSummary');
        }

        $scope.approved_application = function () {
            $location.url('app/AgrTrnProductDescApprovedSummary');
        }

        $scope.rejected_holdappl = function () {
            $location.url('app/AgrTrnProductDescRejectandHoldSummary');
        }

        $scope.advance_appl = function () {
            $location.url('app/AgrTrnProductDeskAdvanceSummary');
        }

    
    }
})();
