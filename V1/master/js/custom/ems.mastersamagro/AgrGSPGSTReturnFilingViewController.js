(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AgrGSPGSTReturnFilingViewController', AgrGSPGSTReturnFilingViewController);

    AgrGSPGSTReturnFilingViewController.$inject = ['$rootScope', '$scope', '$state', '$modal', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', '$sce'];

    function AgrGSPGSTReturnFilingViewController($rootScope, $scope, $state, $modal, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, $sce) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AgrGSPGSTReturnFilingViewController';
        var institution2branch_gid = localStorage.getItem('institution2branch_gid');
        lockUI();
        activate();

        function activate() {

            var params = {
                institution2branch_gid: institution2branch_gid,
            }

            var url = 'api/AgrMstAPIVerifications/GSTReturnFillingViewDetails';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.txtgstin = resp.data.result.gstin;

                $scope.is_any_delay = resp.data.result.compliance_status.is_any_delay;
                $scope.is_defaulter = resp.data.result.compliance_status.is_defaulter;

                $scope.yearly_list = resp.data.result.result;

            }); 

        } 

        $scope.close = function () {
            window.close();
        }
    }
})();
