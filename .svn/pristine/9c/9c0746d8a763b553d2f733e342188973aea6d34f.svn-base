(function () {
    'use strict';

    angular
        .module('angle')
        .controller('SysMstTaskViewController', SysMstTaskViewController);

        SysMstTaskViewController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function SysMstTaskViewController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'SysMstTaskViewController';

        $scope.task_gid = $location.search().lstask_gid;
        var task_gid = $scope.task_gid;

        activate();
        lockUI();
        function activate() {         

            vm.open1 = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();
                vm.opened1 = true;
            };
            vm.formats = ['dd-MM-yyyy'];
            vm.format = vm.formats[0];
            vm.dateOptions = {
                formatYear: 'yy',
                startingDay: 1
            };

            var params = {
                task_gid: task_gid
            };
            var url = 'api/SystemMaster/EditTask';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.lbltask_code = resp.data.task_code;
                $scope.lbltask_name = resp.data.task_name;
                $scope.lbllms_code = resp.data.lms_code;
                $scope.lblbureau_code = resp.data.bureau_code;
                $scope.lbltask_description = resp.data.task_description;
                $scope.lbltat = resp.data.tat;              
            });
            var url = 'api/SystemMaster/GetTaskMultiselectList';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.lblassignedto_name = resp.data.assignedto_name;
                $scope.lblescalationmailto_name = resp.data.escalationmailto_name;
                          
            });
            
        }    

     

        

        $scope.back = function () {
            $state.go('app.SysMstTask');
        };

        $scope.update_task = function () {

                lockUI();

                var params = {
                    task_gid: $scope.task_gid,
                    task_code: $scope.txttask_code,
                    task_name: $scope.txttask_name,
                    lms_code: $scope.txtlms_code,
                    bureau_code: $scope.txtbureau_code,                   
                    task_description: $scope.txttask_description,
                    tat: $scope.txttat,
                    assigned_to: $scope.cboassignedto_edit,
                    escalationmail_to: $scope.cboescalationmailto_edit             
                }
                var url = 'api/SystemMaster/UpdateTask';
                SocketService.post(url, params).then(function (resp) {

                    unlockUI();
                    if (resp.data.status == true) {
                        Notify.alert(resp.data.message, {
                            status: 'success',
                            pos: 'top-center',
                            timeout: 3000
                        });
                        $state.go('app.SysMstTask');
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
})();