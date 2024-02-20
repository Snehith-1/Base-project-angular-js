(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstCADCrimeReportCompanyViewcontroller', MstCADCrimeReportCompanyViewcontroller);

        MstCADCrimeReportCompanyViewcontroller.$inject = ['$rootScope', '$scope', '$state', '$modal', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', '$sce'];

    function MstCADCrimeReportCompanyViewcontroller($rootScope, $scope, $state, $modal, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, $sce) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstCADCrimeReportCompanyViewcontroller';
        var crimereportinstitution_gid = localStorage.getItem('crimereportinstitution_gid');

        lockUI();
        activate();

        function activate() {
            var params = {
                crimereportinstitution_gid: crimereportinstitution_gid,
            }

            var url = 'api/CADCrimeCheckAPI/CrimeReportCompanyView';
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
