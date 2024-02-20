(function () {
    'use strict';

    angular
        .module('angle')
        .controller('SysMstMyOnboardingTaskCompletedController', SysMstMyOnboardingTaskCompletedController);

        SysMstMyOnboardingTaskCompletedController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function SysMstMyOnboardingTaskCompletedController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'SysMstMyOnboardingTaskCompletedController';
        var lspage = 'TaskCompleted'
        // var employee_gid = $location.search().employee_gid;
        activate();

        function activate() {
            var url = 'api/ManageEmployee/GetMyTaskCompleteSummary';
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


    $scope.process = function (employee_gid , taskinitiate_gid) {
        $location.url('app/SysMstMyOnboardingProcess?lsemployee_gid=' + employee_gid +  '&lstaskinitiate_gid=' + taskinitiate_gid + '&lspage='+ lspage);
    }

    $scope.completedremarks= function (task_completeremarks){
        var modalInstance = $modal.open({
            templateUrl: '/completedremarks.html',
            controller: ModalInstanceCtrl,
            backdrop: 'static',
            keyboard: false,
            size: 'md'
        });
        ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
        function ModalInstanceCtrl($scope, $modalInstance) {
            $scope.task_completeremarks=task_completeremarks;
            $scope.back = function () {
                $modalInstance.close('closed');
            }; 
        }
    }

    }
})();

