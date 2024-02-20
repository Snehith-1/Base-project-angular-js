(function () {
    'use strict';

    angular
        .module('angle')
        .controller('iasnMstTeamAdd', iasnMstTeamAdd);

        iasnMstTeamAdd.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function iasnMstTeamAdd($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'iasnMstTeamAdd';

        activate();

        function activate() {
          
            var url = 'api/employee/employee';
            SocketService.get(url).then(function (resp) {
                $scope.employee_list = resp.data.employee_list;
            });
         }

         $scope.Submit=function(){
             var params={
                team_name:$scope.TeamName,
                description:$scope.description,
                zonal_name:$scope.zone,
                team_mailid:$scope.TeamMail,
                MdlRmList:$scope.rmlist,
                MdlCheckerList:$scope.checkerlist
             }

             var url="api/IasnMstTeam/CreateTeam";
             lockUI();
             SocketService.post(url,params).then(function (resp) {
                unlockUI();
               if(resp.data.status==true){
                   $state.go('app.iasnMstTeamManagement');
                Notify.alert(resp.data.message,'success');
               }
               else{
                Notify.alert(resp.data.message,'warning');
               }
            });
         }

         $scope.Back=function(){
             $state.go('app.iasnMstTeamManagement');
         }

        

    }
})();
