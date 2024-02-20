(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstHRLoanPurposeController', MstHRLoanPurposeController);

    MstHRLoanPurposeController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function MstHRLoanPurposeController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams)
    {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstHRLoanPurposeController';

        activate();

        function activate() {
            var url = 'api/MstHRLoanPurpose/GetHRLoanPurpose';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.purpose_data = resp.data.hrloanpurpose_list;
                unlockUI();
            });
        }
        $scope.addpurpose = function () {
            var modalInstance = $modal.open({
                templateUrl: '/addpurpose.html',
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
                        hrloanpurpose_name: $scope.txtpurpose_name,
                        purpose_note: $scope.txtpurpose_note,
                        mandatory: $scope.pur_mandatory,
                        lms_code: $scope.txtlms_code,
                        bureau_code: $scope.txtbureau_code

                    }
                    var url = 'api/MstHRLoanPurpose/CreateHRLoanPurpose';
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
        $scope.editpurpose = function (hrloanpurpose_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/editpurpose.html',
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
                    hrloanpurpose_gid: hrloanpurpose_gid
                }
                var url = 'api/MstHRLoanPurpose/EditHRLoanPurpose';
                SocketService.getparams(url, params).then(function (resp) {

                    $scope.txteditpurpose_name = resp.data.hrloanpurpose_name;
                    $scope.txteditpurpose_note = resp.data.purpose_note;
                    $scope.editpur_mandatory = resp.data.mandatory;
                    $scope.txteditlms_code = resp.data.lms_code;
                    $scope.txteditbureau_code = resp.data.bureau_code;
                    $scope.hrloanpurpose_gid = resp.data.hrloanpurpose_gid;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.update = function () {

                    var url = 'api/MstHRLoanPurpose/UpdateHRLoanPurpose';
                    var params = {
                        hrloanpurpose_name: $scope.txteditpurpose_name,
                        purpose_note: $scope.txteditpurpose_note,
                        mandatory: $scope.editpur_mandatory,
                        lms_code: $scope.txteditlms_code,
                        bureau_code: $scope.txteditbureau_code,
                        hrloanpurpose_gid: $scope.hrloanpurpose_gid
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

        $scope.Status_update = function (hrloanpurpose_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/statuspurpose.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    hrloanpurpose_gid: hrloanpurpose_gid
                }
                var url = 'api/MstHRLoanPurpose/EditHRLoanPurpose';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.hrloanpurpose_gid = resp.data.hrloanpurpose_gid
                    $scope.txtpurpose_name = resp.data.hrloanpurpose_name;
                    $scope.txteditpurpose_note = resp.data.purpose_note;
                    $scope.editpur_mandatory = resp.data.mandatory;
                    $scope.rbo_status = resp.data.Status;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.update_status = function () {

                    var params = {
                        hrloanpurpose_gid: hrloanpurpose_gid,
                        hrloanpurpose_name: $scope.txtpurpose_name,
                        purpose_note: $scope.txtpurpose_note,
                        mandatory: $scope.pur_mandatory,
                        remarks: $scope.txtremarks,
                        rbo_status: $scope.rbo_status

                    }
                    var url = 'api/MstHRLoanPurpose/InactiveHRLoanPurpose';
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
                        } activate();
                    });

                    $modalInstance.close('closed');

                }
                var params = {
                    hrloanpurpose_gid: hrloanpurpose_gid
                }

                var url = 'api/MstHRLoanPurpose/InactiveHRLoanPurposeHistory';

                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.purposeinactivelog_data = resp.data.purposeinactivehistory_list;
                    unlockUI();
                });
            }
        }

        $scope.delete = function (hrloanpurpose_gid) {
            var params = {
                hrloanpurpose_gid: hrloanpurpose_gid
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
                    var url = 'api/MstHRLoanPurpose/DeleteHRLoanPurpose';
                    SocketService.getparams(url, params).then(function (resp) {
                        unlockUI();
                        if (resp.data.status == true) {
                            SweetAlert.swal('Deleted Successfully!');
                            activate();
                        }
                        else {
                            Notify.alert(resp.data.message, {
                                status: 'warning',
                                pos: 'top-center',
                                timeout: 3000
                            });
                            activate();
                            unlockUI();
                        }
                    });
                }
            });
        }
    }
})();