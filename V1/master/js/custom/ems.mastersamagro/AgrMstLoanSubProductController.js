(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AgrMstLoanSubProductController', AgrMstLoanSubProductController);

    AgrMstLoanSubProductController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function AgrMstLoanSubProductController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AgrMstLoanSubProductController';
        $scope.lsoneapipage = $location.search().lsoneapipage;
        var lsoneapipage = $scope.lsoneapipage;


        activate();

        function activate() {
            var url = 'api/AgrMstApplication360/GetLoanSubProduct';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.loansubproduct_list = resp.data.application_list;
                unlockUI();
            });
        }

        $scope.addloansubproduct = function () {
            var modalInstance = $modal.open({
                templateUrl: '/addloansubproduct.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                var url = 'api/AgrMstApplication360/LoanProductList';
                lockUI();
                SocketService.get(url).then(function (resp) {
                    $scope.loanproduct_list = resp.data.loanproduct_list;
                    unlockUI();
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.submit = function () {

                    var params = {
                        loanproduct_gid: $scope.loanproduct.loanproduct_gid,
                        loanproduct_name: $scope.loanproduct.loanproduct_name,
                        loansubproduct_name: $scope.txtloansubproduct_name,
                        lms_code: $scope.txtlms_code,
                        bureau_code: $scope.txtbureau_code

                    }
                    var url = 'api/AgrMstApplication360/CreateLoanSubProduct';
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

        $scope.editloansubproduct = function (loansubproduct_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/editloansubproduct.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                var url = 'api/AgrMstApplication360/LoanProductList';
                lockUI();
                SocketService.get(url).then(function (resp) {
                    $scope.loanproduct_list = resp.data.loanproduct_list;
                    unlockUI();
                });

                var params = {
                    loansubproduct_gid: loansubproduct_gid
                }
                var url = 'api/AgrMstApplication360/EditLoanSubProduct';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.loanproduct_Gid = resp.data.loanproduct_gid;
                    $scope.loanproduct = resp.data.loanproduct_name;
                    $scope.txteditloansubproduct_name = resp.data.loansubproduct_name;
                    $scope.txteditlms_code = resp.data.lms_code;
                    $scope.txteditbureau_code = resp.data.bureau_code;
                    $scope.loansubproduct_gid = resp.data.loansubproduct_gid;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };


                $scope.update = function () {
                    var loanproduct = $('#loanproduct :selected').text();
                    var url = 'api/AgrMstApplication360/UpdateLoanSubProduct';
                    var params = {
                        loansubproduct_name: $scope.txteditloansubproduct_name,
                        lms_code: $scope.txteditlms_code,
                        bureau_code: $scope.txteditbureau_code,
                        loansubproduct_gid: $scope.loansubproduct_gid,
                        loanproduct_name: loanproduct,
                        loanproduct_gid: $scope.loanproduct_Gid
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

        $scope.Status_update = function (loansubproduct_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/statusloansubproduct.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    loansubproduct_gid: loansubproduct_gid
                }

                var url = 'api/AgrMstApplication360/EditLoanSubProduct';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.txtloansubproduct_name = resp.data.loansubproduct_name;
                    $scope.loansubproduct_gid = resp.data.loansubproduct_gid;
                    $scope.rbo_status = resp.data.Status;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.update_status = function () {

                    var params = {
                        loansubproduct_name: $scope.txtloansubproduct_name,
                        remarks: $scope.txtremarks,
                        rbo_status: $scope.rbo_status,
                        loansubproduct_gid: loansubproduct_gid
                    }
                    var url = 'api/AgrMstApplication360/InactiveLoanSubProduct';
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
                        } activate();
                    });

                    $modalInstance.close('closed');

                }
                var param = {
                    loansubproduct_gid: loansubproduct_gid
                }

                var url = 'api/AgrMstApplication360/LoanSubProductInactiveLogview';
                lockUI();
                SocketService.getparams(url, param).then(function (resp) {
                    $scope.loansubproductinactivelog_list = resp.data.application_list;
                    unlockUI();
                });

            }
        }

        $scope.delete = function (loansubproduct_gid) {
            var params = {
                loansubproduct_gid: loansubproduct_gid
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
                    var url = 'api/AgrMstApplication360/DeleteLoanSubProduct';
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

