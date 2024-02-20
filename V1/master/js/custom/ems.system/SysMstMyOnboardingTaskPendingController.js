(function () {
    'use strict';

    angular
        .module('angle')
        .controller('SysMstMyOnboardingTaskPendingController', SysMstMyOnboardingTaskPendingController);

        SysMstMyOnboardingTaskPendingController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function SysMstMyOnboardingTaskPendingController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'SysMstMyOnboardingTaskPendingController';
        $scope.lsoneapipage = $location.search().lsoneapipage;
        var lsoneapipage = $scope.lsoneapipage;

        var lspage = 'TaskPending'
        // var employee_gid = $location.search().employee_gid;
        activate();

        function activate() {
            var url = 'api/ManageEmployee/GetMyTaskPendingSummary';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.mytask_list = resp.data.tasksummarylist;
                
                unlockUI();
            });

            var url = "api/ManageEmployee/GetMyTashCount";
            SocketService.get(url).then(function (resp) {               
                $scope.completed_count = resp.data.completed_count;              
                $scope.pending_count = resp.data.pending_count;
                unlockUI();
            });
        }
    //     $scope.process = function () {
    //         $state.go('app.SysMstMyOnboardingProcess');           
    // }


    $scope.PendingTask = function () {
        $state.go('app.SysMstMyOnboardingTaskPending');
    }      

    // Tagged Request
    $scope.Completed = function () {
        $state.go('app.SysMstMyOnboardingTaskCompleted');
    }


    $scope.process = function (employee_gid, taskinitiate_gid) {
        $location.url('app/SysMstMyOnboardingProcess?lsemployee_gid=' + employee_gid +  '&lstaskinitiate_gid=' + taskinitiate_gid + '&lspage='+ lspage );
    }


  $scope.pslcsacompleted = function () {

            if ( ($scope.txtpslqueries == ''|| $scope.txtpslqueries == undefined)) {
                Notify.alert(' Enter Complete Remarks','warning');
            }
            else {
                var params = {
                    application_gid: application_gid,
                    pslcompleteremarks: $scope.txtpslqueries
                }
                
                var url = "api/MstCAD/UpdatePSLCompleted";
                lockUI();
                SocketService.post(url, params).then(function (resp) {
                    unlockUI();
                    if (resp.data.status = true) {
                        Notify.alert(resp.data.message, 'success');
                        $location.url('app/MstPSLCSAManagement');
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

