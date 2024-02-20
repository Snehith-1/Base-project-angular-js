(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstREAddTemplateController', MstREAddTemplateController);

    MstREAddTemplateController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function MstREAddTemplateController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstREAddTemplateController';
        $scope.ruletemplatemaster_gid = $location.search().ruletemplatemaster_gid;
        var ruletemplatemaster_gid = $scope.ruletemplatemaster_gid;
        activate();

        function activate() {
            var url = 'api/MstRuleEngine/GetRuleSummary';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.rule_list = resp.data.rule_list;
                unlockUI();
            });
        }
        $scope.checkall = function (selected) {
            angular.forEach($scope.rule_list, function (val) {
                val.checked = selected;
            });
        }
        $scope.addtemplate = function () {
            $state.go('app.MstREAddTemplate');
        }
        $scope.submit = function () {
            var ruleGidList = [];
            angular.forEach($scope.rule_list, function (val) {
                if (val.checked == true) {
                    var ruleenginemaster_gid = val.ruleenginemaster_gid;
                    ruleGidList.push(ruleenginemaster_gid);
                }
            });
            var params = {
                ruleenginemaster_gid: ruleGidList,
                template_code: $scope.template_code,
                template_name: $scope.template_name
            }
            var url = 'api/MstRuleEngine/AddTemplate';
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status == true) {
                    Notify.alert('Template Added Successfully!', 'success');
                    $state.go('app.MstRETemplate');
                }
                else {
                    Notify.alert('Select Atleast One Rule!')
                }
            });
        }
        $scope.back = function () {
            $state.go('app.MstRETemplate');
        }
    }
})();

