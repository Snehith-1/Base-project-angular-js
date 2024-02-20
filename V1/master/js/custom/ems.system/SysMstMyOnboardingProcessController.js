
(function () {
    'use strict';

    angular
        .module('angle')
        .controller('SysMstMyOnboardingProcessController',SysMstMyOnboardingProcessController);

        SysMstMyOnboardingProcessController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function SysMstMyOnboardingProcessController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
      
        var vm = this;
        vm.title = 'SysMstMyOnboardingProcessController';

        $scope.employee_gid = $location.search().lsemployee_gid;
        var employee_gid = $scope.employee_gid;
        $scope.taskinitiate_gid = $location.search().lstaskinitiate_gid;
        var taskinitiate_gid = $scope.taskinitiate_gid;      
        $scope.lspage = $location.search().lspage;
        var lspage = $scope.lspage;


        activate();
        var lstab = $location.search().lstab;
        $scope.lstab = lstab;
        lockUI();
        function activate() {
          
            var param = {
                employee_gid: employee_gid
            };
            var url = 'api/ManageEmployee/EmployeePendingEditView';                                 
            SocketService.getparams(url, param).then(function (resp) {             
                $scope.employee_details = resp.data;           
            });
            unlockUI();
            var param = {
                employee_gid: employee_gid,
                taskinitiate_gid: taskinitiate_gid
            };
            var url = 'api/ManageEmployee/GetCompleteflag';
          
            SocketService.getparams(url, param).then(function (resp) { 
                $scope.complete_flag = resp.data.complete_flag;
                
                unlockUI();
            });
            
            var url = 'api/ManageEmployee/GetTaskDetails';
            lockUI();
            SocketService.getparams(url, param).then(function (resp) { 
                $scope.task_details = resp.data;
              
                unlockUI();
            });
          
            }

        // $scope.view_cancel = function () {                     
        //     $state.go('app.SysMstMyOnboardingTaskPending');                               
        //     };
            $scope.view_cancel = function () {
                if (lspage == 'TaskPending') {
                    $location.url('app/SysMstMyOnboardingTaskPending?employee_gid=' + employee_gid );
                }
                else if (lspage == 'TaskCompleted') {
                    $location.url('app/SysMstMyOnboardingTaskCompleted');
                }
                else {
    
                }
            }

            $scope.taskcompleted = function () {

                if ( ($scope.txttaskcompleteremarks == ''|| $scope.txttaskcompleteremarks == undefined)) {
                    Notify.alert(' Enter Complete Remarks','warning');
                }
                else {
                    var params = {
                        employee_gid: employee_gid,
                        task_completeremarks: $scope.txttaskcompleteremarks,
                        taskinitiate_gid: taskinitiate_gid
                    }
                    
                    var url = "api/ManageEmployee/CompleteTask";
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status = true) {
                            Notify.alert(resp.data.message, 'success');
                            $location.url('app/SysMstMyOnboardingTaskPending');
                        }
                        else {
                            Notify.alert(resp.data.message, 'warning');
                            activate();
                        }
                    });
        
                }
            }


    }
})();
