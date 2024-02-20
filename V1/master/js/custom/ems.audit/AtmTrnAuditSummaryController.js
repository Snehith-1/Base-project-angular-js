(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AtmTrnAuditSummaryController', AtmTrnAuditSummaryController);

    AtmTrnAuditSummaryController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', '$modal'];

    function AtmTrnAuditSummaryController($rootScope, $scope, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, $modal) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AtmTrnAuditSummaryController';

        activate();

        function activate() {

            var url = 'api/AtmTrnAuditCreation/GetAuditCreation';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.auditcreation_list = resp.data.auditcreation_list;
                unlockUI();

            });

        }

        $scope.view = function (val1, val2, val3) {
            $location.url('app/AtmTrnAudit360View?auditcreation_gid=' + val1 + '&checklistmaster_gid=' + val2 + '&sampleimport_gid=' + val3)
        }

        $scope.createaudit = function () {
            $state.go('app.AtmTrnCreateAudit');
        }

    }
})();