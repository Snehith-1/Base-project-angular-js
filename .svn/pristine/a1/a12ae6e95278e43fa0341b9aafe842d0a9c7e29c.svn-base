(function () {
    'use strict';

    angular
        .module('angle')
        .controller('Valuechaincontroller', Valuechaincontroller);

    Valuechaincontroller.$inject = ['$rootScope', '$scope', '$modal', '$state', 'AuthenticationService', 'SweetAlert', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', '$route', 'ngTableParams'];

    function Valuechaincontroller($rootScope, $scope, $modal, $state, AuthenticationService, SweetAlert, ScopeValueService, $http, SocketService, Notify, $location, apiManage, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'Valuechaincontroller';
        $scope.lsoneapipage = $location.search().lsoneapipage;
        var lsoneapipage = $scope.lsoneapipage;
        activate();

        function activate() {
            $scope.totalDisplayed = 100;
            var url = 'api/ValueChain/GetValueChain';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.value_chain = resp.data.valuechain_list;
                unlockUI();

            });
        }
        // Add Code Starts
        $scope.popupvaluechain = function () {
            var modalInstance = $modal.open({
                templateUrl: '/myModalContent.html',
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
                $scope.valuechainSubmit = function () {
                    var params = {
                        bureau_code: $scope.txtbureau_code,
                        lms_code: $scope.txtlms_code,
                        valuechain_name: $scope.valuechain_name
                    }
                    var url = 'api/ValueChain/CreateValueChain';

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
                            $modalInstance.close('closed');
                            Notify.alert(resp.data.message, {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            
                        }
                    });
                    $modalInstance.close('closed');
                    activate();
                }
                
            }
        }
        // Add Code Ends

        // Edit Code Starts
        $scope.edit = function (valuechain_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/myModaledit.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    valuechain_gid: valuechain_gid
                }
                var url = 'api/ValueChain/EditValueChain';
                SocketService.getparams(url, params).then(function (resp) {

                    $scope.txteditbureau_code = resp.data.bureau_code;
                    $scope.txteditlms_code = resp.data.lms_code;
                    $scope.valuechainnameedit = resp.data.valuechain_name;
                    $scope.valuechain_gid = resp.data.valuechain_gid;
                });
                
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.valuechainUpdate = function () {

                    var params = {
                        bureau_code: $scope.txteditbureau_code,
                        lms_code: $scope.txteditlms_code,
                        valuechain_name: $scope.valuechainnameedit,
                        valuechain_gid: $scope.valuechain_gid
                    }
                    var url = 'api/ValueChain/UpdateValueChain';

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
                            $modalInstance.close('closed');
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

        // Edit Code Ends

        // Delete Code Starts
        $scope.delete = function (valuechain_gid) {
            var params = {
                valuechain_gid: valuechain_gid
            }
            var url = 'api/ValueChain/DeleteValueChain';
            SocketService.getparams(url, params).then(function (resp) {
                if (resp.data.status == true) {

                    SweetAlert.swal({
                        title: 'Are you sure?',
                        text: 'Do You Want To Delete the Record ?',
                        showCancelButton: true,
                        confirmButtonColor: '#DD6B55',
                        confirmButtonText: 'Yes, delete it!',
                        closeOnConfirm: false
                    }, function (isConfirm) {
                        if (isConfirm) {
                            SweetAlert.swal('Deleted Successfully!');
                            unlockUI();
                            activate();
                        }

                    });
                }
                else {
                    Notify.alert(resp.data.message, {
                        status: 'warning',
                        pos: 'top-center',
                        timeout: 3000
                    });
                    unlockUI();
                    activate();
                }
            });
        };
        // Delete Code Ends

        $scope.Status_update = function (valuechain_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/statusCompanyDocument.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                var params = {
                    valuechain_gid: valuechain_gid
                }
                var url = 'api/ValueChain/EditValueChain';
                SocketService.getparams(url, params).then(function (resp) {

                    $scope.txtbureau_codeedit = resp.data.bureau_code;
                    $scope.txtlms_codeedit = resp.data.lms_code;
                    $scope.valuechainnameedit = resp.data.valuechain_name;
                    $scope.valuechain_gid = resp.data.valuechain_gid;
                    $scope.rbo_status = resp.data.status_log;
                });
                var url = 'api/ValueChain/GetActiveLog';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.valuechain_list = resp.data.valuechain_list;
                 });
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.update_status = function () {

                    var params = {
                        remarks: $scope.txtremarks,
                        status_log: $scope.rbo_status,
                        valuechain_gid: valuechain_gid
                    }
                    var url = 'api/ValueChain/ValueChainStatusUpdate';
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
                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'info',
                                pos: 'top-center',
                                timeout: 3000
                            });
                        }
                    });

                    $modalInstance.close('closed');

                }
            }
        }
    }
})();
