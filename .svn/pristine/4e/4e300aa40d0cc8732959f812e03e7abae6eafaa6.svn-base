(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AgrMstBuyerApprovedSummaryController', AgrMstBuyerApprovedSummaryController);

    AgrMstBuyerApprovedSummaryController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', '$route', 'ngTableParams', 'cmnfunctionService'];

    function AgrMstBuyerApprovedSummaryController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, $route, ngTableParams, cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AgrMstBuyerApprovedSummaryController';

        const lsdynamiclimitmanagementback = 'AgrMstBuyerApprovedSummary';


        activate();

        function activate() { }


        $scope.GetBuyeronboardingApprovedlist = function () {
            getApprovalCount();
            lockUI();
            var url = 'api/AgrMstBuyerOnboard/GetBuyerOnboardApprovedSummary';
            SocketService.get(url).then(function (resp) {
                if (resp.data.status == true) {
                    unlockUI();
                    $scope.buyerapplicationlist = resp.data.onboardapplicationdtl;
                }
                else unlockUI();
            });
        }

        //$scope.GetsupplieronboardingApprovedlist = function () {
        //    getApprovalCount();
        //    lockUI();
        //    var url = 'api/AgrMstBuyerOnboard/GetSupplierOnboardApprovedSummary';
        //    SocketService.get(url).then(function (resp) {
        //        if (resp.data.status == true) {
        //            unlockUI();
        //            $scope.supplierapplicationlist = resp.data.onboardapplicationdtl;
        //        }
        //        else unlockUI();
        //    });
        //}

        function getApprovalCount() {
            lockUI();
            var url = 'api/AgrMstBuyerOnboard/GetApproverApprovedCountDetail';
            SocketService.get(url).then(function (resp) {
                if (resp.data.status == true) {
                    unlockUI();
                    $scope.approvalcount = resp.data;
                }
                else unlockUI();
            });
        }


        $scope.initiate_buyeronboard = function (application_gid, application_no, customer_name) {
            $location.url('app/AgrMstInitiateApplication?hash=' + cmnfunctionService.encryptURL('application_gid=' + application_gid +"&FromBuyerApproval=Y"));
        }


        $scope.applcreation_view = function (application_gid) {
            $location.url('app/AgrMstCustomerOnboardingApproval?hash=' + cmnfunctionService.encryptURL('application_gid=' + application_gid + '&lsapproved=byr' ));
        }

        $scope.suprapplcreation_view = function (application_gid) {
            $location.url('app/AgrMstSupplierOnboardingApproval?hash=' + cmnfunctionService.encryptURL('application_gid=' + application_gid + '&lsapproved=supr' ));
        }

        $scope.onboardappdetailinfo = function (onboard_gid) {
            $location.url('app/AgrMstOnboardingApplicationInfo?hash=' + cmnfunctionService.encryptURL('onboard_gid=' + onboard_gid + '&selectedIndex=' + $scope.selectedIndex + '&lsdynamiclimitmanagementback=' + lsdynamiclimitmanagementback + '&lsApp=Y&FromRM=N' + '&lsparent=AppBuySummary' ));
        }

    }
})();
