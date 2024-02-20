(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstLendingarrangementController', MstLendingarrangementController);

        MstLendingarrangementController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function MstLendingarrangementController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstLendingarrangementController';
        $scope.lsoneapipage = $location.search().lsoneapipage;
        var lsoneapipage = $scope.lsoneapipage;
        activate();

        function activate() { 

            var url = 'api/MstApplication360/GetLendingArrangement';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.LendingArrangement_list = resp.data.application_list;
                unlockUI();
            });
        }

        $scope.addLendingArrangement = function () {
            var modalInstance = $modal.open({
                templateUrl: '/addLendingArrangement.html',
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
                        lendingarrangement_name: $scope.txtLendingArrangement_name,
                        lms_code: $scope.txtlms_code,
                        bureau_code: $scope.txtbureau_code
                    
                    }
                    var url = 'api/MstApplication360/CreateLendingArrangement';
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

        $scope.editLendingArrangement = function (lendingarrangement_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/editLendingArrangement.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                var params = {
                    lendingarrangement_gid: lendingarrangement_gid
                }
                var url = 'api/MstApplication360/EditLendingArrangement';
                SocketService.getparams(url, params).then(function (resp) {

                    $scope.txteditLendingArrangement_name = resp.data.lendingarrangement_name;
                    $scope.txteditlms_code = resp.data.lms_code;
                    $scope.txteditbureau_code = resp.data.bureau_code;
                    $scope.lendingarrangement_gid = resp.data.lendingarrangement_gid;
                }); 
                
                 
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                                              
                $scope.update = function () {

                    var url = 'api/MstApplication360/UpdateLendingArrangement';
                    var params = {
                        lendingarrangement_name: $scope.txteditLendingArrangement_name,
                        lms_code: $scope.txteditlms_code,
                        bureau_code: $scope.txteditbureau_code,
                        lendingarrangement_gid: $scope.lendingarrangement_gid
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

        $scope.Status_update = function (lendingarrangement_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/statusLendingArrangement.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                                
                var params = {
                    lendingarrangement_gid: lendingarrangement_gid
                }
                var url = 'api/MstApplication360/EditLendingArrangement';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.lendingarrangement_gid = resp.data.lendingarrangement_gid
                    $scope.txtlendingarrangement_name = resp.data.lendingarrangement_name;
                    $scope.rbo_status = resp.data.Status;
                });
                           
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.update_status = function () {

                    var params = {
                        lendingarrangement_gid: lendingarrangement_gid,
                        remarks: $scope.txtremarks,
                        rbo_status: $scope.rbo_status

                    }

                    var url = 'api/MstApplication360/InactiveLendingArrangement';
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
                    lendingarrangement_gid: lendingarrangement_gid
                }

                var url = 'api/MstApplication360/InactiveLendingArrangementHistory';
                lockUI();
                SocketService.getparams(url, param).then(function (resp) {
                    $scope.lendingarrangementinactivelog_data = resp.data.inactivehistory_list;
                    unlockUI();
                });
            }
        }

        $scope.delete = function (lendingarrangement_gid) {
             var params = {
                lendingarrangement_gid: lendingarrangement_gid
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
                    var url = 'api/MstApplication360/DeleteLendingArrangement';
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

