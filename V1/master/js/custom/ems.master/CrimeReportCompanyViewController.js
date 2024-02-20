(function () {
    'use strict';

    angular
        .module('angle')
        .controller('CrimeReportCompanyViewcontroller', CrimeReportCompanyViewcontroller);

        CrimeReportCompanyViewcontroller.$inject = ['$rootScope', '$scope', '$state', '$modal', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', '$sce'];

    function CrimeReportCompanyViewcontroller($rootScope, $scope, $state, $modal, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, $sce) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'CrimeReportCompanyViewcontroller';
        var crimereportinstitution_gid = localStorage.getItem('crimereportinstitution_gid');

        lockUI();
        activate();

        function activate() {
            var params = {
                crimereportinstitution_gid: crimereportinstitution_gid,
            }

            var url = 'api/CrimeCheckAPI/CrimeReportCompanyView';
            SocketService.getparams(url, params).then(function (resp) {
                unlockUI();

                $scope.requestId = resp.data.requestId;
                $scope.requestTime = resp.data.requestTime;
                $scope.responseTime = resp.data.responseTime;
                $scope.riskType = resp.data.riskType;
                $scope.riskSummary = resp.data.riskSummary;
                $scope.downloadLink = resp.data.downloadLink;
                $scope.casedetails_list = resp.data.caseDetails;

            }); 

        }

        $scope.close = function () {
            window.close();
        }
    }
})();
