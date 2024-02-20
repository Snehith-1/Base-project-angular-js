(function () {
    'use strict';

    angular
        .module('angle')
        .controller('rskDashboardcontroller', rskDashboardcontroller);

    rskDashboardcontroller.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', 'Colors', 'ChartData', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', '$route', 'ngTableParams', '$timeout', 'SweetAlert', '$cookies'];

    function rskDashboardcontroller($rootScope, $scope, $state, AuthenticationService, Colors, ChartData, $modal, ScopeValueService, $http, SocketService, Notify, $location, $route, ngTableParams, $timeout, SweetAlert, $cookies) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'rskDashboardcontroller';

        activate();

        function activate() {
            vm.barOptions = {
                scaleBeginAtZero: true,
                scaleShowGridLines: true,
                scaleGridLineColor: 'rgba(0,0,0,.05)',
                scaleGridLineWidth: 1,
                barShowStroke: true,
                barStrokeWidth: 2,
                barValueSpacing: 5,
                barDatasetSpacing: 1,
                animationSteps: 100,
                animationEasing: 'easeInOutSine',
            };
            
            var url = "api/RskDashboard/GetAllocationDtl"
            SocketService.get(url).then(function (resp) {
                var last6monthallocation = resp.data.allocationvisitgraphdtl;
                console.log(resp);
                if (last6monthallocation == null) {
                    vm.barData = {
                        labels: [],
                        datasets: [
                          {
                              fillColor: Colors.byName('success'),
                              strokeColor: Colors.byName('success'),
                              highlightFill: Colors.byName('info'),
                              highlightStroke: Colors.byName('info'),
                              data: []
                          },
                          {
                              fillColor: Colors.byName('warning'),
                              strokeColor: Colors.byName('warning'),
                              highlightFill: Colors.byName('primary'),
                              highlightStroke: Colors.byName('primary'),
                              data: []
                          }
                        ]
                    };
                }
                else {

                    vm.barData = {
                        labels: [last6monthallocation[4].monthname, last6monthallocation[3].monthname, last6monthallocation[2].monthname, last6monthallocation[1].monthname, last6monthallocation[0].monthname],
                        datasets: [
                          {
                              fillColor: Colors.byName('success'),
                              strokeColor: Colors.byName('success'),
                              highlightFill: Colors.byName('info'),
                              highlightStroke: Colors.byName('info'),
                              data: [last6monthallocation[4].countAllocated, last6monthallocation[3].countAllocated, last6monthallocation[2].countAllocated, last6monthallocation[1].countAllocated, last6monthallocation[0].countAllocated]
                          },
                          {
                              fillColor: Colors.byName('warning'),
                              strokeColor: Colors.byName('warning'),
                              highlightFill: Colors.byName('primary'),
                              highlightStroke: Colors.byName('primary'),
                              data: [last6monthallocation[4].countCompleted, last6monthallocation[3].countCompleted, last6monthallocation[2].countCompleted, last6monthallocation[1].countCompleted, last6monthallocation[0].countCompleted]
                          }
                        ]
                    };
                }

            });


            var url = 'api/RskDashboard/GetRskPrivilege';
            var user_gid = localStorage.getItem('user_gid');
            SocketService.get(url + '?user_gid=' + user_gid).then(function (resp) {
                var sanction = resp.data.rskprivilege_list.map(function (e) { return e.rskprivilege }).indexOf("RSKSANMAN");
                var customerManagement = resp.data.rskprivilege_list.map(function (e) { return e.rskprivilege }).indexOf("RSKSANCUS");
                var zonalCustomer = resp.data.rskprivilege_list.map(function (e) { return e.rskprivilege }).indexOf("RSKSANMYC");
               
                var caseAllocation = resp.data.rskprivilege_list.map(function (e) { return e.rskprivilege }).indexOf("RSKALCMAN");
                var zonalAllocation = resp.data.rskprivilege_list.map(function (e) { return e.rskprivilege }).indexOf("RSKALCZAC");
                var visitManagement = resp.data.rskprivilege_list.map(function (e) { return e.rskprivilege }).indexOf("RSKALCRMA");
                var transferApproval = resp.data.rskprivilege_list.map(function (e) { return e.rskprivilege }).indexOf("RSKALCTAP");
                var allocationTransfer = resp.data.rskprivilege_list.map(function (e) { return e.rskprivilege }).indexOf("RSKALCACT");
                var observationReportApproval = resp.data.rskprivilege_list.map(function (e) { return e.rskprivilege }).indexOf("RSKALCORA");
                var tierreport = resp.data.rskprivilege_list.map(function (e) { return e.rskprivilege }).indexOf("RSKALCTRT");
                var tier2approval = resp.data.rskprivilege_list.map(function (e) { return e.rskprivilege }).indexOf("RSKALCTIA");
                var tier3preparation = resp.data.rskprivilege_list.map(function (e) { return e.rskprivilege }).indexOf("RSKALCTI3");
                var allocationreport = resp.data.rskprivilege_list.map(function (e) { return e.rskprivilege }).indexOf("RSKALCART");

                if (sanction != -1) {
                    $scope.sanction_show = 'Y';
                }
                else {
                    $scope.sanction_show = 'N';
                }
                if (customerManagement != -1) {
                    $scope.customerManagement_show = 'Y';
                }
                else {
                    $scope.customerManagement_show = 'N';
                }
                if (zonalCustomer != -1) {
                    $scope.zonalCustomer_show = 'Y';
                }
                else
                {
                    $scope.zonalCustomer_show = 'N';
                }
                if (caseAllocation != -1) {
                    $scope.caseAllocation_show = 'Y';
                }
                else {
                    $scope.caseAllocation_show = 'N';
                }
                if (zonalAllocation != -1) {
                    $scope.zonalAllocation_show = 'Y';
                }
                else {
                    $scope.zonalAllocation_show = 'N';
                }
                if (visitManagement != -1) {
                    $scope.visitManagement_show = 'Y';
                }
                else {
                    $scope.visitManagement_show = 'N';
                }
                if (transferApproval != -1) {
                    $scope.transferApproval_show = 'Y';
                }
                else {
                    $scope.transferApproval_show = 'N';
                }
                if (allocationTransfer != -1) {
                    $scope.allocationTransfer_show = 'Y';
                }
                else {
                    $scope.allocationTransfer_show = 'N';
                }
                if (observationReportApproval != -1) {
                    $scope.observationApproval_show = 'Y';
                }
                else {
                    $scope.observationApproval_show = 'N';
                }
                if (tierreport != -1) {
                    $scope.tierreport_show = 'Y';
                }
                else {
                    $scope.tierreport_show = 'N';
                }
                if (tier2approval != -1) {
                    $scope.tier2approval_show = 'Y';
                }
                else {
                    $scope.tier2approval_show = 'N';
                }
                if (tier3preparation != -1) {
                    $scope.tier3preparation_show = 'Y';
                }
                else {
                    $scope.tier3preparation_show = 'N';
                }
                if (allocationreport != -1) {
                    $scope.allocationreport_show = 'Y';
                }
                else {
                    $scope.allocationreport_show = 'N';
                }
            });
        }
    }
})();
