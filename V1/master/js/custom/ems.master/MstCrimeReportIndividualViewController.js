(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstCrimeReportIndividualViewcontroller', MstCrimeReportIndividualViewcontroller);

        MstCrimeReportIndividualViewcontroller.$inject = ['$rootScope', '$scope', '$state', '$modal', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', '$sce'];

    function MstCrimeReportIndividualViewcontroller($rootScope, $scope, $state, $modal, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, $sce) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstCrimeReportIndividualViewcontroller';
        var crimereportcontact_gid = localStorage.getItem('crimereportcontact_gid');

        lockUI();
        activate();

        function activate() {
            var params = {
                crimereportcontact_gid: crimereportcontact_gid,
            }

            var url = 'api/CrimeCheckAPI/CrimeReportIndividualView';
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
