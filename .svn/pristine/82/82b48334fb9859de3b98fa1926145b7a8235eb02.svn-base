(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstVerticalApplicantTypeRulecontroller', MstVerticalApplicantTypeRulecontroller);

        MstVerticalApplicantTypeRulecontroller.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function MstVerticalApplicantTypeRulecontroller($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstVerticalApplicantTypeRulecontroller';
        var vertical_gid = $location.search().lsvertical_gid;
        activate();

        function activate() {
            $scope.editquesorder = true;
            var params = {
                vertical_gid: vertical_gid
            }
            var url = 'api/MstCreditMapping/GetVerticalGroupTitleListAppType';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                $scope.vertical_code = resp.data.vertical_code;
                $scope.vertical_name = resp.data.vertical_name;
                // $scope.GroupTitle_list = resp.data.GroupTitle_dtl;
                GetVerticalquestionsummarylist();
                unlockUI();
            });

            // $scope.answertype = [
            //     { id: 0, answer_typename: 'Calculation' },
            //     { id: 1, answer_typename: 'List' },
            //     { id: 2, answer_typename: 'Number' },
            //     { id: 3, answer_typename: '' }
            // ]

            $scope.applicant_typelist = [
                { id: 0, applicant_type: 'Company' },
                { id: 1, applicant_type: 'Individual' },
             /*   { id: 2, applicant_type: 'Group' },*/
                { id: 3, applicant_type: '' }
            ]

          
        }

        $scope.editapplicanttypeRule = function (vertical_gid, verticalapplicanttyperule_gid, applicant_type) {
            $location.url('app/MstVerticalRule?lsvertical_gid=' + vertical_gid + '&lsverticalapplicanttyperule_gid=' + verticalapplicanttyperule_gid +'&lsapplicant_type=' + applicant_type);
        }

        function GetVerticalquestionsummarylist() {
            var params = {
                vertical_gid: vertical_gid
            }
            var url = 'api/MstCreditMapping/GetVerticalquestionsummaryAppType';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                $scope.MdlGroupTitleQuestionlist = resp.data.MdlVerticalGroupTitleQuestion;
                if($scope.MdlGroupTitleQuestionlist !=null){
                    angular.forEach($scope.MdlGroupTitleQuestionlist, function (value, key) {
                        $scope.applicant_typelist = $scope.applicant_typelist.filter(function (el) { return el.applicant_type !== value.applicant_type });
                         
                    });
                }
                unlockUI();
            });
        }

        $scope.rule_delete = function (verticalapplicanttyperule_gid) {
            var params = {
                verticalapplicanttyperule_gid: verticalapplicanttyperule_gid
            }
            var url = 'api/MstCreditMapping/GetDeleteVerticalQuestionListAppType';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    unlockUI();
                    GetVerticalquestionsummarylist();
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
                else {
                    unlockUI();
                    Notify.alert(resp.data.message, {
                        status: 'warning',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
            });
        }

        $scope.back = function () {
            $state.go('app.vertical');
        };

        $scope.addverticalapplicanttype = function () {
            lockUI();
            
            var params = {
                vertical_gid: vertical_gid,
                vertical_name: $scope.vertical_name,
                vertical_code: $scope.vertical_code,
                applicant_type: $scope.cboapplicant_type.applicant_type,
                applicant_typegid: $scope.cboapplicant_type.id,
            }
            var url = 'api/MstCreditMapping/PostCreateVerticalRuleAppType';

            SocketService.post(url, params).then(function (resp) {

                
                if (resp.data.status == true) {
                    unlockUI();
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }

                else {
                    unlockUI();
                    Notify.alert(resp.data.message, {
                        status: 'warning',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }

                GetVerticalquestionsummarylist();

            });
            
        }

    }
})();
