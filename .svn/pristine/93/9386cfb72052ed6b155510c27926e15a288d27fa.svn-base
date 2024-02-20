(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstSAOnboardingIndividualRenewalGroupingController', MstSAOnboardingIndividualRenewalGroupingController);

    MstSAOnboardingIndividualRenewalGroupingController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', 'cmnfunctionService'];

    function MstSAOnboardingIndividualRenewalGroupingController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstSAOnboardingIndividualRenewalGroupingController';

        var searchObject = cmnfunctionService.decryptURL($location.search().hash);
        $scope.samfin_code = searchObject.samfin_code;
        var samfin_code = $scope.samfin_code;
        $scope.samagro_code = searchObject.samagro_code;
        var samagro_code = $scope.samagro_code;
        $scope.sacontact_gid = searchObject.lssacontact_gid;
        var sacontact_gid = $scope.sacontact_gid;

        activate();
        lockUI();
        function activate() {
            var params = {
                samfin_code: samfin_code,
                samagro_code: samagro_code,
                sacontact_gid: sacontact_gid


            }

            var url = 'api/MstSAOnboardingIndividual/GetIndividualRenewalGroupingSummary';
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
        $scope.renewalindividual = function (sacontact_gid, saentitytype_gid) {
            $location.url('app/MstSAOnboardingIndividualRenewal?hash=' + cmnfunctionService.encryptURL('lssacontact_gid=' + sacontact_gid + '&saentitytype_gid=' + saentitytype_gid + '&samfin_code=' + samfin_code + '&samagro_code=' + samagro_code));
        }
        $scope.renewal = function (sacontact_gid, saentitytype_gid) {
            $location.url('app/MstSAOnboardingIndividualRenewalEdit?hash=' + cmnfunctionService.encryptURL('lssacontact_gid=' + sacontact_gid + '&saentitytype_gid=' + saentitytype_gid + '&samfin_code=' + samfin_code + '&samagro_code=' + samagro_code));
        }
        $scope.back = function () {

            $state.go('app.MstSAOnboardingIndividualGrouping');


        }

    }
})();
