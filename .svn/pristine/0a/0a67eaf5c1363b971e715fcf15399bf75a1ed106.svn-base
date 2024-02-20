(function () {
    'use strict';

    angular
        .module('vcx')
        .controller('viewIssueDetailscontroller', viewIssueDetailscontroller);

    viewIssueDetailscontroller.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', '$cookies'];

    function viewIssueDetailscontroller($rootScope, $scope, $state, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, $cookies) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'viewIssueDetailscontroller';

        activate();

        function activate() {
            var params = {
                issue_gid: localStorage.getItem('issue_gid')
            };
            var url = apiManage.apiList["viewissuedata"].api;
            SocketService.getparams(url, params).then(function (resp) {
                $scope.viewdtl = resp.data;
                $scope.issue_remarks = resp.data.issue_remarks;
                document.getElementById('test').innerHTML += $scope.issue_remarks;
                $scope.docdtl = resp.data.path;
                $scope.issuestatuslog = resp.data.issuestatuslog;
            });
        }

        // Download Document

        $scope.downloads = function (val1, val2) {

            var phyPath = val1;
            var relPath = phyPath.split("StoryboardAPI");
            var relpath1 = relPath[1].replace("\\", "/");
            var hosts = window.location.host;
            var prefix = location.protocol + "//";
            var str = prefix.concat(hosts, relpath1);
            var link = document.createElement("a");
            var name = val2.split(".")
            link.download = name[0];
            var uri = str;
            link.href = uri;
            link.click();
        }

        // Update Vendor Status

        $scope.updateStatus = function (issuetrackergid) {
            var params = {
                issuetrackergid: issuetrackergid,
                IssueStatus: $scope.issueStatus,
                StatusRemarks: $scope.StatusRemarks,
                TargetIssuDate: "",
                DoneBy: $scope.DoneBy
            }
            var url = apiManage.apiList['updateStatus'].api;
            SocketService.post(url, params).then(function (resp) {
                if (resp.data.message == 'success') {
                    Notify.alert('Status Updated Successfully..!', {
                        status: 'success',
                        pos: 'top-right',
                        timeout: 3000
                    });
                }
                else {
                    Notify.alert('Error While Updating Status', {
                        status: 'danger',
                        pos: 'top-right',
                        timeout: 3000
                    });
                }
                $state.go('app.dashboard');
                $scope.issueStatus = '';
                $scope.StatusRemarks = '';
                $scope.DoneBy = '';
            });

        }

        $scope.cancel = function () {
            $state.go('app.dashboard');
        }
    }
})();
