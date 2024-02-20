(function () {
    'use strict';
    angular
        .module('angle')
        .controller('MstColendingVerifyCompletedSummaryController', MstColendingVerifyCompletedSummaryController);

    MstColendingVerifyCompletedSummaryController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'SweetAlert', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', '$cookies', '$filter', 'ngTableParams', '$resource', '$timeout', 'ngTableDataService'];

    function MstColendingVerifyCompletedSummaryController($rootScope, $scope, $modal, $state, SweetAlert, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, $cookies, $filter, ngTableParams, $resource, $timeout, ngTableDataService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstColendingVerifyCompletedSummaryController';

        activate();

        function activate() {
            lockUI();
            var url = 'api/MstAppCreditUnderWriting/GetCreditTaggedCompletedSummary';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.credittaggedcompleted_list = resp.data.credittaggedcompleted_list;
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

        $scope.credittaggedcompleted_process = function (application_gid) {
            $location.url('app/MstCreditCompletedVerification?application_gid=' + application_gid + '&lspage=' + 'CreditCompletedVerification');
        }

        $scope.credittaggedcompleted_view = function (application_gid) {
            $location.url('app/MstApplicationCreationView?application_gid=' + application_gid + '&lstab=CreditCompletedVerification');
        }

    }
})();
