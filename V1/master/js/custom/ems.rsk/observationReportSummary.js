(function () {
    'use strict';

    angular
        .module('angle')
        .controller('observationReportSummary', observationReportSummary);

    observationReportSummary.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', '$route', 'ngTableParams'];

    function observationReportSummary($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'observationReportSummary';

        activate();

        function activate() {
            lockUI();
            $scope.totalDisplayed = 100;
            var url = "api/ObservationReport/GetObservationReportSummary";
            SocketService.get(url).then(function (resp) {
                $scope.observationreport = resp.data.observationreport;
                $scope.count_pending = resp.data.count_pending;
                $scope.count_approved = resp.data.count_approved;
                if ($scope.observationreport == null) {
                    $scope.total = 0;
                    $scope.totalDisplayed = 0;
                }
                else {
                    $scope.total = $scope.observationreport.length;
                    if ($scope.observationreport.length < 100) {
                        $scope.totalDisplayed = $scope.observationreport.length;
                    }
                }
                unlockUI();
            });

        }

        document.getElementById('pagecount').onkeyup = function () {

            if ($scope.pagecount == null) {
                var el = document.getElementById('loadmore');
                el.style.backgroundColor = '#DCDCDC';
            }
            else {
                var el = document.getElementById('loadmore');
                el.style.backgroundColor = '#ffa';
            }
        };

        $scope.loadMore = function (pagecount) {
            if (pagecount == undefined) {
                Notify.alert("Enter the Total Summary Count", "warning");
                return;
            }
            lockUI();
            var Number = parseInt(pagecount);
            if ($scope.observationreport != null) {
                if ($scope.totalDisplayed < $scope.observationreport.length) {
                    $scope.totalDisplayed += Number;
                    unlockUI();
                }
                else {
                    unlockUI();
                    Notify.alert(" Total Summary " + $scope.observationreport.length + " Records Only", "warning");
                    return;
                }
            }
            unlockUI();
        };

        $scope.viewobservationReport = function (observation_reportgid) {
            localStorage.setItem('observation_reportgid', observation_reportgid);
            $state.go('app.observationReportApproval');
        }
    }
})();
