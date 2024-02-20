(function () {
    'use strict';

    angular
        .module('angle')
        .controller('sdcTrnTestDeploymentViewController', sdcTrnTestDeploymentViewController);

    sdcTrnTestDeploymentViewController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'SweetAlert', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', '$cookies', '$filter', 'ngTableParams', '$resource', '$timeout', 'ngTableDataService'];

    function sdcTrnTestDeploymentViewController($rootScope, $scope, $modal, $state, SweetAlert, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, $cookies, $filter, ngTableParams, $resource, $timeout, ngTableDataService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'sdcTrnTestDeploymentViewController';

        activate();


        function activate() {

            lockUI();
            var url = window.location.href;
            var relPath = url.split("lstab=");
            $scope.relpath1 = relPath[1];
           
            var url = "api/SdcTrnTestDeployment/TestDeploymentView"
            var param = {
                test_gid: localStorage.getItem('test_gid')
            };
            SocketService.getparams(url, param).then(function (resp) {
                unlockUI();

                $scope.module_prefix = resp.data.module_prefix;
                $scope.test_description = resp.data.test_description;
                $scope.new_pages = resp.data.new_pages;
                $scope.new_reports = resp.data.new_reports;
                //$scope.new_webconfig = resp.data.new_webconfig;
                $scope.test_Objective = resp.data.test_Objective;
                $scope.test_status = resp.data.test_status;
                $scope.newdll_name = resp.data.newdll_name;
                $scope.dependency_name = resp.data.dependency_name;
                $scope.appjs_text = resp.data.appjs_text;
                $scope.filedescription = resp.data.filedescription;
                $scope.script = resp.data.script;
                //$scope.designation_name = resp.data.designation_name;
                //$scope.department_name = resp.data.department_name;
                //$scope.branch_name = resp.data.branch_name;
                //$scope.employee_photo = resp.data.employee_photo;
                //$scope.txtremarks = resp.data.remarks;
                //$scope.list = resp.data.document_list;
                $scope.upload_list = resp.data.upload_list;
                $scope.uploadjs_list = resp.data.uploadjs_list;
                $scope.versionupload_list = resp.data.versionupload_list;
                $scope.customer_list = resp.data.customer_list;
                console.log(params)
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
            var name = val2;
            link.download = name;
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

        $scope.testViewBack = function () {
            $state.go('app.sdcTrnTestDeploymentSummary');
        }

        $scope.ViewBack = function () {
            $state.go('app.sdcTrnDeploymentSummary');
        }
    }

})();
