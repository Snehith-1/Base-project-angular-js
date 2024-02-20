(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstSamunnatiBranchNameController', MstSamunnatiBranchNameController);

        MstSamunnatiBranchNameController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function MstSamunnatiBranchNameController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstSamunnatiBranchNameController';
        $scope.lsoneapipage = $location.search().lsoneapipage;
        var lsoneapipage = $scope.lsoneapipage;
        activate();

        function activate() { 
            $scope.total = 0;
            $scope.totalDisplayed = 100;
            var url = 'api/MstApplication360/GetSamunnatiBranchName';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.samunnatibranchname_data = resp.data.application_list;
                unlockUI();
            });
        }

        $scope.addSamunnatiBranchName = function () {
            var modalInstance = $modal.open({
                templateUrl: '/addSamunnatiBranchName.html',
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
                        samunnatibranch_name: $scope.txtSamunnatiBranch_name,
                        lms_code: $scope.txtlms_code,
                        bureau_code: $scope.txtbureau_code
                    
                    }
                    var url = 'api/MstApplication360/CreateSamunnatiBranchName';
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

        $scope.editsamunnatibranchname = function (samunnatibranch_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/editSamunnatiBranchName.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                var params = {
                    samunnatibranch_gid: samunnatibranch_gid
                }
                var url = 'api/MstApplication360/EditSamunnatiBranchName';
                SocketService.getparams(url, params).then(function (resp) {

                    $scope.txteditSamunnatiBranch_name = resp.data.samunnatibranch_name;
                    $scope.txteditlms_code = resp.data.lms_code;
                    $scope.txteditbureau_code = resp.data.bureau_code;
                    $scope.samunnatibranch_gid = resp.data.samunnatibranch_gid;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.update = function () {

                    var url = 'api/MstApplication360/UpdateSamunnatiBranchName';
                    var params = {
                        samunnatibranch_name: $scope.txteditSamunnatiBranch_name,
                        lms_code: $scope.txteditlms_code,
                        bureau_code: $scope.txteditbureau_code,
                        samunnatibranch_gid: $scope.samunnatibranch_gid
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

        $scope.Status_update = function (samunnatibranch_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/statusSamunnatiBranchName.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    samunnatibranch_gid: samunnatibranch_gid
                }
                var url = 'api/MstApplication360/EditSamunnatiBranchName';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.samunnatibranch_gid = resp.data.samunnatibranch_gid
                    $scope.txtsamunnatibranch_name = resp.data.samunnatibranch_name;
                    $scope.rbo_status = resp.data.Status;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.update_status = function () {

                    var params = {
                        samunnatibranch_gid: samunnatibranch_gid,
                        samunnatibranch_name: $scope.txtsamunnatibranch_name,
                        remarks: $scope.txtremarks,
                        rbo_status: $scope.rbo_status
                    }

                    var url = 'api/MstApplication360/InactiveSamunnatiBranchName';
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
                    samunnatibranch_gid: samunnatibranch_gid
                }

                var url = 'api/MstApplication360/SamunnatiBranchNameInactiveLogview';
                lockUI();
                SocketService.getparams(url, param).then(function (resp) {
                    $scope.samunnatibranchinactivelog_data = resp.data.application_list;
                    unlockUI();
                });
            }
        }

        $scope.delete = function (samunnatibranch_gid) {
            var params = {
                samunnatibranch_gid: samunnatibranch_gid
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
                    var url = 'api/MstApplication360/DeleteSamunnatiBranchName';
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

