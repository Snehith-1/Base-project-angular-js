(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstBankAccountTypeController', MstBankAccountTypeController);

        MstBankAccountTypeController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function MstBankAccountTypeController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstBankAccountTypeController';
        $scope.lsoneapipage = $location.search().lsoneapipage;
        var lsoneapipage = $scope.lsoneapipage;
        activate();

        function activate() { 
            
            var url = 'api/MstApplication360/GetBankAccountType';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.bankaccounttype_list = resp.data.application_list;
                unlockUI();
            });
        }

        $scope.addBankAccountType = function () {
            var modalInstance = $modal.open({
                templateUrl: '/addBankAccountType.html',
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
                        bankaccounttype_name: $scope.txtbankaccounttype_name,
                        lms_code: $scope.txtlms_code,
                        bureau_code: $scope.txtbureau_code

                    }
                    var url = 'api/MstApplication360/CreateBankAccountType';
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

        $scope.editBankAccountType = function (bankaccounttype_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/editBankAccountType.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    bankaccounttype_gid: bankaccounttype_gid
                }
                var url = 'api/MstApplication360/EditBankAccountType';
                SocketService.getparams(url, params).then(function (resp) {

                    $scope.txteditbankaccounttype_name = resp.data.bankaccounttype_name;
                    $scope.txteditlms_code = resp.data.lms_code;
                    $scope.txteditbureau_code = resp.data.bureau_code;
                    $scope.bankaccounttype_gid = resp.data.bankaccounttype_gid;
                });
                
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.update = function () {

                    var url = 'api/MstApplication360/UpdateBankAccountType';
                    var params = {
                        bankaccounttype_name: $scope.txteditbankaccounttype_name,
                        lms_code: $scope.txteditlms_code,
                        bureau_code: $scope.txteditbureau_code,
                        bankaccounttype_gid: $scope.bankaccounttype_gid
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

        $scope.Status_update = function (bankaccounttype_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/statusBankAccountType.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                var params = {
                    bankaccounttype_gid: bankaccounttype_gid
                }
                var url = 'api/MstApplication360/EditBankAccountType';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.bankaccounttype_gid = resp.data.bankaccounttype_gid;
                    $scope.bankaccounttype_name = resp.data.bankaccounttype_name;
                    $scope.rbo_status = resp.data.Status;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.update_status = function () {

                    var params = {
                        bankaccounttype_name: $scope.bankaccounttype_name,
                        bankaccounttype_gid: $scope.bankaccounttype_gid,
                        remarks: $scope.txtremarks,
                        rbo_status: $scope.rbo_status

                    }
                    var url = 'api/MstApplication360/InactiveBankAccountType';
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
                    bankaccounttype_gid: bankaccounttype_gid
                }

                var url = 'api/MstApplication360/InactiveBankAccountTypeHistory';
                lockUI();
                SocketService.getparams(url, param).then(function (resp) {
                    $scope.bankaccounttypeinactivelog_data = resp.data.inactivehistory_list;
                    unlockUI();
                });

            }
        }

        $scope.delete = function (bankaccounttype_gid) {
            var params = {
                bankaccounttype_gid: bankaccounttype_gid
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
                    var url = 'api/MstApplication360/DeleteBankAccountType';
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

