(function () {
    'use strict';

    angular
        .module('angle')
        .controller('iasnMstTeamManagement', iasnMstTeamManagement);

        iasnMstTeamManagement.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function iasnMstTeamManagement($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'iasnMstTeamManagement';

        activate();

        function activate() {
            $scope.totalDisplayed=100;
            $scope.total = 0;

            var url = "api/IasnMstTeam/TeamSummary";
            SocketService.get(url).then(function (resp) {
                unlockUI();
               
                $scope.teammgmt_list = resp.data.MdlTeamSummary;
                $scope.total = $scope.teammgmt_list.length;
            });

         }

         $scope.loadMore= function (pagecount) {
          
            if(pagecount==undefined){
                Notify.alert("Enter the Total Summary Count","warning");
                return;
            }
            lockUI();
        
            var Number = parseInt(pagecount);
            // new code start
            if ($scope.total != 0) {
               
                if (pagecount < $scope.total) {
                    $scope.totalDisplayed += Number;
                    if($scope.total<$scope.totalDisplayed){
                        $scope.totalDisplayed =$scope.total;
                        Notify.alert(" Total Summary " + $scope.total + " Records Only", "warning");
                    }
                    unlockUI();
                }
                else {
                    unlockUI();
                    Notify.alert(" Total Summary " + $scope.total + " Records Only", "warning");
                    return;
                }
            }
            // new code end
            // $scope.totalDisplayed += Number;
            // console.log(pagecount);
            unlockUI();
        };

      
        // Add Team Code Ends

        $scope.addTeam = function () {
            
            $state.go('app.iasnMstTeamAdd');
        }

        $scope.EditTeam = function (val) {
            localStorage.setItem('team_gid', val);

            $state.go('app.iasnMstTeamEdit');
        }

    }
})();
