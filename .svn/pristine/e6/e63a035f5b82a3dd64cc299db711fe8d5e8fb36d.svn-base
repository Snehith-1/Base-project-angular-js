(function () {
    'use strict';

    angular
        .module('angle')
        .controller('lglDashboardcontroller', lglDashboardcontroller);

    lglDashboardcontroller.$inject = ['$rootScope', '$scope', '$state', '$modal', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', '$route', '$cookies'];

    function lglDashboardcontroller($rootScope, $scope, $state, $modal, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, $route, $cookies) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'lglDashboardcontroller';

        activate();

        function activate() {

            $scope.lawyerempanelment = true;
            $scope.legalservices = true;
            $scope.legalcompliance = true;
            $scope.report = true;

            var user_gid = localStorage.getItem('user_gid');
            var url = 'api/user/privilegelevel3';
            SocketService.get(url + '?user_gid=' + user_gid).then(function (resp) {
                var registerlawyer = resp.data.privilegelevel3.map(function (e) { return e.project }).indexOf("LGLLERLRN");
                var managelawfirm = resp.data.privilegelevel3.map(function (e) { return e.project }).indexOf("LGLLERLFM");
                var requestcompliance = resp.data.privilegelevel3.map(function (e) { return e.project }).indexOf("LGLDCMREC");
                var managecompliance = resp.data.privilegelevel3.map(function (e) { return e.project }).indexOf("LGLDCMCOM");
                var dntracker = resp.data.privilegelevel3.map(function (e) { return e.project }).indexOf("LGLLCMDNP");
                var dntrackercbo = resp.data.privilegelevel3.map(function (e) { return e.project }).indexOf("LGLLCMDNC");
                var dntrackerreport = resp.data.privilegelevel3.map(function (e) { return e.project }).indexOf("LGLRPTDTR");
                var legalsrauthentication = resp.data.privilegelevel3.map(function (e) { return e.project }).indexOf("LGLLCMSRP");
                var legalsrapproval = resp.data.privilegelevel3.map(function (e) { return e.project }).indexOf("LGLLCMARM");
                var legalSLN = resp.data.privilegelevel3.map(function (e) { return e.project }).indexOf("LGLLCMSLN");           
                var lawyerpayment = resp.data.privilegelevel3.map(function (e) { return e.project }).indexOf("LGLLCMLTP");
                var dntatreport = resp.data.privilegelevel3.map(function (e) { return e.project }).indexOf("LGLRPTTAT");
                var dntrackerae = resp.data.privilegelevel3.map(function (e) { return e.project }).indexOf("LGLLCMDAE");
                var dntrackerfpoac = resp.data.privilegelevel3.map(function (e) { return e.project }).indexOf("LGLLCMDNC");
                var dntrackerfpo = resp.data.privilegelevel3.map(function (e) { return e.project }).indexOf("LGLLCMFPO");
                var dntrackerothers = resp.data.privilegelevel3.map(function (e) { return e.project }).indexOf("LGLLCMOTV");
                var dntrackerretail = resp.data.privilegelevel3.map(function (e) { return e.project }).indexOf("LGLLCMRET");

                if (requestcompliance != -1) {
                    $scope.requestcompliance_show = 'Y';
                }
                if (managecompliance != -1) {
                    $scope.managecompliance_show = 'Y';
                }
                if (dntracker != -1) {
                    $scope.dntracker_show = 'Y';
                }
                if (dntrackercbo != -1) {
                    $scope.dntrackercbo_show = 'Y';
                }
                if (dntrackerreport != -1) {
                    $scope.dntrackerreport_show = 'Y';
                }
                if (legalsrauthentication != -1) {
                    $scope.legalsrauthentication_show = 'Y';
                }
                if (legalsrapproval != -1) {
                    $scope.legalsrapproval_show = 'Y';
                }
                if (dntrackerae != -1) {
                    $scope.dntracker_ae = 'Y';
                }
                if (dntrackerfpoac != -1) {
                    $scope.dntracker_fpoac = 'Y';
                }
                if (dntrackerfpo != -1) {
                    $scope.dntracker_fpo = 'Y';
                }
                if (dntrackerothers != -1) {
                    $scope.dntracker_others = 'Y';
                }
                if (dntrackerretail != -1) {
                    $scope.dntracker_retail = 'Y';
                }
            });
            $scope.requestcompliance = function () {
                $scope.welcome_msg = 'Request Compliance';
            };
            $scope.managecompliance = function () {
                $scope.welcome_msg = 'Manage Compliance';
            };
            $scope.dntracker = function () {
                $scope.welcome_msg = 'DN Tracker';
            };
            $scope.dntrackercbo = function () {
                $scope.welcome_msg = 'DN Tracker CBO';
            };
            $scope.dntrackerreport = function () {
                $scope.welcome_msg = 'DN Tracker Report';
            };
            $scope.legalsrauthentication = function () {
                $scope.welcome_msg = 'Legal SR Authentication';
            };
            $scope.legalsrapproval = function () {
                $scope.welcome_msg = 'Legal SR Approval';
            };
        }
    }
})();
