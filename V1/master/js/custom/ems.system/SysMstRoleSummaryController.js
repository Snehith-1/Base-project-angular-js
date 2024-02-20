(function () {
    'use strict';

    angular
        .module('angle')
        .controller('SysMstRoleSummaryController', SysMstRoleSummaryController);

        SysMstRoleSummaryController.$inject = ['$rootScope', '$scope', '$state', '$modal', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'SweetAlert'];

        function SysMstRoleSummaryController($rootScope, $scope, $state, $modal, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route,SweetAlert) {
        
        activate();
        var vm = this;
        vm.title = 'SysMstRoleSummaryController';

        $scope.lsoneapipage = $location.search().lsoneapipage;
            var lsoneapipage = $scope.lsoneapipage;

        function activate() {
            
                var url = 'api/ManageRole/RoleSummary';
                lockUI();
                SocketService.get(url).then(function (resp) {
                    $scope.rolelist = resp.data.role;
                    unlockUI();
                });
                            
           }
            
            $scope.role_edit = function (role_gid) {   
            $scope.role_gid = role_gid;
            $scope.role_gid = localStorage.setItem('role_gid', role_gid);
            $state.go('app.SysMstRoleEdit');
        };

        $scope.role_add = function () {
            $state.go('app.SysMstRoleAdd');   
            };
 

            $scope.delete = function (role_gid) {
                $scope.role_gid = role_gid;
                $scope.role_gid = localStorage.setItem('role_gid', role_gid);
                
                SweetAlert.swal({
                    title: 'Are you sure ?',
                    text: 'Do You Want To Delete ?',
                    showCancelButton: true,
                    confirmButtonColor: '#DD6B55',
                    confirmButtonText: 'Yes, delete it!',
                    closeOnConfirm: false
                }, function (isConfirm) {
                    if (isConfirm) {
                       
                        var url = 'api/ManageRole/RoleDelete';
                        $scope.role_gid = localStorage.getItem('role_gid');
                        var param = {
                            role_gid: $scope.role_gid
                        };
                        SocketService.getparams(url, param).then(function (resp) {
                            lockUI();
                            if (resp.data.status == true) {
                                SweetAlert.swal('Role Deleted Successfully !');
                                activate();
                            }
                           
                            else {
                                Notify.alert(resp.data.message, {
                                    status: 'warning',
                                    pos: 'top-center',
                                    timeout: 4000
                                });
                                activate();
                                unlockUI();
                            }
                        });
    
                    }
    
                });
            };


 }
    
})();
