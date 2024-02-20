(function () {
    'use strict';

    angular
        .module('angle')
        .controller('osdDashboardcontroller', osdDashboardcontroller);

    osdDashboardcontroller.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', 'Colors', 'ChartData', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', '$route', 'ngTableParams', '$timeout', 'SweetAlert', '$cookies'];

    function osdDashboardcontroller($rootScope, $scope, $state, AuthenticationService, Colors, ChartData, $modal, ScopeValueService, $http, SocketService, Notify, $location, $route, ngTableParams, $timeout, SweetAlert, $cookies) {
      
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'osdDashboardcontroller';

        activate();

        function activate() {

            var url = 'api/osddashboard/osdprivilege';
            var user_gid = localStorage.getItem('user_gid');
            SocketService.get(url + '?user_gid=' + user_gid).then(function (resp) {

                var businessunitmaster = resp.data.osdprivilege_list.map(function (e) { return e.osdprivilege }).indexOf("OSDMSTMBU");
                var activebusinessunitmaster = resp.data.osdprivilege_list.map(function (e) { return e.osdprivilege }).indexOf("OSDMSTDPM");
                var activitymaster = resp.data.osdprivilege_list.map(function (e) { return e.osdprivilege }).indexOf("OSDMSTACM");
                var teammaster = resp.data.osdprivilege_list.map(function (e) { return e.osdprivilege }).indexOf("OSDMSTTEM");
                var service_request = resp.data.osdprivilege_list.map(function (e) { return e.osdprivilege }).indexOf("OSDTRNSER");
                var my_activity = resp.data.osdprivilege_list.map(function (e) { return e.osdprivilege }).indexOf("OSDTRNMYT");
                var activity_management = resp.data.osdprivilege_list.map(function (e) { return e.osdprivilege }).indexOf("OSDTRNTIR");
                var all_tickets = resp.data.osdprivilege_list.map(function (e) { return e.osdprivilege }).indexOf("OSDRPTALT");
                var query_assignment = resp.data.osdprivilege_list.map(function (e) { return e.osdprivilege }).indexOf("OSDCQMQAT");
                var assigned_query = resp.data.osdprivilege_list.map(function (e) { return e.osdprivilege }).indexOf("OSDCQMASQ");
                var allocatedto_rm = resp.data.osdprivilege_list.map(function (e) { return e.osdprivilege }).indexOf("OSDBAMARM");
                var bankalert_assignment = resp.data.osdprivilege_list.map(function (e) { return e.osdprivilege }).indexOf("OSDBAMMAL");
             
             
                if (businessunitmaster != -1) {
                    $scope.busineesunitmaster_show = 'Y';
                }

                if (activebusinessunitmaster != -1) {
                    $scope.activatebusineesunitmaster_show = 'Y';
                }

                if (activitymaster != -1) {
                    $scope.activitymaster_show = 'Y';
                }
                if (teammaster != -1) {
                    $scope.teammaster_show = 'Y';
                }
                if (service_request != -1) {
                    $scope.service_request_show = 'Y';
                }
                if (my_activity != -1) {
                    $scope.my_activity_show = 'Y';
                }
                if (activity_management != -1) {
                    $scope.activity_management_show = 'Y';
                }

                if (all_tickets != -1) {
                    $scope.all_tickets_show = 'Y';
                }

                if (query_assignment != -1) {
                    $scope.query_assignment = 'Y';
                }

                if (assigned_query != -1) {
                    $scope.assigned_query = 'Y';
                }
                if (allocatedto_rm != -1) {
                    $scope.allocatedto_rm = 'Y';
                }

                if (bankalert_assignment != -1) {
                    $scope.bankalert_assignment = 'Y';
                }
               
            });

            vm.pieOptions = {
                segmentShowStroke: true,
                segmentStrokeColor: '#fff',
                segmentStrokeWidth: 2,
                percentageInnerCutout: 0,
                animationSteps: 100,
                animationEasing: 'easeInOutBack',
                animateRotate: true,
                animateScale: false
            };

            vm.pieData = [
              {
                  value: 14,
                  color: Colors.byName('yellow'),
                  highlight: Colors.byName('info'),
                  label: 'Allotted'
              },
              {
                  value: 17,
                  color: Colors.byName('danger'),
                  highlight: Colors.byName('info'),
                  label: 'Work-In-Progress'
              },
              {
                  value: '2',
                  color: Colors.byName('purple'),
                  highlight: Colors.byName('info'),
                  label: 'Completed'
              },
              {
                  value: '1',
                  color: Colors.byName('success'),
                  highlight: Colors.byName('info'),
                  label: 'Closed'
              }
              
            ];

        }
       

    }
})();
