(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AgrMstCustomerApprovalSummaryController', AgrMstCustomerApprovalSummaryController);

    AgrMstCustomerApprovalSummaryController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', '$route', 'ngTableParams', 'cmnfunctionService'];

    function AgrMstCustomerApprovalSummaryController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, $route, ngTableParams, cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AgrMstCustomerApprovalSummaryController';
        var selectedIndex = $location.search().selectedIndex;

        const lsdynamiclimitmanagementback = 'AgrMstCustomerApprovalSummary';


        activate(); 
        function activate() { 
            if (selectedIndex == "" || selectedIndex == undefined)
                $scope.selectedIndex = 0;
            else
                $scope.selectedIndex = selectedIndex;
        }

        $scope.GetBuyeronboardingPendinglist = function () {
            getApprovalCount();
            lockUI();
            var url = 'api/AgrMstBuyerOnboard/GetBuyerOnboardingApprovalPending';
            SocketService.get(url).then(function (resp) {
                if (resp.data.status == true) {
                    unlockUI();
                    $scope.buyerapplicationlist = resp.data.onboardapplicationdtl;
                }
                else unlockUI();
            });
        }

        $scope.GetsupplieronboardingPendinglist = function () { 
            getApprovalCount();
            lockUI();
            var url = 'api/AgrMstBuyerOnboard/GetSuprOnboardingApprovalPending';
            SocketService.get(url).then(function (resp) {
                if (resp.data.status == true) {
                    unlockUI();
                    $scope.supplierapplicationlist = resp.data.onboardapplicationdtl;
                }
                else unlockUI();
            });
        }
         
        function getApprovalCount() { 
            lockUI();
            var url = 'api/AgrMstBuyerOnboard/GetApproverPendingCountDetail';
            SocketService.get(url).then(function (resp) {
                if (resp.data.status == true) {
                    unlockUI();
                    $scope.approvalcount = resp.data;
                }
                else unlockUI();
            });
        }

        $scope.rejectedsummary = function () {
            $location.url('app/AgrMstCustomerOnboardRejectedSummary?hash=' + cmnfunctionService.encryptURL('FromRM=N'));
        }

        $scope.applcreation_view = function (application_gid) {
            $location.url('app/AgrMstCustomerOnboardingApproval?hash=' + cmnfunctionService.encryptURL('application_gid=' + application_gid + '&selectedIndex=' + $scope.selectedIndex + '&lsApp=N&FromRM=N'));
        }

        $scope.suprapplcreation_view = function (application_gid) {
            $location.url('app/AgrMstSupplierOnboardingApproval?hash=' + cmnfunctionService.encryptURL('application_gid=' + application_gid + '&selectedIndex=' + $scope.selectedIndex + '&lsApp=N&FromRM=N'));
        }

        $scope.applcreation_edit = function (application_gid) {
            $location.url('app/AgrMstByrOnboardApprovalEdit?hash=' + cmnfunctionService.encryptURL('application_gid=' + application_gid + '&lsedit=OnPending'));
        }

        $scope.suprapplcreation_edit = function (application_gid) {
            $location.url('app/AgrMstSuprOnboardApprovalEdit?hash=' + cmnfunctionService.encryptURL('application_gid=' + application_gid + '&lsedit=OnPending'));
        }

        $scope.onboardappdetailinfo = function (onboard_gid) {
            $location.url('app/AgrMstOnboardingApplicationInfo?hash=' + cmnfunctionService.encryptURL('onboard_gid=' + onboard_gid + '&selectedIndex=' + $scope.selectedIndex + '&lsdynamiclimitmanagementback=' + lsdynamiclimitmanagementback + '&lsApp=N&FromRM=N'));
        }
    }
})();
