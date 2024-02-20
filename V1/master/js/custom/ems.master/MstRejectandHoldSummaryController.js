(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstRejectandHoldSummaryController', MstRejectandHoldSummaryController);

        MstRejectandHoldSummaryController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function MstRejectandHoldSummaryController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstRejectandHoldSummaryController';
      
        activate();

        function activate() {
            var url = 'api/MstCreditApproval/GetMyAppRejectHoldSummary';
            lockUI();
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.apprejecthold_list = resp.data.applicaition_list;
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
            $location.url('app/MstCcCommitteeApplicationView?application_gid=' + val + '&lspage=RejectHoldAppl');
        }

        $scope.start_creditunderwriting = function (application_gid, appcreditapproval_gid) {
            $location.url('app/MstStartCreditUnderwriting?application_gid=' + application_gid + '&appcreditapproval_gid=' + appcreditapproval_gid + '&lspage=rejecthold');
        }


        $scope.creditcmd_statusupdate = function (application_gid, employee_gid, applicationapproval_gid, initiate_flag) {
            $location.url('app/MstCreditQueryStatus?application_gid=' + application_gid + '&employee_gid=' + employee_gid + '&initiate_flag=N' + '&lspage=rejectedapproval');
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
        
    }
})();
