(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AgrTrnSuprCadPhysicalDocCheckerSummaryController', AgrTrnSuprCadPhysicalDocCheckerSummaryController);

        AgrTrnSuprCadPhysicalDocCheckerSummaryController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams'];

    function AgrTrnSuprCadPhysicalDocCheckerSummaryController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AgrTrnSuprCadPhysicalDocCheckerSummaryController';

        activate();

        function activate() {
            lockUI();
            var url = 'api/AgrTrnSuprPhysicalDocument/GetCADPhysicalDocCheckerSummary';
            SocketService.get(url).then(function (resp) {
                $scope.scannedcheckerpendinglist = resp.data.physicalmakerapplication;
            });

            var url = 'api/AgrTrnSuprPhysicalDocument/CADAppPhysicalDocCount';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.overallCountinfo = resp.data;
            });
        }

        $scope.CheckerPendingSummary = function () {
            lockUI();
            var url = 'api/AgrTrnSuprPhysicalDocument/GetCADPhysicalDocCheckerSummary';
            SocketService.get(url).then(function (resp) {
                $scope.scannedcheckerpendinglist = resp.data.physicalmakerapplication;
            });

            var url = 'api/AgrTrnSuprPhysicalDocument/CADAppPhysicalDocCount';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.overallCountinfo = resp.data;
            });
        }

        $scope.CheckerApprovalPendingSummary = function () {
            lockUI();
            var url = 'api/AgrTrnSuprPhysicalDocument/GetCADPhysicalDocApprovalCheckerSummary';
            SocketService.get(url).then(function (resp) {
                $scope.scannedcheckerapprovalpendinglist = resp.data.physicalmakerapplication;
            });

            var url = 'api/AgrTrnSuprPhysicalDocument/CADAppPhysicalDocCount';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.overallCountinfo = resp.data;
            });
        }

        $scope.CheckerFollowupSummary = function () {
            lockUI();
            var url = 'api/AgrTrnSuprPhysicalDocument/GetCADPhysicalDocFollowupCheckerSummary';
            SocketService.get(url).then(function (resp) {
                $scope.scannedcheckerFollowuplist = resp.data.physicalmakerapplication;
            });
            var url = 'api/AgrTrnSuprPhysicalDocument/CADAppPhysicalDocCount';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.overallCountinfo = resp.data;
            });
        }

        $scope.maker = function () {
            $location.url('app/AgrTrnSuprCadPhysicalDocSummary');
        }

        $scope.checker = function () {
            $location.url('app/AgrTrnSuprCadPhysicalDocCheckerSummary');
        }

        $scope.approval = function () {
            $location.url('app/AgrTrnSuprCadPhysicalDocApprovalSummary');
        }

        $scope.view = function (val) {
            $location.url('app/AgrMstSuprCadApplicationView?application_gid=' + val + '&lspage=CadPhysicalDocChecker');
        }

        $scope.Completed = function () {
            $location.url('app/AgrTrnSuprCadPhysicalDocCompletedSummary');
        }

        $scope.checker_process = function (val, val1, processtypeassign_gid) {
            if (processtypeassign_gid != undefined)
                $location.url('app/AgrTrnSuprCadPhysicalDocGuarantorDtls?application_gid=' + val + '&lsemployee_gid=' + val1 + '&lsprocesstypeassign_gid=' + processtypeassign_gid + '&lspage=CadPhysicalDocChecker&lspath=Checker');
            else
                $location.url('app/AgrTrnSuprCadPhysicalDocGuarantorDtls?application_gid=' + val + '&lsemployee_gid=' + val1 + '&lspage=CadPhysicalDocChecker');
        }


        
    }
})();
