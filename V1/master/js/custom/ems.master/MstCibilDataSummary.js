(function () {
    'use strict';

    angular
        .module('angle')
        .controller('cibildatasummaryController', cibildatasummaryController);

    cibildatasummaryController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function cibildatasummaryController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        $scope.title = 'cibildatasummaryController';

        activate();

        function activate() {
            $scope.totalDisplayed = 100;
            $scope.show = true;
            $scope.cibildata_gid = localStorage.getItem('cibildata_gid');
            lockUI();
            var params = {
                cibildata_gid:$scope.cibildata_gid
            }
            console.log(params)
            var url = "api/MstCibilData/GetCibilSummary";
            SocketService.getparams(url,params).then(function (resp) {
                unlockUI();
                $scope.cibilsummary_list = resp.data.cibilsummary_list;
                $scope.total = $scope.cibilsummary_list.length;
            });
        }
        $scope.edit=function(cibildatadtl_gid)
        {
            $scope.cibildatadtl_gid = localStorage.setItem('cibildatadtl_gid', cibildatadtl_gid);
            $state.go('app.MstCibilEdit')
        }
        $scope.back=function()
        {
            $state.go('app.MstCibilSummary')
        }
    }
})();
