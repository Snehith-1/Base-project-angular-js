(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AgrMstScannedCompletedSummaryController', AgrMstScannedCompletedSummaryController);

        AgrMstScannedCompletedSummaryController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams'];

    function AgrMstScannedCompletedSummaryController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AgrMstScannedCompletedSummaryController';

        activate();

        function activate() {
            lockUI();
            var url = 'api/AgrMstScannedDocument/GetCADScannedDocCompletedSummary';
            SocketService.get(url).then(function (resp) {
                $scope.scannedmakerapplicationlist = resp.data.scannedmakerapplication;
            });
            var url = 'api/AgrMstScannedDocument/CADAppScannedDocCount';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.overallCountinfo = resp.data;
            });
        }

        $scope.maker = function () {
            $location.url('app/AgrMstCadDeferralSummary');
        }

        $scope.checker = function () {
            $location.url('app/AgrMstCadDeferralCheckerSummary');
        }

        $scope.approval = function () {
            $location.url('app/AgrMstCadDeferralApprovalSummary');
        }

        $scope.maker_process = function (val, val1) {
            $location.url('app/AgrTrnCadDeferralGuarantorDtls?application_gid=' + val + '&lsemployee_gid=' + val1 + '&lspage=CadScannedCompleted');
        }

        $scope.view = function (val) {
            $location.url('app/AgrMstCadApplicationView?application_gid=' + val + '&lspage=CadScannedCompleted');
        }

        $scope.Completed = function () {

        }

    }
})();