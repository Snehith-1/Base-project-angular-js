(function () {
    'use strict';

    angular
        .module('angle')
        .controller('welcome', welcome);

    welcome.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage',  '$route'];

    function welcome($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route){
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'welcome';

        activate();

        function activate() {
            $scope.welcome_msg = 'Financial Intermediation & Services Pvt. Ltd.';
            var user_gid = localStorage.getItem('user_gid');
            var url = 'api/user/privilege';
            SocketService.get(url + '?user_gid=' + user_gid).then(function (resp) {
                var its = resp.data.privileges.map(function (e) { return e.project }).indexOf("ITS");
                var ocs = resp.data.privileges.map(function (e) { return e.project }).indexOf("OCS");
                var asset = resp.data.privileges.map(function (e) { return e.project }).indexOf("AMS");
                var myapprovals = resp.data.privileges.map(function (e) { return e.project }).indexOf("APP");
                var ecms = resp.data.privileges.map(function (e) { return e.project }).indexOf("ECMS");
                if (its != -1)
                {
                    $scope.its = "Y";
                }
                if (ocs != -1) {
                    $scope.ocs = "Y";
                }
                if (asset != -1) {
                    $scope.asset = "Y";
                }
                if (myapprovals != -1) {
                    $scope.myapprovals = "Y";
                }
                if (ecms != -1) {
                    $scope.ecms = "Y";
                }

            });
            var url = 'api/landingPage/landingpagedata';
            SocketService.get(url).then(function (resp) {
                $scope.count = resp.data.count_acknowledgement + resp.data.count_surrender + resp.data.count_tmpsurrender + resp.data.count_tmpholding + resp.data.count_temporaryhandover;
                $scope.count1 = resp.data.count_myapprovals;
                $scope.count2 = resp.data.count_response;
            });

        };
        $scope.ecms = function () {
            $scope.welcome_msg = 'Exceptions & Covenant Management System for SAMFIN & SAMAGRO';
           
             };
        $scope.ams = function () {
            $scope.welcome_msg = 'Asset Management System';
        };
        $scope.sd = function () {
            $scope.welcome_msg = 'Service Desk';
        };
        $scope.TMS = function () {
            $scope.welcome_msg = 'Task Management System';
        };
        $scope.cms = function () {
            $scope.welcome_msg = 'Change Management System';
        };
        $scope.approval = function () {
            $scope.welcome_msg = 'My Approvals ( Service Desk , Change Management System )';
        };
    }
})();
