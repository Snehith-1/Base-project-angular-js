(function () {
    'use strict';

    angular
        .module('angle')
        .controller('dnReportcontroller', dnReportcontroller);

    dnReportcontroller.$inject = ['$rootScope', '$scope', '$state', '$modal', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route'];

    function dnReportcontroller($rootScope, $scope, $state, $modal, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'dnReportcontroller';

        activate();
        function activate() {
            lockUI();
            var url = "api/lglDashboard/getdnTAT"

            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.dnTAT_list = resp.data.dnTAT_list;
                console.log(resp.data.dnTAT_list);
            });

        }



    }
})();
