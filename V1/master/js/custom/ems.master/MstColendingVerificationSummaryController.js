(function () {
    'use strict';
    angular
           .module('angle')
           .controller('MstColendingVerificationSummaryController', MstColendingVerificationSummaryController);

    MstColendingVerificationSummaryController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'SweetAlert', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', '$cookies', '$filter', 'ngTableParams', '$resource', '$timeout', 'ngTableDataService'];

    function MstColendingVerificationSummaryController($rootScope, $scope, $modal, $state, SweetAlert, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, $cookies, $filter, ngTableParams, $resource, $timeout, ngTableDataService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstColendingVerificationSummaryController';

        activate();

        function activate() {          
            lockUI();             
            var url = 'api/MstAppCreditUnderWriting/GetCreditTaggedPendingSummary';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.credittaggedpending_list = resp.data.credittaggedpending_list;
            });  

            var url = 'api/MstAppCreditUnderWriting/ColendingVerificationCount';
            lockUI();
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.creditpending_count = resp.data.creditpending_count;
                $scope.creditcompleted_count = resp.data.creditcompleted_count;
                $scope.ccapprovedverify_count = resp.data.ccapprovedverify_count;
                $scope.finalapprovedverified_count = resp.data.finalapprovedverified_count;
                $scope.lstotalcount = resp.data.lstotalcount;
            });
        }   

        $scope.credittagged_pending = function () {
            $location.url('app/MstColendingVerificationSummary');
        }

        $scope.credittagged_completed = function () {
            $location.url('app/MstColendingVerifyCompletedSummary');
        }

        $scope.cc_approved = function () {
            $location.url('app/MstColendingCCApprovedVerifySummary');
        }

        $scope.final_approved = function () {
            $location.url('app/MstCCFinalApprovedVerifiedSummary');
        }

        $scope.credittaggedpending_process = function (application_gid) {
            $location.url('app/MstColendingCreditVerification?application_gid=' + application_gid + '&lspage=' + 'ColendingCreditVerification');
        } 
             
        $scope.credittaggedpending_view = function (application_gid) {
            $location.url('app/MstApplicationCreationView?application_gid=' + application_gid + '&lstab=CreditPendingVerification');
        }

    }
})();
