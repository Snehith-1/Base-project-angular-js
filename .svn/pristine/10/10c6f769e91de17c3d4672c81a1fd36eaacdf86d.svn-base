(function () {
    'use strict';

    angular
        .module('angle')
        .controller('tier3Preparation', tier3Preparation);

    tier3Preparation.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', '$route', 'ngTableParams'];

    function tier3Preparation($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'tier3Preparation';

        activate();

        function activate() {
            lockUI();
            $scope.totalDisplayed = 100;
            var url = 'api/TierMeeting/GetTier3Summary';
            SocketService.get(url).then(function (resp) {
                $scope.tier3preparation_list = resp.data.tier3preparation;
                $scope.count_pending = resp.data.count_pending;
                $scope.count_completed = resp.data.count_completed;
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
            if ($scope.totalDisplayed < $scope.tier3preparation_list.length) {
                $scope.totalDisplayed += Number;
                unlockUI();
            }
            else {
                unlockUI();
                Notify.alert(" Total Summary " + $scope.tier3preparation_list.length + " Records Only", "warning");
                return;
            }
        };

        $scope.createtier3preparation = function () {
            $state.go('app.tier3Create');
        }

        $scope.viewtier3completed = function (tier3preparation_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/completetier3.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.close = function () {
                    $modalInstance.close('closed');
                }
                $scope.completetier3 = function () {
                    lockUI();
                   
                    var tier3dtl = {
                        tier3preparation_gid: tier3preparation_gid,
                        completed_remarks: $scope.txttier3_completedremarks,
                    }
                    var url = "api/TierMeeting/PostTier3Complete";
                    SocketService.post(url, tier3dtl).then(function (resp) {
                        if (resp.data.status == true) {
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            unlockUI();
                            $modalInstance.close('closed');
                            activate();
                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'danger',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            unlockUI();
                        }
                    });
                }
            }
        }

        $scope.viewtier3details = function (tier3preparation_gid) {
            localStorage.setItem('tier3preparation_gid', tier3preparation_gid);
            $state.go('app.tier3PreparationView');
        }
    }
})();
