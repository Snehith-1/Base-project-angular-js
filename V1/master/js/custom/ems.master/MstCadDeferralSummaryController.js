(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstCadDeferralSummaryController', MstCadDeferralSummaryController);

    MstCadDeferralSummaryController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams'];

    function MstCadDeferralSummaryController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstCadDeferralSummaryController';

        activate();

        function activate() {
            lockUI();
            var url = 'api/MstScannedDocument/GetCADScannedDocMakerSummary';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.scannedmakerpendinglist = resp.data.scannedmakerapplication;
            });

           
            var url = 'api/MstScannedDocument/CADAppScannedDocCount';
            lockUI();
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.overallCountinfo = resp.data;
            });
        }

        $scope.makerPendingSUmmary = function () {
            lockUI();
            var url = 'api/MstScannedDocument/GetCADScannedDocMakerSummary';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.scannedmakerpendinglist = resp.data.scannedmakerapplication;
            });
        } 

        $scope.makerFolloupSummary = function () {
            lockUI();
            var url = 'api/MstScannedDocument/GetCADScannedDocFollowupMakerSummary';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.scannedmakerFollowuplist = resp.data.scannedmakerapplication;
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

        $scope.maker_process = function (val, val1, processtypeassign_gid) {
            if (processtypeassign_gid != undefined)
                $location.url('app/MstCadDeferralGuarantorDtls?application_gid=' + val + '&lsemployee_gid=' + val1 + '&lsprocesstypeassign_gid=' + processtypeassign_gid + '&lspage=CadDeferralMaker&lspath=Maker');
            else
                $location.url('app/MstCadDeferralGuarantorDtls?application_gid=' + val + '&lsemployee_gid=' + val1 + '&lspage=CadDeferralMaker');
        }

        $scope.view = function (val) {
            $location.url('app/MstCadApplicationView?application_gid=' + val + '&lspage=CadScannedDocMaker');
        }

        $scope.Completed = function () {
            $location.url('app/MstScannedCompletedSummary');
        }

    }
})();