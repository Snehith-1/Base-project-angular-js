(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstBankNameController', MstBankNameController);

        MstBankNameController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function MstBankNameController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstBankNameController';
        $scope.lsoneapipage = $location.search().lsoneapipage;
        var lsoneapipage = $scope.lsoneapipage;
        activate();

        function activate() { 
            
            var url = 'api/MstApplication360/GetBankName';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.bankname_data = resp.data.application_list;
                unlockUI();
            });
        }

        $scope.addBankName = function () {
            var modalInstance = $modal.open({
                templateUrl: '/addBankName.html',
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
                        bankname_name: $scope.txtBankName_name,
                        lms_code: $scope.txtlms_code,
                        bureau_code: $scope.txtbureau_code

                    }
                    var url = 'api/MstApplication360/CreateBankName';
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

        $scope.editBankName = function (bankname_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/editBankName.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    bankname_gid: bankname_gid
                }
                var url = 'api/MstApplication360/EditBankName';
                SocketService.getparams(url, params).then(function (resp) {

                    $scope.txteditBankName_name = resp.data.bankname_name;
                    $scope.txteditlms_code = resp.data.lms_code;
                    $scope.txteditbureau_code = resp.data.bureau_code;
                    $scope.bankname_gid = resp.data.bankname_gid;
                });
                
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.update = function () {

                    var url = 'api/MstApplication360/UpdateBankName';
                    var params = {
                        bankname_name: $scope.txteditBankName_name,
                        lms_code: $scope.txteditlms_code,
                        bureau_code: $scope.txteditbureau_code,
                        bankname_gid: $scope.bankname_gid
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

        $scope.Status_update = function (bankname_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/statusBankName.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                var params = {
                    bankname_gid: bankname_gid
                }
                var url = 'api/MstApplication360/EditBankName';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.bankname_gid = resp.data.bankname_gid
                    $scope.bankname_name = resp.data.bankname_name;
                    $scope.rbo_status = resp.data.Status;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.update_status = function () {

                    var params = {
                        bankname_name :$scope.bankname_name,
                        bankname_gid: bankname_gid,
                        remarks: $scope.txtremarks,
                        rbo_status: $scope.rbo_status

                    }
                    var url = 'api/MstApplication360/InactiveBankName';
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
                    bankname_gid: bankname_gid
                }

                var url = 'api/MstApplication360/InactiveBankNameHistory';
                lockUI();
                SocketService.getparams(url, param).then(function (resp) {
                    $scope.banknameinactivelog_data = resp.data.inactivehistory_list;
                    unlockUI();
                });

            }
        }

        $scope.delete = function (bankname_gid) {
            var params = {
                bankname_gid: bankname_gid
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
                    var url = 'api/MstApplication360/DeleteBankName';
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

