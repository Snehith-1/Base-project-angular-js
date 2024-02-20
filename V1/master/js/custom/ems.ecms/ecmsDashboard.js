(function () {
    'use strict';

    angular
        .module('angle')
        .controller('ecmsDashboard', ecmsDashboard);

    ecmsDashboard.$inject = ['$location', '$scope','SocketService'];

    function ecmsDashboard($location, $scope, SocketService) {
        /* jshint validthis:true */
        var vm = this;
       

        vm.title = 'ecmsDashboard';

        activate();

        function activate() {
            
            $scope.welcome_msg = 'Deferral Tracking System';
            var url = 'api/ecmsdashboard/ecmmsprivilege';
            var user_gid = localStorage.getItem('user_gid');
            SocketService.get(url + '?user_gid=' + user_gid).then(function (resp) {

                var customer = resp.data.ecmsprivilege_list.map(function (e) { return e.ecmsprivilege }).indexOf("ECMMSTCMR");
               
                var deferralmgmt = resp.data.ecmsprivilege_list.map(function (e) { return e.ecmsprivilege }).indexOf("ECMTSNDFM");
                var loanmgmt = resp.data.ecmsprivilege_list.map(function (e) { return e.ecmsprivilege }).indexOf("ECMTSNLON");
                var cadreport = resp.data.ecmsprivilege_list.map(function (e) { return e.ecmsprivilege }).indexOf("ECMRPTRPV");
                var userreport = resp.data.ecmsprivilege_list.map(function (e) { return e.ecmsprivilege }).indexOf("ECMRPTUSE");
                var deferralreport = resp.data.ecmsprivilege_list.map(function (e) { return e.ecmsprivilege }).indexOf("ECMRPTDFR");
                var rmdeferral = resp.data.ecmsprivilege_list.map(function (e) { return e.ecmsprivilege }).indexOf("ECMTSNRMD");
                var transferrm = resp.data.ecmsprivilege_list.map(function (e) { return e.ecmsprivilege }).indexOf("ECMTSNTRM");
                var defapproval = resp.data.ecmsprivilege_list.map(function (e) { return e.ecmsprivilege }).indexOf("ECMTSNDRA");


                var reopendeferrals = resp.data.ecmsprivilege_list.map(function (e) { return e.ecmsprivilege }).indexOf("ECMTSNRE");
                var collateralmanagement = resp.data.ecmsprivilege_list.map(function (e) { return e.ecmsprivilege }).indexOf("ECMTSNCOL");
                var penaltyalert = resp.data.ecmsprivilege_list.map(function (e) { return e.ecmsprivilege }).indexOf("ECMTSNPEA");

                var customeralert = resp.data.ecmsprivilege_list.map(function (e) { return e.ecmsprivilege }).indexOf("ECMTSNCRA");
                var mailmanagement = resp.data.ecmsprivilege_list.map(function (e) { return e.ecmsprivilege }).indexOf("ECMTSNCAM");
                var checkerapproval = resp.data.ecmsprivilege_list.map(function (e) { return e.ecmsprivilege }).indexOf("ECMTSNCKA");

                var userreport2 = resp.data.ecmsprivilege_list.map(function (e) { return e.ecmsprivilege }).indexOf("ECMRPTUS2");
               
                if (customer != -1) {
                    $scope.customer_show = 'Y';
                }
                if (deferralmgmt != -1) {
                    $scope.deferralmgmt_show = 'Y';
                }
                if (loanmgmt != -1) {
                    $scope.loanmgmt_show = 'Y';
                }
                if (cadreport != -1) {
                    $scope.cadreport_show = 'Y';
                }
                if (userreport != -1) {
                    $scope.userreport_show = 'Y';
                }
                if (deferralreport != -1) {
                    $scope.deferralreport_show = 'Y';
                }
                if (rmdeferral != -1) {
                    $scope.rmdeferral_show = 'Y';
                }
                if (transferrm != -1) {
                    $scope.transferrm_show = 'Y';
                }
                if (defapproval != -1) {
                    $scope.deferralapproval_show = 'Y';
                }
                if (reopendeferrals != -1) {
                    $scope.reopendeferrals_show = 'Y';
                }
                if (collateralmanagement != -1) {
                    $scope.coltralmgmnt_show = 'Y';
                }
                else if (collateralmanagement == -1) {
                    $scope.coltralmgmnt_show = 'N';
                }
                if (penaltyalert != -1) {
                    $scope.penaltyalert_show = 'Y';
                }
                if (customeralert != -1) {
                    $scope.customeralert_show = 'Y';
                }
                if (mailmanagement != -1) {
                    $scope.mailmgmt_show = 'Y';
                }
                if (checkerapproval != -1) {
                    $scope.checkerapproval_show = 'Y';
                }
                if(userreport2 != -1)
                {
                    $scope.userreport2_show = 'Y';
                }
            });

        };
        $scope.customer = function () {
            $scope.welcome_msg = 'Customer';
        };
        $scope.loanmgmt = function () {
            $scope.welcome_msg = 'Loan Management';
        };
        $scope.deferralmgmt = function () {
            $scope.welcome_msg = 'Deferral Management';
        };
        $scope.cad_report = function () {
            $scope.welcome_msg = 'CAD Report';
        };
        $scope.user_report = function () {
            $scope.welcome_msg = 'User Report';
        };
        $scope.deferral_report = function () {
            $scope.welcome_msg = 'Deferral Report';
        };

    }
})();
