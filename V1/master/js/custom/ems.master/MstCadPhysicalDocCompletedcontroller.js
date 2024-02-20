(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstCadPhysicalDocCompletedcontroller', MstCadPhysicalDocCompletedcontroller);

    MstCadPhysicalDocCompletedcontroller.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams'];

    function MstCadPhysicalDocCompletedcontroller($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstCadPhysicalDocCompletedcontroller';

        activate();

        function activate() {
            lockUI();
            var url = 'api/MstPhysicalDocument/GetCADPhysicalDocCompletedSummary';
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.scannedmakerapplicationlist = resp.data.physicalmakerapplication;
            });
            var url = 'api/MstPhysicalDocument/CADAppPhysicalDocCount';
            lockUI();
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.overallCountinfo = resp.data;
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

        $scope.maker_process = function (val, val1) {
            $location.url('app/MstCadPhysicalDocGuarantorDtls?application_gid=' + val + '&lsemployee_gid=' + val1 + '&lspage=CadPhysicalCompleted');
        }

        $scope.view = function (val) {
            $location.url('app/MstCadApplicationView?application_gid=' + val + '&lspage=CadPhysicalCompleted');
        }
    }
})();
