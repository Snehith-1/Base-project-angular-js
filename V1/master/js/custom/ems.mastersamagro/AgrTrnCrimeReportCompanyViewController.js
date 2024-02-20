(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AgrTrnCrimeReportCompanyViewcontroller', AgrTrnCrimeReportCompanyViewcontroller);

        AgrTrnCrimeReportCompanyViewcontroller.$inject = ['$rootScope', '$scope', '$state', '$modal', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', '$sce'];

    function AgrTrnCrimeReportCompanyViewcontroller($rootScope, $scope, $state, $modal, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, $sce) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AgrTrnCrimeReportCompanyViewcontroller';
        var crimereportinstitution_gid = localStorage.getItem('crimereportinstitution_gid');

        lockUI();
        activate();

        function activate() {
            var params = {
                crimereportinstitution_gid: crimereportinstitution_gid,
            }

            var url = 'api/AgrCrimeCheckAPI/CrimeReportCompanyView';
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
