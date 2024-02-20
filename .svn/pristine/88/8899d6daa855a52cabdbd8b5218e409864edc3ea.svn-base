(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstCadDeferralCheckerSummary', MstCadDeferralCheckerSummary);

    MstCadDeferralCheckerSummary.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams'];

    function MstCadDeferralCheckerSummary($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstCadDeferralCheckerSummary';

        activate();

        function activate() {
            lockUI();
            var url = 'api/MstScannedDocument/GetCADScannedDocCheckerSummary';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.scannedcheckerpendinglist = resp.data.scannedmakerapplication;
            }); 
           
            var url = 'api/MstScannedDocument/CADAppScannedDocCount';
            lockUI();
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.overallCountinfo = resp.data;
            });
        }

        $scope.CheckerPendingSummary = function () {
            lockUI();
            var url = 'api/MstScannedDocument/GetCADScannedDocCheckerSummary';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.scannedcheckerpendinglist = resp.data.scannedmakerapplication;
            });

            var url = 'api/MstScannedDocument/CADAppScannedDocCount';
            lockUI();
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.overallCountinfo = resp.data;
            });
        }

        $scope.CheckerApprovalPendingSummary = function () {
            lockUI();
            var url = 'api/MstScannedDocument/GetCADScannedDocApprovalCheckerSummary';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.scannedcheckerapprovalpendinglist = resp.data.scannedmakerapplication;
            });

            var url = 'api/MstScannedDocument/CADAppScannedDocCount';
            lockUI();
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.overallCountinfo = resp.data;
            });
        }

        $scope.CheckerFollowupSummary = function () {
            lockUI();
            var url = 'api/MstScannedDocument/GetCADScannedDocFollowupCheckerSummary';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.scannedcheckerFollowuplist = resp.data.scannedmakerapplication;
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

        $scope.view = function (val) {
            $location.url('app/MstCadApplicationView?application_gid=' + val + '&lspage=CadScannedDocChecker');
        }

        $scope.Completed = function () {
            $location.url('app/MstScannedCompletedSummary');
        }

        $scope.checker_process = function (val, val1, processtypeassign_gid) {
            if (processtypeassign_gid != undefined)
                $location.url('app/MstCadDeferralGuarantorDtls?application_gid=' + val + '&lsemployee_gid=' + val1 + '&lsprocesstypeassign_gid=' + processtypeassign_gid + '&lspage=CadDeferralChecker&lspath=Checker');
            else
                $location.url('app/MstCadDeferralGuarantorDtls?application_gid=' + val + '&lsemployee_gid=' + val1 + '&lspage=CadDeferralChecker');
        }
    }
})();