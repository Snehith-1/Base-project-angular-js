(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstMyApplicationsSummaryController', MstMyApplicationsSummaryController);

        MstMyApplicationsSummaryController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function MstMyApplicationsSummaryController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstMyApplicationsSummaryController';
    
        activate();
        function activate() {
            var url = 'api/MstCreditApproval/GetMyAppAssignedSummary';
            lockUI();
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.myappassignment_list = resp.data.applicaition_list;
            });
            var url = 'api/MstCreditApproval/MyApplicationCount';
            lockUI();
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.newcreditapplication_count = resp.data.newcreditapplication_count;
                $scope.approvedapplication_count = resp.data.approvedapplication_count;
                $scope.rejectholdapplication_count = resp.data.rejectholdapplication_count;
                $scope.submitted2ccapp_count = resp.data.submitted2ccapp_count;
                $scope.ccapproved_count = resp.data.ccapproved_count;
                $scope.lstotalcount = resp.data.lstotalcount;
            });

        }

        $scope.applcreation_view = function (val) {
            $location.url('app/MstApplicationCreationView?application_gid=' + val + '&lstab=MyApplications');
        }
        // MstMyApplicationsSummary_start_creditunderwriting
        $scope.start_creditunderwriting = function (application_gid, appcreditapproval_gid, product_gid, variety_gid) {
            $location.url('app/MstStartCreditUnderwriting?application_gid=' + application_gid + '&appcreditapproval_gid=' + appcreditapproval_gid + '&lspage=myapp' + '&product_gid=' + product_gid + '&variety_gid=' + variety_gid);
        }

        $scope.GradingTool_add = function (val) {
            $location.url('app/MstCreditAssessedScoreAdd?application_gid=' + val + '&lstab=gradingtool');             
        }

        $scope.VisitReport_add = function (val) {
            $location.url('app/MstCreditVisitReportAdd?application_gid=' + val + '&lstab=InProgress');         
        }

        $scope.submitedto_approval = function () {
            $location.url('app/MstSubmittedtoApprovalSummary');
        }

        $scope.submittedto_cc = function () {
            $location.url('app/MstSubmittedtoCCSummary');
        }

        $scope.ccskipped_appl = function () {
            $location.url('app/MstCCSkippedApplicationSummary');
        }

        $scope.rejected_holdappl = function () {
            $location.url('app/MstRejectandHoldSummary');
        }

        $scope.inprogress_appl = function () {
            $location.url('app/MstMyApplicationsSummary');
        }

        $scope.cc_approved = function () {
            $location.url('app/MstCCApprovedSummary');
        }

        $scope.creditcmd_statusupdate = function (application_gid, employee_gid, applicationapproval_gid,initiate_flag) {
            $location.url('app/MstCreditQueryStatus?application_gid=' + application_gid + '&employee_gid=' + employee_gid + '&lspage=MyApplications');
        }

        $scope.history = function (application_gid, employee_gid, applicationapproval_gid, initiate_flag) {
            $location.url('app/MstSentbackcctoCreditHistory?application_gid=' + application_gid + '&employee_gid=' + employee_gid + '&lspage=MyApplications');
        }
        
    }
})();
