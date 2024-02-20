(function () {
    'use strict';

    angular
        .module('angle')
        .controller('SysMstTaskAddController', SysMstTaskAddController);

        SysMstTaskAddController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function SysMstTaskAddController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'SysMstTaskAddController';

        $scope.program_gid = $location.search().lsprogram_gid;
        var program_gid = $scope.program_gid;

        activate();

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


            var url = 'api/SystemMaster/GetEmployeelist';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.assignedto_list = resp.data.employeelist;
                $scope.escalationmailto_list = resp.data.employeelist;
                unlockUI();
            });
        }

        $scope.task_submit = function () {

            if (($scope.txttask_name == '') || ($scope.txttask_description == undefined) || ($scope.cboassigned_to == '') || ($scope.cboescalationmail_to == undefined)) {
                Notify.alert('Enter all mandatory values..!', 'warning');
            }
            else {
                var params = {
                    task_name: $scope.txttask_name,
                    lms_code: $scope.txtlms_code,
                    bureau_code: $scope.txtbureau_code,
                    task_description: $scope.txttask_description,
                    assigned_to: $scope.cboassigned_to,
                    escalationmail_to: $scope.cboescalationmail_to,
                    tat: $scope.txttat
                }
                var url = 'api/SystemMaster/PostTaskAdd';
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    unlockUI();

                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    $location.url('app/SysMstTask');
                    activate();
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

        
        $scope.Back = function ()
        {
            $location.url('app/SysMstTask');
        }

    }
})();

