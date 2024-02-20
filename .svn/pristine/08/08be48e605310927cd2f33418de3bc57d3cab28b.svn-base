(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstLiveStockController', MstLiveStockController);

        MstLiveStockController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function MstLiveStockController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstLiveStockController';
        $scope.lsoneapipage = $location.search().lsoneapipage;
        var lsoneapipage = $scope.lsoneapipage;
        activate();

        function activate() {          
             var url = 'api/MstApplication360/GetLiveStock';
             lockUI();
            SocketService.get(url).then(function (resp) {
               $scope.livestock_list = resp.data.application_list;
                 unlockUI();
             });
        }
        $scope.addLiveStock = function () {
            var modalInstance = $modal.open({
                templateUrl: '/addLiveStock.html',
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
                        livestock_name: $scope.txtlivestock_name,
                        lms_code: $scope.txtlms_code,
                        bureau_code: $scope.txtbureau_code
                    
                    }
                    var url = 'api/MstApplication360/CreateLiveStock'; 
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
                            $modalInstance.close('closed');
                            activate();
                        }
                    });
                }
                
            }
        }
        $scope.editlivestock = function (livestock_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/editLiveStock.html',
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
                var params = {
                    livestock_gid: livestock_gid
                }
                var url = 'api/MstApplication360/Editlivestock';
                SocketService.getparams(url, params).then(function (resp) {

                    $scope.txteditlivestock_name = resp.data.livestock_name;
                    $scope.txteditlms_code = resp.data.lms_code;
                    $scope.txteditbureau_code = resp.data.bureau_code;
                    $scope.livestock_gid = resp.data.livestock_gid;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.update = function () {

                    var url = 'api/MstApplication360/UpdateLiveStock';
                    var params = {
                        livestock_name: $scope.txteditlivestock_name,
                        lms_code: $scope.txteditlms_code,
                        bureau_code: $scope.txteditbureau_code,
                        livestock_gid: $scope.livestock_gid
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

        $scope.Status_update = function (livestock_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/statusLiveStock.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    livestock_gid: livestock_gid
                }            
                 var url = 'api/MstApplication360/EditLiveStock';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.livestock_gid = resp.data.livestock_gid
                    $scope.txtlivestock_name = resp.data.livestock_name;
                    $scope.rbo_status = resp.data.Status;
                });    
                           
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.update_status = function () {

                    var params = {
                        livestock_gid:livestock_gid,
                        remarks: $scope.txtremarks,
                        rbo_status:$scope.rbo_status
                    
                    }
                     var url = 'api/MstApplication360/InactiveLiveStock'; 
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
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                        }activate();
                    }); 

                    $modalInstance.close('closed');

                }
                var params = {
                    livestock_gid: livestock_gid
                }

                var url = 'api/MstApplication360/InactiveLiveStockHistory';

                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.livestockinactivelog_data = resp.data.inactivehistory_list;
                    unlockUI();
                }); 
            }
        }

        $scope.delete = function (livestock_gid) {
            var params = {
                livestock_gid: livestock_gid
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
                    var url = 'api/MstApplication360/DeleteLiveStock';
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
                        }
                    });
                    }
            });
        }
    }
})();

