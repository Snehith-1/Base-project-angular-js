(function () {
    'use strict';

    angular
        .module('angle')
        .controller('assetDashboardcontroller', assetDashboardcontroller);

    assetDashboardcontroller.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams'];

    function assetDashboardcontroller($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'assetDashboardcontroller';

        activate();

        function activate() {
            var user_gid = localStorage.getItem('user_gid');
            var url = 'api/user/privilegelevel3';
            SocketService.get(url + '?user_gid=' + user_gid).then(function (resp) {
                var viewasset = resp.data.privilegelevel3.map(function (e) { return e.project }).indexOf("AMSAMSVIW");
                var ackasset = resp.data.privilegelevel3.map(function (e) { return e.project }).indexOf("AMSAMSACK");
                var surrenderasset = resp.data.privilegelevel3.map(function (e) { return e.project }).indexOf("AMSAMSSRA");
                var tempasset = resp.data.privilegelevel3.map(function (e) { return e.project }).indexOf("AMSAMSTHO");
                if (viewasset != -1) {
                    $scope.viewasset = 'Y';
                }
                if (ackasset != -1) {
                    $scope.ackasset = 'Y';
                }
                if (surrenderasset != -1) {
                    $scope.surrenderasset = 'Y';
                }
                if (tempasset != -1) {
                    $scope.tempasset = 'Y';
                }

            });
            var url = 'api/landingPage/landingpagedata';
            lockUI();
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.count_acknowledgement = resp.data.count_acknowledgement;
                $scope.count_myasset = resp.data.count_myasset;
                $scope.count_surrender = resp.data.count_surrender;
                $scope.count_temporaryhandover = resp.data.count_temporaryhandover + resp.data.count_tmpsurrender + resp.data.count_tmpholding;
                $scope.employee_id = resp.data.employee_id;
                $scope.count_response = resp.data.count_response;
                $scope.count_myapprovals = resp.data.count_myapprovals;
            });
        }
    }
})();
