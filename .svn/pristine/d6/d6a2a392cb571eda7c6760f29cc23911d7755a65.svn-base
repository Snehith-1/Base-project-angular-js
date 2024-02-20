(function () {
    'use strict';

    angular
        .module('angle')
        .controller('BRSActivityController', BRSActivityController);

        BRSActivityController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function BRSActivityController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'BRSActivityController';

        activate();

        function activate() { 
          
            var url = 'api/BRSMaster/GetBRSActivity';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.BRSactivity_list = resp.data.BRSActivity_List;
                unlockUI();
            });
        }

        $scope.addbrsactivity = function () {
            var modalInstance = $modal.open({
                templateUrl: '/addbrsactivity.html',
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
                        brsactivity_name: $scope.txtbrsactivity_name,
                        lms_code: $scope.txtlms_code,
                        bureau_code: $scope.txtbureau_code
                    
                    }
                    var url = 'api/BRSMaster/CreateBRSActivity';
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

        $scope.editbrsactivity = function (brsactivity_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/editbrsactivity.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                 var params = {
                    brsactivity_gid: brsactivity_gid
                }
                 var url = 'api/BRSMaster/EditBRSActivity';
                SocketService.getparams(url, params).then(function (resp) {

                    $scope.txteditbrsactivity_name = resp.data.brsactivity_name;
                    $scope.txteditlms_code = resp.data.lms_code;
                    $scope.txteditbureau_code = resp.data.bureau_code;
                    $scope.brsactivity_gid = resp.data.brsactivity_gid;
                }); 
                
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                
               
                $scope.update = function () {

                    var url = 'api/BRSMaster/UpdateBRSActivity';
                    var params = {
                        brsactivity_name: $scope.txteditbrsactivity_name,
                        lms_code: $scope.txteditlms_code,
                        bureau_code: $scope.txteditbureau_code,
                        brsactivity_gid: $scope.brsactivity_gid
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

        $scope.Status_update = function (brsactivity_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/statusbrsactivity.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    brsactivity_gid: brsactivity_gid
                }
                var url = 'api/BRSMaster/EditBRSActivity';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.txtbrsactivity_name = resp.data.brsactivity_name;
                   $scope.brsactivity_gid = resp.data.brsactivity_gid;
                    $scope.rbo_status = resp.data.status_log;
                });    
                var url = 'api/BRSMaster/GetBRSActivityActiveLog';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.brsactivityinactive_list = resp.data.BRSActivity_List;
                });
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.update_status = function () {

                    var params = {
                        brsactivity_name: $scope.txtbrsactivity_name,
                        remarks: $scope.txtremarks,
                        status_log:$scope.rbo_status,
                        brsactivity_gid:brsactivity_gid
                    }
                    var url = 'api/BRSMaster/BRSActivityStatusUpdate';
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
                        }activate();
                    }); 

                    $modalInstance.close('closed');

                }
                
            }
        }

        $scope.delete = function (brsactivity_gid) {
             var params = {
                brsactivity_gid: brsactivity_gid
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
                            var url = 'api/BRSMaster/BRSActivityDelete';
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

