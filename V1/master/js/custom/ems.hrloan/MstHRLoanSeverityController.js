﻿(function () {
    'use strict';

    angular
        .module('angle')
        .controller('MstHRLoanSeverityController', MstHRLoanSeverityController);

    MstHRLoanSeverityController.$inject = ['$rootScope', '$scope', '$state', 'AuthenticationService', '$modal', 'ScopeValueService', '$http', 'SocketService', 'Notify', '$location', 'apiManage', 'SweetAlert', '$route', 'ngTableParams'];

    function MstHRLoanSeverityController($rootScope, $scope, $state, AuthenticationService, $modal, ScopeValueService, $http, SocketService, Notify, $location, apiManage, SweetAlert, $route, ngTableParams) {
        /* jshint validthis:true */
        var vm = this;
        vm.title = 'MstHRLoanSeverityController';

        activate();

        function activate() {
            var url = 'api/MstHRLoanSeverity/GetHRLoanSeverity';
            lockUI();
            SocketService.get(url).then(function (resp) {
                $scope.severity_data = resp.data.hrloanseverity_list;
                unlockUI();
            });
        }
        $scope.addseverity = function () {
            var modalInstance = $modal.open({
                templateUrl: '/addseverity.html',
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
                        hrloanseverity_name: $scope.txtseverity_name,
                        lms_code: $scope.txtlms_code,
                        bureau_code: $scope.txtbureau_code

                    }
                    var url = 'api/MstHRLoanSeverity/CreateHRLoanSeverity';
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
        $scope.editseverity = function (hrloanseverity_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/editseverity.html',
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
                    hrloanseverity_gid: hrloanseverity_gid
                }
                var url = 'api/MstHRLoanSeverity/EditHRLoanSeverity';
                SocketService.getparams(url, params).then(function (resp) {

                    $scope.txteditseverity_name = resp.data.hrloanseverity_name;
                    $scope.txteditlms_code = resp.data.lms_code;
                    $scope.txteditbureau_code = resp.data.bureau_code;
                    $scope.hrloanseverity_gid = resp.data.hrloanseverity_gid;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };

                $scope.update = function () {

                    var url = 'api/MstHRLoanSeverity/UpdateHRLoanSeverity';
                    var params = {
                        hrloanseverity_name: $scope.txteditseverity_name,
                        lms_code: $scope.txteditlms_code,
                        bureau_code: $scope.txteditbureau_code,
                        hrloanseverity_gid: $scope.hrloanseverity_gid
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

        $scope.Status_update = function (hrloanseverity_gid) {
            var modalInstance = $modal.open({
                templateUrl: '/statusseverity.html',
                controller: ModalInstanceCtrl,
                backdrop: 'static',
                keyboard: false,
                size: 'md'
            });
            ModalInstanceCtrl.$inject = ['$scope', '$modalInstance'];
            function ModalInstanceCtrl($scope, $modalInstance) {
                var params = {
                    hrloanseverity_gid: hrloanseverity_gid
                }
                var url = 'api/MstHRLoanSeverity/EditHRLoanSeverity';
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.hrloanseverity_gid = resp.data.hrloanseverity_gid
                    $scope.txtseverity_name = resp.data.hrloanseverity_name;
                    $scope.rbo_status = resp.data.Status;
                });

                $scope.ok = function () {
                    $modalInstance.close('closed');
                };
                $scope.update_status = function () {

                    var params = {
                        hrloanseverity_gid: hrloanseverity_gid,
                        hrloanseverity_name: $scope.txtseverity_name,
                        remarks: $scope.txtremarks,
                        rbo_status: $scope.rbo_status

                    }
                    var url = 'api/MstHRLoanSeverity/InactiveHRLoanSeverity';
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
                    hrloanseverity_gid: hrloanseverity_gid
                }

                var url = 'api/MstHRLoanSeverity/InactiveHRLoanSeverityHistory';

                lockUI();
                SocketService.getparams(url, params).then(function (resp) {
                    $scope.severityinactivelog_data = resp.data.severityinactivehistory_list;
                    unlockUI();
                });
            }
        }

        $scope.delete = function (hrloanseverity_gid) {
            var params = {
                hrloanseverity_gid: hrloanseverity_gid
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
                    var url = 'api/MstHRLoanSeverity/DeleteHRLoanSeverity';
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