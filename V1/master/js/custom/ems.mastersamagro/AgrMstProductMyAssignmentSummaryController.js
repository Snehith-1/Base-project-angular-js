(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AgrMstProductMyAssignmentSummaryController', AgrMstProductMyAssignmentSummaryController);

    AgrMstProductMyAssignmentSummaryController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', 'DownloaddocumentService'];

    function AgrMstProductMyAssignmentSummaryController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, DownloaddocumentService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AgrMstProductMyAssignmentSummaryController';
        lockUI();
        activate();
        function activate() {
            var url = 'api/AgrTrnProductApproval/GetMyAppProductAssignedSummary';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.myappassignment_list = resp.data.applicationProduct_list;
            });
            var url = 'api/AgrTrnProductApproval/MyApplicationProductCount';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.newcreditapplication_count = resp.data.newapplication_count;
                $scope.approvedapplication_count = resp.data.approvedapplication_count;
                $scope.rejectholdapplication_count = resp.data.rejectholdapplication_count; 
                $scope.lstotalcount = resp.data.lstotalcount;
            });

        }

        $scope.applcreation_view = function (val) {
            $location.url('app/AgrApplicationCreationView?application_gid=' + val + '&lstab=ProductDescMyAssignment');
        }

        $scope.product_verification = function (application_gid, appproductapproval_gid, product_gid, variety_gid, shortclosing_flag) {
            $location.url('app/AgrMstProducDesctVerification?application_gid=' + application_gid + '&appproductapproval_gid=' + appproductapproval_gid + '&lspage=ProductDescMyAssignment' + '&product_gid=' + product_gid + '&variety_gid=' + variety_gid + '&shortclosing_flag=' + shortclosing_flag);
        }
       
        $scope.VisitReport_add = function (val) {
            $location.url('app/AgrProductDescVisitReportAdd?application_gid=' + val + '&lstab=ProductDescMyAssignment');
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
            $location.url('app/AgrMstProductDescQueryStatus?application_gid=' + application_gid + '&employee_gid=' + employee_gid + '&lspage=ProductDescMyAssignment');
        }

    }
})();
