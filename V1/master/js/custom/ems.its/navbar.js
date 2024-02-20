(function () {
    'use strict';

    angular
        .module('angle')
        .controller('navbar', navbar);

    navbar.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route'];

    function navbar($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'navbar';

        activate();

        function activate() {
            setInterval(notifications, 6000);
            var user_gid = localStorage.getItem('user_gid');
            var url = apiManage.apiList['userData'].api;
            SocketService.get(url + '?user_gid=' + user_gid).then(function (resp) {
                if (resp.data.status == true) {
                    $scope.userData = resp.data;
                   
                }
            });
        };
        function notifications() {
            var url = 'api/landingPage/landingpagedata';
            SocketService.get(url).then(function (resp) {
                var lscount_acknowledgement, lscount_surrender, lscount_tmpsurrender, lscount_tmpholding, lscount_temporaryhandover, lscount_response, lscount_myapprovals;
                if (resp.data.count_acknowledgement==null) {
                  lscount_acknowledgement = '0';
                }
                else {
                     lscount_acknowledgement = resp.data.count_acknowledgement;
                }
                if (resp.data.count_surrender == null) {
                    lscount_surrender = '0';
                }
                else {
                    lscount_surrender = resp.data.count_surrender;
                }
                if (resp.data.count_tmpsurrender == null) {
                    var lscount_tmpsurrender = '0';
                }
                else {
                    lscount_tmpsurrender = resp.data.count_tmpsurrender;
                }
                if (resp.data.count_tmpholding == null) {
                    lscount_tmpholding = '0';
                }
                else {
                    lscount_tmpholding = resp.data.count_tmpholding;
                }
                if (resp.data.count_temporaryhandover == null) {
                    lscount_temporaryhandover = '0';
                }
                else {
                    lscount_temporaryhandover = resp.data.count_temporaryhandover;
                }
                if (resp.data.count_myapprovals == null) {
                    lscount_myapprovals = '0';
                }
                else {
                    lscount_myapprovals = resp.data.count_myapprovals;
                }
                if (resp.data.count_response == null) {
                    lscount_response = '0';
                }
                else {
                    lscount_response = resp.data.count_response;
                }

                $scope.notification_count = lscount_acknowledgement + lscount_surrender + lscount_tmpsurrender + lscount_tmpholding + lscount_temporaryhandover + lscount_myapprovals + lscount_response;
 
            });
        };
    }
})();
