(function () {
    'use strict';

    angular
        .module('angle')
        .controller('viewTemplateDetailsController', viewTemplateDetailsController);

    viewTemplateDetailsController.$inject = ['$rootScope', '$scope', '$modal', '$state', 'SweetAlert', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', '$cookies', '$filter', 'ngTableParams', '$resource', '$timeout', 'ngTableDataService'];

    function viewTemplateDetailsController($rootScope, $scope, $modal, $state, SweetAlert, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, $cookies, $filter, ngTableParams, $resource, $timeout, ngTableDataService) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'viewTemplateDetailsController';
        $scope.template_gid = $location.search().template_gid;
        activate();


        function activate() {

            lockUI();
            var url = "api/idasMstTemplate/GetTemplateDtl"
            var param = {
                template_gid: $scope.template_gid
            };

            SocketService.getparams(url, param).then(function (resp) {
                unlockUI();

                $scope.template_name = resp.data.template_name;
                $scope.templatetype_name = resp.data.templatetype_name;
                $scope.template_content = resp.data.template_content;
                
            });
        }

        $scope.back = function () {
            $state.go('app.idasMstTemplateSummary');

        }

    }
})();
