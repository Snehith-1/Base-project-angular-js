(function () {
    'use strict';

    angular
        .module('vcx')
        .controller('ReleaseMgmt', ReleaseMgmt);

    ReleaseMgmt.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', '$cookies'];

    function ReleaseMgmt($rootScope, $scope, $state, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, $cookies) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'ReleaseMgmt';

        activate();

        function activate() {
            var url = apiManage.apiList['releaseMgmt'].api;
            lockUI();
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.tabledata = resp.data.ReleaseDetails;
            });
        }

        $scope.view = function (release_gid) {
            localStorage.setItem('release_gid', release_gid);
            $state.go('app.viewReleasePlan');
           
        }

        $scope.status = function (val) {
            $scope.release_gid = val;
            var doc = document.getElementById('statusCompleted');
            doc.style.display = 'block';
        }

        $scope.readytorelease = function (val) {
            $state.go('app.readytorelease');
        }

        $scope.MakeCompleted = function () {
            var params = {
                release_gid: $scope.release_gid,
                IssueStatus: "Completed",
                StatusRemarks: $scope.ReleaseRemarks,
                TargetIssuDate: "",
                DoneBy: $scope.DoneBy
            }
            var url = apiManage.apiList['statusCompleted'].api;
            SocketService.post(url, params).then(function (resp) {
                if (resp.data.message == 'success') {
                    Notify.alert('Status Updated Successfully..!', {
                        status: 'success',
                        pos: 'top-right',
                        timeout: 3000
                    });
                }
                else {
                    location.reload();
                    Notify.alert('Error While Updating Status', {
                        status: 'danger',
                        pos: 'top-right',
                        timeout: 3000
                    });
                }
                activate();
                $scope.close('statusCompleted');
            });
           
        }

        $scope.close = function (val) {
            var doc = document.getElementById(val);
            doc.style.display = 'none';
        }



    }
})();
