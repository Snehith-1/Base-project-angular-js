(function () {
    'use strict';

    angular
        .module('angle')
        .controller('tierReportcontroller', tierReportcontroller);

    tierReportcontroller.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', '$route', 'ngTableParams'];

    function tierReportcontroller($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'tierReportcontroller';

        activate();

        function activate() {
            lockUI();
            $scope.totalDisplayed = 100;
            var url = 'api/TierMeeting/GetTier3CompletedSummary';
            SocketService.get(url).then(function (resp) {
                $scope.tier3preparation_list = resp.data.tier3preparation;
                if ($scope.tier3preparation_list == null) {
                    $scope.total = 0;
                    $scope.totalDisplayed = 0;
                }
                else {
                    $scope.total = $scope.tier3preparation_list.length;
                    if ($scope.tier3preparation_list.length < 100) {
                        $scope.totalDisplayed = $scope.tier3preparation_list.length;
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
            if ($scope.totalDisplayed < $scope.tier3preparation_list.length)
            {
                $scope.totalDisplayed += Number;
                unlockUI();
            }
            else {
                unlockUI();
                Notify.alert(" Total Summary " + $scope.tier3preparation_list.length + " Records Only", "warning");
                return;
            }
        };

        $scope.viewtier3details = function (tier3preparation_gid) {
            localStorage.setItem('tier3preparation_gid', tier3preparation_gid);
            $state.go('app.tierReportView');
        }
    }
})();
