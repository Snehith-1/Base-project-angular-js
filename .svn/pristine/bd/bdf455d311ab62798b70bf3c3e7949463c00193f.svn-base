(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstSAVerificationInstitutionDistractSummaryController', MstSAVerificationInstitutionDistractSummaryController);

    MstSAVerificationInstitutionDistractSummaryController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$filter', '$route', 'ngTableParams', 'cmnfunctionService'];

    function MstSAVerificationInstitutionDistractSummaryController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $filter, $route, ngTableParams, cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstSAVerificationInstitutionDistractSummaryController';


        activate();

        function activate() {
            var url = 'api/MstSAOnboardingInstitution/GetSAVerfiyInstitutionMakerDistractSummary';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.saOnboardSummary_list = resp.data.saOnboardSummary_list;
                unlockUI();
            });
            var url = 'api/MstSAOnboardingInstitution/GetSaMakerCounts';
            SocketService.get(url).then(function (resp) {
                unlockUI()
                $scope.institution_count = resp.data.institution_count;
                $scope.individual_count = resp.data.individual_count;
                $scope.institutioninitiated_count = resp.data.institutioninitiated_count;
                $scope.individualinitiated_count = resp.data.individualinitiated_count;
                $scope.individualtrash_count = resp.data.individualtrash_count;
                $scope.institutiontrash_count = resp.data.institutiontrash_count;

            });
        }
        $scope.verification_pending = function () {
            $location.url('app/MstSAVerificationPending')
        }
        $scope.verification_individual = function () {
            $location.url('app/MstSAVerificationIndividualPending')
        }

        $scope.verification_maker = function () {
            $location.url('app/MstSAVerificationMakerSummary')
        }

        $scope.verification_checker = function () {
            $location.url('app/MstSAVerificationInstPendingSummary')
        }
        $scope.maker_pending = function () {
            $state.go('app.MstSAVerificationMakerSummary');
        }

        $scope.maker_initiated = function () {
            $state.go('app.MstSAVerificationMakerInitiatedSummary');
        }

        $scope.maker_individaul = function () {
            $state.go('app.MstSAVerificationMakerPendingSummary');
        }

        $scope.maker_individualinitiated = function () {
            $state.go('app.MstSAVerificationMakerIndInstSummary');
        }
        $scope.maker_individualdistract = function () {
            $state.go('app.MstSAVerificationMakerIndividualDistractSummary');
        }
        $scope.maker_institutiondistract = function () {
            $state.go('app.MstSAVerificationMakerInstitutionDistractSummary');
        }
        $scope.saonboardingverification = function (sacontactinstitution_gid) {

            // $location.url('app/MstSAOnboardingInstitutionVerification');

            $location.url('app/MstSAVerificationInstitutionMakerTrashView?hash=' + cmnfunctionService.encryptURL('lssacontactinstitution_gid=' + sacontactinstitution_gid + '&lspage=InstitutePending'));
        }
    }
})();


