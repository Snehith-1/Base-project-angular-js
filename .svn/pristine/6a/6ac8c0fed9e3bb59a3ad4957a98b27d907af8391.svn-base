(function () {
    'use strict';

    angular
        .module('angle')
        .controller('tier2Preparation', tier2Preparation);

    tier2Preparation.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', '$route', 'ngTableParams'];

    function tier2Preparation($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'tier2Preparation';

        activate();

        function activate() {
            $scope.totalDisplayed = 100;
            var url = 'api/TierMeeting/GetTier2Summary';
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

        $scope.createtier2preparation = function () {
            $state.go('app.tier2Create');
        }


       
        $scope.viewtier2details = function (tier2preparation_gid) {
            localStorage.setItem('tier2preparation_gid', tier2preparation_gid);
            $state.go('app.tier2PreparationView');
        }
    }
})();
