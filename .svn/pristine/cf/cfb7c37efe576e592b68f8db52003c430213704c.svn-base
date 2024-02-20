(function () {
    'use strict';
    angular
        .module('angle')
        .controller('MstBREExecutionController', MstBREExecutionController);
    MstBREExecutionController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function MstBREExecutionController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstBREExecutionController';
        $scope.postruleenginerun_gid = $location.search().postruleenginerun_gid;
        var postruleenginerun_gid = $location.search().postruleenginerun_gid;
        activate();
        function activate() {
            var url = 'api/MstRuleEngine/GetTemplateDropDown';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.Template_DropDown = resp.data.Template_DropDown;
                unlockUI();
            });

            var url = 'api/MstRuleEngine/GetApplicationDropDown';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.Application_DropDown = resp.data.Application_DropDown;
                unlockUI();
            });

            var url = 'api/MstRuleEngine/PostExecuteSummary';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.postexecute_list = resp.data.postexecute_list;
                unlockUI();
            });
        }
            $scope.processdata = function   () {
                if (($scope.cboTemplateName == undefined) || ($scope.cboTemplateName == null) || ($scope.cboTemplateName == '')) {
                    Notify.alert('Select Template', 'warning');

                    if (($scope.cboApplicationNumber == undefined) || ($scope.cboApplicationNumber == null) || ($scope.cboApplicationNumber == '')) {
                        Notify.alert('Select Application', 'warning');
                    }
                }
                else {
                    var params = {
                        ruletemplatemaster_gid: $scope.cboTemplateName.ruletemplatemaster_gid,
                        application_gid: $scope.cboApplicationNumber.application_gid,
                    }
                    var url = 'api/TrnRuleEngine/RuleEngineExecute';
                    lockUI();
                    SocketService.getparams(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {                       
                            activate();
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            
                        }
                    });
                }
            }
                $scope.viewresult = function (val1) {
                        $location.url('app/MstBREExecutionView?postruleenginerun_gid=' + val1);
                }          
    }
})();

