(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AgrMstCadDeferralCheckerSummary', AgrMstCadDeferralCheckerSummary);

        AgrMstCadDeferralCheckerSummary.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams'];

    function AgrMstCadDeferralCheckerSummary($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AgrMstCadDeferralCheckerSummary';

        activate();

        function activate() {
            lockUI();
            var url = 'api/AgrMstScannedDocument/GetCADScannedDocCheckerSummary';
            SocketService.get(url).then(function (resp) {
                $scope.scannedcheckerpendinglist = resp.data.scannedmakerapplication;
            }); 
           
            var url = 'api/AgrMstScannedDocument/CADAppScannedDocCount';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.overallCountinfo = resp.data;
            });
        }

        $scope.CheckerPendingSummary = function () {
            lockUI();
            var url = 'api/AgrMstScannedDocument/GetCADScannedDocCheckerSummary';
            SocketService.get(url).then(function (resp) {
                $scope.scannedcheckerpendinglist = resp.data.scannedmakerapplication;
            });

            var url = 'api/AgrMstScannedDocument/CADAppScannedDocCount';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.overallCountinfo = resp.data;
            });
        }

        $scope.CheckerApprovalPendingSummary = function () {
            lockUI();
            var url = 'api/AgrMstScannedDocument/GetCADScannedDocApprovalCheckerSummary';
            SocketService.get(url).then(function (resp) {
                $scope.scannedcheckerapprovalpendinglist = resp.data.scannedmakerapplication;
            });

            var url = 'api/AgrMstScannedDocument/CADAppScannedDocCount';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.overallCountinfo = resp.data;
            });
        }

        $scope.CheckerFollowupSummary = function () {
            lockUI();
            var url = 'api/AgrMstScannedDocument/GetCADScannedDocFollowupCheckerSummary';
            SocketService.get(url).then(function (resp) { 
                $scope.scannedcheckerFollowuplist = resp.data.scannedmakerapplication;
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

        $scope.view = function (val) {
            $location.url('app/AgrMstCadApplicationView?application_gid=' + val + '&lspage=CadScannedDocChecker');
        }

        $scope.Completed = function () {
            $location.url('app/AgrMstScannedCompletedSummary');
        }

        $scope.checker_process = function (val, val1, processtypeassign_gid) {
            if (processtypeassign_gid != undefined)
                $location.url('app/AgrTrnCadDeferralGuarantorDtls?application_gid=' + val + '&lsemployee_gid=' + val1 + '&lsprocesstypeassign_gid=' + processtypeassign_gid + '&lspage=CadDeferralChecker&lspath=Checker');
            else
                $location.url('app/AgrTrnCadDeferralGuarantorDtls?application_gid=' + val + '&lsemployee_gid=' + val1 + '&lspage=CadDeferralChecker');
        }
    }
})();