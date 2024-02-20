(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AgrMstSuprMyApplicationsSummaryController', AgrMstSuprMyApplicationsSummaryController);

    AgrMstSuprMyApplicationsSummaryController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', 'DownloaddocumentService'];

    function AgrMstSuprMyApplicationsSummaryController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, DownloaddocumentService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AgrMstSuprMyApplicationsSummaryController';
        lockUI();
        activate();
        function activate() {
            var url = 'api/AgrTrnSuprCreditApproval/GetMyAppAssignedSummary';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.myappassignment_list = resp.data.applicaition_list;
            });
            var url = 'api/AgrTrnSuprCreditApproval/MyApplicationCount';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.newcreditapplication_count = resp.data.newcreditapplication_count;
                $scope.approvedapplication_count = resp.data.approvedapplication_count;
                $scope.rejectholdapplication_count = resp.data.rejectholdapplication_count;
                $scope.submitted2ccapp_count = resp.data.submitted2ccapp_count;
                $scope.ccapproval_count = resp.data.ccapproval_count;
                $scope.lstotalcount = resp.data.lstotalcount;
            });

        }

        $scope.applcreation_view = function (val) {
            $location.url('app/AgrMstSuprApplicationCreationView?application_gid=' + val + '&lstab=MyApplications');
        }

        $scope.start_creditunderwriting = function (application_gid, appcreditapproval_gid, product_gid, variety_gid) {
            $location.url('app/AgrTrnSuprStartCreditUnderwriting?application_gid=' + application_gid + '&appcreditapproval_gid=' + appcreditapproval_gid + '&lspage=myapp' + '&product_gid=' + product_gid + '&variety_gid=' + variety_gid);
        }

        $scope.GradingTool_add = function (val) {
            $location.url('app/AgrMstSuprCreditAssessedScoreAdd?application_gid=' + val + '&lstab=MyApplications');
        }

        $scope.VisitReport_add = function (val) {
            $location.url('app/AgrSuprCreditVisitReportAdd?application_gid=' + val + '&lstab=MyApplications');
        }

        $scope.submitedto_approval = function () {
            $location.url('app/AgrMstSuprSubmittedtoApprovalSummary');
        }

        $scope.submittedto_cc = function () {
            $location.url('app/AgrMstSuprSubmittedtoCCSummary');
        }

        $scope.ccskipped_appl = function () {
            $location.url('app/AgrMstSuprCCSkippedApplicationSummary');
        }

        $scope.rejected_holdappl = function () {
            $location.url('app/AgrMstSuprRejectandHoldSummary');
        }

        $scope.inprogress_appl = function () {
            $location.url('app/AgrMstSuprMyApplicationsSummary');
        }

        $scope.creditcmd_statusupdate = function (application_gid, employee_gid, applicationapproval_gid, initiate_flag) {
            $location.url('app/AgrMstSuprCreditQueryStatus?application_gid=' + application_gid + '&employee_gid=' + employee_gid + '&lspage=MyApplications');
        }

        $scope.history = function (application_gid, employee_gid, applicationapproval_gid, initiate_flag) {
            $location.url('app/AgrMstSuprSentbackcctoCreditHistory?application_gid=' + application_gid + '&employee_gid=' + employee_gid + '&lspage=MyApplications');
        }

        $scope.ccapproved = function () {
            $location.url('app/AgrMstSuprCCApprovedSummary');
        } 

    }
})();
