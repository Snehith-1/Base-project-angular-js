(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AgrMstSuprHoldApplicationSummaryController', AgrMstSuprHoldApplicationSummaryController);

    AgrMstSuprHoldApplicationSummaryController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function AgrMstSuprHoldApplicationSummaryController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AgrMstSuprHoldApplicationSummaryController';
        lockUI();
        activate();

        function activate() { 
            var url = 'api/AgrMstSuprApplicationAdd/GetApplicationHoldSummary';
               SocketService.get(url).then(function (resp) {
                   unlockUI();
                   $scope.applicationadd_list = resp.data.applicationadd_list;
            });

            var url = 'api/AgrMstSuprApplicationAdd/ApplicationCount';
               SocketService.get(url).then(function (resp) {
                   unlockUI();
                   $scope.newapplication_count = resp.data.newapplication_count;
                   $scope.rejected_count = resp.data.rejected_count;
                   $scope.hold_count = resp.data.hold_count;
                   $scope.ccapproved_count = resp.data.ccapproved_count;
                   $scope.totalcount = resp.data.lstotalcount;
            });
        }
       
        $scope.rm_approval = function (application_gid, employee_gid) {
            $location.url('app/AgrMstSuprApplicationCreationRMApproval?application_gid=' + application_gid +  '&employee_gid=' + employee_gid + '&lspage=HoldApplications');         
        }

        $scope.applcreation_view = function (application_gid) {
            $location.url('app/AgrMstSuprApplicationCreationView?application_gid=' + application_gid + '&lstab=AppHoldApplications');
        }

        $scope.addapp_creation = function () {
            $state.go('app.AgrMstSuprApplicationGeneralAdd');
        }

        $scope.my_applications = function () {
            $state.go('app.AgrMstSuprApplicationCreationSummary');
        }
        $scope.rejected_applications = function () {
            $state.go('app.AgrMstSuprRejectedApplicationSummary');
        }
        $scope.hold_applications = function () {
            $state.go('app.AgrMstSuprHoldApplicationSummary');
        }
        $scope.approved_applications = function () {
            $state.go('app.AgrMstSuprApprovedApplicationSummary');
        }
        
       
    }
})();

