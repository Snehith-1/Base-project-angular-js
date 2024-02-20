(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AgrMstApprovedApplicationSummaryController', AgrMstApprovedApplicationSummaryController);

    AgrMstApprovedApplicationSummaryController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function AgrMstApprovedApplicationSummaryController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AgrMstApprovedApplicationSummaryController';
        lockUI();
        activate();

        function activate() { 
            var url = 'api/AgrMstApplicationAdd/GetApplicationApprovedSummary';
               SocketService.get(url).then(function (resp) {
                   unlockUI();
                   $scope.applicationadd_list = resp.data.applicationadd_list;
            });

            var url = 'api/AgrMstApplicationAdd/ApplicationCount';
               SocketService.get(url).then(function (resp) {
                   unlockUI();
                   $scope.newapplication_count = resp.data.newapplication_count;
                   $scope.rejected_count = resp.data.rejected_count;
                   $scope.hold_count = resp.data.hold_count;
                   $scope.ccapproved_count = resp.data.ccapproved_count;
                   $scope.totalcount = resp.data.lstotalcount;
            });
        }
       
        $scope.rm_approval = function (application_gid,  employee_gid) {
            $location.url('app/AgrMstApplicationCreationRMApproval?application_gid=' + application_gid +  '&employee_gid=' + employee_gid + '&lspage=ApprovedApplications');        
        }

        $scope.applcreation_view = function (application_gid) {
            $location.url('app/AgrApplicationCreationView?application_gid=' + application_gid + '&lstab=AppCCApproved');
        }

        $scope.addapp_creation = function () {
            $state.go('app.AgrMstApplicationGeneralAdd');
        }

        $scope.my_applications = function () {
            $state.go('app.AgrMstApplicationCreationSummary');
        }
        $scope.rejected_applications = function () {
            $state.go('app.AgrMstRejectedApplicationSummary');
        }
        $scope.hold_applications = function () {
            $state.go('app.AgrMstHoldApplicationSummary');
        }
        $scope.approved_applications = function () {
            $state.go('app.AgrMstApprovedApplicationSummary');
        }
        
       
    }
})();

