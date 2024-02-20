(function () {
    'use strict';

    angular
        .module('angle')
        .controller('mstDashboard', mstDashboard);

    mstDashboard.$inject = ['$location', '$scope', 'SocketService'];

    function mstDashboard($location, $scope, SocketService) {
        /* jshint validthis:true */
        var vm = this;


        vm.title = 'mstDashboard';

        activate();

        function activate() {

           

            var user_gid = localStorage.getItem('user_gid');
            var url = 'api/user/privilegelevel3';
            SocketService.get(url + '?user_gid=' + user_gid).then(function (resp) {
                var customer = resp.data.privilegelevel3.map(function (e) { return e.project }).indexOf("MSTMSTCUS");
                var register_customer = resp.data.privilegelevel3.map(function (e) { return e.project }).indexOf("MSTMSTRCS");
                var sanction_mis = resp.data.privilegelevel3.map(function (e) { return e.project }).indexOf("MSTMSTMIS");
                var sanction = resp.data.privilegelevel3.map(function (e) { return e.project }).indexOf("MSTMSTSAN");
                var vertical = resp.data.privilegelevel3.map(function (e) { return e.project }).indexOf("MSTMSTVER");
                var security = resp.data.privilegelevel3.map(function (e) { return e.project }).indexOf("MSTMSTSEC");
                var loan_facility = resp.data.privilegelevel3.map(function (e) { return e.project }).indexOf("MSTMSTFAC");
              
                if (customer != -1) {
                    $scope.customer_show = 'Y';
                }
                if (register_customer != -1) {
                    $scope.registercustomer_show = 'Y';
                }
                if (sanction_mis != -1) {
                    $scope.sanctionMIS_show = 'Y';
                }
                if (sanction != -1) {
                    $scope.sanction_show = 'Y';
                }
                if (vertical != -1) {
                    $scope.vertical_show = 'Y';
                }
                if (security != -1) {
                    $scope.securitytype_show = 'Y';
                }
                if (loan_facility != -1) {
                    $scope.loanfacility_show = 'Y';
                }
            });
            $scope.customer = function () {
                $scope.welcome_msg = 'Request Compliance';
            };
            $scope.register_customer = function () {
                $scope.welcome_msg = 'Manage Compliance';
            };
            $scope.sanction_mis = function () {
                $scope.welcome_msg = 'DN Tracker';
            };
            $scope.sanction = function () {
                $scope.welcome_msg = 'DN Tracker CBO';
            };
            $scope.vertical = function () {
                $scope.welcome_msg = 'DN Tracker Report';
            };
            $scope.security = function () {
                $scope.welcome_msg = 'Legal SR Authentication';
            };
            $scope.loan_facility = function () {
                $scope.welcome_msg = 'Legal SR Approval';
            };
        }
    }
})();
