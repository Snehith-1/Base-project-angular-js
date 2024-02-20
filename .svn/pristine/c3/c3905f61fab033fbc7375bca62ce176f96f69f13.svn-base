(function () {
    'use strict';
    angular
        .module('angle')
        .controller('MstRETemplateController', MstRETemplateController);
    MstRETemplateController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];
    function MstRETemplateController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstRETemplateController';
        $scope.ruletemplatemaster_gid = $location.search().ruletemplatemaster_gid;
        var ruletemplatemaster_gid = $scope.ruletemplatemaster_gid;
        activate();
        function activate() {
            var url = 'api/MstRuleEngine/GetTemplateSummary';
             lockUI();
             SocketService.get(url).then(function (resp) {
                 $scope.ruletemplate_list = resp.data.ruletemplate_list;
                 unlockUI();
             });
        }
        $scope.configtemplate = function (val1) {
            $location.url('app/MstREDoConfiguration?ruletemplatemaster_gid=' + val1);
        }
        $scope.addtemplate = function () {
            $state.go('app.MstREAddTemplate');
        }
    }
})();

