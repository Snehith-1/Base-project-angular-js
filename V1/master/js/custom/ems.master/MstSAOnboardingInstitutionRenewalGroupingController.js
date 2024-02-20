(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstSAOnboardingInstitutionRenewalGroupingController', MstSAOnboardingInstitutionRenewalGroupingController);

    MstSAOnboardingInstitutionRenewalGroupingController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', 'cmnfunctionService'];

    function MstSAOnboardingInstitutionRenewalGroupingController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstSAOnboardingInstitutionRenewalGroupingController';

        var searchObject = cmnfunctionService.decryptURL($location.search().hash);
        $scope.samfin_code = searchObject.samfin_code;
        var samfin_code = $scope.samfin_code;
        $scope.samagro_code = searchObject.samagro_code;
        var samagro_code = $scope.samagro_code;
        $scope.sacontactinstitution_gid = searchObject.lssacontactinstitution_gid;
        var sacontactinstitution_gid = $scope.sacontactinstitution_gid;

        activate();
        lockUI();
        function activate() {
            var params = {
                samfin_code: samfin_code,
                samagro_code: samagro_code,
                sacontactinstitution_gid: sacontactinstitution_gid


            }

            var url = 'api/MstSAOnboardingInstitution/GetSaInstitutionRenewalGroupingSummary';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.saOnboardSummary_list = resp.data.saOnboardSummary_list;
            });

            //var url = 'api/MstCAD/GetRMRnewalApplicationSummary';
            //SocketService.getparams(url, params).then(function (resp) {
            //    unlockUI();
            //    $scope.rmrenewalapplication_list = resp.data.cadapplicationlist;
            //});
        }
        $scope.renewalinstitution = function (sacontactinstitution_gid,saentitytype_gid) {
            $location.url('app/MstSAOnboardingInstitutionRenewal?hash=' + cmnfunctionService.encryptURL('lssacontactinstitution_gid=' + sacontactinstitution_gid + '&saentitytype_gid=' + saentitytype_gid + '&samfin_code=' + samfin_code + '&samagro_code=' + samagro_code));
        }
        $scope.renewal = function (sacontactinstitution_gid, saentitytype_gid) {
            $location.url('app/MstSAOnboardingInstitutionRenewalEdit?hash=' + cmnfunctionService.encryptURL('lssacontactinstitution_gid=' + sacontactinstitution_gid + '&saentitytype_gid=' + saentitytype_gid + '&samfin_code=' + samfin_code + '&samagro_code=' + samagro_code));
        }
        $scope.back = function () {

            $state.go('app.MstSAOnboardingInstitutionGrouping');


        }

    }
})();
