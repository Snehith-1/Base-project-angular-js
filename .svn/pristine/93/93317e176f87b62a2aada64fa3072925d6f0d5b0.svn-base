(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstBusinessApprovalSummaryContoller', MstBusinessApprovalSummaryContoller);

        MstBusinessApprovalSummaryContoller.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function MstBusinessApprovalSummaryContoller($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstBusinessApprovalSummaryContoller';
        lockUI();
        activate();

        function activate() {
          
            var url = 'api/MstApplicationApproval/GetApplicationNewSummary';
               SocketService.get(url).then(function (resp) {               
                   $scope.applicationadd_list = resp.data.applicationadd_list;
               });
      
            var url = 'api/MstApplicationApproval/BusinessApplicationCount';
               SocketService.get(url).then(function (resp) {
                   unlockUI();
                   $scope.upcomingbusinessapplication_count = resp.data.upcomingbusinessapplication_count;
                   $scope.newbusinessapplication_count = resp.data.newbusinessapplication_count;
                   $scope.rejectedapplication_count = resp.data.rejectedapplication_count;
                   $scope.holdapplication_count = resp.data.holdapplication_count;
                   $scope.approvedapplication_count = resp.data.approvedapplication_count;
                   $scope.lstotalcount = resp.data.lstotalcount;
            });
        }
       
        $scope.business_approval = function (application_gid, employee_gid, applicationapproval_gid,initiate_flag) {
            $location.url('app/MstBusinessApproval?application_gid=' + application_gid + '&employee_gid=' + employee_gid +'&applicationapproval_gid=' + applicationapproval_gid+ '&lspage=BusinessApproval'+'&initiate_flag=' + initiate_flag);         
        }

        $scope.myapproval_applications = function () {
            $state.go('app.MstBusinessApprovalSummary');
        }
        $scope.myupcomingapproval_applications = function () {
            $state.go('app.MstUpcomingBusinessApprovalSummary');
        }


        $scope.rejected_applications = function () {
            $state.go('app.MstBusinessRejectedSummary');
        }

        $scope.hold_applications = function () {
            $state.go('app.MstBusinessHoldSummary');
        }

        $scope.approved_applications = function () {
            $state.go('app.MstBusinessApprovedSummary');
        }

        $scope.applcreation_view = function (application_gid) {
            $location.url('app/MstApplicationCreationView?application_gid=' + application_gid + '&lstab=BusinessApproval');
        }
       
    }
})();
