(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AgrMstSuprBusinessApprovalSummaryContoller', AgrMstSuprBusinessApprovalSummaryContoller);

        AgrMstSuprBusinessApprovalSummaryContoller.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function AgrMstSuprBusinessApprovalSummaryContoller($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AgrMstSuprBusinessApprovalSummaryContoller';
        lockUI();
        activate();

        function activate() {
          
            var url = 'api/AgrTrnSuprApplicationApproval/GetApplicationNewSummary';
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
       
        $scope.business_approval = function (application_gid, employee_gid, applicationapproval_gid,initiate_flag) {
            $location.url('app/AgrMstSuprBusinessApproval?application_gid=' + application_gid + '&employee_gid=' + employee_gid +'&applicationapproval_gid=' + applicationapproval_gid+ '&lspage=BusinessApproval'+'&initiate_flag=' + initiate_flag);         
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
            $location.url('app/AgrMstSuprApplicationCreationView?application_gid=' + application_gid + '&lstab=BusinessApproval');
        }
       
    }
})();
