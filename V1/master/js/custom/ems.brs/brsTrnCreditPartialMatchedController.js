
(function () {
    'use strict';

    angular
        .module('angle')
        .controller('brsTrnCreditPartialMatchedController', brsTrnCreditPartialMatchedController);

        brsTrnCreditPartialMatchedController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', 'DownloaddocumentService','cmnfunctionService'];

    function brsTrnCreditPartialMatchedController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, DownloaddocumentService,cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'brsTrnCreditPartialMatchedController';
       // console.log('test');
        activate();

        function activate() {
          
            var url = 'api/RepaymentReconcillation/GetCreditPartialMatchedSummary';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.creditpartialmatched_list = resp.data.creditpartialmatched_list;
                unlockUI();

            });
            var url = "api/RepaymentReconcillation/GetDebitCount";
            SocketService.get(url).then(function (resp) {
                $scope.debit_count = resp.data.debit_count;
                unlockUI();
            });

            var url = "api/RepaymentReconcillation/GetCreditCount";
            SocketService.get(url).then(function (resp) {
                $scope.credit_count = resp.data.credit_count;
                $scope.creditmatch_count = resp.data.creditmatch_count;
                $scope.partialcredit_count = resp.data.partialcredit_count;
                $scope.unmatchunassign_count = resp.data.unmatchunassign_count;
                $scope.unmatchassign_count = resp.data.unmatchassign_count;
                $scope.creditclose_count = resp.data.creditclose_count;
                unlockUI();
            });


            
        }
        $scope.recon = function () {
            $state.go('app.brsTrnCreditMatched');
        }

        $scope.unrecon = function () {
            $state.go('app.brsTrnDebitMatched');
        }
        $scope.partialmatch = function () {
            $state.go('app.brsTrnCreditPartialMatched');
        }
        $scope.unmatch = function () {
            $state.go('app.brsTrnCreditUnMatchedUnAssigned');
        }
        $scope.assign = function () {
            $state.go('app.brsTrnCreditUnMatchedAssigned');
        }
         $scope.close = function () {
            $state.go('app.brsTrnCreditClosed');
        }
        $scope.view = function (banktransc_gid) {
            /* $location.url('app/brsTrnUnreconTagViewAssignedHistory?banktransc_gid=' + banktransc_gid);*/
            $location.url("app/brsTrnPartialMatchedView?hash=" + cmnfunctionService.encryptURL("banktransc_gid=" + banktransc_gid + '&lspage=CreditPartial'));
        }
        
    }
})();
