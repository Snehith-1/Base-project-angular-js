(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstSAOnboardingCodeApprovalInsSummaryController', MstSAOnboardingApprovalInsSummaryController);

    MstSAOnboardingApprovalInsSummaryController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function MstSAOnboardingApprovalInsSummaryController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstSAOnboardingCodeApprovalInsSummaryController';

        activate();

        function activate() {
            var url = 'api/MstSAOnboardingInstitution/GetSaApprovalInitiatedSummary';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.saOnboardSummary_list = resp.data.saOnboardSummary_list;
                unlockUI();
            });
            var url = 'api/MstSAOnboardingInstitution/GetSaApproverCounts';
            SocketService.get(url).then(function (resp) {
                unlockUI()
                $scope.institution_count = resp.data.institution_count;
                $scope.individual_count = resp.data.individual_count;
                $scope.institutioninitiated_count = resp.data.institutioninitiated_count;
                $scope.individualinitiated_count = resp.data.individualinitiated_count;

            });
        }
        $scope.viewInstitution = function () {
            $state.go('app.MstSAOnboardingInstitutionApproval');
        }

        $scope.institution_approved = function () {
            $state.go('app.MstSAOnboardingCodeApprovalInsSummary');
        }
        $scope.institution_pending = function () {
            $state.go('app.MstSAApprovalInsCodePendingSummary');
        }

        $scope.individual_approved = function () {
            $state.go('app.MstSAOnboardingCodeApprovalIndSummary');
        }
        $scope.individual_pending = function () {
            $state.go('app.MstSAApprovalIndCodePendingSummary');
        }

        $scope.saonboardingverification = function (sacontactinstitution_gid) {

            // $location.url('app/MstSAOnboardingInstitutionVerification');

            $location.url('app/MstSAOnboardingInstitutionCodecreation?lssacontactinstitution_gid=' + sacontactinstitution_gid + '&lspage=InstituteInitiate');
        }
    }
})();
