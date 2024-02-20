(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AgrSuprHighmarkReportController', AgrSuprHighmarkReportController);

    AgrSuprHighmarkReportController.$inject = ['$rootScope', '$scope', '$state', '$modal', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', '$sce'];

    function AgrSuprHighmarkReportController($rootScope, $scope, $state, $modal, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, $sce) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AgrSuprHighmarkReportController';
        $scope.tmpcicdocument_gid = localStorage.getItem('tmpcicdocument_gid');
        lockUI();
        activate();

        function activate() {
            $scope.trustAsHtml = function (string) { return $sce.trustAsHtml(string); };

            var params = {
                tmpcicdocument_gid: $scope.tmpcicdocument_gid
            }

            var url = 'api/AgrSuprBureauAPI/GetHighmarkHTMLContent';

            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();
                $scope.html_content = resp.data.html_content;
            });

        }



        $scope.close = function () {
            window.close();
        }
    }
})();
