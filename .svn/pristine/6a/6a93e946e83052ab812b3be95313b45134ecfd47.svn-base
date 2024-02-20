(function () {
    'use strict';

    angular
        .module('angle')
        .controller('idasTrnRmResponseSummary', idasTrnRmResponseSummary);

    idasTrnRmResponseSummary.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function idasTrnRmResponseSummary($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        $scope.title = 'idasTrnRmResponseSummary';

        activate();

        function activate() {
            lockUI();
            var url = "api/IdasTrnSanctionDoc/RmSummary";
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.sanctionlist = resp.data.MdlMakercheckerSummary;

            });
        }

        $scope.doc = function (sanction_gid) {

            localStorage.setItem('sanction_gid', sanction_gid);

            $state.go('app.idasTrnRmResponseDoc');
        }
    }
})();
