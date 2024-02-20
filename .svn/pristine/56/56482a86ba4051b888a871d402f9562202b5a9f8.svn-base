(function () {
    'use strict';

    angular
        .module('angle')
        .controller('idasDashboard', idasDashboard);

    idasDashboard.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', 'Colors', 'ChartData', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', '$route', 'ngTableParams', '$timeout', 'SweetAlert', '$cookies'];

    function idasDashboard($rootScope, $scope, $state, AuthenticationService, Colors, ChartData, $modal, ScopeValueService, $http, SocketService, Notify, $location, $route, ngTableParams, $timeout, SweetAlert, $cookies) {
        var vm = this;
        vm.title = 'idasDashboard';

        activate();

        function activate() {
            var url = 'api/IdasDashboard/IdasUserPrivilege';
            
            var params = {
                module_gid: 'IDS'
            };
            SocketService.getparams(url, params).then(function (resp) {
               
                var lsSanctionDocument = resp.data.idasUserPrivilege_List.map(function (e) { return e.idasUserPrivilege }).indexOf("IDSMSTCOR");
                var lsMaker = resp.data.idasUserPrivilege_List.map(function (e) { return e.idasUserPrivilege }).indexOf("IDSDOCMAK");
                var lsChecker = resp.data.idasUserPrivilege_List.map(function (e) { return e.idasUserPrivilege }).indexOf("IDSDOCCHK");
                
                var lsRmResponse = resp.data.idasUserPrivilege_List.map(function (e) { return e.idasUserPrivilege }).indexOf("IDSDOCRMR");

                var lsLSA = resp.data.idasUserPrivilege_List.map(function (e) { return e.idasUserPrivilege }).indexOf("IDSDOCLSA");
                var lsPhysicalDocument = resp.data.idasUserPrivilege_List.map(function (e) { return e.idasUserPrivilege }).indexOf("IDSDOCPHY");
                var lsFileMgmt = resp.data.idasUserPrivilege_List.map(function (e) { return e.idasUserPrivilege }).indexOf("IDSDOMARC");
                var lsRetrievalReq = resp.data.idasUserPrivilege_List.map(function (e) { return e.idasUserPrivilege }).indexOf("IDSDOMRET");
              
                var lsDocUpload = resp.data.idasUserPrivilege_List.map(function (e) { return e.idasUserPrivilege }).indexOf("IDSDOMUPL");
                var lsCourierMgmt = resp.data.idasUserPrivilege_List.map(function (e) { return e.idasUserPrivilege }).indexOf("IDSDOMCOU");
                var lsDocTagging = resp.data.idasUserPrivilege_List.map(function (e) { return e.idasUserPrivilege }).indexOf("IDSDOMCUL");

                if (lsSanctionDocument != -1) {
                    $scope.SanctionDocument = 'Y';
                }
                else {
                    $scope.SanctionDocument = 'N';
                }
                if (lsMaker != -1) {
                    $scope.Maker = 'Y';
                }
                else {
                    $scope.Maker = 'N';
                }
                if (lsChecker != -1) {
                    $scope.Checker = 'Y';
                }
                else {
                    $scope.Checker = 'N';
                }
                if (lsRmResponse != -1) {
                    $scope.RmResponse = 'Y';
                }
                else {
                    $scope.RmResponse = 'N';
                }
                if (lsLSA != -1) {
                    $scope.lsa = 'Y';
                }
                else {
                    $scope.lsa = 'N';
                }
              
                if (lsPhysicalDocument != -1) {
                    $scope.PhysicalDoc = 'Y';
                }
                else {
                    $scope.PhysicalDoc = 'N';
                }

                if (lsFileMgmt != -1) {
                    $scope.FileMgmt = 'Y';
                }
                else {
                    $scope.FileMgmt = 'N';
                }

                if (lsRetrievalReq != -1) {
                    $scope.RetrievalReq = 'Y';
                }
                else {
                    $scope.RetrievalReq = 'N';
                }


                if (lsCourierMgmt != -1) {
                    $scope.CourierMgmt = 'Y';
                }
                else {
                    $scope.CourierMgmt = 'N';
                }

                if (lsDocUpload != -1) {
                    $scope.DocUpload = 'Y';
                }
                else {
                    $scope.DocUpload = 'N';
                }

                if (lsDocTagging != -1) {
                    $scope.DocumentTagging = 'Y';
                }
                else {
                    $scope.DocumentTagging = 'N';
                }
              
               
            });
        }
    }
})();
