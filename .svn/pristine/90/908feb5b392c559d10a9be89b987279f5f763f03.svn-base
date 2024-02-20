(function () {
    'use strict';

    angular
        .module('angle')
        .controller('tier1Summarycontroller', tier1Summarycontroller);

    tier1Summarycontroller.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', '$route', 'ngTableParams'];

    function tier1Summarycontroller($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'tier1Summarycontroller';

        activate();

        function activate() {
            $scope.totalDisplayed = 100;
            var url = "api/TierMeeting/GetTier1formatlist";
            SocketService.get(url).then(function (resp) {
                $scope.count_pending = resp.data.count_pending;
                $scope.count_approved = resp.data.count_approved;
                $scope.count_rejected = resp.data.count_rejected;
                $scope.tier1format = resp.data.tier1format;
                if ($scope.tier1format == null) {
                    $scope.total = 0;
                    $scope.totalDisplayed = 0;
                }
                else {
                    $scope.total = $scope.tier1format.length;
                    if ($scope.tier1format.length < 100) {
                        $scope.totalDisplayed = $scope.tier1format.length;
                    }
                }
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
            $scope.totalDisplayed += Number;
            unlockUI();
        };
        $scope.viewtier1format = function (tier1format_gid, allocationdtl_gid) {
            localStorage.setItem('tier1format_gid', tier1format_gid);
            localStorage.setItem('allocationdtl_gid', allocationdtl_gid);
            $state.go('app.tier1Approval');
        }
    }
})();
