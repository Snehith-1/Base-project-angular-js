(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AgrMstProductSubmittedtoApprovalSummaryController', AgrMstProductSubmittedtoApprovalSummaryController);

    AgrMstProductSubmittedtoApprovalSummaryController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', '$sce', 'DownloaddocumentService'];

    function AgrMstProductSubmittedtoApprovalSummaryController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, $sce, DownloaddocumentService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AgrMstProductSubmittedtoApprovalSummaryController';
        lockUI();
        activate();
        function activate() {

            var url = 'api/AgrTrnProductApproval/GetMyAppProductSubmittedSummary';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.appsubmitted_list = resp.data.applicationProduct_list;
            });

            var url = 'api/AgrTrnProductApproval/MyApplicationProductCount';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.newcreditapplication_count = resp.data.newapplication_count;
                $scope.approvedapplication_count = resp.data.approvedapplication_count;
                $scope.rejectholdapplication_count = resp.data.rejectholdapplication_count;
                $scope.submitted2ccapp_count = resp.data.submitted2ccapp_count;
                $scope.lstotalcount = resp.data.lstotalcount;
            });
        }

        $scope.applcreation_view = function (val) {
            $location.url('app/AgrMstCcCommitteeApplicationView?application_gid=' + val + '&lstab=ProductSubmittedtoApproval');
        }

        $scope.product_verification = function (application_gid, appcreditapproval_gid, product_gid, variety_gid) {
            $location.url('app/AgrMstProducDesctVerification?application_gid=' + application_gid + '&appcreditapproval_gid=' + appcreditapproval_gid + '&lspage=ProductSubmittedtoApproval' + '&product_gid=' + product_gid + '&variety_gid=' + variety_gid);
        }

        $scope.submitedto_approval = function () {
            $location.url('app/AgrMstProductSubmittedtoApprovalSummary');
        }

        $scope.rejected_holdappl = function () {
            $location.url('app/AgrMstProductRejectedApplSummary');
        }

        $scope.inprogress_appl = function () {
            $location.url('app/AgrMstProductMyAssignmentSummary');
        }

        $scope.productcmd_statusupdate = function (application_gid, employee_gid, applicationapproval_gid, initiate_flag) {
            $location.url('app/AgrMstProductDescQueryStatus?application_gid=' + application_gid + '&employee_gid=' + employee_gid + '&lspage=ProductSubmittedtoApproval');
        }
    }
})();
