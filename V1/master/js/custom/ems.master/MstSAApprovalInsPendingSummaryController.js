(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstSAApprovalInsPendingSummaryController', MstSAApprovalInsPendingSummaryController);

    MstSAApprovalInsPendingSummaryController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', 'cmnfunctionService'];

    function MstSAApprovalInsPendingSummaryController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, cmnfunctionService) {
    /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstSAApprovalInsPendingSummaryController';

        activate();

        function activate() { 
            var url = 'api/MstSAOnboardingInstitution/GetSaApprovalPendingSummary';
            lockUI();
            SocketService.get(url).then(function (resp) {               
                $scope.saOnboardSummary_list = resp.data.saOnboardSummary_list;
                unlockUI();
            });
            var url = 'api/MstSAOnboardingBussDevtVerification/GetSaApproverCounts';
            SocketService.get(url).then(function (resp) {
                unlockUI()
                $scope.institution_count = resp.data.institution_count;
                $scope.individual_count = resp.data.individual_count;
                $scope.institutioninitiated_count = resp.data.institutioninitiated_count;
                $scope.individualinitiated_count = resp.data.individualinitiated_count;
                $scope.institutiondeferred_count = resp.data.institutiondeferred_count;
                $scope.individualdeferred_count = resp.data.individualdeferred_count;

            });
        }

        
        $scope.institution_approved = function () {
            $state.go('app.MstSAOnboardingApprovalInsSummary');
        }      
        $scope.institution_pending = function () {
            $state.go('app.MstSAApprovalInsPendingSummary');
        }   
    
        $scope.individual_approved = function () {
            $state.go('app.MstSAOnboardingApprovalIndSummary');
        }
        $scope.individual_pending = function () {
            $state.go('app.MstSAApprovalIndPendingSummary');
        }
        $scope.institution_deferred = function () {
            $state.go('app.MstSAApprovalInstitutionDeferredSummary');
        }
        $scope.individual_deferred = function () {
            $state.go('app.MstSAApprovalIndividualDeferredSummary');
        }


        $scope.saonboardingverification = function (sacontactinstitution_gid) {

            // $location.url('app/MstSAOnboardingInstitutionVerification');

            $location.url('app/MstSAOnboardingInstitutionApproval?hash=' + cmnfunctionService.encryptURL('lssacontactinstitution_gid=' + sacontactinstitution_gid + '&lspage=InstitutePending' ));
        }
    }
})();
