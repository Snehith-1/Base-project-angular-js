(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstCheckerSummaryController', MstCheckerSummaryController);

    MstCheckerSummaryController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function MstCheckerSummaryController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstCheckerSummaryController';

        activate();

        function activate() {
            lockUI();

            var url = "api/IdasMstSanction/SanctionToCheckerSummary";
            SocketService.get(url).then(function (resp) {
                $scope.sanctionlist = resp.data.sanctiondetails;
                unlockUI();
            });
        }

        $scope.checkerview = function (customer2sanction_gid) {
            $location.url('app/idasMstSanctionLetterWordView?sanction_gid=' + customer2sanction_gid + '&lspage=checkersummary');
        }
    }
})();