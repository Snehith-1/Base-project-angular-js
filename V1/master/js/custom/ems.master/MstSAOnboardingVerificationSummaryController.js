(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstSAOnboardingVerificationSummaryController', MstSAOnboardingVerificationSummaryController);

    MstSAOnboardingVerificationSummaryController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', 'cmnfunctionService'];

    function MstSAOnboardingVerificationSummaryController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, cmnfunctionService) {
    /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstSAOnboardingVerificationSummaryController';

        activate();

        function activate() { 
            var url = 'api/MstSAOnboardingInstitution/GetSAVerifyInitiatedSummary';
            lockUI();
            SocketService.get(url).then(function (resp) {               
                $scope.saOnboardSummary_list = resp.data.saOnboardSummary_list;
                unlockUI();
            });
            var url = 'api/MstSAOnboardingInstitution/GetSaCheckerCounts';
            SocketService.get(url).then(function (resp) {
                unlockUI()
                $scope.institution_count = resp.data.institution_count;
                $scope.individual_count = resp.data.individual_count;
                $scope.institutioninitiated_count = resp.data.institutioninitiated_count;
                $scope.individualinitiated_count = resp.data.individualinitiated_count;
                $scope.institutiontrash_count = resp.data.institutiontrash_count;
                $scope.individualtrash_count = resp.data.individualtrash_count;

            });
        }
        
        $scope.verification_checker = function () {
            $state.go('app.MstSAOnboardingVerificationSummary');
        }

        $scope.institution_pending = function () {
            $state.go('app.MstSAVerificationInstPendingSummary');
        }
        
        $scope.institution_initiated = function () {
            $state.go('app.MstSAOnboardingVerificationSummary');
        }      
    
        $scope.individual_pending = function () {
            $state.go('app.MstSAVerificationIndPendingSummary');
        }

        $scope.individual_initiated = function () {
            $state.go('app.MstSAOnboardingIndividualVerificationSummary');
        }
        $scope.institution_distract = function () {
            $state.go('app.MstSAVerificationInstitutionCheckerDistractSummary');
        }
        $scope.individual_distract = function () {
            $state.go('app.MstSAVerificationIndividualCheckerDistractSummary');
        }

        $scope.saonboardingverification = function (sacontactinstitution_gid) {

            // $location.url('app/MstSAOnboardingInstitutionVerification');

            $location.url('app/MstSAVerificationCheckerInstitutionInitiatedView?hash=' + cmnfunctionService.encryptURL('lssacontactinstitution_gid=' + sacontactinstitution_gid + '&lspage=InstituteInitiate'));
        }
        $scope.saonboardingverificationedit = function (sacontactinstitution_gid) {

            // $location.url('app/MstSAOnboardingInstitutionVerification');

            $location.url('app/MstSAOnboardingInstitutionVerificationEdit?hash=' + cmnfunctionService.encryptURL('lssacontactinstitution_gid=' + sacontactinstitution_gid + '&lspage=InstituteInitiate'));
        }
    }
})();
