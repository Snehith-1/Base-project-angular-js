(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AgrMstSectorClassificationController', AgrMstSectorClassificationController);

        AgrMstSectorClassificationController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function AgrMstSectorClassificationController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AgrMstSectorClassificationController';
        $scope.lsoneapipage = $location.search().lsoneapipage;
        var lsoneapipage = $scope.lsoneapipage;


        activate();

        function activate() { 
            
            var url = 'api/AgrMstSamAgroMaster/Getsectorclassification';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.sectorclassification_data = resp.data.applicationmst_list;
                unlockUI();
            });
        }

        $scope.addSectorClassification = function () {
            var modalInstance = $modal.open({
                templateUrl: '/addSectorClassification.html',
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
                        sectorclassification_name: $scope.txtsectorclassification_name,
                        lms_code: $scope.txtlms_code,
                        bureau_code: $scope.txtbureau_code

                    }
                    var url = 'api/AgrMstSamAgroMaster/Createsectorclassification';
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
                                status: 'info',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            $modalInstance.close('closed');
                            activate();
                        }
                    });

                    // $modalInstance.close('closed');

                }
                
            }
        }

        $scope.editSectorClassification = function (sectorclassification_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/editSectorClassification.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    sectorclassification_gid: sectorclassification_gid
                }
                var url = 'api/AgrMstSamAgroMaster/EditSectorClassification';
                SocketService.getparams(url, params).then(function (resp) {

                    $scope.txteditsectorclassification_name = resp.data.sectorclassification_name;
                    $scope.txteditlms_code = resp.data.lms_code;
                    $scope.txteditbureau_code = resp.data.bureau_code;
                    $scope.sectorclassification_gid = resp.data.sectorclassification_gid;
                });
                
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.update = function () {

                    var url = 'api/AgrMstSamAgroMaster/Updatesectorclassification';
                    var params = {
                        sectorclassification_name: $scope.txteditsectorclassification_name,
                        lms_code: $scope.txteditlms_code,
                        bureau_code: $scope.txteditbureau_code,
                        sectorclassification_gid: $scope.sectorclassification_gid
                    }
                    SocketService.post(url, params).then(function (resp) {
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
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                        }
                    });$modalInstance.close('closed');

                }
            }
        }

        $scope.Status_update = function (sectorclassification_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/statusSectorClassification.html',    
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                var params = {
                    sectorclassification_gid: sectorclassification_gid
                }
                var url = 'api/AgrMstSamAgroMaster/Editsectorclassification';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.sectorclassification_gid = resp.data.sectorclassification_gid
                    $scope.sectorclassification_name = resp.data.sectorclassification_name;
                    $scope.rbo_status = resp.data.Status;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.update_status = function () {

                    var params = {
                        sectorclassification_name :$scope.sectorclassification_name,
                        sectorclassification_gid: sectorclassification_gid,
                        remarks: $scope.txtremarks,
                        rbo_status: $scope.rbo_status

                    }
                    var url = 'api/AgrMstSamAgroMaster/InactiveSectorClassification';
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
                    sectorclassification_gid: sectorclassification_gid
                }

                var url = 'api/AgrMstSamAgroMaster/InactivesectorclassificationHistory';
                lockUI();
                SocketService.getparams(url, param).then(function (resp) {
                    $scope.sectorclassificationinactivelog_data = resp.data.inactivehistory_list;
                    unlockUI();
                });

            }
        }

        $scope.delete = function (sectorclassification_gid) {
            var params = {
                sectorclassification_gid: sectorclassification_gid
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
                    var url = 'api/AgrMstSamAgroMaster/DeleteSectorClassification';
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

