(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AgrTrnSuprCadPhysicalDocCompletedcontroller', AgrTrnSuprCadPhysicalDocCompletedcontroller);

        AgrTrnSuprCadPhysicalDocCompletedcontroller.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams'];

    function AgrTrnSuprCadPhysicalDocCompletedcontroller($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AgrTrnSuprCadPhysicalDocCompletedcontroller';

        activate();

        function activate() {
            lockUI();
            var url = 'api/AgrTrnSuprPhysicalDocument/GetCADPhysicalDocCompletedSummary';
            SocketService.get(url).then(function (resp) {
                $scope.scannedmakerapplicationlist = resp.data.physicalmakerapplication;
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

        $scope.maker_process = function (val, val1) {
            $location.url('app/AgrTrnSuprCadPhysicalDocGuarantorDtls?application_gid=' + val + '&lsemployee_gid=' + val1 + '&lspage=CadPhysicalCompleted');
        }

        $scope.view = function (val) {
            $location.url('app/AgrMstSuprCadApplicationView?application_gid=' + val + '&lspage=CadPhysicalCompleted');
        }
    }
})();
