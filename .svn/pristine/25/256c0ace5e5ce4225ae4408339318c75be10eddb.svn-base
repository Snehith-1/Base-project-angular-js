(function () {
    'use strict';

    angular
        .module('angle')
        .controller('mailHistoryViewcontroller', mailHistoryViewcontroller);

    mailHistoryViewcontroller.$inject = ['$rootScope', '$scope', '$modal', '$state', 'SweetAlert', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', '$cookies', '$filter', 'ngTableParams', '$resource', '$timeout', 'ngTableDataService'];

    function mailHistoryViewcontroller($rootScope, $scope, $modal, $state, SweetAlert, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, $cookies, $filter, ngTableParams, $resource, $timeout, ngTableDataService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'mailHistoryViewcontroller';
        activate();
        function activate() {



            $scope.customermail_gid = localStorage.getItem('customermail_gid');
            var params = {
                customermail_gid: $scope.customermail_gid
            };

            var url = 'api/customerAlertGenerate/mailHistoryView';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                $scope.objCutomerAlert = resp.data;
                $scope.content = resp.data.content;
                document.getElementById('test').innerHTML += $scope.content;
                $scope.mail_data = resp.data.mailhistorydeferral_list;
                unlockUI();
            });



        }

       

        $scope.back = function () {

            $state.go('app.customerAlertHistory');
        }
    }
})();
