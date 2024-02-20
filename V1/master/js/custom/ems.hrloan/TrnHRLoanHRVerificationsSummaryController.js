(function () {
    'use strict';

    angular
        .module('angle')
        .controller('TrnHRLoanHRVerificationsSummaryController', TrnHRLoanHRVerificationsSummaryController);

        TrnHRLoanHRVerificationsSummaryController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams','cmnfunctionService'];

    function TrnHRLoanHRVerificationsSummaryController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams,cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'TrnHRLoanHRVerificationsSummaryController';
        lockUI();
        activate();

        function activate() {
            lockUI();
            var url = 'api/TrnHRLoanHRVerifications/GetHRloanHRheadVerificationsDetails';
               SocketService.get(url).then(function (resp) {               
                   $scope.verificationsdetails_list = resp.data.verifications_summary;
                   unlockUI();
               });
               var url = 'api/TrnHRLoanHRVerifications/GetHRloanHRheadVerificationsDetailscount';
               SocketService.get(url).then(function (resp) {
                   unlockUI();
                   $scope.lspendinghrVerify_count = resp.data.pendinghrVerify_count;
                   $scope.lsapprovedhrVerify_count = resp.data.approvedhrVerify_count;                  
                   $scope.lsrejectedhrVerify_count = resp.data.rejectedhrVerify_count;                   
             });
        }
       
        $scope.verifications = function (request_gid, employee_gid) {
            $location.url('app/TrnHRLoanHRVerifications?hash=' + cmnfunctionService.encryptURL('request_gid=' + request_gid + '&employee_gid=' + employee_gid ));
        }

        $scope.myverifications = function () {
            $state.go('app.TrnHRLoanHRVerificationsSummary');
        }

        $scope.approved_verifications = function () {
            $state.go('app.TrnHRLoanHRVerificationsApprovedSummary');
        }

        $scope.rejected_verifications = function () {
            $state.go('app.TrnHRLoanHRVerificationsRejectedSummary');
        } 
               
        $scope.viewrequests = function (request_gid) {
            $location.url('app/TrnHRLoanHRVerificationsView?hash=' + cmnfunctionService.encryptURL('request_gid=' + request_gid));
        }       
       
    }
})();
