﻿(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AgrTrnProductDescRejectandHoldSummaryController', AgrTrnProductDescRejectandHoldSummaryController);

    AgrTrnProductDescRejectandHoldSummaryController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', '$sce', 'DownloaddocumentService'];

    function AgrTrnProductDescRejectandHoldSummaryController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, $sce, DownloaddocumentService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AgrTrnProductDescRejectandHoldSummaryController';
        lockUI();
        activate();
        function activate() {
            var url = 'api/AgrTrnProductApproval/GetProductRejectHoldSummary';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.apprejecthold_list = resp.data.applicationProduct_list;
            });
            var url = 'api/AgrTrnProductApproval/ProductApplicationCount';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.newcreditapplication_count = resp.data.newapplication_count;
                $scope.approvedapplication_count = resp.data.approvedapplication_count;
                $scope.rejectholdapplication_count = resp.data.rejectholdapplication_count;
                $scope.Advanceapplication_count = resp.data.Advanceapplication_count;
                $scope.lstotalcount = resp.data.lstotalcount;
            });
        }

        $scope.applcreation_view = function (val) {
            $location.url('app/AgrTrnCcCommitteeApplicationView?application_gid=' + val + '&lspage=ProductDescRejectHold');
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
            $location.url('app/AgrTrnProductDeskAutoApprovalSummary');
        }

        $scope.productdesc_approval = function (application_gid, employee_gid, appcreditapproval_gid, initiate_flag) {
            $location.url('app/AgrTrnProductDescApproval?application_gid=' + application_gid + '&employee_gid=' + employee_gid + '&appcreditapproval_gid=' + appcreditapproval_gid + '&lspage=ProductDescRejectHold' + '&initiate_flag=' + initiate_flag);
        }

    }
})();