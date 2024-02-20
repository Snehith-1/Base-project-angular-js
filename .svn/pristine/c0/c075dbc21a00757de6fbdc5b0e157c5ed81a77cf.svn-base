(function () {
    'use strict';

    angular
        .module('angle')
        .controller('iasnDashboardController', iasnDashboardController);

        iasnDashboardController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', 'Colors', 'ChartData', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', '$route', 'ngTableParams', '$timeout', 'SweetAlert', '$cookies'];

    function iasnDashboardController($rootScope, $scope, $state, AuthenticationService, Colors, ChartData, $modal, ScopeValueService, $http, SocketService, Notify, $location, $route, ngTableParams, $timeout, SweetAlert, $cookies) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'iasnDashboardController';

        activate();

        function activate() {
               
            var url = 'api/IasnTrnWorkItem/WorkItemCounts';
            SocketService.get(url).then(function (resp) {
                $scope.count_workitempending = resp.data.count_workitempending;
                $scope.count_workitemassigned=resp.data.count_workitemassigned;
                $scope.count_pushback = resp.data.count_pushback;
                $scope.count_forward=resp.data.count_forward;
                $scope.count_close = resp.data.count_close;
                $scope.count_archival=resp.data.count_archival;
                $scope.count_workitemtotal = resp.data.count_workitemtotal; 
                
            });
              var user_gid = localStorage.getItem('user_gid');
              var url = 'api/user/privilegelevel3';
              SocketService.get(url + '?user_gid=' + user_gid).then(function (resp) {
                var zonalmapping = resp.data.privilegelevel3.map(function (e) { return e.project }).indexOf("ISNMSTZRM");
                var workitem = resp.data.privilegelevel3.map(function (e) { return e.project }).indexOf("ISNWOMWO");
                var myworkitem = resp.data.privilegelevel3.map(function (e) { return e.project }).indexOf("ISNWOMMWO");
                var archival = resp.data.privilegelevel3.map(function (e) { return e.project }).indexOf("ISNWOMARC");
                var report = resp.data.privilegelevel3.map(function (e) { return e.project }).indexOf("ISNRPTCSR");
                    if (zonalmapping != -1) {
                        $scope.zonal_show = 'Y';
                    }
                    else {
                        $scope.zonal_show = 'N';
                    }
                    if (workitem != -1) {
                        $scope.workitem_show = 'Y';
                    }
                    else {
                        $scope.workitem_show = 'N';
                    }
                    if (myworkitem != -1) {
                        $scope.myworkitem_show = 'Y';
                    }
                    else {
                        $scope.myworkitem_show = 'N';
                    }
                    if (archival != -1) {
                        $scope.archival_show = 'Y';
                    }
                    else {
                        $scope.archival_show = 'N';
                    }
                    if (report != -1) {
                        $scope.report_show = 'Y';
                    }
                    else {
                        $scope.report_show = 'N';
                    }
              });

        }
    }
})();
