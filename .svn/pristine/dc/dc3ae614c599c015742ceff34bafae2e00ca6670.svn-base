(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AgrTrnCadPhysicalDocSummaryController', AgrTrnCadPhysicalDocSummaryController);

        AgrTrnCadPhysicalDocSummaryController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams'];

    function AgrTrnCadPhysicalDocSummaryController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AgrTrnCadPhysicalDocSummaryController';

        activate();

        function activate() {
            lockUI();
            var url = 'api/AgrTrnPhysicalDocument/GetCADPhysicalDocMakerSummary';
            SocketService.get(url).then(function (resp) {
                $scope.scannedmakerpendinglist = resp.data.physicalmakerapplication;
            });


            var url = 'api/AgrTrnPhysicalDocument/CADAppPhysicalDocCount';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.overallCountinfo = resp.data;
            });
        }

        $scope.makerPendingSUmmary = function () {
            lockUI();
            var url = 'api/AgrTrnPhysicalDocument/GetCADPhysicalDocMakerSummary';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.scannedmakerpendinglist = resp.data.physicalmakerapplication;
            });
        }

        $scope.makerFolloupSummary = function () {
            lockUI();
            var url = 'api/AgrTrnPhysicalDocument/GetCADPhysicalDocFollowupMakerSummary';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.scannedmakerFollowuplist = resp.data.physicalmakerapplication;
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

        $scope.Completed = function () {
            $location.url('app/AgrTrnCadPhysicalDocCompletedSummary');
        }

        $scope.maker_process = function (val, val1, processtypeassign_gid) {
            if (processtypeassign_gid != undefined)
                $location.url('app/AgrTrnCadPhysicalDocGuarantorDtls?application_gid=' + val + '&lsemployee_gid=' + val1 + '&lsprocesstypeassign_gid=' + processtypeassign_gid + '&lspage=CadPhysicalDocMaker&lspath=Maker');
            else
                $location.url('app/AgrTrnCadPhysicalDocGuarantorDtls?application_gid=' + val + '&lsemployee_gid=' + val1 + '&lspage=CadPhysicalDocMaker');
        }

        $scope.view = function (val) {
            $location.url('app/AgrMstCadApplicationView?application_gid=' + val + '&lspage=CadPhysicalDocMaker');
        }
    }
})();
