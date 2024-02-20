(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstTemplateSummaryController', MstTemplateSummaryController);

        MstTemplateSummaryController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route'];

    function MstTemplateSummaryController($rootScope, $scope, $state, AuthenticationService, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route) {

        $scope.title = 'MstTemplateSummaryController';
        var vm = this;
        var vertical_gid = $location.search().lsvertical_gid;
        $scope.lsoneapipage = $location.search().lsoneapipage;
        var lsoneapipage = $scope.lsoneapipage;
        activate();

        function activate() {
            var params = {
                vertical_gid : vertical_gid,
                template_type : 'CAM'
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
            $location.url('app/MstEditTemplate?lsvertical_gid='+ vertical_gid +'&template_gid=' + val);
        }

        $scope.popupTemplate = function () {
            $location.url('app/MstAddTemplate?lsvertical_gid='+ vertical_gid); 
        }
        $scope.back = function (vertical_gid) {
            $location.url('app/vertical');
        }
    }
})();