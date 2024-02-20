(function () {
    'use strict';

    angular
        .module('angle')
        .controller('acknowledgeMyAssetcontroller', acknowledgeMyAssetcontroller);

    acknowledgeMyAssetcontroller.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route'];

    function acknowledgeMyAssetcontroller($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'acknowledgeMyAssetcontroller';

        activate();
        $scope.input = {
            reason_reject: ''
        };
        function activate() {
            var url = 'api/acknowledgeMyAsset/acknowledgement';
            lockUI();
            SocketService.get(url).then(function (resp) {
                unlockUI();
                $scope.acksummary = resp.data.acksummary;
            });
        }

        $scope.ack_click = function (val1) {
            var params = { asset2custodian_gid: val1 }
            var url = 'api/acknowledgeMyAsset/submitacknowledgement';
            lockUI();
            SocketService.post(url, params).then(function (resp) {
                unlockUI();
                if (resp.data.status) {
                    Notify.alert(resp.data.message, {
                        status: 'success',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    activate();
                }
            })


        };

        $scope.reject_popup = function (val) {
            $scope.asset2custodian_gid = localStorage.setItem('val', val);
            var modalInstance = $modal.open({
                templateUrl: '/rejectasset.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var url = 'api/employee/employee';
                SocketService.get(url).then(function (resp) {
                    $scope.employee_list = resp.data.employee_list;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                //Submit event

                $scope.ackreject_click = function () {
                    $scope.asset2custodian_gid=localStorage.getItem('val');
                    var params = {

                        asset2custodian_gid: $scope.asset2custodian_gid,
                        reason_reject: $scope.input.reason_reject
                    }
                    console.log(params);
                    var url = 'api/acknowledgeMyAsset/acknowledgementreject';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
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
                            Notify.alert(resp.data.message, {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            activate();
                        }

                    });
                }
            }
        }

    }
})();
