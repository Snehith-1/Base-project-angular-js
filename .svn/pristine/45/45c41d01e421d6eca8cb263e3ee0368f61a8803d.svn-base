// JavaScript source code

(function () {
    'use strict';

    angular
        .module('angle')
        .controller('BrsMstCloseRemainingAmountController', BrsMstCloseRemainingAmountController);

    BrsMstCloseRemainingAmountController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams', 'cmnfunctionService'];

    function BrsMstCloseRemainingAmountController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams, cmnfunctionService) {
        var vm = this;
        vm.title = 'BrsMstCloseRemainingAmountController';

        activate();

        function activate() {

            var url = 'api/CloseRemaindingAmount/GetRemaindingAmount';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.Remaining_lits = resp.data.remaindingamount_list;
                unlockUI();
            });
            var url = 'api/CloseRemaindingAmount/GetRemainingAmountStatus';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.activeamount_count = resp.data.activeamount_count;
                unlockUI();
            });
        }

        $scope.addRemainingAmount = function () {
            var modalInstance = $modal.open({
                templateUrl: '/addRemainingAmount.html',
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
                        remaindingamount_name : $scope.txtremainding_name,
                        remaindingamount_amount: $scope.txtremainding_amount,
                        remarks: $scope.txtremarks
                    }
                    var url = 'api/CloseRemaindingAmount/CreateRemaindingAmount';
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

                        }
                    });

                    $modalInstance.close('closed');

                }

            }
        }
        $scope.Status_update = function (remaindingamount_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/status.html',
                controller: ModalInstanceCtrl,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                var params = {
                    remaindingamount_gid: remaindingamount_gid
                }
                var url = 'api/CloseRemaindingAmount/GetRemainingAmount';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.remaindingamount_gid = resp.data.remaindingamount_gid;
                    $scope.txtremainding_amount = resp.data.remaindingamount_amount;
                    $scope.txtremarks = resp.data.remarks;
                    $scope.rbo_status = resp.data.Status;


                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.update_status = function () {

                    var params = {
                        remaindingamount_amount: $scope.txtremainding_amount,
                        remarks: $scope.txtremarks,
                        rbo_status: $scope.rbo_status,
                        remaindingamount_gid: remaindingamount_gid
                    }
                    var url = 'api/CloseRemaindingAmount/InactiveRemaindingAmount';
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
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                        }
                    });

                    $modalInstance.close('closed');

                }

                var param = {
                    remaindingamount_gid: remaindingamount_gid
                }

                var url = 'api/CloseRemaindingAmount/RemaindingAmountInactiveLogview';
                lockUI();
                SocketService.getparams(url, param).then(function (resp) {
                    $scope.inactiveremaindingamount_list = resp.data.inactiveremaindingamount_list;
                    unlockUI();
                });

            }
        }
        $scope.process = function (remaindingamount_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/ProcessRemainingAmount.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    remaindingamount_gid: remaindingamount_gid
                }
                var url = 'api/CloseRemaindingAmount/GetRemainingAmount';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.txtremainding_amount = resp.data.remaindingamount_amount;
                });    
                $scope.open1 = function ($event) {
                    $event.preventDefault();
                    $event.stopPropagation();
                    $scope.opened1 = true;
                };
                $scope.open2 = function ($event) {
                    $event.preventDefault();
                    $event.stopPropagation();
                    $scope.opened2 = true;
                };

                //$scope.minDate = new Date();

                $scope.formats = ['dd-MM-yyyy'];
                $scope.format = $scope.formats[0];
                $scope.dateOptions = {
                    formatYear: 'yy',
                    startingDay: 1
                };
               
                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.add_remainingamount = function () {

                    var params = {
                        remaindingamount_gid: remaindingamount_gid,
                        remainding_amount: $scope.txtremainding_amount,
                        start_date: $scope.txtfrom_date,
                        end_date: $scope.txtend_date,

                    }
                    var url = 'api/CloseRemaindingAmount/RemainingAmountClosed';
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
                        }
                        activate();
                    });

                    $modalInstance.close('closed');


                }


            }
        }
    }
})();