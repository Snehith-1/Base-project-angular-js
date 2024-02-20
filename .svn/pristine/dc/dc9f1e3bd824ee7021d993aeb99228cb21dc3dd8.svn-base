(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AgrTrnCadPhysicalDocCheckerSummaryController', AgrTrnCadPhysicalDocCheckerSummaryController);

        AgrTrnCadPhysicalDocCheckerSummaryController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams'];

    function AgrTrnCadPhysicalDocCheckerSummaryController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AgrTrnCadPhysicalDocCheckerSummaryController';

        activate();

        function activate() {
            lockUI();
            var url = 'api/AgrTrnPhysicalDocument/GetCADPhysicalDocCheckerSummary';
            SocketService.get(url).then(function (resp) {
                $scope.scannedcheckerpendinglist = resp.data.physicalmakerapplication;
            });

            var url = 'api/AgrTrnPhysicalDocument/CADAppPhysicalDocCount';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.overallCountinfo = resp.data;
            });
        }

        $scope.CheckerPendingSummary = function () {
            lockUI();
            var url = 'api/AgrTrnPhysicalDocument/GetCADPhysicalDocCheckerSummary';
            SocketService.get(url).then(function (resp) {
                $scope.scannedcheckerpendinglist = resp.data.physicalmakerapplication;
            });

            var url = 'api/AgrTrnPhysicalDocument/CADAppPhysicalDocCount';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.overallCountinfo = resp.data;
            });
        }

        $scope.CheckerApprovalPendingSummary = function () {
            lockUI();
            var url = 'api/AgrTrnPhysicalDocument/GetCADPhysicalDocApprovalCheckerSummary';
            SocketService.get(url).then(function (resp) {
                $scope.scannedcheckerapprovalpendinglist = resp.data.physicalmakerapplication;
            });

            var url = 'api/AgrTrnPhysicalDocument/CADAppPhysicalDocCount';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.overallCountinfo = resp.data;
            });
        }

        $scope.CheckerFollowupSummary = function () {
            lockUI();
            var url = 'api/AgrTrnPhysicalDocument/GetCADPhysicalDocFollowupCheckerSummary';
            SocketService.get(url).then(function (resp) {
                $scope.scannedcheckerFollowuplist = resp.data.physicalmakerapplication;
            });
            var url = 'api/AgrTrnPhysicalDocument/CADAppPhysicalDocCount';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.overallCountinfo = resp.data;
            });
        }

        $scope.maker = function () {
            $location.url('app/AgrTrnCadPhysicalDocSummary');
        }

        $scope.checker = function () {
            $location.url('app/AgrTrnCadPhysicalDocCheckerSummary');
        }

        $scope.approval = function () {
            $location.url('app/AgrTrnCadPhysicalDocApprovalSummary');
        }

        $scope.view = function (val) {
            $location.url('app/AgrMstCadApplicationView?application_gid=' + val + '&lspage=CadPhysicalDocChecker');
        }

        $scope.Completed = function () {
            $location.url('app/AgrTrnCadPhysicalDocCompletedSummary');
        }

        $scope.checker_process = function (val, val1, processtypeassign_gid) {
            if (processtypeassign_gid != undefined)
                $location.url('app/AgrTrnCadPhysicalDocGuarantorDtls?application_gid=' + val + '&lsemployee_gid=' + val1 + '&lsprocesstypeassign_gid=' + processtypeassign_gid + '&lspage=CadPhysicalDocChecker&lspath=Checker');
            else
                $location.url('app/AgrTrnCadPhysicalDocGuarantorDtls?application_gid=' + val + '&lsemployee_gid=' + val1 + '&lspage=CadPhysicalDocChecker');
        }

    }
})();
