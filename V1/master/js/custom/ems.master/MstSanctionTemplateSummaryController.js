(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstSanctionTemplateSummaryController', MstSanctionTemplateSummaryController);

        MstSanctionTemplateSummaryController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route'];

    function MstSanctionTemplateSummaryController($rootScope, $scope, $state, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route) {

        $scope.title = 'MstSanctionTemplateSummaryController';
        var vm = this;
        var vertical_gid = $location.search().lsvertical_gid;
        activate();

        function activate() {
            var params = {
                vertical_gid : vertical_gid,
                template_type : 'Sanction'
            }
            var url = 'api/MstTemplate/GetTemplateSummary';
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
            $location.url('app/MstSanctionEditTemplate?lsvertical_gid='+ vertical_gid +'&template_gid=' + val);
        }

        $scope.popupTemplate = function () {
            $location.url('app/MstSanctionAddTemplate?lsvertical_gid='+ vertical_gid); 
        }
        $scope.back = function (vertical_gid) {
            $location.url('app/vertical');
        }
    }
})();