(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstCustomerRejectedRequestSummaryController', MstCustomerRejectedRequestSummaryController);

    MstCustomerRejectedRequestSummaryController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function MstCustomerRejectedRequestSummaryController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstCustomerRejectedRequestSummaryController';
        $scope.application_gid = $location.search().application_gid;
        var application_gid = $scope.application_gid;
        $scope.lspage = $location.search().lspage;
        var lspage = $scope.lspage;

        //$scope.selectallmenu_show = true;

        lockUI();
        activate();

        function activate() {

            var url = 'api/MstCADApplication/GetCustomerRejectedSummary';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.rejected_list = resp.data.rejected_list;
                unlockUI();
            });
            var url = 'api/MstCADApplication/Getcount';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.total_count = resp.data.total_count;
                unlockUI();
            });

        }

        $scope.pending = function () {
            $location.url('app/MstCustomerCreationRequestSummary');
        }
        $scope.urn_update = function () {
            $location.url('app/MstCustomerUpdatedRequestSummary');
        }
        $scope.rejected = function () {
            $location.url('app/MstCustomerRejectedRequestSummary');
        }
        $scope.view = function (cuclms_gid, application_gid) {

            $location.url('app/MstCustomercreationlmsurnupdation?cuclms_gid=' + cuclms_gid + '&application_gid=' + application_gid + '&lspage=CADCustomerURNrejectview');
        }




    }
})();