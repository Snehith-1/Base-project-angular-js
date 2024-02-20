(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AgrTrnApplCreationGradingToolViewController', AgrTrnApplCreationGradingToolViewController);

    AgrTrnApplCreationGradingToolViewController.$inject = ['$rootScope', '$scope', '$state', '$modal', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route'];

    function AgrTrnApplCreationGradingToolViewController($rootScope, $scope, $state, $modal, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AgrTrnApplCreationGradingToolViewController';
        var application2gradingtool_gid = localStorage.getItem('application2gradingtool_gid');
        lockUI();
        activate();

        function activate() {

            var params = {
                application2gradingtool_gid: application2gradingtool_gid
            }
            var url = 'api/AgrMstApplicationView/GetGradingView';

            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.txtfpo_acscore = resp.data.fpo_acscore;
                $scope.txtno_activefig = resp.data.numnerofaactive_fig;
                $scope.txtno_existingrate = resp.data.existinglending_directandindirect;
                $scope.txtnon_negotiable = resp.data.nonnegotiableconditions_met;
                $scope.txtoutstanding_member = resp.data.outstandingportfolio_directandindirect;
                $scope.txtdoes_indirectborrowing = resp.data.institution_directandindrectborrowing;
                $scope.txt_totallender = resp.data.totaldisbursements_otherlenders;
                $scope.txtpar90_group = resp.data.par90_managedbyonlyinstitution_direct;
                $scope.txtrecommendation = resp.data.fpo_recommendation;
                $scope.txtdate_survey = resp.data.dateofsurvey;
                $scope.txtoverall_fporating = resp.data.overallfporating;
                $scope.txtoverall_fpograde = resp.data.overallfpograde;
                $scope.txtmajor_crops = resp.data.majorcrops;
                $scope.txtalternative_income = resp.data.alternativeincomesource;
                $scope.txtobjective_fpo = resp.data.objevtiveoffpo;
                $scope.txtreportrecommendation = resp.data.recommendation;
                $scope.txtno_states = resp.data.numberofstates;
                $scope.txtno_districts = resp.data.numberofdistricts;
                $scope.txtno_branches = resp.data.numberofbranches;
                $scope.txtno_members = resp.data.numberofmembers;
                $scope.txtno_activemembers = resp.data.numberof_activemembers;
                $scope.txtno_groups = resp.data.numberofgroups;
                $scope.txtzonal_offices = resp.data.zonaloffices;
                $scope.txtregional_offices = resp.data.regionaloffices;
                $scope.txtbranches = resp.data.branches;
                $scope.txtadmin_staff = resp.data.adminstaff;
                $scope.txtfield_staff = resp.data.fieldstaff;
                $scope.txtfield_groupratio = resp.data.fieldstaff_ratio;
                $scope.assessment_list = resp.data.mstassessment_list;

            });

        }

        $scope.close = function () {
            window.close();
        }


    }
})();
