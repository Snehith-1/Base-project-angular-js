(function () {
    'use strict';

    angular
        .module('angle')
        .controller('cibillogController', cibillogController);

    cibillogController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function cibillogController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        $scope.title = 'cibillogController';

        activate();

        function activate() {
            $scope.totalDisplayed = 100;
            $scope.show = true;
            $scope.cibildata_gid = localStorage.getItem('cibildata_gid');
            lockUI();
            var params = {
                cibildata_gid: $scope.cibildata_gid
            }
            var url = "api/MstCibilData/GetCibilLog";
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.cibillog_list = resp.data.cibilsummary_list;
                $scope.total = $scope.cibillog_list.length;
            });
        }
       
        $scope.back = function () {
            $state.go('app.MstCibilSummary')
        }
    }
})();
