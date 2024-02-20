(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AgrTransUnionReportController', AgrTransUnionReportController);

    AgrTransUnionReportController.$inject = ['$rootScope', '$scope', '$state', '$modal', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', '$sce'];

    function AgrTransUnionReportController($rootScope, $scope, $state, $modal, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, $sce) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AgrTransUnionReportController';
        $scope.tmpcicdocument_gid = localStorage.getItem('tmpcicdocument_gid');
        lockUI();
        activate();

        function activate() {
            $scope.trustAsHtml = function (string) { return $sce.trustAsHtml(string); };

            var params = {
                tmpcicdocument_gid: $scope.tmpcicdocument_gid
            }

            var url = 'api/AgrBureauAPI/GetTransUnionXMLContent';

            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.xml_content = resp.data.xml_content;
            });

        }



        $scope.close = function () {
            window.close();
        }
    }
})();
