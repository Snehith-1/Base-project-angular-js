(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstBankAccountLevelController', MstBankAccountLevelController);

    MstBankAccountLevelController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function MstBankAccountLevelController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstBankAccountLevelController';
        $scope.lsoneapipage = $location.search().lsoneapipage;
        var lsoneapipage = $scope.lsoneapipage;
        activate();

        function activate() {
            $scope.total = 0;
            $scope.totalDisplayed = 100;
            var url = 'api/MstApplication360/GetBankAccountLevel';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.bankaccountlevel_data = resp.data.application_list;
                unlockUI();
            });
        }

        $scope.addBankAccountLevel = function () {
            var modalInstance = $modal.open({
                templateUrl: '/addBankAccountLevel.html',
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
                        bankaccountlevel_name: $scope.txBankAccountLevel_name,
                        lms_code: $scope.txtlms_code,
                        bureau_code: $scope.txtbureau_code

                    }
                    var url = 'api/MstApplication360/CreateBankAccountLevel';
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
                }
                
            }
        }

        $scope.editbankaccountlevel = function (bankaccountlevel_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/editBankAccountLevel.html',
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
                    bankaccountlevel_gid: bankaccountlevel_gid
                }
                var url = 'api/MstApplication360/EditBankAccountLevel';
                SocketService.getparams(url, params).then(function (resp) {

                    $scope.txteditBankAccountLevel_name = resp.data.bankaccountlevel_name;
                    $scope.txteditlms_code = resp.data.lms_code;
                    $scope.txteditbureau_code = resp.data.bureau_code;
                    $scope.bankaccountlevel_gid = resp.data.bankaccountlevel_gid;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.update = function () {

                    var url = 'api/MstApplication360/UpdateBankAccountLevel';
                    var params = {
                        bankaccountlevel_name: $scope.txteditBankAccountLevel_name,
                        lms_code: $scope.txteditlms_code,
                        bureau_code: $scope.txteditbureau_code,
                        bankaccountlevel_gid: $scope.bankaccountlevel_gid
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

        $scope.Status_update = function (bankaccountlevel_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/statusBankAccountLevel.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    bankaccountlevel_gid: bankaccountlevel_gid
                }
                var url = 'api/MstApplication360/EditBankAccountLevel';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.bankaccountlevel_gid = resp.data.bankaccountlevel_gid
                    $scope.txtbankaccountlevel_name = resp.data.bankaccountlevel_name;
                    $scope.rbo_status = resp.data.Status;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.update_status = function () {

                    var params = {
                        bankaccountlevel_gid: bankaccountlevel_gid,
                        bankaccountlevel_name: $scope.txtbankaccountlevel_name,
                        remarks: $scope.txtremarks,
                        rbo_status: $scope.rbo_status
                    }

                    var url = 'api/MstApplication360/InactiveBankAccountLevel';
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
                    bankaccountlevel_gid: bankaccountlevel_gid
                }

                var url = 'api/MstApplication360/BankAccountLevelInactiveLogview';
                lockUI();
                SocketService.getparams(url, param).then(function (resp) {
                    $scope.bankaccountlevelinactivelog_data = resp.data.application_list;
                    unlockUI();
                });
            }
        }

        $scope.delete = function (bankaccountlevel_gid) {
            var params = {
                bankaccountlevel_gid: bankaccountlevel_gid
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
                            var url = 'api/MstApplication360/DeleteBankAccountLevel';
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

