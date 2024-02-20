(function () {
    'use strict';

    angular
        .module('vcx')
        .controller('viewReleasePlancontroller', viewReleasePlancontroller);

    viewReleasePlancontroller.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', '$cookies'];

    function viewReleasePlancontroller($rootScope, $scope, $state, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, $cookies) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'viewReleasePlancontroller';

        activate();

        function activate() {
            var params = {
                release_gid: localStorage.getItem('release_gid')
            }
            var url = apiManage.apiList["issue2Release"].api;
            SocketService.getparams(url, params).then(function (resp) {
                $scope.releasedetails = resp.data;
                $scope.issue2Release = resp.data.tabledata;
                $scope.uatdocument_list = resp.data.uatdocument_list;
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

        $scope.back = function () {
            $state.go('app.releaseManagement');
        }
    }
})();
