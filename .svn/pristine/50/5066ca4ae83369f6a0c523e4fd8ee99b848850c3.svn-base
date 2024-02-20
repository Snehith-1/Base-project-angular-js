(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstClientDetailsController', MstClientDetailsController);

        MstClientDetailsController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function MstClientDetailsController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstClientDetailsController';
        $scope.lsoneapipage = $location.search().lsoneapipage;
        var lsoneapipage = $scope.lsoneapipage;

        activate();

        function activate() { 
            
            var url = 'api/MstApplication360/GetClientDetails';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.clientdetails_data = resp.data.application_list;
                unlockUI();
            });
        }

        $scope.addClientDetails = function () {
            var modalInstance = $modal.open({
                templateUrl: '/addClientDetails.html',
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
                        clientdetails_name: $scope.txtClientDetails_name,
                        lms_code: $scope.txtlms_code,
                        bureau_code: $scope.txtbureau_code

                    }
                    var url = 'api/MstApplication360/CreateClientDetails';
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

        $scope.editClientDetails = function (clientdetails_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/editClientDetails.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    clientdetails_gid: clientdetails_gid
                }
                var url = 'api/MstApplication360/EditClientDetails';
                SocketService.getparams(url, params).then(function (resp) {

                    $scope.txteditClientDetails_name = resp.data.clientdetails_name;
                    $scope.txteditlms_code = resp.data.lms_code;
                    $scope.txteditbureau_code = resp.data.bureau_code;
                    $scope.clientdetails_gid = resp.data.clientdetails_gid;
                });
                
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.update = function () {

                    var url = 'api/MstApplication360/UpdateClientDetails';
                    var params = {
                        clientdetails_name: $scope.txteditClientDetails_name,
                        lms_code: $scope.txteditlms_code,
                        bureau_code: $scope.txteditbureau_code,
                        clientdetails_gid: $scope.clientdetails_gid
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

        $scope.Status_update = function (clientdetails_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/statusClientDetails.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                var params = {
                    clientdetails_gid: clientdetails_gid
                }
                var url = 'api/MstApplication360/EditClientDetails';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.clientdetails_gid = resp.data.clientdetails_gid
                    $scope.clientdetails_name = resp.data.clientdetails_name;
                    $scope.rbo_status = resp.data.Status;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.update_status = function () {

                    var params = {
                        clientdetails_name: $scope.clientdetails_name,
                        clientdetails_gid: clientdetails_gid,
                        remarks: $scope.txtremarks,
                        rbo_status: $scope.rbo_status,
                    }
                    var url = 'api/MstApplication360/InactiveClientDetails';
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
                    clientdetails_gid: clientdetails_gid
                }

                var url = 'api/MstApplication360/InactiveClientDetailsHistory';
                lockUI();
                SocketService.getparams(url, param).then(function (resp) {
                    $scope.clientdetailsinactivelog_data = resp.data.inactivehistory_list;
                    unlockUI();
                });

            }
        }

        $scope.delete = function (clientdetails_gid) {
            var params = {
                clientdetails_gid: clientdetails_gid
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
                    var url = 'api/MstApplication360/DeleteClientDetails';
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

