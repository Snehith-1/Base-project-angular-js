(function () {
    'use strict';

    angular
        .module('angle')
        .controller('AgrMstMilestonePaymentTypeController', AgrMstMilestonePaymentTypeController);

    AgrMstMilestonePaymentTypeController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function AgrMstMilestonePaymentTypeController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'AgrMstMilestonePaymentTypeController';
        $scope.lsoneapipage = $location.search().lsoneapipage;
        var lsoneapipage = $scope.lsoneapipage;


        activate();

        function activate() {
            var url = 'api/AgrMstApplication360/GetMilestonePaymentType';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.milestonepaymenttype_data = resp.data.application_list;
                unlockUI();
            });
        }

        $scope.addmilestonepaymenttype = function () {
            var modalInstance = $modal.open({
                templateUrl: '/addmilestonepaymenttype.html',
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
                        milestonepaymenttype_name: $scope.txtmilestone_paymenttype,
                        lms_code: $scope.txtlms_code,
                        bureau_code: $scope.txtbureau_code
                    }
                    var url = 'api/AgrMstApplication360/CreatetMilestonePaymentType';
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

        $scope.editmilestonepaymenttype = function (milestonepaymenttype_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/editmilestonepaymenttype.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {

                var params = {
                    milestonepaymenttype_gid: milestonepaymenttype_gid
                }
                var url = 'api/AgrMstApplication360/EditMilestonePaymentType';
                SocketService.getparams(url, params).then(function (resp) {

                    $scope.txteditmilestone_paymenttype = resp.data.milestonepaymenttype_name;
                    $scope.txteditlms_code = resp.data.lms_code;
                    $scope.txteditbureau_code = resp.data.bureau_code;
                    $scope.milestonepaymenttype_gid = resp.data.milestonepaymenttype_gid;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.update = function () {
                    var url = 'api/AgrMstApplication360/UpdateMilestonePaymentType';
                    var params = {
                        milestonepaymenttype_name: $scope.txteditmilestone_paymenttype,
                        lms_code: $scope.txteditlms_code,
                        bureau_code: $scope.txteditbureau_code,
                        milestonepaymenttype_gid: $scope.milestonepaymenttype_gid
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

        $scope.Status_update = function (milestonepaymenttype_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/statusmilestonepaymenttype.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    milestonepaymenttype_gid: milestonepaymenttype_gid
                }
                var url = 'api/AgrMstApplication360/EditMilestonePaymentType';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.milestonepaymenttype_gid = resp.data.milestonepaymenttype_gid
                    $scope.txtmilestone_paymenttype = resp.data.milestonepaymenttype_name;
                    $scope.rbo_status = resp.data.Status;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.update_status = function () {

                    var params = {
                        milestonepaymenttype_gid: $scope.milestonepaymenttype_gid,
                        milestonepaymenttype_name: $scope.txtmilestone_paymenttype,
                        remarks: $scope.txtremarks,
                        rbo_status: $scope.rbo_status

                    }
                    var url = 'api/AgrMstApplication360/InactiveMilestonePaymentType';
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
                    milestonepaymenttype_gid: milestonepaymenttype_gid
                }

                var url = 'api/AgrMstApplication360/MilestonePaymentTypeInactiveLogview';
                lockUI();
                SocketService.getparams(url, param).then(function (resp) {
                    $scope.milestonepaymenttypeinactivelog_data = resp.data.application_list;
                    unlockUI();
                });
            }
        }

        $scope.delete = function (milestonepaymenttype_gid) {
            var params = {
                milestonepaymenttype_gid: milestonepaymenttype_gid
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
                    var url = 'api/AgrMstApplication360/DeleteMilestonePaymentType';
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

