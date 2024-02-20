(function () {
    'use strict';

    angular
        .module('angle')
        .controller('idasMstTemplateSummaryController', idasMstTemplateSummaryController);

    idasMstTemplateSummaryController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route'];

    function idasMstTemplateSummaryController($rootScope, $scope, $state, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route) {

        $scope.title = 'idasMstTemplateSummaryController';
        var vm = this;

        activate();

        function activate() {
            var url = 'api/idasMstTemplate/GetTemplateSummary';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.templatelist = resp.data.TemplateDtlsList;
                unlockUI();
            });
        };

        $scope.viewtemplatedtl = function (val) {
            $location.url('app/viewTemplateDetails?template_gid=' + val);
        }

        $scope.edittemplatedtl = function (val) {
         
            $location.url('app/idasMstEditTemplate?template_gid=' + val);
        }

        $scope.popupTemplate = function () {

            $state.go('app.idasMstAddTemplate');
        }
    }
})();