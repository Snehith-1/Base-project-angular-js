(function () {
    'use strict';

    angular
        .module('angle')
        .controller('deploymentController', deploymentController);

    deploymentController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'SweetAlert', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', '$cookies'];
    function deploymentController($rootScope, $scope, $modal, $state, SweetAlert, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, $cookies) {
        var vm = this;
        vm.title = 'deploymentController';

        activate();

        function activate() {
            var url = 'api/deployment/getClientList';
            SocketService.get(url).then(function (resp) {
                $scope.client_list = resp.data.clients;
            });
        };

        $scope.getProject = function (val) {
            var url = 'api/deployment/getProjectList';
            SocketService.get(url + '?client_gid=' + val).then(function (resp) {
                $scope.project_list = resp.data.projects;
               
            });
        };
        vm.validateInput = function (name, type) {
            var input = vm.formValidate[name];
            return (input.$dirty || vm.submitted) && input.$error[type];
        };
        vm.submitForm = function () {
            vm.submitted = true;
            if (vm.formValidate.$valid) {
                //console.log('Submitted!!');
                var indexProject = $scope.project_list.map(function (e) { return e.project_gid }).indexOf($scope.selectProject);
                var indexClient = $scope.client_list.map(function (e) { return e.client_gid }).indexOf($scope.selectClient);
                var project = $scope.project_list[indexProject].project_name;
                var client = $scope.client_list[indexClient].client_name;
                var params = {
                    user_gid: localStorage.getItem('user_gid'),
                    deployment_mode: $scope.selectMode,
                    deployment_client_gid: $scope.selectClient,
                    deployment_client: client,
                    deployment_project: project,
                    deployment_project_gid: $scope.selectProject,
                    deployment_description: $scope.description,
                    deployment_client_need: $scope.selectNeed,
                    deployment_page_flag: $scope.checkPage,
                    deployment_pages: $scope.pagename,
                    deployment_report_flag: $scope.checkRpt,
                    deployment_report: $scope.reportname
                };

                var url = 'api/deployment/addDeployment';
                SocketService.post(url, params).then(function (resp) {
                    if (resp.data.status == true) {
                        SweetAlert.swal('Success!', 'Your Deployment Record Added!', 'success');
                    }
                    else {
                        SweetAlert.swal('Error!', 'An Error Occured While Adding Deployment Record', 'warning');
                    }
                    setTimeout($state.go('app.deploymentsummary'), 2000);
                });
            } else {
                   console.log('Not valid!!');
                return false;
            }
        };
     //   $scope.alert = function () {
                       
     //   }

        $scope.back = function () {
            $state.go('app.deploymentsummary');
        };
    }
})();