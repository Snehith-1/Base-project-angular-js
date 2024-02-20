(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AgrMstSuprCadDeferralCheckerSummary', AgrMstSuprCadDeferralCheckerSummary);

        AgrMstSuprCadDeferralCheckerSummary.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams'];

    function AgrMstSuprCadDeferralCheckerSummary($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AgrMstSuprCadDeferralCheckerSummary';

        activate();

        function activate() {
            lockUI();
            var url = 'api/AgrMstSuprScannedDocument/GetCADScannedDocCheckerSummary';
            SocketService.get(url).then(function (resp) {
                $scope.scannedcheckerpendinglist = resp.data.scannedmakerapplication;
            }); 
           
            var url = 'api/AgrMstSuprScannedDocument/CADAppScannedDocCount';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.overallCountinfo = resp.data;
            });
        }

        $scope.CheckerPendingSummary = function () {
            lockUI();
            var url = 'api/AgrMstSuprScannedDocument/GetCADScannedDocCheckerSummary';
            SocketService.get(url).then(function (resp) {
                $scope.scannedcheckerpendinglist = resp.data.scannedmakerapplication;
            });

            var url = 'api/AgrMstSuprScannedDocument/CADAppScannedDocCount';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.overallCountinfo = resp.data;
            });
        }

        $scope.CheckerApprovalPendingSummary = function () {
            lockUI();
            var url = 'api/AgrMstSuprScannedDocument/GetCADScannedDocApprovalCheckerSummary';
            SocketService.get(url).then(function (resp) {
                $scope.scannedcheckerapprovalpendinglist = resp.data.scannedmakerapplication;
            });

            var url = 'api/AgrMstSuprScannedDocument/CADAppScannedDocCount';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.overallCountinfo = resp.data;
            });
        }

        $scope.CheckerFollowupSummary = function () {
            lockUI();
            var url = 'api/AgrMstSuprScannedDocument/GetCADScannedDocFollowupCheckerSummary';
            SocketService.get(url).then(function (resp) { 
                $scope.scannedcheckerFollowuplist = resp.data.scannedmakerapplication;
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

        $scope.view = function (val) {
            $location.url('app/AgrMstSuprCadApplicationView?application_gid=' + val + '&lspage=CadScannedDocChecker');
        }

        $scope.Completed = function () {
            $location.url('app/AgrMstSuprScannedCompletedSummary');
        }

        $scope.checker_process = function (val, val1, processtypeassign_gid) {
            if (processtypeassign_gid != undefined)
                $location.url('app/AgrTrnSuprCadDeferralGuarantorDtls?application_gid=' + val + '&lsemployee_gid=' + val1 + '&lsprocesstypeassign_gid=' + processtypeassign_gid + '&lspage=CadDeferralChecker&lspath=Checker');
            else
                $location.url('app/AgrTrnSuprCadDeferralGuarantorDtls?application_gid=' + val + '&lsemployee_gid=' + val1 + '&lspage=CadDeferralChecker');
        }
    }
})();