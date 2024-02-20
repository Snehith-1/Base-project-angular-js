(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AgrMstCustomerOnboardRejectedSummarycontroller', AgrMstCustomerOnboardRejectedSummarycontroller);

    AgrMstCustomerOnboardRejectedSummarycontroller.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', '$route', 'ngTableParams', 'cmnfunctionService'];

    function AgrMstCustomerOnboardRejectedSummarycontroller($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, $route, ngTableParams, cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AgrMstCustomerOnboardRejectedSummarycontroller';
        var searchObject = cmnfunctionService.decryptURL($location.search().hash);
        var FromRM = searchObject.FromRM;
        activate();

        function activate() {

        }
        $scope.GetBuyeronboardingrejectedlist = function () {
            getApprovalCount();
            lockUI();
            var params = {
                FromRM: FromRM
            }
            var url = 'api/AgrMstBuyerOnboard/GetBuyerRejectedSummary';
            SocketService.getparams(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    unlockUI();
                    $scope.buyerapplicationlist = resp.data.onboardapplicationdtl;
                }
                else unlockUI();
            });
        }

        $scope.Getsupplieronboardingrejectedlist = function () {
            getApprovalCount();
            lockUI();
            var params = {
                FromRM: FromRM
            }
            var url = 'api/AgrMstBuyerOnboard/GetSupplierRejectedSummary';
            SocketService.getparams(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    unlockUI();
                    $scope.supplierapplicationlist = resp.data.onboardapplicationdtl;
                }
                else unlockUI();
            });
        }

        function getApprovalCount() {
            lockUI();
            var params = {
                FromRM: FromRM
            }
            var url = 'api/AgrMstBuyerOnboard/GetRejectedCountDetail';
            SocketService.getparams(url, params).then(function (resp) {
                if (resp.data.status == true) {
                    unlockUI();
                    $scope.approvalcount = resp.data;
                }
                else unlockUI();
            });
        }

        $scope.backbtn = function () {
            if (FromRM == "Y")
                $location.url('app/AgrMstCustomerOnboardingSummary');
            else
                $location.url('app/AgrMstCustomerApprovalSummary');
        }

        $scope.applcreation_view = function (application_gid) {
            $location.url('app/AgrMstCustomerOnboardingApproval?hash=' + cmnfunctionService.encryptURL('application_gid=' + application_gid + '&selectedIndex=' + $scope.selectedIndex + '&lsApp=R&FromRM=' +FromRM));
        }

        $scope.suprapplcreation_view = function (application_gid) {
            $location.url('app/AgrMstSupplierOnboardingApproval?hash=' + cmnfunctionService.encryptURL('application_gid=' + application_gid + '&selectedIndex=' + $scope.selectedIndex + '&lsApp=R&FromRM=' + FromRM));
        }
    }
})();
