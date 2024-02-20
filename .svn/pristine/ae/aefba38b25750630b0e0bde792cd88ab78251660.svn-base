(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AgrMstSuprScannedCompletedSummaryController', AgrMstSuprScannedCompletedSummaryController);

        AgrMstSuprScannedCompletedSummaryController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams'];

    function AgrMstSuprScannedCompletedSummaryController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AgrMstSuprScannedCompletedSummaryController';

        activate();

        function activate() {
            lockUI();
            var url = 'api/AgrMstSuprScannedDocument/GetCADScannedDocCompletedSummary';
            SocketService.get(url).then(function (resp) {
                $scope.scannedmakerapplicationlist = resp.data.scannedmakerapplication;
            });
            var url = 'api/AgrMstSuprScannedDocument/CADAppScannedDocCount';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.overallCountinfo = resp.data;
            });
        }

        $scope.maker = function () {
            $location.url('app/AgrMstSuprCadDeferralSummary');
        }

        $scope.checker = function () {
            $location.url('app/AgrMstSuprCadDeferralCheckerSummary');
        }

        $scope.approval = function () {
            $location.url('app/AgrMstSuprCadDeferralApprovalSummary');
        }

        $scope.maker_process = function (val, val1) {
            $location.url('app/AgrTrnSuprCadDeferralGuarantorDtls?application_gid=' + val + '&lsemployee_gid=' + val1 + '&lspage=CadScannedCompleted');
        }

        $scope.view = function (val) {
            $location.url('app/AgrMstSuprCadApplicationView?application_gid=' + val + '&lspage=CadScannedCompleted');
        }

        $scope.Completed = function () {

        }

    }
})();