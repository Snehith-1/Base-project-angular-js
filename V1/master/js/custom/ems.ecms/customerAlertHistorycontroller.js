(function () {
    'use strict';

    angular
        .module('angle')
        .controller('customerAlertHistorycontroller', customerAlertHistorycontroller);

    customerAlertHistorycontroller.$inject = ['$rootScope', '$scope', '$modal', '$state', 'SweetAlert', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', '$cookies', '$filter', 'ngTableParams', '$resource', '$timeout', 'ngTableDataService'];

    function customerAlertHistorycontroller($rootScope, $scope, $modal, $state, SweetAlert, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, $cookies, $filter, ngTableParams, $resource, $timeout, ngTableDataService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'customerAlertHistorycontroller';
        activate();
        function activate() {



            $scope.customer_gid = localStorage.getItem('customer_gid');
            $scope.pageNavigation = localStorage.getItem('mailManagement');
         
            var params = {
                customer_gid: $scope.customer_gid
            };

            var url = 'api/customerAlertGenerate/mailHistory';
            lockUI();
            SocketService.getparams(url, params).then(function (resp) {
                $scope.mail_data = resp.data.mailhistory_list;
                unlockUI();
            });


           
        }

        $scope.view = function (val) {
            $scope.customermail_gid = val;
            $scope.customermail_gid = localStorage.setItem('customermail_gid', val);
            $state.go('app.mailHistoryView');
        }

        $scope.back = function () {
            if ($scope.pageNavigation == "mailManagement")
            {
                $state.go('app.mailManagement');
            }
            else
            {
                $state.go('app.customerAlert');
            }
           
        }
    }
})();
