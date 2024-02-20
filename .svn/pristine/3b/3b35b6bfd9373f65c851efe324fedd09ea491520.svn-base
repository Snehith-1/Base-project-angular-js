(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AtmTrnAuditApprovalController', AtmTrnAuditApprovalController);

    AtmTrnAuditApprovalController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', '$modal'];

    function AtmTrnAuditApprovalController($rootScope, $scope, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, $modal) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AtmTrnAuditApprovalController';

        activate();

        function activate() {


            var url = 'api/AtmTrnAuditorMaker/GetObservationApproval';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.approvalhistory_list = resp.data.approvalhistory_list;
                unlockUI();

            });

            var url = 'api/AtmTrnAuditorMaker/GetAuditApprovalHistory';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.auditapprovalhistory_list = resp.data.auditapprovalhistory_list;
                unlockUI();

            });
            $scope.auditview = function (val1, val2) {
                $location.url('app/AtmTrnAuditApprovalView?observationapproval_gid=' + val1 + '&auditcreation_gid=' + val2)
            }
        }

        $scope.back = function (val) {
            $state.go('app.AtmTrnAuditorMakerSummary');
        }

    }
})();