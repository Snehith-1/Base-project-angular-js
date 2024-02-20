(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstAssessmentCriteriaDetailsEditController', MstAssessmentCriteriaDetailsEditController);

    MstAssessmentCriteriaDetailsEditController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function MstAssessmentCriteriaDetailsEditController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstAssessmentCriteriaDetailsEditController';
        var application_gid = $location.search().application_gid;
        var application2gradingtool_gid = $location.search().application2gradingtool_gid;
        var application2gradingassesment_gid = $location.search().application2gradingassesment_gid;
        var lspage = $location.search().lspage;
        activate();

        function activate() {

            var params = {
                application2gradingassesment_gid: application2gradingassesment_gid
            }

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
        }
        var url = 'api/MstApplicationGradingTool/GetAssessmentCriteriaDropDown';
        SocketService.get(url).then(function (resp) {
            $scope.assessmentcriteria_list = resp.data.criteria_list;
        });
       
        $scope.assessmentUpdate = function () {


                var params = {
                    application2gradingassesment_gid: application2gradingassesment_gid,
                    gradingtool_list: $scope.editcboassessmentcriteria_name,
                    maximum_score: $scope.edittxtmax_score,
                    actual_score: $scope.edittxtactual_score,
                    assessment_in: $scope.edittxtin,
                    assessment_ingrade: $scope.edittxtin_grade,
                    shareholders_male: $scope.edittxtshareholders_male,
                    shareholders_female: $scope.edittxtshareholders_female,
                    bodmale_in: $scope.edittxtbods_male,
                    bodfemale_in: $scope.edittxtbods_female

                }


                var url = "api/MstApplicationGradingTool/UpdateAssessmentCriteriaDetails";
                lockUI();
                SocketService.post(url, params).then(function (resp) {
                    unlockUI();
                    if (resp.data.status == true) {
                        if (lspage == "add") {
                            $location.url('app/MstGradingToolAdd?application_gid=' + application_gid);
                        }
                        else if (lspage == "edit") {
                            $location.url('app/MstGradingToolEdit?application2gradingtool_gid=' + application2gradingtool_gid + '&application_gid=' + application_gid);
                        };
                        Notify.alert(resp.data.message, 'success');
                    }
                    else {
                        Notify.alert(resp.data.message, 'warning');
                    }
                });
                
            }
        
        $scope.back = function () {
            if (lspage == "add")
            {
                $location.url('app/MstGradingToolAdd?application_gid=' + application_gid);
            }
            else if (lspage == "edit") {
                $location.url('app/MstGradingToolEdit?application2gradingtool_gid=' + application2gradingtool_gid + '&application_gid=' + application_gid);
            }
           
        }
    }
})();