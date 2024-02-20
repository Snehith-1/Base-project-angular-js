(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AgrTrnSuprCadPhysicalDocSummaryController', AgrTrnSuprCadPhysicalDocSummaryController);

        AgrTrnSuprCadPhysicalDocSummaryController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams'];

    function AgrTrnSuprCadPhysicalDocSummaryController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AgrTrnSuprCadPhysicalDocSummaryController';

        activate();

        function activate() {
            lockUI();
            var url = 'api/AgrTrnSuprPhysicalDocument/GetCADPhysicalDocMakerSummary';
            SocketService.get(url).then(function (resp) {
                $scope.scannedmakerpendinglist = resp.data.physicalmakerapplication;
            });


            var url = 'api/AgrTrnSuprPhysicalDocument/CADAppPhysicalDocCount';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.overallCountinfo = resp.data;
            });
        }

        $scope.makerPendingSUmmary = function () {
            lockUI();
            var url = 'api/AgrTrnSuprPhysicalDocument/GetCADPhysicalDocMakerSummary';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.scannedmakerpendinglist = resp.data.physicalmakerapplication;
            });
        }

        $scope.makerFolloupSummary = function () {
            lockUI();
            var url = 'api/AgrTrnSuprPhysicalDocument/GetCADPhysicalDocFollowupMakerSummary';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.scannedmakerFollowuplist = resp.data.physicalmakerapplication;
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

        $scope.Completed = function () {
            $location.url('app/AgrTrnSuprCadPhysicalDocCompletedSummary');
        }

        $scope.maker_process = function (val, val1, processtypeassign_gid) {
            if (processtypeassign_gid != undefined)
                $location.url('app/AgrTrnSuprCadPhysicalDocGuarantorDtls?application_gid=' + val + '&lsemployee_gid=' + val1 + '&lsprocesstypeassign_gid=' + processtypeassign_gid + '&lspage=CadPhysicalDocMaker&lspath=Maker');
            else
                $location.url('app/AgrTrnCSupradPhysicalDocGuarantorDtls?application_gid=' + val + '&lsemployee_gid=' + val1 + '&lspage=CadPhysicalDocMaker');
        }

        $scope.view = function (val) {
            $location.url('app/AgrMstSuprCadApplicationView?application_gid=' + val + '&lspage=CadPhysicalDocMaker');
        }
    }
})();
