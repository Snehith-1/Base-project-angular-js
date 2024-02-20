(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstHoldApplicationSummaryController', MstHoldApplicationSummaryController);

        MstHoldApplicationSummaryController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function MstHoldApplicationSummaryController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstHoldApplicationSummaryController';
        lockUI();
        activate();

        function activate() { 
            var url = 'api/MstApplicationAdd/GetApplicationHoldSummary';
               SocketService.get(url).then(function (resp) {
                   unlockUI();
                   $scope.applicationadd_list = resp.data.applicationadd_list;
            });

            var url = 'api/MstApplicationAdd/ApplicationCount';
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
            $location.url('app/MstApplicationCreationRMApproval?application_gid=' + application_gid +  '&employee_gid=' + employee_gid + '&lspage=HoldApplications');         
        }

        $scope.addapp_creation = function () {
            $state.go('app.MstApplicationGeneralAdd');
        }

        $scope.my_applications = function () {
            $state.go('app.MstApplicationCreationSummary');
        }
        $scope.rejected_applications = function () {
            $state.go('app.MstRejectedApplicationSummary');
        }
        $scope.hold_applications = function () {
            $state.go('app.MstHoldApplicationSummary');
        }
        $scope.approved_applications = function () {
            $state.go('app.MstApprovedApplicationSummary');
        }
        
        $scope.applcreation_view = function (application_gid) {
            $location.url('app/MstApplicationCreationView?application_gid=' + application_gid + '&lstab=applicationcreationHold');
        }

    }
})();

