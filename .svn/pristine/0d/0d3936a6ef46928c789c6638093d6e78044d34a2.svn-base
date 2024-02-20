(function () {
    'use strict';

    angular
        .module('angle')
        .controller('tier2ApprovalSummary', tier2ApprovalSummary);

    tier2ApprovalSummary.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'SweetAlert', '$route', 'ngTableParams'];

    function tier2ApprovalSummary($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'tier2ApprovalSummary';

        activate();

        function activate() {
            lockUI();
            var url = 'api/TierMeeting/GetTier2ApprovalSummary';
            SocketService.get(url).then(function (resp) {
                $scope.tier2preparation_list = resp.data.tier2preparation;
                $scope.count_pending = resp.data.count_pending;
                $scope.count_approved = resp.data.count_approved;
                $scope.count_rejected = resp.data.count_rejected;
                if ($scope.tier2preparation_list == null) {
                    $scope.total = 0;
                    $scope.totalDisplayed = 0;
                }
                else {
                    $scope.total = $scope.tier2preparation_list.length;
                    if ($scope.tier2preparation_list.length < 100) {
                        $scope.totalDisplayed = $scope.tier2preparation_list.length;
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
            if ($scope.totalDisplayed < $scope.tier2preparation_list.length) {
                $scope.totalDisplayed += Number;
                unlockUI();
            }
            else {
                unlockUI();
                Notify.alert(" Total Summary " + $scope.tier2preparation_list.length + " Records Only", "warning");
                return;
            }
        };


        $scope.viewtier2dtl = function (tier2preparation_gid) {
            localStorage.setItem('tier2preparation_gid', tier2preparation_gid)
            $state.go('app.tier2Approval');
        }
    }
})();
