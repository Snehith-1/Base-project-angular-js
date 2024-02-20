(function () {
    'use strict';

    angular
        .module('angle')
        .controller('companypoliciescontroller', companypoliciescontroller);

    companypoliciescontroller.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams','$sce'];

    function companypoliciescontroller($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams,$sce) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'companypoliciescontroller';

        activate();

        function activate() {
            var url = "api/companyPolicy/policy";
            lockUI();
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.policy_list = resp.data.policy_list;
            });
        }
        $scope.trustAsHtml = function (string) { return $sce.trustAsHtml(string); };
    }
})();