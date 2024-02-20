(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstCadPhysicalDocSummaryController', MstCadPhysicalDocSummaryController);

    MstCadPhysicalDocSummaryController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams'];

    function MstCadPhysicalDocSummaryController($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstCadPhysicalDocSummaryController';

        activate();

        function activate() {
            lockUI();
            var url = 'api/MstPhysicalDocument/GetCADPhysicalDocMakerSummary';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.scannedmakerpendinglist = resp.data.physicalmakerapplication;
            });


            var url = 'api/MstPhysicalDocument/CADAppPhysicalDocCount';
            lockUI();
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.overallCountinfo = resp.data;
            });
        }

        $scope.makerPendingSUmmary = function () {
            lockUI();
            var url = 'api/MstPhysicalDocument/GetCADPhysicalDocMakerSummary';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.scannedmakerpendinglist = resp.data.physicalmakerapplication;
            });
        }

        $scope.makerFolloupSummary = function () {
            lockUI();
            var url = 'api/MstPhysicalDocument/GetCADPhysicalDocFollowupMakerSummary';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.scannedmakerFollowuplist = resp.data.physicalmakerapplication;
            });
        }
          
        $scope.maker = function () {
            $location.url('app/MstCadPhysicalDocSummary');
        }

        $scope.checker = function () {
            $location.url('app/MstCadPhysicalDocCheckerSummary');
        }

        $scope.approval = function () {
            $location.url('app/MstCadPhysicalDocApprovalSummary');
        }

        $scope.Completed = function () {
            $location.url('app/MstCadPhysicalDocCompletedSummary');
        }

        $scope.maker_process = function (val, val1, processtypeassign_gid) {
            if (processtypeassign_gid != undefined)
                $location.url('app/MstCadPhysicalDocGuarantorDtls?application_gid=' + val + '&lsemployee_gid=' + val1 + '&lsprocesstypeassign_gid=' + processtypeassign_gid + '&lspage=CadPhysicalDocMaker&lspath=Maker');
            else
                $location.url('app/MstCadPhysicalDocGuarantorDtls?application_gid=' + val + '&lsemployee_gid=' + val1 + '&lspage=CadPhysicalDocMaker');
        }

        $scope.view = function (val) {
            $location.url('app/MstCadApplicationView?application_gid=' + val + '&lspage=CadPhysicalDocMaker');
        }
    }
})();
