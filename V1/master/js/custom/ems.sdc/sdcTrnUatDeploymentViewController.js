(function () {
    'use strict';

    angular
        .module('angle')
        .controller('sdcTrnUatDeploymentViewController', sdcTrnUatDeploymentViewController);

    sdcTrnUatDeploymentViewController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'SweetAlert', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', '$cookies', '$filter', 'ngTableParams', '$resource', '$timeout', 'ngTableDataService'];

    function sdcTrnUatDeploymentViewController($rootScope, $scope, $modal, $state, SweetAlert, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, $cookies, $filter, ngTableParams, $resource, $timeout, ngTableDataService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'sdcTrnUatDeploymentViewController';

        activate();


        function activate() {

            lockUI();
            var url = window.location.href;
            var relPath = url.split("lstab=");
            $scope.relpath1 = relPath[1];

            var url = "api/SdcTrnUatDeployment/UatDeploymentView";
            var param = {
                uat_gid: localStorage.getItem('uat_gid')
            };
            SocketService.getparams(url, param).then(function (resp) {
                unlockUI();

                $scope.module_prefix = resp.data.module_prefix;
                $scope.test_description = resp.data.test_description;
                $scope.new_pages = resp.data.new_pages;
                $scope.new_reports = resp.data.new_reports;
                $scope.test_Objective = resp.data.test_Objective;
                $scope.test_status = resp.data.test_status;
                $scope.newdll_name = resp.data.newdll_name;
                $scope.dependency_name = resp.data.dependency_name;
                $scope.appjs_text = resp.data.appjs_text;
                $scope.filedescription = resp.data.filedescription;
                $scope.script = resp.data.script;
                $scope.upload_list = resp.data.upload_list;
                $scope.uploadjs_list = resp.data.uploadjs_list;
                $scope.versionupload_list = resp.data.versionupload_list;
                $scope.filedesc_list = resp.data.filedesc_list;
            });

        }
        $scope.downloads = function (val1, val2) {
            var phyPath = val1;
            var relPath = phyPath.split("StoryboardAPI");
            var relpath1 = relPath[1].replace("\\", "/");
            var hosts = window.location.host;
            var prefix = location.protocol + "//";
            var str = prefix.concat(hosts, relpath1);
            var link = document.createElement("a");
            var name = val2.split('.');
            link.download = name[0];
            var uri = str;
            link.href = uri;
            link.click();
        }
        $scope.jsdownloads = function (val1, val2) {
            var phyPath = val1;
            var relPath = phyPath.split("StoryboardAPI");
            console.log(relPath);
            var relpath1 = relPath[1].replace("\\", "/");
            var hosts = window.location.host;
            var prefix = location.protocol + "//";
            var str = prefix.concat(hosts, relpath1);
            var link = document.createElement("a");
            var name = val2.split('.');
            link.download = name[0];
            var uri = str;
            link.href = uri;
            link.click();
        }

        $scope.versiondocumentdownloads = function (val1, val2) {
            var phyPath = val1;
            var relPath = phyPath.split("StoryboardAPI");
            console.log(relPath);
            var relpath1 = relPath[1].replace("\\", "/");
            var hosts = window.location.host;
            var prefix = location.protocol + "//";
            var str = prefix.concat(hosts, relpath1);
            var link = document.createElement("a");
            var name = val2.split('.');
            link.download = name[0];
            var uri = str;
            link.href = uri;
            link.click();
        }
        $scope.uatViewBack = function () {
            $state.go('app.sdcTrnUatDeploymentSummary');
        }

        $scope.ViewBack = function () {
            $state.go('app.sdcTrnUatSummary');
        }
    }

})();
