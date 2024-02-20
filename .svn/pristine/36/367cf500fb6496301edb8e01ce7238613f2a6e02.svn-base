(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstSBACodeCreationSummaryCompletedController', MstSBACodeCreationSummaryCompletedController);

    MstSBACodeCreationSummaryCompletedController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', 'DownloaddocumentService', 'cmnfunctionService'];

    function MstSBACodeCreationSummaryCompletedController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, DownloaddocumentService, cmnfunctionService) {
        /* jshint validthis:true **/
        var vm = this;
        vm.title = 'MstSBACodeCreationSummaryCompletedController';

        activate();

        function activate() {
            var url = 'api/MstSAOnboardingInstitution/GetcodecreationCompletedSummary';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.saOnboardSummary_list = resp.data.saOnboardSummary_list;
                unlockUI();
            });
            var url = 'api/MstSAOnboardingInstitution/GetSacodecreationCounts';
            SocketService.get(url).then(function (resp) {
                unlockUI()
                $scope.institution_count = resp.data.institution_count;
                $scope.individual_count = resp.data.individual_count;
                $scope.institutioninitiated_count = resp.data.institutioninitiated_count;
                $scope.individualinitiated_count = resp.data.individualinitiated_count;

            });
        }
        $scope.viewIndividual = function (sacontactinstitution_gid) {
            $location.url('app/MstSAOnboardingInstitutionCodeCreateView?hash=' + cmnfunctionService.encryptURL('lssacontactinstitution_gid=' + sacontactinstitution_gid + '&lspage=Institutioncodecreate'));
        }

        $scope.institution_pending = function () {
            $state.go('app.MstSBACodeCreationSummary');///app.MstSAOnboardingCodeApprovalInsSummary
        }
        $scope.institution_completed = function () {
            $state.go('app.MstSBACodeCreationCompletedSummary');///app.MstSAOnboardingCodeApprovalInsSummary
        }
        $scope.individual_completed = function () {
            $location.url('app/MstSAOnboardingCodeApprovalIndCompletedSummary')
        }

        $scope.individual_pending = function () {
            $state.go('app.MstSAOnboardingCodeApprovalIndSummary');
        }
      
       

        $scope.saonboardingverification = function (sacontactinstitution_gid) {

            // $location.url('app/MstSAOnboardingInstitutionVerification');

            $location.url('app/MstSAOnboardingInstitutionCodecreation?hash=' + cmnfunctionService.encryptURL('lssacontactinstitution_gid=' + sacontactinstitution_gid + '&lspage=InstituteInitiate'));
        }
        $scope.InstitutionCodecompletedReport = function () {
            lockUI();
            var url = 'api/MstSAOnboardingInstitution/InstitutionCodeCompletedReport';
            SocketService.get(url).then(function (resp) {
                if (resp.data.status == true) {
                    unlockUI();
                    DownloaddocumentService.Downloaddocument(resp.data.lscloudpath, resp.data.lsname);

                    //var phyPath = resp.data.lspath;
                    //var relPath = phyPath.split("EMS");
                    //var relpath1 = relPath[1].replace("\\", "/");
                    //var hosts = window.location.host;
                    //var prefix = location.protocol + "//";
                    //var str = prefix.concat(hosts, relpath1);
                    //var link = document.createElement("a");
                    //var name = resp.data.lsname.split('.');
                    //link.download = name[0];
                    //var uri = str;
                    //link.href = uri;
                    //link.click();
                }
                else {
                    unlockUI();
                    Notify.alert('Error Occurred While Exporting !', 'warning')

                }

            });
        }
    }
})();