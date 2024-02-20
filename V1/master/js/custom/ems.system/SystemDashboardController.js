(function () {
    'use strict';

    angular
        .module('angle')
        .controller('SystemDashboardController', SystemDashboardController);

        SystemDashboardController.$inject = ['$rootScope', '$scope', '$state', '$modal', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', '$route', '$cookies'];

    function SystemDashboardController($rootScope, $scope, $state, $modal, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, $route, $cookies) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'SystemDashboardController';

        activate();

        function activate() {
            var user_gid = localStorage.getItem('user_gid');
            var url = 'api/user/privilegelevel3';
            SocketService.get(url + '?user_gid=' + user_gid).then(function (resp) {
                var BloodGroup = resp.data.privilegelevel3.map(function (e) { return e.project }).indexOf("SYSTRNBGM");
                var BaseLocation = resp.data.privilegelevel3.map(function (e) { return e.project }).indexOf("SYSTRNBLM");
                var BusinessHead = resp.data.privilegelevel3.map(function (e) { return e.project }).indexOf("SYSTRNBSH");
                var CalendarGroup = resp.data.privilegelevel3.map(function (e) { return e.project }).indexOf("SYSTRNCGM");
                var ClientRole = resp.data.privilegelevel3.map(function (e) { return e.project }).indexOf("SYSTRNCRM");
                var Employee = resp.data.privilegelevel3.map(function (e) { return e.project }).indexOf("SYSTRNEMP");
                var GroupBusinessHead = resp.data.privilegelevel3.map(function (e) { return e.project }).indexOf("SYSTRNGBH");
                var OtherApplications = resp.data.privilegelevel3.map(function (e) { return e.project }).indexOf("SYSTRNOTA");
                var ProductHead = resp.data.privilegelevel3.map(function (e) { return e.project }).indexOf("SYSTRNPRH");
                var PhysicalStatus = resp.data.privilegelevel3.map(function (e) { return e.project }).indexOf("SYSTRNPSM");
                var Projects = resp.data.privilegelevel3.map(function (e) { return e.project }).indexOf("SYSTRNPTM");                
                var Salutation = resp.data.privilegelevel3.map(function (e) { return e.project }).indexOf("SYSTRNSTM");
                var TriggerUser = resp.data.privilegelevel3.map(function (e) { return e.project }).indexOf("SYSTRNTUM");                
                var rolemanagement = resp.data.privilegelevel3.map(function (e) { return e.project }).indexOf("SYSSETRMN");

                if (BloodGroup != -1) {
                    $scope.BloodGroup_show = 'Y';
                }if (BaseLocation != -1) {
                    $scope.BaseLocation_show = 'Y';
                }if (BusinessHead != -1) {
                    $scope.BusinessHead_show = 'Y';
                }if (CalendarGroup != -1) {
                    $scope.CalendarGroup_show = 'Y';
                }if (ClientRole != -1) {
                    $scope.ClientRole_show = 'Y';
                }if (Employee != -1) {
                    $scope.Employee_show = 'Y';
                }if (GroupBusinessHead != -1) {
                    $scope.GroupBusinessHead_show = 'Y';
                }if (OtherApplications != -1) {
                    $scope.OtherApplications_show = 'Y';
                }if (ProductHead != -1) {
                    $scope.ProductHead_show = 'Y';
                }if (PhysicalStatus != -1) {
                    $scope.PhysicalStatus_show = 'Y';
                }if (Projects != -1) {
                    $scope.Projects_show = 'Y';
                }if (Salutation != -1) {
                    $scope.Salutation_show = 'Y';
                }if (TriggerUser != -1) {
                    $scope.TriggerUser_show = 'Y';
                }if (rolemanagement != -1) {
                    $scope.RoleManagement_show = 'Y';
                }
            });
            
        }
    }
})();
