(function () {
    'use strict';

    angular
        .module('angle')
        .controller('SysMstTaskEditController', SysMstTaskEditController);

        SysMstTaskEditController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function SysMstTaskEditController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'SysMstTaskEditController';

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
                $scope.txttask_code = resp.data.task_code;
                $scope.txttask_name = resp.data.task_name;

                $scope.txtlms_code = resp.data.lms_code;
                $scope.txtbureau_code = resp.data.bureau_code;

                $scope.txttask_description = resp.data.task_description;
                $scope.txttat = resp.data.tat;
               
                $//scope.assignedto_editlist = resp.data.assigned_to;
                $scope.assignedto_general = resp.data.assignedto_general;
                $scope.cboassignedto_edit = [];
                if (resp.data.assigned_to != null) {
                    var count = resp.data.assigned_to.length;
                    for (var i = 0; i < count; i++) {
                        //var indexs = $scope.assignedto_general.findIndex(x => x.employee_gid === resp.data.assigned_to[i].employee_gid);
                        var indexs = $scope.assignedto_general.map(function (x) { return x.employee_gid; }).indexOf(resp.data.assigned_to[i].employee_gid);
                        $scope.cboassignedto_edit.push($scope.assignedto_general[indexs]);
                        $scope.$parent.cboassignedto_edit = $scope.cboassignedto_edit;
                    }
                }

                $scope.assignedto_general = resp.data.assignedto_general;
                $scope.cboassignedto_edit = [];
                if (resp.data.assigned_to != null) {
                    var count = resp.data.assigned_to.length;
                    for (var i = 0; i < count; i++) {
                        //var indexs = $scope.assignedto_general.findIndex(x => x.employee_gid === resp.data.assigned_to[i].employee_gid);
                        var indexs = $scope.assignedto_general.map(function (x) { return x.employee_gid; }).indexOf(resp.data.assigned_to[i].employee_gid);
                        $scope.cboassignedto_edit.push($scope.assignedto_general[indexs]);
                        $scope.$parent.cboassignedto_edit = $scope.cboassignedto_edit;
                    }
                }

                $scope.escalationmailto_general = resp.data.escalationmailto_general;
                $scope.cboescalationmailto_edit = [];
                if (resp.data.escalationmail_to != null) {
                    var count = resp.data.escalationmail_to.length;
                    for (var i = 0; i < count; i++) {
                        //var indexs = $scope.escalationmailto_general.findIndex(x => x.employee_gid === resp.data.escalationmail_to[i].employee_gid);
                        var indexs = $scope.escalationmailto_general.map(function (x) { return x.employee_gid; }).indexOf(resp.data.escalationmail_to[i].employee_gid);
                        $scope.cboescalationmailto_edit.push($scope.escalationmailto_general[indexs]);
                        $scope.$parent.cboescalationmailto_edit = $scope.cboescalationmailto_edit;
                    }
                }

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