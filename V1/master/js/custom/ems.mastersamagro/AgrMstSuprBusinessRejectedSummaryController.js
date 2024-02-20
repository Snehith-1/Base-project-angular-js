(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AgrMstSuprBusinessRejectedSummaryController', AgrMstSuprBusinessRejectedSummaryController);

        AgrMstSuprBusinessRejectedSummaryController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function AgrMstSuprBusinessRejectedSummaryController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AgrMstSuprBusinessRejectedSummaryController';
        lockUI();
        activate();

        function activate() { 
            var url = 'api/AgrTrnSuprApplicationApproval/GetApplicationRejectedSummary';
               SocketService.get(url).then(function (resp) {                  
                   $scope.applicationadd_list = resp.data.applicationadd_list;
            });
            var url = 'api/AgrTrnSuprApplicationApproval/BusinessApplicationCount';
               SocketService.get(url).then(function (resp) {
                   unlockUI();
                   $scope.newbusinessapplication_count = resp.data.newbusinessapplication_count;
                   $scope.rejectedapplication_count = resp.data.rejectedapplication_count;
                   $scope.holdapplication_count = resp.data.holdapplication_count;
                   $scope.approvedapplication_count = resp.data.approvedapplication_count;
                   $scope.lstotalcount = resp.data.lstotalcount;
            });
        }       
      
        $scope.business_approval = function (application_gid, employee_gid) {
            $location.url('app/AgrMstSuprBusinessApproval?application_gid=' + application_gid + '&employee_gid=' + employee_gid + '&lspage=MyTeamApplications');         
        }

        $scope.myapproval_applications = function () {
            $state.go('app.AgrMstSuprBusinessApprovalSummary');
        }

        $scope.rejected_applications = function () {
            $state.go('app.AgrMstSuprBusinessRejectedSummary');
        }

        $scope.hold_applications = function () {
            $state.go('app.AgrMstSuprBusinessHoldSummary');
        }

        $scope.approved_applications = function () {
            $state.go('app.AgrMstSuprBusinessApprovedSummary');
        }

        $scope.applcreation_view = function (application_gid) {
            $location.url('app/AgrMstSuprApplicationCreationView?application_gid=' + application_gid + '&lstab=BusinessReject');
        }

        $scope.business_approval = function (application_gid, employee_gid, applicationapproval_gid) {
            $location.url('app/AgrMstSuprBusinessApproval?application_gid=' + application_gid + '&employee_gid=' + employee_gid +'&applicationapproval_gid=' + applicationapproval_gid + '&lspage=BusinessReject' + '&lsflag=N');         
        }
       
    }
})();

