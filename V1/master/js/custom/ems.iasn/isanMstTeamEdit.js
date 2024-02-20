(function () {
    'use strict';

    angular
        .module('angle')
        .controller('iasnMstTeamEdit', iasnMstTeamEdit);

        iasnMstTeamEdit.$inject =['$rootScope', '$scope', '$state', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', '$route', '$cookies', '$filter', 'ngTableParams', '$timeout'];

    function iasnMstTeamEdit($rootScope, $scope, $state, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, $route, $cookies, $filter, ngTableParams, $timeout) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'iasnMstTeamEdit';

        activate();

        function activate() {
          
            // var url = 'api/employee/employee';
            // SocketService.get(url).then(function (resp) {
            //     $scope.employee_list = resp.data.employee_list;
               
            // });
          
            var params={
                team_gid:localStorage.getItem('team_gid')
            }
            var url = 'api/IasnMstTeam/EditTeam';
            SocketService.getparams(url,params).then(function (resp) {
             
                $scope.TeamCode=resp.data.team_code;
                $scope.TeamName = resp.data.team_name;
                $scope.TeamMail=resp.data.team_mailid;
                $scope.zone=resp.data.zonal_name;
                $scope.description=resp.data.description;
                $scope.employee_list = resp.data.employee_list;
             
                if (resp.data.MdlRmList != null) {
                    $scope.rmlist = [];
                    var count = resp.data.MdlRmList.length;
                    for (var i = 0; i < count; i++) {
                        var indexs = $scope.employee_list.map(function (x) { return x.employee_gid; }).indexOf(resp.data.MdlRmList[i].employee_gid);
                        $scope.rmlist.push($scope.employee_list[indexs]);
                    }
                }

                if (resp.data.MdlCheckerList != null) {
                    var count = resp.data.MdlCheckerList.length;
                    $scope.checkerlist = [];
                    for (var i = 0; i < count; i++) {
                        var indexs = $scope.employee_list.map(function (x) { return x.employee_gid; }).indexOf(resp.data.MdlCheckerList[i].employee_gid);
                        $scope.checkerlist.push($scope.employee_list[indexs]);
                    }
                }
            });

         }

         $scope.Update=function(){

            if($scope.TeamName==undefined){
                Notify.alert('Team Name is Mandatory','warning');
                return;
            }
            if($scope.TeamMail==undefined){
                Notify.alert('Team Mail is Mandatory','warning');
                return;
            }
            if($scope.description==undefined){
                Notify.alert('Description is Mandatory','warning');
                return;
            }
            if($scope.zone==undefined){
                Notify.alert('Zone is Mandatory','warning');
                return;
            }
            if($scope.rmlist==undefined){
                Notify.alert('Kindly select the RM List','warning');
                return;
            }
            if($scope.checkerlist==undefined){
                Notify.alert('Kindly select the Checker List','warning');
                return;
            }
           
             var params={
                team_gid:localStorage.getItem('team_gid'),
                team_name:$scope.TeamName,
                description:$scope.description,
                zonal_name:$scope.zone,
                team_mailid:$scope.TeamMail,
                MdlRmList:$scope.rmlist,
                MdlCheckerList:$scope.checkerlist
             }
            

             var url="api/IasnMstTeam/UpdateTeam";
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
