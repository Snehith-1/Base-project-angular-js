(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AgrMstContrateTemplateSummaryController', AgrMstContrateTemplateSummaryController);

    AgrMstContrateTemplateSummaryController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route'];

    function AgrMstContrateTemplateSummaryController($rootScope, $scope, $state, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route) {

        $scope.title = 'AgrMstContrateTemplateSummaryController';
        var vm = this;
        var vertical_gid = $location.search().lsvertical_gid;
        activate();

        function activate() {
            var params = {
                vertical_gid : vertical_gid,
                template_type: 'Contract'
            }
            var url = 'api/AgrTemplate/GetTemplateSummary';
            lockUI();
            SocketService.getparams(url,params).then(function (resp) {
                $scope.templatelist = resp.data.TemplateDtlsList;
                $scope.lblvertical_name = resp.data.vertical_name;
                unlockUI();
            });
        };

        // $scope.viewtemplatedtl = function (val) {
        //     $location.url('app/viewTemplateDetails?template_gid=' + val);
        // }

        $scope.edittemplatedtl = function (val) { 
            $location.url('app/AgrSanctionEditTemplate?lsvertical_gid='+ vertical_gid +'&template_gid=' + val);
        }

        $scope.popupTemplate = function () {
            $location.url('app/AgrSanctionAddTemplate?lsvertical_gid='+ vertical_gid); 
        }
        $scope.back = function (vertical_gid) {
            $location.url('app/vertical');
        }
    }
})();