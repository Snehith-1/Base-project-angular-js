(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AgrMstBusinessHoldSummaryController', AgrMstBusinessHoldSummaryController);

        AgrMstBusinessHoldSummaryController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function AgrMstBusinessHoldSummaryController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AgrMstBusinessHoldSummaryController';
        lockUI();
        activate();

        function activate() {
         
            var url = 'api/AgrMstApplicationApproval/GetApplicationHoldSummary';
               SocketService.get(url).then(function (resp) {
                // unlockUI();
                   $scope.applicationadd_list = resp.data.applicationadd_list;
               });
               lockUI();
            var url = 'api/AgrMstApplicationApproval/BusinessApplicationCount';
               SocketService.get(url).then(function (resp) {
                   unlockUI();
                   $scope.newbusinessapplication_count = resp.data.newbusinessapplication_count;
                   $scope.upcomingbusinessapplication_count = resp.data.upcomingbusinessapplication_count;
                   $scope.rejectedapplication_count = resp.data.rejectedapplication_count;
                   $scope.holdapplication_count = resp.data.holdapplication_count;
                   $scope.approvedapplication_count = resp.data.approvedapplication_count;
                   $scope.lstotalcount = resp.data.lstotalcount;
            });
        }       
       
        $scope.myapproval_applications = function () {
            $state.go('app.AgrMstBusinessApprovalSummary');
        }

        $scope.rejected_applications = function () {
            $state.go('app.AgrMstBusinessRejectedSummary');
        }

        $scope.hold_applications = function () {
            $state.go('app.AgrMstBusinessHoldSummary');
        }

        $scope.approved_applications = function () {
            $state.go('app.AgrMstBusinessApprovedSummary');
        }

        $scope.applcreation_view = function (application_gid) {
            $location.url('app/AgrApplicationCreationView?application_gid=' + application_gid + '&lstab=BusinessHold');
        }

        $scope.business_approval = function (application_gid, employee_gid, applicationapproval_gid) {
            $location.url('app/AgrMstBusinessApproval?application_gid=' + application_gid + '&employee_gid=' + employee_gid +'&applicationapproval_gid=' + applicationapproval_gid+ '&lspage=BusinessHold' + '&lsflag=N');         
        }
        $scope.myupcomingapproval_applications = function () {
            $state.go('app.AgrMstUpcomingBusinessApprovalSummary');
        }
       
    }
})();

