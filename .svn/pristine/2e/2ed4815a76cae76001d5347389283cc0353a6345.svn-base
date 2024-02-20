(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstColendingRuleaddcontroller', MstColendingRuleaddcontroller);

    MstColendingRuleaddcontroller.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function MstColendingRuleaddcontroller($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstColendingRuleaddcontroller';
        var colendingprogram_gid = $location.search().lscolendingprogram_gid;
           
        
        activate();

        function activate() {           
            var params = {
                colendingprogram_gid: colendingprogram_gid                
            }
            var url = 'api/MstApplication360/GetColendingGroupTitleList';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {               
                $scope.colendar_name = resp.data.colendar_name;                              
                unlockUI();
            });
            var url = 'api/MstApplication360/Getapplicanttypesummary';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {                
                $scope.applicant_list = resp.data.MdlColendingapplicant;
                if($scope.applicant_list !=null){
                    angular.forEach($scope.applicant_list, function (value, key) {
                        $scope.applicant_typelist = $scope.applicant_typelist.filter(function (el) { return el.applicant_type !== value.applicant_type });                       

                    });
                }
                unlockUI();
            });                

            $scope.applicant_typelist = [
                { id: 0, applicant_type: 'Company' },
                { id: 1, applicant_type: 'Individual' },
              /*  { id: 2, applicant_type: 'Group' },*/
                { id: 3, applicant_type: '' }
            ]

        }
        function GetCreditquestionsummarylist() {
            $scope.applicant_typelist = [
                { id: 0, applicant_type: 'Company' },
                { id: 1, applicant_type: 'Individual' },
               /* { id: 2, applicant_type: 'Group' },*/
                { id: 3, applicant_type: '' }
            ]
            var params = {
                colendingprogram_gid: colendingprogram_gid                
            }
            var url = 'api/MstApplication360/Getapplicanttypesummary';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                $scope.applicant_list = resp.data.MdlColendingapplicant;
                if($scope.applicant_list !=null){
                    angular.forEach($scope.applicant_list, function (value, key) {
                        $scope.applicant_typelist = $scope.applicant_typelist.filter(function (el) { return el.applicant_type !== value.applicant_type });                       

                    });
                }
                unlockUI();
            });
        }

        $scope.addcolending = function () {           
          
            var params = {
                colendingprogram_gid: colendingprogram_gid,
                applicant_type: $scope.cboapplicant_type.applicant_type,
                applicant_typegid: $scope.cboapplicant_type.id,                
            }
            lockUI();
            var url = 'api/MstApplication360/PostCreateColendingRuleadd';
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'warning',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
                GetCreditquestionsummarylist();
                
            });
        }

        $scope.colendingRule = function (colendingprogram_gid,colendingaddquestionrule_gid,applicant_type) {
            $location.url('app/MstColendingRule?lscolendingprogram_gid=' + colendingprogram_gid + '&lscolendingaddquestionrule_gid=' + colendingaddquestionrule_gid + '&lsapplicant_type=' + applicant_type);
        }

        $scope.back = function () {
            $state.go('app.MstColendingPrograms');
        };

        $scope.colendingrule_delete = function (colendingaddquestionrule_gid) {
            var params = {
                colendingaddquestionrule_gid: colendingaddquestionrule_gid
            }
            lockUI();          
            var url = 'api/MstApplication360/Deleteapplicanttype';
            SocketService.getparams(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    unlockUI();
                    GetCreditquestionsummarylist();                   
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
        
    }
})();
