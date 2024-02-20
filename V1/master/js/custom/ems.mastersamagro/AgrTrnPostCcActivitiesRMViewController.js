(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AgrTrnPostCcActivitiesRMViewController', AgrTrnPostCcActivitiesRMViewController);

        AgrTrnPostCcActivitiesRMViewController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams'];

    function AgrTrnPostCcActivitiesRMViewController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AgrTrnPostCcActivitiesRMViewController';
        var application_gid = $location.search().application_gid;

        activate();

        function activate() { }

        $scope.Back = function () {
            $location.url('app/AgrMstRMCustomerSummary');
        }

         $scope.GotoSanction = function () {
             $location.url('app/AgrMstRMSanctionSummary?application_gid=' + application_gid);
         }

        $scope.GotoDeferral = function () {
            $location.url('app/AgrTrnRMDeferralDtls?application_gid=' + application_gid);
        }
        // $scope.GotoWaiver = function () {
        //     $location.url('app/MstRMInitiateWaiverSummary?application_gid=' + application_gid);
        // }

        // $scope.GotoDocumentChecklist = function () {
        //     $location.url('app/MstRMDocChecklistDtls?application_gid=' + application_gid);
        // }

        // $scope.GotoPenalty = function () {
        //     $location.url('app/MstRMPenaltyDtls?application_gid=' + application_gid);
        // }
        
        // $scope.GotoLoanDetails = function () {
        //     $location.url('app/MstRMLoanDetailsDtls?application_gid=' + application_gid);
        // }
        // $scope.GotoTDS = function () {
        //     $location.url('app/MstRMTDSDtls?application_gid=' + application_gid);
        // }
        // $scope.GotoNDC = function () {
        //     $location.url('app/MstRMNDCDtls?application_gid=' + application_gid);
        // }
        // $scope.GotoNOC = function () {
        //     $location.url('app/MstRMNOCDtls?application_gid=' + application_gid);
        // }
        // $scope.GotoMoratorium = function () {
        //     $location.url('app/MstRMMoratoriumDtls?application_gid=' + application_gid);
        // }
        // $scope.GotoBankAccountDetails = function () {
        //     $location.url('app/MstRMBankAccountDetails?application_gid=' + application_gid);
        // }
        // $scope.GotoDeviation = function () {
        //     $location.url('app/MstRMDeviationDtls?application_gid=' + application_gid);
        // }
        // $scope.GotoDisbursementRequest = function () {
        //     $location.url('app/MstRMDisbursementRequest?application_gid=' + application_gid);
        // }

    }
})();
