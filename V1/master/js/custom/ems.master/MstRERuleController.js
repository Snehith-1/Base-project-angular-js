(function () {
    'use strict';
    angular
        .module('angle')
        .controller('MstRERuleController', MstRERuleController);
    MstRERuleController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];
    function MstRERuleController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstRERuleController';
        $scope.lsoneapipage = $location.search().lsoneapipage;
        var lsoneapipage = $scope.lsoneapipage;
        
        activate();
        function activate() {
            var url = 'api/MstRuleEngine/GetRuleSummary';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.rule_list = resp.data.rule_list;
                unlockUI();
            });
        }
        $scope.addrule = function () {
            var modalInstance = $modal.open({
                templateUrl: '/addrule.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.submit = function () {
                    var params = {
                        rule_id: $scope.rule_id,
                        rule_title: $scope.rule_title,
                        rule_function: $scope.rule_function,
                        rule_category: $scope.rule_category,
                        rule_datatype:  $scope.rule_datatype
                    }
                    var url = 'api/MstRuleEngine/AddRule';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {

                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            $modalInstance.close('closed');
                            activate();
                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                        }
                    });
                    $modalInstance.close('closed');
                }
            }
        }
        $scope.config = function (ruleenginemaster_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/myModaladdconfiguration.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });        
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    ruleenginemaster_gid: ruleenginemaster_gid
                }
                var url = 'api/MstRuleEngine/configRule';
                lockUI();   
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.rule_datatype = resp.data.rule_datatype;
                    $scope.ruleenginemaster_gid = resp.data.ruleenginemaster_gid;                 
                });
                    $scope.ok = function () {
                        $modalInstance.close('closed');
                    };
                    $scope.configsubmit = function () {                  
                        var url = 'api/MstRuleEngine/PostconfigureRule';
                        lockUI();
                        var params = {
                            param_name: $scope.param_name,
                            param_value: $scope.param_value,
                            param_remarks: $scope.param_remarks,
                            ruleenginemaster_gid: $scope.ruleenginemaster_gid
                        }
                        SocketService.post(url, params).then(function (resp) {
                            unlockUI();
                            if (resp.data.status == true) {
                                $modalInstance.close('closed');
                                Notify.alert(resp.data.message, {
                                    status: 'success',
                                    pos: 'top-center',
                                    timeout: 3000
                                });
                                activate();
                            }
                            else {
                                $modalInstance.close('closed');
                                Notify.alert(resp.data.message, {
                                    status: 'warning',
                                    pos: 'top-center',
                                    timeout: 3000
                                });
                            }
                        });
                        $modalInstance.close('closed');
                    }
                }
        }
        $scope.EditConfig = function (ruleenginemaster_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/myModaledit.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    ruleenginemaster_gid: ruleenginemaster_gid
                }
                var url = 'api/MstRuleEngine/EditconfigureRule';
                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    unlockUI();
                    $scope.param_name = resp.data.param_name;
                    $scope.param_value = resp.data.param_value;
                    $scope.param_remarks = resp.data.param_remarks;
                    $scope.ruleenginemaster_gid = resp.data.ruleenginemaster_gid;
                });
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.UpdateConfiguration = function () {
                    var params = {                    
                        param_value: $scope.param_value,
                        param_remarks: $scope.param_remarks,
                        ruleenginemaster_gid: ruleenginemaster_gid
                    }
                    var url = 'api/MstRuleEngine/UpdateconfigureRule';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {
                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            activate();
                            $modalInstance.close('closed');
                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            activate();
                            $modalInstance.close('closed');
                        }
                    })
                }
            }
        }      
    }
})();

