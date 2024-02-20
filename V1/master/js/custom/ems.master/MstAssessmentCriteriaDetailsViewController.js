(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstAssessmentCriteriaDetailsViewController', MstAssessmentCriteriaDetailsViewController);

    MstAssessmentCriteriaDetailsViewController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];


    function MstAssessmentCriteriaDetailsViewController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstAssessmentCriteriaDetailsViewController';
        var application_gid = $location.search().application_gid;
        var application2gradingtool_gid = $location.search().application2gradingtool_gid;
        var application2gradingassesment_gid = $location.search().application2gradingassesment_gid;
       
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
                $scope.assessmentcriteria_name = resp.data.assessmentcriteria_name;
              
            });
        }


        $scope.back = function () {
           
                $location.url('app/MstGradingToolView?application2gradingtool_gid=' + application2gradingtool_gid + '&application_gid=' + application_gid);
            

        }
    }
})();
