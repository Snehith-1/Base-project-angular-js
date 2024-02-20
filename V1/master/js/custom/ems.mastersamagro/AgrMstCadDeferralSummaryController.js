(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AgrMstCadDeferralSummaryController', AgrMstCadDeferralSummaryController);

        AgrMstCadDeferralSummaryController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams'];

    function AgrMstCadDeferralSummaryController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AgrMstCadDeferralSummaryController';

        activate();

        function activate() {
            lockUI();
            var url = 'api/AgrMstScannedDocument/GetCADScannedDocMakerSummary';
            SocketService.get(url).then(function (resp) {
                $scope.scannedmakerpendinglist = resp.data.scannedmakerapplication;
            });

           
            var url = 'api/AgrMstScannedDocument/CADAppScannedDocCount';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.overallCountinfo = resp.data;
            });
        }

        $scope.makerPendingSUmmary = function () {
            lockUI();
            var url = 'api/AgrMstScannedDocument/GetCADScannedDocMakerSummary';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.scannedmakerpendinglist = resp.data.scannedmakerapplication;
            });
        } 

        $scope.makerFolloupSummary = function () {
            lockUI();
            var url = 'api/AgrMstScannedDocument/GetCADScannedDocFollowupMakerSummary';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.scannedmakerFollowuplist = resp.data.scannedmakerapplication;
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

        $scope.maker_process = function (val, val1, processtypeassign_gid) {
            if (processtypeassign_gid != undefined)
                $location.url('app/AgrTrnCadDeferralGuarantorDtls?application_gid=' + val + '&lsemployee_gid=' + val1 + '&lsprocesstypeassign_gid=' + processtypeassign_gid + '&lspage=CadDeferralMaker&lspath=Maker');
            else
                $location.url('app/AgrTrnCadDeferralGuarantorDtls?application_gid=' + val + '&lsemployee_gid=' + val1 + '&lspage=CadDeferralMaker');
        }

        $scope.view = function (val) {
            $location.url('app/AgrMstCadApplicationView?application_gid=' + val + '&lspage=CadScannedDocMaker');
        }

        $scope.Completed = function () {
            $location.url('app/AgrMstScannedCompletedSummary');
        }
        
    }
})();