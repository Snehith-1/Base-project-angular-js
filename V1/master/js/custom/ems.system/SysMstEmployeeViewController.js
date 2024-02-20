
(function () {
    'use strict';

    angular
        .module('angle')
        .controller('SysMstEmployeeViewController', SysMstEmployeeViewController);

        SysMstEmployeeViewController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function SysMstEmployeeViewController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
      
        var vm = this;
        vm.title = 'SysMstEmployeeViewController';
        var employee_gid = $location.search().employee_gid;
      
        var lstab = $location.search().lstab;
        $scope.lstab = lstab;
        activate();
        function activate() {
            lockUI();
            if ( $scope.lstab== 'pending') {
                var url = 'api/ManageEmployee/EmployeePendingEditView';
            } 
            else {
                var url = 'api/ManageEmployee/EmployeeEditView';
            }
                        
            var param = {
                employee_gid: employee_gid
            };

            SocketService.getparams(url, param).then(function (resp) {
                $scope.employee_details = resp.data;
            });

            var url = 'api/ManageEmployee/EmployeeRelievingView';
            SocketService.getparams(url, param).then(function (resp) {
                $scope.relive_date = resp.data.relive_date;
                $scope.remarks = resp.data.remarks;
            });
            unlockUI();

            if (lstab == 'pending') {
                $scope.pending=1;
            }
            else if (lstab == 'active') {
                $scope.active=1;
            }
            else if (lstab == 'inactive') {
                $scope.inactive=1;
            }
            else if (lstab == 'relieving') {
                $scope.relieving=1;
            }
        }

        $scope.view_cancel = function () {
           
            if (lstab == 'pending') {
                $location.url('app/SysMstEmployeePendingSummary');
            }
            else if (lstab == 'active') {
                $state.go('app.SysMstEmployeeActiveUserSummary');
            }
            else if (lstab == 'inactive') {
                $state.go('app.SysMstEmployeeInactiveSummary');
            }
            else if (lstab == 'relieving') {
                $state.go('app.SysMstEmployeeRelievingSummary');
            }
            else {
                $state.go('app.SysMstEmployeeSummary');   
            }
        };
    }
})();
