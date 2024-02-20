﻿(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AgrMstSuprCCSkippedApplicationSummaryController', AgrMstSuprCCSkippedApplicationSummaryController);

    AgrMstSuprCCSkippedApplicationSummaryController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function AgrMstSuprCCSkippedApplicationSummaryController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AgrMstSuprCCSkippedApplicationSummaryController';
        //lockUI();
        activate();
        function activate() {
               //var url = 'api/AgrMstApplicationAdd/GetAppAssignmentSummary';
               //SocketService.get(url).then(function (resp) {
               //   unlockUI();
               //   $scope.appassignment_list = resp.data.applicationadd_list;
               //});
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
            $location.url('app/AgrMstSuprCcCommitteeApplicationView?application_gid=' + val + '&lstab=CCSkippedAppl');
        }

        $scope.start_creditunderwriting = function (application_gid) {
            $location.url('app/AgrTrnSuprStartCreditUnderwriting?application_gid=' + application_gid);
        }

        $scope.GradingTool_add = function (val) {
            $location.url('app/AgrMstCreditAssessedScoreAdd?application_gid=' + val + '&lstab=MyApplications');
        }

        $scope.VisitReport_add = function (val) {
            $location.url('app/AgrMstCreditVisitReportAdd?application_gid=' + val + '&lstab=MyApplications');
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

        $scope.ccapproved = function () {
            $location.url('app/AgrMstSuprCCApprovedSummary');
        } 
    }
})();
