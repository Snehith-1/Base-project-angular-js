(function () {
    'use strict';
    angular
        .module('angle')
        .controller('MstREDoConfigurationController', MstREDoConfigurationController);
    MstREDoConfigurationController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];
    function MstREDoConfigurationController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstREDoConfigurationController';
        $scope.ruletemplatemaster_gid = $location.search().ruletemplatemaster_gid;
        var ruletemplatemaster_gid = $scope.ruletemplatemaster_gid;
        activate();
        function activate() {
            var param = {
                ruletemplatemaster_gid: ruletemplatemaster_gid
            };
            var url = 'api/MstRuleEngine/GetTemplateSummaryForConfiguration';
             lockUI();
            SocketService.getparams(url, param).then(function (resp) {
                $scope.templateforconfigure_list = resp.data.templateforconfigure_list;
                $scope.template_name = resp.data.template_name;
                $scope.template_code = resp.data.template_code;               
                 unlockUI();
             });
        }
        $scope.submit = function () {
            var templateforconfigure_GidList = [];
            var rule_parameterList = [];
            angular.forEach($scope.templateforconfigure_list, function (val) {               
                var ruletemplateparameter_gid = val.ruletemplateparameter_gid;
                var rule_parameter = val.rule_parameter;
                templateforconfigure_GidList.push(ruletemplateparameter_gid);
                rule_parameterList.push(rule_parameter);
            });
            var params = {
                ruletemplateparameter_gid: templateforconfigure_GidList,
                rule_parameter: rule_parameterList               
            }
            var url = 'api/TrnRuleEngine/SubmitConfiguredValuesofTemplate';
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {
                    Notify.alert('Template configured Successfully!', 'success');
                    $state.go('app.MstRETemplate');
                }
                else {
                    Notify.alert('Select Atleast One!')
                }
            });
        }
        $scope.addtemplate = function () {
            $state.go('app.MstREAddTemplate');
        }
        $scope.back = function () {
            $state.go('app.MstRETemplate');
        }
    }
})();

