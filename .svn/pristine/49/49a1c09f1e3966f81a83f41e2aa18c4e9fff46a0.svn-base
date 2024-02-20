(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstGradingToolAddController', MstGradingToolAddController);

    MstGradingToolAddController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function MstGradingToolAddController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstGradingToolAddController';
        var application_gid = $location.search().application_gid;
        activate();
        function activate() {

            $scope.add = true;
            // Calender Popup... //

            vm.calender1 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();

                vm.open1 = true;
            };
            vm.formats = ['dd-MM-yyyy'];
            vm.format = vm.formats[0];
            vm.dateOptions = {
                formatYear: 'yy',
                startingDay: 1
            };
            var url = 'api/MstApplicationGradingTool/DeletetmpGradingTool';
            SocketService.get(url).then(function (resp) {
            

            });
            var params = {
                application_gid: application_gid,
                statusupdated_by: 'RM',
            }
            var url = 'api/MstApplicationGradingTool/GetGradingTool';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.gradingtool_list = resp.data.grading_list;

            });

        }
   
        var url = 'api/MstApplicationGradingTool/GetAssessmentCriteriaDropDown';
        SocketService.get(url).then(function (resp) {
            $scope.assessmentcriteria_list = resp.data.criteria_list;
        });
        
        $scope.editAssessmentCriteria = function (val) {
            //    $location.url('app/MstAssessmentCriteriaDetailsEdit?application2gradingassesment_gid=' + val + '&application_gid=' + application_gid + '&lspage=add');
            var params = {
                application2gradingassesment_gid: val
            }
            localStorage.setItem('application2gradingassesment_gid', val);

            var url = 'api/MstApplicationGradingTool/EditAssessmentCriteriaDetails';
            SocketService.getparams(url, params).then(function (resp) {

                $scope.edittxtmax_score = resp.data.maximum_score;
                $scope.edittxtactual_score = resp.data.actual_score;
                $scope.edittxtin = resp.data.assessment_in;
                $scope.edittxtin_grade = resp.data.assessment_ingrade;
                $scope.edittxtshareholders_male = resp.data.shareholders_male;
                $scope.edittxtshareholders_female = resp.data.shareholders_female;
                $scope.edittxtbods_male = resp.data.bodmale_in;
                $scope.edittxtbods_female = resp.data.bodfemale_in;
                $scope.edittxtapplication2gradingtool_gid = resp.data.application2gradingtool_gid;
                $scope.edittxtapplication_gid = resp.data.application_gid;
                $scope.gradingtool_list = resp.data.gradingtool_list;
                $scope.assessmentcriteria_listedit = resp.data.assessmentcriteria_list;

                $scope.editcboassessmentcriteria_name = [];
                if (resp.data.gradingtool_list != null) {
                    var count = resp.data.gradingtool_list.length;
                    for (var i = 0; i < count; i++) {
                        var indexs = $scope.assessmentcriteria_listedit.map(function (x) { return x.assessmentcriteria_gid; }).indexOf(resp.data.gradingtool_list[i].assessmentcriteria_gid);
                        $scope.editcboassessmentcriteria_name.push($scope.assessmentcriteria_listedit[indexs]);
                    }
                }
            });
            $scope.edit = true;
            $scope.add = false;
            var params = {
                application_gid: application_gid,
                statusupdated_by: 'RM',
            }
            var url = 'api/MstApplicationGradingTool/GetGradingTool';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.gradingtool_list = resp.data.grading_list;

            });
        }
        
        $scope.Cancel = function()
        {
            $scope.add = true;
            $scope.edit = false;
            var params = {
                application_gid: application_gid,
                statusupdated_by: 'RM',
            }
            var url = 'api/MstApplicationGradingTool/GetGradingTool';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.gradingtool_list = resp.data.grading_list;

            });
        }

        $scope.assessmentUpdate = function ()
        {
            if ($scope.editcboassessmentcriteria_name == '' || $scope.editcboassessmentcriteria_name == null || $scope.editcboassessmentcriteria_name == undefined) {
                Notify.alert('Select Assesment Criteria', 'warning');
            }

            else {
                $scope.application2gradingassesment_gid = localStorage.getItem('application2gradingassesment_gid');
                var params = {
                    application2gradingassesment_gid: $scope.application2gradingassesment_gid,
                    gradingtool_list: $scope.editcboassessmentcriteria_name,
                    maximum_score: $scope.edittxtmax_score,
                    actual_score: $scope.edittxtactual_score,
                    assessment_in: $scope.edittxtin,
                    assessment_ingrade: $scope.edittxtin_grade,
                    shareholders_male: $scope.edittxtshareholders_male,
                    shareholders_female: $scope.edittxtshareholders_female,
                    bodmale_in: $scope.edittxtbods_male,
                    bodfemale_in: $scope.edittxtbods_female
                };
                var url = "api/MstApplicationGradingTool/UpdateAssessmentCriteriaDetails";
                lockUI();
                SocketService.post(url, params).then(function (resp) {
                    unlockUI();
                    if (resp.data.status == true) {

                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        assessmentcriteria_list();
                        $scope.cboassessmentcriteria_name = '';
                        $scope.txtfpoac_score = '';
                        $scope.txtmax_score = '';
                        $scope.txtactual_score = '';
                        $scope.txtin = '';
                        $scope.txtin_grade = '';
                        $scope.txtshareholders_male = '';
                        $scope.txtshareholders_female = '';
                        $scope.txtbods_male = '';
                        $scope.txtbods_female = '';
                        $scope.add = true;
                        $scope.edit = false;
                        var params = {
                            application_gid: application_gid,
                            statusupdated_by: 'RM',
                        }
                        var url = 'api/MstApplicationGradingTool/GetGradingTool';
                        SocketService.getparams(url, params).then(function (resp) {
                            $scope.gradingtool_list = resp.data.grading_list;

                        });
                    }
                    else {
                        Notify.alert(resp.data.message, {
                            status: 'warning',
                            pos: 'top-center',
                            timeout: 3000
                        });
                    }


                });
            }
        }

        $scope.assessmentcriteria_add = function () {
            if ($scope.cboassessmentcriteria_name == '' || $scope.cboassessmentcriteria_name == null || $scope.cboassessmentcriteria_name == undefined) {
                Notify.alert('Select Assesment Criteria', 'warning');
            }

            else {
                var params = {

                    gradingtool_list: $scope.cboassessmentcriteria_name,
                    maximum_score: $scope.txtmax_score,
                    actual_score: $scope.txtactual_score,
                    assessment_in: $scope.txtin,
                    assessment_ingrade: $scope.txtin_grade,
                    shareholders_male: $scope.txtshareholders_male,
                    shareholders_female: $scope.txtshareholders_female,
                    bodmale_in: $scope.txtbods_male,
                    bodfemale_in: $scope.txtbods_female
                };
                var url = 'api/MstApplicationGradingTool/SubmitGradingassesment';
                lockUI();
                SocketService.post(url, params).then(function (resp) {
                    unlockUI();
                    if (resp.data.status == true) {

                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        assessmentcriteria_list();
                        $scope.cboassessmentcriteria_name = '';
                        $scope.txtfpoac_score = '';
                        $scope.txtmax_score = '';
                        $scope.txtactual_score = '';
                        $scope.txtin = '';
                        $scope.txtin_grade = '';
                        $scope.txtshareholders_male = '';
                        $scope.txtshareholders_female = '';
                        $scope.txtbods_male = '';
                        $scope.txtbods_female = '';
                        var params = {
                            application_gid: application_gid,
                            statusupdated_by: 'RM',
                        }
                        var url = 'api/MstApplicationGradingTool/GetGradingTool';
                        SocketService.getparams(url, params).then(function (resp) {
                            $scope.gradingtool_list = resp.data.grading_list;

                        });
                    }
                    else {
                        Notify.alert(resp.data.message, {
                            status: 'warning',
                            pos: 'top-center',
                            timeout: 3000
                        });
                    }


                });
            }
        }

        function assessmentcriteria_list() {
            var url = 'api/MstApplicationGradingTool/GettmpGradingTool';
            SocketService.get(url).then(function (resp) {
                $scope.assesment_list = resp.data.gradingtool_list;

            });
        }
        
        $scope.save_gradingtool = function () {
            if ($scope.txtdateofsurvey_date == '' || $scope.txtdateofsurvey_date == null || $scope.txtdateofsurvey_date == undefined) {
                Notify.alert('Select Date Of Survey', 'warning');
            }

            else {
            var params = {
                application_gid : application_gid,
                dateofsurvey: $scope.txtdateofsurvey_date,
                overallfporating: $scope.txtoverallfpo_rating,
                overallfpograding: $scope.txtoverallfpo_grade,
                majorcrops: $scope.txtmajor_crops,
                alternativeincomesource: $scope.txtalternative_income,
                objevtiveoffpo: $scope.txtobjective_FPO,
                recommendation: $scope.txtrecommendation,
                fpo_acscore: $scope.txtfpoac_score,
                numnerofaactive_fig: $scope.txtnumber_activefigs,
                existinglending_directindirect: $scope.txtn0_existingleading,
                nonnegotiableconditions_met: $scope.txtnon_negotiable,
                outstandingportfolio_directindirect : $scope.txtout_portfolio,
                institution_directindrectborrowing : $scope.txtout_portfolio1,
                totaldisbursements_otherlenders: $scope.txtpar90_lending,
                par90_managedbyonlyinstitution_direct: $scope.txtpar_90groups,
                recommendation1: $scope.txtrecommendation1,
                gradingtool_list: $scope.cboassessmentcriteria_name,
                maximum_score: $scope.txtmax_score,
                actual_score: $scope.txtactual_score,
                assessment_in: $scope.txtin,
                assessment_ingrade: $scope.txtin_grade,
                shareholders_male: $scope.txtshareholders_male,
                shareholders_female: $scope.txtshareholders_female,
                bodmale_in: $scope.txtbods_male,
                bodfemale_in: $scope.txtbods_female,
                numberofstates: $scope.txtno_states,
                numberofdistricts: $scope.txtno_districts,
                numberofbranches: $scope.txtno_branches,
                numberofmembers: $scope.txtno_members,
                numberof_activemembers: $scope.txtno_activemembers,
                numberofgroups: $scope.txtno_groups,
                zonaloffices: $scope.txtzonal_offices,
                regionaloffices: $scope.txtreginal_offices,
                branches: $scope.txtbranches,
                adminstaff: $scope.txtadmin_staff,
                fieldstaff: $scope.txtfield_staff,
                fieldstaff_ratio: $scope.txtfield_togroupratio,
                statusupdated_by: 'RM',

            };
            var url = 'api/MstApplicationGradingTool/SaveasDraftGradingToolDetails';
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {
                    var params = {
                        application_gid: application_gid,
                        statusupdated_by: 'RM',
                    }
                    var url = 'api/MstApplicationGradingTool/GetGradingTool';
                    SocketService.getparams(url, params).then(function (resp) {
                        $scope.gradingtool_list = resp.data.gradingtool_list;

                    });
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    assessmentcriteria_list()
                    $scope.txtdateofsurvey_date = ' ';
                    $scope.txtoverallfpo_rating = '';
                    $scope.txtoverallfpo_grade = '';
                    $scope.txtmajor_crops = '';
                    $scope.txtalternative_income = '';
                    $scope.txtobjective_FPO = '';
                    $scope.txtrecommendation = '';
                    $scope.txtfpoac_score = '';
                    $scope.txtnumber_activefigs = '';
                    $scope.txtn0_existingleading = '';
                    $scope.txtnon_negotiable = '';
                    $scope.txtout_portfolio = '';
                    $scope.txtout_portfolio1 = '';
                    $scope.txtpar90_lending = '';
                    $scope.txtpar_90groups = '';
                    $scope.txtrecommendation1 = '';
                    $scope.txtno_states = '';
                    $scope.txtno_districts = '';
                    $scope.txtno_branches = '';
                    $scope.txtno_members = '';
                    $scope.txtno_activemembers = '';
                    $scope.txtno_groups = '';
                    $scope.txtzonal_offices = '';
                    $scope.txtreginal_offices = '';
                    $scope.txtbranches = '';
                    $scope.txtadmin_staff = '';
                    $scope.txtfield_staff = '';
                    $scope.txtfield_togroupratio = '';
                    activate();
                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'warning',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
              
            });
        }
    }
      
        $scope.submitgrading = function () {
            var params = {
                application_gid : application_gid,
                dateofsurvey: $scope.txtdateofsurvey_date,
                overallfporating: $scope.txtoverallfpo_rating,
                overallfpograding: $scope.txtoverallfpo_grade,
                majorcrops: $scope.txtmajor_crops,
                alternativeincomesource: $scope.txtalternative_income,
                objevtiveoffpo: $scope.txtobjective_FPO,
                recommendation: $scope.txtrecommendation,
                fpo_acscore: $scope.txtfpoac_score,
                numnerofaactive_fig: $scope.txtnumber_activefigs,
                existinglending_directindirect: $scope.txtn0_existingleading,
                nonnegotiableconditions_met: $scope.txtnon_negotiable,
                outstandingportfolio_directindirect: $scope.txtout_portfolio,
                institution_directindrectborrowing: $scope.txtout_portfolio1,
                totaldisbursements_otherlenders: $scope.txtpar90_lending,
                par90_managedbyonlyinstitution_direct: $scope.txtpar_90groups,
                recommendation1: $scope.txtrecommendation1,
                gradingtool_list: $scope.cboassessmentcriteria_name,
                maximum_score: $scope.txtmax_score,
                actual_score: $scope.txtactual_score,
                assessment_in: $scope.txtin,
                assessment_ingrade: $scope.txtin_grade,
                shareholders_male: $scope.txtshareholders_male,
                shareholders_female: $scope.txtshareholders_female,
                bodmale_in: $scope.txtbods_male,
                bodfemale_in: $scope.txtbods_female,
                numberofstates: $scope.txtno_states,
                numberofdistricts: $scope.txtno_districts,
                numberofbranches: $scope.txtno_branches,
                numberofmembers: $scope.txtno_members,
                numberof_activemembers: $scope.txtno_activemembers,
                numberofgroups: $scope.txtno_groups,
                zonaloffices: $scope.txtzonal_offices,
                regionaloffices: $scope.txtreginal_offices,
                branches: $scope.txtbranches,
                adminstaff: $scope.txtadmin_staff,
                fieldstaff: $scope.txtfield_staff,
                fieldstaff_ratio: $scope.txtfield_togroupratio,
                statusupdated_by: 'RM',

            };
            var url = 'api/MstApplicationGradingTool/SubmitGradingToolDetails';
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {
                    var params = {
                        application_gid: application_gid,
                        statusupdated_by: 'RM',
                    }
                    var url = 'api/MstApplicationGradingTool/GetGradingTool';
                    SocketService.getparams(url, params).then(function (resp) {
                        $scope.gradingtool_list = resp.data.gradingtool_list;

                    });
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    assessmentcriteria_list()
                    $scope.txtdateofsurvey_date = ' ';
                    $scope.txtoverallfpo_rating = '';
                    $scope.txtoverallfpo_grade = '';
                    $scope.txtmajor_crops = '';
                    $scope.txtalternative_income = '';
                    $scope.txtobjective_FPO = '';
                    $scope.txtrecommendation = '';
                    $scope.txtfpoac_score = '';
                    $scope.txtnumber_activefigs = '';
                    $scope.txtn0_existingleading = '';
                    $scope.txtnon_negotiable = '';
                    $scope.txtout_portfolio = '';
                    $scope.txtout_portfolio1 = '';
                    $scope.txtpar90_lending = '';
                    $scope.txtpar_90groups = '';
                    $scope.txtrecommendation1 = '';
                    $scope.txtno_states = '';
                    $scope.txtno_districts = '';
                    $scope.txtno_branches = '';
                    $scope.txtno_members = '';
                    $scope.txtno_activemembers = '';
                    $scope.txtno_groups = '';
                    $scope.txtzonal_offices = '';
                    $scope.txtreginal_offices = '';
                    $scope.txtbranches = '';
                    $scope.txtadmin_staff = '';
                    $scope.txtfield_staff = '';
                    $scope.txtfield_togroupratio = '';
                    activate();
                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'warning',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
               
            });
        }
      
        $scope.Back = function () {
            $state.go('app.MstApplicationCreationSummary');
        }
      
        $scope.gradingtooldetails_edit = function (val)
        {
            $location.url('app/MstGradingToolEdit?application2gradingtool_gid=' + val + '&application_gid='+ application_gid );
        }

        $scope.gradingtooldetails_view = function (val) {
            $location.url('app/MstGradingToolView?application2gradingtool_gid=' + val + '&application_gid=' + application_gid);
        }

        $scope.assessmentcriteriadetails_delete = function (application2gradingassesment_gid) {
            var params = {
                application2gradingassesment_gid: application2gradingassesment_gid
            }
            SweetAlert.swal({
                title: 'Are you sure?',
                text: 'Do You Want To Delete the Record ?',
                showCancelButton: true,
                confirmButtonColor: '#DD6B55',
                confirmButtonText: 'Yes, delete it!',
                closeOnConfirm: false
            }, function (isConfirm) {
                if (isConfirm) {
                    var url = 'api/MstApplicationGradingTool/Deletegradingassesment';
                    SocketService.getparams(url, params).then(function (resp) {
                        if (resp.data.status == true) {
                            assessmentcriteria_list();
                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            assessmentcriteria_list();
                          
                        }
                    });
                    SweetAlert.swal('Deleted Successfully!');
                }

            });
        };
   
    }
})();

