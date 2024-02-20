(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AtmTrnApprovalController', AtmTrnApprovalController);

    AtmTrnApprovalController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', '$modal'];

    function AtmTrnApprovalController($rootScope, $scope, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, $modal) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AtmTrnApprovalController';

        activate();

        function activate() {


            var url = 'api/AtmTrnAuditorMaker/GetInitialApproval';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.initialapproval_list = resp.data.initialapproval_list;
                $scope.approvalhistory_list = resp.data.approvalhistory_list;
                unlockUI();

            });           
            $scope.view = function (val1,val2) {
                $location.url('app/AtmTrnApprovalView?initialapproval_gid=' + val1 + '&auditcreation_gid=' + val2)
            }
        }

        $scope.back = function (val) {
            $state.go('app.AtmTrnAuditorMakerSummary');
        }

    }
})();
