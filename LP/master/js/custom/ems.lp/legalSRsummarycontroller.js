(function () {
    'use strict';

    angular
        .module('angle')
        .controller('legalSRsummarycontroller', legalSRsummarycontroller);

    legalSRsummarycontroller.$inject = ['$rootScope', '$scope', '$state', '$cookies', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', '$route'];

    function legalSRsummarycontroller($rootScope, $scope, $state, $cookies, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, $route) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'legalSRsummarycontroller';

        activate();

        function activate() {
            var url = 'api/lawyerlegalSR/GetraiselegalSR';
            lockUI();
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.assignlegalSR = resp.data.assignlegalSR;
                console.log(resp);
            });
        }

        $scope.ViewlegalSR = function (legalSR_gid,customer_gid) {
            localStorage.setItem('LawlegalSR_gid', legalSR_gid);
            localStorage.setItem('Lawcustomer_gid', customer_gid);
            $state.go('app.lawyerLegalSR360');
        }
    }
})();
