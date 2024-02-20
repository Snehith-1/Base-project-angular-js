(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstAssetstypeController', MstAssetstypeController);

        MstAssetstypeController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function MstAssetstypeController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstAssetstypeController';
        $scope.lsoneapipage = $location.search().lsoneapipage;
        var lsoneapipage = $scope.lsoneapipage;
        activate();

        function activate() { 
           
            var url = 'api/MstApplication360/GetAssetsType';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.AssetsType_list = resp.data.application_list;
                unlockUI();
            });
        }

        $scope.addAssetsType = function () {
            var modalInstance = $modal.open({
                templateUrl: '/addAssetsType.html',
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
                        assetstype_name: $scope.txtAssetsType_name,
                        lms_code: $scope.txtlms_code,
                        bureau_code: $scope.txtbureau_code
                    
                    }
                    var url = 'api/MstApplication360/CreateAssetsType';
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
                
            }
        }

        $scope.editAssetsType = function (assetstype_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/editAssetsType.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    assetstype_gid: assetstype_gid
                }
                var url = 'api/MstApplication360/EditAssetsType';
                SocketService.getparams(url, params).then(function (resp) {

                    $scope.txteditAssetsType_name = resp.data.assetstype_name;
                    $scope.txteditlms_code = resp.data.lms_code;
                    $scope.txteditbureau_code = resp.data.bureau_code;
                    $scope.assetstype_gid = resp.data.assetstype_gid;
                });
                
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                                              
                $scope.update = function () {
                    var url = 'api/MstApplication360/UpdateAssetsType';
                    var params = {
                        assetstype_name: $scope.txteditAssetsType_name,
                        lms_code: $scope.txteditlms_code,
                        bureau_code: $scope.txteditbureau_code,
                        assetstype_gid: $scope.assetstype_gid
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

        $scope.Status_update = function (assetstype_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/statusAssetsType.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                                
                var params = {
                    assetstype_gid: assetstype_gid
                }
                var url = 'api/MstApplication360/EditAssetsType';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.assetstype_gid = resp.data.assetstype_gid
                    $scope.txtassetstype_name = resp.data.assetstype_name;
                    $scope.rbo_status = resp.data.Status;
                });
                           
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.update_status = function () {

                    var params = {
                        assetstype_name: $scope.txtassetstype_name,
                        assetstype_gid : $scope.assetstype_gid,
                        remarks: $scope.txtremarks,
                        rbo_status:$scope.rbo_status
                    
                    }
                    var url = 'api/MstApplication360/InactiveAssetsType';
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
                    assetstype_gid: assetstype_gid
                }

                var url = 'api/MstApplication360/InactiveAssetsTypeHistory';
                lockUI();
                SocketService.getparams(url, param).then(function (resp) {
                    $scope.assetstypeinactivelog_data = resp.data.inactivehistory_list;
                    unlockUI();
                });
            }
        }

        $scope.delete = function (assetstype_gid) {
             var params = {
                 assetstype_gid: assetstype_gid
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
                    var url = 'api/MstApplication360/DeleteAssetsType';
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

