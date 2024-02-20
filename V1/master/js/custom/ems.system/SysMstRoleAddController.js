
(function () {
    'use strict';

    angular
        .module('angle')
        .controller('SysMstRoleAddController', SysMstRoleAddController);

        SysMstRoleAddController.$inject = ['$rootScope', '$scope', '$state', '$modal', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams'];

    function SysMstRoleAddController($rootScope, $scope, $state, $modal, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'SysMstRoleAddController';

        activate();

        function activate() {
            var url = 'api/ManageRole/PopRoleReportingToAdd';
            SocketService.get(url).then(function (resp) {
                $scope.reportingtoList = resp.data.rolereporting_to;
              
            });
         }
        $scope.role_cancel = function () {
            $state.go('app.SysMstRoleSummary');   
            };
           

        /* Role Add */

        $scope.role_submit = function () {
            var params = {
                role_code: $scope.txtrolecode,
                role_name: $scope.txtroletitle,
                reportingto_gid: $scope.txtreportingto,
                probation_period: $scope.txtprobationperiod,
                job_description: $scope.txtjobdescription,
                role_responsible: $scope.txtroleresponsible,
                
            }
   
            var url = 'api/ManageRole/RoleAdd';
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {                  
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 4000
                    });
                    activate();
                    $state.go('app.SysMstRoleSummary');
                }
                else {
                
                    Notify.alert(resp.data.message, {
                        status: 'warning',
                        pos: 'top-center',
                        timeout: 4000
                    });
                    activate();
                    $state.go('app.SysMstRoleSummary');
                }
            });
           
        } 
    }
})();
