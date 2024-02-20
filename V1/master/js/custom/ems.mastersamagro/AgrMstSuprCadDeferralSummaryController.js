(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AgrMstSuprCadDeferralSummaryController', AgrMstSuprCadDeferralSummaryController);

        AgrMstSuprCadDeferralSummaryController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams'];

    function AgrMstSuprCadDeferralSummaryController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AgrMstSuprCadDeferralSummaryController';

        activate();

        function activate() {
            lockUI();
            var url = 'api/AgrMstSuprScannedDocument/GetCADScannedDocMakerSummary';
            SocketService.get(url).then(function (resp) {
                $scope.scannedmakerpendinglist = resp.data.scannedmakerapplication;
            });

           
            var url = 'api/AgrMstSuprScannedDocument/CADAppScannedDocCount';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.overallCountinfo = resp.data;
            });
        }

        $scope.makerPendingSUmmary = function () {
            lockUI();
            var url = 'api/AgrMstSuprScannedDocument/GetCADScannedDocMakerSummary';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.scannedmakerpendinglist = resp.data.scannedmakerapplication;
            });
        } 

        $scope.makerFolloupSummary = function () {
            lockUI();
            var url = 'api/AgrMstSuprScannedDocument/GetCADScannedDocFollowupMakerSummary';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.scannedmakerFollowuplist = resp.data.scannedmakerapplication;
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

        $scope.maker_process = function (val, val1, processtypeassign_gid) {
            if (processtypeassign_gid != undefined)
                $location.url('app/AgrTrnSuprCadDeferralGuarantorDtls?application_gid=' + val + '&lsemployee_gid=' + val1 + '&lsprocesstypeassign_gid=' + processtypeassign_gid + '&lspage=CadDeferralMaker&lspath=Maker');
            else
                $location.url('app/AgrTrnSuprCadDeferralGuarantorDtls?application_gid=' + val + '&lsemployee_gid=' + val1 + '&lspage=CadDeferralMaker');
        }

        $scope.view = function (val) {
            $location.url('app/AgrMstSuprCadApplicationView?application_gid=' + val + '&lspage=CadScannedDocMaker');
        }

        $scope.Completed = function () {
            $location.url('app/AgrMstSuprScannedCompletedSummary');
        }
        
    }
})();