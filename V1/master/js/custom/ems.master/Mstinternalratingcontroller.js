(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstInternalRatingController', MstInternalRatingController);

        MstInternalRatingController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function MstInternalRatingController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstInternalRatingController';
        $scope.lsoneapipage = $location.search().lsoneapipage;
        var lsoneapipage = $scope.lsoneapipage;
        activate();

        function activate() {          
             var url = 'api/MstApplication360/GetInternalRating';
             lockUI();
            SocketService.get(url).then(function (resp) {
               $scope.internalrating_list = resp.data.application_list;
                 unlockUI();
             });
        }
        $scope.addInternalRating = function () {
            var modalInstance = $modal.open({
                templateUrl: '/addInternalRating.html',
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
                        internalrating_name: $scope.txtinternalrating_name,
                        lms_code: $scope.txtlms_code,
                        bureau_code: $scope.txtbureau_code
                    }
                    var url = 'api/MstApplication360/Createinternalrating'; 
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
        $scope.editinternalrating = function (internalrating_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/editinternalrating.html',
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
                    internalrating_gid: internalrating_gid
                }
                var url = 'api/MstApplication360/Editinternalrating';
                SocketService.getparams(url, params).then(function (resp) {

                    $scope.txteditinternalrating_name = resp.data.internalrating_name;
                    $scope.txteditlms_code = resp.data.lms_code;
                    $scope.txteditbureau_code = resp.data.bureau_code;
                    $scope.internalrating_gid = resp.data.internalrating_gid;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.update = function () {

                    var url = 'api/MstApplication360/Updateinternalrating';
                    var params = {
                        internalrating_name: $scope.txteditinternalrating_name,
                        lms_code: $scope.txteditlms_code,
                        bureau_code: $scope.txteditbureau_code,
                        internalrating_gid: $scope.internalrating_gid
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

        $scope.Status_update = function (internalrating_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/statusinternalrating.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    internalrating_gid: internalrating_gid
                }            
                 var url = 'api/MstApplication360/Editinternalrating';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.internalrating_gid = resp.data.internalrating_gid
                    $scope.txtinternalrating_name = resp.data.internalrating_name;
                    $scope.rbo_status = resp.data.Status;
                });    
                           
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.update_status = function () {

                    var params = {
                        internalrating_gid:internalrating_gid,
                        remarks: $scope.txtremarks,
                        rbo_status:$scope.rbo_status
                    
                    }
                     var url = 'api/MstApplication360/Inactiveinternalrating'; 
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
                    internalrating_gid: internalrating_gid
                }

                var url = 'api/MstApplication360/InactiveinternalratingHistory';

                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.internalratinginactivelog_data = resp.data.application_list;
                    unlockUI();
                }); 
            }
        }

        $scope.delete = function (internalrating_gid) {
            var params = {
                internalrating_gid: internalrating_gid
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
                    var url = 'api/MstApplication360/Deleteinternalrating';
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

                 




