(function () {
    'use strict';

    angular
        .module('angle')
        .controller('penalityAlertViewcontroller', penalityAlertViewcontroller);

    penalityAlertViewcontroller.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', 'ScopeValueService', '$http', 'Notify', 'SocketService', '$location', '$route', '$filter', 'ngTableParams', '$resource'];

    function penalityAlertViewcontroller($rootScope, $scope, $state, AuthenticationService, ScopeValueService, $http, Notify, SocketService, $location, $route, $filter, ngTableParams, $resource) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'penalityAlertViewcontroller';

        activate();

        function activate() {
            lockUI();
            var params = {
                customeralert_gid: localStorage.getItem('penalityalert_gid')
            }
            var url = "api/penalityAlert/Getcustomerpenalitydetails";
            SocketService.getparams(url, params).then(function (resp) {
               
                $scope.customerdetails = resp.data;
                $scope.deferral_data = resp.data.mailalert_list;
                $scope.penalityalert = resp.data.penalityalert_list;
                $scope.penalityhistory = resp.data.mailhistorydeferral_list;
            });

            unlockUI();
        }

        $scope.back = function () {
            $state.go('app.penalityAlert');
        }
    }
})();
