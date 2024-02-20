(function () {
    'use strict';

    angular
        .module('angle')
        .controller('editLegalSRcontroller', editLegalSRcontroller);

    editLegalSRcontroller.$inject = ['$rootScope', '$scope', '$modal', '$state', 'SweetAlert', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', '$cookies', '$filter', 'ngTableParams', '$resource', '$timeout', 'ngTableDataService'];

    function editLegalSRcontroller($rootScope, $scope, $modal, $state, SweetAlert, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, $cookies, $filter, ngTableParams, $resource, $timeout, ngTableDataService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'editLegalSRcontroller';

        activate();


        function activate() {
            //var url = 'api/Lawfirm/lawfirmdetail';
            //lockUI();
            //SocketService.get(url).then(function (resp) {
            //    unlockUI();
            //    $scope.lawfirm_data = resp.data.lawfirm_list;

            //});

        }

        $scope.srback = function (val) {
            $state.go('app.legalSRsummary');
        }

    }
})();
