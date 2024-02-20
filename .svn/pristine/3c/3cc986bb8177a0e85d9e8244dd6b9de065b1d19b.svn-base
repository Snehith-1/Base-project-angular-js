(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AgrGradingToolViewController', AgrGradingToolViewController);

        AgrGradingToolViewController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];


    function AgrGradingToolViewController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AgrGradingToolViewController';
        var application_gid = $location.search().application_gid;
        var application2gradingtool_gid = $location.search().application2gradingtool_gid;
        activate();

        function activate() {
            var params = {
                application2gradingtool_gid: application2gradingtool_gid

            }
            var url = 'api/AgrMstApplicationGradingTool/GetEditGradingToolassesment';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.assesment_list = resp.data.gradingtool_list;

            });
            var url = 'api/AgrMstApplicationGradingTool/GetEditGradingTooltotal';
            SocketService.getparams(url, params).then(function (resp) {
                if (resp.data.gradingdraft_flag == "Y") {
                    $scope.draft = true;
                    $scope.update = false;
                }
                else {
                    $scope.draft = false;
                    $scope.update = true;
                }
                $scope.txtdateofsurvey_date = resp.data.dateofsurvey;
                $scope.txtoverallfpo_rating = resp.data.overallfporating;
                $scope.txtoverallfpo_grade = resp.data.overallfpograding;
                $scope.txtmajor_crops = resp.data.majorcrops;
                $scope.txtalternative_income = resp.data.alternativeincomesource;
                $scope.txtobjective_FPO = resp.data.objevtiveoffpo;
                $scope.txtrecommendation = resp.data.recommendation;
                $scope.txtfpoac_score = resp.data.fpo_acscore;
                $scope.txtnumber_activefigs = resp.data.numnerofaactive_fig;
                $scope.txtn0_existingleading = resp.data.existinglending_directindirect;
                $scope.txtnon_negotiable = resp.data.nonnegotiableconditions_met;
                $scope.txtout_portfolio = resp.data.outstandingportfolio_directindirect;
                $scope.txtout_portfolio1 = resp.data.institution_directindrectborrowing;
                $scope.txtpar90_lending = resp.data.totaldisbursements_otherlenders;
                $scope.txtpar_90groups = resp.data.par90_managedbyonlyinstitution_direct;
                $scope.txtrecommendation1 = resp.data.recommendation1;
                $scope.cboassessmentcriteria_name = resp.data.gradingtool_list;
                $scope.txtmax_score = resp.data.maximum_score;
                $scope.txtactual_score = resp.data.actual_score;
                $scope.txtin = resp.data.assessment_in;
                $scope.txtin_grade = resp.data.assessment_ingrade;
                $scope.txtshareholders_male = resp.data.shareholders_male;
                $scope.txtshareholders_female = resp.data.shareholders_female;
                $scope.txtbods_male = resp.data.bodmale_in;
                $scope.txtbods_female = resp.data.bodfemale_in;
                $scope.txtno_states = resp.data.numberofstates;
                $scope.txtno_districts = resp.data.numberofdistricts;
                $scope.txtno_branches = resp.data.numberofbranches;
                $scope.txtno_members = resp.data.numberofmembers;
                $scope.txtno_activemembers = resp.data.numberof_activemembers;
                $scope.txtno_groups = resp.data.numberofgroups;
                $scope.txtzonal_offices = resp.data.zonaloffices;
                $scope.txtreginal_offices = resp.data.regionaloffices;
                $scope.txtbranches = resp.data.branches;
                $scope.txtadmin_staff = resp.data.adminstaff;
                $scope.txtfield_staff = resp.data.fieldstaff;
                $scope.txtfield_togroupratio = resp.data.fieldstaff_ratio;

            });

        }

        $scope.Back = function () {
            $location.url('app/AgrGradingToolAdd?application_gid=' + application_gid);
        }


        $scope.ViewAssessmentCriteria = function (val) {
            $location.url('app/AgrAssessmentCriteriaDetailsView?application2gradingassesment_gid=' + val + '&application2gradingtool_gid=' + application2gradingtool_gid + '&application_gid=' + application_gid);

        }
    }
})();
