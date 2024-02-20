(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstSAVerificationMakerIndividualDistractSummaryController', MstSAVerificationMakerIndividualDistractSummaryController);

    MstSAVerificationMakerIndividualDistractSummaryController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', 'cmnfunctionService'];

    function MstSAVerificationMakerIndividualDistractSummaryController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstSAVerificationMakerIndividualDistractSummaryController';

        activate();

        function activate() {
            var url = 'api/MstSAOnboardingIndividual/GetSAVerfiyIndividualMakerDistractSummary';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.saOnboardSummary_list = resp.data.saOnboardSummary_list;
                unlockUI();
            });
            var url = 'api/MstSAOnboardingIndividual/GetSaMakerCounts';
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

        $scope.maker_individual = function () {
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
        $scope.saonboardingverification = function (sacontact_gid) {

            //$location.url('app/MstSAOnboardingIndividualVerification');

            $location.url('app/MstSAVerificationIndividualMakerTrashView?hash=' + cmnfunctionService.encryptURL('lssacontact_gid=' + sacontact_gid + '&lspage=IndividualPending'));
        }
    }
})();
