(function () {
    'use strict';

    angular
        .module('angle')
        .controller('idasTrnCreateBox', idasTrnCreateBox);

    idasTrnCreateBox.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', '$route', 'ngTableParams'];

    function idasTrnCreateBox($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, $route, ngTableParams) {
        var vm = this;
        vm.title = 'idasTrnCreateBox';

        activate();
       

        function activate() {

            vm.open = function ($event) {
                $event.preventDefault();
                $event.stopPropagation();

                vm.opened = true;
            };

            vm.dateOptions = {
                formatYear: 'yy',
                startingDay: 1
            };

            var url = 'api/IdasTrnCartonBox/BatchSummary';
            lockUI();
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.batch_list = resp.data.MdlbathSummary;
                console.log(resp.data.MdlbathSummary);
            });
   
        }

        
        $scope.box_back=function()
        {
            $state.go('app.boxMgmt');
        }
        $scope.box_submit = function()
        {
            var batch_list = [];

            angular.forEach($scope.batch_list, function (val) {

                if (val.checked == true) {
                    var batch_gid = val.batch_gid;

                    batch_list.push(batch_gid);

                }
            });
            var params = {
                cartonbox_date: $scope.box_date,
                remarks: $scope.boxremarks,
                batch_gid: batch_list,
               
            }

            console.log(params);
            var url = 'api/IdasTrnCartonBox/CartonBoxCreate';
            lockUI()
            SocketService.post(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    unlockUI()
                    $state.go('app.boxMgmt');
                    Notify.alert(resp.data.message, 'success')
                }
                else {
                    unlockUI();
                    $state.go('app.boxMgmt');
                    Notify.alert(resp.data.message)
                }



            });
        }

        $scope.checkall = function (selected) {
            angular.forEach($scope.batch_list, function (val) {
                val.checked = selected;

            });
        }
    }
})();
