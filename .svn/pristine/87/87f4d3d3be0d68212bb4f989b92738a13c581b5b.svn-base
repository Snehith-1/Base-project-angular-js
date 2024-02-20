(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstVerticalTagsController', MstVerticalTagsController);

        MstVerticalTagsController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function MstVerticalTagsController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstVerticalTagsController';
        $scope.lsoneapipage = $location.search().lsoneapipage;
        var lsoneapipage = $scope.lsoneapipage;
        activate();

        function activate() { 
            var url = 'api/MstApplication360/GetVerticalTaggs';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.verticaltaggs_list = resp.data.application_list;
                unlockUI();
            });

           
        }

        $scope.addverticaltags = function () {
            var modalInstance = $modal.open({
                templateUrl: '/addverticaltags.html',
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
                var url = 'api/MstApplication360/EntityList';
                lockUI();
                SocketService.get(url).then(function (resp) {
                    $scope.entitylist = resp.data.entitylist;
                    unlockUI();
                });

                $scope.submit = function () {

                    var params = {
                        entity_gid: $scope.entity.entity_gid,
                        entity_name: $scope.entity.entity_name,
                        verticaltaggs_name: $scope.txtverticaltaggs_name,
                        lms_code: $scope.txtlms_code,
                        bureau_code: $scope.txtbureau_code
                    
                    }
                    var url = 'api/MstApplication360/CreateVerticalTaggs';
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

        $scope.editverticaltags = function (verticaltaggs_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/editverticaltags.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var url = 'api/MstApplication360/EntityList';
                lockUI();
                SocketService.get(url).then(function (resp) {
                    $scope.entitylist = resp.data.entitylist;
                    unlockUI();
                });



                var params = {
                    verticaltaggs_gid: verticaltaggs_gid
                }
                var url = 'api/MstApplication360/EditVerticalTaggs';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.entity_Gid = resp.data.entity_gid;
                    $scope.entity_Name = resp.data.entity_name;
                    $scope.txteditverticaltaggs_name = resp.data.verticaltaggs_name;
                    $scope.txteditlms_code = resp.data.lms_code;
                    $scope.txteditbureau_code = resp.data.bureau_code;
                    $scope.verticaltaggs_gid = resp.data.verticaltaggs_gid;
                });



                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.update = function () {
                    var entity_name = $('#entity_Name :selected').text();
                    var url = 'api/MstApplication360/UpdateVerticalTaggs';
                    var params = {
                        verticaltaggs_gid: verticaltaggs_gid,
                        verticaltaggs_name: $scope.txteditverticaltaggs_name,
                        lms_code: $scope.txteditlms_code,
                        bureau_code: $scope.txteditbureau_code,
                        entity_name: entity_name,
                        entity_gid: $scope.entity_Gid
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
                            
                        }
                    });

                    $modalInstance.close('closed');

                }
                
            }
        }

        $scope.Status_update = function (verticaltaggs_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/statusverticaltags.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    verticaltaggs_gid: verticaltaggs_gid
                }          
                var url = 'api/MstApplication360/EditVerticalTaggs';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.verticaltaggs_gid = resp.data.verticaltaggs_gid;
                    $scope.txtverticaltaggs_name = resp.data.verticaltaggs_name;
                    $scope.rbo_status = resp.data.Status;
                });   
                           
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.update_status = function () {

                    var params = {
                        entity:$scope.entity,
                        verticaltaggs_name: $scope.txtverticaltaggs_name,
                        remarks: $scope.txtremarks,
                        rbo_status: $scope.rbo_status,
                        verticaltaggs_gid:$scope.verticaltaggs_gid
                    
                    }
                    var url = 'api/MstApplication360/InactiveVerticalTaggs';
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
                    verticaltaggs_gid: verticaltaggs_gid
                }

                var url = 'api/MstApplication360/VerticalTaggsInactiveLogview';
                lockUI();
                SocketService.getparams(url, param).then(function (resp) {
                    $scope.verticaltaggsinactivelog_list = resp.data.application_list;
                    unlockUI();
                });
            }
        }

        $scope.delete = function (verticaltaggs_gid) {
             var params = {
                verticaltaggs_gid: verticaltaggs_gid
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
                            var url = 'api/MstApplication360/DeleteVerticalTaggs';
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

