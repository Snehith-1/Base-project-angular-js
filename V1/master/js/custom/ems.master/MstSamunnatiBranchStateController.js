(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstSamunnatiBranchStateController', MstSamunnatiBranchStateController);

        MstSamunnatiBranchStateController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function MstSamunnatiBranchStateController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstSamunnatiBranchStateController';
        $scope.lsoneapipage = $location.search().lsoneapipage;
        var lsoneapipage = $scope.lsoneapipage;
        activate();

        function activate() { 
            $scope.total = 0;
            $scope.totalDisplayed = 100;
            var url = 'api/MstApplication360/GetSamunnatiBranchState';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.samunnatibranchstate_data = resp.data.application_list;
                unlockUI();
            });
        }

        $scope.addSamunnatiBranchState = function () {
            var modalInstance = $modal.open({
                templateUrl: '/addSamunnatiBranchState.html',
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
                        samunnatibranchstate_name: $scope.txtSamunnatiBranchState_name,
                        lms_code: $scope.txtlms_code,
                        bureau_code: $scope.txtbureau_code
                    
                    }
                    var url = 'api/MstApplication360/CreateSamunnatiBranchState';
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
                            activate();
                        }
                    });

                    $modalInstance.close('closed');
                    activate();
                }
                
            }
        }

        $scope.editsamunnatibranchstate = function (samunnatibranchstate_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/editSamunnatiBranchState.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                var params = {
                    samunnatibranchstate_gid: samunnatibranchstate_gid
                }
                var url = 'api/MstApplication360/EditSamunnatiBranchState';
                SocketService.getparams(url, params).then(function (resp) {

                    $scope.txteditSamunnatiBranchState_name = resp.data.samunnatibranchstate_name;
                    $scope.txteditlms_code = resp.data.lms_code;
                    $scope.txteditbureau_code = resp.data.bureau_code;
                    $scope.samunnatibranchstate_gid = resp.data.samunnatibranchstate_gid;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.update = function () {

                    var url = 'api/MstApplication360/UpdateSamunnatiBranchState';
                    var params = {
                        samunnatibranchstate_name: $scope.txteditSamunnatiBranchState_name,
                        lms_code: $scope.txteditlms_code,
                        bureau_code: $scope.txteditbureau_code,
                        samunnatibranchstate_gid: $scope.samunnatibranchstate_gid
                    }
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

        $scope.Status_update = function (samunnatibranchstate_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/statusSamunnatiBranchState.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    samunnatibranchstate_gid: samunnatibranchstate_gid
                }
                var url = 'api/MstApplication360/EditSamunnatiBranchState';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.samunnatibranchstate_gid = resp.data.samunnatibranchstate_gid
                    $scope.txtsamunnatibranchstate_name = resp.data.samunnatibranchstate_name;
                    $scope.rbo_status = resp.data.Status;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.update_status = function () {

                    var params = {
                        samunnatibranchstate_name: $scope.txtsamunnatibranchstate_name,
                        samunnatibranchstate_gid: samunnatibranchstate_gid,
                        remarks: $scope.txtremarks,
                        rbo_status: $scope.rbo_status
                    }

                    var url = 'api/MstApplication360/InactiveSamunnatiBranchState';
                    lockUI();
                    SocketService.post(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {

                            Notify.alert(resp.data.message, {
                                status: 'success',
                                pos: 'top-center',
                                timeout: 3000
                            });

                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'info',
                                pos: 'top-center',
                                timeout: 3000
                            });
                        }
                        activate();
                    });

                    $modalInstance.close('closed');

                }
                var param = {
                    samunnatibranchstate_gid: samunnatibranchstate_gid
                }

                var url = 'api/MstApplication360/SamunnatiBranchStateInactiveLogview';
                lockUI();
                SocketService.getparams(url, param).then(function (resp) {
                    $scope.samunnatibranchstateinactivelog_data = resp.data.application_list;
                    unlockUI();
                });
            }
        }

        $scope.delete = function (samunnatibranchstate_gid) {
            var params = {
                samunnatibranchstate_gid: samunnatibranchstate_gid
            }
            SweetAlert.swal({
                title: 'Are you sure?',
                text: 'Do You Want To Delete the Record ?',
                showCancelButton: true,
                confirmButtonColor: '#DD6B55',
                confirmButtonText: 'Yes, delete it!',
                closeOnConfirm: false
            }, function (isConfirm) {
                if (isConfirm) {
                    lockUI();
                    var url = 'api/MstApplication360/DeleteSamunnatiBranchState';
                    SocketService.getparams(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {
                            SweetAlert.swal('Deleted Successfully!');
                            activate();
                        }
                        else {
                            alert(resp.data.message, {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            activate();
                            unlockUI;
                        }
                    });
                    }
            });
        }
    }
})();

