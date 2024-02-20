(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstScannedCompletedSummaryController', MstScannedCompletedSummaryController);

    MstScannedCompletedSummaryController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams'];

    function MstScannedCompletedSummaryController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstScannedCompletedSummaryController';

        activate();

        function activate() {
            lockUI();
            var url = 'api/MstScannedDocument/GetCADScannedDocCompletedSummary';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.scannedmakerapplicationlist = resp.data.scannedmakerapplication;
            });
            var url = 'api/MstScannedDocument/CADAppScannedDocCount';
            lockUI();
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.overallCountinfo = resp.data;
            });
        }

        $scope.maker = function () {
            $location.url('app/MstCadDeferralSummary');
        }

        $scope.checker = function () {
            $location.url('app/MstCadDeferralCheckerSummary');
        }

        $scope.approval = function () {
            $location.url('app/MstCadDeferralApprovalSummary');
        }

        $scope.maker_process = function (val, val1) {
            $location.url('app/MstCadDeferralGuarantorDtls?application_gid=' + val + '&lsemployee_gid=' + val1 + '&lspage=CadScannedCompleted');
        }

        $scope.view = function (val) {
            $location.url('app/MstCadApplicationView?application_gid=' + val + '&lspage=CadScannedCompleted');
        }

        $scope.Completed = function () {

        }

    }
})();