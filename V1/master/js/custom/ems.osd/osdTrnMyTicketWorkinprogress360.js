(function () {
    'use strict';

    angular
        .module('angle')
        .controller('osdTrnMyTicketWorkinprogress360', osdTrnMyTicketWorkinprogress360);

    osdTrnMyTicketWorkinprogress360.$inject = ['$rootScope', '$scope', '$modal', '$state', 'SweetAlert', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', '$cookies', '$filter', 'ngTableParams', '$resource', '$timeout', 'ngTableDataService','cmnfunctionService'];

    function osdTrnMyTicketWorkinprogress360($rootScope, $scope, $modal, $state, SweetAlert, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, $cookies, $filter, ngTableParams, $resource, $timeout, ngTableDataService,cmnfunctionService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'osdTrnMyTicketWorkinprogress360';

        activate();

        function activate() {
            lockUI();
            var url = window.location.href;
            var relpath = url.split("lstab=");
            $scope.relpath1 = relpath[1];
            unlockUI();
        }
        $scope.back = function (relpath1) {
            $location.url('app/osdTrnMyTicket?hash=' + cmnfunctionService.encryptURL('lstab=' + relpath1));
        }
    }
})();