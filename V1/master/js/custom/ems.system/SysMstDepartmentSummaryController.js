(function () {
    'use strict';

    angular
        .module('angle')
        .controller('SysMstDepartmentSummaryController', SysMstDepartmentSummaryController);

    SysMstDepartmentSummaryController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function SysMstDepartmentSummaryController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'SysMstDepartmentSummaryController';

        activate();

        function activate() {
            var url = 'api/SystemMaster/GetDepartmentSummary';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.department_data = resp.data.master_list;
                unlockUI();
            });
        }

       
    }
})();

