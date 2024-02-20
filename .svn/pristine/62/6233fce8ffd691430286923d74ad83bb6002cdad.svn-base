(function () {
    'use strict';

    angular
        .module('angle')
        .controller('dependencyApprovalcontroller', dependencyApprovalcontroller);

    dependencyApprovalcontroller.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'], 'DownloaddocumentService';

    function dependencyApprovalcontroller($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, DownloaddocumentService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'dependencyApprovalcontroller';

        activate();

        function activate() {
            $scope.release_gid = localStorage.getItem('release_gid');
            var params = {
                release_gid: $scope.release_gid
            };
            var url = 'api/myApprovals/releasedetails';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.releaseapprovaldetails = resp.data;
                $scope.releaseissue_list = resp.data.releaseissue_list;
                $scope.uatlog_list = resp.data.uatlog_list;
                $scope.dependency_list = resp.data.dependency_list;
                $scope.uatdocument_list = resp.data.uatdocument_list;
            });
        }

        // View UAT- Details..//

        $scope.uatdetails = function (issuetracker_gid,id) {
            var params = {
                issuetracker_gid: issuetracker_gid
            };
            var url = 'api/myApprovals/uatdetails';
            SocketService.getparams(url, params).then(function (resp) {
                $scope.releaseissue_list[id][issuetracker_gid] = resp.data.uatlog_list;  
            });
        }

        // Dependency Approve & Reject .....//

        $scope.btn_dependencyapprove = function (dependentapproval_gid, release_gid) {
            var params = {
                dependentapproval_gid: dependentapproval_gid,
                release_gid: release_gid
            }
            lockUI();
            var url = 'api/myApprovals/dependencyapprove';
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status = true) {
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'warning',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
             
                $state.go('app.myApproval');
            });
        }

        $scope.btn_dependencyreject = function (dependentapproval_gid, release_gid) {
            var params = {
                dependentapproval_gid: dependentapproval_gid,
                release_gid: release_gid
            }
            lockUI();
            var url = 'api/myApprovals/dependencyreject';
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status = true) {
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'warning',
                        pos: 'top-center',
                        timeout: 3000
                    });
                }
              
                $state.go('app.myApproval');
            });
        }

        // Download Document

        $scope.downloads = function (val1, val2) {
            DownloaddocumentService.Downloaddocument(val1, val2);
        }

        //$scope.downloads = function (val1, val2) {

        //    var phyPath = val1;
        //    var relPath = phyPath.split("StoryboardAPI");
        //    var relpath1 = relPath[1].replace("\\", "/");
        //    var hosts = window.location.host;
        //    var prefix = location.protocol + "//";
        //    var str = prefix.concat(hosts, relpath1);
        //    var link = document.createElement("a");
        //    var name = val2.split(".")
        //    link.download = name[0];
        //    var uri = str;
        //    link.href = uri;
        //    link.click();
        //}

        $scope.back = function () {
            $state.go('app.myApproval');
        }
    }
})();
