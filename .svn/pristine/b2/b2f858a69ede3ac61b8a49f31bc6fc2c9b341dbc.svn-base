
(function () {
    'use strict';

    angular
        .module('angle')
        .controller('SysMstRoleEditController', SysMstRoleEditController);

        SysMstRoleEditController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route'];

    function SysMstRoleEditController($rootScope, $scope, $state, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'SysMstRoleEditController';
        activate();

        function activate() {

            var url = 'api/ManageRole/PopRoleReportingToEdit';
            var params = {
                role_gid: $scope.role_gid
            };
            SocketService.getparams(url,params).then(function (resp) {
                $scope.reportingtoListedit = resp.data.rolereporting_toEdit;
              console.log(resp.data);
            });
           
            $scope.role_gid = localStorage.getItem('role_gid');
            var url = 'api/ManageRole/RoleEdit';
            var param = {
                role_gid: $scope.role_gid
            };
         
            SocketService.getparams(url, param).then(function (resp) {

                $scope.txtrolecode = resp.data.role_code;
                $scope.txtroletitle = resp.data.role_name;
                $scope.txtreportingto = resp.data.reportingto_gid;
                $scope.txtprobationperiod = resp.data.probation_period;
                $scope.txtjobdescription = resp.data.job_description;
                $scope.txtroleresponsible = resp.data.role_responsible;
                              
            });

           
         
        }

        $scope.roleedit_cancel = function () {
            $state.go('app.SysMstRoleSummary');   
            };     
             
        $scope.role_update = function () {

            var params = {
                role_gid: $scope.role_gid,
                role_code: $scope.txtrolecode,
                role_name: $scope.txtroletitle,
                reportingto_gid: $scope.txtreportingto,
                probation_period: $scope.txtprobationperiod,
                job_description: $scope.txtjobdescription,
                role_responsible: $scope.txtroleresponsible,         
               
            }
         
            var url = 'api/ManageRole/RoleUpdate';
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {
                    activate();
                    $state.go('app.SysMstRoleSummary');
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 4000
                    });
                }

                else {
                    Notify.alert(resp.data.message, {
                        status: 'warning',
                        pos: 'top-center',
                        timeout: 4000
                    });
                }
                activate();
            });
        } 
    }
})();
