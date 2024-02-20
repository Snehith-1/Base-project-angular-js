(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstSAVerificationIndividualDeferredSummaryController', MstSAVerificationIndividualDeferredSummaryController);

    MstSAVerificationIndividualDeferredSummaryController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$filter', '$route', 'ngTableParams', 'cmnfunctionService'];

    function MstSAVerificationIndividualDeferredSummaryController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $filter, $route, ngTableParams, cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstSAVerificationIndividualDeferredSummaryController';


        activate();

        function activate() {
            var url = 'api/MstSAOnboardingIndividual/GetIndividualDeferredSummary';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.saOnboardSummary_list = resp.data.saOnboardSummary_list;
                unlockUI();
            });

            var url = 'api/MstSAOnboardingBussDevtVerification/SaonboardingBDVerificationDeferredCounts';
            SocketService.get(url).then(function (resp) {
                unlockUI()
                $scope.pending_Inst_count = resp.data.institutiondeferred_count;
                $scope.pending_Indi_count = resp.data.individualdeferred_count;

            });

        }
        $scope.verification_pending = function () {
            $location.url('app/MstSAVerificationPending')
        }
        $scope.verification_individual = function () {
            $location.url('app/MstSAVerificationIndividualPending')
        }



        $scope.verification_approved = function () {
            $location.url('app/MstSAVerificationApprovedSummary')
        }

        $scope.verification_rejected = function () {
            $location.url('app/MstSAVerificationRejectedSummary')
        }

        $scope.institution_rejected = function () {
            $location.url('app/MstSAVerificationRejectedSummary')
        }

        $scope.institution_approved = function () {
            $state.go('app.MstSAVerificationApprovedSummary');
        }

        $scope.individual_approved = function () {
            $state.go('app.MstSAVerificationApprovedIndSummary');
        }
        $scope.mapping_pending = function () {
            $location.url('app/MstSAVerificationMappingPending')
        }
        $scope.mapping_institution = function () {
            $location.url('app/MstSAVerificationMappingPending')
        }
        $scope.mapping_individual = function () {
            $location.url('app/MstSAVerificationIndividualMappingPending')
        }
        $scope.institution_deferred = function () {
            $location.url('app/MstSAVerificationInstitutionDeferredSummary')
        }
        $scope.individual_deferred = function () {
            $location.url('app/MstSAVerificationIndividualDeferredSummary')
        }
        $scope.mappinginformation = function (sacontact_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/Approvalinformation.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'lg'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    sacontact_gid: sacontact_gid
                }
                var url = 'api/MstSAOnboardingIndividual/GetAssignedInformation';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.lblmaker_name = resp.data.employee_name;
                    $scope.lblchecker_name = resp.data.checkeremployee_name;

                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
            }
        }


        $scope.saverificationedit = function (sacontact_gid) {
            $location.url('app/MstSAVerificationInstitutionEdit?hash=' + cmnfunctionService.encryptURL('lssacontact_gid=' + sacontact_gid));
        }
        $scope.deferredview = function (sacontact_gid) {
            $location.url('app/MstSAVerificationIndividualDeferredView?hash=' + cmnfunctionService.encryptURL('lssacontact_gid=' + sacontact_gid));

        }
    }
})();


